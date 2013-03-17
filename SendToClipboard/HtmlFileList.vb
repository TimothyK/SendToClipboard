
''' <summary>
''' Converts a list of files to HTML
''' </summary>
''' <remarks></remarks>
Public Class HtmlFileList

    Private ReadOnly Files As IEnumerable(Of IO.FileSystemInfo)

    Public Sub New(files As IEnumerable(Of IO.FileSystemInfo))
        Me.Files = files
    End Sub
    Public Sub New(file As IO.FileSystemInfo)
        MyClass.New(New IO.FileSystemInfo() {file})
    End Sub
    Public Sub New(filename As String)
        MyClass.New(New IO.FileInfo(filename))
    End Sub
    ''' <summary>
    ''' Unit Test constructor
    ''' </summary>
    ''' <param name="testFunc">Function for resolving UNC path of files.  This is only used for testing</param>
    ''' <remarks></remarks>
    Public Sub New(filename As String, testFunc As MappedDrivePathToUncDelegate)
        MyClass.New(filename)
        MappedDriveLetterToUnc = testFunc
    End Sub

    Private ReadOnly MappedDriveLetterToUnc As MappedDrivePathToUncDelegate = AddressOf MappedDriveLetterExpander.FullNetPath

    Public Delegate Function MappedDrivePathToUncDelegate(mappedDrivePath As String) As String


    Public Function FileListToHtml() As String
        Dim payload As String

        If Files.Count = 1 Then
            payload = FilenameToHtml(Files.Single)
        Else
            payload = "<ul>" & vbNewLine
            payload &= String.Join(vbNewLine, Files.Select(Function(file) "<li>" & FilenameToHtml(file) & "</li>").ToArray())
            payload &= vbNewLine & "</ul>"
        End If
        Return payload

    End Function

    Private Function FilenameToHtml(file As IO.FileSystemInfo) As String

        If TypeOf file Is IO.FileInfo Then
            Return FilenameToHtml(DirectCast(file, IO.FileInfo))
        ElseIf TypeOf file Is IO.DirectoryInfo Then
            Return DirectoryToHtml(DirectCast(file, IO.DirectoryInfo))
        Else
            Throw New NotSupportedException("Unknown FileSystemInfo subclass: " & file.GetType.FullName)
        End If

    End Function

    Private Function DirectoryToHtml(path As IO.DirectoryInfo) As String
        Return Hyperlink(MappedDriveLetterToUnc(path.FullName), path.FullName)
    End Function

    Private Function FilenameToHtml(file As IO.FileInfo) As String

        Dim html As String
        Dim uncPath = New IO.FileInfo(MappedDriveLetterToUnc(file.FullName))

        'Hyperlink to directory
        html = Hyperlink(uncPath.DirectoryName, file.DirectoryName)
        html &= "\"
        'Hyperlink to file
        html &= Hyperlink(uncPath.FullName, file.Name)

        Return html

    End Function

    Private Shared Function Hyperlink(ByVal linkTarget As String, ByVal linkText As String) As String
        Dim html As String

        html = "<A href=""" & HtmlEncode(linkTarget) & """>"
        html &= HtmlEncode(linkText)
        html &= "</A>"

        Return html

    End Function

    Private Shared Function HtmlEncode(value As String) As String
        Return Replace(value,"&","&amp;")
    End Function
End Class
