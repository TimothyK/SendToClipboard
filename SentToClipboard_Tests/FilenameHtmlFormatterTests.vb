
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Text.RegularExpressions
Imports SendToClipboard

<TestClass()> Public Class FilenameHtmlFormatterTests

    <TestMethod> Public Sub FilenameHtmlFormatter_LocalDrive_ReturnsHtml()

        Const FILENAME = "C:\mappedDrivePathname\File.txt"

        Dim formatter = New HtmlFileList(FILENAME)
        Dim actual = formatter.FileListToHtml()

        Const EXPECTED As String = "<A href=""C:\mappedDrivePathname"">C:\mappedDrivePathname</A>\<A href=""C:\mappedDrivePathname\File.txt"">File.txt</A>"
        Assert.AreEqual(EXPECTED, actual)

    End Sub

    <TestMethod> Public Sub FilenameHtmlFormatter_MappedDrive_ReturnsHtml()

        Const FILENAME = "Q:\mappedDrivePathname\File.txt"

        Dim formatter = New HtmlFileList(FILENAME, AddressOf MappedDriveLetterExpanderStub)
        Dim actual = formatter.FileListToHtml()

        Const EXPECTED As String = "<A href=""\\Server\Share\mappedDrivePathname"">Q:\mappedDrivePathname</A>\<A href=""\\Server\Share\mappedDrivePathname\File.txt"">File.txt</A>"
        Assert.AreEqual(EXPECTED, actual)

    End Sub

    <TestMethod> Public Sub FilenameHtmlFormatter_LocalDirectory_ReturnsSingleHtmlHyperlink()

        Dim filename = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)

        Dim formatter = New HtmlFileList(New IO.DirectoryInfo(filename))
        Dim actual = formatter.FileListToHtml()

        Dim expected As String = "<A href=""" & filename & """>" & filename & "</A>"
        Assert.AreEqual(expected, actual)

    End Sub

    <TestMethod()> Public Sub MappedDriveLetterExpanderStub_Qdrive_ReturnsServerShare()

        Const FILENAME = "Q:\mappedDrivePathname\File.txt"
        Dim actual = MappedDriveLetterExpanderStub(FILENAME)

        Assert.AreEqual("\\Server\Share\mappedDrivePathname\File.txt", actual)

    End Sub

    Private Shared Function MappedDriveLetterExpanderStub(filename As String) As String
        Return Regex.Replace(filename, "^[A-Z]\:\\", "\\Server\Share\")
    End Function



End Class