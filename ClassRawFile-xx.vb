Option Explicit On
Imports System.Data.OleDb

Public Class ClassRawFile
    '--------------------------------------------------------------------
    ' Cut set 읽어서 memory에 저장하기 위한 기본적인 array 들
    '--------------------------------------------------------------------

    ' Event Data 저장용 -------------------------
    Structure XDataType
        Dim BlockName As String ' Block Name (참고용임)
        Dim NumOfEvent As Integer ' 각 data block에 포함된 basic event 수 - Range (1, NumOfEvent)
        Dim EventName() As String ' Event Name  - Range (1, NumOfEvent)
        Dim EventProb() As Single ' Event 확률  - Range (1, NumOfEvent)
        Dim EventProbR() As Single ' Event Random 확률  - Range (1, NumOfEvent)

        'Dim FVI() As Single
        'Dim RRW() As Single
        'Dim RAW() As Single
        'Dim BB() As Single ' Birnbaum
        'Dim ProbaInRAW() As Single ' RAW 에서 읽힌 값
        'Dim ProbaInDB() As Single ' KIRAP DB 에 있는 값
        'Dim NoMcs() As Integer ' 관련된 Cut Set의 수
    End Structure

    ' Cut Set 저장용 -----------------------------
    Structure XaCutSetType
        Dim NoElem As Short ' # of Elements
        Dim Elems() As Integer ' 한 Cut Set의 elements 들에 대한 Index - Range  (0, NoElem-1)
        ' Xdata.EventName(E) 에 이름 있음
        Dim CutProba As Single ' 각 Cut Set의 확률
        'Dim FVI As Single '//
        'Dim Acc As Single '//
    End Structure

    Public Structure XCutSetType
        Dim BlockName As String ' Block Name (참고용임)
        Dim NumOfCutSet As Integer ' # of Cut Sets (0, NumOfCutSet - 1)
        Dim XaCutSet() As XaCutSetType ' 각 Cut Set   -   Range : (0, NumOfCutSet-1)
    End Structure

    Structure XBlockType ' 각 Cut Set Block의 Position을 기록
        Dim BlockName As String ' Block File Name
        Dim CutSetBlockPos As Integer ' Raw File Cut Set Block에 대한 Position
        Dim DataBlockPos As Integer ' Raw File Data Block에 대한 Position
    End Structure

    Public XData As New XDataType       ' Event 확률 저장용
    Public XCutSet As New XCutSetType   ' Cut Set 저장용

    ' ------------------------------------------
    Public XCutTopProba As Single ' 현재 Cut Set의 값

    ' Cut Set Block의 Position 저장용 ------------------
    Public MxBlock As Integer = 0 ' 최대 허용 # of Block
    Public XBlock() As XBlockType ' Block Array  - Range (0, XNoBlock-1)
    Public XNoBlock As Integer ' 읽혀진 Cut Set Block의 수

    ' ==========================================================================

    ' 그 외 ------------------------------------------------
    Public RawFile As String ' Raw File 이름
    Public XTopEvent As String ' Raw File에서 읽을 Top Event 이름

    ' File Number & Position
    Public fPos As Integer ' Raw File Position : C로 쓰여진 Raw File에서 위치로 이동하도록
    Public RawFileID As Integer

    Public IndexOfCS As Integer

    ' Cut Set의 확률 계산
    Public Function CalculateCutSetsProba() As Double
        Dim i, NoSet_1 As Integer
        Dim j, NoElem_1 As Integer
        Dim E As Integer
        Dim V, F As Double
        V = 0 : NoSet_1 = XCutSet.NumOfCutSet - 1

        For i = 0 To NoSet_1
            F = 1 : NoElem_1 = XCutSet.XaCutSet(i).NoElem - 1 ' Cut Set 내 Element 수
            For j = 0 To NoElem_1
                E = XCutSet.XaCutSet(i).Elems(j) ' Cut Set 내의 Event 하나
                F = F * EventProbaCS(E)
            Next j
            XCutSet.XaCutSet(i).CutProba = F ' 확률 다시 할당
            V = V + F
        Next i
        CalculateCutSetsProba = V ' Top Event 확률
    End Function

    ' Cutset 내 각 Event의 확률 
    Public Function EventProbaCS(ByRef Evt As Integer) As Double
        If (Evt > 0) Then
            EventProbaCS = XData.EventProb(Evt)
        Else
            EventProbaCS = 1.0# - XData.EventProb(-Evt)
        End If
    End Function

    ' Read RAW File
    ' Cut Set 읽기 ---------------------------------------------------
    Public Sub ReadRawFile(ByRef RawFileName As String)
        'Dim DataBlockPos, CSBlockPos As Integer ' Raw File Position
        Dim BlockType As Char   'VB6.FixedLengthString(1)
        Dim BlockName As String 'VB6.FixedLengthString(32)
        Dim nEvent, nCutset As Integer
        Dim EvtName, EvtNameD As String   'VB6.FixedLengthString(32)
        Dim i, NoElem, Elem As Integer
        Dim j As Integer, RawFileID As Integer
        Dim ID As Short
        Dim Pr As Single
        Dim TopPr, TopPr1, Prob As Double

        '        On Error GoTo Err_Renamed

        ' File Open -------------------------------
        RawFileID = 1
        FileOpen(RawFileID, RawFileName, OpenMode.Binary, OpenAccess.Read) ' File 열기
        FileGet(RawFileID, ID)

        ' Data Block ------------------------------
        ' block type byte
        '        BlockType = "d"
        FileGet(RawFileID, BlockType)
        If BlockType = "c" Then     'BlockType = C
            BlockName = Space(128)
            FileGet(RawFileID, BlockName)
            FileGet(RawFileID, BlockType)
            BlockName = Space(32)
            FileGet(RawFileID, BlockName)
            FileGet(RawFileID, nEvent)

            XData.NumOfEvent = nEvent
            ReDim XData.EventName(nEvent)
            ReDim XData.EventProb(nEvent)

            EvtName = Space(28)
            EvtNameD = Space(4)

            For i = 1 To nEvent
                FileGet(RawFileID, Pr)
                FileGet(RawFileID, EvtName)
                FileGet(RawFileID, EvtNameD)

                XData.EventName(i) = EvtName
                XData.EventProb(i) = Pr
            Next i

        Else                        ' BlockType = d
            BlockName = Space(32)
            FileGet(RawFileID, BlockName)
            FileGet(RawFileID, nEvent)
            XData.NumOfEvent = nEvent
            EvtName = Space(32)
            ReDim XData.EventName(nEvent)
            ReDim XData.EventProb(nEvent)

            For i = 1 To nEvent
                FileGet(RawFileID, Pr)
                FileGet(RawFileID, EvtName)

                XData.EventName(i) = EvtName
                XData.EventProb(i) = Pr
            Next i
        End If

        Call TrimEventName()

        FileGet(RawFileID, Pr)

        ' Cut Set Block ----------------------------
        FileGet(RawFileID, BlockType)
        FileGet(RawFileID, BlockName)
        FileGet(RawFileID, nCutset)
        XCutSet.NumOfCutSet = nCutset

        Message.Add(nCutset)  ' 확인용 LHK
        ReDim XCutSet.XaCutSet(nCutset)

        TopPr = 0.0
        For i = 0 To nCutset - 1

            FileGet(RawFileID, Pr)
            FileGet(RawFileID, NoElem)

            XCutSet.XaCutSet(i).NoElem = NoElem
            XCutSet.XaCutSet(i).CutProba = Pr

            ReDim XCutSet.XaCutSet(i).Elems(NoElem)

            Prob = 1.0
            For j = 0 To NoElem - 1
                FileGet(RawFileID, Elem)
                XCutSet.XaCutSet(i).Elems(j) = Elem
                If (Elem > 0) Then
                    Elem = Elem
                    Prob = XData.EventProb(Elem) * Prob
                Else
                    Elem = -Elem
                    Prob = (1.0 - XData.EventProb(Elem)) * Prob
                End If
            Next j

            TopPr = TopPr + Prob
        Next i

        FileGet(RawFileID, TopPr1)

        Message.Add(TopPr1)
        Message.Add(CalculateCutSetsProba())

        FileClose(RawFileID)

        Message.Show()

        Exit Sub
Err_Renamed:
        MsgBox("Fails to Read Cut Sets")
    End Sub

    ' RAW file 내 event name 을 정리
    Public Sub TrimEventName()
        Dim ss() As String
        Dim Ssep(2) As String
        Ssep(0) = " "
        Ssep(1) = vbNullChar
        Ssep(2) = vbTab

        For i = 1 To XData.NumOfEvent
            ss = XData.EventName(i).Split(Ssep, System.StringSplitOptions.RemoveEmptyEntries)
            XData.EventName(i) = ss(0)
        Next
    End Sub

    ' mdb File 읽기
    Public Sub ReadDataFile(ByRef DataFileName As String)

        Dim MyRs As New ADODB.Recordset()
        Dim MyCn As New ADODB.Connection()
        Dim dbSource As String
        Dim dbProvider As String
        Dim dbPath As String
        Dim dbTable, dbTableCommand As String
        Dim Evtname(), s As String
        Dim EvtProb(), pE As Single
        Dim index, iE As Integer

        dbSource = DataFileName
        dbProvider = "Provider=Microsoft.jet.oledb.4.0;data source="
        dbPath = dbProvider & dbSource
        MyCn.Open(dbPath)

        dbTable = InputBox("Table")
        dbTableCommand = "Select * from " & dbTable
        MyRs.Open(dbTableCommand, MyCn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic)

        'Create and fill the DataSet from the Recordset. Populate the grid from the DataSet. 
        Dim myDA As OleDbDataAdapter = New OleDbDataAdapter()
        Dim myDS As DataSet = New DataSet()
        myDA.Fill(myDS, MyRs, dbTable)

        Dim arrCount As Integer
        arrCount = myDS.Tables(0).Rows.Count
        'MessageBox.Show(arrCount)

        ReDim Evtname(arrCount)
        ReDim EvtProb(arrCount)
        ReDim XData.EventProbR(XData.NumOfEvent)


        For index = 0 To arrCount - 1
            Evtname(index + 1) = myDS.Tables(0).Rows(index).Item(0)
            Evtname(index + 1) = Trim(Evtname(index + 1))
            EvtProb(index + 1) = myDS.Tables(0).Rows(index).Item(3)
        Next
        MyRs = Nothing
        MyCn.Close()
        MyCn = Nothing

        Message.Add("-- Event in DB ---- ")
        s = ""
        For index = 1 To arrCount
            pE = EvtProb(index)    ' Proba
            iE = GetIndexOfEvent(Evtname(index)) ' Event Index
            If (iE = 9900) Then
                s = Evtname(index) & vbTab & vbTab & pE
                Message.Add(s)
                GoTo n1
            End If
            XData.EventProbR(iE) = pE

            s = XData.EventName(iE) & vbTab & iE.ToString
            Message.Add(s)
            Message.Add((XData.EventProb(iE) - XData.EventProbR(iE)))
n1:
        Next
        Message.Show()

    End Sub

    ' 주어진 이름이 몇번째 XEvents()에 저장되어 있는지 Index Return 
    ' 이름이 없으면 9900 return
    Public Function GetIndexOfEvent(EvtName As String) As Integer
        Dim xEName As String
        'MessageBox.Show(EvtName)
        ' 기존에 Xevents()에 저장된 것이면 찾아서 Index를 Return --
        For i = 1 To XData.NumOfEvent
            xEName = Trim(XData.EventName(i))
            If (EvtName = xEName) Then
                Return i
            End If
        Next
        Return 9900
    End Function

    'Monte Calro Uncertainty Analysis
    Public Sub CalculateMonte(NoSamples As Integer, FileName As String)

        Dim sx As String = "start"
        Dim uncerR(NoSamples - 1) As Single

        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        'Call RSample()

        'ReDim uncerR(NoSamples)

        'Start a new workbook in Excel
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add
        oSheet = oBook.Worksheets(1)

        oExcel.Visible = False

        For j = 1 To XData.NumOfEvent
            XData.EventProb(j) = XData.EventProbR(j)
        Next
        oSheet.Range("A1:A1").Value = CalculateCutSetsProba()

        'Add headers to the worksheet on row 1
        Dim Umax, Umin, Ustep, Sd, Mean, P5, P50, P95, sum, UU As Single
        Dim NoUstep, i As Integer
        sum = 0.0

        For i = 1 To NoSamples

            For j = 1 To XData.NumOfEvent
                XData.EventProb(j) = UniFoR(0.0001, 0.001)
            Next
            uncerR(i - 1) = CalculateCutSetsProba()
            sx = i.ToString & vbTab & " | " & uncerR(i - 1).ToString
            'Transfer the array to the worksheet starting at cell A2
            oSheet.Cells(i + 1, 1).Value = uncerR(i - 1)
            sum = sum + ((uncerR(i - 1) - Mean) ^ 2)
            Message.Add(sx) ' 확인용 LHK
        Next

        oSheet.Cells(1, 3).Value = "Mean"
        oSheet.Cells(2, 3).Value = "Sd"
        oSheet.Cells(4, 3).Value = "Umax"
        oSheet.Cells(5, 3).Value = "Umin"
        oSheet.Cells(6, 3).Value = "P5"
        oSheet.Cells(7, 3).Value = "P50"
        oSheet.Cells(8, 3).Value = "P95"

        Array.Sort(uncerR)
        Mean = uncerR.Average
        Umax = uncerR.Max
        Umin = uncerR.Min
        Sd = Math.Sqrt(sum) / (NoSamples - 1)
        P5 = uncerR(Math.Round(NoSamples / 100 * 5) - 1)
        P50 = uncerR(Math.Round(NoSamples / 100 * 50) - 1)
        P95 = uncerR(Math.Round(NoSamples / 100 * 95) - 1)
        oSheet.Cells(1, 4).Value = Mean
        oSheet.Cells(2, 4).Value = Sd
        oSheet.Cells(4, 4).Value = Umax
        oSheet.Cells(5, 4).Value = Umin
        oSheet.Cells(6, 4).Value = P5
        oSheet.Cells(7, 4).Value = P50
        oSheet.Cells(8, 4).Value = P95

        NoUstep = Math.Round((Umax - Umin) / (Umin * 0.4))
        Ustep = (Umax - Umin) / NoUstep
        Message.Add(NoUstep)
        Message.Add(Ustep)
        i = 0
        Do
            UU = Umin * 0.1 + i * Ustep
            oSheet.Cells(i + 1, 2).Value = UU
            i = i + 1
        Loop While UU < Umax * 1.2

        oBook.SaveAs(FileName)
        oExcel.Quit()
        Message.Show()

    End Sub
End Class
