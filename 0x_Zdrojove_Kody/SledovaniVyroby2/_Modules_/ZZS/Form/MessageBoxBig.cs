using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FASK.SledovaniVyroby.Module.ZZS.Forms
{
	/// <summary>
	/// Implementuje MessageBox s velkymi tlacitky
	/// </summary>
	public partial class MessageBoxBig : System.Windows.Forms.Form, System.IDisposable
	{
		private Label Message_l;
		private Button Ano_but;
        private Button Ne_but;
        private Button Ok_but;
		private string WavFile = "";
		//private System.Threading.ThreadStart entryPoint;
        private Button opakovat_but;
        private Button cancel_but;
        //private System.Threading.Thread MyThread;
        private Fask.Graphic.ImageControl iconctrl;
        private MessageBoxButtons buttons;
        private bool playSound = true;

		public MessageBoxBig(string text,string caption, 
			MessageBoxButtons buttons,
			MessageBoxBigIcon icon,
			MessageBoxDefaultButton defaultButton,
            Color textColor,
            bool playSound)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            this.playSound = playSound;

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

			//entryPoint = new System.Threading.ThreadStart(this.PlayWav);
			//MyThread = new System.Threading.Thread(entryPoint);
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
                case MessageBoxButtons.AbortRetryIgnore:
                    this.Ano_but.Visible = true;
                    this.Ne_but.Visible = true;
                    this.cancel_but.Visible = true;

                    // Jine chovani tlacitek pro ARI
                    this.Ano_but.Text = "Zrušit";
                    this.Ne_but.Text = "Opakovat";
                    this.cancel_but.Text = "Ignorovat";
                    this.Ano_but.DialogResult = DialogResult.Abort;
                    this.Ne_but.DialogResult = DialogResult.Retry;
                    this.cancel_but.DialogResult = DialogResult.Ignore;

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

            //switch (icon)
            //{
            //    case MessageBoxBigIcon.Warning:
            //        this.iconctrl.Image = Properties.Resources.imgWarning;
            //        this.WavFile = Main.SoundDir + "chimes.wav";
            //        break;
            //    case MessageBoxBigIcon.Critical:
            //        this.iconctrl.Image = Properties.Resources.imgCritical;
            //        this.WavFile = Main.SoundDir + "chyba.wav";
            //        break;
            //    case MessageBoxBigIcon.Information:
            //        this.iconctrl.Image = Properties.Resources.imgInformation;
            //        this.WavFile = Main.SoundDir + "info.wav";
            //        break;
            //    case MessageBoxBigIcon.Question:
            //        this.iconctrl.Image = Properties.Resources.imgQuestion;
            //        this.WavFile = Main.SoundDir + "dotaz.wav";
            //        break;
            //    case MessageBoxBigIcon.None:
            //    default:
            //        this.iconctrl.Image = Properties.Resources.None;
            //        break;
            //}
            if (this.iconctrl.Image != null)
            {
                Bitmap bmp = this.iconctrl.Image as Bitmap;
                if (bmp != null)
                    this.iconctrl.Transparent = bmp.GetPixel(0, 0);
            }

            //if (this.playSound)
            //    this.MyThread.Start();
		}

        //private void PlayWav()
        //{
        //    // Win32.Sound.PlayWavResource(WavFile);
        //    MySystem.Audio.PlaySound(this.WavFile);
        //}

        /// <summary>
        /// Zobrazi dialog
        /// </summary>
        /// <param name="text">Text zpravy</param>
        /// <param name="caption">Nadpis dialogu</param>
        /// <param name="buttons">Ktera tlacitka se maji zobrazit</param>
        /// <param name="icon">Ktera ikona se ma zobrazit</param>
        /// <param name="defaultButton">Ktere tlacitko ma fokus</param>
        /// <param name="textColor">Barva textu</param>
        /// <param name="playSound">True=Prehraje defaultni zvuk pro dialog dle nastaveneho typu, False=neprehraje zvuk</param>
        /// <returns>Vraci standartni DialogResult</returns>
        public static System.Windows.Forms.DialogResult Show(string text, string caption,
            MessageBoxButtons buttons,
            MessageBoxBigIcon icon,
            MessageBoxDefaultButton defaultButton,
            Color textColor,
            bool playSound)
        {
            using (MessageBoxBig MyMessageBox = new MessageBoxBig(text, caption,
                           buttons, icon, defaultButton, textColor, playSound))
            {
                DialogResult dr = DialogResult.None;
                //bool scannerenabled = false;
                //try
                //{
                //    scannerenabled = Program.mstw.Scanner.Enabled;
                //    if (scannerenabled)
                //        Program.mstw.Scanner.Disable();
                //}
                //catch { }
                //try
                //{
                //    if (Main.splashScreen != null)
                //        Main.splashScreen.Hide();

                    dr = MyMessageBox.ShowDialog();

                //    if (Main.splashScreen != null)
                //        Main.splashScreen.Show();
                //}
                //catch (Exception ex)
                //{
                //    Logging.Log.Write(ex);
                //    //MessageBox.Show(ex.Message);
                //}

                //try
                //{
                //    if (scannerenabled)
                //        Program.mstw.Scanner.Enable();
                //}
                //catch { }

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
        /// <param name="textColor">Barva textu</param>
        /// <returns>Vraci standartni DialogResult</returns>
        public static System.Windows.Forms.DialogResult Show(string text, string caption,
            MessageBoxButtons buttons,
            MessageBoxBigIcon icon,
            MessageBoxDefaultButton defaultButton,
            Color textColor)
        {
            return Show(text, caption, buttons, icon, defaultButton, textColor, true);
        }

        /// <summary>
        /// Zobrazi dialog
        /// </summary>
        /// <param name="text">Text zpravy</param>
        /// <param name="caption">Nadpis dialogu</param>
        /// <param name="buttons">Ktera tlacitka se maji zobrazit</param>
        /// <param name="icon">Ktera ikona se ma zobrazit</param>
        /// <param name="defaultButton">Ktere tlacitko ma fokus</param>
        /// <param name="playSound">True=Prehraje defaultni zvuk pro dialog dle nastaveneho typu, False=neprehraje zvuk</param>
        /// <returns>Vraci standartni DialogResult</returns>
        public static System.Windows.Forms.DialogResult Show(string text, string caption,
            MessageBoxButtons buttons,
            MessageBoxBigIcon icon,
            MessageBoxDefaultButton defaultButton,
            bool playSound)
        {
            return Show(text, caption, buttons, icon, defaultButton, Color.Black, playSound);
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
        /// <param name="playSound">True=Prehraje defaultni zvuk pro dialog dle nastaveneho typu, False=neprehraje zvuk</param>
        /// <returns>Vraci standartni DialogResult</returns>
        public static System.Windows.Forms.DialogResult Show(string text, string caption,
            MessageBoxButtons buttons,
            MessageBoxBigIcon icon,
            bool playSound)
        {
            return Show(text, caption, buttons, icon, MessageBoxDefaultButton.Button1, playSound);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBoxBig));
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
            resources.ApplyResources(this.Message_l, "Message_l");
            this.Message_l.Name = "Message_l";
            // 
            // Ano_but
            // 
            resources.ApplyResources(this.Ano_but, "Ano_but");
            this.Ano_but.Name = "Ano_but";
            // 
            // Ne_but
            // 
            resources.ApplyResources(this.Ne_but, "Ne_but");
            this.Ne_but.Name = "Ne_but";
            // 
            // Ok_but
            // 
            resources.ApplyResources(this.Ok_but, "Ok_but");
            this.Ok_but.Name = "Ok_but";
            // 
            // opakovat_but
            // 
            resources.ApplyResources(this.opakovat_but, "opakovat_but");
            this.opakovat_but.Name = "opakovat_but";
            // 
            // cancel_but
            // 
            resources.ApplyResources(this.cancel_but, "cancel_but");
            this.cancel_but.Name = "cancel_but";
            // 
            // iconctrl
            // 
            resources.ApplyResources(this.iconctrl, "iconctrl");
            this.iconctrl.Image = null;
            this.iconctrl.Name = "iconctrl";
            this.iconctrl.SizeMode = Fask.Graphic.ImageControl.ImageSizeMode.FitImage;
            this.iconctrl.Transparent = System.Drawing.Color.Black;
            // 
            // MessageBoxBig
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.iconctrl);
            this.Controls.Add(this.cancel_but);
            this.Controls.Add(this.opakovat_but);
            this.Controls.Add(this.Ok_but);
            this.Controls.Add(this.Ne_but);
            this.Controls.Add(this.Ano_but);
            this.Controls.Add(this.Message_l);
            this.Name = "MessageBoxBig";
            this.Load += new System.EventHandler(this.MessageBoxBig_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MessageBox_KeyDown);
            this.ResumeLayout(false);

		}
		#endregion

        private void MessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            //klavesa byla uspesne odchycena, zadna dalsi akce nebude
            e.Handled = true;

            if ((e.KeyCode == Keys.Enter))
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
                    case MessageBoxButtons.AbortRetryIgnore:
                        DialogResult = DialogResult.Retry;
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
                    case MessageBoxButtons.AbortRetryIgnore:
                        DialogResult = DialogResult.Abort;
                        break;
                }
            }
            else
            { //klavesa nebyla osetrena na teto urovni
                e.Handled = false;
                return;
            }

        }

        private void MessageBoxBig_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            //Fask.Localization.LocalizationExtensionForm.Localize(this);

            //location 
            //this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            //this.Size = Forms.FormLocation.ScreenResolution;
            //this.Location = Forms.FormLocation.GetFormLocation(this.Size);

            UpdateSize();
        }


        private void UpdateSize()
        {
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    break;
                case MessageBoxButtons.OKCancel:
                    break;
                case MessageBoxButtons.RetryCancel:
                    break;
                case MessageBoxButtons.YesNo:
                    break;
                case MessageBoxButtons.YesNoCancel:
                case MessageBoxButtons.AbortRetryIgnore:
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
