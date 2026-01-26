using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Fask.Vyroba_P.Keyboard
{
    public partial class FormKeyboard : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private static IntPtr handleWindowToSend = IntPtr.Zero;
        public static IntPtr HandleWindowToSend
        {
            get { return handleWindowToSend; }
            set
            {
                handleWindowToSend = value;
                //SetForegroundWindow(handleWindowToSend);
            }
        }

        private static FormKeyboard frmKeyboard = null;

        /// <summary>
        /// dddd
        /// </summary>
        public static void ShowInstance()
        {
            if (frmKeyboard != null && !(frmKeyboard.IsDisposed || frmKeyboard.Disposing))
            {
                frmKeyboard.Close();
            }
            frmKeyboard = new Fask.Vyroba_P.Keyboard.FormKeyboard();
            frmKeyboard.TopLevel = true;
            frmKeyboard.TopMost = true;
            if (frmKeyboard.Visible)
                frmKeyboard.Visible = false;
            //if (owner != null)
            //    frmKeyboard.Show(owner);
            //else
            frmKeyboard.Show();
        }

        public FormKeyboard()
        {
            InitializeComponent();
        }

        private const int WS_EX_NOACTIVATE = 0x08000000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= WS_EX_NOACTIVATE;
                return createParams;
                //return base.CreateParams;
            }
        }

        private const int WM_MOUSEACTIVATE = 0x21;
        private const int MA_NOACTIVATE = 0x3;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_MOUSEACTIVATE)
            {
                m.Result = (System.IntPtr)MA_NOACTIVATE;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void Label1_Click(object sender, System.EventArgs e)
        {
            char X;
            Label l = sender as Label;
            if (l == null)
                return;

            X = l.Text.Trim()[0];
            //IntPtr theHandle;
            //theHandle = FindWindow(null, "Untitled - Notepad");
            //If theHandle <> IntPtr.Zero Then
            //  SetForegroundWindow(theHandle)
            //  SendKeys.Send(X)
            //End If
            //TextBox1.Text = TextBox1.Text & Label1.Text
            //AssignKeys()
            //}

            if (handleWindowToSend != IntPtr.Zero)
            {
                if (SetForegroundWindow(handleWindowToSend))
                {
                    SendKeys.Send(X.ToString());
                }
            }

            textBox1.Text += X.ToString();
        }

        private void FormKeyboard_Shown(object sender, EventArgs e)
        {
            if (handleWindowToSend != IntPtr.Zero)
            {
                SetForegroundWindow(handleWindowToSend);
            }
        }
    }
}
