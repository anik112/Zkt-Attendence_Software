Imports Microsoft.VisualBasic

Public Class Class1
    'Open Com Port
    Public Declare Function TSMOpenComm Lib "TSMCOM32.DLL" (ByVal port As String, ByVal p1 As Long, ByVal p2 As Byte, ByVal p3 As Long, ByVal p4 As Long, ByVal p5 As Long, ByVal p6 As Long, ByRef pcdll As Byte) As Integer
End Class
