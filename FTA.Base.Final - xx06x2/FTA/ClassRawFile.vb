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
        Dim EventDistrN() As String   ' Event Distribution Name
        Dim EventDistrP1() As Single   ' Event Distribution Parameter 1
        Dim EventDistrP2() As Single   ' Event Distribution Parameter 2

        Dim index() As Integer
        Dim NumOfIE, NumOfSeis, NumOfR As Integer

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
        Dim SElems() As Integer ' Rearrange 된 event 번호 (0, NoElem-1)
        'Dim FVI As Single '//
        'Dim Acc As Single '//
    End Structure

    Public Structure XCutSetType
        Dim BlockName As String ' Block Name (참고용임)
        Dim NumOfCutSet As Integer ' # of Cut Sets (0, NumOfCutSet - 1)
        Dim XaCutSet() As XaCutSetType ' 각 Cut Set   -   Range : (0, NumOfCutSet-1)

        Dim index() As Integer
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
    Public Function CalculateCutSetsProba() As Single
        Dim i, NoSet_1 As Integer
        Dim j, NoElem_1 As Integer
        Dim E As Integer
        Dim V, F As Single

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
    Public Function EventProbaCS(ByRef Evt As Integer) As Single
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
        Dim EvtName, EvtNameD, s As String   'VB6.FixedLengthString(32)
        Dim i, NoElem, Elem As Integer
        Dim j As Integer, RawFileID As Integer
        Dim ID As Short
        Dim Pr As Single
        Dim TopPr1 As Double
        Dim TopPr, Prob As Single

        Dim S1, S2 As String

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

        Console.WriteLine(DateTime.Now.ToString)
        'Message.Add(XData.NumOfEvent)  ' event 확인용 LHK
        'For i = 1 To XData.NumOfEvent
        '    Message.Add(i.ToString & "    " & XData.EventName(i))
        'Next
        'Message.Show()

        FileGet(RawFileID, Pr)

        ' Cut Set Block ----------------------------
        FileGet(RawFileID, BlockType)
        FileGet(RawFileID, BlockName)
        FileGet(RawFileID, nCutset)
        XCutSet.NumOfCutSet = nCutset

        ReDim XCutSet.XaCutSet(nCutset)

        TopPr = 0.0F
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
            XCutSet.XaCutSet(i).CutProba = Prob

            TopPr = TopPr + Prob
        Next i

        Console.WriteLine(DateTime.Now.ToString)
        'Message.Add(nCutset)  ' cutset 확인용 LHK
        'If (XCutSet.NumOfCutSet < 10000) Then
        '    For i = 0 To nCutset - 1
        '        s = i.ToString & "   " & XCutSet.XaCutSet(i).CutProba.ToString
        '        For j = 0 To XCutSet.XaCutSet(i).NoElem - 1
        '            If (XCutSet.XaCutSet(i).Elems(j) > 0) Then
        '                s = s & " = " & XData.EventName(XCutSet.XaCutSet(i).Elems(j))
        '            Else
        '                s = s & " = " & "-" & XData.EventName(-XCutSet.XaCutSet(i).Elems(j))
        '            End If
        '        Next
        '        Message.Add(s)
        '    Next
        'End If

        FileGet(RawFileID, TopPr1)

        Message.Add(TopPr1)
        Message.Add(CalculateCutSetsProba())

        FileClose(RawFileID)

        Message.Show()

        S1 = InputBox("IE Pattern")
        S2 = InputBox("SEISMIC Pattern")
        Call Sub() SortingEventNames(S1, S2)

        Console.WriteLine(DateTime.Now.ToString)

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

    ' RAW file 내 event name 순서를 정리 : Sorting Event Names: 초기사건그룹 + 지진사건그룹 + 내부사건 기본사건그룹
    ' Xdata.Sort(i) = 순서대로 조정된 이벤트번호
    Public Sub SortingEventNames(S1 As String, S2 As String)

        Dim temp() As Integer

        XData.NumOfIE = 0
        XData.NumOfSeis = 0
        XData.NumOfR = 0

        ' Reodrering of events
        ReDim XData.index(XData.NumOfEvent) '기본사건 그룹별로 정리된 순서 시작은 index(1)=초기사건

        For i = 1 To XData.NumOfEvent
            If (XData.EventName(i).Contains(S1)) Then
                XData.NumOfIE = XData.NumOfIE + 1
                XData.index(i) = XData.NumOfIE
            ElseIf (XData.EventName(i).Contains(S2)) Then
                XData.NumOfSeis = XData.NumOfSeis + 1
                XData.index(i) = XData.NumOfSeis
            Else
                XData.NumOfR = XData.NumOfR + 1
                XData.index(i) = XData.NumOfR
            End If
        Next

        ReDim temp(XData.NumOfEvent)
        For i = 1 To XData.NumOfEvent
            temp(i) = XData.index(i)
        Next

        If XData.NumOfIE + XData.NumOfSeis + XData.NumOfR = XData.NumOfEvent Then
            For i = 1 To XData.NumOfEvent
                If (XData.EventName(i).Contains(S1)) Then
                    XData.index(i) = temp(i)
                ElseIf (XData.EventName(i).Contains(S2)) Then
                    XData.index(i) = XData.NumOfIE + temp(i)
                Else
                    XData.index(i) = XData.NumOfIE + XData.NumOfSeis + temp(i)
                End If
            Next

            For i = 1 To XData.NumOfEvent
                temp(i) = XData.index(i)
            Next

            For i = 1 To XData.NumOfEvent
                XData.index(temp(i)) = i
            Next
        Else
            MsgBox("Fails to Ordering of Events")
        End If

        ' Reordering of cutsets
        Dim j As Integer
        Dim array_size As Integer = XCutSet.NumOfCutSet - 1
        Dim a(array_size) As Single
        Dim tindex(array_size) As Integer

        ReDim XCutSet.index(XCutSet.NumOfCutSet) 'XCutset.index(0)=최대값, XCutset.index(NumOfCutset-1)=최소값
        For i = 0 To array_size
            tindex(i) = i
            a(i) = XCutSet.XaCutSet(i).CutProba
        Next

        Array.Sort(a, tindex)
        For i = 0 To array_size
            XCutSet.index(i) = tindex(array_size - i)
        Next
        Console.WriteLine(DateTime.Now.ToString)

        'For i = 0 To array_size
        '    Console.WriteLine(i & "  " & XCutSet.XaCutSet(XCutSet.index(i)).CutProba)
        'Next

    End Sub
    ' mdb File 읽기
    Public Sub ReadDataFile(ByRef DataFileName As String)

        Dim MyRs As New ADODB.Recordset()
        Dim MyCn As New ADODB.Connection()
        Dim dbSource As String
        Dim dbProvider As String
        Dim dbPath As String
        Dim dbTable, dbTableCommand As String
        Dim Evtname(), s, columnNames As String
        Dim EvtProb(), pE As Single
        Dim EvtDistrN() As String
        Dim EvtDistrP1() As Single
        Dim EvtDistrP2() As Single
        Dim index, iE As Integer

        'On Error GoTo Err_Renamed

        dbSource = DataFileName
        dbProvider = "Provider=Microsoft.jet.oledb.4.0;data source="
        dbPath = dbProvider & dbSource
        MyCn.Open(dbPath)

        'dbTable = InputBox("Table") '테이블 이름 지정
        dbTable = "event"           '테이블 이름 지정
        columnNames = "Name, Type, Mean, CalType, Lamda, LamdaUnit, Tau, TauUnit, Factor, EF, DistType, DistPara1, DistPara2" ', Type, Mean, CalType, Lamda, LamdaUnit, Tau, TauUnit, Factor, EF, DistType, DistPara1, DistPara2"
        dbTableCommand = "Select " & columnNames & " from " & dbTable
        'dbTableCommand = "Select * from " & dbTable
        MyRs.Open(dbTableCommand, MyCn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic)

        'Create and fill the DataSet from the Recordset. Populate the grid from the DataSet. 
        Dim myDA As OleDbDataAdapter = New OleDbDataAdapter()
        Dim myDS As DataSet = New DataSet()
        myDA.Fill(myDS, MyRs, dbTable)

        Dim arrCount As Integer

        arrCount = myDS.Tables(0).Rows.Count

        Message.Add(arrCount)

        ReDim Evtname(arrCount)
        ReDim EvtProb(arrCount)
        ReDim EvtDistrN(arrCount)
        ReDim EvtDistrP1(arrCount)
        ReDim EvtDistrP2(arrCount)

        ReDim XData.EventProbR(XData.NumOfEvent)
        ReDim XData.EventDistrN(XData.NumOfEvent)
        ReDim XData.EventDistrP1(XData.NumOfEvent)
        ReDim XData.EventDistrP2(XData.NumOfEvent)

        For index = 0 To arrCount - 1
            Evtname(index + 1) = myDS.Tables(0).Rows(index).Item(0)
            Evtname(index + 1) = Trim(Evtname(index + 1))
            EvtProb(index + 1) = myDS.Tables(0).Rows(index).Item(2)
            EvtDistrN(index + 1) = myDS.Tables(0).Rows(index).Item(10)
            EvtDistrP1(index + 1) = myDS.Tables(0).Rows(index).Item(11)
            EvtDistrP2(index + 1) = myDS.Tables(0).Rows(index).Item(12)
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

            XData.EventProbR(iE) = EvtProb(index)
            XData.EventDistrN(iE) = Trim(EvtDistrN(index))
            XData.EventDistrP1(iE) = EvtDistrP1(index)
            XData.EventDistrP2(iE) = EvtDistrP2(index)

            s = XData.EventName(iE) & "-" & XData.EventDistrN(iE).ToString & "-" & (XData.EventProb(iE) - XData.EventProbR(iE)).ToString
            Message.Add(s)

            ' mdb 의 값이 정상적이 않음. random sampling 과정의 에러를 방지하기 위한 조건식임.
            If Strings.InStr(1, XData.EventDistrN(iE), "L") + Strings.InStr(1, XData.EventDistrN(iE), "l") = 1 And XData.EventDistrP2(iE) > 1.0 Then
                XData.EventDistrN(iE) = "L"
                XData.EventDistrP2(iE) = Math.Log(XData.EventDistrP2(iE)) / 1.645
                XData.EventDistrP1(iE) = Math.Log(XData.EventProbR(iE)) - Math.Pow(XData.EventDistrP2(iE), 2.0) / 2.0
                s = XData.EventDistrN(iE) & "-" & XData.EventDistrP1(iE).ToString & "-" & XData.EventDistrP2(iE).ToString
                Message.Add(s)
            Else
                XData.EventDistrN(iE) = " "
            End If
n1:
        Next

        Message.Show()
        Exit Sub

Err_Renamed:
        MsgBox("Fails to Read mdb File")

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

        'On Error GoTo Err_Renamed

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
        Dim R5000EventProb() As Double = {0.0#}
        Dim XX(,) As Double = {{0.0#, 0.0#}}
        sum = 0.0

        ' 수정시작 -x06
        'For j = 1 To XData.NumOfEvent
        '    If XData.EventDistrN(j) = "L" Then
        '        Call LogN(XData.EventDistrP1(j), XData.EventDistrP2(j), NoSamples, R5000EventProb)
        '        MessageBox.Show(j.ToString)
        '        For i = 1 To NoSamples
        '            XX(i, j) = R5000EventProb(j)
        '        Next

        '        sx = j.ToString & "_" & XData.EventName(j) & "-" & XData.EventProb(j).ToString
        '        Message.Add(sx)

        '    End If
        'Next

        'For i = 1 To NoSamples

        '    For j = 1 To XData.NumOfEvent
        '        If XData.EventDistrN(j) = "L" Then
        '            XData.EventProb(j) = XX(j, i)
        '        End If
        '    Next
        ' 수정 끝

        For i = 1 To NoSamples

            For j = 1 To XData.NumOfEvent
                If XData.EventDistrN(j) = "L" Then
                    XData.EventProb(j) = LogN(XData.EventDistrP1(j), XData.EventDistrP2(j))
                    sx = j.ToString & "_" & XData.EventName(j) & "-" & XData.EventProb(j).ToString
                    Message.Add(sx)

                End If
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
        Exit Sub

Err_Renamed:
        MsgBox("Fails to Write Uncertainty Results")

    End Sub

    ' Text Data FileName 가져온 후, Text Data 읽기
    Public Sub ReadTextDataFile(ByRef DataFileName As String)

        Dim aLine As String
        Dim sL() As String, Sep(1) As String
        Dim iE As Integer, pE As Single
        Dim S As String = ""        ' FT 내용을 기록할 String

        Message.Add("-- No Event in Cutsets ---- ")

        Sep(0) = " " : Sep(1) = vbTab

        ' File 열기 (using StreamReader)
        Dim strReader As System.IO.StreamReader = System.IO.File.OpenText(DataFileName)

        Do While (strReader.Peek() <> -1)   ' IMPORT 까지 한줄씩 읽기
            aLine = strReader.ReadLine
            If (aLine.ToUpper = "IMPORT") Then Exit Do
        Loop

        ReDim XData.EventProbR(XData.NumOfEvent)

        ' Data 읽기
        Do While (strReader.Peek() <> -1)
            aLine = strReader.ReadLine   ' 한줄 읽기
            sL = aLine.Split(Sep, StringSplitOptions.RemoveEmptyEntries)     ' Word 단위로 분리하여 SL() 에 저장

            If (sL(0).ToUpper = "LIMIT") Then Exit Do ' LIMIT 까지 읽기
            ' Event에 대한 Data
            pE = Convert.ToSingle(sL(0))    ' Proba
            iE = GetIndexOfEvent(sL(1)) ' Event Index
            'MessageBox.Show(iE)

            If (iE = 9900) Then
                S = sL(1) & " | " & vbTab & pE
                Message.Add(S)
                GoTo n1
            End If

            XData.EventProbR(iE) = pE
            S = XData.EventName(iE) & " | " & iE.ToString & vbTab & XData.EventProb(iE) & XData.EventProbR(iE)
            Message.Add(S)

n1:
        Loop

        Message.Show()
        strReader.Close()   ' File 닫기

    End Sub

End Class
