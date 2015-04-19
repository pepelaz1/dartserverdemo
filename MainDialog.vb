Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Text
Imports DartServerDemo.Servers
Imports Dart.Sockets
Imports System.Collections
Imports System.Net
Imports System.Net.NetworkInformation
Imports DartServerDemo.Clients


Namespace DartServerDemo
    Partial Public Class MainDialog
        Inherits Form

        Private Declare Function SendARP Lib "iphlpapi.dll" (ByVal DestIP As Integer, ByVal SrcIP As Integer, ByRef pMACAddr As Integer, ByRef PhyAddrLen As Integer) As Integer
        Private Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByRef dst As Byte, ByRef src As Integer, ByVal bcount As Integer)

        Public Sub New()
            InitializeComponent()
        End Sub

        Private _rows_hash As Hashtable = New Hashtable()
        Private _connections_hash As Hashtable = New Hashtable()

        Public Event ConnectionsChanged As EventHandler


        Private Function GetMacAddress(ByVal ip As IPAddress) As String
            Try
                Dim destip As Int32 = BitConverter.ToInt32(ip.GetAddressBytes(), 0)
                Dim mac As Integer
                Dim b(6) As Byte
                Dim ret As Integer = SendARP(destip, 0, mac, 6)
                MessageBox.Show(ret.ToString())

                CopyMemory(b(0), mac, 6)
                Return BitConverter.ToString(b, 0, 6)
            Catch ex As Exception
                Return ""
            End Try
        End Function



        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            ClientList2 = New Generic.Dictionary(Of IntPtr, EchoClient) ' All Client collection

            ' Create 4 echo servers and add them to the combobox
            ' The text in the combobox comes from the 'ToString()' override of the server object
            cboServers.Items.Add(New EchoServer("Server 1", 900, Me))
            cboServers.Items.Add(New EchoServer("Server 2", 1, Me))
            'cboServers.Items.Add(New EchoServer("Server 3", 9, Me))
            cboServers.Items.Add(New HttpProxyServer("SSL Only Proxy", 8080, Me))
            cboServers.Items.Add(New TunnelServer("Simple Tunnel", 80, "127.0.0.1", 7, Me))
            'cboServers.Items.Add(New TunnelServer("Simple Tunnel", 80, "10.115.2.182", 1370, Me))
            'cboServers.Items.Add(New TunnelServer("Simple Tunnel", 81, "10.115.2.182", 1370, Me))
            'cboServers.Items.Add(New TunnelServer("Simple Tunnel", 21, "10.115.2.182", 1370, Me))
            'cboServers.Items.Add(New TunnelServer("Simple Tunnel", 5060, "10.115.2.182", 1370, Me))
            cboServers.SelectedIndex = 0
            cmbSearchItems.SelectedIndex = 0

            ' Initialize each server in the combobox
            For Each s As IServerBase In cboServers.Items
                ' Add the ConnectionsChanged handler
                AddHandler s.ConnectionsChanged, AddressOf server_ConnectionsChanged
                ' Start the server
                s.Start()

            Next s
            RaiseEvent ConnectionsChanged(Me, New EventArgs())
        End Sub

        Private Sub UpdateLabels()
            Dim total As Integer
            For Each s As IServerBase In cboServers.Items
                Dim type = s.GetType()
                Select Case s.GetType()
                    Case GetType(TunnelServer)
                        Dim tunnel As TunnelServer = TryCast(s, TunnelServer)
                        If tunnel._Port = 80 Then
                            lblTunnel80.Text = tunnel.ClientCount.ToString()
                        End If
                    Case GetType(EchoServer)
                        Dim echo As EchoServer = TryCast(s, EchoServer)
                        If echo._Port = 1 Then
                            lblEcho1.Text = echo.ClientCount.ToString()
                        Else
                            lblEchoN.Text = echo.ClientCount.ToString()
                        End If
                    Case GetType(HttpProxyServer)
                        Dim http As HttpProxyServer = TryCast(s, HttpProxyServer)
                        If http._Port = 8080 Then
                            lblHttp.Text = http.ClientCount.ToString()
                        End If
                End Select

                Dim base As ServerBase = TryCast(s, ServerBase)
                If Not base Is Nothing Then
                    total += base.ClientCount
                End If
            Next
            lblTotal.Text = total.ToString()
            AllClientsTotal = total.ToString()
        End Sub

        Delegate Sub UpdateClientsGidCallback(ByVal sender As Object, ByVal e As ConnectionsChangedEventArgs)


        Private Sub UpdateClientsGrid(ByVal sender As Object, ByVal e As ConnectionsChangedEventArgs)
            If Me.dgvClients.InvokeRequired Then
                Dim c As New UpdateClientsGidCallback(AddressOf UpdateClientsGrid)
                Me.Invoke(c, New Object() {sender, e})
            Else
                Dim base As ServerBase = TryCast(sender, ServerBase)
                If Not base Is Nothing Then
                    If Not e.Connection.Socket Is Nothing Then
                        Dim n As Integer = dgvClients.Rows.Add()

                        dgvClients.Rows.Item(n).Cells(0).Value = e.Connection.Socket.Handle.ToString()
                        dgvClients.Rows.Item(n).Cells(1).Value = Guid.NewGuid.ToString()
                        dgvClients.Rows.Item(n).Cells(2).Value = e.Connection.ConnectTime.ToString()
                        dgvClients.Rows.Item(n).Cells(3).Value = base.ToString()
                        dgvClients.Rows.Item(n).Cells(4).Value = e.Connection.RemoteEndPoint.Address.ToString() + ":" + e.Connection.RemoteEndPoint.Port.ToString()
                        dgvClients.Rows.Item(n).Cells(5).Value = GetMacAddress(e.Connection.RemoteEndPoint.Address)

                        '                        e.Connection.Socket.RemoteEndPoint


                        If base.GetType() = GetType(EchoServer) Then
                            e.Connection.Tag = "EchoClient"
                        End If

                        _rows_hash(e.Connection) = dgvClients.Rows.Item(n)
                        _connections_hash(dgvClients.Rows.Item(n)) = e.Connection
                    Else
                        Dim row As DataGridViewRow = DirectCast(_rows_hash(e.Connection), DataGridViewRow)
                        If Not row Is Nothing Then
                            dgvClients.Rows.Remove(row)
                            _connections_hash.Remove(row)
                        End If
                        _rows_hash.Remove(e.Connection)
                    End If
                End If
            End If
        End Sub

        Private Sub server_ConnectionsChanged(ByVal sender As Object, ByVal e As EventArgs)
            ' This event informs you that the number of connections on the server has changed
            ' The sender object is the server
            ' If the server is selected, update labels
            UpdateLabels()
            UpdateClientsGrid(sender, DirectCast(e, ConnectionsChangedEventArgs))
        End Sub

        Private Sub btnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click
            For Each row As DataGridViewRow In dgvClients.SelectedRows
                Dim tcp As Tcp = DirectCast(_connections_hash(row), Tcp)
                If Not tcp Is Nothing Then
                    tcp.Close()
                End If
            Next

        End Sub

        Public Shared Function strToByteArray(str As String) As Byte()
            Dim encoding As New System.Text.UTF8Encoding()
            Return encoding.GetBytes(str)
        End Function

        Private Sub btnSendMsg_Click(sender As Object, e As EventArgs) Handles btnSendMsg.Click
            For Each row As DataGridViewRow In dgvClients.SelectedRows
                Dim tcp As Tcp = DirectCast(_connections_hash(row), Tcp)
                If Not tcp Is Nothing Then
                    Dim msg As String = "Test message"
                    If DirectCast(tcp.Tag, String) = "EchoClient" Then
                        'Dim client As EchoClient = New EchoClient(tcp)
                        'client.SendMsg(msg)
                        Dim client As EchoClient = ClientList2.Item(tcp.Socket.Handle)
                        If Not client Is Nothing Then
                            client.SendMsg(msg)
                        End If

                    Else
                        tcp.Write(strToByteArray(msg))
                    End If
                End If
            Next
        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            'Dim sendBytes As [Byte]() ' Byte array
            ' Dim data = "1||asfa"
            'data = data & Chr(0) ' If the data is sent to flash, append end of string character

            'sendBytes = Encoding.ASCII.GetBytes(data) ' Convert data string to byte array

            'Dim server As ServerBase = TryCast(cboServers.SelectedItem, ServerBase)
            'loop for all servers
            'For Each s As DartServerDemo.EchoServer In cboServers.Items
            '    'loob for all connections for each servers
            '    For Each TcpObJect In s._Server.Connections
            '        ' TcpObJect.Write("*lost*") '& e.Tcp.Tag & vbCrLf)
            '        'send the data ...
            '        Debug.WriteLine(TcpObJect.Socket.Handle)
            '        TcpObJect.Write(sendBytes) '
            '        TcpObJect.Write(sendBytes)
            '    Next
            'Next s

            ' lblClientCount2.Text = CStr(ClientList2.Count())


            '  Dim server As Servers.ServerBase = TryCast(cboServers.SelectedItem, Servers.ServerBase)

            ' lblClientCount3.Text = server.ClientCount.ToString()

            ' sen dmsg to all connected clients ( who stay at lobby and tables )
            'For Each tSok As Generic.KeyValuePair(Of IntPtr, EchoClient) In ClientList2

            '    ' Debug.WriteLine(tSok.Value._Id)

            '    tSok.Value.sendString("1||" & TxtMsgBox.Text, True)


            'Next
        End Sub

        Private Sub CheckBoxMaintenanceStart_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxMaintenanceStart.CheckedChanged

            If CheckBoxMaintenanceStart.Checked Then
                MaintenanceIsOn = True
            Else
                MaintenanceIsOn = False
            End If
        End Sub

        Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            dgvClients.ClearSelection()
            Dim result As Boolean = False
            For Each row As DataGridViewRow In dgvClients.Rows
                If Not row.Cells(cmbSearchItems.SelectedIndex).Value Is Nothing Then
                    Dim s As String = row.Cells(cmbSearchItems.SelectedIndex).Value.ToString()
                    If s.Contains(tbSearchHandle.Text) Then
                        row.Selected = True
                        dgvClients.FirstDisplayedScrollingRowIndex = row.Index
                        result = True
                        Exit For
                    End If
                End If
            Next
            If result = False Then
                MessageBox.Show("Nothing found")
            End If
        End Sub
    End Class
End Namespace
