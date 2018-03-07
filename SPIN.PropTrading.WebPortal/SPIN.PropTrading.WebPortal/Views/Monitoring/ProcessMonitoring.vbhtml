
@Code
	ViewData("Title") = "ProcessMonitoring"
	Layout = "~/Views/Shared/_Layout.vbhtml"
	Dim services = CType(ViewData("ServiceData"), List(Of ServiceDetail))
End Code
<meta http-equiv="refresh" content="5">
<div class="row">
	<div class="col-md-12">
		<h2 class="heading">Prop Trading Process Monitoring</h2>
		<div class="row">
				
					@for Each s In services
						If s.ServiceStatus = SPIN.PropTrading.Common_VB.Enums.ServiceStatus.Running Then
							@<div Class="col-md-6">
								<div Class="offer offer-success">
									<div Class="offer-content">
										<h3 class="lead">@s.ServiceName On @s.MachineName</h3>
										<p>Description: @s.ServiceResponsibilities</p>
										<p>Status: @s.ServiceStatus.ToString</p>
										<p>Last Checked: @s.LastChecked</p>
									</div>
								</div>
							</div>
						Else
							@<div Class="col-md-6">
								<div Class="offer offer-danger">
									<div Class="offer-content">
										<h3 class="lead">@s.ServiceName On @s.MachineName</h3>
										<p>Description: @s.ServiceResponsibilities</p>
										<p>Status: @s.ServiceStatus.ToString</p>
										<p>Last Checked: @s.LastChecked</p>
									</div>
								</div>
							</div>
						End If
					Next
		</div>
	</div>
</div>

