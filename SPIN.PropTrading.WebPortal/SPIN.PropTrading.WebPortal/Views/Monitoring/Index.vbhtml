@code
	Dim res = CType(ViewData("Summary"), List(Of SportMonitoring))
	Dim missing = CType(ViewData("MissingResults"), List(Of MissingReultsData))
End Code
<meta http-equiv="refresh" content="10">
<div class="row">
	<div class="col-md-12">
		@if missing.Count > 0 Then
			@<div class="offer offer-danger">
				<div class="offer-content">
					<h3 class="lead">WARNING! There is results data missing from the following sources:</h3>
					@for each m In missing
						@<div class="col-md-3">
							<h4>@m.DataSource</h4>
							@For Each f In m.Fixtures
										@<p>@f</p>
							Next
						</div>
					Next
				</div>
			</div>
		End If

		<h2 class="heading">@ViewData("Sport") Monitoring</h2>
		@if Not res Is Nothing Then
        @<table class="table">
			<thead>
			<tr>
				<th></th>
				<th>FixtureKey</th>
				<th>Kick Off (UK)</th>
				<th>Got Lineups</th>
				<th>Last Price Scrape</th>
				@If ViewData("Sport") = "Basketball" Or ViewData("Sport") = "Baseball" Then
					@<th> Last Squad Scrape</th>
				End If
				<th>Match Simmed</th>
			</tr>
			</thead>
			<tbody>
				@for Each s As SportMonitoring In res
					If Not s.LastPriceUpdate.Contains("No Prices") AndAlso s.HasLineups.ContainsValue(True) Then
					@<tr class="success maindata">
						<td class="md">More Detail <i class="glyphicon glyphicon-plus"></i></td>
						<td>@s.FixtureKey</td>
						<td>@s.KickOff</td>
						<td>
							@for Each l In s.HasLineups
							@Code dim a = l.Key.ToString & ", " & l.Value.ToString end code
								@a
								@<br />
								Next
						</td>
						<td>@s.LastPriceUpdate</td>
						@If ViewData("Sport") = "Basketball" Or ViewData("Sport") = "Baseball" Then
							@<td>
								@for each l In s.LastSquadScrape
								@code Dim a = l.Key.ToString & ": " & l.Value.ToString end code
									@a
									@<br />
								Next
						</td>
						@<td>@s.LastSimmed</td>
						End If
						</tr>
						@<tr Class="collapse row success more-info">
						 	<td></td>
							<td></td>
							<td><b> Latest Prices: </b><br />
								@if s.Prices.Count > 0 Then
									For Each m In s.Prices
										@code dim Ma = m.Key & " : " & m.Value end code
										@ma
										@<br />
									Next
								End If				
							</td>
						 	<td>
						 		<b>Home Lineup: </b> <br />
						 		@if s.HomeLineup.Count > 0 Then
								   For Each l In s.HomeLineup
									   Dim f = l.ToString()
									   @f
						 			@<br />
								   Next
							   End If
						 </td>
						 <td>
						 	<b>Away Lineup: </b><br />
						 	@if s.AwayLineup.Count > 0 Then
								For Each l In s.AwayLineup
									Dim f = l.ToString()
									@f
						 		@<br />
								Next
							End If
						 </td>
						</tr>
					Else
						@<tr class="danger maindata">
						 	<td class="md">More Detail <i class="glyphicon glyphicon-plus"></i></td>
							<td>@s.FixtureKey</td>
							<td>@s.KickOff</td>
							<td>
								@for Each l In s.HasLineups
								@Code dim a = l.Key.ToString & ", " & l.Value.ToString end code
									@a
									@<br />Next
						</td>
						<td>@s.LastPriceUpdate</td>
						@If ViewData("Sport") = "Basketball" Or ViewData("Sport") = "Baseball" Then
							@<td>
								@for each l In s.LastSquadScrape
								@code Dim a = l.Key.ToString & ": " & l.Value.ToString end code
									@a
									@<br />
								Next
						</td>
						@<td>@s.LastSimmed</td>
						End If
					</tr>
					@<tr Class="collapse row danger more-info">
						<td></td>
					 	<td></td>
						<td><b>Latest Prices: </b><br />
							@if s.Prices.Count > 0 Then
								For Each m In s.Prices
									@code dim Ma = m.Key & " : " & m.Value end code
										@Ma
										@<br />
								Next
							End If
						</td>
						<td><b>Home Lineup: </b> <br />
							@if s.HomeLineup.Count > 0 Then
								For Each l In s.HomeLineup
									Dim f = l.ToString()
									@f
									@<br />
								Next
							End If
						</td>
						<td><b>Away Lineup: </b><br />
							@if s.AwayLineup.Count > 0 Then
								For Each l In s.AwayLineup
									Dim f = l.ToString()
									@f
									@<br />
								Next
							End If				
						</td>
					</tr>

				End If

			Next
	</table>
		Else
			@<h2 class="heading"> No Fixtures Today </h2>
		End if
	</div>
</div>
