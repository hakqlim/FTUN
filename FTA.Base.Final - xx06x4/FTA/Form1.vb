Public Class Form1

    Dim FT As New ClassFtData       ' FT 구조 저장용
    Dim cBdd As New ClassBdd        ' BDD 계산용
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
    ' MCS를 RAW file에서 읽기
    Private Sub btnMCS_Click(sender As Object, e As EventArgs) Handles btnMCS.Click
        OpenFileDialog1.Title = "Read MCS on RAW File"
        OpenFileDialog1.Filter = "RAW File|*.Raw"
        OpenFileDialog1.FileName = "*.raw"
        Call Test(10)
        Call RSample()

        If (OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK) Then
            Dim RawFileName As String = OpenFileDialog1.FileName
            Call cBdd.ReadMCS(RawFileName)
        End If
    End Sub
    ' 고장율 데이터를 mdb file에서 읽기
    Private Sub btnData_Click(sender As Object, e As EventArgs) Handles btnData.Click
        Dim DataFileName As String
        OpenFileDialog1.Title = "Read Failure Data on mdb File"
        OpenFileDialog1.Filter = "mdb|*.mdb"
        OpenFileDialog1.FileName = "*.mdb"
        If (OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        DataFileName = OpenFileDialog1.FileName     ' MDB File 이름
        Call cBdd.ReadData(DataFileName)

    End Sub
    ' 지진취약도를 mdb file에서 읽기
    Private Sub btnFrag_Click(sender As Object, e As EventArgs) Handles btnFrag.Click
        Dim DataFileName As String
        OpenFileDialog1.Title = "Read Fragiity Data on mdb File"
        OpenFileDialog1.Filter = "mdb|*.mdb"
        OpenFileDialog1.FileName = "*.mdb"
        If (OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        DataFileName = OpenFileDialog1.FileName     ' MDB File 이름
        Call cBdd.ReadDataF(DataFileName)

    End Sub
    ' 지진재해도를 txt file에서 읽기
    Private Sub btnHazard_Click(sender As Object, e As EventArgs) Handles btnHazard.Click
        Dim DataFileName As String
        OpenFileDialog1.Title = "Read Hazard Data on inp File"
        OpenFileDialog1.Filter = "inp|*.inp"
        OpenFileDialog1.FileName = "*.inp"
        If (OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        DataFileName = OpenFileDialog1.FileName     ' inp File 이름
        Call cBdd.ReadTextDataFile(DataFileName)

    End Sub
    ' Monte Carlo로 Top Event 값 계산하기
    Private Sub btnMonte_Click(sender As Object, e As EventArgs) Handles btnMonte.Click
        Dim NoSample As Integer = txtSamples.Text
        Dim FileName As String
        SaveFileDialog1.Title = "Save Excel"
        SaveFileDialog1.Filter = "xlsx File|*.xlsx"
        OpenFileDialog1.FileName = "*.xlsx"
        If (OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If
        If (SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK) Then
            FileName = SaveFileDialog1.FileName
            Call cBdd.CalculateMonte(NoSample, FileName)
        End If
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        End
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk

    End Sub
    ' 지진재해도를 mdb file에서 읽기
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim DataFileName As String
        OpenFileDialog1.Title = "Read Hazard Data on mdb File"
        OpenFileDialog1.Filter = "mdb|*.mdb"
        OpenFileDialog1.FileName = "*.mdb"
        If (OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        DataFileName = OpenFileDialog1.FileName     ' MDB File 이름
        Call cBdd.ReadDataH(DataFileName)
    End Sub
    ' 지진취약도를 inp file에서 읽기
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim DataFileName As String
        OpenFileDialog1.Title = "Read Fragility Data on inp File"
        OpenFileDialog1.Filter = "inp|*.inp"
        OpenFileDialog1.FileName = "*.inp"
        If (OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        DataFileName = OpenFileDialog1.FileName     ' inp File 이름
        Call cBdd.ReadDataH(DataFileName)

    End Sub
    ' 지진CDF를 monte carlo로 계산하기
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim NoSample As Integer = TextBox2.Text
        Dim FileName As String
        SaveFileDialog1.Title = "Save Excel"
        SaveFileDialog1.Filter = "xlsx File|*.xlsx"
        OpenFileDialog1.FileName = "*.xlsx"
        If (OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If
        If (SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK) Then
            FileName = SaveFileDialog1.FileName
            Call cBdd.CalculateMonte(NoSample, FileName)
        End If
    End Sub
    ' FT를 FTP 포멧으로 읽기
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim FileName As String
        OpenFileDialog1.Title = "Read FT on FTP File"
        OpenFileDialog1.Filter = "ftp|*.ftp"
        OpenFileDialog1.FileName = "*.ftp"
        If (OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        FileName = OpenFileDialog1.FileName     ' FTP File 이름
        FT.ReadFT(FileName)

    End Sub
    ' FT를 BDD로 풀기
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim TopName As String = TextBox3.Text.ToUpper

        cBdd.Initialize(FT)
        cBdd.SolveFaultTree(TopName)

    End Sub

End Class
