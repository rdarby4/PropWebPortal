Public Class TaskDetail

    Public Sub New(ByRef tName As String)
        Name = tName
    End Sub

    Public Property Name As String

    Public Property LastRun As String
    
    Public Property LastFailure As String

    Public Property LastSuccess As String

    Public Property ConsecutiveFailures As List(Of String)

    Public Property ConsecutiveFailuresCount As Integer

    Public Property Recurring As Boolean

    Public Property StartTime As String

    Public Property EndTime As String

    Public Property TimesRunToday As Integer

    Public Property TimesSucceededToday As Integer

    Public Property TimesFailedToday As Integer

    Public Property DailyStats As Dictionary(Of String, Integer)

    Public property Frequency As integer

    Public Property LastExecutionTime As Double

End Class
