Option Strict On
Option Explicit On

Imports System.Text

Namespace DartServerDemo.Clients
	' Base class for all Clients 
	Public MustInherit Class ClientBase
		Private privateId As Guid
		Public Property Id() As Guid
			Get
				Return privateId
			End Get
			Private Set(ByVal value As Guid)
				privateId = value
			End Set
		End Property

		Public Sub New()
			' Create the client's ID
			Me.Id = Guid.NewGuid()
        End Sub

        Public Shared Function strToByteArray(str As String) As Byte()
            Dim encoding As New System.Text.UTF8Encoding()
            Return encoding.GetBytes(str)
        End Function

    End Class


	' Each client MUST implement the following methods/events
	Public Interface IClientBase
		Sub Start()
        Sub [Stop]()
        Sub SendMsg(ByVal msg As String)
	End Interface
End Namespace
