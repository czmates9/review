using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Definition_SQL_Struncture
{
    public partial class Definice_Dokumentace : Form
    {

        #region Editace Description v SQL
        //možnost přidat popis do SQL
        //procedura
        //
        //[sp_addextendedproperty]
        //
        // SELECT * FROM sys.extended_properties

        #region Procedury pro praci s SQL


        /*

        EXEC sp_addextendedproperty
        @name = N'MS_Description',    -- konstanta
        @value = 'Test poznamky',    -- Poznamka
        @level0type = N'Schema', @level0name = 'dbo',   
        @level1type = N'Table',  @level1name = 'Corrects',  
        @level2type = N'Column', @level2name = 'Production';  
        GO

        */

        /*

        exec sp_updateextendedproperty
             @name = N'MS_Description' 
            ,@value = 'Uprava Textu.' 
            ,@level0type = N'Schema', @level0name = 'dbo' 
            ,@level1type = N'Table',  @level1name = 'Corrects'
            ,@level2type = N'Column', @level2name = 'Production'
        GO	

        */

        /*

        EXEC sp_dropextendedproperty
        @name = N'MS_Description',   
        @level0type = N'Schema', @level0name = 'dbo',  
        @level1type = N'Table',  @level1name = 'Corrects',  
        @level2type = N'Column', @level2name = 'Production';  
        GO
        */


        #endregion


        //Nasledně by bylo o to jednoduchší automaticky generovat dokumentaci, když bude popis současti SQL

        #endregion

        #region Dotaže informaci z SQL
        /*
        SELECT
        col.TABLE_NAME as [TABLE],
        tbl.TABLE_TYPE as [typ],
        tbl.TABLE_SCHEMA as [Schema],
        col.COLUMN_NAME as [COLUMN],
        col.DATA_TYPE as [TYPECOL],
        ISNULL(CONVERT(nvarchar(20), col.CHARACTER_MAXIMUM_LENGTH),'-') as [MAXLEN],
        ISNULL(col.COLUMN_DEFAULT,'null') as [DEFVAL] , 
        col.IS_NULLABLE as [ISNULL],  
        ISNULL(CONVERT(nvarchar(20), col.NUMERIC_PRECISION),'-') as [NUMPREC],
        ISNULL(CONVERT(nvarchar(20), col.NUMERIC_SCALE),'-') as [NUMSCALE],
        ISNULL(CONVERT(nvarchar(20), col.DATETIME_PRECISION),'-') as [DTPREC],
        ISNULL(prop.value,'') AS[DESCCOL]
        FROM INFORMATION_SCHEMA.TABLES AS tbl
        INNER JOIN INFORMATION_SCHEMA.COLUMNS AS col ON col.TABLE_NAME = tbl.TABLE_NAME
        INNER JOIN sys.columns AS sc ON sc.object_id = object_id(tbl.table_schema + '.' + tbl.table_name)
            AND sc.NAME = col.COLUMN_NAME
        LEFT JOIN sys.extended_properties prop ON prop.major_id = sc.object_id
            AND prop.minor_id = sc.column_id
            AND prop.NAME = 'MS_Description'
        */
        #endregion


        public Definice_Dokumentace()
        {
            InitializeComponent();

            tb_TableName.Text = "Corrects";
        }

        private void WriteError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        private void Writeinfo(string txt)
        {
            MessageBox.Show(this, txt, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void btn_ClearFilter_Click(object sender, EventArgs e)
        {
            try
            {
                tb_ColumnName.Text = string.Empty;
                tb_TableName.Text = string.Empty;
                cb_Type.SelectedItem = string.Empty;
                cb_Type_Note.SelectedItem = string.Empty;
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                Dokumentace.Filter Filter = new Dokumentace.Filter();

                if (!string.IsNullOrEmpty((string)cb_Type_Note.SelectedItem))
                {
                    Filter.DESCNAME = ((string)cb_Type_Note.SelectedItem).Trim();
                }

                if (!string.IsNullOrEmpty(tb_TableName.Text))
                {
                    Filter.TABLE_NAME = tb_TableName.Text.Trim();
                }

                if (!string.IsNullOrEmpty(tb_ColumnName.Text))
                {
                    Filter.COLUMN_NAME = tb_ColumnName.Text.Trim();
                }

                if (!string.IsNullOrEmpty((string)cb_Type.SelectedItem))
                {
                    Filter.DATA_TYPE = ((string)cb_Type.SelectedItem).Trim();
                }

                Dokumentace.Databaze_Com.Fill_All(this.ds_dok, Filter);

                bs_dok.DataSource = this.ds_dok;

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

                var Row = ((DataRowView)(dg_dok.SelectedRows[0].DataBoundItem)).Row as Dokumentace.Dok.DT_DocumentationRow;

                if (Row == null)
                    return;


                using (Definice_Dokumentace_Edit edit = new Definice_Dokumentace_Edit())
                {

                    edit.Typ_Poznamky = Row.DESCNAME;
                    edit.Poznamka_Original = Row.DESCCOL;
                    edit.Schema = Row.Schema;
                    edit.Typ = Row.typ;
                    edit.Schema = Row.Schema;
                    edit.Tabulka = Row.TABLE;
                    edit.Stloupec = Row.COLUMN;

                    edit.ShowDialog();

                }

            }
            catch (System.Exception ex)
            {
                WriteError(ex);
            }
        }

        private void Definice_Dokumentace_Load(object sender, EventArgs e)
        {
            try
            {
                string[] tmp = Enum.GetNames(typeof(SqlDbType));
                cb_Type.Items.Add(string.Empty);


                foreach (var item in tmp)
                {
                    cb_Type.Items.Add(item.ToLower());
                }


                cb_Type_Note.Items.Add(string.Empty);

                var dt = Dokumentace.Databaze_Com.Get_DESCNAMEs();

                if (dt.Count > 0)
                {
                    foreach (var row in dt)
                    {
                        cb_Type_Note.Items.Add(row.NAME);
                    }
                }
            }
            catch (System.Exception ex)
            {
                WriteError(ex);
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {

                var Row = ((DataRowView)(dg_dok.SelectedRows[0].DataBoundItem)).Row as Dokumentace.Dok.DT_DocumentationRow;

                if (Row == null)
                    return;

                if (Row.IsDESCNAMENull())
                {
                    WriteError(new Exception("Nenalezena poznamka ke smazaní..."));

                    return;
                }

                string msg = string.Format(
                    "Opravdu chcete smazat poznámku typu:'{0}'" 
                    + Environment.NewLine + 
                    "V tabulce:'{1}' u stloupce:'{2}' ?",
                    Row.DESCNAME,
                    Row.TABLE,
                    Row.COLUMN
                    );


                var dr = MessageBox.Show(this, msg, "Opravdu?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (dr != DialogResult.Yes)
                    return;

                
                var state = Dokumentace.Databaze_Com.Delete_Poznamka(Row.DESCNAME ,Row.Schema, Row.TABLE, Row.COLUMN);

                if(state == false)
                    WriteError(new Exception("Nastala chyba pri smazani..."));
                else
                    WriteError(new Exception("Poznamka uspešně smazana"));

                btn_Search_Click(null, null);

            }
            catch (Exception ex)
            {
                WriteError(ex);
            }
        }

        private void btn_Gen_HTML_Click(object sender, EventArgs e)
        {
            try
            {


                Dokumentace.Dok ds = new Dokumentace.Dok();
                Dokumentace.Databaze_Com.Fill_All(ds, new Dokumentace.Filter());

                using (var generatorDokumentace = new Dokumentace.Generate_HTML.GeneratorDokumentace())
                {
                    generatorDokumentace.Generuj(ds);
                }

                Writeinfo("Dokumentace uspešne vygenerovana....");
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }
        }


    }
}
