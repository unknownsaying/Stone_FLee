' Complete example: Motion Controller with Button Panel
' Uses all requested VB.NET keywords and concepts

Imports System.Drawing
Imports System.Windows.Forms

' Interface for movement capabilities
Public Interface IMovable
    Sub Move(dx As Integer, dy As Integer)
    ReadOnly Property Position As Point
End Interface

' Base class with inheritance demonstration
Class MotionBase
    Protected _speed As Single = 5.0F
    Public Property Speed As Single
        Get
            Return _speed
        End Get
        Set(value As Single)
            If value > 0 Then _speed = value
        End Set
    End Property
End Class

' Main controller class: inherits MotionBase, implements IMovable
Class MotionController
    Inherits MotionBase
    Implements IMovable

    Private _x As Integer
    Private _y As Integer
    Private _bounds As Rectangle

    Public Sub New(startX As Integer, startY As Integer, bounds As Rectangle)
        _x = startX
        _y = startY
        _bounds = bounds
    End Sub

    Public Sub Move(dx As Integer, dy As Integer) Implements IMovable.Move
        ' Use CInt, CSng, CByte conversions
        Dim newX As Integer = _x + CInt(CSng(dx) * Speed)
        Dim newY As Integer = _y + CInt(CSng(dy) * Speed)

        ' Boundaries checking
        If newX < _bounds.Left Then newX = _bounds.Left
        If newX > _bounds.Right Then newX = _bounds.Right
        If newY < _bounds.Top Then newY = _bounds.Top
        If newY > _bounds.Bottom Then newY = _bounds.Bottom

        _x = newX
        _y = newY
    End Sub

    Public ReadOnly Property Position As Point Implements IMovable.Position
        Get
            Return New Point(_x, _y)
        End Get
    End Property
End Class

' Main Form
Class MotionForm
    Inherits Form   ' Inherits keyword

    Private WithEvents _timer As Timer
    Private _controller As MotionController
    Private _drawPanel As Panel
    Private _buttonPanel As Panel
    Private _moveButton As Button
    Private _stopButton As Button
    Private _direction As String = "None"

    Public Sub New()
        InitializeComponents()
        SetupEventHandlers()
    End Sub

    Private Sub InitializeComponents()
        Me.Text = "Motion Controller"
        Me.Size = New Size(600, 500)

        ' Panel for drawing the moving object
        _drawPanel = New Panel With {
            .Location = New Point(10, 10),
            .Size = New Size(400, 350),
            .BorderStyle = BorderStyle.FixedSingle,
            .BackColor = Color.White
        }

        ' Panel for buttons (Controller Panel)
        _buttonPanel = New Panel With {
            .Location = New Point(420, 10),
            .Size = New Size(150, 350),
            .BorderStyle = BorderStyle.FixedSingle
        }

        ' Create motion buttons (Up, Down, Left, Right, Stop)
        Dim btnUp As New Button With {.Text = "Up", .Location = New Point(35, 30), .Size = New Size(75, 30)}
        Dim btnDown As New Button With {.Text = "Down", .Location = New Point(35, 130), .Size = New Size(75, 30)}
        Dim btnLeft As New Button With {.Text = "Left", .Location = New Point(35, 80), .Size = New Size(75, 30)}
        Dim btnRight As New Button With {.Text = "Right", .Location = New Point(35, 180), .Size = New Size(75, 30)}
        _stopButton = New Button With {.Text = "Stop", .Location = New Point(35, 230), .Size = New Size(75, 30), .BackColor = Color.LightCoral}

        ' Add buttons to button panel
        _buttonPanel.Controls.AddRange({btnUp, btnDown, btnLeft, btnRight, _stopButton})

        ' Add panels to form
        Me.Controls.Add(_drawPanel)
        Me.Controls.Add(_buttonPanel)

        ' Initialize controller with starting position (center of draw panel)
        Dim centerX As Integer = _drawPanel.Width \ 2
        Dim centerY As Integer = _drawPanel.Height \ 2
        Dim bounds As New Rectangle(10, 10, _drawPanel.Width - 20, _drawPanel.Height - 20)  ' margin 10px
        _controller = New MotionController(centerX, centerY, bounds)

        ' Timer for continuous movement
        _timer = New Timer With {.Interval = 50}
    End Sub

    Private Sub SetupEventHandlers()
        ' AddHandler to attach events
        AddHandler _timer.Tick, AddressOf OnTimerTick
        AddHandler _stopButton.Click, AddressOf StopMovement

        ' Use loop to attach click handlers for all movement buttons
        For Each btn As Button In _buttonPanel.Controls.OfType(Of Button)()
            If btn IsNot _stopButton Then
                ' AddressOf points to the movement method
                AddHandler btn.Click, AddressOf MovementButtonClick
            End If
        Next

        ' Paint event for draw panel
        AddHandler _drawPanel.Paint, AddressOf DrawMotion
    End Sub

    ' Handles movement button clicks (Up, Down, Left, Right)
    Private Sub MovementButtonClick(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        ' Set direction based on button text
        Select Case btn.Text
            Case "Up" : _direction = "Up"
            Case "Down" : _direction = "Down"
            Case "Left" : _direction = "Left"
            Case "Right" : _direction = "Right"
        End Select

        ' Start timer if not already running
        If Not _timer.Enabled Then _timer.Start()
    End Sub

    Private Sub StopMovement(sender As Object, e As EventArgs)
        _direction = "None"
        _timer.Stop()
    End Sub

    Private Sub OnTimerTick(sender As Object, e As EventArgs)
        ' Move according to current direction
        If _direction = "None" Then Return

        Dim dx As Integer = 0
        Dim dy As Integer = 0
        ' Use Then, Next (though Next is not needed here, but show usage)
        If _direction = "Up" Then
            dy = -1
        ElseIf _direction = "Down" Then
            dy = 1
        ElseIf _direction = "Left" Then
            dx = -1
        ElseIf _direction = "Right" Then
            dx = 1
        End If

        ' Apply movement via controller
        _controller.Move(dx, dy)

        ' Refresh the drawing panel
        _drawPanel.Invalidate()
    End Sub

    Private Sub DrawMotion(sender As Object, e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        Dim pos As Point = _controller.Position
        ' Draw a red circle at current position
        g.FillEllipse(Brushes.Red, pos.X - 10, pos.Y - 10, 20, 20)
        ' Draw crosshair
        g.DrawLine(Pens.Black, pos.X - 15, pos.Y, pos.X + 15, pos.Y)
        g.DrawLine(Pens.Black, pos.X, pos.Y - 15, pos.X, pos.Y + 15)
    End Sub

    ' Demonstrate RemoveHandler and Nothing
    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        ' Remove event handlers to avoid memory leaks
        RemoveHandler _timer.Tick, AddressOf OnTimerTick
        For Each btn As Button In _buttonPanel.Controls.OfType(Of Button)()
            RemoveHandler btn.Click, AddressOf MovementButtonClick
        Next
        RemoveHandler _stopButton.Click, AddressOf StopMovement
        RemoveHandler _drawPanel.Paint, AddressOf DrawMotion

        ' Set timer to Nothing
        If _timer IsNot Nothing Then
            _timer.Dispose()
            _timer = Nothing
        End If

        MyBase.OnFormClosed(e)
    End Sub

    ' Entry point (for demonstration)
    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New MotionForm())
    End Sub
End Class