@*<style>
	.navbar, .navbar-inverse, .navbar-fixed-top {
		display:none;
	}
</style>*@
<style>
	table {
		margin: 5px;
	}
</style>
@code
	Dim allBets = CType(ViewData("Bets"), List(Of List(Of String)))
	Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
<div class="row">
	@for Each bet In allBets
    @<div class="col-md-6">
		<table border="0" Class="table table-hover">
			<thead>
		<tr class="heading">
					<th>Fixture</th>
					<th>Market</th>
					<th>Selection</th>
					<th>Odds</th>
					<th>PnL (£)</th>
					<th>Timestamp</th>
				</tr>
			</thead>
			<tbody>
				@for Each b In bet
					Dim i = b.Split(",")
					If i(4) > 0 Then
						@<tr class="success">
							<td>@i(0)</td>
							<td>@i(1)</td>
							<td>@i(2)</td>
							<td>@i(3)</td>
							<td>@i(4)</td>
							<td>@i(5)</td>
						</tr>
					Else
						@<tr class="danger">
							<td>@i(0)</td>
							<td>@i(1)</td>
							<td>@i(2)</td>
							<td>@i(3)</td>
							<td>@i(4)</td>
							<td>@i(5)</td>
						</tr>
					End If
				Next
			</tbody>
		</table>
	</div>
	Next
</div>