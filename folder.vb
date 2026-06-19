Imports e.vb
Imports EE.vb
Imports EeE.vb
Imports f.vb
Imports manifold.vb
Imports orbifold.vb
Public Sub New(ByVal path As String, ByVal name As String, ByVal description As String, ByVal owner As String)
    MyBase.New(path, name, description)
End Sub
Private Shared Sub New (Byref e,EE,EeE As E,Byref f As Boolean,Byref manifold As Boolean,Byref orbifold As Boolean)
    MyClass.New(E,f,manifold,orbifold)
End Sub