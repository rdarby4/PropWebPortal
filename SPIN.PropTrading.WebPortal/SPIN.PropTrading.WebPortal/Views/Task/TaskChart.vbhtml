@code
	Dim stats = CType(ViewData("TaskChart"), Dictionary(Of String, Integer))
	Dim c As New Chart(325, 325)
	c.AddSeries(name:="Success Rate", yValues:={stats("success"), stats("failure")}, xValue:={"Success: " & (stats("success") / stats("total")).ToString("P"), "Failure: " & (stats("failure") / stats("total")).ToString("P")}, chartType:="Pie")
	c.AddLegend("Success Rate")
	c.Write()
End Code
