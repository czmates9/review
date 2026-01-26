using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Fask.MST_W.MySystem;

namespace Fask.MST_W.Forms
{
	/// <summary>
	/// Formular vyzyvajici k zalogovani operatora
	/// </summary>
	public class ConfigAppForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox login_tb;
        private Button konec_but;
        private Button ok_but;
        private Panel panel1;
        private Panel panel2;

        /// <summary>
        /// Zvolena konfigurace uzivatelem
        /// </summary>
        public AppConfiguration SelectedConfig
        {
            get { return (AppConfiguration)this.login_tb.SelectedItem; }
        }

        private int _selectedModuleIndex = 0;

        public ConfigAppForm()
		{
			InitializeComponent();
            this.Size = Forms.FormLocation.ScreenResolution;
            this.panel1_Resize(null, null);
		}

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigAppForm));
            this.label1 = new System.Windows.Forms.Label();
            this.login_tb = new System.Windows.Forms.ComboBox();
            this.konec_but = new System.Windows.Forms.Button();
            this.ok_but = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // login_tb
            // 
            resources.ApplyResources(this.login_tb, "login_tb");
            this.login_tb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.login_tb.Name = "login_tb";
            // 
            // konec_but
            // 
            resources.ApplyResources(this.konec_but, "konec_but");
            this.konec_but.Name = "konec_but";
            this.konec_but.TabStop = false;
            this.konec_but.Click += new System.EventHandler(this.konec_but_Click);
            // 
            // ok_but
            // 
            resources.ApplyResources(this.ok_but, "ok_but");
            this.ok_but.Name = "ok_but";
            this.ok_but.TabStop = false;
            this.ok_but.Click += new System.EventHandler(this.ok_but_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.konec_but);
            this.panel1.Controls.Add(this.ok_but);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.login_tb);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // ConfigAppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "ConfigAppForm";
            this.Load += new System.EventHandler(this.ConfigAppForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConfigAppForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

        private void performEnterPress()
        {
            _selectedModuleIndex = login_tb.SelectedIndex;

            UpdateLastIndex();

            this.DialogResult = DialogResult.OK;
        }

		private void ok_but_Click(object sender, System.EventArgs e)
		{
            performEnterPress();
		}

        private void ConfigAppForm_Load(object sender, System.EventArgs e)
		{
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            string configAppXmlPath = Path.Combine(Main.WrkDir, "ConfigApp.xml");
            if (!File.Exists(configAppXmlPath))
            {
                //Main.DataDir
                login_tb.Items.Add(new AppConfiguration(string.Empty, @"Data\"));
                login_tb.SelectedIndex = 0;
                this.DialogResult = DialogResult.OK;
                return;
            }
            FillData();

            // pouze jeden prvek, zvoli se aktualni
            if (login_tb.Items.Count == 1)
            {
                performEnterPress();
            }
		}

        private void FillData()
        {
            string configAppXmlPath = Path.Combine(Main.WrkDir, "ConfigApp.xml");

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(configAppXmlPath);

            //getAppConfiguration(xmldoc, "/Settings/Configuration");

            login_tb.Items.Clear();
            foreach (AppConfiguration cngf in getAppConfiguration(xmldoc, "/Settings/Configuration"))
            {
                login_tb.Items.Add(cngf);
            }

            login_tb.EndUpdate();

            if ((_selectedModuleIndex <= (login_tb.Items.Count - 1)) && (_selectedModuleIndex >= 0))
                login_tb.SelectedIndex = _selectedModuleIndex;
            else login_tb.SelectedIndex = _selectedModuleIndex = 0;
        }

        private void UpdateLastIndex()
        {
            string configAppXmlPath = Path.Combine(Main.WrkDir, "ConfigApp.xml");

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(configAppXmlPath);

            XmlElement configNode = xmldoc.SelectSingleNode("/Settings/Configuration") as XmlElement;

            if (configNode != null)
            {

                XmlNode nextSibling = configNode.NextSibling;
                while (nextSibling != null && nextSibling.Name == "Configuration")
                {
                    nextSibling = nextSibling.NextSibling;
                }

                if (nextSibling.Name == "LastConfiguration")
                {
                   nextSibling.Attributes["index"].Value = _selectedModuleIndex.ToString();
                }
            }

            xmldoc.Save(configAppXmlPath);
        }

        private List<AppConfiguration> getAppConfiguration(XmlDocument xmldoc, string NodeName)
        {
            List<AppConfiguration> list = new List<AppConfiguration>();
            string nodeValue = string.Empty;

            XmlElement configNode = xmldoc.SelectSingleNode(NodeName) as XmlElement;

            if (configNode != null)
            {
                list.Add(new AppConfiguration(configNode.Attributes["name"].Value, configNode.Attributes["dataFolderPath"].Value));

                XmlNode nextSibling = configNode.NextSibling;
                while (nextSibling != null && nextSibling.Name == "Configuration")
                {
                    list.Add(new AppConfiguration(nextSibling.Attributes["name"].Value, nextSibling.Attributes["dataFolderPath"].Value));

                    nextSibling = nextSibling.NextSibling;
                }

                if (nextSibling.Name == "LastConfiguration")
                {
                    _selectedModuleIndex = int.Parse(nextSibling.Attributes["index"].Value);
                }
            }

            return list;
        }


        private void ConfigAppForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                performEnterPress();
            if (e.KeyCode == Keys.Escape)
                this.DialogResult = DialogResult.Cancel;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panel1.Size.Width / 2, panel1.Size.Height);
            ok_but.Size = nsize;
            konec_but.Size = nsize;
        }

        private void konec_but_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }



	}
}
