using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes
{
    /// <summary>
    /// Selected Win AI Function Calls
    /// </summary>

    public class FullScreen
    {
        private Form _Form;
        private FormWindowState _cWindowState;
        private FormBorderStyle _cBorderStyle;
        private Rectangle _cBounds;
        private bool _FullScreen;

        /// <summary>
        /// Full screen constructor.
        /// </summary>
        /// <param name="form">The WinForm to be show or hide as full screen</param>
        public FullScreen(Form form)
        {
            _Form = form;
            _FullScreen = false;
        }

        /// <summary>
        /// Show or Hide your WinForm in full screen mode.
        /// </summary>
        private void ScreenMode()
        {
            // set full screen
            if (!_FullScreen)
            {
                // Get the WinForm properties
                _cBorderStyle = _Form.FormBorderStyle;
                _cBounds = _Form.Bounds;
                _cWindowState = _Form.WindowState;

                // set to false to avoid site effect
                _Form.Visible = false;

                HandleTaskBar.hideTaskBar();

                // set new properties
                _Form.FormBorderStyle = FormBorderStyle.None;
                _Form.WindowState = FormWindowState.Maximized;

                _Form.MaximizeBox = false;
                _Form.MinimizeBox = false;
                _Form.TopMost = true;

                /* Pripadne neco pridat...
                 
                    this.Bounds = Screen.PrimaryScreen.Bounds;
                    this.MaximizeBox = false;
                    this.MinimizeBox = false;
                    this.TopMost = true;
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                */

                _Form.Visible = true;
                _FullScreen = true;
            }
            else  // reset full screen
            {
                // reset the normal WinForm properties
                // always set WinForm.Visible to false to avoid site effect
                _Form.Visible = false;
                _Form.WindowState = _cWindowState;
                _Form.FormBorderStyle = _cBorderStyle;
                _Form.Bounds = _cBounds;

                HandleTaskBar.showTaskBar();

                _Form.Visible = true;

                // Not in full screen mode
                _FullScreen = false;
            }
        }

        /// <summary>
        /// Show or hide full screen mode
        /// </summary>
        public void ShowFullScreen()
        {
            ScreenMode();
        }
        /// <summary>
        /// You can use this to reset the Taskbar in case of error.
        /// I don't want to handle exception in this class.
        /// You can change it if you like!
        /// </summary>
        public void ResetTaskBar()
        {
            HandleTaskBar.showTaskBar();
        }
    }

    internal class HandleTaskBar
    {
        private const int SWP_HIDEWINDOW = 0x0080;
        private const int SWP_SHOWWINDOW = 0x0040;

        public HandleTaskBar()
        {
        }

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll")]
        private static extern int SetWindowPos(int hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        public static void showTaskBar()
        {
            int hWnd = FindWindow("Shell_TrayWnd", "");
            SetWindowPos(hWnd, 0, 0, 0, 0, 0, SWP_SHOWWINDOW);
        }

        public static void hideTaskBar()
        {
            int hWnd = FindWindow("Shell_TrayWnd", "");
            SetWindowPos(hWnd, 0, 0, 0, 0, 0, SWP_HIDEWINDOW);
        }
    }
}
