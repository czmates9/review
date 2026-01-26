<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ColorPicker
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PanelPick = New System.Windows.Forms.Panel
        Me.SuspendLayout()
        '
        'PanelPick
        '
        Me.PanelPick.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PanelPick.Location = New System.Drawing.Point(3, 3)
        Me.PanelPick.Name = "PanelPick"
        Me.PanelPick.Size = New System.Drawing.Size(234, 194)
        '
        'ColorPicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 200)
        Me.Controls.Add(Me.PanelPick)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ColorPicker"
        Me.Text = "Pick your color"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelPick As System.Windows.Forms.Panel
End Class
