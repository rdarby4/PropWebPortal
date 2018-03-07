Public Class HomeController
    Inherits System.Web.Mvc.Controller

    'Function Index() As ActionResult
    '    ret
    'End Function
    'Function Login() As ActionResult
    '    Return View()
    'End Function

    Public Function Login(Byval user As User)
        If Not user.Username Is Nothing andalso Not user.Password Is Nothing then
            If ModelState.IsValid Then
                If user.IsValid(user.UserName, user.Password) Then
                    Session("UserName") = user.Username.ToString()
                    Session("LoggedIn") =  True
                    Return RedirectToAction("Index", "Dashboard")
                Else
                    ModelState.AddModelError("", "Incorrect Username or Password")
                    Return View(user)
                End If
            End If
        Else
            Return View(user)
        End If
        Return View(user)
    End Function

    'Function About() As ActionResult
    '    ViewData("Message") = "Your application description page."

    '    Return View()
    'End Function

    'Function Contact() As ActionResult
    '    ViewData("Message") = "Your contact page."

    '    Return View()
    'End Function
End Class
