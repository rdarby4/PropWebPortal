Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

Public Module RouteConfig
    Public Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

        routes.MapRoute(
            "TaskFailure",
            "Task/TaskFailureChart/{dFrom}/{dTo}",
            New With{.controller = "Task", .action= "TaskFailureChart"} 
            )

        routes.MapRoute(
            "TaskChart",
            "Task/TaskChart/{taskName}/{dFrom}/{dTo}",
            New With{.controller = "Task", .action= "TaskChart"} 
            )

        routes.MapRoute(
            "Download",
            "Download/DownloadFile/{data}",
            New With{.controller = "Download", .action= "DownloadFile"} 
            )

        routes.MapRoute(
            "GetMarkets",
            "Trading/GetMarkets/{sport}",
            New With{.controller = "Trading", .action= "GetMarkets"} 
            )

        routes.MapRoute(
            name:="Default",
            url:="{controller}/{action}/{id}",
            defaults:=New With {.controller = "Home", .action = "Login", .id = UrlParameter.Optional}
        )

      

    End Sub
End Module