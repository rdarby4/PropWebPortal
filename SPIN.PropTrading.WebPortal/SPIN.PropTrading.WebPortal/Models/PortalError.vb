Public Class PortalError

    Public property Message As String

    Public Sub New(byref msg As string)
        Message = msg
    End Sub

    Public Sub New
        Message = ""
    End Sub

End Class
