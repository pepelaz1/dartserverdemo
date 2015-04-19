Option Strict On
Option Explicit On

Imports System.Text
Imports System.ComponentModel
Imports Dart.Sockets
Imports DartServerDemo.Clients

Namespace DartServerDemo.Servers
	Public Class TunnelServer
		Inherits ServerBase
		Implements IServerBase

		Public Sub New(ByVal name As String, ByVal sourcePort As Integer, ByVal destinationHost As String, ByVal destinationPort As Integer, ByVal synchronizingObject As ISynchronizeInvoke)
			MyBase.New(name, sourcePort, synchronizingObject)
			_DestinationHost = destinationHost
			_DestinationPort = destinationPort
		End Sub

		' This is the text that will appear in the combobox on the main form
		Public Overrides Function ToString() As String
			Return String.Format("{0} (Tunnel)", MyBase.ToString())
		End Function

		' Start the server
		' When connections arrive, the acceptConnection method will be called
		Public Sub Start() Implements IServerBase.Start
			_Server.Start(AddressOf acceptConnection, _Port, Nothing)
		End Sub

		Public Sub [Stop]() Implements IServerBase.Stop

        End Sub

        ' Fires when a new connection has arrived
        Private Sub acceptConnection(ByVal connection As TcpBase, ByVal state As Object)
            Try
                ' Open a connection to the destination
                '  If connection.State = ConnectionState.Connected Then

                ' Encoding.ASCII.GetBytes(MsgData) ' Convert data string to byte array
                ' connection.Write(Encoding.ASCII.GetBytes("<?xml version=" & Chr(34) & "1.0" & Chr(34) & "?><cross-domain-policy><allow-access-from domain=" & Chr(34) & "*" & Chr(34) & "to-ports=" & Chr(34) & "*" & Chr(34) & " /></cross-domain-policy>" & Chr(0))) ' Writa bytes to stream

                ' End If

                Dim tcp As New Tcp()
                tcp.Connect(New TcpSession(New IPEndPoint(_DestinationHost, _DestinationPort)))
                ' Create a new client
                Dim client As New TunnelClient(connection, tcp)
                ' Start the client

                client.Start()
            Catch
            End Try
        End Sub

        'Public Shadows Event ConnectionsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements IServerBase.ConnectionsChanged
        Public Event ConnectionsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements IServerBase.ConnectionsChanged

        Protected Overrides Function getConnectionsChangedEvent() As EventHandler
            Return ConnectionsChangedEvent
        End Function


		Private _DestinationHost As String = Nothing
		Private _DestinationPort As Integer = 0
	End Class
End Namespace
