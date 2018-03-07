@Code
    ViewData("Title") = "ShowReport"
    Layout = "~/Views/Shared/_Layout.vbhtml"
	Dim results = CType(ViewData("Results"), System.Data.DataSet)
	Dim sql = CType(ViewData("Sql"), String)
End Code

<div class="row">
	<h2 class="heading">Trading Report</h2>
	<div style="text-align:right; float:right;">
		@Using Html.BeginForm("DownloadFile", "Download")
			@<input type="text" hidden="" value="@sql" name="sql"/>
            @<div Class="heading"><input type="submit" class="btn heading" value="Export To Excel" /></div>
		End Using
	</div>
    <div class="col-md-12 heading">

	@For each dt As System.Data.DataTable in results.Tables
		If ViewData("Detail") = True  AndAlso ViewData("GroupBy") = false then
			@<table class="table heading CustReport">
				<thead>
					<tr><th><h3>@dt.tablename</h3></th></tr>
					<tr>
						@for Each c As Data.DataColumn in dt.Columns
								@<th>@c.ColumnName</th>
							Next
					</tr>
				</thead>		
				<tbody>
					@for Each r As Data.DataRow in dt.Rows
						@<tr>
							@for each i In r.ItemArray
								@<td>@i</td>
							Next
						</tr>
					Next
				</tbody>
			</table>
		End If
		If ViewData("GroupBy") = False then
		@<table class="table heading CustReport">
			<thead>
				<tr><th><h3>@dt.TableName Totals</h3></th></tr>
				<tr>
					<th>Bets</th>
					<th>Staked</th>
					<th>PnL</th>
				</tr>
			</thead>
			<tbody>
				<tr>
					<td>@dt.Rows.Count</td>
					<td>@Math.Round(dt.Compute("SUM(Staked)",""), 2)</td>
					<td>@Math.Round(dt.Compute("SUM(Pnl)", ""), 2)</td>
				</tr>
			</tbody>
		</table>
		Else
			@<table class="table heading CustReport">
				<thead>
					<tr><th><h3>@dt.TableName</h3></th></tr>
					<tr>
						<th>@dt.Columns(0).ColumnName</th>
						<th>@dt.Columns(1).ColumnName</th>
						<th>@dt.Columns(2).ColumnName</th>
						<th>@dt.Columns(3).ColumnName</th>
					</tr>
				</thead>
				<tbody>
					@for Each dr As data.datarow In dt.Rows
					@<tr>
						<td>@dr(0)</td>
						<td>@Math.Round(dr(1),2)</td>
						<td>@Math.Round(dr(2),2)</td>
					 	<td>@Math.Round(dr(3),2)</td>
					</tr>
					Next
				</tbody>
			</table>
			@*@<Table Class="table heading">
				<thead>
					<tr> <th> <h3>@dt.TableName Totals</h3></th></tr>
				<tr>
				<th>Bets</th>
					<th> Staked</th>
					<th> PnL</th>
				</tr>
				</thead>
				<tbody>
					<tr>
						<td>@dt.Rows.Count</td>
						<td>@Math.Round(dt.Compute("SUM(Staked)",""), 2)</td>
						<td>@Math.Round(dt.Compute("SUM(PnL)", ""), 2)</td>
					</tr>
				</tbody>
			</table>*@
		End If
		Next
	</div>
</div>
