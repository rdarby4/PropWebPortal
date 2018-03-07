Imports System.IO


Public Class ExcelExporter

    Public Function ExportData(Byval sql As string) As String

        Dim filename = "PropReport" & Now.ToString("ddMMyyyyHHmmss") & ".xlsx"
        Dim path =  HttpContext.Current.Server.MapPath(filename)
        Dim dt = _db.gettable(sql)

        Dim sw As New StreamWriter(path)
        sw.WriteLine(BuildHeaders(dt))

        For Each dr As DataRow In dt.Rows
            sw.WriteLine(BuildRow(dr))
        Next
        
        sw.Close

        Return path

    End Function

    Private function BuildHeaders(byref dt As datatable) As String
        
        Dim s = ""

        For i = 0 To dt.Columns.Count -1
            s = s & dt.Columns(i).ToString
            If i < dt.Columns.Count - 1 Then s = s & ","
        Next

        Return s

    End function

    Private Function BuildRow(byref dr As datarow) As String

        Dim s = ""

        For i = 0 To dr.ItemArray.Count -1
            s =s + dr.Item(i).ToString
            If i < dr.ItemArray.Count - 1 Then s = s & ","
        Next

        Return s
    End Function

    Private ReadOnly _db As New Common_VB.Db

End Class
