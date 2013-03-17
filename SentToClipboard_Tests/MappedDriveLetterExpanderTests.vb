Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Text.RegularExpressions
Imports SendToClipboard

<TestClass()> Public Class MappedDriveLetterExpanderTests

    <TestMethod()> Public Sub FilenameTextFormatter_LocalFile_ReturnsSame()

        Dim filename = New IO.FileInfo("C:\Test.txt")

        Dim actual = MappedDriveLetterExpander.FullNetPath(filename)

        Assert.AreEqual(filename.FullName, actual)

    End Sub

    <TestMethod()> Public Sub FilenameTextFormatter_MappedDrive_ReturnsUncPath()

        Dim networkDrive = IO.DriveInfo.GetDrives.FirstOrDefault( _
            Function(drive) (drive.DriveType = IO.DriveType.Network) _
        )

        If (networkDrive Is Nothing) Then
            Assert.Inconclusive("This test cannot be run because this computer has no mapped drive letters")
        End If

        Const FILENAME As String = "Test.txt"
        Dim pathname = New IO.FileInfo(networkDrive.Name & FILENAME)

        Dim actual = MappedDriveLetterExpander.FullNetPath(pathname)

        Dim uncPattern = New Regex("\\\\\w+\\\w+\\" & Regex.Escape(FILENAME) & "$")
        StringAssert.Matches(actual, uncPattern)

    End Sub

End Class