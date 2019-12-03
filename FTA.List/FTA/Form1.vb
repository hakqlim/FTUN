Public Class Form1

    Dim FT As New ClassFtData       ' FT 구조 저장용
    Dim cBdd As New ClassBdd        ' BDD 계산용

    ' FT FileName 가져온 후, FT 읽기
    Private Sub btnReadFT_Click(sender As Object, e As EventArgs) Handles btnReadFT.Click

        Dim FileName As String
        OpenFileDialog1.Filter = "FTAP|*.Ftp"
        OpenFileDialog1.FileName = "*.Ftp"
        If (OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        FileName = OpenFileDialog1.FileName     ' FTP File 이름
        FT.ReadFT(FileName)

    End Sub

    ' FT를 제대로 읽었는지 출력해보기 
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FT.PrintFT()
    End Sub

    ' Monte Carlo로 Top Event 값 계산하기
    Private Sub btnMonte_Click(sender As Object, e As EventArgs) Handles btnMonte.Click

        Dim TopName As String = txtTopEvent.Text.ToUpper
        Dim NoSample As Integer = txtSamples.Text

        Dim cMonte As New ClassMonte(FT, NoSample)
        cMonte.SolveFaultTree(TopName)

    End Sub

    ' Gate 및 BE Order 찾아서 저장하기
    '   --> Find_Order.Gate_Order_BU, Find_Order.BE_Order
    Private Sub btnOrder_Click(sender As Object, e As EventArgs) Handles btnOrder.Click

        Dim TopName As String
        TopName = txtTopEvent.Text  ' 주어진 Top Event 이름

        ' Gate Ordering
        Dim Find_Order As New ClassFT_Order
        Find_Order.Find_BU_Gate_Order_FT(FT, TopName)    ' Order 찾고

        Dim oList As List(Of Integer) = Find_Order.Gate_Order_BU

        Dim S As String = "-- Gate Order"
        For Each X In oList
            S += vbCrLf & X & " | " & FT.XEvents(X).Name
        Next

        ' BE Ordering 
        Call Find_Order.Find_BE_Order(FT, TopName)       ' BE Order 찾고
        Dim eList As Integer() = Find_Order.BE_Order

        S += vbCrLf & vbCrLf & "-- BE Order"""
        Dim nEvent As Integer = FT.XEvents.Count
        For i = 2 To nEvent - 1
            S += vbCrLf & i & " | " & FT.XEvents(i).Name
        Next

        MessageBox.Show(S)

    End Sub

    ' BDD 풀기
    Private Sub btnBDD_Click(sender As Object, e As EventArgs) Handles btnBDD.Click

        Dim TopName As String = txtTopEvent.Text.ToUpper

        cBdd.Initialize(FT)
        cBdd.SolveFaultTree(TopName)

    End Sub

    ' BDD Solve한 ITE 구조 출력하기
    Private Sub btnPrintITE_Click(sender As Object, e As EventArgs) Handles btnPrintITE.Click
        cBdd.PrintITE()
    End Sub

    ' BDD 결과를 RAW file에 저장하기 (AIMS-PSA에 읽을 수 있음)
    Private Sub btnSaveBDD_Click(sender As Object, e As EventArgs) Handles btnSaveBDD.Click

        SaveFileDialog1.Title = "Save BDD on RAW File"
        SaveFileDialog1.Filter = "RAW File|*.Raw"
        If (SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK) Then
            Dim RawFileName As String = SaveFileDialog1.FileName
            Call cBdd.SaveBDD(RawFileName)
        End If

    End Sub

    ' BDD 결과를 MCS 로 변환후 RAW file에 저장하기 (AIMS-PSA에 읽을 수 있음)
    Private Sub btnMCS_Click(sender As Object, e As EventArgs) Handles btnMCS.Click
        SaveFileDialog1.Title = "Save MCS on RAW File"
        SaveFileDialog1.Filter = "RAW File|*.Raw"
        If (SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK) Then
            Dim RawFileName As String = SaveFileDialog1.FileName
            Call cBdd.SaveMCS(RawFileName)
        End If
    End Sub

    ' FT Traverse 하기
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnTraverse1.Click

        Dim TraverseFT As New ClassTraverse(FT) ' TraverFT 생성하고, FT Assign
        Dim Top As String = txtTopEvent.Text
        TraverseFT.TraverseFT1(Top)

    End Sub

    ' FT Traverse 하기 (방문한 곳은 다시 방문하지 않기)
    Private Sub btnTraverse2_Click(sender As Object, e As EventArgs) Handles btnTraverse2.Click

        Dim TraverseFT As New ClassTraverse(FT) ' TraverFT 생성하고, FT Assign
        Dim Top As String = txtTopEvent.Text
        TraverseFT.TraverseFT2(Top)

    End Sub

    ' OR, AND Gate 값 계산하기
    Private Sub btnCalcualteTop_Click(sender As Object, e As EventArgs) Handles btnCalcualteTop.Click

        Dim TraverseFT As New ClassTraverse(FT) ' TraverFT 생성하고, FT Assign
        Dim Top As String = txtTopEvent.Text
        TraverseFT.Calcualte_Top(Top)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
