using System; 
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Fask.MST_W.Forms
{
	/// <summary>
	/// Implementuje MessageBox s velkymi tlacitky
	/// </summary>
	public partial class MessageBoxBigTimeout : System.Windows.Forms.Form, System.IDisposable
	{

        private Timer timer;
        private Timer timerInfo;
        public int timerDefaultInterval = 3000; //3 sekundy
		private System.Windows.Forms.Label Message_l;
		private System.Windows.Forms.Button Ano_but;
        private System.Windows.Forms.Button Ne_but;
		private System.Windows.Forms.Button Ok_but;
		private string WavFile = "";
		private System.Threading.ThreadStart entryPoint;
        private Button opakovat_but;
        private Button ignorovat_but;
        private System.Threading.Thread MyThread;
        private Fask.Graphic.ImageControl iconctrl;
        private Label labelTimeInfo;
        private MessageBoxButtons buttons;
        private bool playsound = true;

        public MessageBoxBigTimeout(string text, string caption,
            MessageBoxButtons buttons,
            MessageBoxBigIcon icon,
            MessageBoxDefaultButton defaultButton,
            Color textColor
            ) : this(text, caption, buttons, icon, defaultButton, textColor, true)
        {
        }

        public MessageBoxBigTimeout(string text, string caption, 
			MessageBoxButtons buttons,
			MessageBoxBigIcon icon,
			MessageBoxDefaultButton defaultButton,
            Color textColor,
            bool playsound
            )
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

            this.Ano_but.Click += new EventHandler(but_Click);
            this.Ne_but.Click +=new EventHandler(but_Click);
            this.Ok_but.Click += new EventHandler(but_Click);
            this.opakovat_but.Click += new EventHandler(but_Click);
            this.ignorovat_but.Click += new EventHandler(but_Click);

            //this.Ano_but.DialogResult = DialogResult.Yes;
            //this.Ne_but.DialogResult = DialogResult.No;
            //this.Ok_but.DialogResult = DialogResult.OK;
            //this.opakovat_but.DialogResult = DialogResult.Retry;
            //this.ignorovat_but.DialogResult = DialogResult.Cancel;
			this.Text = caption;
			this.Message_l.Text = text.Replace(@"\", @"\ ");

			entryPoint = new System.Threading.ThreadStart(this.PlayWav);
			MyThread = new System.Threading.Thread(entryPoint);
            this.Ano_but.Visible = false;
            this.Ne_but.Visible = false;
            this.Ok_but.Visible = false;
            this.opakovat_but.Visible = false;
            this.ignorovat_but.Visible = false;
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
                    this.ignorovat_but.Visible = true;
                    switch (defaultButton)
                    {
                        case MessageBoxDefaultButton.Button1:
                            this.opakovat_but.Focus();
                            break;
                        case MessageBoxDefaultButton.Button2:
                            this.ignorovat_but.Focus();
                            break;
                    }
                    break;
                case MessageBoxButtons.YesNoCancel:
                    this.Ano_but.Visible = true;
                    this.Ne_but.Visible = true;
                    this.ignorovat_but.Visible = true;
                    switch (defaultButton)
                    {
                        case MessageBoxDefaultButton.Button1:
                            this.Ano_but.Focus();
                            break;
                        case MessageBoxDefaultButton.Button2:
                            this.Ne_but.Focus();
                            break;
                        case MessageBoxDefaultButton.Button3:
                            this.ignorovat_but.Focus();
                            break;
                        default:
                            break;
                    }
                    break;
			}

			switch (icon)
			{
                case MessageBoxBigIcon.Warning:
                    this.iconctrl.Image = Properties.Resources.imgWarning;
                    this.WavFile = Main.SoundDir + "chimes.wav";
                    break;
                case MessageBoxBigIcon.Critical:
                    this.iconctrl.Image = Properties.Resources.imgCritical;
                    this.WavFile = Main.SoundDir + "chyba.wav";
					break;
                case MessageBoxBigIcon.Information:
                    this.iconctrl.Image = Properties.Resources.imgInformation;
                    this.WavFile = Main.SoundDir + "info.wav";
					break;
                case MessageBoxBigIcon.Question:
                    this.iconctrl.Image = Properties.Resources.imgQuestion;
                    this.WavFile = Main.SoundDir + "dotaz.wav";
					break;
                case MessageBoxBigIcon.None:
                default:
                    this.iconctrl.Image = Properties.Resources.None;
                    break;
			}
            if (this.iconctrl.Image != null)
            {
                Bitmap bmp = this.iconctrl.Image as Bitmap;
                if (bmp != null)
                    this.iconctrl.Transparent = bmp.GetPixel(0, 0);
            }

            this.playsound = playsound;
            if (this.playsound)
                this.MyThread.Start();
		}

        private void but_Click(object sender, EventArgs e)
        {
            TimerStop();
            if (sender == this.Ano_but)
                DialogResult = DialogResult.Yes;
            else if (sender == this.Ne_but)
                DialogResult = DialogResult.No;
            else if (sender == this.Ok_but)
                DialogResult = DialogResult.OK;
            else if (sender == this.opakovat_but)
                DialogResult = DialogResult.Retry;
            else if (sender == this.ignorovat_but)
                DialogResult = DialogResult.Cancel;
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
            using (MessageBoxBigTimeout MyMessageBox = new MessageBoxBigTimeout(text, caption,
                           buttons, icon, defaultButton, textColor))
            {
                DialogResult dr = DialogResult.None;
                bool scannerenabled = false;
                try
                {
                    scannerenabled = Program.mstw.Scanner.Enabled;
                    if (scannerenabled)
                        Program.mstw.Scanner.Disable();
                }
                catch { }
                try
                {
                    if (Main.splashScreen != null)
                        Main.splashScreen.Hide();

                    dr = MyMessageBox.ShowDialog();

                    if (Main.splashScreen != null)
                        Main.splashScreen.Show();

                }
                catch { }

                try
                {
                    if (scannerenabled)
                        Program.mstw.Scanner.Enable();
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
        public static System.Windows.Forms.DialogResult Show(string text, string caption,
            MessageBoxButtons buttons,
            MessageBoxBigIcon icon,
            Color color,
            decimal TimeInterval)
        {
            using (MessageBoxBigTimeout MyMessageBox = new MessageBoxBigTimeout(text, caption,
               buttons, icon, MessageBoxDefaultButton.Button1, color))
            {
                DialogResult dr = DialogResult.None;
                bool scannerenabled = false;
                MyMessageBox.timerDefaultInterval = (int)TimeInterval * 1000;
                try
                {
                    scannerenabled = Program.mstw.Scanner.Enabled;
                    if (scannerenabled)
                        Program.mstw.Scanner.Disable();
                }
                catch { }
                try
                {
                    dr = MyMessageBox.ShowDialog();
                }
                catch { }

                try
                {
                    if (scannerenabled)
                        Program.mstw.Scanner.Enable();
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
        /// <returns>Vraci standartni DialogResult</returns>
        public static System.Windows.Forms.DialogResult Show(string text, decimal interval)
        {
            return Show(text, string.Empty, MessageBoxButtons.OK, MessageBoxBigIcon.None, interval);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBoxBigTimeout));
            this.Message_l = new System.Windows.Forms.Label();
            this.Ano_but = new System.Windows.Forms.Button();
            this.Ne_but = new System.Windows.Forms.Button();
            this.Ok_but = new System.Windows.Forms.Button();
            this.opakovat_but = new System.Windows.Forms.Button();
            this.ignorovat_but = new System.Windows.Forms.Button();
            this.iconctrl = new Fask.Graphic.ImageControl();
            this.labelTimeInfo = new System.Windows.Forms.Label();
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
            // ignorovat_but
            // 
            resources.ApplyResources(this.ignorovat_but, "ignorovat_but");
            this.ignorovat_but.Name = "ignorovat_but";
            // 
            // iconctrl
            // 
            this.iconctrl.Image = null;
            resources.ApplyResources(this.iconctrl, "iconctrl");
            this.iconctrl.Name = "iconctrl";
            this.iconctrl.SizeMode = Fask.Graphic.ImageControl.ImageSizeMode.FitImage;
            this.iconctrl.Transparent = System.Drawing.Color.Black;
            // 
            // labelTimeInfo
            // 
            resources.ApplyResources(this.labelTimeInfo, "labelTimeInfo");
            this.labelTimeInfo.Name = "labelTimeInfo";
            // 
            // MessageBoxBigTimeout
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.labelTimeInfo);
            this.Controls.Add(this.iconctrl);
            this.Controls.Add(this.ignorovat_but);
            this.Controls.Add(this.opakovat_but);
            this.Controls.Add(this.Ok_but);
            this.Controls.Add(this.Ne_but);
            this.Controls.Add(this.Ano_but);
            this.Controls.Add(this.Message_l);
            this.Name = "MessageBoxBigTimeout";
            this.Load += new System.EventHandler(this.MessageBoxBig_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MessageBox_KeyDown);
            this.ResumeLayout(false);

		}
		#endregion

        private void MessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            //this.Ano_but.DialogResult = DialogResult.Yes;
            //this.Ne_but.DialogResult = DialogResult.No;
            //this.Ok_but.DialogResult = DialogResult.OK;
            //this.opakovat_but.DialogResult = DialogResult.Retry;
            //this.ignorovat_but.DialogResult = DialogResult.Cancel;

            //klavesa byla uspesne odchycena, zadna dalsi akce nebude
            e.Handled = true;

            TimerStop();
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
                switch (buttons)
                {
                    case MessageBoxButtons.OK:
                        DialogResult = DialogResult.OK;
                        break;
                    case MessageBoxButtons.YesNo:
                        DialogResult = DialogResult.Yes;
                        break;
                    case MessageBoxButtons.RetryCancel:
                        DialogResult = DialogResult.Retry;
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
                        DialogResult = DialogResult.OK;
                        break;
                    case MessageBoxButtons.YesNo:
                        DialogResult = DialogResult.No;
                        break;
                    case MessageBoxButtons.RetryCancel:
                        DialogResult = DialogResult.Cancel;
                        break;
                    case MessageBoxButtons.YesNoCancel:
                        DialogResult = DialogResult.No;
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
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            this.Location = Forms.FormLocation.GetFormLocation(this.Size);

            UpdateSize();

            timerInfo = new Timer();
            timerInfo.Interval = 1000;
            timerInfo.Tick += new EventHandler(timer_Tickinfo);
            timerInfo.Enabled = true;

            timer = new Timer();
            timer.Interval = timerDefaultInterval;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;

            UpdateTimeoutInfo();
        }
        private int timerinfocount = 0;
        void timer_Tickinfo(object sender, EventArgs e)
        {
            UpdateTimeoutInfo();
        }

        private void UpdateTimeoutInfo()
        {
            labelTimeInfo.Text = (timer.Interval / timerInfo.Interval - (timerinfocount++)).ToString();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            TimerStop();
            this.BeginInvoke(new KeyEventHandler(MessageBox_KeyDown), new object[] { Ok_but, new KeyEventArgs(Keys.Escape) });
        }

        private void TimerStop()
        {
            try
            {
                if (timerInfo != null)
                {
                    timerInfo.Enabled = false;
                    timerInfo.Dispose();
                    timerInfo = null;
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            try
            {
                if (timer != null)
                {
                    timer.Enabled = false;
                    timer.Dispose();
                    timer = null;
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
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
                    Size nsize = new Size(this.Width / 3, this.Ano_but.Height);
                    this.Ano_but.Location = new Point(nsize.Width * 0, this.Ano_but.Location.Y); this.Ano_but.Size = nsize;
                    this.Ne_but.Location = new Point(nsize.Width * 1, this.Ne_but.Location.Y); this.Ne_but.Size = nsize;
                    this.ignorovat_but.Location = new Point(nsize.Width * 2, this.ignorovat_but.Location.Y); this.ignorovat_but.Size = nsize;
                    break;
                default:
                    break;
            }
        }

        //internal static void Show(string p, string p_2, MessageBoxButtons messageBoxButtons, MessageBoxBigIcon messageBoxBigIcon, int p_5)
        //{
        //    throw new NotImplementedException();
        //}


        public static System.Windows.Forms.DialogResult Show(
            string text, 
            string caption,
            MessageBoxButtons buttons,
            MessageBoxBigIcon icon,
            decimal timerInterval)
        {
            return Show(text, caption, buttons, icon, timerInterval, true);
        }

        /// <summary>
        /// Zobrazi dialog
        /// </summary>
        /// <param name="text">Text zpravy</param>
        /// <param name="caption">Nadpis dialogu</param>
        /// <param name="buttons">Ktera tlacitka se maji zobrazit</param>
        /// <param name="icon">Ktera ikona se ma zobrazit</param>
        /// <param name="defaultButton">Ktere tlacitko ma fokus</param>
        /// <param name="timerInterval"> interval zobrazeni v sekundach</param>
        /// <param name="playsound"></param>
        /// <returns>Vraci standartni DialogResult</returns>
        public static System.Windows.Forms.DialogResult Show(string text, string caption,
            MessageBoxButtons buttons,
            MessageBoxBigIcon icon,
            decimal timerInterval,
            bool playsound)
        {
            using (MessageBoxBigTimeout MyMessageBox = new MessageBoxBigTimeout(text, caption,
                           buttons, icon, MessageBoxDefaultButton.Button1, Color.Black, playsound))
            {
                DialogResult dr = DialogResult.None;
                bool scannerenabled = false;
                MyMessageBox.timerDefaultInterval = (int)timerInterval * 1000;
                try
                {
                    scannerenabled = Program.mstw.Scanner.Enabled;
                    if (scannerenabled)
                        Program.mstw.Scanner.Disable();
                }
                catch { }
                try
                {
                    dr = MyMessageBox.ShowDialog();
                }
                catch { }

                try
                {
                    if (scannerenabled)
                        Program.mstw.Scanner.Enable();
                }
                catch { }

                return dr;
            }
        }
    }
}
