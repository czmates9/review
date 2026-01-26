using Fask.MST_W.Online.BYZNYS.DatabaseOnlineTableAdapters;
namespace Fask.MST_W.Online.BYZNYS
{
    partial class FormVyberPartnera
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageVolby = new System.Windows.Forms.TabPage();
            this.rb_DIC = new System.Windows.Forms.RadioButton();
            this.rb_ICO = new System.Windows.Forms.RadioButton();
            this.rb_Nazev = new System.Windows.Forms.RadioButton();
            this.rb_Klic_Odb = new System.Windows.Forms.RadioButton();
            this.t_Hodnota = new System.Windows.Forms.TextBox();
            this.tabPageList = new System.Windows.Forms.TabPage();
            this.oNLPARTNERBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.databaseOnline = new Fask.MST_W.Online.BYZNYS.DatabaseOnline();
            this.tblStyle_vyberPartnera = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn2 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn3 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn4 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn5 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.oNL_PARTNERTableAdapter = new Fask.MST_W.Online.BYZNYS.DatabaseOnlineTableAdapters.ONL_PARTNERTableAdapter();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.bOK = new Fask.Graphic.GraphicButton();
            this.bStorno = new Fask.Graphic.GraphicButton();
            this.tabControl1.SuspendLayout();
            this.tabPageVolby.SuspendLayout();
            this.tabPageList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.oNLPARTNERBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.databaseOnline)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            this.mainMenu1.MenuItems.Add(this.menuItem3);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Zpět";
            this.menuItem2.Click += new System.EventHandler(this.gb_Zpet_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "Výběr";
            this.menuItem3.Click += new System.EventHandler(this.gb_OK_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageVolby);
            this.tabControl1.Controls.Add(this.tabPageList);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(258, 204);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageVolby
            // 
            this.tabPageVolby.AutoScroll = true;
            this.tabPageVolby.Controls.Add(this.rb_DIC);
            this.tabPageVolby.Controls.Add(this.rb_ICO);
            this.tabPageVolby.Controls.Add(this.rb_Nazev);
            this.tabPageVolby.Controls.Add(this.rb_Klic_Odb);
            this.tabPageVolby.Controls.Add(this.t_Hodnota);
            this.tabPageVolby.Location = new System.Drawing.Point(4, 27);
            this.tabPageVolby.Name = "tabPageVolby";
            this.tabPageVolby.Size = new System.Drawing.Size(250, 173);
            this.tabPageVolby.Text = "Volby";
            this.tabPageVolby.GotFocus += new System.EventHandler(this.rb_GotFocus);
            // 
            // rb_DIC
            // 
            this.rb_DIC.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.rb_DIC.Location = new System.Drawing.Point(12, 82);
            this.rb_DIC.Name = "rb_DIC";
            this.rb_DIC.Size = new System.Drawing.Size(84, 20);
            this.rb_DIC.TabIndex = 4;
            this.rb_DIC.TabStop = false;
            this.rb_DIC.Text = "DIČ";
            this.rb_DIC.GotFocus += new System.EventHandler(this.rb_GotFocus);
            // 
            // rb_ICO
            // 
            this.rb_ICO.Checked = true;
            this.rb_ICO.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.rb_ICO.Location = new System.Drawing.Point(12, 56);
            this.rb_ICO.Name = "rb_ICO";
            this.rb_ICO.Size = new System.Drawing.Size(84, 20);
            this.rb_ICO.TabIndex = 3;
            this.rb_ICO.Text = "IČO";
            this.rb_ICO.GotFocus += new System.EventHandler(this.rb_GotFocus);
            // 
            // rb_Nazev
            // 
            this.rb_Nazev.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.rb_Nazev.Location = new System.Drawing.Point(12, 30);
            this.rb_Nazev.Name = "rb_Nazev";
            this.rb_Nazev.Size = new System.Drawing.Size(84, 20);
            this.rb_Nazev.TabIndex = 2;
            this.rb_Nazev.TabStop = false;
            this.rb_Nazev.Text = "Název";
            this.rb_Nazev.GotFocus += new System.EventHandler(this.rb_GotFocus);
            // 
            // rb_Klic_Odb
            // 
            this.rb_Klic_Odb.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.rb_Klic_Odb.Location = new System.Drawing.Point(12, 4);
            this.rb_Klic_Odb.Name = "rb_Klic_Odb";
            this.rb_Klic_Odb.Size = new System.Drawing.Size(84, 20);
            this.rb_Klic_Odb.TabIndex = 1;
            this.rb_Klic_Odb.TabStop = false;
            this.rb_Klic_Odb.Text = "Klíč";
            this.rb_Klic_Odb.GotFocus += new System.EventHandler(this.rb_GotFocus);
            // 
            // t_Hodnota
            // 
            this.t_Hodnota.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.t_Hodnota.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.t_Hodnota.Location = new System.Drawing.Point(12, 136);
            this.t_Hodnota.Name = "t_Hodnota";
            this.t_Hodnota.Size = new System.Drawing.Size(226, 25);
            this.t_Hodnota.TabIndex = 0;
            // 
            // tabPageList
            // 
            this.tabPageList.Controls.Add(this.dataGrid1);
            this.tabPageList.Location = new System.Drawing.Point(4, 27);
            this.tabPageList.Name = "tabPageList";
            this.tabPageList.Size = new System.Drawing.Size(250, 173);
            this.tabPageList.Text = "Seznam";
            // 
            // oNLPARTNERBindingSource
            // 
            this.oNLPARTNERBindingSource.AllowNew = false;
            this.oNLPARTNERBindingSource.DataMember = "ONL_PARTNER";
            this.oNLPARTNERBindingSource.DataSource = this.databaseOnline;
            this.oNLPARTNERBindingSource.Sort = "";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.DataSource = this.oNLPARTNERBindingSource;
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dataGrid1.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dataGrid1.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dataGrid1.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.MultiSelect = false;
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.NumberFormat = "N";
            this.dataGrid1.PreferredRowHeight = 24;
            this.dataGrid1.RowHeightDefault = 23;
            this.dataGrid1.Size = new System.Drawing.Size(250, 173);
            this.dataGrid1.Sort = "";
            this.dataGrid1.SortByHeaderDoubleClick = true;
            this.dataGrid1.TabIndex = 0;
            this.dataGrid1.TableStyles.Add(this.tblStyle_vyberPartnera);
            this.dataGrid1.DoubleClick += new System.EventHandler(this.dataGrid1_DoubleClick);
            // 
            // databaseOnline
            // 
            this.databaseOnline.DataSetName = "DatabaseOnline";
            this.databaseOnline.Locale = new System.Globalization.CultureInfo("");
            this.databaseOnline.Prefix = "";
            this.databaseOnline.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblStyle_vyberPartnera
            // 
            this.tblStyle_vyberPartnera.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.tblStyle_vyberPartnera.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.tblStyle_vyberPartnera.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.tblStyle_vyberPartnera.GridColumnStyles.Add(this.dataGridTextBoxColumn4);
            this.tblStyle_vyberPartnera.GridColumnStyles.Add(this.dataGridTextBoxColumn5);
            this.tblStyle_vyberPartnera.MappingName = "ONL_PARTNER";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Název";
            this.dataGridTextBoxColumn1.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn1.MappingName = "nazev";
            this.dataGridTextBoxColumn1.SelectionShow = false;
            this.dataGridTextBoxColumn1.Tag = "";
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "IČO";
            this.dataGridTextBoxColumn2.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn2.MappingName = "ico";
            this.dataGridTextBoxColumn2.SelectionShow = false;
            this.dataGridTextBoxColumn2.Tag = "";
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "DIČ";
            this.dataGridTextBoxColumn3.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn3.MappingName = "dic";
            this.dataGridTextBoxColumn3.SelectionShow = false;
            this.dataGridTextBoxColumn3.Tag = "";
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "Klíč odb.";
            this.dataGridTextBoxColumn4.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn4.MappingName = "klic_odb";
            this.dataGridTextBoxColumn4.SelectionShow = false;
            this.dataGridTextBoxColumn4.Tag = "";
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "Místo dod.";
            this.dataGridTextBoxColumn5.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn5.MappingName = "mistodod";
            this.dataGridTextBoxColumn5.SelectionShow = false;
            this.dataGridTextBoxColumn5.Tag = "";
            // 
            // oNL_PARTNERTableAdapter
            // 
            this.oNL_PARTNERTableAdapter.ClearBeforeFill = true;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.bOK);
            this.panelButtons.Controls.Add(this.bStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 204);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(258, 35);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // bOK
            // 
            this.bOK.BitmapNormal = null;
            this.bOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bOK.FocusMargin = 5;
            this.bOK.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.bOK.Location = new System.Drawing.Point(138, 0);
            this.bOK.Name = "bOK";
            this.bOK.Pressed = false;
            this.bOK.Size = new System.Drawing.Size(120, 35);
            this.bOK.TabIndex = 3;
            this.bOK.Text = "Výběr";
            this.bOK.Transparent = System.Drawing.Color.White;
            this.bOK.Click += new System.EventHandler(this.gb_OK_Click);
            // 
            // bStorno
            // 
            this.bStorno.BitmapNormal = null;
            this.bStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.bStorno.FocusMargin = 5;
            this.bStorno.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.bStorno.Location = new System.Drawing.Point(0, 0);
            this.bStorno.Name = "bStorno";
            this.bStorno.Pressed = false;
            this.bStorno.Size = new System.Drawing.Size(138, 35);
            this.bStorno.TabIndex = 2;
            this.bStorno.Text = "Zpět";
            this.bStorno.Transparent = System.Drawing.Color.White;
            this.bStorno.Click += new System.EventHandler(this.gb_Zpet_Click);
            // 
            // FormVyberPartnera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(258, 239);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelButtons);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "FormVyberPartnera";
            this.Text = "Partner";
            this.Load += new System.EventHandler(this.FormVyberPartnera_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormVyberPartnera_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPageVolby.ResumeLayout(false);
            this.tabPageList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.oNLPARTNERBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.databaseOnline)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageVolby;
        private System.Windows.Forms.TabPage tabPageList;
        private System.Windows.Forms.RadioButton rb_DIC;
        private System.Windows.Forms.RadioButton rb_ICO;
        private System.Windows.Forms.RadioButton rb_Nazev;
        private System.Windows.Forms.RadioButton rb_Klic_Odb;
        private System.Windows.Forms.TextBox t_Hodnota;
        private System.Windows.Forms.BindingSource oNLPARTNERBindingSource;
        private Fask.MST_W.Online.BYZNYS.DatabaseOnline databaseOnline;
        private Fask.Graphic.DataGrid2 dataGrid1;
        private ONL_PARTNERTableAdapter oNL_PARTNERTableAdapter;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn5;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn4;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn3;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn2;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTableStyle tblStyle_vyberPartnera;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.Panel panelButtons;
        public Fask.Graphic.GraphicButton bOK;
        public Fask.Graphic.GraphicButton bStorno;
    }
}