Module modGeneral

#Region " Declarations "

    Public MaintenanceIsOn As Boolean = False ' used to send msg for those who login while Maintenance start.
    Public AllClientsTotal As String 'all players total
    Public LastChatText As String ' store last text sent by last user to send when others join latter we add time etc..
#End Region

#Region " Enumerations "

    ' Client Action
    Public Class clsClientAction
        Public Const LoginToLobbyChat As String = "1g" ' login to lobby

        Public Const LobbyChatMSgtoAll As String = "3" ' send msg to all who stay at lobby chat


    End Class

    Public Class clsServerAction
        '    Public Const enDisconnectRemoteClient As String = "rd"
    End Class



    Public Enum enPlayerStatus
        active = 0
        banned = 1
    End Enum

#End Region

#Region " Subs "

    Public Sub HandleAllExceptions(ByVal seEx As System.Exception, Optional ByVal strText As String = "")
        SaveText(Now & " - Error:" & seEx.ToString & " Additional Info:" & strText & "||||", _
                 My.Application.Info.DirectoryPath & "\error_log.txt")
    End Sub

    ' Save text in file
    Public Sub SaveText(ByRef Text As String, ByRef filename As String)
        Dim sTemp As String ' Temporary string to hold the value of the Text parameter
        Dim hFile As Integer ' File handler
        Try

            If FileExists(filename) Then ' Check if the file exists

                hFile = FreeFile() ' Returns an Integer value that represents the next file number available for use by the FileOpen function

                sTemp = Text

                FileOpen(hFile, filename, OpenMode.Append) ' Open file to append text

                PrintLine(hFile, sTemp) ' Write text to file

                FileClose(hFile) ' Close file

            End If

        Catch ex As Exception
            HandleAllExceptions(ex)
        End Try
    End Sub

    ' Check if file exists
    Public Function FileExists(ByRef strPath As String) As Boolean
        Try

            strPath = Trim(strPath)

            If Trim(strPath).Length = 0 Then
                FileExists = False
                Exit Function
            End If

            FileExists = Len(Dir(strPath)) <> 0

        Catch ex As Exception
            FileExists = False
            HandleAllExceptions(ex)
        End Try
    End Function

#End Region

    ' Get timestamp
    Public Function TimeStamp() As String
        Dim StartDate As String ' Start date
        Dim EndDate As String ' End date
        Dim StartTime As String ' Start time
        Dim EndTime As String ' End time
        Dim DateTimeStart As Date ' Start date and time
        Dim DateTimeEnd As Date ' End date and time
        Try

            StartDate = "1/1/1970" ' Set start Date value
            StartTime = "00:00:00" ' Set start Time value
            DateTimeStart = CDate(FormatDateTime(CDate(StartDate & " " & StartTime))) ' Set start DateTime value

            EndDate = CStr(Today) ' Set end Date value to current system date
            EndTime = CStr(TimeOfDay) ' Set end Time value to current system time
            DateTimeEnd = CDate(FormatDateTime(CDate(EndDate & " " & EndTime))) ' Set end DateTime value to current system date and time

            ' Calculate TimeStamp based on start and end DateTime values
            TimeStamp = CStr(DateDiff(Microsoft.VisualBasic.DateInterval.Second, DateTimeStart, DateTimeEnd, FirstDayOfWeek.System, FirstWeekOfYear.System))

        Catch ex As Exception
            TimeStamp = String.Empty
            HandleAllExceptions(ex)
        End Try
    End Function


    ' Unescape characters
    Public Function unescape(ByVal sts As String) As String
        Try

            sts = Replace(sts, "%C4%9F", "ð")
            sts = Replace(sts, "%C4%9E", "Ð")
            sts = Replace(sts, "%C3%BC", "ü")
            sts = Replace(sts, "%C3%9C", "Ü")
            sts = Replace(sts, "%C5%9F", "þ")
            sts = Replace(sts, "%C5%9E", "Þ")
            sts = Replace(sts, "%C4%B0", "Ý")
            sts = Replace(sts, "%C4%B1", "ý")
            sts = Replace(sts, "%C3%B6", "ö")
            sts = Replace(sts, "%C3%96", "Ö")
            sts = Replace(sts, "%C3%A7", "ç")
            sts = Replace(sts, "%C3%87", "Ç")
            sts = Replace(sts, "%2F", "/")
            sts = Replace(sts, "%2E", ".")
            sts = Replace(sts, "%3A", ":")
            sts = Replace(sts, "%2C", ",")
            sts = Replace(sts, "%3B", ";")
            sts = Replace(sts, "%27", "'")
            sts = Replace(sts, "%22", Chr(34))
            sts = Replace(sts, "%20", " ")
            sts = Replace(sts, "%5F", "_")
            sts = Replace(sts, "%2D", "-")

            unescape = sts

        Catch ex As Exception
            unescape = sts
            HandleAllExceptions(ex)
        End Try
    End Function


    ' Escape characters
    Public Function escape(ByVal sts As String) As String
        Try

            sts = Replace(sts, "ð", "%C4%9F")
            sts = Replace(sts, "Ð", "%C4%9E")
            sts = Replace(sts, "ü", "%C3%BC")
            sts = Replace(sts, "Ü", "%C3%9C")
            sts = Replace(sts, "þ", "%C5%9F")
            sts = Replace(sts, "Þ", "%C5%9E")
            sts = Replace(sts, "Ý", "%C4%B0")
            sts = Replace(sts, "ý", "%C4%B1")
            sts = Replace(sts, "ö", "%C3%B6")
            sts = Replace(sts, "Ö", "%C3%96")
            sts = Replace(sts, "ç", "%C3%A7")
            sts = Replace(sts, "Ç", "%C3%87")
            sts = Replace(sts, "/", "%2F")
            sts = Replace(sts, ".", "%2E")
            sts = Replace(sts, ":", "%3A")
            sts = Replace(sts, ",", "%2C")
            sts = Replace(sts, ";", "%3B")
            sts = Replace(sts, "'", "%27")
            sts = Replace(sts, Chr(34), "%22")
            sts = Replace(sts, Chr(10), "%22")
            sts = Replace(sts, Chr(0), "")
            sts = Replace(sts, Chr(13), "%22")
            sts = Replace(sts, " ", "%20")
            '  sts = Replace(sts, "_", "%5F")
            sts = Replace(sts, "-", "%2D")

            escape = sts

        Catch ex As Exception
            escape = sts
            HandleAllExceptions(ex)
        End Try
    End Function


End Module
