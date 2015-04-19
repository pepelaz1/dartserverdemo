Namespace DartServerDemo
	Partial Public Class MainDialog
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
            Me.cboServers = New System.Windows.Forms.ComboBox()
            Me.panelStats = New System.Windows.Forms.Panel()
            Me.btnSendMsg = New System.Windows.Forms.Button()
            Me.btnDisconnect = New System.Windows.Forms.Button()
            Me.dgvClients = New System.Windows.Forms.DataGridView()
            Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.lblEchoN = New System.Windows.Forms.Label()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.lblEcho1 = New System.Windows.Forms.Label()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.lblHttp = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.lblTunnel80 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.lblTotal = New System.Windows.Forms.Label()
            Me.lblClientCountA = New System.Windows.Forms.Label()
            Me.CheckBoxMaintenanceStart = New System.Windows.Forms.CheckBox()
            Me.TxtMsgBox = New System.Windows.Forms.TextBox()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.tbSearchHandle = New System.Windows.Forms.TextBox()
            Me.btnSearch = New System.Windows.Forms.Button()
            Me.cmbSearchItems = New System.Windows.Forms.ComboBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.panelStats.SuspendLayout()
            CType(Me.dgvClients, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'cboServers
            '
            Me.cboServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboServers.FormattingEnabled = True
            Me.cboServers.Location = New System.Drawing.Point(16, 12)
            Me.cboServers.Name = "cboServers"
            Me.cboServers.Size = New System.Drawing.Size(179, 21)
            Me.cboServers.TabIndex = 0
            '
            'panelStats
            '
            Me.panelStats.Controls.Add(Me.btnSendMsg)
            Me.panelStats.Controls.Add(Me.btnDisconnect)
            Me.panelStats.Controls.Add(Me.dgvClients)
            Me.panelStats.Controls.Add(Me.lblEchoN)
            Me.panelStats.Controls.Add(Me.Label8)
            Me.panelStats.Controls.Add(Me.lblEcho1)
            Me.panelStats.Controls.Add(Me.Label6)
            Me.panelStats.Controls.Add(Me.lblHttp)
            Me.panelStats.Controls.Add(Me.Label4)
            Me.panelStats.Controls.Add(Me.lblTunnel80)
            Me.panelStats.Controls.Add(Me.Label2)
            Me.panelStats.Controls.Add(Me.lblTotal)
            Me.panelStats.Controls.Add(Me.lblClientCountA)
            Me.panelStats.Location = New System.Drawing.Point(12, 35)
            Me.panelStats.Name = "panelStats"
            Me.panelStats.Size = New System.Drawing.Size(868, 371)
            Me.panelStats.TabIndex = 1
            '
            'btnSendMsg
            '
            Me.btnSendMsg.Location = New System.Drawing.Point(780, 219)
            Me.btnSendMsg.Name = "btnSendMsg"
            Me.btnSendMsg.Size = New System.Drawing.Size(88, 41)
            Me.btnSendMsg.TabIndex = 38
            Me.btnSendMsg.Text = "Send Msg "
            Me.btnSendMsg.UseVisualStyleBackColor = True
            '
            'btnDisconnect
            '
            Me.btnDisconnect.Location = New System.Drawing.Point(780, 172)
            Me.btnDisconnect.Name = "btnDisconnect"
            Me.btnDisconnect.Size = New System.Drawing.Size(88, 41)
            Me.btnDisconnect.TabIndex = 39
            Me.btnDisconnect.Text = "Disconnect Selected"
            Me.btnDisconnect.UseVisualStyleBackColor = True
            '
            'dgvClients
            '
            Me.dgvClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvClients.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6})
            Me.dgvClients.Location = New System.Drawing.Point(6, 172)
            Me.dgvClients.Name = "dgvClients"
            Me.dgvClients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvClients.Size = New System.Drawing.Size(765, 196)
            Me.dgvClients.TabIndex = 12
            '
            'Column1
            '
            Me.Column1.HeaderText = "Unique_Socket Handle"
            Me.Column1.Name = "Column1"
            Me.Column1.Width = 150
            '
            'Column2
            '
            Me.Column2.HeaderText = "Unique User ID"
            Me.Column2.Name = "Column2"
            Me.Column2.Width = 110
            '
            'Column3
            '
            Me.Column3.HeaderText = "Connect Time"
            Me.Column3.Name = "Column3"
            '
            'Column4
            '
            Me.Column4.HeaderText = "Server"
            Me.Column4.Name = "Column4"
            '
            'Column5
            '
            Me.Column5.HeaderText = "IP Address"
            Me.Column5.Name = "Column5"
            '
            'Column6
            '
            Me.Column6.HeaderText = "Mac Address (Physical Address)"
            Me.Column6.Name = "Column6"
            Me.Column6.Width = 200
            '
            'lblEchoN
            '
            Me.lblEchoN.AutoSize = True
            Me.lblEchoN.Location = New System.Drawing.Point(216, 119)
            Me.lblEchoN.Name = "lblEchoN"
            Me.lblEchoN.Size = New System.Drawing.Size(13, 13)
            Me.lblEchoN.TabIndex = 11
            Me.lblEchoN.Text = "0"
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Location = New System.Drawing.Point(3, 119)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(203, 13)
            Me.Label8.TabIndex = 10
            Me.Label8.Text = "Clients connected via normal Echo port N"
            '
            'lblEcho1
            '
            Me.lblEcho1.AutoSize = True
            Me.lblEcho1.Location = New System.Drawing.Point(216, 97)
            Me.lblEcho1.Name = "lblEcho1"
            Me.lblEcho1.Size = New System.Drawing.Size(13, 13)
            Me.lblEcho1.TabIndex = 9
            Me.lblEcho1.Text = "0"
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(3, 97)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(201, 13)
            Me.Label6.TabIndex = 8
            Me.Label6.Text = "Clients connected via normal Echo port 1"
            '
            'lblHttp
            '
            Me.lblHttp.AutoSize = True
            Me.lblHttp.Location = New System.Drawing.Point(216, 75)
            Me.lblHttp.Name = "lblHttp"
            Me.lblHttp.Size = New System.Drawing.Size(13, 13)
            Me.lblHttp.TabIndex = 7
            Me.lblHttp.Text = "0"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(3, 75)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(173, 13)
            Me.Label4.TabIndex = 6
            Me.Label4.Text = "Clients connected through http port"
            '
            'lblTunnel80
            '
            Me.lblTunnel80.AutoSize = True
            Me.lblTunnel80.Location = New System.Drawing.Point(216, 53)
            Me.lblTunnel80.Name = "lblTunnel80"
            Me.lblTunnel80.Size = New System.Drawing.Size(13, 13)
            Me.lblTunnel80.TabIndex = 5
            Me.lblTunnel80.Text = "0"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(3, 53)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(194, 13)
            Me.Label2.TabIndex = 4
            Me.Label2.Text = "Clients connected to Port 80 tunnel port"
            '
            'lblTotal
            '
            Me.lblTotal.AutoSize = True
            Me.lblTotal.Location = New System.Drawing.Point(216, 12)
            Me.lblTotal.Name = "lblTotal"
            Me.lblTotal.Size = New System.Drawing.Size(13, 13)
            Me.lblTotal.TabIndex = 3
            Me.lblTotal.Text = "0"
            '
            'lblClientCountA
            '
            Me.lblClientCountA.AutoSize = True
            Me.lblClientCountA.Location = New System.Drawing.Point(3, 12)
            Me.lblClientCountA.Name = "lblClientCountA"
            Me.lblClientCountA.Size = New System.Drawing.Size(166, 13)
            Me.lblClientCountA.TabIndex = 2
            Me.lblClientCountA.Text = "All Listening Servers Total Clients:"
            '
            'CheckBoxMaintenanceStart
            '
            Me.CheckBoxMaintenanceStart.AutoSize = True
            Me.CheckBoxMaintenanceStart.Location = New System.Drawing.Point(484, 15)
            Me.CheckBoxMaintenanceStart.Name = "CheckBoxMaintenanceStart"
            Me.CheckBoxMaintenanceStart.Size = New System.Drawing.Size(105, 17)
            Me.CheckBoxMaintenanceStart.TabIndex = 19
            Me.CheckBoxMaintenanceStart.Text = "Maintenance On"
            Me.CheckBoxMaintenanceStart.UseVisualStyleBackColor = True
            '
            'TxtMsgBox
            '
            Me.TxtMsgBox.Location = New System.Drawing.Point(203, 11)
            Me.TxtMsgBox.Name = "TxtMsgBox"
            Me.TxtMsgBox.Size = New System.Drawing.Size(148, 20)
            Me.TxtMsgBox.TabIndex = 18
            '
            'Button1
            '
            Me.Button1.Location = New System.Drawing.Point(357, 11)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(121, 23)
            Me.Button1.TabIndex = 17
            Me.Button1.Text = "Send Msg To All"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'tbSearchHandle
            '
            Me.tbSearchHandle.Location = New System.Drawing.Point(168, 422)
            Me.tbSearchHandle.Name = "tbSearchHandle"
            Me.tbSearchHandle.Size = New System.Drawing.Size(141, 20)
            Me.tbSearchHandle.TabIndex = 40
            '
            'btnSearch
            '
            Me.btnSearch.Location = New System.Drawing.Point(313, 420)
            Me.btnSearch.Name = "btnSearch"
            Me.btnSearch.Size = New System.Drawing.Size(50, 23)
            Me.btnSearch.TabIndex = 40
            Me.btnSearch.Text = "Search"
            Me.btnSearch.UseVisualStyleBackColor = True
            '
            'cmbSearchItems
            '
            Me.cmbSearchItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbSearchItems.FormattingEnabled = True
            Me.cmbSearchItems.Items.AddRange(New Object() {"handle", "user id", "connect time", "server", "ip address", "mac address"})
            Me.cmbSearchItems.Location = New System.Drawing.Point(76, 421)
            Me.cmbSearchItems.Name = "cmbSearchItems"
            Me.cmbSearchItems.Size = New System.Drawing.Size(87, 21)
            Me.cmbSearchItems.TabIndex = 40
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(15, 425)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(55, 13)
            Me.Label1.TabIndex = 40
            Me.Label1.Text = "Search by"
            '
            'MainDialog
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(889, 462)
            Me.Controls.Add(Me.cmbSearchItems)
            Me.Controls.Add(Me.btnSearch)
            Me.Controls.Add(Me.tbSearchHandle)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.CheckBoxMaintenanceStart)
            Me.Controls.Add(Me.TxtMsgBox)
            Me.Controls.Add(Me.Button1)
            Me.Controls.Add(Me.panelStats)
            Me.Controls.Add(Me.cboServers)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "MainDialog"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Dart Server Demo"
            Me.panelStats.ResumeLayout(False)
            Me.panelStats.PerformLayout()
            CType(Me.dgvClients, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

#End Region

        Private WithEvents cboServers As System.Windows.Forms.ComboBox
        Private panelStats As System.Windows.Forms.Panel
        Private lblTotal As System.Windows.Forms.Label
        Private lblClientCountA As System.Windows.Forms.Label
        Private WithEvents lblEchoN As System.Windows.Forms.Label
        Private WithEvents Label8 As System.Windows.Forms.Label
        Private WithEvents lblEcho1 As System.Windows.Forms.Label
        Private WithEvents Label6 As System.Windows.Forms.Label
        Private WithEvents lblHttp As System.Windows.Forms.Label
        Private WithEvents Label4 As System.Windows.Forms.Label
        Private WithEvents lblTunnel80 As System.Windows.Forms.Label
        Private WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents dgvClients As System.Windows.Forms.DataGridView
        Friend WithEvents btnSendMsg As System.Windows.Forms.Button
        Friend WithEvents btnDisconnect As System.Windows.Forms.Button
        Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CheckBoxMaintenanceStart As System.Windows.Forms.CheckBox
        Friend WithEvents TxtMsgBox As System.Windows.Forms.TextBox
        Friend WithEvents Button1 As System.Windows.Forms.Button
        Friend WithEvents tbSearchHandle As System.Windows.Forms.TextBox
        Friend WithEvents btnSearch As System.Windows.Forms.Button
        Private WithEvents cmbSearchItems As System.Windows.Forms.ComboBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
    End Class
End Namespace

