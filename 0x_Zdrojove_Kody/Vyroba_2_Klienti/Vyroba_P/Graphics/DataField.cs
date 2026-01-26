using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Fask.Graphic
{
    public partial class DataField : UserControl
    {
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
            set
            {
                tData.TextAlign = value;
                switch (value)
                {
                    case ContentAlignment.TopCenter:
                        tDataEdit.TextAlign = HorizontalAlignment.Center;
                        break;
                    case ContentAlignment.TopLeft:
                        tDataEdit.TextAlign = HorizontalAlignment.Left;
                        break;
                    case ContentAlignment.TopRight:
                        tDataEdit.TextAlign = HorizontalAlignment.Right;
                        break;
                    default:
                        break;
                }
            }
            get
            {
                return tData.TextAlign;
            }
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

        private bool _readOnly = true;
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                if (_readOnly)
                {
                    tDataEdit.Hide();
                    tData.Show();
                }
                else
                {
                    tDataEdit.Show();
                    tData.Hide();
                }
            }
        }


        /// <summary>
        /// Obsah datoveho pole
        /// </summary>
        public string Data
        {
            get { return tDataEdit.Text; }
            set
            {
                tData.Text = value; 
                tDataEdit.Text = value;
                tDataEdit.SelectAll();
            }
        }

        /// <summary>
        /// Maximalni delka vstupniho pole
        /// </summary>
        /// <remarks>Maximum je 32767</remarks>
        public int DataMaxLength
        {
            get { return tDataEdit.MaxLength; }
            set { tDataEdit.MaxLength = value; }
        }

        /// <summary>
        /// Barva pozadi datoveho pole
        /// </summary>
        public Color DataBackColor
        {
            get { return tDataEdit.BackColor; }
            set
            {
                tDataEdit.BackColor = value;
                tData.BackColor = value;
            }
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                lPopis.Font = value;
                tData.Font = value;
                tDataEdit.Font = value;
            }
        }

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

        public bool MultiLine
        {
            get { return this.tDataEdit.Multiline; }
            set { this.tDataEdit.Multiline = value; }
        }
        
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

        public DataField()
        {
            InitializeComponent();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.Parent != null)
                this.BackColor = this.Parent.BackColor;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            try
            {
                this.tDataEdit.Focus();
                this.tDataEdit.SelectAll();
            }
            catch
            {
            }
        }

        public override bool Focused
        {
            get
            {
                //return base.Focused;
                if (base.Focused)
                    return true;
                else if (this.tDataEdit.Focused)
                    return true;

                return false;
            }
        }
    }
}
