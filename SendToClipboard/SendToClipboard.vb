Imports System.Windows.Forms

Module SendToClipboard

    Sub Main(args() As String)

        If (args.Length = 1) AndAlso Text.RegularExpressions.Regex.IsMatch(args(0).ToLower, "/(\?|help)") Then
            WriteHelp()
        Else
            Try
                SendArgsToClipboard(args)
            Catch ex As Exception
                Console.Error.WriteLine(ex.ToString())
                Environment.Exit(1)
            End Try
        End If

    End Sub

    Private Sub WriteHelp()
        Console.WriteLine("SendToClipboard")
        Console.WriteLine()
        Console.WriteLine("Copies command line arguments to the clipboard.")
        Console.WriteLine("It is designed to be used from the SendTo folder.")
        Console.WriteLine("It will copy the full path of the 'sent' file to the Clipboard.")
    End Sub

    Private Sub SendArgsToClipboard(ByVal args As String())

        Dim files = FileArgParser.ParseArgs(args)

        If files.Count = 0 Then
            Console.WriteLine("The clipboard was not changed")
            Return
        End If

        Clipboard.Clear()
        Dim dataObject = New DataObject

        'Html Format
        Dim htmlFileList = New HtmlFileList(files)
        HtmlClipboard.AddHtmlToDataObject(htmlFileList.FileListToHtml, dataObject)

        'Text Format
        dataObject.SetData(DataFormats.Text, True, FilesAsText(files))

        'Copy to Clipboard
        Clipboard.SetDataObject(dataObject, True)

        Media.SystemSounds.Beep.Play()
        
    End Sub

    Private Function FilesAsText(files As IEnumerable(Of IO.FileSystemInfo)) As String
        Return String.Join(vbNewLine, files.Select(Function(file) MappedDriveLetterExpander.FullNetPath(file)).ToArray())
    End Function


End Module