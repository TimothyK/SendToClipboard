
Public Class MappedDriveLetterExpander
    
    ''' <summary>Gets the full UNC path of a file or folder.  Removes mapped drive letter references.</summary>
    ''' <param name="mappedDrivePathname">Original full path name.  May contain a mapped drive letter.</param>
    ''' <returns>Pathname in UNC format, if original value used a mapped drive letter.</returns>
    ''' <remarks>
    ''' If you pass in "Q:\Path" this function will return "\\Server\ShareName\Path".
    ''' If the drive is not a mapped network drive no action is taken.  
    ''' If you pass in "C:\Path" this function returns "C:\Path"
    ''' </remarks>
    Public Shared Function FullNetPath(mappedDrivePathname As IO.FileSystemInfo) As String
        Return FullNetPath(mappedDrivePathname.FullName)
    End Function


#Region "API Declare statements for FullNetPath"

    Private Declare Function WNetGetConnection Lib "mpr.dll" _
    Alias "WNetGetConnectionA" ( _
        ByVal lpszLocalName As String _
        , ByVal lpszRemoteName As String _
        , ByRef cbRemoteName As Int32 _
    ) As Int32

#End Region
    ''' <summary>Gets the full UNC path of a file or folder.  Removes mapped drive letter references.</summary>
    ''' <param name="mappedDrivePathname">Original full path name.  May contain a mapped drive letter.</param>
    ''' <returns>Pathname in UNC format, if original value used a mapped drive letter.</returns>
    ''' <remarks>
    ''' If you pass in "Q:\Path" this function will return "\\Server\ShareName\Path".
    ''' If the drive is not a mapped network drive no action is taken.  
    ''' If you pass in "C:\Path" this function returns "C:\Path"
    ''' </remarks>
    Public Shared Function FullNetPath(ByVal mappedDrivePathname As String) As String

        Dim uncPathname As String

        Try
            uncPathname = StrDup(256, Chr(0))
            If WNetGetConnection(mappedDrivePathname.Substring(0, 2), uncPathname, uncPathname.Length) = 0 Then
                Dim pathWithoutDriveLetter = mappedDrivePathname.Substring(2)
                Return uncPathname.TrimEnd(Chr(0)) & pathWithoutDriveLetter
            End If
        Catch ex As Exception
            Console.Error.WriteLine("Failed to resolve full net path of " & mappedDrivePathname)
            Console.Error.WriteLine(ex.ToString())
        End Try

        Return mappedDrivePathname

    End Function

End Class
