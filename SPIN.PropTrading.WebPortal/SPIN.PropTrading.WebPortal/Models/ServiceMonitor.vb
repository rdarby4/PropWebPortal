
Imports System.ServiceProcess
Imports System.Web.Configuration
Imports SPIN.PropTrading.Common_VB

Public Class ServiceMonitor

    Private Const DCDataMachineName As String = "scmappdata01.gb.sportingindex.com"
    Private Const DCTradingMachineName As String = "scmappdata02.gb.sportingindex.com"
    Private Const Trading01MachineName As String = "scmapptrading01.gb.sportingindex.com"
    Private Const Trading02MachineName As String = "scmapptrading02.gb.sportingindex.com"
    Private Const RabbitMachineName As String = "scmapprabbit01.gb.sportingindex.com"

    Private Const DataCollationServiceName As String = "SPIN.PropTrading.DataCollation"
    Private Const BetEngineServiceName As String = "SPIN.PropTrading.BetEngine"
    Private Const ExchangeMontorServiceName As String = "SPIN.PropTrading.ExchangeMonitor"
    Private Const TennisPointsHubServiceName As String = "SPIN.PropTrading.Tennis.PointsFeed"
    Private Const TennisMarketMakerServiceName As String = "SPIN.PropTrading.Tennis.MarketMaker"
    Private Const DalPriceFeedServiceName As String = "SPIN.PropTrading.DalPriceFeed"
    Private Const InPlayBaseballServiceName As String = "SPIN.PropTrading.InPlayBaseballFeed"


    Public Function DcData1Details() As ServiceDetail

        Dim d As New ServiceDetail
        d.ServiceName = DataCollationServiceName
        d.MachineName = DCDataMachineName
        d.ServiceResponsibilities = "Data Collation - DATA01 - DATA "
        d.ServiceStatus = CheckIsRunning(d.ServiceName, d.MachineName, True)
        d.LastChecked = Now.ToString

        Return d

    End Function

    Public Function DcData2Details() As ServiceDetail

        Dim d As New ServiceDetail
        d.ServiceName = DataCollationServiceName
        d.MachineName = DCTradingMachineName
        d.ServiceResponsibilities = "Data Collation - DATA02 - TRADING "
        d.ServiceStatus = CheckIsRunning(d.ServiceName, d.MachineName, True)
        d.LastChecked = Now.ToString

        Return d

    End Function

    Public Function TradingBetEngineDetails() As ServiceDetail

        Dim d As New ServiceDetail
        d.ServiceName = BetEngineServiceName
        d.MachineName = Trading01MachineName
        d.ServiceResponsibilities = "TRADING - Bet Engine"
        d.ServiceStatus = CheckIsRunning(d.ServiceName, d.MachineName, True)
        d.LastChecked = Now.ToString

        Return d

    End Function

    Public Function TradingExchangeMonitorDetails() As ServiceDetail

        Dim d As New ServiceDetail
        d.ServiceName = ExchangeMontorServiceName
        d.MachineName = Trading01MachineName
        d.ServiceResponsibilities = "TRADING - Exchange Monitor"
        d.ServiceStatus = CheckIsRunning(d.ServiceName, d.MachineName, True)
        d.LastChecked = Now.ToString

        Return d

    End Function

    Public Function TennisPointsHubDetails() As ServiceDetail

        Dim d As New ServiceDetail
        d.ServiceName = TennisPointsHubServiceName
        d.MachineName = Trading02MachineName
        d.ServiceResponsibilities = "TENNIS - Points Feed"
        d.ServiceStatus = CheckIsRunning(d.ServiceName, d.MachineName, True)
        d.LastChecked = Now.ToString

        Return d

    End Function

    Public Function TennisMarketMakerDetails() As ServiceDetail

        Dim d As New ServiceDetail
        d.ServiceName = TennisMarketMakerServiceName
        d.MachineName = Trading02MachineName
        d.ServiceResponsibilities = "TENNIS - Market Maker"
        d.ServiceStatus = CheckIsRunning(d.ServiceName, d.MachineName, True)
        d.LastChecked = Now.ToString

        Return d

    End Function

    Public Function DalPriceFeedDetails() As ServiceDetail

        Dim d As New ServiceDetail
        d.ServiceName = DalPriceFeedServiceName
        d.MachineName = RabbitMachineName
        d.ServiceResponsibilities = "DAL Price Feed"
        d.ServiceStatus = CheckIsRunning(d.ServiceName, d.MachineName, True)
        d.LastChecked = Now.ToString

        Return d

    End Function

    Public Function BaseballInPlayFeedDetails() As ServiceDetail

        Dim d As New ServiceDetail
        d.ServiceName = InPlayBaseballServiceName
        d.MachineName = RabbitMachineName
        d.ServiceResponsibilities = "In Play Baseball Feed"
        d.ServiceStatus = CheckIsRunning(d.ServiceName, d.MachineName, True)
        d.LastChecked = Now.ToString

        Return d

    End Function

    Public Sub RestartDC() ' restarts both dc services

    End Sub

    Private Function CheckIsRunning(ByRef serviceName As String, ByRef machineName As String, ByRef gbDomain As Boolean) As Enums.ServiceStatus

        Dim sc
        Dim gbpw As String = GetGbPw()

        Dim imp As New Impersonator
        If gbDomain Then
            imp.Impersonate("GB", "941MCompton", _crypto.Decrypt(gbpw))
        Else
           ' imp.Impersonate("10.13.101.101", "winadmin", _crypto.Decrypt(ospw))
        End If
        sc = New ServiceController(serviceName, machineName)
        If sc.Status = 4 Then
            imp.Undo()
            Return Enums.ServiceStatus.Running
        Else
            imp.Undo()
            Return Enums.ServiceStatus.Stopped
        End If

    End Function

    Private Function GetGbPw()

        Return _db.GetTable("SELECT * FROM [Scheduler].[dbo].[tblVmPw]")(0)(0)

    End Function

    Private ReadOnly _db As New Db
    Private ReadOnly _crypto As New Cryptography
End Class
