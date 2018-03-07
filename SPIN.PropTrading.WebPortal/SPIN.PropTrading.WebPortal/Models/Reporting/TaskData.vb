Imports SPIN.PropTrading.Common_VB

Public Class TaskData

#Region" Constants "

    Private Const TaskLogTable As String = "[Scheduler].[dbo].[tblTaskLog]"
    Private const TaskScheduleTable As String = "[Scheduler].[dbo].[tblTasks]"

    Private Const FailedTasksTodaySql As String = "SELECT [TaskName], MAX([Timestamp]) FROM " & TaskLogTable & " where TaskSucceeded = 0 and Timestamp > '$$DATE$$' GROUP BY TaskName"
    Private Const FailedEveryTimeSql As String = "SELECT [TaskName] FROM " & TaskLogTable & " where TaskSucceeded = 0 and Timestamp > '$$DATE$$' AND TaskName NOT IN (SELECT TaskName from " & TaskLogTable & " where TaskSucceeded = 1 and Timestamp > '$$DATE$$') group by taskname"
    Private const OneADayTaskSummarySql As String = "Select taskname, lastRun, TaskSucceeded from " & TaskScheduleTable & " where TaskEnabled = 1 and RunEveryNSeconds = 0 order by lastrun desc"
    Private Const RecurringTaskSummarySql as String="Select taskname, starttime, endtime, lastRun, TaskSucceeded from " & TaskScheduleTable & " where TaskEnabled = 1 and RunEveryNSeconds > 0 order by lastrun desc"
    Private Const MostRecentTasksSql as String = "SELECT TOP $$N$$ taskname, [TimeStamp], taskSucceeded FROM " & TaskLogTable & " WHERE Action = 'COMPLETE' Order By [TimeStamp] DESC"
    Private Const TotalTasksSql as string = "Select COUNT(*) as tasks FROM " & TaskLogTable & " where [Timestamp] > '$$FROM$$' and [Timestamp] < '$$TO$$' and Action = 'COMPLETE'"
    Private Const TotalSuccessSql as string = "Select COUNT(*) as success FROM " & TaskLogTable & " where [Timestamp] > '$$FROM$$' and [Timestamp] < '$$TO$$' and Action = 'COMPLETE' and TaskSucceeded = 1"
    Private Const TotalFailSql as String = "Select COUNT(*) as failure FROM " & TaskLogTable & " where [Timestamp] > '$$FROM$$' and [Timestamp] < '$$TO$$'  and Action = 'COMPLETE' and TaskSucceeded = 0"
    Private Const GetLastRunSql As String = "Select TOP 1 Timestamp FROM " & TaskLogTable & " WHERE TaskName = '$$TASK$$' AND Action = 'Complete' Order by [Timestamp] Desc" 
    Private Const GetLastSuccessSql As String = "Select TOP 1 Timestamp FROM " & TaskLogTable & " WHERE TaskName = '$$TASK$$' AND Action = 'Complete' AND TaskSucceeded = 1 Order by [Timestamp] Desc"
    Private Const GetLastFailureSql As String = "Select TOP 1 Timestamp FROM " & TaskLogTable & " WHERE TaskName = '$$TASK$$' AND Action = 'Complete' AND TaskSucceeded = 0 Order by [Timestamp] Desc"
    Private Const ConsecutiveFailuresSql As String = "SELECT * FROM " & TaskLogTable & " where taskname = '$$TASK$$' and Action = 'COMPLETE' Order by TimeStamp Desc"
    Private Const GetNotRunTodaySql As String = "select t.*, l.TimeStamp, CASE WHEN l.TimeStamp < GETDATE()-1 THEN '1' ELSE NULL END as Problem from tblTasks t
                                                INNER JOIN 
                                                (
                                                Select TaskName,MAX(Timestamp) as TimeStamp FROM tblTaskLog
                                                WHERE Action = 'STARTED'
                                                GROUP BY TaskName
                                                ) l
                                                On t.taskname = l.taskname AND t.TaskEnabled = 1 
                                                ORDER BY t.TaskType, t.TaskName"
    
    Private Const GetInProgressSql As String = "SELECT tmax.*, case when t.Timestamp IS NULL THEN 'IN PROG' END as inProg FROM
                                                (
                                                Select TaskName, MAX(Timestamp) as TimeStamp FROM" & TaskLogTable &
                                                " WHERE Action = 'STARTED'" &
                                                " GROUP BY TaskName" &
                                                " ) tMax LEFT JOIN tblTaskLog t on t.TaskName = tMax.TaskName AND t.Action = 'COMPLETE' and t.Timestamp > tMax.TimeStamp"

#End Region

    Public Function GetFailedTasksByDate(byref day As string)As List(Of String)

        Dim t As New List(Of String)
        Dim dt = _db.GetTable(FailedTasksTodaySql.Replace("$$DATE$$", day))

        For Each dr As DataRow In dt.Rows
            t.Add(dr(0) & "," & dr(1))
        Next
        'add failure count to these results

        Return t

    End Function

    Public function Get1ADayTaskStatus() As Dictionary(Of String(), Boolean)

        Dim dt = _db.GetTable(OneADayTaskSummarySql)
        Dim tasks As New Dictionary(Of String(), Boolean)

        If Not dt Is Nothing Then

            for each dr As DataRow In dt.Rows

                tasks.Add({dr(0), dr(1)}, dr(2))

            Next

            Return tasks

        End If
        
        Return nothing

    End function

    Public Function GetMostRecentTasks(ByRef amnt as integer) As Dictionary(Of String(), Boolean)

        dim dt = _db.GetTable(MostRecentTasksSql.replace("$$N$$", amnt))
        dim tasks as new Dictionary(Of String(), Boolean)

        If Not dt Is Nothing Then

            for each dr As DataRow In dt.Rows

                tasks.Add({dr(0), dr(1)}, dr(2))

            Next

            Return tasks

        End If
        
        Return nothing

    End Function

    Public function GetAllTasks() As List(Of String)
        Dim l As New List(Of String)
        Dim dt = _db.GetTable("Select Distinct TaskName from " & TaskScheduleTable & " WHERE TaskEnabled = 1")

        For each dr As DataRow In dt.Rows
            l.Add(dr(0))
        Next

        Return l

    End function

    Public Function GetRecurringTaskStatus() As Dictionary(Of String(), Boolean)

        Dim dt = _db.GetTable(RecurringTaskSummarySql)
        Dim tasks As New Dictionary(Of String(), Boolean)

        If Not dt Is Nothing Then

            for each dr As DataRow In dt.Rows

                tasks.Add({dr(0), dr(1).ToString(), dr(2).ToString(), dr(3)}, dr(4))

            Next

            Return tasks

        End If
        
        Return nothing

    End Function

    Public Function GetTaskStats(byref startdate as string, byref enddate As string, optional byref taskName as string = "") as Dictionary(of String, integer)

        Dim where As string
        If Not String.IsNullOrEmpty(taskName) Then
            where = " AND TaskName = '" & taskName & "'" 
            Else
            where = ""
        End If

        dim stats as New Dictionary(Of string, integer)
        Dim totals = _db.GetTable(TotalTasksSql.Replace("$$FROM$$", startdate).Replace("$$TO$$", enddate) & where)
        Dim failure = _db.GetTable(TotalFailSql.Replace("$$FROM$$", startdate).Replace("$$TO$$", enddate) & where)
        Dim success = _db.GetTable(TotalSuccessSql.Replace("$$FROM$$", startdate).Replace("$$TO$$", enddate) & where)

        if Not totals.rows.count = 0 AndAlso Not failure.rows.count = 0  AndAlso Not success.rows.count = 0 then

            stats.Add("total", totals(0)(0))
            stats.Add("success", success(0)(0))
            stats.Add("failure", failure(0)(0))

        End If

        return stats

    End Function

    Public Function GetNotRunToday()As List(Of String)

        Dim l As New List(Of String)
        Dim dt = _db.GetTable(GetNotRunTodaySql)

        For each r As DataRow In dt.Rows
        
            If not isDBNull(r("Problem")) then
                l.Add(r("TaskName") & "," & r("LastRun"))
            End if
        Next

        Return l

    End Function

    Public Function GetCompleteFailures(byref day As string) As List(Of String)
        Dim l As New List(Of String)
        Dim dt = _db.GetTable(FailedEveryTimeSql.Replace("$$DATE$$", day))

        For each r As DataRow In dt.Rows
            l.Add(r(0))
        Next

        Return l

    End Function

    Public function GetLastActivity(byref taskName As string, ByRef activity As Enums.TaskActivity) As String

        Dim sql As String

        Select Case True
            Case activity = Enums.TaskActivity.Failure
                sql = GetLastFailureSql
            Case activity = Enums.TaskActivity.Run
                sql = GetLastRunSql
            Case activity = Enums.TaskActivity.Success
                sql = GetLastSuccessSql
        End Select
    
        sql = sql.Replace("$$TASK$$", taskName)

        dim dt = _db.GetTable(sql)
        If dt.Rows.Count > 0 Then Return dt(0)(0)

        Return "No Data"

    End function

    Public Function GetCountActivity(ByRef taskName As String, byref activity As Enums.TaskActivity, ByRef startdate As String, ByRef enddate As string) As Integer

        Dim sql As String
        Dim where = " AND TaskName = '" & taskName & "'"

        Select Case True
            Case activity = Enums.TaskActivity.Failure
                sql = TotalFailSql.Replace("$$FROM$$", startdate).Replace("$$TO$$", enddate) & where
            Case activity = Enums.TaskActivity.Run
                sql = TotalTasksSql.Replace("$$FROM$$", startdate).Replace("$$TO$$", enddate) & where
            Case activity = Enums.TaskActivity.Success
                sql = TotalSuccessSql.Replace("$$FROM$$", startdate).Replace("$$TO$$", enddate) & where
        End Select

        Dim dt = _db.GetTable(sql)

        If dt.Rows.Count > 0 Then Return dt(0)(0)

        Return 0

    End Function

    Public function GetBasicInfo(byref taskName As string) As Dictionary(Of String, String)

        Dim d As New Dictionary(Of String, String)
        Dim dt = _db.GetTable("SELECT * FROM " & TaskScheduleTable & " WHERE TaskName = '" & taskName & "'")

        d.Add("StartTime", dt(0)("StartTime").ToString)
        d.Add("EndTime", dt(0)("EndTime").ToString)
        If dt(0)("RunEveryNSeconds") > 0 Then 
            d.Add("Recurring", "1")
            d.Add("Frequency", dt(0)("RunEveryNSeconds"))
        Else
            d.Add("Recurring", "0")
        End If

        Return d

    End function

    Public Function GetTasksInProgress As List(Of String)

        Dim l As New List(Of String)
        Dim dt = _db.GetTable(GetInProgressSql)

        If Not dt Is Nothing
            For each dr As DataRow In dt.Rows
                If Not IsDBNull(dr("inProg")) AndAlso dr("inProg") = "IN PROG" Then
                    l.Add(dr("TaskName") & "," & dr("TimeStamp"))
                End If
            Next
        End If

        If l.Count > 0 Then Return l

        Return Nothing

    End Function

    Public Function GetConsecutiveFailures(byref taskname As string) As List(Of String)

        Dim l As New List(Of String)
        Dim dt = _db.GetTable(ConsecutiveFailuresSql.Replace("$$TASK$$", taskname))
        Dim con As New List(Of Integer)

        If Not dt Is Nothing 
            For i = 0 to dt.Rows.Count-2
               If dt(i)("TaskSucceeded") = 0 andalso dt(i+1)("TaskSucceeded") = 0 Then
                    For t = i To dt.Rows.Count - 1
                        If dt(t)("TaskSucceeded") = 0 then
                            l.Add(dt(t)("TimeStamp"))
                        Else
                            Return l
                        End If
                    Next
               End If
            Next
        End If

        l.Add("No Data "  & Now.ToString)

        Return l

    End Function


    Private ReadOnly _db As New Db

End Class
