Imports System.IO
Imports SPIN.PropTrading.Common_VB

Public Class MonitoringData

#Region " Constants "

    Private Const PendingFixturesTable As String = "[$$SPORT$$].[dbo].[tblPendingFixtures]"
    Private Const PendingFixtureLineupsTable As String = "[$$SPORT$$].[dbo].[tblPendingFixtureLineups]"
    Private Const PlayersTable As String = "[$$SPORT$$].[dbo].[tblPlayers]"
    Private Const BBrefSquadsTable As String = "[$$SPORT$$].[dbo].[tblSquads_BBRef]"
    Private Const LatestPricesTable As String = "[$$SPORT$$Trading].[dbo].[tbl$$SPORT$$Prices_PI]"

#End Region

#Region " Public "
    Public Function GetDailySummary(ByRef sport As string) As List(Of SportMonitoring)

        Dim db = PendingFixturesTable.Replace("$$SPORT$$", sport)
        Dim res As New List(Of SportMonitoring)

        Select Case True
            Case sport = "Basketball", sport = "Baseball"
                Dim dt = _db.GetTable("SELECT * FROM " & db & " WHERE FixtureKey Like '%" & Today.ToString("dd-MMM-yyyy") & "%'")
                For Each r As DataRow In dt.Rows
                    Dim s As New SportMonitoring
                    s.FixtureKey = r("FixtureKey")
                    s.KickOff = r("UkFixtureDateTime")
                    s.LastPriceUpdate = GetLastPriceUpdate(sport, s.FixtureKey)
                    s.HasLineups = HasLineups(sport, s.FixtureKey)
                    s.LastSquadScrape = GetLastSquadUpdate(s.FixtureKey.Substring(0, 3), s.FixtureKey.Substring(6, 3), sport)
                    s.Prices = GetLatestPriceList(s.FixtureKey, sport)
                    s.HomeLineup = GetLineup(s.FixtureKey, s.FixtureKey.Substring(0, 3), sport)
                    s.AwayLineup = GetLineup(s.FixtureKey, s.FixtureKey.Substring(6, 3), sport)
                    s.LastSimmed = GetLastSimmed(sport, s.FixtureKey)
                    res.Add(s)
                Next

            Case sport = "Tennis"
                Dim dt = _db.GetTable("Select * FROM " & db & " WHERE FixtureDateTime LIKE '%" & Today.ToString("yyyy-MM-dd") & "%'")
                For Each r As DataRow In dt.Rows
                    Dim s As New SportMonitoring
                    s.FixtureKey = r("FixtureKey")
                    s.KickOff = r("FixtureDateTime")
                    s.LastPriceUpdate = GetLastPriceUpdate(sport, s.FixtureKey)
                    s.Prices = GetLatestPriceList(s.FixtureKey, sport)
                    s.LastSimmed = GetLastSimmed(sport, s.FixtureKey)
                    res.Add(s)
                Next

            Case sport = "Darts"
                Dim dartsDb = "[DartsTrading].[dbo].[tblDartsMarketDetails_BF]"
                Dim dt = _db.GetTable("SELECT * FROM " & dartsDb & " WHERE FixtureKey Like '%" & Today.ToString("dd-MMM-yyyy") & "%'")
                For Each r As DataRow In dt.Rows
                    Dim s As New SportMonitoring
                    s.FixtureKey = r("FixtureKey")
                    ' s.KickOff = r("FixtureDateTime")
                    s.LastPriceUpdate = GetLastPriceUpdate(sport, s.FixtureKey)
                    s.Prices = GetLatestPriceList(s.FixtureKey, sport)
                    s.LastSimmed = GetLastSimmed(sport, s.FixtureKey)
                    res.Add(s)
                Next


        End Select

        Return Res

    End Function

    Public Function GetMissingResultsData(byref sport As String) As List(Of MissingReultsData)

        Dim data As New List(Of MissingReultsData)

        Select Case True
            Case sport = "Basketball"
                For each s In _basketballDataSources
                    Dim d = ResultsDataMissing(sport, s)
                    if Not d Is Nothing Then data.Add(d)
                Next 
        End Select

        Return data

    End Function

#End Region

#Region " Private "

    Private function ResultsDataMissing(byref sport As string, byref datasource As string)As MissingReultsData

        Dim m As New MissingReultsData(datasource)
        Dim sql As String

        Select Case True
            Case sport = "Basketball"
                'sql = "Select fb.FixtureURL as BBref, fn.FixtureKey as FixtureKey, fs.FixtureId as Stuffer, c.FixtureURL as coaches, n.fixtureId as NBA
                '    from [Basketball].[dbo].[tblFixtures_BBRef] fb left join [Basketball].[dbo].[tblFixtureNameMap] fn on fb.FixtureURL = fn.BBRefFixtureUrl left JOIN [Basketball].[dbo].[tblFixtures_Stuffer] fs on fs.FixtureId = fn.NbaStufferId left join [Basketball].[dbo].[tblFixtureCoaches] c on c.FixtureURL = fn.BBRefFixtureUrl left join [Basketball].[dbo].[tblFixtureBoxScoreAdvanced_NBA] n on n.fixtureid = fn.NbaStufferId
                '    WHERE fb.Season = " & _thisSeasonBasketball & " AND " & GetMissingDataFilter(sport, datasource)
                sql = "Select Distinct fn.FixtureKey as FixtureKey
                    from [Basketball].[dbo].[tblFixtures_BBRef] fb left join [Basketball].[dbo].[tblFixtureNameMap] fn on fb.FixtureURL = fn.BBRefFixtureUrl left JOIN [Basketball].[dbo].[tblFixtures_Stuffer] fs on fs.FixtureId = fn.NbaStufferId left join [Basketball].[dbo].[tblFixtureCoaches] c on c.FixtureURL = fn.BBRefFixtureUrl left join [Basketball].[dbo].[tblFixtureBoxScoreAdvanced_NBA] n on n.fixtureid = fn.NbaStufferId
                    WHERE fb.Season = " & _thisSeasonBasketball & " AND " & GetMissingDataFilter(sport, datasource)
        End Select
        
        Dim dt = _db.GetTable(sql)

        If dt.Rows.Count > 0 Then
            For each dr As DataRow In dt.Rows
                m.Fixtures.Add(dr("FixtureKey"))
            Next
            Return m
        Else
            Return Nothing
        End If

    End function

    Private function GetMissingDataFilter(byref sport As String, byref datasource As string)

        Dim s As string

        Select Case true
            Case sport = "Basketball"
                Select Case True 
                    Case datasource = "NBA Stuffer"
                        s = "fs.FixtureId is null"
                    Case datasource = "NBA Adcvanced Stats"
                        s = "n.FixtureID is null"
                    Case datasource = "BBRef Coaches"
                        s = "c.FixtureUrl is null"
                    Case datasource = "BBRef Results"
                        s = "fb.FixtureURL is null"
                End Select
        End Select

        Return s

    End function

    Private Function GetLineup(ByRef fixturekey As String, ByRef team As String, ByRef sport As String) As List(Of String)

        Dim db = PendingFixtureLineupsTable.Replace("$$SPORT$$", sport)
        Dim l = New List(Of String)
        Dim dt = _db.GetTable("SELECT * FROM " & db & " WHERE fixturekey = '" & fixturekey & "' And Team = '" & team & "'")

        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                l.Add(dr("Position") & ": " & PlayerNameFromId(dr("PlayerId"), sport))
            Next
        End If

        Return l

    End Function

    Private Function PlayerNameFromId(ByRef id As Integer, ByRef sport As String) As String

        Dim db = PlayersTable.Replace("$$SPORT$$", sport)
        Dim dt = _db.GetTable("Select * FROM " & db & "WHERE PlayerId = " & id)

        Return dt(0)("Firstname") & " " & dt(0)("Surname")

    End Function

    Private Function GetLatestPriceList(ByRef fk As string,  ByRef sport as string) As Dictionary(Of String, Double)

        Dim db = LatestPricesTable.Replace("$$SPORT$$", sport)
        Dim d As New Dictionary(Of String, Double)
        Dim dt = _db.GetTable("SELECT * FROM " & db & " WHERE FixtureKey = '" & fk & "' ORDER BY Timestamp DESC")
        Dim m 
        Dim ts

        If dt.Rows.Count > 0 Then
            ts = dt(0)("Timestamp")
            For each dr As DataRow In dt.Rows
                m = dr("Market") & ", " & dr("Selection") & ", " & dr("Handicap")
                If Not d.ContainsKey(m) Andalso dr("Timestamp") = ts Then
                    d.Add(m, dr("Odds"))
                End If

            Next
           
        End If

        Return d

    End Function

    Private function GetLastPriceUpdate(byref sport As string, byref fk As string) As String

        Dim db = LatestPricesTable.Replace("$$SPORT$$", sport)

        Dim dt = _db.GetTable("SELECT TOP 1 * FROM " & db & " WHERE FixtureKey ='" & fk & "' ORDER BY Timestamp DESC")

        If dt.Rows.Count > 0 Then 
            Return dt(0)("Timestamp").ToString
        Else
            Return "No Prices Yet"
        End If

    End function

    Private Function HasLineups(byref sport, byref fixtureKey) As Dictionary(Of String, Boolean)
        Dim db = PendingFixtureLineupsTable.Replace("$$SPORT$$", sport)
        Dim d As New Dictionary(Of String, Boolean)

        Dim dt = _db.GetTable("Select * FROM " & db & " WHERE FixtureKey ='" & fixtureKey & "'")

        If dt.Rows.Count = 0 Then
            d.Add("All Sources", False)
        Else
            For Each dr As DataRow In dt.Rows
                If Not d.ContainsKey(dr("Source")) Then d.Add(dr("Source"), True)
            Next
        End If

        Return d

    End Function

    Private Function GetLastSquadUpdate(byref home As String, byref away As String, ByRef sport As string) As Dictionary(Of String, Date)

        Dim db = BBrefSquadsTable.Replace("$$SPORT$$", sport)
        Dim d As New Dictionary(Of String, Date)
        Dim dtHome = _db.GetTable("SELECT Top 1 Timestamp from " & db & " WHERE Team = '" & home & "'")
        Dim dtAway = _db.GetTable("SELECT Top 1 Timestamp from " & db & " WHERE Team = '" & away & "'")

        d.Add(home, dtHome(0)(0))
        d.Add(away, dtAway(0)(0))

        Return d

    End Function

    Private Function GetLastSimmed(ByRef sport As string, ByRef fixtureKey As String) As String

        Dim path = "\\modelling.sig.ads\PropTrading\Trading\" & sport & "\ModelOutput\" & Now.Year & "\" & Now.Month.ToString("00") & "\" & Now.Day.ToString("00") & "\" & fixtureKey

        If Directory.Exists(path) Then
            Return Directory.GetLastWriteTime(path).ToShortDateString
        Else
            Return "Not Simmed"
        End If

    End Function

#End Region

#Region " Variables "

    Private ReadOnly _db As New Db
    Private _thisSeasonBasketball As Integer = Now.AddMonths(4).Year
    Private _basketballDataSources As String() = {"NBA Stuffer", "NBA Adcvanced Stats", "BBRef Coaches", "BBRef Results"}

#End Region

End Class
