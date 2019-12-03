Public Class ClassFtData

    Public Const MxChild = 8       ' 최대 Child 수 
    Public Const MxEvent = 1000   ' 최대 Event 수 

    Public Structure XEvent_Type
        Dim Name As String
        Dim Type As String
        Dim Proba As Single
        Dim NoChild As Integer      ' 실제 갯수만큼
        Dim Child() As Integer      ' Child(MxChild)  : (0 ~ NoChild-1)

        Public Sub SetChildArr()
            ReDim Child(MxChild)
        End Sub
    End Structure

    ' FT Structure 관련
    Public XEvents(MxEvent) As XEvent_Type      ' (0 - NoEvent)
    Public NoEvent As Integer  ' # of XEvents
    Public FtFileName As String

    Public Sub ReadFT(FileName As String)
        If System.IO.File.Exists(FileName) Then

            FtFileName = FileName

            'Open a text file using StreamReader
            Dim strReader As System.IO.StreamReader = System.IO.File.OpenText(FileName)

            ' 초기화 
            NoEvent = 1 ' 2부터 저장되도록 (0 - False, 1 - True 용)

            ' Read FT Block
            Call ReadFtBlock(strReader)

            ' Read Data Block
            Call ReadDataBlock(strReader)

            strReader.Close()
        End If

    End Sub

    Private Sub ReadFtBlock(strReader As System.IO.StreamReader)

        Dim aLine As String
        Dim sL() As String, Sep(1) As String
        Dim iGate, iC, nC As Integer

        Sep(0) = " "
        Sep(1) = vbTab

        Do While (strReader.Peek() <> -1)   ' 끝이 아니면 계속 읽기

            ' 한줄 읽기
            aLine = strReader.ReadLine

            If (aLine.Substring(0, 1) <> "*") Then  ' * 가 아니면

                If (aLine.ToUpper = "ENDTREE") Then ' ENDTREE 까지 읽기
                    Exit Do
                End If

                ' Word 단위로 분리하여 SL() 에 저장
                sL = aLine.Split(Sep, StringSplitOptions.RemoveEmptyEntries)

                ' Name, Type, Child로 분리
                'If (sL.Length > 2) Then
                iGate = GetIndexOfEvent(sL(0))
                XEvents(iGate).Type = sL(1)
                nC = 0
                For j = 2 To sL.Length - 1
                    nC += 1
                    iC = GetIndexOfEvent(sL(j))
                    XEvents(iGate).Child(nC - 1) = iC
                Next
                XEvents(iGate).NoChild = nC
                'End If

            End If

        Loop

    End Sub

    Private Sub ReadDataBlock(strReader As System.IO.StreamReader)
        Dim aLine As String
        Dim sL() As String, Sep(1) As String
        Dim iE As Integer, pE As Single

        Sep(0) = " "
        Sep(1) = vbTab

        Do While (strReader.Peek() <> -1)
            ' 한줄 읽기
            aLine = strReader.ReadLine
            If (aLine.ToUpper = "IMPORT") Then  ' IMPORT 까지 읽기
                Exit Do
            End If
        Loop

        ' Data 읽기
        Do While (strReader.Peek() <> -1)
            ' 한줄 읽기
            aLine = strReader.ReadLine

            ' Word 단위로 분리하여 SL() 에 저장
            sL = aLine.Split(Sep, StringSplitOptions.RemoveEmptyEntries)

            If (sL(0).ToUpper = "LIMIT") Then ' LIMIT 까지 읽기
                Exit Do
            End If

            ' Event에 대한 Data
            pE = Convert.ToSingle(sL(0))    ' Proba
            iE = GetIndexOfEvent(sL(1))     ' Event Index

            XEvents(iE).Proba = pE
            XEvents(iE).Type = "B"
        Loop

    End Sub

    Public Function GetIndexOfEvent(S As String) As Integer
        For i = 2 To NoEvent
            If (S = XEvents(i).Name) Then
                Return i
            End If
        Next

        ' 새로운 Event
        NoEvent += 1
        XEvents(NoEvent).Name = S
        XEvents(NoEvent).NoChild = 0
        Call XEvents(NoEvent).SetChildArr()

        Return (NoEvent)

    End Function

    'Public Function GetIndexForFT(S As String) As Integer
    '    Return GetIndexOfEvent(S)
    'End Function

    Public Sub PrintFT()
        Dim S As String = ""
        Dim e As Integer

        S = "-- FT ---- "
        For i = 2 To NoEvent
            If (XEvents(i).NoChild > 0) Then
                S += vbCrLf & i.ToString & " | " & XEvents(i).Name & vbTab & XEvents(i).Type
                For j = 0 To XEvents(i).NoChild - 1
                    e = XEvents(i).Child(j)
                    S += vbTab & e & " | " & XEvents(e).Name
                Next
            Else
                S += vbCrLf & i.ToString & " | " & XEvents(i).Name & vbTab & XEvents(i).Proba
            End If
        Next

        Message(S)

    End Sub

End Class
