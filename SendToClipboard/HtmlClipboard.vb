
Imports System.Windows.Forms
Imports System.Text

''' <summary>
''' Writes HTML fragment to the clipboard
''' </summary>
''' <remarks></remarks>
Public Class HtmlClipboard

    ''' <summary>
    ''' Copies a HTML fragment to the clipboard.  Overwrites any existing Clipboard contents.
    ''' </summary>
    ''' <param name="html"></param>
    ''' <remarks></remarks>
    Shared Sub AddHtmlToClipboard(html As String)

        Dim dataObject As New DataObject
        AddHtmlToDataObject(html, dataObject)
        Clipboard.SetDataObject(dataObject)

    End Sub

    ''' <summary>
    ''' Adds an HTML fragment to a DataObject.  Use as one step in adding multiple formats to the clipboard.
    ''' </summary>
    ''' <param name="html">HTML fragment</param>
    ''' <param name="dataObject">object to be added to</param>
    ''' <remarks>
    ''' http://blogs.msdn.com/b/jmstall/archive/2007/01/21/sample-code-html-clipboard.aspx
    ''' 
    ''' <example> Listing 1: Add HTML to clipboard
    ''' <code>
    ''' Dim dataObject As System.Windows.Forms.DataObject
    ''' HtmlClipboard.AddHtmlToDataObject("&lt;HTML&gt;Hello, world!&lt;/HTML&gt;", dataObject)
    ''' 'Add other formats if desired
    ''' dataObject.SetData(DataFormats.Text, True, "Hello, world!")
    ''' System.Windows.Forms.Clipboard.SetDataObject(dataObject, True)
    ''' </code>
    ''' </example>
    ''' </remarks>
    Public Shared Sub AddHtmlToDataObject(html As String, dataObject As IDataObject)

        Dim payload As New StringBuilder

        payload.AppendLine("Format:HTML Format")
        payload.AppendLine("Version:1.0")
        payload.AppendLine("StartHtml:<<<<<<<1")
        payload.AppendLine("EndHtml:<<<<<<<2")
        payload.AppendLine("StartFragment:<<<<<<<3")
        payload.AppendLine("EndFragment:<<<<<<<4")
        payload.AppendLine("StartSelection:<<<<<<<3")
        payload.AppendLine("EndSelection:<<<<<<<3")
        Dim startHtml = payload.Length

        payload.AppendLine("<html>")
        payload.AppendLine("<head><title>Clipboard</title></head>")
        payload.AppendLine("<body><!--StartFragment-->")
        Dim startFragment = payload.Length

        payload.AppendLine(html)
        Dim endFragment = payload.Length

        payload.AppendLine("<!--EndFragment--></body>")
        payload.AppendLine("</html>")
        Dim endHtml = payload.Length

        payload.Replace("<<<<<<<1", startHtml.ToString().PadLeft(8, "0"c))
        payload.Replace("<<<<<<<2", endHtml.ToString().PadLeft(8, "0"c))
        payload.Replace("<<<<<<<3", startFragment.ToString().PadLeft(8, "0"c))
        payload.Replace("<<<<<<<4", endFragment.ToString().PadLeft(8, "0"c))

        dataObject.SetData(DataFormats.Html, True, payload)

    End Sub

End Class
