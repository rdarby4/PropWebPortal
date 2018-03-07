@code
	Dim ch As New Chart(400, 400)
	ch.AddSeries("2016/17", xValue:=ViewData("BaseballMonthlyChart").items(0), yValues:=ViewData("BaseballMonthlyChart").items(1), xField:="Month", yFields:="PnL (£)")
	ch.Write()
End Code
