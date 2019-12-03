Public Class Form1

    Dim FT As New ClassFtData       ' FT 구조 저장용
    Dim cBdd As New ClassBdd        ' BDD 계산용

    Private Sub btnReadFT_Click(sender As Object, e As EventArgs) Handles btnReadFT.Click

        Dim FileName As String
        OpenFileDialog1.Filter = "FTAP|*.Ftp"
        OpenFileDialog1.FileName = "*.Ftp"
        If (OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.OK) Then
            Exit Sub
        End If

        FileName = OpenFileDialog1.FileName
        Call ReadFT(FileName)

    End Sub

    Private Sub ReadFT(FileName As String)

        FT.ReadFT(FileName)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FT.PrintFT()
    End Sub


    Private Sub btnMonte_Click(sender As Object, e As EventArgs) Handles btnMonte.Click

        Dim TopName As String = txtTopEvent.Text.ToUpper
        Dim NoSample As Integer = txtSamples.Text

        Dim cMonte As New ClassMonte
        cMonte.Initialize(FT, NoSample)
        cMonte.SolveFaultTree(TopName)

    End Sub

    Private Sub btnOrder_Click(sender As Object, e As EventArgs) Handles btnOrder.Click
        Dim TopName As String
        TopName = txtTopEvent.Text

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

    Private Sub btnBDD_Click(sender As Object, e As EventArgs) Handles btnBDD.Click

        Dim TopName As String = txtTopEvent.Text.ToUpper

        cBdd.Initialize(FT)
        cBdd.SolveFaultTree(TopName)

    End Sub

    Private Sub btnPrintITE_Click(sender As Object, e As EventArgs) Handles btnPrintITE.Click
        cBdd.PrintITE()
    End Sub

    Private Sub btnSaveBDD_Click(sender As Object, e As EventArgs) Handles btnSaveBDD.Click
        SaveFileDialog1.Title = "Save BDD on RAW File"
        SaveFileDialog1.Filter = "RAW File|*.Raw"
        If (SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK) Then
            Dim RawFileName As String = SaveFileDialog1.FileName
            Call cBdd.SaveBDD(RawFileName)
        End If
    End Sub

    Private Sub btnMCS_Click(sender As Object, e As EventArgs) Handles btnMCS.Click
        SaveFileDialog1.Title = "Save MCS on RAW File"
        SaveFileDialog1.Filter = "RAW File|*.Raw"
        If (SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK) Then
            Dim RawFileName As String = SaveFileDialog1.FileName
            Call cBdd.SaveMCS(RawFileName)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnTraverse1.Click
        Dim TraverseFT As New ClassTraverse(FT) ' TraverFT 생성하고, FT Assign
        Dim Top As String = txtTopEvent.Text
        TraverseFT.TraverseFT1(Top)

    End Sub

    Private Sub btnTraverse2_Click(sender As Object, e As EventArgs) Handles btnTraverse2.Click
        Dim TraverseFT As New ClassTraverse(FT) ' TraverFT 생성하고, FT Assign
        Dim Top As String = txtTopEvent.Text
        TraverseFT.TraverseFT2(Top)
    End Sub

    Private Sub btnCalcualteTop_Click(sender As Object, e As EventArgs) Handles btnCalcualteTop.Click
        Dim TraverseFT As New ClassTraverse(FT) ' TraverFT 생성하고, FT Assign
        Dim Top As String = txtTopEvent.Text
        TraverseFT.Calcualte_Top(Top)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
