using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Definition_SQL_Struncture
{




    public partial class Definice_Edit : Form
    {

        public CreateStruncture.DS_Information.SchemaColumnsRow Row;


        public Definice_Edit()
        {
            InitializeComponent();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateData())
                    return;

                DialogResult = DialogResult.OK;

            }
            catch (Exception ex)
            {

            }
        }


        private bool ValidateData()
        {
            try
            {
                errorProvider1.Clear();


                if (string.IsNullOrEmpty(tb_TABLE_NAME.Text))
                {
                    errorProvider1.SetError(tb_TABLE_NAME, "Nazev tabulky MUSI byt vyplnen");
                }
                else { Row.TABLE_NAME = tb_TABLE_NAME.Text.Trim(); }

                if (string.IsNullOrEmpty(tb_COLUMN_NAME.Text))
                {
                    errorProvider1.SetError(tb_COLUMN_NAME, "Nazev stloupce MUSI byt vyplnen");
                }
                else { Row.COLUMN_NAME = tb_COLUMN_NAME.Text.Trim(); }


                if (string.IsNullOrEmpty((string)cbox_DATA_TYPE.SelectedItem))
                {
                    errorProvider1.SetError(tb_COLUMN_NAME, "Typ MUSI byt vyplnen");
                }
                else { Row.DATA_TYPE = ((string)cbox_DATA_TYPE.SelectedItem).Trim(); }



                Row.COLUMN_DEFAULT = string.IsNullOrEmpty(tb_COLUMN_DEFAULT.Text) ? null : tb_COLUMN_DEFAULT.Text.Trim();


                if (!string.IsNullOrEmpty(tb_CHARACTER_MAXIMUM_LENGTH.Text))
                {
                    int a;
                    if (int.TryParse(tb_CHARACTER_MAXIMUM_LENGTH.Text.Trim(), out a))
                    {
                        Row.CHARACTER_MAXIMUM_LENGTH = a;
                    }
                    else
                    {
                        Row.SetCHARACTER_MAXIMUM_LENGTHNull();
                    }
                }
                else
                {
                    Row.SetCHARACTER_MAXIMUM_LENGTHNull();
                }

                if (!string.IsNullOrEmpty(tb_NUMERIC_PRECISION.Text))
                {
                    byte a;
                    if (byte.TryParse(tb_NUMERIC_PRECISION.Text.Trim(), out a))
                    {
                        Row.NUMERIC_PRECISION = a;
                    }
                    else
                    {
                        Row.SetNUMERIC_PRECISIONNull();
                    }
                }
                else
                {
                    Row.SetNUMERIC_PRECISIONNull();
                }



                if (!string.IsNullOrEmpty(tb_NUMRIC_SCALE.Text))
                {
                    int a;
                    if (int.TryParse(tb_NUMRIC_SCALE.Text.Trim(), out a))
                    {
                        Row.NUMERIC_SCALE = a;
                    }
                    else
                    {
                        Row.SetNUMERIC_SCALENull();
                    }
                }
                else
                {
                    Row.SetNUMERIC_SCALENull();
                }


                if (!string.IsNullOrEmpty(tb_DATETIME_PRECISION.Text))
                {
                    short a;
                    if (short.TryParse(tb_DATETIME_PRECISION.Text.Trim(), out a))
                    {
                        Row.DATETIME_PRECISION = a;
                    }
                    else
                    {
                        Row.SetDATETIME_PRECISIONNull();
                    }
                }
                else
                {
                    Row.SetDATETIME_PRECISIONNull();
                }

                Row.IS_NULLABLE = (string)cbox_IS_NULLABLE.SelectedItem;
             

                


            }
            catch (Exception ex)
            {
                return false;
            }
            return IsAllValid();
        }

        private bool IsAllValid()
        {
            //foreach (Control c in errorProvider1.ContainerControl.Controls)
            foreach (Control c in this.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;
            return true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void EditKonstanty_Edit_Load(object sender, EventArgs e)
        {
            string[] tmp = Enum.GetNames(typeof(SqlDbType));

            foreach (var item in tmp)
            {
                cbox_DATA_TYPE.Items.Add(item.ToLower());
            }


            if (Row != null)
            {

                tb_TABLE_NAME.Text = Row.TABLE_NAME;
                tb_COLUMN_DEFAULT.Text = Row.IsCOLUMN_DEFAULTNull() ? string.Empty : Row.COLUMN_DEFAULT;
                tb_NUMERIC_PRECISION.Text = Row.IsNUMERIC_PRECISIONNull() ? string.Empty : Row.NUMERIC_PRECISION.ToString();
                tb_CHARACTER_MAXIMUM_LENGTH.Text = Row.IsCHARACTER_MAXIMUM_LENGTHNull() ? string.Empty : Row.CHARACTER_MAXIMUM_LENGTH.ToString();
                tb_NUMRIC_SCALE.Text = Row.IsNUMERIC_SCALENull() ? string.Empty : Row.NUMERIC_SCALE.ToString();
                tb_DATETIME_PRECISION.Text = Row.IsDATETIME_PRECISIONNull() ? string.Empty : Row.DATETIME_PRECISION.ToString();
                cbox_IS_NULLABLE.SelectedItem = Row.IsIS_NULLABLENull() ? string.Empty : Row.IS_NULLABLE;
                cbox_DATA_TYPE.SelectedItem = Row.DATA_TYPE;
                tb_COLUMN_NAME.Text = Row.COLUMN_NAME;


            }
            else
            {
                CreateStruncture.DS_Information.SchemaColumnsDataTable dt = new CreateStruncture.DS_Information.SchemaColumnsDataTable();
                Row = dt.NewSchemaColumnsRow();
            }

        }
    }
}

