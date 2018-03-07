@Code
	ViewData("Title") = "TaskByTaskList"
	Layout = "~/Views/Shared/_Layout.vbhtml"
	Dim allTasks = CType(ViewData("Tasks"), List(Of String))
End Code

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



