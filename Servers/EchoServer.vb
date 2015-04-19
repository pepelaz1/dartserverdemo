Option Strict On
Option Explicit On

Imports System.Text
Imports Dart.Sockets
Imports System.ComponentModel
Imports DartServerDemo.Clients

Namespace DartServerDemo.Servers
	' EchoServer inherits from ServerBase and also must implement the IServerBase interface
	Public Class EchoServer
		Inherits ServerBase
		Implements IServerBase

		Public Sub New(ByVal name As String, ByVal port As Integer, ByVal synchronizingObject As ISynchronizeInvoke)
			MyBase.New(name, port, synchronizingObject)

		End Sub

		' This is the text that will appear in the combobox on the main form
		Public Overrides Function ToString() As String
			Return String.Format("{0} (Echo)", MyBase.ToString())
		End Function

		' Start the server
		' When connections arrive, the acceptConnection method will be called
		Public Sub Start() Implements IServerBase.Start
            _Server.Start(AddressOf acceptConnection, _Port, Nothing)
        End Sub

		' Stop the server
		Public Sub [Stop]() Implements IServerBase.Stop
			_Server.Close()
        End Sub

        'Public Shadows Event ConnectionsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements IServerBase.ConnectionsChanged
        Public Event ConnectionsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements IServerBase.ConnectionsChanged

        Protected Overrides Function getConnectionsChangedEvent() As EventHandler
            Return ConnectionsChangedEvent
        End Function

		' Fires when a new connection has arrived
		Private Sub acceptConnection(ByVal connection As TcpBase, ByVal state As Object)
            ' Create a new Echo Client with the base conenction
            'Dim client As New ClsSocketConnectClients(connection)

            '  client.sendString(ClientList(connection.Socket.Handle).PhysicalAddress)

            ' client.ClientPcName = client.ClientPcName ' store pc name for client
            ' client.ClientIP = connection.RemoteEndPoint.ToString ' store IP for client
            'client.ClientMacAddress = ClientList(connection.Socket.Handle).PhysicalAddress ' store Mac Address for client

            '   socClient.send(soketPolicy, True)

            'If ClientList.ContainsKey(connection.Socket.Handle) Then
            '    If Not ClientList(connection.Socket.Handle).IsConnected Then
            '        ClientList(socClient.Handle) = Nothing
            '        ClientList.Remove(socClient.Handle)
            '        ClientList.Add(socClient.Handle, socClient)
            '    Else
            '        socClient.Disconnect()
            '    End If
            'Else
            '    ClientList.Add(socClient.Handle, socClient)
            'End If



            ' check if checked box of Maintenance is checked and inform logged in client
            If MaintenanceIsOn = True Then
                ' client.sendString("Maintenance is on ", True)

                Dim MsgData As String = " Maintenance is on you can't login"

                'If toFlash = True Then
                MsgData = MsgData & Chr(0) ' If the data is sent to flash, append end of string character

                ' Encoding.ASCII.GetBytes(MsgData) ' Convert data string to byte array
                connection.Write(Encoding.ASCII.GetBytes(MsgData)) ' Writa bytes to stream

                Exit Sub  ' we can prevent client from continue login and ask him to refresh etc..

            End If


            ' If ConnectionState.Connected Then

            If ClientList2.ContainsKey(connection.Socket.Handle) Then
                ClientList2(connection.Socket.Handle) = Nothing
                ClientList2.Remove(connection.Socket.Handle)

            End If


            Dim client As New EchoClient(connection)

            'client.Start() ' one theread for each client 



            client.StartAsync() ' few thread to manage all clients

            'for dart
            'ClientList2.Add(connection.Socket.Handle, client)
            '  Else

            '  ClientList2.Remove(connection.Socket.Handle)

            '  End If

            'If state. = ConnectionState.Closed Then
            '   
            ' MessageBox.Show(connection.Socket.Handle.ToString)
            '    

            'Else
            '    MessageBox.Show("open we close")
            'End If

        End Sub

    End Class

End Namespace
