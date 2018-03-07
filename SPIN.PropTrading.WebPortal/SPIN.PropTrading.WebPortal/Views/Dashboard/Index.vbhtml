@Code
	ViewData("Title") = "Index"
	Layout = "~/Views/Shared/_Layout.vbhtml"
	Dim rt = CType(ViewData("RecentTasks"), Dictionary(Of String(), Boolean))
End Code

	<br/>
	<div class="row">
			<div class="col-md-12">
				<h2 class="heading">PropTrading Dashboard</h2>
			</div>
			<div id="news_ticker">
				<span>Recently Settled Bets</span>
				<ul>
					@for Each b In ViewData("RecentBets")
						Dim bet = b.Text.Split(",")
				@<li><a href="#">Fixture : @bet(0) Market : @bet(1) Selection : @bet(2) Price : @bet(3) PnL : @bet(4)</a></li>
					Next
				</ul>
			</div>
		</div>
	<div class="row">
		<div class="col-md-4">
				<div class="offer offer-primary">
					<div class="offer-content">
						<h3 class="lead">Total Bets Placed Yesterday.</h3>
						<p>@ViewData("BetsPlaced")</p>
					</div>
				</div>
		</div>

		<div class="col-md-4">
			@if ViewData("DailyPnL£") > 0 Then
				@<div class="offer offer-success">
					<div class="offer-content">
						<h3 class="lead">Total PnL(£) Yesterday</h3>
						<p>@ViewData("DailyPnL£")</p>
					</div>
				</div>
				Else
				@<div class="offer offer-danger">
					<div class="offer-content">
						<h3 class="lead">Total PnL(£) Yesterday</h3>
						<p>@ViewData("DailyPnL£")</p>
					</div>
				</div>
			End If
		</div>

		<div class="col-md-4">	
			@if ViewData("DailyPnL%") > 0 then
			@<div class="offer offer-success">
				<div class="offer-content">
					<h3 class="lead">Total PnL(%) Yesterday</h3>
					<p>@ViewData("DailyPnL%")</p>
				</div>
			</div>
			Else
			@<div class="offer offer-danger">
				<div class="offer-content">
					<h3 class="lead">Total PnL(%) Yesterday</h3>
					<p>@ViewData("DailyPnL%")</p>
				</div>
			</div>
			End If
		</div>

	</div>
<br />
        <div Class="col-md-8">
			<h4> Totals</h4>
				<Table border = "0" Class="table table-hover">
					<tbody>
						<tr>
					<th> Total Weekly Pnl(£)</th>
							<th> Total Weekly PnL(%)</th>
							<th> Total Monthly PnL(£)</th>
							<th> Total Monthly PnL(%)</th>
							<th> Total Annual PnL(£)</th>
							<th> Total Annual PnL(%)</th>
						</tr>
						<tr>
					@*Pnl data will go in here*@
							<td>@ViewData("Pnl1Week£")</td>
							<td>@ViewData("Pnl1Week%")</td>
							<td>@ViewData("Pnl1Month£")</td>
							<td>@ViewData("Pnl1Month%")</td>
							<td>@ViewData("Pnl1Year£")</td>
							<td>@ViewData("Pnl1Year%")</td>
						</tr>
						<tr Class="heading">
							<th> <h4> Baseball</h4></th>
						</tr>
						<tr>
							<th> Baseball Weekly Pnl(£)</th>
							<th> Baseball Weekly PnL(%)</th>
							<th> Baseball Monthly PnL(£)</th>
							<th> Baseball Monthly PnL(%)</th>
							<th> Baseball Annual PnL(£)</th>
							<th> Baseball Annual PnL(%)</th>
						</tr>
						<tr>
							<td>@ViewData("Baseball1Week£")</td>
							<td>@ViewData("Baseball1Week%")</td>
							<td>@ViewData("Baseball1Month£")</td>
							<td>@ViewData("Baseball1Month%")</td>
							<td>@ViewData("Baseball1Year£")</td>
							<td>@ViewData("Baseball1Year%")</td>
						</tr>
						<tr Class="heading">
							<th> <h4> Tennis</h4></th>
						</tr>
						<tr>
							<th> Tennis Weekly Pnl(£)</th>
							<th> Tennis Weekly PnL(%)</th>
							<th> Tennis Monthly PnL(£)</th>
							<th> Tennis Monthly PnL(%)</th>
							<th> Tennis Annual PnL(£)</th>
							<th> Tennis Annual PnL(%)</th>
						</tr>
						<tr>
							<td>@ViewData("Tennis1Week£")</td>
							<td>@ViewData("Tennis1Week%")</td>
							<td>@ViewData("Tennis1Month£")</td>
							<td>@ViewData("Tennis1Month%")</td>
							<td>@ViewData("Tennis1Year£")</td>
							<td>@ViewData("Tennis1Year%")</td>
						</tr>
					</tbody>
				</table>
			<br />
				<div Class="row">
					<h4> Month By Month PnL</h4>
					<Table border = "0" Class="table table-hover">
						<tbody>
							<tr>
								<th> Baseball</th>
								<th> Tennis</th>
							</tr>
							<tr>
								<td> <img src = "/Dashboard/BaseballChart" /></td>
								<td> <img src = "/Dashboard/TennisChart" /></td>
							</tr>
						</tbody>
					</table>

				</div>
			</div>
			<div Class="col-md-4">
				 <h4 class="heading"> Recently Run Tasks</h4>
				<Table border = "0" Class="table table-hover" id="recentTasks">
					<thead>
						<tr Class="heading">
							<th> TaskName</th>
							<th> Time</th>
							<th> Success</th>
						</tr>
					</thead>
					<tbody>
						@For Each t In rt
							If t.Value = True Then
						@<tr class="success">
							<td>@t.Key(0)</td>
							<td>@t.Key(1)</td>
							<td>@t.Value.ToString</td>
						</tr>
							Else
						@<tr class="danger">
							<td>@t.Key(0)</td>
							<td>@t.Key(1)</td>
							<td>@t.Value.ToString</td>
						</tr>
							End If
						Next
					</tbody>
				</table>
		</div>



