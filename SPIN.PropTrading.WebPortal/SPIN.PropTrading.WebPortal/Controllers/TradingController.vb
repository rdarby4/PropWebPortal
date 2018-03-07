Imports System.Web.Mvc
Imports Newtonsoft.Json

Namespace Controllers
    Public Class TradingController
        Inherits Controller

        ' GET: Trading
        Function Index() As ActionResult
            Try
                Call GetReportFilters()
                Return View()
            Catch ex As Exception
                Dim e = New PortalError(ex.Message)
                Return RedirectToAction("ShowError", "Dashboard", e)
            End Try
        End Function

        Function GetRecentBets() as ActionResult
            Try
                ViewData("Sports") = _Sports
                Return PartialView()
            Catch ex As Exception
                Dim e = New PortalError(ex.Message)
                Return RedirectToAction("ShowError", "Dashboard", e)
            End Try
        End Function

        Function RecentBets(ByVal sports As String()) As ActionResult
            Try
                Dim l As New List(Of List(Of String))
                For Each s In sports             
                    l.Add(_tr.GetRecentBets(s, 20))
                Next
                ViewData("Bets") = l
                Return PartialView()
            Catch ex As Exception
                Dim e = New PortalError(ex.Message)
                Return RedirectToAction("ShowError", "Dashboard", e)
            End Try
        End Function

        Function OpenBets As ActionResult
            Try
                ViewData("Sports") = _Sports
                Return View()
            Catch ex As Exception
                Dim e = New PortalError(ex.Message)
                Return RedirectToAction("ShowError", "Dashboard", e)
            End Try
        End Function

        Function GetOpenBets(ByVal sports() As String) As ActionResult
            'get open bets for each sport, add to list, send list to view
            'Return View()
        End Function

        Function ShowReport(byval sports As String(), ByVal from As String, byval [to] As String, ByVal markettype As String(), byval groupby as String, byval orderby as String(), ByVal detailed As String, ByVal stakeSizeFrom As Double?, ByVal stakeSizeTo As Double?, byval fk As String) As ActionResult
            Dim sql = ""
            Try
            ViewData("Results") = _tr.RunReport(sports, from, [to], markettype, groupby, orderby, stakeSizeFrom, stakeSizeTo, fk, sql)
            ViewData("Sql") = sql
            If detailed = "True" Then
                ViewData("Detail") = True
            Else
                ViewData("Detail") = false
            End If
            If Not groupby Is Nothing OrElse not String.IsNullOrEmpty(groupby) Then
                ViewData("GroupBy") = True
            Else
                ViewData("GroupBy") = False
            End If 
            Return View()
            Catch ex As Exception
                Dim e = New PortalError(ex.Message)
                Return RedirectToAction("ShowError", "Dashboard", e)
            End Try
        End Function

        Private Sub GetReportFilters()

           ' ViewData("Markets") = _tr.GetReprtFilterOptions("MarketType", "Baseball")

        End Sub

        Public Function GetMarkets(byval sport As string) As String

            Dim s As List(Of String) =  _tr.GetReprtFilterOptions("MarketType", sport)
            return JsonConvert.SerializeObject(s)

        End Function

        Private ReadOnly _tr as new TradingData
        Private ReadOnly _Sports() as String = {"Basketball", "Baseball", "Darts", "Tennis"}
        
    End Class

End Namespace