<style>
	ul {
		list-style:none;
	}
</style>
@Code
    ViewData("Title") = "TaskByTask"
    Layout = "~/Views/Shared/_Layout.vbhtml"
	Dim taskDetail = CType(ViewData("Tasks"), List(Of TaskDetail))
End Code

<div class="col-md-12">
		<h2 class="heading">Task By Task</h2>

		@for each td In taskDetail
			@<div class=" row">
				<div class=" col-md-12">
					@If td.LastRun = td.LastFailure Then
					@<div class="offer offer-danger">
						<div class="offer-content">
							<h3 class="lead">@td.Name</h3>
							<div class="col-md-3">
								<h4 class="heading">About</h4>
								<ul>
									<li>Recurring : @td.Recurring</li>
									@if td.Recurring = True Then
											@<li>Frequency : @td.Frequency</li>
									End If
									<li>Start Time : @td.StartTime</li>
									<li>End Time : @td.EndTime</li>
								</ul>
							</div>
                            <div class="col-md-3">
								<h4 class="heading">Recent Stats</h4>
								<ul>
									<li>Last Run : @td.LastRun</li>
									<li>Last Failure : @td.LastFailure</li>
									<li>Last Success : @td.LastSuccess</li>
									<li>Failures Today : @td.TimesFailedToday</li>
									<li>Successes Today : @td.TimesSucceededToday</li>
									<li>Last Consecutive Failures Count: @td.ConsecutiveFailuresCount</li>
								</ul>
							</div>
							<div class="col-md-3">
								<h4 class="heading">Daily Performance</h4>
								<img class="heading" src="~/Task/TaskChart/@td.Name/@Today.ToString("yyyy-MM-dd 000000")/@Today.ToString("yyyy-MM-dd 235959")" />
							</div>
							<div class="col-md-3">
								<h4 class="heading">Last Consecutive Failures</h4>
								<table class="table-condensed">
									<thead>
										<tr>
											<th>TimeStamp</th>
										</tr>
									</thead>
									<tbody>
										@For each t In td.ConsecutiveFailures
											@<tr>
												<td>@t</td>
											</tr>
										Next
									</tbody>
								</table>
							</div>
						</div>
					</div>
					Else
						@<div class="offer offer-success">
							<div class="offer-content">
								<h3 class="lead">@td.Name</h3>
								<div class="col-md-3">
									<h4 class="heading">About</h4>
									<ul>
										<li>Recurring : @td.Recurring</li>
										@if td.Recurring = True Then
										@<li>Frequency : @td.Frequency</li>
										End If
										<li>Start Time : @td.StartTime</li>
										<li>End Time : @td.EndTime</li>
									</ul>
								</div>
								<div class="col-md-3">
									<h4 class="heading">Recent Stats</h4>
									<ul>
										<li>Last Run : @td.LastRun</li>
										<li>Last Failure : @td.LastFailure</li>
										<li>Last Success : @td.LastSuccess</li>
										<li>Failures Today : @td.TimesFailedToday</li>
										<li>Successes Today : @td.TimesSucceededToday</li>
										<li>Last Consecutive Failures Count: @td.ConsecutiveFailuresCount</li>
									</ul>
								</div>
								<div class="col-md-3">
									<h4 class="heading">Daily Performance</h4>
									<img class="heading" src="~/Task/TaskChart/@td.Name/@Today.ToString("yyyy-MM-dd 000000")/@Today.ToString("yyyy-MM-dd 235959")" />
								</div>
								<div class="col-md-3">
									<h4 class="heading">Last Consecutive Failures</h4>
									<table class="table-condensed">
										<thead>
											<tr>
												<th>TimeStamp</th>
											</tr>
										</thead>
										<tbody>
											@For Each t In td.ConsecutiveFailures
												@<tr>
													<td>@t</td>
												</tr>
											Next
										</tbody>
									</table>
								</div>
							</div>
						</div>
					End If
				</div>
			</div>
		Next
</div>