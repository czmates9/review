using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Fask.Graphic
{
    public partial class DataField4 : UserControl
    {
        private Label lPopis;
        private Label lData;
        /// <summary>
        /// Obsah informacniho pole
        /// </summary>
        public string Popis
        {
            get { return lPopis.Text; }
            set { lPopis.Text = value; }
        }

        /// <summary>
        /// Zarovnani popisu
        /// </summary>
        public ContentAlignment PopisTextAlign
        {
            set { lPopis.TextAlign = value; }
            get { return lPopis.TextAlign; }
        }

        /// <summary>
        /// Zarovnani popisu
        /// </summary>
        public ContentAlignment DataTextAlign
        {
            set { lData.TextAlign = value; }
            get { return lData.TextAlign; }
        }

        /// <summary>
        /// Sirka popisneho pole.
        /// </summary>
        /// <remarks>Slouzi ke sjednoceni sirek poli</remarks>
        public int PopisWidth
        {
            get { return lPopis.Size.Width; }
            set { lPopis.Size = new Size(value, lPopis.Size.Height); }
        }

        /// <summary>
        /// Vyska popisneho pole.
        /// </summary>
        /// <remarks>Slouzi ke sjednoceni vysek poli</remarks>
        public int PopisHeight
        {
            get { return lPopis.Size.Height; }
            set { lPopis.Size = new Size(lPopis.Size.Width, value); }
        }

        /// <summary>
        /// Umožní doknutí vlevo, vpravo, nahoru, dolu. Fill a None jsou zakázány
        /// </summary>
        public DockStyle PopisDock
        {
            get { return lPopis.Dock; }
            set
            {
                //lPopis.Dock = value;
                switch (value)
                {
                    case DockStyle.Fill:
                    case DockStyle.None:
                        break;
                    //case DockStyle.Bottom:
                    //case DockStyle.Left:
                    //case DockStyle.Right:
                    //case DockStyle.Top:
                    default:
                        lPopis.Dock = value;
                        break;
                }
            }
        }


        ////private bool _readOnly = true;
        //public bool ReadOnly
        //{
        //    get { return tDataEdit.ReadOnly; }
        //    set { tDataEdit.ReadOnly = value;}
        //}

        ////private bool _readOnly = true;
        //public bool DataEnable
        //{
        //    get { return tDataEdit.Enabled; }
        //    set { tDataEdit.Enabled = value; }
        //}



        /// <summary>
        /// Obsah datoveho pole
        /// </summary>
        public string Data
        {
            get { return lData.Text; }
            set { lData.Text = value; }
        }

        /// <summary>
        /// Maximalni delka vstupniho pole
        /// </summary>
        /// <remarks>Maximum je 32767</remarks>
        //public int DataMaxLength
        //{
        //    get { return tDataEdit.MaxLength; }
        //    set { tDataEdit.MaxLength = value; }
        //}

        /// <summary>
        /// Barva pozadi datoveho pole
        /// </summary>
        //public Color DataBackColor
        //{
        //    get { return tDataEdit.BackColor; }
        //    set
        //    {
        //        tDataEdit.BackColor = value;
        //        //tData.BackColor = value;
        //    }
        //}

        //public override Font Font
        //{
        //    get
        //    {
        //        return base.Font;
        //    }
        //    set
        //    {
        //        base.Font = value;
        //        lPopis.Font = value;
        //        //tData.Font = value;
        //        tDataEdit.Font = value;
        //    }
        //}

        public Font PopisFont 
        {
            get { return lPopis.Font; }
            set { lPopis.Font = value; }
        }

        public Font DataFont
        {
            get { return lData.Font; }
            set { lData.Font = value; }
        }

        //public Color PopisForeColor
        //{
        //    get { return lPopis.ForeColor; }
        //    set { lPopis.ForeColor = value; }
        //}

        //public Color DataForeColor
        //{
        //    get { return tDataEdit.ForeColor; }
        //    set { tDataEdit.ForeColor = value; }
        //}



        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                this.lPopis.BackColor = value;
            }
        }

        //public bool MultiLine
        //{
        //    get { return this.tDataEdit.Multiline; }
        //    set { this.tDataEdit.Multiline = value; }
        //}
        
        public override string Text
        {
            get
            {
                //return base.Text;
                return this.Popis;
            }
            set
            {
                //base.Text = value;
                this.Popis = value;
            }
        }

        public DataField4()
        {
            InitializeComponent();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.Parent != null)
                this.BackColor = this.Parent.BackColor;
        }

        //protected override void OnGotFocus(EventArgs e)
        //{
        //    base.OnGotFocus(e);
        //    try
        //    {
        //        this.tDataEdit.Focus();
        //        this.tDataEdit.SelectAll();
        //    }
        //    catch
        //    {
        //    }
        //}

        //public override bool Focused
        //{
        //    get
        //    {
        //        //return base.Focused;
        //        if (base.Focused)
        //            return true;
        //        else if (this.tDataEdit.Focused)
        //            return true;

        //        return false;
        //    }
        //}

        private void InitializeComponent()
        {
            this.lPopis = new System.Windows.Forms.Label();
            this.lData = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lPopis
            // 
            this.lPopis.Dock = System.Windows.Forms.DockStyle.Left;
            this.lPopis.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.lPopis.Location = new System.Drawing.Point(0, 0);
            this.lPopis.Name = "lPopis";
            this.lPopis.Size = new System.Drawing.Size(95, 23);
            // 
            // lData
            // 
            this.lData.BackColor = System.Drawing.Color.White;
            this.lData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lData.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.lData.Location = new System.Drawing.Point(95, 0);
            this.lData.Name = "lData";
            this.lData.Size = new System.Drawing.Size(123, 23);
            // 
            // DataField4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.lData);
            this.Controls.Add(this.lPopis);
            this.Name = "DataField4";
            this.Size = new System.Drawing.Size(218, 23);
            this.ResumeLayout(false);

        }
    }
}
