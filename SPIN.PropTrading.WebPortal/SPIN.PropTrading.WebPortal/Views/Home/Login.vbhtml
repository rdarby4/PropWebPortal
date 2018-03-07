@ModelType SPIN.PropTrading.WebPortal.User
@Code
    ViewData("Title") = "Login"
    Layout = "~/Views/Shared/_NLLayout.vbhtml"
End Code
<div class="wrapper">
	<h1 class="header">PropTrading Web Portal</h1>
	<div class="header main-content">
		<img src="/Content/Images/logo.png" />
	</div>	
	<h2 class="header">Login</h2>
	<div class="main-content">
		@Html.ValidationMessageFor(Function(login) login)
	</div>
	@Using Html.BeginForm("Login", "Home", FormMethod.Post, New With{.class = "main-content"})
		@<fieldset>
			<div Class="row" size="40">
				@Html.LabelFor(Function(user) user.Username)
				@Html.TextBoxFor(Function(user) user.Username, New With {.size = 20})
			</div>
			<div class="row" size="40">
				@Html.LabelFor(Function(user) user.Password)
				@Html.TextBoxFor(Function(user) user.Password, New With {.size = 20, .type = "password"})
			</div>
			<div class="row">
				<input type="submit" value="Login" class="btn btn-primary"/>
			</div>
		</fieldset>
	End Using
</div>
