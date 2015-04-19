' selfTimer.vb
'   This modules provides a simple Timer interface

Option Strict On
Option Explicit On
Imports System.Timers

Friend Class SelfTimer

#Region " Declarations "

    Private m_Enabled As Boolean ' Boolean indicating whether the timer is enabled or not
    Private m_Interval As Long ' Gets or sets the interval at which to raise the System.Timers.Timer.Elapsed event in milliseconds
    Private timer1 As Timer ' Timer
    Public Event TimerEvent() ' Timer event. The event handler is defined in the clsMasa class

#End Region

#Region " Properties "

    ' Boolean indicating whether the timer is enabled or not
    Public Property Enabled() As Boolean
        Get
            Enabled = m_Enabled
        End Get
        Set(ByVal Value As Boolean)
            ' The order is important !!!!
            timer1.Enabled = Value
            m_Enabled = Value
        End Set
    End Property

    ' Gets or sets the interval at which to raise the System.Timers.Timer.Elapsed event in milliseconds
    Public Property Interval() As Long
        Get
            Interval = m_Interval
        End Get
        Set(ByVal Value As Long)
            timer1.Interval = Value
            m_Interval = Value
        End Set
    End Property

#End Region

#Region " Timer Operations "

    ' On timer event. Calls Timer_Renamed().
    Private Sub OnTimerEvent(ByVal [source] As Object, ByVal e As EventArgs)
        RaiseEvent TimerEvent()
    End Sub

    ' New
    Public Sub New()
        Try

            If m_Interval < 100 Then
                m_Interval = 1000 ' Milliseconds
            End If

            timer1 = New Timer()

            timer1.Enabled = m_Enabled
            timer1.Interval = m_Interval
            timer1.AutoReset = True

            AddHandler timer1.Elapsed, AddressOf OnTimerEvent

            If m_Enabled = True Then
                timer1.Start()
            End If

        Catch ex As Exception
            HandleAllExceptions(ex)
        End Try
    End Sub

#End Region

End Class