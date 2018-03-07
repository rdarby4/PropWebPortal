<style>
	.dtp{
		text-align:center;
	}
	

</style>

@Code
	ViewData("Title") = "Index"
	Layout = "~/Views/Shared/_Layout.vbhtml"
	Dim ts = CType(ViewData("TaskSummary"), Dictionary(Of String(), Boolean))
	Dim rts = CType(ViewData("ReTaskSummary"),Dictionary(Of String(), Boolean))
	Dim totalFailures = CType(ViewData("CompleteFailures"), List(Of String))
	Dim nonRunners = CType(ViewData("NotRun"), List(Of String))
End Code

<br />
<div class="row">
	@*<ul class="nav nav-tabs">
		<li class="active"><a data-toggle="tab" href="#home">Overview</a></li>
		@*<li><a data-toggle="tab" href="#menu1">Reporting & Insights</a></li>*@
		@*<li><a data-toggle="tab" href="#menu2">Task By Task</a></li>*@
		@*<li><a data-toggle="tab" href="#menu3">Currently Running</a></li>*@
	@*</ul>*@
	@*<div class="tab-content">
		<div id="home" class="tab-pane fade active">*@
			<div class="col-md-12">
				<h2 class="heading">PropTrading Tasks Overview</h2>
                <div class="row">
					@if totalFailures.Count > 0 Then
						@<div class="col-md-12">
						 	<div class="offer offer-danger">
						 		<div class="offer-content">
						 			<h3 class="lead">WARNING! the following tasks have not succeeded all day:</h3>
						 			@for each t in totalFailures
										@<p>@t</p>
									Next
						 		</div>
						 </div>
					</div>
					End If
					@if nonRunners.Count > 0 Then
						@<div class="col-md-12">
							<div class="offer offer-danger">
								<div class="offer-content">
									<h3 class="lead">WARNING! the following tasks have not run today:</h3>
									@for each t in nonRunners
										Dim i = t.Split(",")
										@<p>@i(0) Last Run: @i(1)</p>
									Next
								</div>
							</div>
						</div>
					End If
                    <div class="col-md-5">
						<h4 class="heading">Once a Day Task Summary</h4>
						<table class="table" id="1PerDayTasks" border="0">
							<thead>
								<tr>
									<th>Task Name</th>
									<th>Last Run</th>
									<th>Success</th>
									<th>Action</th>
								</tr>
							</thead>
							<tbody>
								@for Each t In ts
									If t.Value = True Then
										@<tr class="success">
											<td>@t.Key(0)</td>
											<td>@t.Key(1)</td>
											<td>@t.Value.ToString</td>
											<td><button class="btn" value="Run Manually" onclick="#">Run Manually</button></td>
										</tr>
									Else
										@<tr class="danger">
											<td>@t.Key(0)</td>
											<td>@t.Key(1)</td>
											<td>@t.Value.ToString</td>
											<td><button class="btn" value="Run Manually" onclick="#">Run Manually</button></td>
										</tr>
									End If
								Next
							</tbody>
						</table>
					</div>
					<div class="col-md-7">
						<h4 class="heading">Recurring Task Summary</h4>
						<table class="table" id="recurringTasks" border="0">
							<thead>
								<tr>
									<th>Task Name</th>
									<th>Start Time</th>
									<th>End Time</th>
									<th>Last Run</th>
									<th>Success</th>
									<th>Action</th>
								</tr>
							</thead>
							<tbody>
								@for Each t In rts
									If t.Value = True Then
										@<tr class="success">
											<td>@t.Key(0)</td>
											<td>@t.Key(1)</td>
											<td>@t.Key(2)</td>
											<td>@t.Key(3)</td>
											<td>@t.Value.ToString</td>
											<td><button class="btn" value="Run Manually" onclick="#">Run Manually</button></td>
										</tr>
									Else
										@<tr class="danger">
											<td>@t.Key(0)</td>
											<td>@t.Key(1)</td>
											<td>@t.Key(2)</td>
											<td>@t.Key(3)</td>
											<td>@t.Value.ToString</td>
											<td><button class="btn" value="Run Manually" onclick="#">Run Manually</button></td>
										</tr>
									End If
								Next
							</tbody>
						</table>
					</div>
				</div>
			</div>

			<div class="row">
				<div class="col-md-6">
					<h4 class="heading">Task Success Rate</h4>	
					<div class="row">
						<div class="dtp">
							<label>From : <input id="from" type="date" value="YYYY-MM-DD" size="10" /></label>
							<label>To : <input id="to" type="date" value="YYYY-MM-DD" size="10" /></label>
							<br />
							<input id="refreshFailure" type="button" value="Refresh" class="btn" />
						</div>
					</div>
					<div class="heading">
						<img id="TaskFailureChart" src="~/Task/TaskFailureChart/@Today.ToString("yyyy-MM-dd 000000")/@Today.ToString("yyyy-MM-dd 235959")" />
					</div>
				</div>
				<div class="col-md-6">
					<h4 class="heading">Todays Failed Tasks</h4>
					<table class="table table-hover">
						<thead>
							<tr>
								<th>Task Name</th>
								<th>Last Failure Time</th>
							</tr>
						</thead>
						<tbody>
							@for each f In ViewData("FailedTasks")
								Dim task = f.Text.split(",")
								@<tr class="danger">
									<td>@task(0)</td>
									<td>@task(1)</td>
								</tr>
								Next
						</tbody>
					</table>
				</div>
			</div>
		</div>
		@*<div id="menu1" class="tab-pane fade">
            <h2 class="heading">Task Reporting and Analysis</h2>


		</div>*@
		@*<div id="menu2" class="tab-pane fade">
			<h2 class="heading">Detailed Task By Task Summary</h2>
			<div class="row">
				<div class="col-md-12">
					@Using Html.BeginForm("TaskByTask", "Task")
						For Each t In allTasks
							@<div Class="checkbox">
								<label><input type="checkbox" value="@t" name="tasks" />@t</label>
							</div>
						Next
						@<input type="submit" class="btn" value="Go" />
					End Using
				</div>
			</div>
		</div>*@

		@*<div id="menu3" class="tab-pane fade">
			<h2 class="heading">Tasks Currently In Progress</h2>
			<div class="row">
				<div class="col-md-10 col-md-offset-1">
					<table class="table table-hover">
						<thead>
							<tr>
								<th>Task Name</th>
								<th>Start Time</th>
							</tr>
						</thead>
						<tbody>
							@if Not inProg Is Nothing Then
								For Each task In inProg
									Dim t = task.Split(",")
									@<tr>
										<td>@t(0)</td>
										<td>@t(1)</td>
									</tr>
								Next
							Else
								@<tr>
									<td>No Tasks In Progress</td>
									<td>@Now.ToString</td>
								</tr>
							End If
						</tbody>
					</table>
				</div>
			</div>
		</div>*@

	@*</div>
</div>*@
