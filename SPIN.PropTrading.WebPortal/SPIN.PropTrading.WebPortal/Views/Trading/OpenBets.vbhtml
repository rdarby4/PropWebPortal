﻿
@Code
	ViewData("Title") = "OpenBets"
	Layout = "~/Views/Shared/_Layout.vbhtml"
	Dim Sports As String() = CType(ViewData("Sports"), String())
End Code
<div class="row">
	<div class="col-md-12">

		<h2 class="heading">View Open Bets</h2>
	
		<div class="sports">
		@Using Html.BeginForm("GetOpenBets", "Trading")
			for each s In Sports
				@<Label class="checkbox-inline"><input type="checkbox" value="@s" name="sports"/> @s </Label>
			Next
			@<input type="submit" class="btn" value="Get Open Bets" />
		End Using
		</div>

	</div>
</div>
