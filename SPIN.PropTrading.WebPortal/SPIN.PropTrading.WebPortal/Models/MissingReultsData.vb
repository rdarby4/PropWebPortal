Public Class MissingReultsData

    Public Property DataSource As String
    Public Property Fixtures As New List(Of String)

    Public Sub New(byref name As string)
        DataSource = name
    End Sub

End Class
