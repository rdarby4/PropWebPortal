Imports System.Web.Mvc

Namespace Controllers
    Public Class DownloadController
        Inherits Controller

        ' GET: Download
        Function DownloadFile(byval sql As string) As FilePathResult

            Dim f = _xl.ExportData(sql)
            Dim c = MimeMapping.GetMimeMapping(f)
            Return File(f , c, "PropReport" & Now.ToString & ".xls")
            
        End Function

        Private ReadOnly _xl As New ExcelExporter

    End Class 

End Namespace