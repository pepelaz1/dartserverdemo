Option Strict On
Option Explicit On

Imports System.Text
Imports Dart.Sockets

Namespace DartServerDemo.Clients
	Public Class TunnelBase
		Inherits ClientBase

		Public Sub New(ByVal source As TcpBase, ByVal destination As TcpBase)
			Me.Source = source
			Me.Destination = destination
		End Sub

		Private privateSource As TcpBase
		Protected Property Source() As TcpBase
			Get
				Return privateSource
			End Get
			Private Set(ByVal value As TcpBase)
				privateSource = value
			End Set
		End Property
		Private privateDestination As TcpBase
		Protected Property Destination() As TcpBase
			Get
				Return privateDestination
			End Get
			Private Set(ByVal value As TcpBase)
				privateDestination = value
			End Set
		End Property

		' Relays data from the destination to the source.
		Protected Sub RelayIn(ByVal state As Object)
			Dim buffer(1023) As Byte
			Try
				Dim data As Data = Me.Destination.Read(buffer)
				Do While data IsNot Nothing
					Me.Source.Write(data.Buffer, data.Offset, data.Count)
					data = Me.Destination.Read(buffer)
				Loop
			Catch
			Finally
				' Destination terminated connection, but make sure it is closed
                Me.Destination.Close()
                '	 Must terminate connection to source.
                Me.Source.Close()
			End Try
		End Sub

		' Relays data from the source to the destination. Shared by all proxies
		Protected Sub RelayOut(ByVal state As Object)
			Dim buffer(1023) As Byte
			Try
				Dim data As Data = Me.Source.Read(buffer)
				Do While data IsNot Nothing
					Me.Destination.Write(data.Buffer, data.Offset, data.Count)
					data = Me.Source.Read(buffer)
				Loop
			Catch
			Finally
				' Source terminated connection, but make sure it is closed
				Me.Source.Close()
				' Must terminate connection to destination.
				Me.Destination.Close()
			End Try
		End Sub
	End Class
End Namespace
