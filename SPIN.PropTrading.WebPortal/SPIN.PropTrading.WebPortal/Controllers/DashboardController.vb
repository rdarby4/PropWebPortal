Imports System.Web.Mvc

Namespace Controllers
    Public Class DashboardController
        Inherits Controller


#Region" Pages "

        ' GET: Dashboard
        Function Index() As ActionResult
            Call InitDashoard()
            Return View()
        End Function

        Function BaseBallChart() As ActionResult
            Call LoadBaseballMonthly()
            Return View()
        End Function

        Function TennisChart() As ActionResult
            Call LoadTennisMonthly()
            Return View()
        End Function

        Function ShowError(byval errorMsg as PortalError) As ActionResult
            ViewData("Msg") = errorMsg.Message
            Return View()
        End Function

#End Region

        Private Sub InitDashoard()
            Call LoadDashboardPnL()
            Call GetMostRecentTasks()
            Call LoadBetsTicker()
            Call LoadDailySummary()
        End Sub

        Private Sub LoadDashboardPnL()

            ViewData("Pnl1Week£") = _td.PnlToDate(Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss"), "£")
            ViewData("Pnl1Week%") = _td.PnlToDate(Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss"), "%")
            ViewData("Pnl1Month£") = _td.PnlToDate(Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss"), "£")
            ViewData("Pnl1Month%") = _td.PnlToDate(Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss"), "%")
            ViewData("Pnl1Year£") = _td.PnlToDate(Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss"), "£")
            ViewData("Pnl1Year%") = _td.PnlToDate(Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss"), "%")

            ViewData("Baseball1Week£") = _td.PnlToDate(Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss"), "£", "Baseball")
            ViewData("Baseball1Week%") = _td.PnlToDate(Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss"), "%", "Baseball")
            ViewData("Baseball1Month£") = _td.PnlToDate(Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss"), "£", "Baseball")
            ViewData("Baseball1Month%") = _td.PnlToDate(Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss"), "%", "Baseball")
            ViewData("Baseball1Year£") = _td.PnlToDate(Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss"), "£", "Baseball")
            ViewData("Baseball1Year%") = _td.PnlToDate(Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss"), "%", "Baseball")

            ViewData("Tennis1Week£") = _td.PnlToDate(Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss"), "£", "Tennis")
            ViewData("Tennis1Week%") = _td.PnlToDate(Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss"), "%", "Tennis")
            ViewData("Tennis1Month£") = _td.PnlToDate(Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss"), "£", "Tennis")
            ViewData("Tennis1Month%") = _td.PnlToDate(Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss"), "%", "Tennis")
            ViewData("Tennis1Year£") = _td.PnlToDate(Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss"), "£", "Tennis")
            ViewData("Tennis1Year%") = _td.PnlToDate(Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss"), "%", "Tennis")
        End Sub

        Private sub LoadDailySummary()

            ViewData("BetsPlaced") = _td.GetDailyBetsPlaced
            ViewData("DailyPnL£") = _td.GetDailyPnlGbp
            ViewData("DailyPnL%") = _td.GetDailyPnlPrcnt

        End sub

        Private Sub LoadBetsTicker()

            Dim l = _td.GetRecentBets
            ViewData("RecentBets") = New SelectList(l)

        End Sub

        Private sub LoadDashboardFailedTasks()
            Dim tasks As New List(Of String)
            tasks = _tad.GetFailedTasksByDate(Today.ToString("yyyy-MM-dd 00:00:00"))
            If tasks.Count = 0 Then tasks.Add("No Failed Tasks, " & Now.ToString("yyyy-MM-dd hh:mm:ss"))
            ViewData("FailedTasks") = New SelectList(tasks)

        End sub
        
        Private sub LoadBaseballMonthly()

            Dim bChart = _td.BaseBallMonthlyPnl
            ViewData("BaseballMonthlyChart") = New SelectList(bChart)

        End sub

        Private sub LoadTennisMonthly()

            Dim bChart = _td.TennisMonthlyPnl
            ViewData("TennisMonthlyChart") = New SelectList(bChart)

        End sub

        Private Sub GetMostRecentTasks()

            Dim tasks As New Dictionary(Of String(), Boolean)
            tasks = _tad.GetMostRecentTasks(25)
            If tasks.Count = 0 Then 
                tasks.Add({"An Error Occured", Now.ToString}, False)
            Else
                ViewData("RecentTasks") = tasks
            End If

        End Sub


        private ReadOnly _td As New TradingData
        Private ReadOnly _tad As New TaskData

    End Class

End Namespace