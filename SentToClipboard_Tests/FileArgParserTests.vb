
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Text.RegularExpressions
Imports SendToClipboard

<TestClass()> Public Class FileArgParserTests

    <TestMethod()> Public Sub FileArgParser_EmptyArray_ReturnsEmptyArray()

        Dim args() As String = New String() {}
        Dim actual = FileArgParser.ParseArgs(args)

        Assert.AreEqual(0, actual.Count)

    End Sub


    <TestMethod()> Public Sub FileArgParser_SingleFile_ReturnsFile()

        Const FILENAME As String = "C:\File.txt"

        Dim args() As String = New String() {FILENAME}
        Dim actual = FileArgParser.ParseArgs(args)

        Assert.AreEqual(1, actual.Count)
        Assert.AreEqual(FILENAME, actual(0).FullName)

    End Sub

    <TestMethod()> Public Sub FileArgParser_QuotedFile_ReturnsFile()

        Const FILENAME As String = "C:\Path With Space File.txt"
        Const QUOTED_FILENAME As String = """" & FILENAME & """"

        Dim args() As String = New String() {QUOTED_FILENAME}
        Dim actual = FileArgParser.ParseArgs(args)

        Assert.AreEqual(1, actual.Count)
        Assert.AreEqual(FILENAME, actual(0).FullName)

    End Sub

    <TestMethod()> Public Sub FileArgParser_Folder_ReturnsDirectoryInfo()

        Dim path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)

        Dim args() As String = New String() {path}
        Dim actual = FileArgParser.ParseArgs(args)

        Assert.AreEqual(1, actual.Count)
        Assert.IsInstanceOfType(actual(0), GetType(IO.DirectoryInfo))

    End Sub

    <TestMethod()> Public Sub FileArgParser_InvalidFile_ReportsBadFileInErrorMessage()

        Const GOOD_FILENAME As String = "C:\File.txt"
        Const BAD_FILENAME As String = "*??*"

        Dim args() As String = New String() {GOOD_FILENAME, BAD_FILENAME}

        Try
            FileArgParser.ParseArgs(args)
        Catch ex As Exception
            StringAssert.Contains(ex.Message, BAD_FILENAME)
            StringAssert.DoesNotMatch(ex.Message, New Regex(Regex.Escape(GOOD_FILENAME)))
        End Try

    End Sub

End Class