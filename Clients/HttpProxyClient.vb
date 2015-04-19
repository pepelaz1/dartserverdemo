Option Strict On
Option Explicit On

Imports System.Text
Imports Dart.Sockets

Namespace DartServerDemo.Clients
	' HttpProxyClient represents a single connection on an EchoServer
	' HttpProxyClient inherits from ClientBase and must implement the IClientBase interface
	Public Class HttpProxyClient
		Inherits ClientBase
		Implements IClientBase

		Public Sub New(ByVal source As TcpBase, ByVal host As String, ByVal port As Integer, ByVal request As String)
			_Source = source
			AddHandler _Source.Error, AddressOf _Connection_Error
			_Port = port
			_Host = host
			_Request = request
		End Sub

		Public Sub New(ByVal connection As TcpBase, ByVal destination As TcpBase)
			_Source = connection
			AddHandler _Source.Error, AddressOf _Connection_Error

			_Destination = destination
			AddHandler _Destination.Error, AddressOf _Destination_Error
		End Sub

		Private Sub _Destination_Error(ByVal sender As Object, ByVal e As ErrorEventArgs)
			'todo
		End Sub

		Private Sub _Connection_Error(ByVal sender As Object, ByVal e As ErrorEventArgs)
			'todo
		End Sub

		Public Sub Start() Implements IClientBase.Start
			Dim relayer As New Tcp(_Source.Socket)
			'Connected successfully, inform client
			_Source.Write("HTTP/1.0 200 Connection established" & vbCrLf & "Proxy-agent: Dart Communications Proxy Sample" & vbCrLf & vbCrLf)
			' Start relays
			relayer.Start(AddressOf clientToServerRelay, New Object() { _Source, _Destination })
			relayer.Start(AddressOf serverToClientRelay, New Object() { _Source, _Destination })
		End Sub

		Public Sub [Stop]() Implements IClientBase.Stop
			'todo
		End Sub

        Public Sub SendMsg(ByVal msg As String) Implements IClientBase.SendMsg
            'todo
        End Sub

		Private _Source As TcpBase = Nothing
		Private _Destination As TcpBase = Nothing
		Private _Request As String = Nothing
		Private _Host As String = Nothing
		Private _Port As Integer = 80

		''' <summary>
		''' Relays data from the client to the server. Shared by all proxies
		''' </summary>
		''' <param name="state">object[] {connection to the remote server (tcpServer), connection to client (tcpClient)}</param>
		Protected Sub clientToServerRelay(ByVal state As Object)
			Dim objArray() As Object = TryCast(state, Object())
			Dim tcpServer As Tcp = TryCast(objArray(0), Tcp)
			Dim tcpClient As Tcp = TryCast(objArray(1), Tcp)
			Dim buffer(1023) As Byte
			Try
				Dim data As Data = tcpClient.Read(buffer)
				Do While data IsNot Nothing
					tcpServer.Write(data.Buffer, data.Offset, data.Count)
					data = tcpClient.Read(buffer)
				Loop
			Catch
			Finally
				'Client terminated connection, but make sure it is closed
                tcpClient.Close()
				'Must terminate connection to server.
                tcpServer.Close()
			End Try
		End Sub

		''' <summary>
		''' Relays data from the server to the client. Shared by all proxies
		''' </summary>
		''' <param name="state">object[] {connection to the remote server (tcpServer), connection to client (tcpClient)}</param>
		Protected Sub serverToClientRelay(ByVal state As Object)
			Dim objArray() As Object = TryCast(state, Object())
			Dim tcpServer As Tcp = TryCast(objArray(0), Tcp)
			Dim tcpClient As Tcp = TryCast(objArray(1), Tcp)
			Dim buffer(1023) As Byte
			Try
				Dim data As Data = tcpServer.Read(buffer)
				Do While data IsNot Nothing
					tcpClient.Write(data.Buffer, data.Offset, data.Count)
					data = tcpServer.Read(buffer)
				Loop
			Catch
			Finally
				'Server terminated connection, but make sure it is closed
                tcpServer.Close()
				'Must terminate connection to client.
                tcpClient.Close()
			End Try
		End Sub
	End Class
End Namespace
