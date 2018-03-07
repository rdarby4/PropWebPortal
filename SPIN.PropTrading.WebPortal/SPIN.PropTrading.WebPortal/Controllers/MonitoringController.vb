Imports System.Web.Mvc

Namespace Controllers
    Public Class MonitoringController
        Inherits Controller

        ' GET: Monitoring
        Function Index(id As String) As ActionResult
            Try
                ViewData("Sport") = id
                Call GetDailySummary(id)
                Return View()
            Catch ex As Exception
                Dim e = New PortalError(ex.Message)
                Return RedirectToAction("ShowError", "Dashboard", e)
            End Try
        End Function

        Function ProcessMonitoring() As ActionResult

            Dim s As New List(Of ServiceDetail)
            Try 
                s.Add(_sm.DcData1Details)
                s.Add(_sm.DcData2Details)
                s.Add(_sm.TradingBetEngineDetails)
                s.Add(_sm.TradingExchangeMonitorDetails)
                s.Add(_sm.TennisPointsHubDetails)
                s.Add(_sm.TennisMarketMakerDetails)
                s.Add(_sm.DalPriceFeedDetails)
                s.Add(_sm.BaseballInPlayFeedDetails)
                ViewData("ServiceData") = s
                Return View()
            Catch ex As Exception
               Dim e = New PortalError(ex.Message)
               Return RedirectToAction("ShowError", "Dashboard", e)
            End Try

        End Function

        Private Sub GetDailySummary(sport As string)

            Dim s = _md.GetDailySummary(sport)
            ViewData("Summary") = s
            Dim missing = _md.GetMissingResultsData(sport)
            ViewData("MissingResults") = missing

       End Sub

        Private _md As New MonitoringData
        Private _sm As New ServiceMonitor

    End Class
End Namespace