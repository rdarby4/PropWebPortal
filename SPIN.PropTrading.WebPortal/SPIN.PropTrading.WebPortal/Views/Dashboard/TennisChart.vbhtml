@code
	Dim ch As New Chart(400, 400)
	ch.AddSeries("2016/17", xValue:=ViewData("TennisMonthlyChart").items(0), yValues:=ViewData("TennisMonthlyChart").items(1), xField:="Month", yFields:="PnL (£)")
	ch.Write()
End Code
