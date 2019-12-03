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

    Public XData As XDataType       ' Event 확률 저장용
    Public XCutSet As XCutSetType   ' Cut Set 저장용

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
    Public Function CalculateCutSetsProba() As Single
        Dim i, NoSet_1 As Integer
        Dim j, NoElem_1 As Integer
        Dim E As Integer
        Dim V, F As Double
        V = 0 : NoSet_1 = Me.XCutSet.NumOfCutSet - 1

        For i = 0 To NoSet_1
            F = 1 : NoElem_1 = Me.XCutSet.XaCutSet(i).NoElem - 1 ' Cut Set 내 Element 수
            For j = 0 To NoElem_1
                E = Me.XCutSet.XaCutSet(i).Elems(j) ' Cut Set 내의 Event 하나
                F = F * EventProbaCS(E)
            Next j
            Me.XCutSet.XaCutSet(i).CutProba = F ' 확률 다시 할당
            V = V + F
        Next i
        CalculateCutSetsProba = V ' Top Event 확률
    End Function

    ' 각 Event의 확률 
    Public Function EventProbaCS(ByRef Evt As Integer) As Single
        If (Evt > 0) Then
            EventProbaCS = XData.EventProb(Evt)
        Else
            EventProbaCS = 1.0# - XData.EventProb(-Evt)
        End If
    End Function

    '------------------------------------------------------------------------------
    ' Save Cut Set
    ' Cut Set 저장하기 ---------------------------------------------------
    Public Sub SaveRawFile(ByRef RawFileName As String, ByRef CsBlockName As String)
        Dim DataBlockPos, CSBlockPos As Integer ' Raw File Position
        Dim BlockType As Char   'VB6.FixedLengthString(1)
        Dim BlockName As String 'VB6.FixedLengthString(32)
        Dim n As Integer
        Dim EvtName As String   'VB6.FixedLengthString(32)
        Dim i, NoElem, Elem As Integer
        Dim j As Integer, RawFileID As Integer
        Dim ID As Short
        Dim Pr As Single
        Dim TopPr As Double
        On Error GoTo Err_Renamed

        ' File 이 있으면 삭제
        If (IsExistFile(RawFileName)) Then
            My.Computer.FileSystem.DeleteFile(RawFileName)
        End If

        ' File Open -------------------------------
        RawFileID = FreeFile()
        FileOpen(RawFileID, RawFileName, OpenMode.Binary, OpenAccess.Write) ' File 열기
        ID = 20301
        FilePut(RawFileID, ID)

        ' Data Block ------------------------------
        ' block type byte
        BlockType = "d"
        FilePut(RawFileID, BlockType)
        BlockName = Space(32)
        FilePut(RawFileID, BlockName)

        n = XData.NumOfEvent
        FilePut(RawFileID, n)

        For i = 1 To n
            Pr = XData.EventProb(i)
            EvtName = XData.EventName(i)
            EvtName = FillNumberOfChar(EvtName, 32)
            FilePut(RawFileID, Pr)
            FilePut(RawFileID, EvtName)
        Next i

        Pr = 0.0#
        FilePut(RawFileID, Pr)

        ' Cut Set Block ----------------------------
        BlockType = "-"
        FilePut(RawFileID, BlockType)
        BlockName = FillNumberOfChar(CsBlockName, 32)
        FilePut(RawFileID, BlockName)

        n = XCutSet.NumOfCutSet
        FilePut(RawFileID, n)

        TopPr = 0.0#
        For i = 0 To n - 1
            NoElem = XCutSet.XaCutSet(i).NoElem
            Pr = XCutSet.XaCutSet(i).CutProba

            FilePut(RawFileID, Pr)
            FilePut(RawFileID, NoElem)
            For j = 0 To NoElem - 1
                Elem = XCutSet.XaCutSet(i).Elems(j)
                FilePut(RawFileID, Elem)
            Next j
            TopPr = TopPr + Pr
        Next i

        FilePut(RawFileID, TopPr)

        FileClose(RawFileID)
        Exit Sub
Err_Renamed:
        MsgBox("Fails to Save Cut Sets")
    End Sub

    ' 정해진 글자수로 String 채우기
    Private Function FillNumberOfChar(ByVal S As String, ByVal No As Integer) As String
        Dim n As Integer
        n = S.Length
        If (n > No) Then        ' 만일 더 크면 자르기
            FillNumberOfChar = Mid(S, 1, No)
        Else
            FillNumberOfChar = S & Space(No - n)
        End If
    End Function

    ' 주어진 File이 있는지 점검 --------------------------------------
    Private Function IsExistFile(ByVal S As String) As Boolean
        On Error GoTo Err_Renamed
        IsExistFile = My.Computer.FileSystem.FileExists(S)
Err_Renamed:
    End Function


End Class
