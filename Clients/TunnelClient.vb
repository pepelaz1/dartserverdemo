Option Strict On
Option Explicit On

Imports System.Text
Imports Dart.Sockets

Namespace DartServerDemo.Clients
	Public Class TunnelClient
		Inherits TunnelBase
		Implements IClientBase

		Public Sub New(ByVal source As TcpBase, ByVal destination As TcpBase)
			MyBase.New(source, destination)

		End Sub

		Public Sub Start() Implements IClientBase.Start
			Dim relayer As New Tcp(Me.Source.Socket)
			' Start relays
			relayer.Start(AddressOf Me.RelayIn, Nothing)
			relayer.Start(AddressOf Me.RelayOut, Nothing)
		End Sub

		Public Sub [Stop]() Implements IClientBase.Stop
            'todo
        End Sub

        Public Sub SendMsg(ByVal msg As String) Implements IClientBase.SendMsg
            'todo
        End Sub
	End Class
End Namespace
