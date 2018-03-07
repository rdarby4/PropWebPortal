<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>@ViewBag.Title</title>
	@Styles.Render("~/Content/css")
	@Scripts.Render("~/bundles/modernizr")
	<link rel="shortcut icon" href="~/Content/Images/favicon.png" type="image/x-icon" />
	<style>
		.wrapper {
			width: 760px;
			margin: auto;
		}
		.header {
			text-align:center;
		}
		.main-content {
			text-align:center;
			float: inherit;
		}
		.row {
			padding-top: 10px;
		}
		.logo {
			padding-top: 20px;
			padding-bottom: 20px;
		}
		.navbar, .navbar-inverse, .navbar-fixed-top {
			background-color: #0e223c;
		}
		html * {
			font-family: Calibri !important;
		}
		.btn {
			background-color: #0e223c !important;
		}
	</style>
</head>
<body>
	<div class="navbar navbar-inverse navbar-fixed-top">
		<div class="container">
			
		</div>
	</div>
	<div class="container body-content">
		@RenderBody()
		<hr />
		<footer>
			@*<p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>*@
		</footer>
	</div>

	@Scripts.Render("~/bundles/jquery")
	@Scripts.Render("~/bundles/bootstrap")
	@RenderSection("scripts", required:=False)
</body>
</html>
