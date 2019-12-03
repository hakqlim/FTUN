Module Module2
    'Public Declare Function BiN Lib "C:\Users\kings\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Integer, ByVal s As Double) As Integer
    'Public Declare Function Geom Lib "C:\Users\kings\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double) As Integer
    'Public Declare Function Poiss Lib "C:\Users\kings\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double) As Integer

    'Public Declare Function Expon Lib "C:\Users\kings\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double) As Double
    'Public Declare Function LogN Lib "C:\Users\kings\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double, ByVal s As Double) As Double
    'Public Declare Function Gamma Lib "C:\Users\kings\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double, ByVal s As Double) As Double
    'Public Declare Function Norma Lib "C:\Users\kings\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double, ByVal s As Double) As Double
    'Public Declare Function UniFoR Lib "C:\Users\kings\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double, ByVal s As Double) As Double
    Public Declare Function BiN Lib "C:\Users\임학규\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Integer, ByVal s As Double) As Integer
    Public Declare Function Geom Lib "C:\Users\임학규\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double) As Integer
    Public Declare Function Poiss Lib "C:\Users\임학규\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double) As Integer

    Public Declare Function Expon Lib "C:\Users\임학규\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double) As Double
    Public Declare Function LogN Lib "F:\download document\desktop\FTA_Execise - x06 - 복사본\xaigo\Debug\aigo.dll" (ByVal m As Double, ByVal s As Double) As Double
    Public Declare Function Gamma Lib "C:\Users\임학규\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double, ByVal s As Double) As Double
    Public Declare Function Norma Lib "C:\Users\임학규\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double, ByVal s As Double) As Double
    Public Declare Function UniFoR Lib "C:\Users\임학규\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double, ByVal s As Double) As Double
    Public Declare Function BetaR Lib "F:\download document\desktop\FTA_Execise - x06 - 복사본\xaigo\Debug\aigo.dll" (ByVal m As Double, ByVal s As Double) As Double

    Private Declare Sub AddLongs_Pointer Lib "F:\download document\desktop\FTA_Execise - x06 - 복사본\test\Debug\test.dll" (ByRef FirstElement As Double, ByVal lElements As Integer, ByVal Itype As Integer, ByVal m As Double, ByVal s As Double)
    Sub Test(nsample As Integer)
        Dim ArrayOfLongs() As Double
        Dim k As Integer = 0

        ReDim ArrayOfLongs(nsample)
        Dim m As Double = 0.0
        Dim s As Double = 1.0
        Dim IType As Integer = 3
        Call AddLongs_Pointer(ArrayOfLongs(0), UBound(ArrayOfLongs) + 1, IType, m, s)
        For k = 0 To UBound(ArrayOfLongs)
            Console.WriteLine(ArrayOfLongs(k))
        Next
    End Sub

    Sub RSample()
        Dim m, s As Double
        Dim RExpon, RLogN, RGamma, RNorma, RUniFoR, RBetaR As Double
        Dim intm, IBiN, IGeom, IPoiss As Integer
        Dim RSum As String
        Dim R() As Double = {0.0#}

        s = 88.0#
        m = 0.5#
        intm = 10

        For i = 0 To 1000
            'IBiN = BiN(intm, s)
            'IGeom = Geom(s)
            'IPoiss = Poiss(m)
            'RSum = CStr(IBiN) & "   :   " & CStr(IGeom) & "   :   " & CStr(IPoiss)
            ''MsgBox(RSum)

            'RLogN = LogN(m, s)
            'RGamma = Gamma(m, s)
            'RNorma = Norma(m, s)
            'RExpon = Expon(m)
            'RUniFoR = UniFoR(m, m + 10.0#)
            RBetaR = BetaR(m, s)
            'RSum = CStr(RLogN) & "   :   " & CStr(RGamma) & "   :   " & CStr(RNorma) & "   :   " & CStr(RExpon) & "   :   " & CStr(RUniFoR)
            'MsgBox(RSum)
            Console.WriteLine(RBetaR)
        Next
    End Sub
End Module
