using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Konzola
{
	public class SplashScreen : Form
	{
		private const double OpacityDecrement = .05;
		private const double OpacityIncrement = .01;
		private const int TimerInterval = 30;
		private static Boolean FadeMode;
		private static Boolean FadeInOut;
		private static Image BGImage;
		//private static String Information;
		//private static String Status;
        private static String DateLic;
        private static String Zakaznik;
        private static SplashScreen SplashScreenForm;
		//private static Thread SplashScreenThread;
		private static Color TransparentKey;
		private System.Windows.Forms.Timer SplashTimer;
        private ProgressBar progressBar1;
        private Panel panel_Info;
        private Label L_DateLic;
        private Label label1;
        private PictureBox pictureBox1;
        private Label L_Zakaznik;
        private Label label4;
        private IContainer components;
		private delegate void UpdateLabel();
		private delegate void CloseSplash();

        #region Public Properties & Methods

        //public void SetLocation(Point location)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        this.BeginInvoke((MethodInvoker)delegate { SetLocation(location); });
        //        return;
        //    }

        //    this.Location = location;
        //}

        /// <summary>
        /// These methods can all be called to set configurable parameters for the Splash Screen
        /// </summary>
        //public String SetInfo
        //{
        //	get { return Information; }
        //	set
        //	{
        //		Information = value;
        //		if(ProgramInfoLabel.InvokeRequired)
        //		{
        //			Delegate InfoUpdate = new UpdateLabel(UpdateInfo);
        //			Invoke(InfoUpdate);
        //		}
        //		else
        //		{
        //			UpdateInfo();
        //		}
        //	}
        //}

        //public String SetStatus
        //{
        //	get { return Status; }
        //	set
        //	{
        //		Status = value;
        //		if(StatusLabel.InvokeRequired)
        //		{
        //			Delegate StatusUpdate = new UpdateLabel(UpdateStatus);
        //			Invoke(StatusUpdate);
        //		}
        //		else
        //		{
        //			UpdateStatus();
        //		}
        //	}
        //}

        public String SetDateLic
        {
            get { return DateLic; }
            set
            {
                DateLic = value;
                if (L_DateLic.InvokeRequired)
                {
                    Delegate StatusUpdate = new UpdateLabel(UpdateInfoLicenceDatum);
                    Invoke(StatusUpdate);
                }
                else
                {
                    UpdateInfoLicenceDatum();
                }
            }
        }

        public String SetZakaznik
        {
            get { return Zakaznik; }
            set
            {
                Zakaznik = value;
                if (L_Zakaznik.InvokeRequired)
                {
                    Delegate StatusUpdate = new UpdateLabel(UpdateZakaznik);
                    Invoke(StatusUpdate);
                }
                else
                {
                    UpdateZakaznik();
                }
            }
        }

        public Image SetBackgroundImage
		{
			get { return BGImage; }
			set
			{
				BGImage = value;
				if (value != null)
				{
                    pictureBox1.Image = BGImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    //panelLogo.ClientSize = panelLogo.BackgroundImage.Size;
				}
			}
		}

		public Color SetTransparentKey
		{
			get { return TransparentKey; }
			set
			{
				TransparentKey = value;
				if (value != Color.Empty)
					TransparencyKey = SetTransparentKey;
			}
		}

		public Boolean SetFade
		{
			get { return FadeInOut; }
			set
			{
				FadeInOut = value;
				Opacity = value ? .00 : 1.00;
			}
		}

		public static SplashScreen Current
		{
			get
			{
				if (SplashScreenForm == null)
					SplashScreenForm = new SplashScreen();
				return SplashScreenForm;
			}
		}

        //public void SetStatusLabel(Point StatusLabelLocation, Int32 StatusLabelWidth, Int32 StatusLabelHeight)
        //{
        //	if (StatusLabelLocation != Point.Empty)
        //		StatusLabel.Location = StatusLabelLocation;
        //	if (StatusLabelWidth == 0 && StatusLabelHeight == 0)
        //		StatusLabel.AutoSize = true;
        //	else
        //	{
        //		if (StatusLabelWidth > 0)
        //			StatusLabel.Width = StatusLabelWidth;
        //		if (StatusLabelHeight > 0)
        //			StatusLabel.Height = StatusLabelHeight;
        //	}
        //}

        //public void SetInfoLabel(Point InfoLabelLocation, Int32 InfoLabelWidth, Int32 InfoLabelHeight)
        //{
        //    if (InfoLabelLocation != Point.Empty)
        //        ProgramInfoLabel.Location = InfoLabelLocation;
        //    if (InfoLabelWidth == 0 && InfoLabelHeight == 0)
        //        ProgramInfoLabel.AutoSize = true;
        //    else
        //    {
        //        if (InfoLabelWidth > 0)
        //            ProgramInfoLabel.Width = InfoLabelWidth;
        //        if (InfoLabelHeight > 0)
        //            ProgramInfoLabel.Height = InfoLabelHeight;
        //    }
        //}

        public void ShowSplashScreen()
		{
            ShowForm();
            //SplashScreenThread = new Thread(new ThreadStart(ShowForm));
            //SplashScreenThread.IsBackground = true;
            //SplashScreenThread.Name = "SplashScreenThread";
            //SplashScreenThread.Start();
		}
        public void CloseSplashScreen(TimeSpan timetoclose)
        {
            System.Threading.Timer t = new System.Threading.Timer(new TimerCallback(closeSplashScreenTimerCallback));
            t.Change(timetoclose, new TimeSpan(-1));
        }

        public void closeSplashScreenTimerCallback(object o)
        {
            this.CloseSplashScreen();
        }

		public void CloseSplashScreen()
		{
			if (SplashScreenForm != null)
			{
				if(InvokeRequired)
				{
					Delegate ClosingDelegate = new CloseSplash(HideSplash);
					Invoke(ClosingDelegate);
				}
				else
				{
					HideSplash();
				}
			}
		}
		#endregion

		public SplashScreen()
		{
			InitializeComponent();
		}

		private static void ShowForm()
		{
			Application.Run(SplashScreenForm);
		}

        //private void UpdateStatus()
        //{
        //	StatusLabel.Text = SetStatus;
        //}

        //private void UpdateInfo()
        //{
        //	ProgramInfoLabel.Text = SetInfo;
        //}

        private void UpdateInfoLicenceDatum()
        {
            L_DateLic.Text = SetDateLic;
        }

        private void UpdateZakaznik()
        {
            L_Zakaznik.Text = SetZakaznik;
        }

        private void SplashTimer_Tick(object sender, EventArgs e)
		{
			if(FadeMode) // Form is opening (Increment)
			{
                if (Opacity < 1.00)
                {
                    Opacity += OpacityIncrement;
                    SetProgres(Opacity);
                }
                else
                {
                    SplashTimer.Stop();
                    HideSplash();
                }
			}
			else // Form is closing (Decrement)
			{
				if(Opacity > .00)
					Opacity -= OpacityDecrement;
				else
					Dispose();
			}
			
		}

        private void SetProgres(double Opacity)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { SetProgres(Opacity); });
                return;
            }

            int tmp = (int)(Opacity * 100);
            progressBar1.Value = tmp;
        }

		#region InitComponents

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.SplashTimer = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel_Info = new System.Windows.Forms.Panel();
            this.L_Zakaznik = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.L_DateLic = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel_Info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // SplashTimer
            // 
            this.SplashTimer.Tick += new System.EventHandler(this.SplashTimer_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 377);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(541, 19);
            this.progressBar1.TabIndex = 2;
            // 
            // panel_Info
            // 
            this.panel_Info.Controls.Add(this.L_Zakaznik);
            this.panel_Info.Controls.Add(this.label4);
            this.panel_Info.Controls.Add(this.L_DateLic);
            this.panel_Info.Controls.Add(this.label1);
            this.panel_Info.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Info.Location = new System.Drawing.Point(0, 319);
            this.panel_Info.Name = "panel_Info";
            this.panel_Info.Size = new System.Drawing.Size(541, 58);
            this.panel_Info.TabIndex = 4;
            // 
            // L_Zakaznik
            // 
            this.L_Zakaznik.AutoSize = true;
            this.L_Zakaznik.Location = new System.Drawing.Point(80, 22);
            this.L_Zakaznik.Name = "L_Zakaznik";
            this.L_Zakaznik.Size = new System.Drawing.Size(34, 13);
            this.L_Zakaznik.TabIndex = 3;
            this.L_Zakaznik.Text = "FASK";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Zakaznik:";
            // 
            // L_DateLic
            // 
            this.L_DateLic.AutoSize = true;
            this.L_DateLic.Location = new System.Drawing.Point(80, 9);
            this.L_DateLic.Name = "L_DateLic";
            this.L_DateLic.Size = new System.Drawing.Size(76, 13);
            this.L_DateLic.TabIndex = 1;
            this.L_DateLic.Text = "Platná do XXX";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Licence:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(541, 319);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // SplashScreen
            // 
            this.ClientSize = new System.Drawing.Size(541, 396);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel_Info);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.panel_Info.ResumeLayout(false);
            this.panel_Info.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private void SplashScreen_Load(object sender, EventArgs e)
		{
			if (SetFade)
			{
				FadeMode = true;
				SplashTimer.Interval = TimerInterval;
				SplashTimer.Start();
			}
		}

		private void HideSplash()
		{
			if(SetFade)
			{
				FadeMode = false;
				SplashTimer.Start();
			}
			else
				Dispose();
		}
	}
}