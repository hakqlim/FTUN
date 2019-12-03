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
    Public Declare Function LogN Lib "C:\Users\임학규\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double, ByVal s As Double) As Double
    Public Declare Function Gamma Lib "C:\Users\임학규\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double, ByVal s As Double) As Double
    Public Declare Function Norma Lib "C:\Users\임학규\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double, ByVal s As Double) As Double
    Public Declare Function UniFoR Lib "C:\Users\임학규\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double, ByVal s As Double) As Double
    Public Declare Function BetaR Lib "C:\Users\임학규\Desktop\xaigo\Debug\aigo.dll" (ByVal m As Double, ByVal s As Double) As Double


    Sub RSample()
        Dim m, s As Double
        Dim RExpon, RLogN, RGamma, RNorma, RUniFoR, RBetaR As Double
        Dim intm, IBiN, IGeom, IPoiss As Integer
        Dim RSum As String
        Dim R() As Double = {0.0#}

        s = 88.0#
        m = 0.5#
        intm = 10

        For i = 0 To 10000
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
