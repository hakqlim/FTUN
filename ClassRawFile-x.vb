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
    ' 각 Cut Set의 확률 계산
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
    ' 각 Event의 확률 
    Public Function EventProbaCS(ByRef Evt As Integer) As Double
        If (Evt > 0) Then
            EventProbaCS = XData.EventProb(Evt)
        Else
            EventProbaCS = 1.0# - XData.EventProb(-Evt)
        End If
    End Function
    ' LHK
    ' Read Cut Set
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
        Dim TopPr, Prob As Double
        Dim sx As String
        Dim Ssep(2), Ss() As String

        Ssep(0) = " "
        Ssep(1) = vbNullChar
        Ssep(2) = vbTab

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

                Ss = EvtName.Split(Ssep, System.StringSplitOptions.RemoveEmptyEntries)
                EvtName = Ss(0)

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

                Ss = EvtName.Split(Ssep, System.StringSplitOptions.RemoveEmptyEntries)
                EvtName = Ss(0)

                XData.EventName(i) = EvtName
                XData.EventProb(i) = Pr
            Next i
        End If

        Call PrintEvents()

        FileGet(RawFileID, Pr)

        ' Cut Set Block ----------------------------
        FileGet(RawFileID, BlockType)
        FileGet(RawFileID, BlockName)
        FileGet(RawFileID, nCutset)
        XCutSet.NumOfCutSet = nCutset

        MessageBox.Show(nCutset)  ' 확인용 LHK
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

        ' TopPr = CalculateCutSetsProba()
        sx = "start"
        Message.Add("start")
        For i = 1 To 10
            'MessageBox.Show(CalculateCutSetsProba()) ' 확인용 LHK
            sx = i.ToString & vbTab & " | " & CalculateCutSetsProba().ToString
            Message.Add(sx) ' 확인용 LHK
        Next
        Message.Show() ' 확인용 LHK

        FileGet(RawFileID, TopPr)

        FileClose(RawFileID)
        Exit Sub
Err_Renamed:
        MsgBox("Fails to Read Cut Sets")
    End Sub

    ' RAW file 내 events 내용 Print 
    Public Sub PrintEvents()
        Dim s As String

        Dim Ssep(2) As String
        'Ssep(0) = " "
        'Ssep(1) = vbNullChar
        'Ssep(2) = vbTab

        Message.Add("-- Data ---- ")
        For i = 1 To XData.NumOfEvent
            'ss = XData.EventName(i).Split(Ssep, System.StringSplitOptions.RemoveEmptyEntries)
            'XData.EventName(i) = ss(0)
            'MessageBox.Show(CStr(Len(ss(0))) & ss(0))
            s = XData.EventName(i) & " | " & i.ToString & vbTab & XData.EventProb(i)
            Message.Add(s)
        Next
        Message.Show()
    End Sub

    ' LHK
    ' File 읽기

    ' Data FileName 가져온 후, Data 읽기
    Public Sub ReadDataFile(ByRef DataFileName As String)

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

            XData.EventProb(iE) = pE
            S = XData.EventName(iE) & " | " & iE.ToString & vbTab & XData.EventProb(iE)
            Message.Add(S)

n1:
        Loop

        Message.Show()
        strReader.Close()   ' File 닫기

    End Sub
    ' 주어진 이름이 몇번째 XEvents()에 저장되어 있는지 Index Return 
    '   없으면 9900
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

    ' FT 내용 Print 
    Public Sub PrintDataFile()

        Dim S As String = ""        ' FT 내용을 기록할 String

        Message.Add("-- Data ---- ")

        For i = 1 To XData.NumOfEvent
            S = XData.EventName(i) & " | " & i.ToString & vbTab & XData.EventProb(i)
            Message.Add(S)
        Next
        Message.Show()

    End Sub
    Public Sub CalculateMonte(NoSamples As Integer)
        Dim sx As String = "start"

        For i = 1 To NoSamples

            For j = 1 To XData.NumOfEvent
                XData.EventProb(j) = Rnd()
            Next

            sx = i.ToString & vbTab & " | " & CalculateCutSetsProba().ToString
            Message.Add(sx) ' 확인용 LHK
        Next
        Message.Show()

    End Sub
End Class
