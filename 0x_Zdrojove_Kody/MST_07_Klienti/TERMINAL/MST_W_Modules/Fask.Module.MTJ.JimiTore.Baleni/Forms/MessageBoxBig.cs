using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Fask.Module.MTJ.JimiTore.Baleni.Forms
{
	/// <summary>
	/// Implementuje MessageBox s velkymi tlacitky
	/// </summary>
	public partial class MessageBoxBig : System.Windows.Forms.Form, System.IDisposable
	{
		private System.Windows.Forms.Label Message_l;
		private System.Windows.Forms.Button Ano_but;
        private System.Windows.Forms.Button Ne_but;
        private System.Windows.Forms.Button Ok_but;
		private string WavFile = "";
		private System.Threading.ThreadStart entryPoint;
        private Button opakovat_but;
        private Button cancel_but;
        private System.Threading.Thread MyThread;
        private Fask.Graphic.ImageControl iconctrl;
        private MessageBoxButtons buttons;

		public MessageBoxBig(string text,string caption, 
			MessageBoxButtons buttons,
			MessageBoxBigIcon icon,
			MessageBoxDefaultButton defaultButton,
            Color textColor)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            this.Message_l.ForeColor = textColor;
            this.KeyPreview = true;
            this.buttons = buttons;
            //this.Size.Height = Forms.FormLocation.ScreenResolution().Height;
            //this.Location = Fask.MST_W.MySystem.FormMidLocation.GetFormLocation(this.Size);

			this.Ano_but.DialogResult = DialogResult.Yes;
			this.Ne_but.DialogResult = DialogResult.No;
			this.Ok_but.DialogResult = DialogResult.OK;
            this.opakovat_but.DialogResult = DialogResult.Retry;
            this.cancel_but.DialogResult = DialogResult.Cancel;

			this.Text = caption;
			this.Message_l.Text = text;

			entryPoint = new System.Threading.ThreadStart(this.PlayWav);
			MyThread = new System.Threading.Thread(entryPoint);
            this.Ano_but.Visible = false;
            this.Ne_but.Visible = false;
            this.Ok_but.Visible = false;
            this.opakovat_but.Visible = false;
            this.cancel_but.Visible = false;
            switch (buttons)
			{
				case MessageBoxButtons.YesNo:	
					this.Ano_but.Visible = true;
					this.Ne_but.Visible = true;
					switch (defaultButton)
					{
						case MessageBoxDefaultButton.Button1:
							this.Ano_but.Focus();
							break;
						case MessageBoxDefaultButton.Button2:
							this.Ne_but.Focus();
							break;
					}
					break;
				case MessageBoxButtons.OK:	
					this.Ok_but.Visible = true;
					switch (defaultButton)
					{
						case MessageBoxDefaultButton.Button1:
							this.Ok_but.Focus();
							break;
					}
					break;
                case MessageBoxButtons.RetryCancel:
                    this.opakovat_but.Visible = true;
                    this.cancel_but.Visible = true;
                    switch (defaultButton)
                    {
                        case MessageBoxDefaultButton.Button1:
                            this.opakovat_but.Focus();
                            break;
                        case MessageBoxDefaultButton.Button2:
                            this.cancel_but.Focus();
                            break;
                    }
                    break;
                case MessageBoxButtons.YesNoCancel:
                    this.Ano_but.Visible = true;
                    this.Ne_but.Visible = true;
                    this.cancel_but.Visible = true;
                    switch (defaultButton)
                    {
                        case MessageBoxDefaultButton.Button1:
                            this.Ano_but.Focus();
                            break;
                        case MessageBoxDefaultButton.Button2:
                            this.Ne_but.Focus();
                            break;
                        case MessageBoxDefaultButton.Button3:
                            this.cancel_but.Focus();
                            break;
                        default:
                            break;
                    }
                    break;
			}

			switch (icon)
			{
                case MessageBoxBigIcon.Warning:
                    //this.iconctrl.Image = Properties.Resources.imgWarning;
                    this.WavFile = Globals.SoundDir + "chimes.wav";
                    break;
                case MessageBoxBigIcon.Critical:
                    //this.iconctrl.Image = Properties.Resources.imgCritical;
                    this.WavFile = Globals.SoundDir + "chyba.wav";
					break;
                case MessageBoxBigIcon.Information:
                    //this.iconctrl.Image = Properties.Resources.imgInformation;
                    this.WavFile = Globals.SoundDir + "info.wav";
					break;
                case MessageBoxBigIcon.Question:
                    //this.iconctrl.Image = Properties.Resources.imgQuestion;
                    this.WavFile = Globals.SoundDir + "dotaz.wav";
					break;
                case MessageBoxBigIcon.None:
                default:
                    //this.iconctrl.Image = Properties.Resources.None;
                    break;
			}
            this.MyThread.Start();
		}

		private void PlayWav()
		{
			// Win32.Sound.PlayWavResource(WavFile);
            MySystem.Audio.PlaySound(this.WavFile);
		}

        /// <summary>
        /// Zobrazi dialog
        /// </summary>
        /// <param name="text">Text zpravy</param>
        /// <param name="caption">Nadpis dialogu</param>
        /// <param name="buttons">Ktera tlacitka se maji zobrazit</param>
        /// <param name="icon">Ktera ikona se ma zobrazit</param>
        /// <param name="defaultButton">Ktere tlacitko ma fokus</param>
        /// <param name="textColor">Barva textu</param>
        /// <returns>Vraci standartni DialogResult</returns>
        public static System.Windows.Forms.DialogResult Show(string text, string caption,
            MessageBoxButtons buttons,
            MessageBoxBigIcon icon,
            MessageBoxDefaultButton defaultButton,
            Color textColor)
        {
            using (MessageBoxBig MyMessageBox = new MessageBoxBig(text, caption,
                           buttons, icon, defaultButton, textColor))
            {
                DialogResult dr = DialogResult.None;
                bool scannerenabled = false;
                try
                {
                    scannerenabled = Globals.Scanner.Enabled;
                    if (scannerenabled)
                        Globals.Scanner.Disable();
                }
                catch { }
                try
                {
                    //if (Main.splashScreen != null)
                    //    Main.splashScreen.Hide();

                    dr = MyMessageBox.ShowDialog();

                    //if (Main.splashScreen != null)
                    //    Main.splashScreen.Show();
                }
                catch { }

                try
                {
                    if (scannerenabled)
                        Globals.Scanner.Enable();
                }
                catch { }

                return dr;
            }
        }

		/// <summary>
		/// Zobrazi dialog
		/// </summary>
		/// <param name="text">Text zpravy</param>
		/// <param name="caption">Nadpis dialogu</param>
		/// <param name="buttons">Ktera tlacitka se maji zobrazit</param>
		/// <param name="icon">Ktera ikona se ma zobrazit</param>
		/// <param name="defaultButton">Ktere tlacitko ma fokus</param>
		/// <returns>Vraci standartni DialogResult</returns>
		public static System.Windows.Forms.DialogResult Show(string text,string caption, 
			MessageBoxButtons buttons,
            MessageBoxBigIcon icon,
			MessageBoxDefaultButton defaultButton)
		{
            return Show(text, caption, buttons, icon, defaultButton, Color.Black);
        }

        /// <summary>
        /// Zobrazi dialog
        /// </summary>
        /// <param name="text">Text zpravy</param>
        /// <param name="caption">Nadpis dialogu</param>
        /// <param name="buttons">Ktera tlacitka se maji zobrazit</param>
        /// <param name="icon">Ktera ikona se ma zobrazit</param>
        /// <returns>Vraci standartni DialogResult</returns>
        public static System.Windows.Forms.DialogResult Show(string text, string caption,
            MessageBoxButtons buttons,
            MessageBoxBigIcon icon, 
            Color color)
        {
            return Show(text, caption, buttons, icon, MessageBoxDefaultButton.Button1, color);
        }

		/// <summary>
		/// Zobrazi dialog
		/// </summary>
		/// <param name="text">Text zpravy</param>
		/// <param name="caption">Nadpis dialogu</param>
		/// <param name="buttons">Ktera tlacitka se maji zobrazit</param>
		/// <param name="icon">Ktera ikona se ma zobrazit</param>
		/// <returns>Vraci standartni DialogResult</returns>
		public static System.Windows.Forms.DialogResult Show(string text,string caption, 
			MessageBoxButtons buttons,
            MessageBoxBigIcon icon)
		{
			return Show(text,caption,buttons,icon,MessageBoxDefaultButton.Button1);
		}

		/// <summary>
		/// Zobrazi dialog
		/// </summary>
		/// <param name="text">Text zpravy</param>
		/// <param name="caption">Nadpis dialogu</param>
		/// <param name="buttons">Ktera tlacitka se maji zobrazit</param>
		/// <returns>Vraci standartni DialogResult</returns>
		public static System.Windows.Forms.DialogResult Show(string text,string caption, 
			MessageBoxButtons buttons)
		{
            return Show(text, caption, buttons, MessageBoxBigIcon.None, MessageBoxDefaultButton.Button1);
		}

		/// <summary>
		/// Zobrazi dialog
		/// </summary>
		/// <param name="text">Text zpravy</param>
		/// <param name="caption">Nadpis dialogu</param>
		/// <returns>Vraci standartni DialogResult</returns>
		public static System.Windows.Forms.DialogResult Show(string text,string caption)
		{
            return Show(text, caption, MessageBoxButtons.OK, MessageBoxBigIcon.None, MessageBoxDefaultButton.Button1);
		}	
		
		/// <summary>
		/// Zobrazi dialog
		/// </summary>
		/// <param name="text">Text zpravy</param>
		/// <returns>Vraci standartni DialogResult</returns>
		public static System.Windows.Forms.DialogResult Show(string text)
		{
            return Show(text, string.Empty, MessageBoxButtons.OK, MessageBoxBigIcon.None, MessageBoxDefaultButton.Button1);
		}

        /// <summary>
        /// Zobrazi dialog
        /// </summary>
        /// <param name="text">Text zpravy</param>
        /// <param name="textColor">Barva zpravy</param>
        /// <returns>Vraci standartni DialogResult</returns>
        public static System.Windows.Forms.DialogResult Show(string text, Color textColor)
        {
            return Show(text, string.Empty, MessageBoxButtons.OK, MessageBoxBigIcon.None, MessageBoxDefaultButton.Button1, textColor);
        }
		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.Message_l = new System.Windows.Forms.Label();
            this.Ano_but = new System.Windows.Forms.Button();
            this.Ne_but = new System.Windows.Forms.Button();
            this.Ok_but = new System.Windows.Forms.Button();
            this.opakovat_but = new System.Windows.Forms.Button();
            this.cancel_but = new System.Windows.Forms.Button();
            this.iconctrl = new Fask.Graphic.ImageControl();
            this.SuspendLayout();
            // 
            // Message_l
            // 
            this.Message_l.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Message_l.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.Message_l.Location = new System.Drawing.Point(46, 8);
            this.Message_l.Name = "Message_l";
            this.Message_l.Size = new System.Drawing.Size(253, 177);
            // 
            // Ano_but
            // 
            this.Ano_but.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Ano_but.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.Ano_but.Location = new System.Drawing.Point(42, 188);
            this.Ano_but.Name = "Ano_but";
            this.Ano_but.Size = new System.Drawing.Size(104, 62);
            this.Ano_but.TabIndex = 4;
            this.Ano_but.Text = "Ano";
            // 
            // Ne_but
            // 
            this.Ne_but.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Ne_but.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.Ne_but.Location = new System.Drawing.Point(162, 188);
            this.Ne_but.Name = "Ne_but";
            this.Ne_but.Size = new System.Drawing.Size(104, 62);
            this.Ne_but.TabIndex = 3;
            this.Ne_but.Text = "Ne";
            // 
            // Ok_but
            // 
            this.Ok_but.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Ok_but.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.Ok_but.Location = new System.Drawing.Point(101, 188);
            this.Ok_but.Name = "Ok_but";
            this.Ok_but.Size = new System.Drawing.Size(104, 62);
            this.Ok_but.TabIndex = 2;
            this.Ok_but.Text = "OK";
            // 
            // opakovat_but
            // 
            this.opakovat_but.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.opakovat_but.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.opakovat_but.Location = new System.Drawing.Point(42, 188);
            this.opakovat_but.Name = "opakovat_but";
            this.opakovat_but.Size = new System.Drawing.Size(104, 62);
            this.opakovat_but.TabIndex = 7;
            this.opakovat_but.Text = "Opakovat";
            // 
            // cancel_but
            // 
            this.cancel_but.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cancel_but.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.cancel_but.Location = new System.Drawing.Point(162, 188);
            this.cancel_but.Name = "cancel_but";
            this.cancel_but.Size = new System.Drawing.Size(104, 62);
            this.cancel_but.TabIndex = 8;
            this.cancel_but.Text = "Storno";
            // 
            // iconctrl
            // 
            this.iconctrl.Image = null;
            this.iconctrl.Location = new System.Drawing.Point(4, 8);
            this.iconctrl.Name = "iconctrl";
            this.iconctrl.Size = new System.Drawing.Size(36, 33);
            this.iconctrl.SizeMode = Fask.Graphic.ImageControl.ImageSizeMode.FitImage;
            this.iconctrl.TabIndex = 14;
            this.iconctrl.Transparent = System.Drawing.Color.Black;
            // 
            // MessageBoxBig
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(306, 253);
            this.ControlBox = false;
            this.Controls.Add(this.iconctrl);
            this.Controls.Add(this.cancel_but);
            this.Controls.Add(this.opakovat_but);
            this.Controls.Add(this.Ok_but);
            this.Controls.Add(this.Ne_but);
            this.Controls.Add(this.Ano_but);
            this.Controls.Add(this.Message_l);
            this.Name = "MessageBoxBig";
            this.Text = "MessageBox";
            this.Load += new System.EventHandler(this.MessageBoxBig_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MessageBox_KeyDown);
            this.ResumeLayout(false);

		}
		#endregion

        private void MessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
                switch (buttons)
                {
                    case MessageBoxButtons.OK:
                        DialogResult = Ok_but.DialogResult;
                        break;
                    case MessageBoxButtons.YesNo:
                        DialogResult = Ano_but.DialogResult;
                        break;
                    case MessageBoxButtons.RetryCancel:
                        DialogResult = opakovat_but.DialogResult;
                        break;
                    case MessageBoxButtons.YesNoCancel:
                        DialogResult = DialogResult.Yes;
                        break;
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                switch (buttons)
                {
                    case MessageBoxButtons.OK:
                        DialogResult = Ok_but.DialogResult;
                        break;
                    case MessageBoxButtons.YesNo:
                        DialogResult = Ne_but.DialogResult;
                        break;
                    case MessageBoxButtons.RetryCancel:
                        DialogResult = cancel_but.DialogResult;
                        break;
                    case MessageBoxButtons.YesNoCancel:
                        DialogResult = DialogResult.No;
                        break;
                }
            }
            else
            { //klavesa nebyla osetrena na teto urovni
                return;
            }

            //klavesa byla uspesne odchycena, zadna dalsi akce nebude
            e.Handled = true;
        }

        private void MessageBoxBig_Load(object sender, EventArgs e)
        {
            //location 
            //this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            //this.Location = GetFormLocation(this.Size);

            UpdateSize();
        }

        private void UpdateSize()
        {
            switch (buttons)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    break;
                case MessageBoxButtons.OK:
                    break;
                case MessageBoxButtons.OKCancel:
                    break;
                case MessageBoxButtons.RetryCancel:
                    break;
                case MessageBoxButtons.YesNo:
                    break;
                case MessageBoxButtons.YesNoCancel:
                    int okraj = 3; //3pixely zleva, zprava, 2 x mezi buttony
                    Size nsize = new Size((int)((float)(this.Width - 4*okraj) / 3), this.Ano_but.Height);
                    this.Ano_but.Location = new Point(okraj + nsize.Width * 0, this.Ano_but.Location.Y); this.Ano_but.Size = nsize;
                    this.Ne_but.Location = new Point(nsize.Width * 1 + 2*okraj, this.Ne_but.Location.Y); this.Ne_but.Size = nsize;
                    this.cancel_but.Location = new Point(nsize.Width * 2 + 3*okraj, this.cancel_but.Location.Y); this.cancel_but.Size = nsize;
                    break;
                default:
                    break;
            }
        }
	}

    // Summary:
    //     Specifies constants defining which information to display.
    public enum MessageBoxBigIcon
    {
        None,
        Critical,
        Question,
        Information,
        Warning
    }

}
