@Code
	Dim c = HttpContext.Current.Request.RequestContext.RouteData.Values("Controller").ToString()
	Dim cLayout As String
	If c = "Home" Then
		cLayout = "~/Views/Shared/_NLLayout.vbhtml"
	Else
		cLayout = "~/Views/Shared/_Layout.vbhtml"
	End If

	Layout = cLayout
End Code