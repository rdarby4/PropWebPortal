@Code
	ViewData("Title") = "CurrentlyRunning"
	Layout = "~/Views/Shared/_Layout.vbhtml"
	Dim inProg = CType(ViewData("InProgress"), List(Of String))
End Code

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

