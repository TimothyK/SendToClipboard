Imports System.Text.RegularExpressions

''' <summary>
''' Converts an array of strings (arguments) to an array of files)
''' </summary>
''' <remarks></remarks>
Public Class FileArgParser

    Public Shared Function ParseArgs(args As IEnumerable(Of String)) As IO.FileSystemInfo()

        Return ( _
            From arg In args _
            Select CreateFileInfoFromArg = CreateFileSystemInfoFromArg(arg) _
        ).ToArray()

    End Function

    Private Shared Function CreateFileSystemInfoFromArg(ByVal arg As String) As IO.FileSystemInfo

        Try
            Dim path As String = RemoveSurroundingQuotes(arg)
            If IO.Directory.Exists(path) Then
                Return New IO.DirectoryInfo(path)
            Else
                Return New IO.FileInfo(path)
            End If
        Catch ex As Exception
            Throw New ApplicationException("The argument " & arg & " is not a valid pathname", ex)
        End Try
    End Function


    Private Shared Function RemoveSurroundingQuotes(value As String) As String

        If Regex.IsMatch(value, "^"".*""$") Then
            Return value.Substring(1, value.Length - 2)
        End If
        Return value

    End Function


End Class