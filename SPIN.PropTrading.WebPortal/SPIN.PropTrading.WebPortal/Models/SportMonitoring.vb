Public Class SportMonitoring

    Public Property FixtureKey As String
    Public Property KickOff As DateTime
    Public Property HasLineups As Dictionary(Of String, Boolean)
    Public Property LastPriceUpdate As String
    Public Property LastSquadScrape As Dictionary(Of String, Date)
    Public Property Prices As Dictionary(Of String, Double)
    Public Property HomeLineup As List(Of String)
    Public Property AwayLineup As List(Of String)
    Public Property LastSimmed As String

End Class
