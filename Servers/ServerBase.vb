Option Strict On
Option Explicit On

Imports System.Text
Imports Dart.Sockets
Imports System.ComponentModel

Namespace DartServerDemo.Servers
	' This is the base class that all servers will derive from. 
	' It contains the common events, properties and methods.
	Public MustInherit Class ServerBase
		' Event that fires whenever a connection is added or removed
        'Public Event ConnectionsChanged As EventHandler

        Protected Overridable Function getConnectionsChangedEvent() As EventHandler
            Return Nothing
        End Function


        Private Sub OnConnectionsChanged(ByVal sender As Object, ByVal e As ConnectionsChangedEventArgs)
            'Dim hdlr As EventHandler = ConnectionsChangedEvent
            Dim hdlr As EventHandler = GetConnectionsChangedEvent()
            ' The following code is needed because the server is operating in a worker thread
            ' and this event will be firing on the UI thread
            If hdlr IsNot Nothing Then
                If (_Server.SynchronizingObject IsNot Nothing) AndAlso _Server.SynchronizingObject.InvokeRequired Then
                    _Server.SynchronizingObject.BeginInvoke(hdlr, New Object() {Me, e})
                Else
                    hdlr.DynamicInvoke(Me, e)
                End If
            End If
        End Sub

        ' Contructor
        ' name = A human readable name for the server (ex. "Server One")
        ' ort = The port that the server will listen on
        ' synchronizingObject = the object that the events will be marshalled to
        Public Sub New(ByVal name As String, ByVal port As Integer, ByVal synchronizingObject As ISynchronizeInvoke)
            _Name = name
            _Port = port
            _Server.SynchronizingObject = synchronizingObject
            ' Add the ConnectionsChanged event 
            AddHandler _Server.ConnectionsChanged, AddressOf _Server_ConnectionsChanged
        End Sub

        Private Sub _Server_ConnectionsChanged(ByVal sender As Object, ByVal e As ConnectionsChangedEventArgs)
            ' Fire this object's ConnectionsChanged event 
            OnConnectionsChanged(Me, e)
        End Sub

        Friend _Server As New Server()
        Friend _Name As String = Nothing
        Friend _Port As Integer = -1

        Public ReadOnly Property ClientCount() As Integer
            Get
                Return _Server.Connections.Count
            End Get
        End Property


        Public ReadOnly Property Connections() As List(Of Tcp)
            Get
                Return _Server.Connections
            End Get
        End Property

        ' This is the text that will appear in the combobox on the main form
        Public Overrides Function ToString() As String
            Return String.Format("{0} [{1}]", _Name, _Port)
        End Function
    End Class


	' Each server MUST implement the following methods/events
	Public Interface IServerBase
		Sub Start()
		Sub [Stop]()
        Event ConnectionsChanged As EventHandler
	End Interface
End Namespace
