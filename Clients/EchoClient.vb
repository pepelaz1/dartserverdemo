Option Strict On
Option Explicit On

Imports System.Text
Imports Dart.Sockets
Imports System.Collections

Namespace DartServerDemo.Clients
    ' EchoClient represents a single connection on an EchoServer
    ' EchoClient inherits from ClientBase and must implement the IClientBase interface
    Public Class EchoClient
        Inherits ClientBase
        Implements IClientBase

#Region " Declarations "

        'Client Type Guest not authiticated can get only table list/view table can't set ..../ Joined authitcated can play can get points buddy list etc...


        Private ClientCurrentTableID As Integer ' Client table ID

        Public ClientBonusTime As Integer ' Bonus timestamp
        Private ClientisViewerOrSeated As Integer   ' Player type: 1=player, 2=viewer
        Private ClientSeatPosAtTable As Integer ' Player seat position at table

        Private ClientLoggedInToLobby As Integer ' Client login to lobby status: 0=not logged in, 1=logged in

        Private ClientInstantMsgCount As Integer ' Instant message counter sent by the client

        '--- timer
        Private ClientLasttMsgTime As Integer ' Timestamp of last instant message sent by the client
        Private ClientLasttChipTime As Integer ' Timestamp of last send chips operation by the client
        Private ClientlastActivity As Long ' Timestamp of client's last activity. Refreshed on client ping or command in runVeri.

        Private ClientUserID As String ' Username

        Private ClientNameAd As String = "" ' Name


        Private mvarPlayerStatus As Integer ' For banning system
        Private mvarBanEndDate As String

        Private mvarJoinDate As String
        Private mvarLastVisitDate As String


        Public ClientPcName As String ' store pc name for client
        Public ClientIP As String ' store IP for client
        Public ClientMacAddress As String ' store Mac Address for client
        'Public TotalPlayersAtServer As String ' store all # of all players

        Private WithEvents socketKillTimer As SelfTimer

#End Region

#Region " Class Events Initialize.... "
        Private _Connection As TcpBase = Nothing


        Public Sub New(ByVal connection As TcpBase)
            _Connection = connection

            '_Connection.SocketOption.KeepAliveTime...
            'Me._Connection.Socket.Blocking '....


            ' No timeout. Connection will wait forever for data to be sent
            _Connection.SocketOption.ReceiveTimeout = 0
            AddHandler _Connection.Error, AddressOf _Connection_Error


            Dim ttim As Long ' Temporary timestamp to hold current time
            ttim = CLng(TimeStamp()) ' Get current timestamp

            ClientLasttMsgTime = CInt(ttim) ' Initialize client's last message time to current timestamp

            ClientLasttChipTime = CInt(ttim) ' Initialize client's last send chip time to current timestamp

            ClientlastActivity = ttim ' Initialize client's last activity time to current timestamp

            'mBonusTime = ttim ' Initialize client's bonus time to current timestamp

            socketKillTimer = New SelfTimer
            socketKillTimer.Interval = 1800000
            socketKillTimer.Enabled = True


            'for dart add to dictionary ...
            Debug.WriteLine("New Client Created at EchoClient _Connection.Socket.Handle  :-" & _Connection.Socket.Handle.ToString)

            ClientList2.Add(_Connection.Socket.Handle, Me) ' client is connected so we ad to all clients list dictionary


            '
            Me.ClientIP = _Connection.RemoteEndPoint.ToString

            ' Catch ex As Exception
            '  HandleAllExceptions(ex)
            ' End Try


        End Sub

#End Region


        Private Sub _Connection_Error(ByVal sender As Object, ByVal e As ErrorEventArgs)
            'todo
        End Sub

#Region " Client-Server Subs || get/process data ..."

        Public Sub StartAsync()
            ' Authenticate as required
            ' Authenticate(tcp)

            'encypt tcp hash and 1st connection pass in MD5 format and send it as welcome msg to client FirstHashPass
            '  MsgBox(_Connection.Socket.Handle.ToString)
            ' MsgBox(GenerateHash(tcp.Socket.Handle.ToString & FirstHashPass))
            '   GenerateHash(tcp.Socket.Handle.ToString & FirstHashPass)

            '  _Connection.Write("welcome your hash is" & _Connection.Socket.Handle.ToString & "||" & "asdfjasldf " & "||" & "aaaaaa " & "||")

            '  _Connection.Socket.Blocking = True



            ' Receive first data
            Dim buf() As Byte = New Byte(1023) {}
            _Connection.ReadAsync(buf, 0, buf.Length, New ReadAsyncCompleted(AddressOf readCompleted), Nothing)
        End Sub


        Private Sub readCompleted(ByVal tcp As TcpBase, ByVal data As Data, ByVal state As Object)
            If data IsNot Nothing Then


                ' here we should check the lengh of the data if laraga than certian amount we close connection
                'if data size okay we process the data
                ' process data
                'ProcessClientData(tcp, data.ToString)
                ProcessClientData(data.ToString)



                'MsgBox(data.ToString)
                ' echo received data
                '  tcp.Write(data.Buffer, data.Offset, data.Count)
                ' Receive next data
                _Connection.ReadAsync(data.Buffer, 0, data.Buffer.Length, New ReadAsyncCompleted(AddressOf readCompleted), Nothing)
            End If
        End Sub


        Public Sub Start() Implements IClientBase.Start
            ' Create a byte array to store received data
            Dim buffer(1023) As Byte

            ' This connection will exist until it is closed by the client
            ' While connected, process  data and wait for more
            Do While _Connection.State = ConnectionState.Connected

                ' Read a buffer of data
                Dim data As Data = _Connection.Read(buffer)
                ' Return (Echo) the same data back to the client 
                '	_Connection.Write(data.ToString())

                If _Connection.State = ConnectionState.Connected Then

                    socketKillTimer.Enabled = False
                    socketKillTimer.Enabled = True

                    ' _Connection.Write(data.ToString())
                    ' process data 
                    ProcessClientData(data.ToString)
                End If

            Loop
        End Sub



        Public Sub ProcessClientData(ByVal Datastr As String)

            ' This connection will exist until it is closed by the client
            Dim params() As String = Nothing ' Client buffer array
            params = Split(Datastr, "||", 30) ' Split client buffer string into an array


            ReDim Preserve params(30) ' Up to 30 params

            '  MsgBox(data.ToString)

            Select Case params(0)

                Case clsClientAction.LoginToLobbyChat ' "1g" login and authoticate if yes list at lobby list

                    ' Also we should check if he stay at certian table and remove from list !!
                    ' Must authinticate 

                    Me.ClientUserID = escape(params(1))
                    Me.ClientNameAd = escape(params(2))
                    Me.ClientLoggedInToLobby = 1 ' means client logged and and stay at lobby

                    'Me.sendString("1||you are at lobby" & Me.ClientUserID.ToString & "", True)
                    Me.SendMsg("1cc||Welcome " & ClientNameAd & "||" & ClientUserID & "||" & "Chat with All Players at Lobby!" & "||" & AllClientsTotal & "||" & escape(LastChatText))


                    'For Each tSok As Generic.KeyValuePair(Of IntPtr, EchoClient) In ClientList2
                    '    tSok.Value.sendString("1c||" & ClientNameAd & "||" & ClientUserID & "||" & "Joined the Game", True)
                    'Next

                    ' instead of adding into dic we asign value in echo client saying that client at lobby

                    'Case clsClientAction.LoginToTableChat ' we remove from lobby list and we list at certian table


                Case clsClientAction.LobbyChatMSgtoAll '  "3"

                    For Each tSok As Generic.KeyValuePair(Of IntPtr, EchoClient) In ClientList2

                        If tSok.Value.ClientLoggedInToLobby = 1 Then
                            tSok.Value.SendMsg("1c||" & ClientNameAd & "||" & ClientUserID & "||" & escape(params(1)))
                            '  _Connection.Write("1c||" + unescape(ClientNameAd) & "||" & ClientUserID & "||" & "aaaaaa " & "||" & Chr(0))
                        End If

                    Next
                    LastChatText = escape(ClientNameAd) & "__" & ClientUserID & "__" & escape(params(1)) ' store last msg sent and last name and last id

                Case Else

                    Exit Sub
            End Select

        End Sub
#End Region



        Public Sub [Stop]() Implements IClientBase.Stop
            'todo
        End Sub


#Region " General Client Operations || send data .. "

        Public Sub SendMsg(ByVal msg As String) Implements IClientBase.SendMsg

            If _Connection.State = ConnectionState.Connected Then
                _Connection.Write(strToByteArray(msg + Chr(0)))
            End If
        End Sub

#End Region

#Region " Timer Events "

        Private Sub socketKillTimer_TimerEvent() Handles socketKillTimer.TimerEvent
            ' Try

            socketKillTimer.Enabled = False
            Me.disconnectClient()

            ' Catch ex As Exception
            'HandleAllExceptions(ex)
            'End Try
        End Sub

#End Region

#Region " Player and Viewer Actions disconnect etc... "

        Public Sub disconnectClient()


            'dell the timer need to check this
            socketKillTimer = Nothing

            'Remove disconnected socket from the dictionary

            ClientList2.Remove(_Connection.Socket.Handle) ' remove client from All connected clients list

            ' no need for this if we use one dic
            ' ClientListLoggedInAtLobby.Remove(_Connection.Socket.Handle) ' remove from lobby list

            ' remove from table list

            'close the connection
            Me._Connection.Close()

            'Me._Connection.Dispose()
            ' need to check this 
            Me.Finalize()

        End Sub

#End Region

    End Class
End Namespace
