Option Strict On
Option Explicit On

Imports System.Text
Imports System.ComponentModel
Imports Dart.Sockets
Imports DartServerDemo.Clients

Namespace DartServerDemo.Servers
	' HttpProxyServer inherits from ServerBase and also must implement the IServerBase interface
	Public Class HttpProxyServer
		Inherits ServerBase
		Implements IServerBase

		Public Sub New(ByVal name As String, ByVal port As Integer, ByVal synchronizingObject As ISynchronizeInvoke)
			MyBase.New(name, port, synchronizingObject)

		End Sub

		' This is the text that will appear in the combobox on the main form
		Public Overrides Function ToString() As String
			Return String.Format("{0} (HttpProxy)", MyBase.ToString())
		End Function

		' Start the server
		' When connections arrive, the acceptConnection method will be called
		Public Sub Start() Implements IServerBase.Start
			_Server.Start(AddressOf acceptSource, _Port, Nothing)
		End Sub

		' Stop the server
		Public Sub [Stop]() Implements IServerBase.Stop
        End Sub

        'Public Shadows Event ConnectionsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements IServerBase.ConnectionsChanged
        Public Event ConnectionsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements IServerBase.ConnectionsChanged

        Protected Overrides Function getConnectionsChangedEvent() As EventHandler
            Return ConnectionsChangedEvent
        End Function

		' Fires when a new connection has arrived
		Private Sub acceptSource(ByVal source As TcpBase, ByVal state As Object)
			' wait 30 seconds for initial request
			source.Socket.ReceiveTimeout = 30000
			Try
				' Read up to the first space (result will be the command)
				Dim data As Data = source.ReadToDelimiter(" ")
				Dim destination As New Tcp()
				Dim cmd As String = data.ToString().Trim().ToUpper()
				Dim host As String = ""
				Dim port As Integer = 0
				Dim version As String = ""
				Dim header As String = ""
				'string page = "";
				Dim p() As String = Nothing
				Dim client As HttpProxyClient = Nothing

				Select Case cmd
					Case "CONNECT"
						' Read until next space (result will be host)
						' If there is a colon, the format is host:port, otherwise port is 443
						data = source.ReadToDelimiter(" ")
						p = data.ToString().Trim().Split(":"c)
						port = 443
						host = p(0)
						If p.Length = 2 Then
							port = Convert.ToInt32(p(1))
						End If
						' Read until next crlf (result will be version)
						data = source.ReadToDelimiter(vbCrLf)
						version = data.ToString().Trim().ToUpper()
						' Read until next blank line, each line up to then is part of the header
						data = source.ReadToDelimiter(vbCrLf)
						Do While data.ToString() <> vbCrLf
							header &= data.ToString()
							data = source.ReadToDelimiter(vbCrLf)
						Loop
						' Attempt to establish connection, if successful create a new HttpProxyClient
						destination.Connect(New TcpSession(New IPEndPoint(host, port)))
						client = New HttpProxyClient(source, destination)
						client.Start()

					'case "GET":
					'case "POST":
					'    // Read until next space (result will be the requested page)
					'    data = source.ReadToDelimiter(" ");
					'    p = data.ToString().Trim().Split(new char[] { '/' }, 4);
					'    host = p[2];
					'    page = (p.Length > 3) ? p[3] : "";
					'    // Begin forming the request
					'    string request = String.Format("{0} /{1} ", cmd, page);

					'    port = 80;
					'    p = host.Split(':');
					'    if (p.Length == 2)
					'        port = Convert.ToInt32(p[1]);

					'    // Read until next crlf (result will be version)
					'    data = source.ReadToDelimiter("\r\n");
					'    request += data.ToString();

					'    // Read until next blank line, each line up to then is part of the header
					'    data = source.ReadToDelimiter("\r\n");
					'    while(data.ToString() != "\r\n")
					'    {
					'        request += data.ToString();
					'        data = source.ReadToDelimiter("\r\n");
					'    }
					'    request += "\r\n";
					'    client = new HttpProxyClient(source, host, port, request);
					'    client.Start();
					'    break;

					Case Else
						' Invalid command received. Send 405 error, close the connection and return
						source.Write("HTTP/1.0 405 Method Not Allowed" & vbCrLf & "Proxy-agent: Dart Communications Proxy Sample" & vbCrLf & vbCrLf)
						source.Abort()
						Return
				End Select
			Catch ex As Exception
				If source.State = ConnectionState.Connected Then
					source.Write("HTTP/1.0 405 Method Not Allowed" & vbCrLf & "Proxy-agent: Dart Communications Proxy Sample" & vbCrLf & vbCrLf)
					source.Abort()
				End If
				Return
			End Try
		End Sub
	End Class
End Namespace
