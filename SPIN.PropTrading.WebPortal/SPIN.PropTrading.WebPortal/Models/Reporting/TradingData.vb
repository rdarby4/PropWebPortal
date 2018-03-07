Imports SPIN.PropTrading.Common_VB

Public Class TradingData
    
#Region" Constants "

    Private Const SettledBetsTable As String = "[$$TRADINGDB$$].[dbo].[tblSettledBets]"
    Private const PnlToDateSql As String = " SELECT COUNT(distinct s.Fixture+s.Market+s.Selection) as 'Bets Placed', " & _
                                            "SUM(PnL) as 'PnL (GBP)', " & _
                                           "SUM([staked]) as 'Staked (GBP)', " & _
                                           "SUM(PnL) / SUM(ABS([pnl])) * 100 as 'Pnl (%)'  " & _
                                           "FROM " & SettledBetsTable & " s " & _
                                           "WHERE s.Settled >= '$$STARTDATE$$' " & _
                                           "AND Sport = '$$SPORT$$' "

    Private Const PnLByMonthSql As String = "SELECT YEAR(Settled) as 'Year', Month(Settled) as 'Month', " & _
                                         "COUNT(distinct Fixture+Market+Selection) as 'Bets Placed', " & _
                                         "SUM(PnL) as 'PnL (GBP)', " & _
                                         "SUM([staked]) as 'Staked (GBP)', " & _
                                         "SUM(PnL) / SUM(ABS([pnl])) * 100 as 'Pnl (%)'  " & _
                                         "FROM " & SettledBetsTable & _
                                         " where Settled >= '$$STARTDATE$$' " & _
                                         "AND   Sport = '$$SPORT$$' " & _
                                         "group by year(Settled) ,month(Settled) " & _
                                         "order by 'Year','Month'"

    Private Const DailyBetsPLacedSql As String = "SELECT COUNT(distinct Fixture+Market+Selection) as 'Bets Placed' FROM " & SettledBetsTable &
                                                 " WHERE Settled >= '$$FDATE$$' AND Settled <= '$$TDATE$$'" 

    Private Const DailyPnLSql As String = "SELECT SUM(PnL) as 'PnL (GBP)',  case when SUM(ABS([pnl])) > 0 then SUM(PnL) / SUM(ABS([pnl]))  * 100 else 0 End as 'Pnl (%)' FROM " & SettledBetsTable & 
                                          "WHERE Settled >= '$$FDATE$$' AND Settled <= '$$TDATE$$'"

    Private Const RecentBetsSql As String = "SELECT TOP $$N$$ Fixture, Market, Selection, Price, PnL, Settled From " &
                                            SettledBetsTable & "ORDER BY Settled DESC"

    Private Const GetMarketTypeSql As String = "SELECT DISTINCT Market FROM "  & SettledBetsTable

#End Region
                                            
#Region" Public "

    'all sports
    Public function PnlToDate(ByVal startDate As string, ByVal poundpercent As string) As Double
        Dim total = 0#
        
        For Each sport In _sports

            total += PnlToDate(startDate, poundpercent, sport)

        Next

        Return total

    End function

    'sport specific
    Public function PnlToDate(ByVal startDate As string, ByVal poundpercent As string, byval sport As string) As Double
        Dim total = 0#
               
            Dim sql = PnlToDateSql.Replace("$$STARTDATE$$", startDate)
            sql = sql.Replace("$$TRADINGDB$$", sport & "Trading") ' revisit this
            sql = sql.Replace("$$SPORT$$", sport)
            Dim dt = _db.GetTable(sql)
            Dim col = GetCol(poundpercent)

            If Not isDBNull(dt.Rows(0).Item(col)) Then total += math.Round(dt.Rows(0).Item(col), 2)

        Return total
    End function

    Public function RunReport(byref sports As String(), byref from As String, byref [to] As String, byref markettype As String(), byref groupby as String, byref orderby as String(), byref stakefrom As Double?, byref staketo? As Double, byref fk As String, byref sql As string) As DataSet

        Dim ds As New DataSet
        Dim db As String, order As string = "", st = ""
        If Not stakefrom Is Nothing Then
            st = " AND Staked >= " & stakefrom
            If Not staketo Is Nothing Then 
                st = st & " AND Staked <= " & staketo
            End If
        End If
        Call FormatVarchar(markettype)
        If Not orderby Is Nothing Then 
            order = " ORDER BY " & String.Join(",", orderby)
        End If
        For each sport In sports
            db = SettledBetsTable.Replace("$$TRADINGDB$$", sport & "Trading")
            Dim fksql As string = "SELECT Fixture, Market, Selection, Price, ModelOdds, Staked, Settled, PnL, GameState FROM " & db & " WHERE Market in (" & String.Join(",", markettype) & ")" & st & " AND Fixture = '" & fk & "'" & order
            Dim groupbysql As String ="SELECT " & groupby & ", Count(*) as " & groupby & "bets, Sum(Staked) as " & groupby & "Staked, Sum(Pnl) as " & groupby & "Pnl FROM" & db & " WHERE Market in (" & String.Join(",", markettype) & ") AND Settled >= '" & from & "' AND Settled <= '" & [to] & "' GROUP BY " & groupby
            Dim dt
            If groupby Is Nothing OrElse String.IsNullOrEmpty(groupby) Then
                If String.IsNullOrEmpty(fk) Then 
                    dt = _db.GetTable("SELECT Fixture, Market, Selection, Price, ModelOdds, Staked, Settled, PnL, GameState FROM " & db & " WHERE Market in (" & String.Join(",", markettype) & ") AND Settled >= '" & from & "' AND Settled <= '" & [to] & "'" & st & order)
                    sql = "SELECT Fixture, Market, Selection, Price, ModelOdds, Staked, Settled, PnL, GameState FROM " & db & " WHERE Market in (" & String.Join(",", markettype) & ") AND Settled >= '" & from & "' AND Settled <= '" & [to] & "'" & st & order
                    Else
                    dt = _db.GetTable(fksql)
                    sql = fksql
                End if
            Else
                dt = _db.GetTable(groupbysql)
                sql = groupbysql
            End If
            dt.TableName = sport
            ds.Tables.Add(dt)
        Next

        Return ds

    End function

    Private sub FormatVarchar(byref str As String())
        For each s In str
            s.Insert(0,"'")
            s.Insert(s.Length, "'")
        Next
    End sub

    Public function BaseBallMonthlyPnl() As List(Of String())

        Return GetChartData("MonthlyPnL", "Baseball", Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss"))

    End function

    Public function TennisMonthlyPnl() As List(Of String())

        Return GetChartData("MonthlyPnL", "Tennis", Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss"))

    End function


    Public function GetRecentBets As List(Of String)

        return GetSettledBets("Baseball", 10)
     
    End function

    Public function GetRecentBets(byref sport as string, byref amnt as integer) As List(Of String)

        return GetSettledBets(sport, amnt)
     
    End function

    Public Function GetDailyBetsPlaced() As integer
        Dim total = 0
        'Dim d As New Date(2017,4,18)
        For Each sport In _sports
            total += GetBetsPlaced(sport, Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00"), Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59"))
            'total += GetBetsPlaced(sport, d.ToString("yyyy-MM-dd 00:00:00"), d.ToString("yyyy-MM-dd 23:59:59"))
        Next

        return total
    End Function

    Public Function GetDailyPnlGbp() As Double
        Dim total = 0#
        'Dim d As New Date(2017,4,18)
        For Each sport In _sports
            total += GetDailyPnL(sport, "£", Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00"), Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59"))
            'total += GetDailyPnL(sport, "£", d.ToString("yyyy-MM-dd 00:00:00"), d.ToString("yyyy-MM-dd 23:59:59"))
        Next

        Return Math.Round(total, 2)
    End Function

    Public Function GetDailyPnlPrcnt() As Double
        Dim total = 0#
       ' Dim d As New Date(2017,4,18)
        For Each sport In _sports
            total += GetDailyPnL(sport, "%", Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00"), Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59"))
           ' total += GetDailyPnL(sport, "%", d.ToString("yyyy-MM-dd 00:00:00"), d.ToString("yyyy-MM-dd 23:59:59"))
        Next

        Return Math.Round(total, 2)
    End Function

    Public Function GetReprtFilterOptions(byref filter As String, byref sport As string) As List(Of String)
        Dim l As New List(Of String)
        Dim sql As string 

        Select Case True
            Case filter = "MarketType"
                sql = GetMarketTypeSql.Replace("$$TRADINGDB$$", sport & "Trading")

        End Select

        Dim dt = _db.GetTable(sql)

        For each r As DataRow In dt.Rows
            l.Add("'" & r(0) & "'")
        Next

        Return l

    End Function

    Private Function GetOpenBets(ByRef sport As string) As List(Of String)



    End Function

#End Region

#Region" Private "

    Private Function GetDailyPnL(ByRef sport As string, byref poundPercent As String, byref from As String, byref [to] As string) As Double
        Dim t = 0#

        Dim sql = DailyPnLSql.Replace("$$TRADINGDB$$", sport & "Trading")
        sql = sql.Replace("$$FDATE$$", from)
        sql = sql.Replace("$$TDATE$$", [to])
        Dim dt = _db.GetTable(sql)
        Dim col = GetCol(poundpercent)
        
        If dt.Rows.Count > 0 AndAlso Not IsDBNull(dt.Rows(0)(col)) Then
            t += dt.Rows(0)(col)
        End If

        Return t

    End Function

    Private function GetBetsPlaced(ByRef sport As String, byref from As String, byref [to] As string) As Integer

        Dim t = 0

        Dim sql = DailyBetsPLacedSql.Replace("$$TRADINGDB$$", sport & "Trading")
        sql = sql.Replace("$$FDATE$$", from)
        sql = sql.Replace("$$TDATE$$", [to])
        Dim dt = _db.GetTable(sql)

        If dt.Rows.Count > 0 Then
            t += dt.Rows(0)(0)
        End If

        Return t

    End function

    Private function GetSettledBets(ByRef sport As string, ByRef amnt as integer) As List(Of String)

        Dim l As New List(Of String)
        Dim sql As String = RecentBetsSql.Replace("$$TRADINGDB$$", sport & "Trading")
        sql = sql.Replace("$$N$$", amnt)
        Dim dt = _db.GetTable(sql)

        For each dr As DataRow In dt.Rows
            l.Add(dr(0) & "," & dr(1) & "," & dr(2) & "," & Math.Round(dr(3), 2) & "," & Math.Round(dr(4), 2) & "," & dr(5).ToString())
        Next

        Return l
    End function

    Private Function GetChartData(byref chartType As string, byref sport As string, byref startDate As string) As List(Of String())
      
        Dim c As New List(Of String())
        Dim x(11) As String
        Dim y(11) As String

        Select Case True
            Case chartType = "MonthlyPnL"
                Dim sql As String = PnLByMonthSql.Replace("$$STARTDATE$$", startDate)
                sql = sql.Replace("$$TRADINGDB$$", sport & "Trading") ' revisit this
                sql = sql.Replace("$$SPORT$$", sport)
                Dim dt = _db.GetTable(sql)
   
                For i = 0 To dt.rows.Count - 1
                    x(i) = dt.Rows(i)(1)
                    y(i) = dt.Rows(i)(3)
                Next
        End Select

        c.Add(x)
        c.add(y)

        Return c
        
    End Function

    Private Function GetCol(ByRef poundPercent As string)As string

        If poundPercent = "£" Then
            Return "Pnl (GBP)"
        ElseIf poundPercent = "%" then
            Return "Pnl (%)"
        End If

        Return ""

    End Function

    Private function CustomPnlReport() As DataTable



    End function

   

 

#End Region

   ' Private Readonly _sports() as String = {"Baseball", "Tennis", "Darts"}
    Private Readonly _sports() as String = {"Baseball"}
    Private ReadOnly _db As New Db

End Class
