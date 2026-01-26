Imports System.Drawing

<System.Runtime.InteropServices.ComVisible(False)> Public Class ColorPicker
    Public ReturnColor As Color

    'Public Function getcolor() As Color
    '    Dim colDialog As New ColorPicker
    '    If colDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '        getcolor = colDialog.ReturnColor
    '    Else
    '        getcolor = Nothing
    '    End If
    '    colDialog.Dispose()
    'End Function

    Private Sub PanelPick_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PanelPick.MouseUp
        'get out
        Dim dx, dy As Single
        Dim r, g, b As Integer 'Color components cant be accessed directly
        dx = PanelPick.Width / 18
        dy = PanelPick.Height / 13

        If dy * 12 < e.Y Then
            'greyscale mode
            r = Math.Floor((e.X / dx)) * (255 / 17)
            g = r
            b = r
        Else
            'color mode
            r = (Math.Floor(e.X / dx) Mod 6) * 51
            g = (Math.Floor(e.Y / dy) Mod 6) * 51
            b = (Math.Floor(e.X / dx) / 6) * 51
            If ((e.Y / dy) >= 6) Then b += 51 * 2
        End If

        'Sanitize the components(they can get >255)
        While r > 255
            r -= 255
        End While
        While g > 255
            g -= 255
        End While
        While b > 255
            b -= 255
        End While

        ReturnColor = System.Drawing.Color.FromArgb(r, g, b)
        DialogResult = Windows.Forms.DialogResult.OK
        'Me.Close()
    End Sub

    Private Sub PanelPick_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PanelPick.Paint
        Dim dx, dy As Single
        dx = PanelPick.Width / 18
        dy = PanelPick.Height / 13

        Dim x, y, red, green, blue As Integer
        For x = 0 To 17
            For y = 0 To 11
                'draw
                red = 51 * (x Mod 6)
                blue = (x / 6) * 51
                If y >= 6 Then blue += 51 * 2
                If blue > 255 Then blue = 255
                green = 51 * (y Mod 6)
                Dim brush = New System.Drawing.SolidBrush(Color.FromArgb(red, green, blue))
                e.Graphics.FillRectangle(brush, dx * x, dy * y, dx + 1, dy + 1)
            Next
        Next

        For x = 0 To 17
            'draw
            red = 255 / 17 * x
            blue = 255 / 17 * x
            green = 255 / 17 * x
            Dim brush = New System.Drawing.SolidBrush(Color.FromArgb(red, green, blue))
            e.Graphics.FillRectangle(brush, dx * x, dy * 12, dx + 1, dy + 1)
        Next
    End Sub
End Class