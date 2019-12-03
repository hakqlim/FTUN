Public Class ClassBdd
    Dim CS As New ClassRawFile
    ' MCS를 RAW file에서 읽기
    Public Sub ReadMCS(RawFileName As String)
        Call CS.ReadRawFile(RawFileName)
    End Sub
    Public Sub ReadData(DataFileName As String)
        Call CS.ReadDataFile(DataFileName)
    End Sub
    Public Sub ReadDataF(DataFileName As String)
        Call CS.ReadDataFileF(DataFileName)
    End Sub
    Public Sub ReadDataH(DataFileName As String)
        Call CS.ReadDataFileH(DataFileName)
    End Sub
    Public Sub CalculateMonte(NoSamples As Integer, FileName As String)
        Call CS.CalculateMonte(NoSamples, FileName)
    End Sub
    Public Sub ReadTextDataFile(DataFileName As String)
        Call CS.ReadTextDataFile(DataFileName)
    End Sub
End Class
