Imports System.Web.Mvc
Imports SPIN.PropTrading.Common_VB

Namespace Controllers
    Public Class TaskController
        Inherits Controller

#Region " Pages "
        ' GET: Task
        Function Index() As ActionResult 'overview
            Try
                Call GetOneADayTaskSummary
                Call GetRecurringTaskStatus
                Call LoadDailyFailedTasks
                'Call GetAllTasks
                Call LoadCompleteFailures
                Call GetNonRunners
               ' Call GetInProgress
                Return View()
            Catch ex As Exception
                Dim e = New PortalError(ex.Message)
                Return RedirectToAction("ShowError", "Dashboard", e)
            End Try
        End Function

        Function TaskByTaskList() As ActionResult
            Try
                Call GetAllTasks
                Return View()
            Catch ex As Exception
                Dim e = New PortalError(ex.Message)
                Return RedirectToAction("ShowError", "Dashboard", e)
            End Try
        End Function

        Function CurrentlyRunning As ActionResult
            Try
                Call GetInProgress
                Return View()
            Catch ex As Exception
                Dim e = New PortalError(ex.Message)
                Return RedirectToAction("ShowError", "Dashboard", e)
            End Try
        End Function

        Function Reports As ActionResult
            Try
                Return View()
            Catch ex As Exception
                Dim e = New PortalError(ex.Message)
                Return RedirectToAction("ShowError", "Dashboard", e)
            End Try
        End Function

        Function TaskFailureChart(byval dFrom As String, byval dTo As string) As ActionResult

            dFrom = dFrom.Insert(13, ":").Insert(16, ":")
            dTo = dTo.Insert(13, ":").Insert(16, ":")
            call LoadDailyFailureChart(dFrom, dTo)

            Return View()

        End Function

        Function TaskChart(ByVal taskName As String, byval dFrom As String, byval dTo As string) As ActionResult

            dFrom = dFrom.Insert(13, ":").Insert(16, ":")
            dTo = dTo.Insert(13, ":").Insert(16, ":")
            call LoadFailureChart(dFrom, dTo, taskName)

            Return View()

        End Function

        Function TaskByTask(ByVal tasks() as string) As ActionResult
            Dim list As New List(Of TaskDetail)
            For each t In tasks
                Dim td As New TaskDetail(t)
                Call PrepareTaskDetail(td)
                list.Add(td)
            Next
            ViewData("Tasks") = list
            Return View()
        End Function

#End Region


#Region" Private "
        Private Sub GetOneADayTaskSummary()

            Dim tasks As New Dictionary(Of String(), Boolean)
            tasks = _tad.Get1ADayTaskStatus
            If tasks.Count = 0 Then 
                tasks.Add({"An Error Occured", Now.ToString}, False)
                Else
                ViewData("TaskSummary") = tasks
            End If

        End Sub

        Private sub GetRecurringTaskStatus()

            Dim tasks As New Dictionary(Of String(), Boolean)
            tasks = _tad.GetRecurringTaskStatus
            If tasks.Count = 0 Then 
                tasks.Add({"An Error Occured", Now.ToString}, False)
            Else
                ViewData("ReTaskSummary") = tasks
            End If

        End sub

        Private Sub GetAllTasks()
            ViewData("Tasks") = _tad.GetAllTasks
        End Sub

        Private Sub LoadDailyFailureChart(byref from As String, byref [to] As string)

            dim data = _tad.GetTaskStats(from, [to])
            ViewData("FailureRate") = data

        End Sub

        Private Sub LoadFailureChart(byref from As String, byref [to] As string, byref taskname As string)

            dim data = _tad.GetTaskStats(from, [to], taskname)
            ViewData("TaskChart") = data

        End Sub

        Private sub LoadDailyFailedTasks()

            Dim tasks As New List(Of String)
            tasks = _tad.GetFailedTasksByDate(Today.ToString("yyyy-MM-dd 00:00:00"))
            If tasks.Count = 0 Then tasks.Add("No Failed Tasks, " & Now.ToString("yyyy-MM-dd hh:mm:ss"))
            ViewData("FailedTasks") = New SelectList(tasks)

        End sub

        Private Sub LoadCompleteFailures

            Dim tasks = _tad.GetCompleteFailures(Today.ToString("yyyy-MM-dd 00:00:00"))
            ViewData("CompleteFailures") = tasks

        End Sub

        Private Sub GetNonRunners

            Dim nr = _tad.GetNotRunToday
            ViewData("NotRun") = nr

        End Sub

        Private Sub PrepareTaskDetail(byref td As TaskDetail)

            Dim d = _tad.GetBasicInfo(td.Name)
            td.LastRun = _tad.GetLastActivity(td.Name, Enums.TaskActivity.Run)
            td.LastSuccess = _tad.GetLastActivity(td.Name, Enums.TaskActivity.Success)
            td.LastFailure = _tad.GetLastActivity(td.Name, Enums.TaskActivity.Failure)
            td.DailyStats = _tad.GetTaskStats(Today.ToString("yyyy-MM-dd 00:00:00"), Today.ToString("yyyy-MM-dd 23:59:59"), td.Name)
            td.TimesRunToday = _tad.GetCountActivity(td.Name, Enums.TaskActivity.Run, Today.ToString("yyyy-MM-dd 00:00:00"), Today.ToString("yyyy-MM-dd 23:59:59"))
            td.TimesFailedToday = _tad.GetCountActivity(td.Name, Enums.TaskActivity.Failure, Today.ToString("yyyy-MM-dd 00:00:00"), Today.ToString("yyyy-MM-dd 23:59:59"))
            td.TimesSucceededToday = _tad.GetCountActivity(td.Name, Enums.TaskActivity.Success, Today.ToString("yyyy-MM-dd 00:00:00"), Today.ToString("yyyy-MM-dd 23:59:59"))
            td.StartTime = d("StartTime")
            td.EndTime = d("EndTime")
            td.ConsecutiveFailures = _tad.GetConsecutiveFailures(td.Name)

            If d("Recurring") = "1" Then
                td.Recurring = True
                td.Frequency = d("Frequency")
            Else
                td.Recurring = False
            End If

            If td.ConsecutiveFailures.Count > 1 Then 
                td.ConsecutiveFailuresCount = td.ConsecutiveFailures.Count
            Else
                td.ConsecutiveFailuresCount = 0
            End If
            
        End Sub

        Private Sub GetInProgress
            Dim inProg = _tad.GetTasksInProgress()
            ViewData("InProgress") = inProg
        End Sub

#End Region

        Private ReadOnly _tad As New TaskData

    End Class
End Namespace