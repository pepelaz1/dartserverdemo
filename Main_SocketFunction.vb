Option Strict On
Option Explicit On

Imports DartServerDemo
Imports System.Collections
Imports DartServerDemo.Clients

'Imports Enyim.Caching

Module Main_SocketFunction

#Region " Declarations "

    Public listenPortNumber As Long ' Port number used for listening

    Public soketPolicy As String ' Socket policy

    Public globalSalonName As String ' Global salon name

    Public saltCode As String ' Salt code used for md5 hash

    Public maxConnection As Long     ' maximum allowed clients

    Public serverIP As String

    Public ClientList2 As Generic.Dictionary(Of IntPtr, EchoClient) 'All Clients List

    Public hashLoggedInClients As Generic.Dictionary(Of String, Integer)

    ' Memcached
    'Public mcClient As MemcachedClient

#End Region





End Module
