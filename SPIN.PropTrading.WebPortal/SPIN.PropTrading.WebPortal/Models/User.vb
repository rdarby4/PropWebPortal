Imports SPIN.PropTrading.Common_VB

Public Class User
    Private const UsersTable As String = "[Scheduler].[dbo].[tblWebPortalUser]"

    Public Property Username As String

    Public Property Password As String


    Public Function IsValid(ByRef u As String, ByRef p As String) As Boolean

        p = _crypto.Encrypt(password)
        if _db.RowExists(String.Format("SELECT * FROM {0} WHERE Username = '{1}' AND Password = '{2}'", UsersTable, u, p)) Then
            If _db.ExecuteSql(String.Format("UPDATE {0} SET LastLogin = '{1}' WHERE username = '{2}'", UsersTable, Now.ToString("yyyy-MM-dd HH:mm:ss"), u)) = 1 Then
                Return true
            End If
        End If

        Return false

    End Function


    Private ReadOnly _db As New Db
    Private ReadOnly _crypto As New Cryptography
End Class
