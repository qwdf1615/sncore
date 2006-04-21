Option Strict Off
Option Explicit Off
Imports System
Imports EnvDTE
Imports EnvDTE80
Imports System.Diagnostics

Public Module RecordingModule
    Private Sub Replace(ByVal s As String, ByVal t As String, ByVal f As String)
        Try
            DTE.ExecuteCommand("Edit.FindinFiles")
            DTE.Windows.Item("{CF2DDC32-8CAD-11D2-9302-005345000000}").Activate() 'Find and Replace
            DTE.ExecuteCommand("Edit.SwitchtoReplaceInFiles")
            DTE.Find.FilesOfType = f
            DTE.Find.Action = vsFindAction.vsFindActionReplaceAll
            DTE.Windows.Item(Constants.vsWindowKindSolutionExplorer).Activate()
            DTE.Find.FindWhat = s
            DTE.Find.ReplaceWith = t
            DTE.Find.Target = vsFindTarget.vsFindTargetFiles
            DTE.Find.MatchCase = False
            DTE.Find.MatchWholeWord = False
            DTE.Find.MatchInHiddenText = True
            DTE.Find.PatternSyntax = vsFindPatternSyntax.vsFindPatternSyntaxLiteral
            DTE.Find.SearchPath = "Entire Solution"
            DTE.Find.SearchSubfolders = True
            DTE.Find.KeepModifiedDocumentsOpen = False
            DTE.Find.ResultsLocation = vsFindResultsLocation.vsFindResults1
            DTE.Find.Action = vsFindAction.vsFindActionReplaceAll
            If (DTE.Find.Execute() = vsFindResult.vsFindResultNotFound) Then
                Throw New System.Exception("vsFindResultNotFound")
            End If
            DTE.Windows.Item("{CF2DDC32-8CAD-11D2-9302-005345000000}").Close()
        Catch ex As Exception
            DTE.Windows.Item("{CF2DDC32-8CAD-11D2-9302-005345000000}").Close()
        End Try
    End Sub
    Private Sub ReplaceProperty(ByVal name As String, ByVal column As String, ByVal type As String, ByVal targettype As String)
        Replace( _
            "<property name=""" + name + """ column=""" + column + """ type=""" + type + """ />", _
            "<property name=""" + name + """ column=""" + column + """ type=""" + targettype + """ />", _
            "*.hbm.xml")

    End Sub
    Public Sub ReplaceOM()
        Replace("<bag ", "<bag lazy=""true"" ", "*.hbm.xml")
        Replace("Byte[]", "BinaryBlob", "*.hbm.xml")
        ReplaceProperty("Summary", "Summary", "String", "StringClob")
        ReplaceProperty("Xsl", "Xsl", "String", "StringClob")
        ReplaceProperty("Body", "Body", "String", "StringClob")
        ReplaceProperty("Description", "Description", "String", "StringClob")
        ReplaceProperty("Message", "Message", "String", "StringClob")
        ReplaceProperty("Details", "Details", "String", "StringClob")
    End Sub
End Module




