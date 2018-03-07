<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
		<title>@ViewBag.Title - My ASP.NET Application</title>
    @Styles.Render("~/Content/css")
	@Styles.Render("http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css")

    @Scripts.Render("~/bundles/modernizr")
	@Scripts.Render("~/bundles/jquery")
	@Scripts.Render("http://ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js")
	@Scripts.Render("http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js")
	@Scripts.Render("http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js")
	@Scripts.Render("http://cdn.datatables.net/1.10.2/js/jquery.dataTables.min.js")
	@Styles.Render("http://cdn.datatables.net/1.10.2/css/jquery.dataTables.min.css")
	<link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.16/css/jquery.dataTables.css">
	<link rel="shortcut icon" href="~/Content/Images/favicon.png" type="image/x-icon" />
	<script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.16/js/jquery.dataTables.js"></script>
	@Scripts.Render("http://code.jquery.com/ui/1.11.0/jquery-ui.js")

	<script>

		$(document).ready(function () {
			$('#refreshFailure').click(function () {
				$('#TaskFailureChart').attr('src', 'TaskFailureChart/' + $('#from').val() + ' 000000/' + $('#to').val() + ' 235959')
			});
		});

		$(document).ready(function () {
			$.noConflict();
			$('#recentTasks').DataTable({
				"paging": false, "searching": false
			})
			$('#recurringTasks').DataTable({
				"paging": false, "searching": false
			})
			$('#1PerDayTasks').DataTable({
				"paging": false, "searching": false
			})
			$('.CustReport').DataTable({
				"paging": false, "searching": false
			})
			$('.dropdown-toggle').dropdown();
		});


		$(document).ready(function () {
			$('.md').click(function () {
				var hidden = true
				if (hidden == true) {
					$(this).closest('tr').next('tr').css('display', function (i, v) {
						return this.style.display === 'table-row' ? 'none' : 'table-row';
					})
				} else
					$(this).closest('tr').next('tr').css('display', function (i, v) {
						return this.style.display === 'none' ? 'none' : 'none';
					})
			});
		});

		$(document).ready(function () {
			var date_input1 = $('#dfrom'); //our date input has the name "date"
			var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
			var options = {
				format: 'YYYY-MM-DD',
				container: container,
				todayHighlight: true,
				autoclose: true,
			};
			date_input1.datepicker(options);
		})

		$(document).ready(function () {
			var date_input = $('#dto'); //our date input has the name "date"
			var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
			var options = {
				format: 'YYYY-MM-DD',
				container: container,
				todayHighlight: true,
				autoclose: true,
			};
			date_input.datepicker(options);
		})

		$(document).ready(function () {

			$("#fk").change(function (){ 
				$("#dfrom").prop('disabled', true);
				$("#dto").prop('disabled', true);

			})
		})

		$(document).ready(function () {

			$("#dfrom").change(function () {
				$("#fk").prop('disabled', true);
			})
		})

		$(document).ready(function () {
			
			$('[name="sports"]').change(function () {
				var dataId;
				if ($(this).is(':checked')) {
					dataId = $(this).val();
					$.ajax({
						dataType: 'json',
						url: "/Trading/GetMarkets/" + dataId,
						type: "GET",
						success: function (html) {
							html.forEach(function (item) {
								var i = item.replace(/'/g, '');
								$(".Markets").append('<label class="checkbox-inline"><input type="checkbox" value="' + item + '" name="marketType" class="checkbox-inline market"/>' + i + '</label>');
							})
						}
					});
				}
			})
		})

	</script>
<style>
	.username {
			color:white;
			text-align:right;
			}
	.heading {
			text-align:center;
			}

	.navbar, .navbar-inverse, .navbar-fixed-top {
		background-color: #0e223c;
	}
	.logo {
		text-align:right;
		padding-bottom: 5px;
		padding-top: 5px;
	}

	.boy-content {
		position: relative;
	}
	td {
		padding: 5px;
	}
	table {
		text-align:left;
	}
	html * {
		font-family: Calibri !important;
	}
	.btn {
		background-color: #0e223c !important;
		color: white;
	}
	</style>

	<style>

		/*Table Sort*/
		table.dataTable thead .sorting:before, table.dataTable thead .sorting:after, table.dataTable thead .sorting_asc:before, table.dataTable thead .sorting_asc:after, table.dataTable thead .sorting_desc:before, table.dataTable thead .sorting_desc:after {
			padding: 5px;
		}

		.dataTables_wrapper .mdb-select {
			border: none;
		}

			.dataTables_wrapper .mdb-select.form-control {
				padding-top: 0;
				margin-top: -1rem;
				margin-left: 0.7rem;
				margin-right: 0.7rem;
				width: 100px;
			}

		.dataTables_length label {
			display: flex;
			justify-content: left;
		}

		.dataTables_filter label {
			margin-bottom: 0;
		}

			.dataTables_filter label input.form-control {
				margin-top: -0.6rem;
				padding-bottom: 0;
			}

		table.dataTable {
			margin-bottom: 3rem !important;
		}

		div.dataTables_wrapper div.dataTables_info {
			padding-top: 0;
		}



		/*Recent Bets Ticker*/
		#news_ticker {
			background: #002b3b;
			width: 800px;
			height: 33px;
			margin: 40px auto 10px;
			overflow: hidden;
			-webkit-border-radius: 4px;
			-moz-border-radius: 4px;
			border-radius: 4px;
			padding: 3px;
			position: relative;
			-webkit-box-shadow: inset 0px 1px 2px rgba(0,0,0,0.5);
			-moz-box-shadow: inset 0px 1px 2px rgba(0,0,0,0.5);
			box-shadow: inset 0px 1px 2px rgba(0,0,0,0.5);
		}

span {
	float: left;
	color: white;
	background: #0e223c;
	padding: 6px;
	position: relative;
	border-radius: 4px;
	font-size: 12px;
	-webkit-box-shadow: inset 0px 1px 1px rgba(255, 255, 255, 0.2), 0px 1px 1px rgba(0,0,0,0.5);
	-moz-box-shadow: inset 0px 1px 1px rgba(255, 255, 255, 0.2), 0px 1px 1px rgba(0,0,0,0.5);
	box-shadow: inset 0px 1px 1px rgba(255, 255, 255, 0.2), 0px 1px 1px rgba(0,0,0,0.5);
	background: rgb(0,75,103);
	background: -moz-linear-gradient(top, rgba(0,75,103,1) 0%, rgba(0,53,72,1) 100%);
	background: -webkit-linear-gradient(top, rgba(0,75,103,1) 0%,rgba(0,53,72,1) 100%);
	background: -o-linear-gradient(top, rgba(0,75,103,1) 0%,rgba(0,53,72,1) 100%);
	background: -ms-linear-gradient(top, rgba(0,75,103,1) 0%,rgba(0,53,72,1) 100%);
	background: linear-gradient(top, rgba(0,75,103,1) 0%,rgba(0,53,72,1) 100%);
	filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#004b67', endColorstr='#003548',GradientType=0 );
}

#news_ticker > ul {
	float: left;
	padding-left: 20px;
	-webkit-animation: ticker 20s cubic-bezier(1, 0, .5, 0) infinite;
	-moz-animation: ticker 20s cubic-bezier(1, 0, .5, 0) infinite;
	-ms-animation: ticker 20s cubic-bezier(1, 0, .5, 0) infinite;
	animation: ticker 20s cubic-bezier(1, 0, .5, 0) infinite;
}

ul:hover {
	-webkit-animation-play-state: paused;
	-moz-animation-play-state: paused;
	-ms-animation-play-state: paused;
	animation-play-state: paused;
}

li {
	line-height: 26px;
}

#news_ticker a {
	color: #fff;
	text-decoration: none;
	font-size: 13px;
}

@@-webkit-keyframes ticker {
	0% {
		margin-top: 0;
	}

	10% {
		margin-top: -26px;
	}

	20% {
		margin-top: -52px;
	}

	30% {
		margin-top: -78px;
	}

	40% {
		margin-top: -104px;
	}

	50% {
		margin-top: -130px;
	}

	60% {
		margin-top: -156px;
	}

	70% {
		margin-top: -182px;
	}

	80% {
		margin-top: -208px
	}

	90% {
		margin-top: -234px
	}

	100% {
		margin-top: 0;
	}
}

@@-moz-keyframes ticker {
		0% {
			margin-top: 0;
		}

		10% {
			margin-top: -26px;
		}

		20% {
			margin-top: -52px;
		}

		30% {
			margin-top: -78px;
		}

		40% {
			margin-top: -104px;
		}

		50% {
			margin-top: -130px;
		}

		60% {
			margin-top: -156px;
		}

		70% {
			margin-top: -182px;
		}

		80% {
			margin-top: -208px
		}

		90% {
			margin-top: -234px
		}

		100% {
			margin-top: 0;
		}
}

@@-ms-keyframes ticker {
		0% {
			margin-top: 0;
		}

		10% {
			margin-top: -26px;
		}

		20% {
			margin-top: -52px;
		}

		30% {
			margin-top: -78px;
		}

		40% {
			margin-top: -104px;
		}

		50% {
			margin-top: -130px;
		}

		60% {
			margin-top: -156px;
		}

		70% {
			margin-top: -182px;
		}

		80% {
			margin-top: -208px
		}

		90% {
			margin-top: -234px
		}

		100% {
			margin-top: 0;
		}
}

@@keyframes ticker {
		0% {
			margin-top: 0;
		}

		10% {
			margin-top: -26px;
		}

		20% {
			margin-top: -52px;
		}

		30% {
			margin-top: -78px;
		}

		40% {
			margin-top: -104px;
		}

		50% {
			margin-top: -130px;
		}

		60% {
			margin-top: -156px;
		}

		70% {
			margin-top: -182px;
		}

		80% {
			margin-top: -208px
		}

		90% {
			margin-top: -234px
		}

		100% {
			margin-top: 0;
		}
}

/*Daily Overview*/

.shape{
	border-style: solid; border-width: 0 70px 40px 0; float:right; height: 0px; width: 0px;
	-ms-transform:rotate(360deg); /* IE 9 */
	-o-transform: rotate(360deg);  /* Opera 10.5 */
	-webkit-transform:rotate(360deg); /* Safari and Chrome */
	transform:rotate(360deg);
}
.offer{
	background:#fff; border:1px solid #ddd; box-shadow: 0 10px 20px rgba(0, 0, 0, 0.2); margin: 15px 0; overflow:hidden;
}
/*.offer:hover {
    -webkit-transform: scale(1.1);
    -moz-transform: scale(1.1);
    -ms-transform: scale(1.1);
    -o-transform: scale(1.1);
    transform:rotate scale(1.1);
    -webkit-transition: all 0.4s ease-in-out;
-moz-transition: all 0.4s ease-in-out;
-o-transition: all 0.4s ease-in-out;
transition: all 0.4s ease-in-out;
    }*/
.shape {
	border-color: rgba(255,255,255,0) #d9534f rgba(255,255,255,0) rgba(255,255,255,0);
}
.offer-radius{
	border-radius:7px;
}
.offer-danger {	border-color: #d9534f; }
.offer-danger .shape{
	border-color: transparent #d9534f transparent transparent;
}
.offer-success {	border-color: #5cb85c; }
.offer-success .shape{
	border-color: transparent #5cb85c transparent transparent;
}
.offer-default {	border-color: #999999; }
.offer-default .shape{
	border-color: transparent #999999 transparent transparent;
}
.offer-primary {	border-color: #428bca; }
.offer-primary .shape{
	border-color: transparent #428bca transparent transparent;
}
.offer-info {	border-color: #5bc0de; }
.offer-info .shape{
	border-color: transparent #5bc0de transparent transparent;
}
.offer-warning {	border-color: #f0ad4e; }
.offer-warning .shape{
	border-color: transparent #f0ad4e transparent transparent;
}

.shape-text{
	color:#fff; font-size:12px; font-weight:bold; position:relative; right:-40px; top:2px; white-space: nowrap;
	-ms-transform:rotate(30deg); /* IE 9 */
	-o-transform: rotate(360deg);  /* Opera 10.5 */
	-webkit-transform:rotate(30deg); /* Safari and Chrome */
	transform:rotate(30deg);
}
.offer-content{
	padding:0 20px 10px;
}
@@media (min-width: 487px) {
  .container {
    max-width: 750px;
  }
  .col-sm-6 {
    width: 50%;
  }
}
@@media (min-width: 900px) {
  .container {
    max-width: 970px;
  }
  .col-md-4 {
    width: 33.33333333333333%;
  }
}

@@media (min-width: 1200px) {
  .container {
    max-width: 1170px;
  }
  .col-lg-3 {
    width: 25%;
  }
  }

		.dropdown-menu {
			position: absolute;
			top: 100%;
			left: 0;
			z-index: 1000;
			display: none;
			float: left;
			min-width: 160px;
			padding: 5px 0;
			margin: 2px 0 0;
			font-size: 14px;
			list-style: none;
			background-color: #ffffff;
			border: 1px solid #cccccc;
			border: 1px solid rgba(0, 0, 0, 0.15);
			border-radius: 4px;
			-webkit-box-shadow: 0 6px 12px rgba(0, 0, 0, 0.175);
			box-shadow: 0 6px 12px rgba(0, 0, 0, 0.175);
			background-clip: padding-box;
		}

		.caret {
			display: inline-block;
			width: 0;
			height: 0;
			margin-left: 2px;
			vertical-align: middle;
			border-top: 4px solid;
			border-right: 4px solid transparent;
			border-left: 4px solid transparent;
		}

	</style>

</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
			</div>
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                @*@Html.ActionLink("Application name", "Index", "Dashboard", New With { .area = "" }, New With { .class = "navbar-brand" })*@
				@*<div class="username">
					<p>Welcome @Session("Username").ToString</p>
				</div>*@
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li>@Html.ActionLink("Dashboard", "Index", "Dashboard")</li>
					@*<li class="dropdown">@Html.ActionLink("Trading Reports", "Index", "Trading", New With {.class = "dropdown-toggle"})<b Class="caret"></b>*@
					<li class="dropdown"></span><a href="#" class="dropdown-toggle">Trading Reports<b class="caret"></b></a>
						<ul class="dropdown-menu">
							<li><a href="/Trading/Index">Reporting</a></li>
							<li><a href="/Trading/GetRecentBets">Recent Bets Feed</a></li>
							<li><a href="/Trading/OpenBets">Open Bets</a></li>
						</ul>
					</li>
					<li class="dropdown">
						<a href="#" class="dropdown-toggle">Task Reports<b class="caret"></b></a>
						<ul class="dropdown-menu">
							<li><a href="/Task/Index">Overview</a></li>
							<li><a href="/Task/CurrentlyRunning">Currently Running</a></li>
							<li><a href="/Task/TaskByTaskList">Task By Task</a></li>
						</ul>
					</li>
					@*<li>@Html.ActionLink("Task Requests", "Index", "Dasboard")</li>*@
						@*<li>@Html.ActionLink("About", "About", "Home")</li>
						<li>@Html.ActionLink("Contact", "Contact", "Home")</li>*@
					<li class="dropdown">
						<a href="#" class="dropdown-toggle">Monitoring<b class="caret"></b></a>
						<ul class="dropdown-menu">
							<li><a href="/Monitoring/ProcessMonitoring">Services</a></li>
							<li><a href="/Monitoring/Index/Baseball">Baseball</a></li>
							<li><a href="/Monitoring/Index/Basketball">Basketball</a></li>
							<li><a href="/Monitoring/Index/Tennis">Tennis</a></li>
							<li><a href="/Monitoring/Index/Darts">Darts</a></li>
						</ul>
					</li>
                </ul>
				<div class="logo">
					<img src="/Content/Images/logo.png" />
				</div>	
				<p class="username">@*Welcome @Session("Username").ToString*@</p>
            </div>
        </div>
    </div>
    <div class="container-fluid body-content">
        @RenderBody()
        <hr />
        <footer>
            @*<p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>*@
        </footer>
    </div>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)
</body>
</html>
