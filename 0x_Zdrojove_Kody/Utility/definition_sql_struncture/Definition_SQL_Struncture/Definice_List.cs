using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Definition_SQL_Struncture
{
    public partial class Definice_List : Form
    {



        //private CreateStruncture.DS_Information DS = null;

        //public Definition_SQL_Struncture.CreateStruncture.DS_Information.SchemaColumnsRow SelectedRow
        //{
        //    get
        //    {
        //        try
        //        {
        //            return ((DataRowView)(dg.BindingContext[bs].Current)).Row as Definition_SQL_Struncture.CreateStruncture.DS_Information.SchemaColumnsRow;
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}


        public Definice_List()
        {
            InitializeComponent();
            //ds = new CreateStruncture.DS_Information();
        }


        private void WriteError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        private void WriteError(string ex)
        {
            MessageBox.Show(this, ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        private void EditKonstanty_List_Load(object sender, EventArgs e)
        {
            dg.DataError += Dg_DataError;

            textBox1.Text = Settings.LastPath;

            string[] tmp = Enum.GetNames(typeof(SqlDbType));
            cb_Type.Items.Add(string.Empty);


            foreach (var item in tmp)
            {
                cb_Type.Items.Add(item.ToLower());
            }
        }

        private void Dg_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            WriteError(e.Exception);
            WriteError(String.Format("ColumnIndex:{0}, RowIndex:{1}", e.ColumnIndex, e.RowIndex));
        }

        private void PerformLoad()
        {

            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                try
                {
                    if (!File.Exists(textBox1.Text))
                    {
                        MessageBox.Show(this, "Soubor neexistuje...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                catch (Exception ex)
                {
                    WriteError(ex);
                }
            }
            else
            {
                MessageBox.Show(this, "Zadejte cestu...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                return;
            }


            Settings.LastPath = textBox1.Text;


            ds.Clear();
            ds.ReadXml(Settings.LastPath);

            bs.DataSource = ds.SchemaColumns;

            //textBox1.Enabled = false;
            btn_LoadPath.Enabled = false;

            panel_button.Enabled = true;
            panel_Filtr.Enabled = true;

        }

        private void PerformRefresh()
        {
            try
            {
                bs.DataSource = ds.SchemaColumns;
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }
        }


        private void PerformDelete()
        {

            try
            {
                if (dg.SelectedRows.Count == 0)
                {
                    MessageBox.Show("žádný zaznam k smazani", this.Text, MessageBoxButtons.OK);

                    return;
                }

                if (dg.SelectedRows.Count > 1)
                {
                    if (MessageBox.Show("Je vybrano víc zaznamu. Smazat všechny vybrane?", this.Text, MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;
                }



                foreach (DataGridViewRow item in dg.SelectedRows)
                {
                    DataRowView drv = this.bs[item.Index] as DataRowView;
                    var Row = drv.Row as CreateStruncture.DS_Information.SchemaColumnsRow;
                    Row.Delete();
                }
                ds.SchemaColumns.AcceptChanges();
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }

        }

        private void PerformAdd()
        {
            try
            {
                using (Definice_Edit edit = new Definice_Edit())
                {

                    if (edit.ShowDialog() == DialogResult.OK)
                    {
                        var Row = ds.SchemaColumns.NewSchemaColumnsRow();

                        Row.TABLE_NAME = edit.Row.TABLE_NAME;
                        Row.COLUMN_NAME = edit.Row.COLUMN_NAME;
                        Row.DATA_TYPE = edit.Row.DATA_TYPE;


                        if (edit.Row.IsCOLUMN_DEFAULTNull())
                            Row.SetCOLUMN_DEFAULTNull();
                        else
                            Row.COLUMN_DEFAULT = edit.Row.COLUMN_DEFAULT;

                        if (edit.Row.IsIS_NULLABLENull())
                            Row.SetIS_NULLABLENull();
                        else
                            Row.IS_NULLABLE = edit.Row.IS_NULLABLE;

                        if (edit.Row.IsCHARACTER_MAXIMUM_LENGTHNull())
                            Row.SetCHARACTER_MAXIMUM_LENGTHNull();
                        else
                            Row.CHARACTER_MAXIMUM_LENGTH = edit.Row.CHARACTER_MAXIMUM_LENGTH;

                        if (edit.Row.IsNUMERIC_PRECISIONNull())
                            Row.SetNUMERIC_PRECISIONNull();
                        else
                            Row.NUMERIC_PRECISION = edit.Row.NUMERIC_PRECISION;

                        if (edit.Row.IsNUMERIC_SCALENull())
                            Row.SetNUMERIC_SCALENull();
                        else
                            Row.NUMERIC_SCALE = edit.Row.NUMERIC_SCALE;

                        if (edit.Row.IsDATETIME_PRECISIONNull())
                            Row.SetDATETIME_PRECISIONNull();
                        else
                            Row.DATETIME_PRECISION = edit.Row.DATETIME_PRECISION;
                    


                        ds.SchemaColumns.AddSchemaColumnsRow(Row);
                    }
                }

                ds.SchemaColumns.AcceptChanges();
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }


        }

        private void PerformEdit()
        {
            try
            {

                if (dg.SelectedRows.Count == 0)
                {
                    MessageBox.Show("žádný zaznam k editaci", this.Text, MessageBoxButtons.OK);

                    return;
                }

                if (dg.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Je vybrano víc zaznamu.", this.Text, MessageBoxButtons.OK);

                    return;
                }


                using (Definice_Edit edit = new Definice_Edit())
                {

                    edit.Row = ((DataRowView)(dg.SelectedRows[0].DataBoundItem)).Row as CreateStruncture.DS_Information.SchemaColumnsRow;

                    if (edit.ShowDialog() == DialogResult.OK)
                    {
                        ds.SchemaColumns.AcceptChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                WriteError(ex);
            }


        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                PerformEdit();
                PerformRefresh();
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                PerformAdd();
                PerformRefresh();
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                PerformDelete();
                PerformRefresh();
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }
        }


       private CreateStruncture.DS_Information GetInformation()
        {
            CreateStruncture.DS_Information ds = new CreateStruncture.DS_Information();

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.SqlClient.SqlCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = @"select TABLE_NAME, COLUMN_DEFAULT, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE, DATETIME_PRECISION, COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS";

                da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Settings.ConnectionString);

                da.Fill(ds.SchemaColumns);

            }
            catch (Exception ex)
            {
                WriteError(ex);
                return null;
            }

            return ds;
        }



        private void btn_LoadPath_Click(object sender, EventArgs e)
        {
            try
            {

                System.Windows.Forms.OpenFileDialog OF = new System.Windows.Forms.OpenFileDialog();
                OF.FileName = "DS_Information.xml";
                OF.Filter = "XML soubor (*.xml)|*.xml|Všechny subory (*.*)|*.*";
                OF.FilterIndex = 1;
                OF.RestoreDirectory = true;
                OF.InitialDirectory = Settings.LastPath;
                OF.Multiselect = false;

                if (OF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBox1.Text = OF.FileName;
                    PerformLoad();

                }
            }
            catch (Exception ex)
            {
                WriteError(ex);

            }
        }

        private void Definice_List_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Display a MsgBox asking the user to close the form.

            DialogResult dr = MessageBox.Show("Chcete před zavřením uložit? ", "Close Form", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);


            if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else if (dr == DialogResult.Yes)
            {
                try
                {
                    if (!Directory.Exists(Settings.LastPath))
                        Directory.CreateDirectory(Path.GetDirectoryName(Settings.LastPath));

                    ds.WriteXml(Settings.LastPath);
                }
                catch (Exception ex)
                {
                    WriteError(ex);
                }
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                string Filter = string.Empty;


                if (!string.IsNullOrEmpty(tb_TableName.Text))
                {
                    Filter += "TABLE_NAME = '" + tb_TableName.Text.Trim() + "' ";
                }

                if (!string.IsNullOrEmpty(tb_ColumnName.Text))
                {
                    if (!string.IsNullOrEmpty(Filter))
                        Filter += " AND ";

                    Filter += "COLUMN_NAME = '" + tb_ColumnName.Text.Trim() + "' ";
                }

                if (!string.IsNullOrEmpty((string)cb_Type.SelectedItem))
                {
                    if (!string.IsNullOrEmpty(Filter))
                        Filter += " AND ";

                    Filter += "DATA_TYPE = '" + ((string)cb_Type.SelectedItem).Trim() + "' ";
                }


                bs.Filter = Filter;
                //source1.Filter = "artist = 'Dave Matthews' OR cd = 'Tigerlily'";

            }
            catch (Exception ex)
            {
                WriteError(ex);
            }
        }

        private void btn_ClearFilter_Click(object sender, EventArgs e)
        {
            try
            {
                tb_ColumnName.Text = string.Empty;
                tb_TableName.Text = string.Empty;
                bs.Filter = string.Empty;
                cb_Type.SelectedItem = string.Empty;
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }
        }

        private void btn_Verifikace_Click(object sender, EventArgs e)
        {
            try
            {
                //0. v DS.SchemaColumns je načteny XML soubor
                //1. Dotahne se aktualni tabulka z DB co je promenna AktualniDB
                //3. pomoci foreach se projde cela tabulka na stranne SQL serveru

                // 4. Vyhleda se řadek pomoci nazvu tabulky a nazvu stloupce

                //kontrolovat?? jak??


                CreateStruncture.DS_Information AktualniDB = GetInformation();


                using (Definice_Validace frm = new Definice_Validace())
                {

                    frm.ds_File = ds;
                    frm.ds_SQL = AktualniDB;


                    frm.ShowDialog();

                }
            }
            catch (Exception ex)
            {

                WriteError(ex);
            }
        }

        private void tsmi_CreateFile_Click(object sender, EventArgs e)
        {
            
            try
            {
                ds = GetInformation();

                saveFileDialog1.FileName = "DS_Information.xml";
                saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = saveFileDialog1.FileName;

                    if (!Directory.Exists(textBox1.Text))
                        Directory.CreateDirectory(Path.GetDirectoryName(textBox1.Text));

                }

                ds.SchemaColumns.WriteXml(textBox1.Text);
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }
        }

    }
}
