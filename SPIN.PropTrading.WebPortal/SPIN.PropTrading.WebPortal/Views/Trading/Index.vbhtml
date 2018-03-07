@Code
	ViewData("Title") = "Index"
	Layout = "~/Views/Shared/_Layout.vbhtml"
	Dim markets = CType(ViewData("Markets"), List(Of String))
End Code
<style>
	ul {
		list-style: none;
	}
	.form_datetime{
		display:inline;
		text-align:center;
	}

	.form_datetime input{
		display:inline;
	}

	.form_datetime label {
		display: inline;
		padding-left: 25px;
	}

	.ui-datepicker-calendar{
		background-color:lightgrey
	}
	.ui-datepicker-calendar span {
		background-color: lightgrey
	}

</style>

<br />
<div class="row">

			<h2 class="heading">Reporting & Insights</h2>
</div>
			@Using Html.BeginForm("ShowReport", "Trading")
				@<div class="row">
					<div class="col-md-12">
						<div class="heading">
							<div class="input-append date form_datetime">
								<label for="from">From: <input type="text" class="form-control" name="from" id="dfrom" placeholder="Date From" /></label> <label for="to">To: <input type="text" class="form-control" name="to" id="dto" placeholder="Date To" /></label>
							</div>
						</div>
						<div>
							<label for="fk">FixtureKey: <input type="text" class="form-control" name="fk" id="fk"/></label>
						</div>
					</div>
				</div>
				@<div class="row">
					<div class="col-md-6 form-group">
						<div>
							<h4 >Sports</h4>
							<div class="input-group heading">
								<label class="checkbox-inline" value="Baseball"><input type="checkbox" value="Baseball" name="sports" class="checkbox-inline sport"/>Baseball</label>
								<label class="checkbox-inline" value="Basketball"><input type="checkbox" value="Basketball" name="sports" class="checkbox-inline sport"/>Basketball</label>
								<label class="checkbox-inline" value="Tennis"><input type="checkbox" value="Tennis" name="sports" class="checkbox-inline sport"/>Tennis</label>
								<label class="checkbox-inline" value="Darts"><input type="checkbox" value="Darts" name="sports" class="checkbox-inline sport"/>Darts </label>
							</div>
							<h4>Stake Size</h4>
							<div class="input-group heading list-inline">
								<label for="stakeSizeFrom" class="list-inline">Stake From:</label>	
								<div class="input-group">
									<div class="input-group-addon">£</div>
									<input type="text" name="stakeSizeFrom" class="form-control" />		
								</div>
								<label class="list-inline" for="stakeSizeTo">Stake To:</label>	
								<div class="input-group">
									<div class="input-group-addon">£</div>
									<input type="text" name="stakeSizeTo" class="form-control" />
								</div>
							</div>
								<h5>Market Type</h5>
								<div class="Markets">

								</div>
								@*<label class="checkbox-inline"><input type="checkbox" value="MoneyLine" name="marketType" class="market"/>MoneyLine</label>
								@For Each m In markets
								@<label class="checkbox-inline"><input type="checkbox" value="'@m'" name="marketType" class="market"/>@m</label>
								Next*@
							</div>
				 	</div>
				 	<div Class="col-md-6 form-group">
				 		<h4>Layout</h4>
				 		<label class="radio-inline"><input type="radio" value="True" name="detailed" />Detailed</label>
						<h4>Group By</h4>
				 		<label class="radio-inline"><input type="radio" value="Market" name="groupby" class="groupby"/>Market</label>
				 		<label class="radio-inline"><input type="radio" value="Fixture" name="groupby" class="groupby"/>FixtureKey</label>
				 		<h4>Order By</h4>
				 		<label class="checkbox-inline"><input type="checkbox" value="Staked" name="orderby" class="orderby"/>Stake</label>
				 		<label class="checkbox-inline"><input type="checkbox" value="PnL" name="orderby" class="orderby"/>PnL</label>
				 		<label class="checkbox-inline"><input type="checkbox" value="Settled" name="orderby" class="orderby"/>Date Settled</label>
				 		<label class="checkbox-inline"><input type="checkbox" value="fixture" name="orderby" class="orderby"/>FixtureKey</label>
				 	</div>
				 </div>
				@<div class="row">
					<div class="col-md-12">
						<div class="heading"><input type="submit" class="btn heading" value="Run Report" /></div>
					</div>
				</div>
End Using

	


