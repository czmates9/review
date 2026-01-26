using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Logging;

namespace Konzola.Forms
{
    public partial class InputBox_Text_And_Checkbox : Form
    {
        public enum TypeOfCode { AlphaNumeric, NumericInt, NumericDecimal}
        private TypeOfCode typeOfCode = TypeOfCode.AlphaNumeric;

        public TypeOfCode CodeType
        {
            get { return typeOfCode; }
            set { this.typeOfCode = value; }
        }

        public string Kod
        {
            get { return this.textBoxText.Text; }
            set
            {
                this.textBoxText.Text = String.IsNullOrEmpty(value) ? string.Empty : value;
                this.textBoxText.SelectAll();
                this.textBoxText.Focus();
            }
        }

        public string TextInGroupboxTextBox
        {
            get { return this.tb_InGroupBox.Text; }
            set
            {
                this.tb_InGroupBox.Text = String.IsNullOrEmpty(value) ? string.Empty : value;
            }
        }
        

        public bool CheckBox 
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }


        private bool allowEmpty = true;     // povoleni zadani prazdne hodnoty


        private bool checkvalue = false;

        /// <summary>
        /// Kontrolovat hodnotu v pripade cisla (minimum a maximum)
        /// </summary>
        public bool CheckValue
        {
            get { return this.checkvalue; }
            set { this.checkvalue = value; }
        }

        #region INT min max
        private int minvalue = 0;
        /// <summary>
        /// Minimalni umoznena hodnota
        /// </summary>
        public int MinValue
        {
            get { return this.minvalue; }
            set { this.minvalue = value; }
        }

        private int maxvalue = 0;
        /// <summary>
        /// Minimalni umoznena hodnota
        /// </summary>
        public int MaxValue
        {
            get { return this.maxvalue; }
            set { this.maxvalue = value; }
        } 
        #endregion

        #region DEC min max

        private decimal minvalue_DEC = 0;
        /// <summary>
        /// Minimalni umoznena hodnota
        /// </summary>
        public decimal MinValue_DEC
        {
            get { return this.minvalue_DEC; }
            set { this.minvalue_DEC = value; }
        }

        private decimal maxvalue_DEC = 0;
        /// <summary>
        /// Minimalni umoznena hodnota
        /// </summary>
        public decimal MaxValue_DEC
        {
            get { return this.maxvalue_DEC; }
            set { this.maxvalue_DEC = value; }
        }

        #endregion


        private bool _showGrupBox = false;
        public bool ShowGrupBox
        {
            get { return _showGrupBox; }
            set 
            {
                _showGrupBox = value;
                groupBox1.Visible = _showGrupBox;
            }
        }

        public string ShowTextInGroupbox
        {
            get { return this.lb_GroupBox.Text; }
            set
            {
                this.lb_GroupBox.Text = String.IsNullOrEmpty(value) ? string.Empty : value;
            }
        }

        //public InputBox()
        //{
        //    InitializeComponent();
        //}

        protected InputBox_Text_And_Checkbox()
        {
            InitializeComponent();

            buttonStorno.Size = new Size(panelButtons.Width / 2, panelButtons.Height);


            //this.Location = MySystem.FormMidLocation.GetFormLocation(this.Size);
        }

        protected InputBox_Text_And_Checkbox(string caption, string text, string defaultvalue)
            : this()
        {
            this.Text = caption;
            this.labelText.Text = text;
            this.Kod = defaultvalue;
        }

        protected InputBox_Text_And_Checkbox(string caption, string text, string defaultvalue, bool DefaultCheck, string ChechText)
            : this()
        {
            this.Text = caption;
            this.labelText.Text = text;
            this.Kod = defaultvalue;
            this.CheckBox = DefaultCheck;
            this.checkBox1.Text = ChechText;
        }

        protected InputBox_Text_And_Checkbox(string caption, string text, string defaultvalue, bool allowEmpty)
            : this(caption, text, defaultvalue)
        {
            this.allowEmpty = allowEmpty;
            this.labelText.Text = text;
            //this.textBox1.Text = defaultvalue;
            this.Kod = defaultvalue;
        }

        protected InputBox_Text_And_Checkbox(string caption, string text, string defaultvalue, bool allowEmpty, char pwdchar)
            : this(caption, text, defaultvalue,allowEmpty)
        {
            this.textBoxText.PasswordChar = pwdchar;
        }




        public static DialogResult Show(string caption, string defaultvalue, out string value)
        {
            using (InputBox_Text_And_Checkbox myInputBox = new InputBox_Text_And_Checkbox(caption, caption, defaultvalue))
            {
                value = string.Empty;
                DialogResult dr = myInputBox.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    value = myInputBox.Kod;
                }

                return dr;
            }
        }

        public static DialogResult Show(string caption, string text, string defaultvalue, out string value)
        {
            using (InputBox_Text_And_Checkbox myInputBox = new InputBox_Text_And_Checkbox(caption, text, defaultvalue))
            {
                value = string.Empty;
                DialogResult dr = myInputBox.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    value = myInputBox.Kod;
                }

                return dr;
            }
        }

        public static DialogResult Show(string caption, string text, string defaultvalue, TypeOfCode codetype, bool checkvalue, int minvalue, int maxvalue, out string value)
        {
            using (InputBox_Text_And_Checkbox myInputBox = new InputBox_Text_And_Checkbox(caption, text, defaultvalue))
            {
                value = string.Empty;
                myInputBox.CodeType = codetype;
                myInputBox.CheckValue = checkvalue;
                myInputBox.minvalue = minvalue;
                myInputBox.maxvalue = maxvalue;
                myInputBox.allowEmpty = false;

                DialogResult dr = myInputBox.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    value = myInputBox.Kod;
                }

                return dr;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caption">Popis</param>
        /// <param name="text">Text</param>
        /// <param name="defaultvalue"> defaultna hodnota predvyplnena</param>
        /// <param name="codetype">typ zadavanej hodnoty</param>
        /// <param name="checkvalue">kontrola rozsahu</param>
        /// <param name="minvalue"> minimalna hodnota</param>
        /// <param name="maxvalue">maximalna hodnota</param>
        /// <param name="allowEmpty">povolit prazdne</param>
        /// <param name="value"> out hodnota</param>
        /// <returns></returns>
        public static DialogResult Show(string caption, string text, string defaultvalue, TypeOfCode codetype, bool checkvalue, int minvalue, int maxvalue, bool allowEmpty, out string value)
        {
            using (InputBox_Text_And_Checkbox myInputBox = new InputBox_Text_And_Checkbox(caption, text, defaultvalue))
            {
                value = string.Empty;
                myInputBox.CodeType = codetype;
                myInputBox.CheckValue = checkvalue;
                myInputBox.minvalue = minvalue;
                myInputBox.maxvalue = maxvalue;
                myInputBox.allowEmpty = allowEmpty;

                DialogResult dr = myInputBox.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    value = myInputBox.Kod;
                }

                return dr;
            }
        }

        public static DialogResult Show(string caption, string text, string defaultvalue, bool allowEmpty, out string value)
        {
            using (InputBox_Text_And_Checkbox myInputBox = new InputBox_Text_And_Checkbox(caption, text, defaultvalue, allowEmpty))
            {
                value = string.Empty;
                DialogResult dr = myInputBox.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    value = myInputBox.Kod;
                }

                return dr;
            }
        }

        public static DialogResult Show(string caption, string text, string defaultvalue, bool allowEmpty, out string value, char pwdchar)
        {
            using (InputBox_Text_And_Checkbox myInputBox = new InputBox_Text_And_Checkbox(caption, text, defaultvalue, allowEmpty, pwdchar))
            {
                value = string.Empty;
                DialogResult dr = myInputBox.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    value = myInputBox.Kod;
                }

                return dr;
            }
        }

        #region  TaD - Planovany Vyroby
        //public static DialogResult Show(string caption, string text, string defaultvalue, TypeOfCode codetype, bool checkvalue, decimal minvalue, decimal maxvalue, bool allowEmpty, bool defaultCheck, string CheckText, out string value, out bool Check)
        public static DialogResult Show(
            string caption, 
            string text, 
            string defaultvalue, 
            TypeOfCode codetype, 
            bool checkvalue, 
            decimal minvalue, 
            decimal maxvalue, 
            bool allowEmpty, 
            bool defaultCheck, 
            string CheckText, 
            out string value, 
            out bool Check,
            bool ShowGroupbox,
            string ShowTextInGroupbox,
            string ShowTextInGroupboxTextBox,
            out string TextInGroupboxTextBox
            )
        {
            using (InputBox_Text_And_Checkbox myInputBox = new InputBox_Text_And_Checkbox(caption, text, defaultvalue, defaultCheck, CheckText))
            {
                value = string.Empty;
                TextInGroupboxTextBox = string.Empty;
                Check = true;
                myInputBox.CodeType = codetype;
                myInputBox.CheckValue = checkvalue;
                myInputBox.minvalue_DEC = minvalue;
                myInputBox.maxvalue_DEC = maxvalue;
                myInputBox.allowEmpty = allowEmpty;

                myInputBox.TextInGroupboxTextBox = ShowTextInGroupboxTextBox;
                myInputBox.ShowGrupBox = ShowGroupbox;
                myInputBox.ShowTextInGroupbox = ShowTextInGroupbox;

                DialogResult dr = myInputBox.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    value = myInputBox.Kod;
                    Check = myInputBox.CheckBox;
                    TextInGroupboxTextBox = myInputBox.TextInGroupboxTextBox;
                }

                return dr;
            }
        }

        #endregion


        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    PerformOK();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void PerformCancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void PerformOK()
        {
            if (!isRightCode())
                return;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        ///  validace zadanych hodnot
        /// </summary>
        /// <returns>True - vse v poradku, False - chyba</returns>
        private bool isRightCode()
        {
            try
            {
                if (allowEmpty && textBoxText.Text.Trim().Length == 0)
                {
                    //OK v tomto připade je to povoleno prazde, a neni nic vyplneno...
                }
                else
                {
                    if (!allowEmpty && textBoxText.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("Vložte hodnotu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (typeOfCode == TypeOfCode.NumericInt)
                    {
                        int val = 0;
                        bool res = int.TryParse(textBoxText.Text.Trim(), out val);
                        if (!res)
                        {
                            MessageBox.Show("Smíte vkládat pouze celá čísla!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        if (CheckValue)
                        {
                            if (val < minvalue || val > maxvalue)
                            {
                                MessageBox.Show(string.Format("Číslo je mimo povolený rozsah ({0}-{1})", minvalue, maxvalue), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                        }
                    }
                    else if (typeOfCode == TypeOfCode.NumericDecimal)
                    {
                        decimal val = 0;
                        bool res = decimal.TryParse(textBoxText.Text.Trim(), out val);
                        if (!res)
                        {
                            MessageBox.Show("Smíte vkládat pouze čísla!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        if (CheckValue)
                        {
                            if (val < minvalue_DEC || val > maxvalue_DEC)
                            {
                                MessageBox.Show(string.Format("Číslo je mimo povolený rozsah ({0}-{1})", minvalue, maxvalue), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            this.ShowIcon = false;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }


    }
}
