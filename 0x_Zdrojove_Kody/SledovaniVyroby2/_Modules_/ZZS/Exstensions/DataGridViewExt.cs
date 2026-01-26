//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using System.IO;
//using System.Reflection;
//using System.Xml.Serialization;
//using System.Data;

//namespace FASK.SledovaniVyroby.Module
//{
//    [Serializable]
//    public sealed class ColumnInfo
//    {
//        public string Name { get; set; }
//        public int DisplayIndex { get; set; }
//        public int Width { get; set; }
//        public bool Visible { get; set; }
//        public string HeaderText { get; set; }
//    }

//    public static class DataGridViewExtenstions
//    {
//        /// <summary>
//        /// Loads columns information from the specified XML file
//        /// </summary>
//        /// <param name="dgv">DataGridView control instance</param>
//        /// <param name="fileName">XML configuration file</param>
//        public static void LoadConfiguration(this DataGridView dgv, string fileName)
//        {
//            if (dgv == null)
//                return;



//            if (Logging.LogConfig.LoginID != null)
//                fileName = Logging.LogConfig.LoginID.Trim() + fileName;

//            DataGridViewColumnSelector ds = new DataGridViewColumnSelector(dgv);

//            fileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), fileName + ".xml");
//            // zapnuti DoubleBuffered kvuli zrychleni datagridu
//            typeof(DataGridView).InvokeMember(
//                        "DoubleBuffered",
//                        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
//                        null,
//                        dgv,
//                        new object[] { true });

//            if (!File.Exists(fileName))
//                return;
//            List<ColumnInfo> columns;
//            using (var streamReader = new StreamReader(fileName))
//            {
//                var xmlSerializer = new XmlSerializer(typeof(List<ColumnInfo>));
//                columns = (List<ColumnInfo>)xmlSerializer.Deserialize(streamReader);
//            }
//            foreach (var column in columns)
//            {
//                dgv.Columns[column.Name].DisplayIndex = column.DisplayIndex;
//                dgv.Columns[column.Name].Width = column.Width;
//                dgv.Columns[column.Name].Visible = column.Visible;
//                if (!string.IsNullOrEmpty(column.HeaderText))
//                    dgv.Columns[column.Name].HeaderText = column.HeaderText;
//            }
//        }

//        /// <summary>
//        /// Saves columns information to the specified XML file
//        /// </summary>
//        /// <param name="dgv">DataGridView control instance</param>
//        /// <param name="fileName">XML configuration file</param>
//        public static void SaveConfiguration(this DataGridView dgv, string fileName)
//        {
//            if (Logging.LogConfig.LoginID != null)
//                fileName = Logging.LogConfig.LoginID.Trim() + fileName;

//            fileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), fileName + ".xml");

//            var columns = new List<ColumnInfo>();
//            for (int i = 0; i < dgv.Columns.Count; i++)
//            {
//                var column = new ColumnInfo();
//                column.Name = dgv.Columns[i].Name;
//                column.DisplayIndex = dgv.Columns[i].DisplayIndex;
//                column.Width = dgv.Columns[i].Width;
//                column.Visible = dgv.Columns[i].Visible;
//                column.HeaderText = dgv.Columns[i].HeaderText;
//                columns.Add(column);
//            }
//            using (var streamWriter = new StreamWriter(fileName))
//            {
//                var xmlSerializer = new XmlSerializer(typeof(List<ColumnInfo>));
//                xmlSerializer.Serialize(streamWriter, columns);
//            }
//        }

//        // 27.5.2016 PeV: nikdy se nepouzivalo
//        //public static void ExportToCsV(this DataGridView dgv, string fileName)
//        //{
//        //    string stOutput = "";
//        //    // Export titles:
//        //    string sHeaders = "";

//        //    for (int j = 0; j < dgv.Columns.Count; j++)
//        //    {
//        //        // ulozi se pouze, pokud je videt ...
//        //        if (dgv.Columns[j].Visible)
//        //        {
//        //            sHeaders = sHeaders.ToString() + Convert.ToString(dgv.Columns[j].HeaderText) + "\t";
//        //        }
//        //        else
//        //        {
//        //            string test = string.Empty;
//        //        }
//        //    }
//        //    stOutput += sHeaders + "\r\n";
//        //    // Export data.
//        //    //for (int i = 0; i < dgv.RowCount - 1; i++)
//        //    for (int i = 0; i < dgv.Rows.Count; i++)
//        //    {
//        //        string stLine = "";
//        //        for (int j = 0; j < dgv.Rows[i].Cells.Count; j++)
//        //            stLine = stLine.ToString() + Convert.ToString(dgv.Rows[i].Cells[j].Value) + "\t";
//        //        stOutput += stLine + "\r\n";
//        //    }
//        //    Encoding utf16 = Encoding.GetEncoding(1254);
//        //    byte[] output = utf16.GetBytes(stOutput);
//        //    FileStream fs = new FileStream(fileName, FileMode.Create);
//        //    BinaryWriter bw = new BinaryWriter(fs);
//        //    bw.Write(output, 0, output.Length); //write the encoded file
//        //    bw.Flush();
//        //    bw.Close();
//        //    fs.Close();
//        //}

//        // 27.5.2016 PeV: pomale, je potreba mit nainstalovan excel
//        /// <summary>
//        /// Dojde k exportu veškerých zobrazených dat včetně veškerých sloupců (i těch, které jsou skryté)
//        /// </summary>
//        /// <param name="dgv"></param>
//        /// <param name="fileName"></param>
//        //public static void ExportAllRowsAllColumnsToExcel(this DataGridView dgv, string fileName)
//        //{
//        //    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
//        //    Workbook xlWorkbook = ExcelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);

//        //    // Loop over DataTables in DataSet.
//        //    //DataTableCollection collection = this.ukolovaniDataset1.Tables;

//        //    //for (int i = collection.Count; i > 0; i--)
//        //    //{
//        //    Sheets xlSheets = null;
//        //    Worksheet xlWorksheet = null;
//        //    //Create Excel Sheets
//        //    xlSheets = ExcelApp.Sheets;
//        //    xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
//        //                   Type.Missing, Type.Missing, Type.Missing);

//        //    // set cell format to text
//        //    //xlWorksheet.Cells.NumberFormat = "@";
//        //    // set cell format to General
//        //    xlWorksheet.Cells.NumberFormat = "General";

//        //    //Header
//        //    for (int k = 1; k < dgv.Columns.Count + 1; k++)
//        //    {
//        //        //if(dgv.Columns[k - 1].Visible)
//        //        ExcelApp.Cells[1, k] = dgv.Columns[k - 1].HeaderText;

//        //    }

//        //    int rowNumber = 0;
//        //    int columnNumber = 0;

//        //    for (int i = 0; i < dgv.Rows.Count; i++)
//        //    {
//        //        for (int j = 0; j < dgv.Columns.Count; j++)
//        //        {
//        //            //if (dgv.Columns[j - 1].Visible)
//        //            ExcelApp.Range[ExcelApp.Cells[i + 2, j + 1], ExcelApp.Cells[i + 2, j + 1]].NumberFormat = datatype(dgv.Columns[j].ValueType);
//        //            //ExcelApp.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value.ToString();
//        //            //ExcelApp.Cells[i + 2, j + 1] = (dgv.Columns[j].ValueType == Type.GetType("System.DateTime")) ? dgv.Rows[i].Cells[j].Value.ToString() : dgv.Rows[i].Cells[j].Value;

//        //            if ((dgv.Columns[j].ValueType == Type.GetType("System.DateTime")) || (dgv.Columns[j].ValueType == Type.GetType("System.Guid")))
//        //                ExcelApp.Cells[rowNumber + 2, columnNumber + 1] = dgv.Rows[i].Cells[j].Value.ToString();
//        //            else
//        //                ExcelApp.Cells[rowNumber + 2, columnNumber + 1] = dgv.Rows[i].Cells[j].Value;
//        //            //ExcelApp.Cells[rowNumber + 2, columnNumber + 1] = (dgv.Columns[j].ValueType == Type.GetType("System.DateTime")) ? dgv.Rows[i].Cells[j].Value.ToString() : dgv.Rows[i].Cells[j].Value;
//        //            columnNumber++;
//        //        }
//        //        rowNumber++;
//        //        columnNumber = 0;
//        //    }
//        //    ExcelApp.Columns.AutoFit();

//        //    //((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
//        //    ExcelApp.Visible = true;

//        //    ExcelApp = null;
//        //    xlWorkbook = null;
//        //    GC.Collect();
//        //    GC.WaitForPendingFinalizers();
//        //}


//        //DataTable dt1 = null;
//        //BindingSource x = null;
//        //if (dgvCustom.DataSource is BindingSource && transAllergensDataGridView.CurrentRow != null)
//        //{
//        //    x = (BindingSource)transAllergensDataGridView.DataSource;
//        //    dt1 = ((DataSet)x.DataSource).Tables[x.DataMember].Clone();
//        //    dt1.ImportRow(((DataRowView)x.Current).Row); 
//        //}


//        //#region Export to Excel
//        //private static DialogResult showPathDialog(out string filepath)
//        //{
//        //    filepath = string.Empty;
//        //    SaveFileDialog sfd = new SaveFileDialog();
//        //    sfd.Filter = ".xlsx Files (*.xlsx)|*.xlsx";

//        //    DialogResult dr = sfd.ShowDialog();
//        //    if (dr == DialogResult.OK)
//        //    {
//        //        string path = Path.GetDirectoryName(sfd.FileName);
//        //        string filename = Path.GetFileNameWithoutExtension(sfd.FileName);
//        //        filepath = sfd.FileName;
//        //    }

//        //    return dr;
//        //}

//        //public static bool ExportToExcel(this DataGridView dgv, Fask.Console.Interfaces.Classes.EXPORT_DAT typ_exportu)
//        //{
//        //    string path = string.Empty;

//        //    // TODO: konfiguracne
//        //    if (showPathDialog(out path) != DialogResult.OK)
//        //        return false;

//        //    if (System.IO.File.Exists(path))
//        //        System.IO.File.Delete(path);

//        //    System.IO.FileInfo newFile = new System.IO.FileInfo(path);
//        //    using (OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(newFile))
//        //    {
//        //        OfficeOpenXml.ExcelWorksheet ws = pck.Workbook.Worksheets.Add("List1");

//        //        // kopie tabulky s naslednym odstranenim skrytych sloupcu
//        //        System.Data.DataTable tCxC = new System.Data.DataTable();

//        //        // exportovat vse
//        //        if (typ_exportu == Fask.Console.Interfaces.Classes.EXPORT_DAT.VSE)
//        //        {
//        //            if (dgv.DataSource is BindingSource)
//        //            {                        
//        //                BindingSource bs = (BindingSource)dgv.DataSource;
//        //                //tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].Copy();
//        //                if (bs.List is DataView)
//        //                {
//        //                    tCxC = ((DataView)bs.List).ToTable().Copy();
//        //                }
//        //                else if (bs.DataSource is DataSet)
//        //                {
//        //                    tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].DefaultView.ToTable().Copy();
//        //                }
//        //                else if (bs.DataSource is System.Data.DataTable)
//        //                {
//        //                    tCxC = ((System.Data.DataTable)bs.DataSource).DefaultView.ToTable().Copy();
//        //                }
//        //                else throw new Exception("Export Excel: Neznámý typ pro přetypování");
//        //            }
//        //            else if (dgv.DataSource is System.Data.DataTable)
//        //            {
//        //                tCxC = ((System.Data.DataTable)dgv.DataSource).DefaultView.ToTable().Copy();
//        //            }
//        //            else throw new Exception("Export Excel: Neznámý typ pro přetypování");
//        //        }
//        //        else  // exportovat vybrane
//        //        {
//        //            System.Data.DataTable dtClone = new System.Data.DataTable();
//        //            if (dgv.DataSource is BindingSource)
//        //            {
//        //                BindingSource bs = (BindingSource)dgv.DataSource;
//        //                //tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
//        //                //dtClone = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
//        //                if (bs.DataSource is DataSet)
//        //                {
//        //                    tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
//        //                    dtClone = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
//        //                }
//        //                else if (bs.DataSource is System.Data.DataTable)
//        //                {
//        //                    tCxC = ((System.Data.DataTable)bs.DataSource).Clone();
//        //                    dtClone = ((System.Data.DataTable)bs.DataSource).Clone();
//        //                }
//        //                else throw new Exception("Export Excel: Neznámý typ pro přetypování");
//        //            }
//        //            else if (dgv.DataSource is System.Data.DataTable)
//        //            {
//        //                tCxC = ((System.Data.DataTable)dgv.DataSource).Copy();
//        //                dtClone = ((System.Data.DataTable)dgv.DataSource).Copy();
//        //            }
//        //            else throw new Exception("Export Excel: Neznámý typ pro přetypování");

//        //            // import vybranych radku
//        //            foreach (DataGridViewRow row in dgv.SelectedRows)
//        //            {
//        //                dtClone.ImportRow(((DataRowView)row.DataBoundItem).Row);
//        //            }
//        //            dtClone.AcceptChanges();

//        //            // otocit poradi a naimportovat do tCxC
//        //            for (int i = dtClone.Rows.Count - 1; i >= 0; i--)
//        //            {
//        //                tCxC.ImportRow(dtClone.Rows[i]);
//        //            }
//        //            tCxC.AcceptChanges();
//        //        }

//        //        // kopie datatable, se kterym se bude pracovat
//        //        System.Data.DataTable dt2 = tCxC.Copy();

//        //        // odstraneni skrytych sloupcu
//        //        for (int i = 0; i < dgv.Columns.Count; i++)
//        //        {
//        //            if (!dgv.Columns[i].Visible)
//        //                dt2.Columns.Remove(dgv.Columns[i].DataPropertyName);
//        //        }

//        //        // zmena nazvu hlavicek na ty, ktere se zobrazuji
//        //        foreach (DataGridViewColumn item in dgv.Columns)
//        //        {
//        //            if (dt2.Columns.Contains(item.DataPropertyName) && !dt2.Columns.Contains(item.HeaderText))
//        //                dt2.Columns[item.DataPropertyName].ColumnName = item.HeaderText;
//        //        }

//        //        // naplneni excelu daty
//        //        ws.Cells["A1"].LoadFromDataTable(dt2, true);

//        //        // nastaveni formatu datumu
//        //        var dateColumns = from DataColumn d in dt2.Columns
//        //                          where d.DataType == typeof(DateTime)// || d.ColumnName.Contains("Date")
//        //                          select d.Ordinal + 1;
//        //        foreach (var dc in dateColumns)
//        //        {
//        //            ws.Cells[2, dc, dt2.Rows.Count + 2, dc].Style.Numberformat.Format = Settings.ExportyExcelFormatDatum;
//        //        }

//        //        // nastaveni automaticke velikosti sloupcu
//        //        ws.Cells.AutoFitColumns();

//        //        // ulozeni
//        //        pck.Save();
//        //    }

//        //    // spusteni v excelu
//        //    if(Settings.ExportyExcelOtevritPoVygenerovani)
//        //        System.Diagnostics.Process.Start(path);

//        //    return true;
//        //}

//        ///// <summary>
//        ///// Pouziva se pouze pro export nasnimane inventury v pripade, ze je povoleno zobrazeni checkboxu pro vyber
//        ///// </summary>
//        ///// <param name="dgv"></param>
//        ///// <param name="typ_exportu"></param>
//        //public static void ExportToExcelInventura(this DataGridView dgv, Fask.Console.Interfaces.Classes.EXPORT_DAT typ_exportu)
//        //{
//        //    string path = string.Empty;

//        //    // TODO: konfiguracne
//        //    if (showPathDialog(out path) != DialogResult.OK)
//        //        return;

//        //    if (System.IO.File.Exists(path))
//        //        System.IO.File.Delete(path);

//        //    System.IO.FileInfo newFile = new System.IO.FileInfo(path);
//        //    using (OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(newFile))
//        //    {
//        //        OfficeOpenXml.ExcelWorksheet ws = pck.Workbook.Worksheets.Add("List1");

//        //        // kopie tabulky s naslednym odstranenim skrytych sloupcu
//        //        System.Data.DataTable tCxC = new System.Data.DataTable();
//        //        if (typ_exportu == Fask.Console.Interfaces.Classes.EXPORT_DAT.VSE)
//        //        {
//        //            if (dgv.DataSource is BindingSource)
//        //            {
//        //                BindingSource bs = (BindingSource)dgv.DataSource;
//        //                if (bs.DataSource is DataSet)
//        //                {
//        //                    tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].Copy();
//        //                }
//        //                else if (bs.DataSource is System.Data.DataTable)
//        //                {
//        //                    tCxC = ((System.Data.DataTable)bs.DataSource).Copy();
//        //                }
//        //                else throw new Exception("Export Excel: Neznámý typ pro přetypování");
//        //            }
//        //            else if (dgv.DataSource is System.Data.DataTable)
//        //            {
//        //                tCxC = (System.Data.DataTable)dgv.DataSource;
//        //            }
//        //            else throw new Exception("Export Excel: Neznámý typ pro přetypování");
//        //        }
//        //        else
//        //        {
//        //            System.Data.DataTable dtClone = new System.Data.DataTable();
//        //            if (dgv.DataSource is BindingSource)
//        //            {
//        //                BindingSource bs = (BindingSource)dgv.DataSource;

//        //                if(bs.DataSource is DataSet)
//        //                {
//        //                    tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
//        //                    dtClone = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
//        //                }
//        //                else if(bs.DataSource is System.Data.DataTable)
//        //                {
//        //                    tCxC = ((System.Data.DataTable)bs.DataSource).Clone();
//        //                    dtClone = ((System.Data.DataTable)bs.DataSource).Clone();
//        //                }
//        //                else throw new Exception("Export Excel: Neznámý typ pro přetypování");
//        //            }
//        //            else if (dgv.DataSource is System.Data.DataTable)
//        //            {
//        //                tCxC = (System.Data.DataTable)dgv.DataSource;
//        //                dtClone = (System.Data.DataTable)dgv.DataSource;
//        //            }
//        //            else throw new Exception("Export Excel: Neznámý typ pro přetypování");

//        //            foreach (DataGridViewRow row in dgv.Rows)
//        //            {
//        //                if (Convert.ToBoolean(dgv.Rows[row.Index].Cells["Vybrano"].Value))
//        //                    tCxC.ImportRow(((DataRowView)row.DataBoundItem).Row);
//        //            }
//        //            tCxC.AcceptChanges();
//        //        }


//        //        // kopie datatable, se kterym se bude pracovat
//        //        System.Data.DataTable dt2 = tCxC.Copy();

//        //        // odstraneni skrytych sloupcu
//        //        for (int i = 0; i < dgv.Columns.Count; i++)
//        //        {
//        //            if (!dgv.Columns[i].Visible)
//        //                dt2.Columns.Remove(dgv.Columns[i].DataPropertyName);
//        //        }

//        //        // zmena nazvu hlavicek na ty, ktere se zobrazuji
//        //        foreach (DataGridViewColumn item in dgv.Columns)
//        //        {
//        //            if (dt2.Columns.Contains(item.DataPropertyName))
//        //                dt2.Columns[item.DataPropertyName].ColumnName = item.HeaderText;
//        //        }

//        //        // naplneni excelu daty
//        //        ws.Cells["A1"].LoadFromDataTable(dt2, true);

//        //        // nastaveni formatu datumu
//        //        var dateColumns = from DataColumn d in dt2.Columns
//        //                          where d.DataType == typeof(DateTime)// || d.ColumnName.Contains("Date")
//        //                          select d.Ordinal + 1;
//        //        foreach (var dc in dateColumns)
//        //        {
//        //            ws.Cells[2, dc, dt2.Rows.Count + 2, dc].Style.Numberformat.Format = Settings.ExportyExcelFormatDatum;
//        //        }

//        //        // nastaveni automaticke velikosti sloupcu
//        //        ws.Cells.AutoFitColumns();

//        //        // ulozeni
//        //        pck.Save();
//        //    }

//        //    // spusteni v excelu
//        //    if (Settings.ExportyExcelOtevritPoVygenerovani)
//        //        System.Diagnostics.Process.Start(path);
//        //}

//        //#endregion

//        #region Export to Excel (Obsolete)
//        // 27.5.2016 PeV: pomale, je potreba mit nainstalovan excel
//        /// <summary>
//        /// Dojde k exportu veškerých zobrazených dat (skryté sloupce ne neexportují)
//        /// </summary>
//        /// <param name="dgv"></param>
//        /// <param name="fileName"></param>
//        //public static void ExportAllRowsVisibleColumnsToExcel(this DataGridView dgv, string fileName)
//        //{
//        //    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
//        //    //Workbook xlWorkbook = ExcelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
//        //    Workbook xlWorkbook = ExcelApp.Workbooks.Add(Type.Missing);


//        //    Sheets xlSheets = null;
//        //    Worksheet xlWorksheet = null;
//        //    //Create Excel Sheets
//        //    xlSheets = ExcelApp.Sheets;
//        //    xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
//        //                   Type.Missing, Type.Missing, Type.Missing);

//        //    // set cell format to text
//        //    //xlWorksheet.Cells.NumberFormat = "@";
//        //    // set cell format to General
//        //    xlWorksheet.Cells.NumberFormat = "General";

//        //    //Header
//        //    int columnNumberHeader = 1;
//        //    for (int k = 1; k < dgv.Columns.Count + 1; k++)
//        //    {
//        //        if (dgv.Columns[k - 1].Visible)
//        //        {
//        //            ExcelApp.Cells[1, columnNumberHeader] = dgv.Columns[k - 1].HeaderText;
//        //            columnNumberHeader++;
//        //        }
//        //    }

//        //    int rowNumber = 0;
//        //    int columnNumber = 0;

//        //    for (int i = 0; i < dgv.Rows.Count; i++)
//        //    {
//        //        for (int j = 0; j < dgv.Columns.Count; j++)
//        //        {
//        //            if (dgv.Columns[j].Visible)
//        //            {
//        //                ExcelApp.Range[ExcelApp.Cells[rowNumber + 2, columnNumber + 1], ExcelApp.Cells[rowNumber + 2, columnNumber + 1]].NumberFormat = datatype(dgv.Columns[j].ValueType);
//        //                if ((dgv.Columns[j].ValueType == Type.GetType("System.DateTime")) || (dgv.Columns[j].ValueType == Type.GetType("System.Guid")))
//        //                    ExcelApp.Cells[rowNumber + 2, columnNumber + 1] = dgv.Rows[i].Cells[j].Value.ToString();
//        //                else
//        //                    ExcelApp.Cells[rowNumber + 2, columnNumber + 1] = dgv.Rows[i].Cells[j].Value;
//        //                //ExcelApp.Cells[rowNumber + 2, columnNumber + 1] = (dgv.Columns[j].ValueType == Type.GetType("System.DateTime")) ? dgv.Rows[i].Cells[j].Value.ToString() : dgv.Rows[i].Cells[j].Value;
//        //                columnNumber++;
//        //            }
//        //        }
//        //        rowNumber++;
//        //        columnNumber = 0;

//        //    }
//        //    ExcelApp.Columns.AutoFit();

//        //    ExcelApp.Visible = true;

//        //    ExcelApp = null;
//        //    xlWorkbook = null;
//        //    GC.Collect();
//        //    GC.WaitForPendingFinalizers();
//        //}

//        // 27.5.2016 PeV: pomale, je potreba mit nainstalovan excel
//        /// <summary>
//        /// Dojde k exportu zvolených dat (skryté sloupce ne neexportují)
//        /// </summary>
//        /// <param name="dgv"></param>
//        /// <param name="fileName"></param>
//        //public static void ExportSelectedRowsVisibleColumnsToExcel(this DataGridView dgv, string fileName)
//        //{
//        //    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
//        //    Workbook xlWorkbook = ExcelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);

//        //    // Loop over DataTables in DataSet.
//        //    //DataTableCollection collection = this.ukolovaniDataset1.Tables;

//        //    //for (int i = collection.Count; i > 0; i--)
//        //    //{
//        //    Sheets xlSheets = null;
//        //    Worksheet xlWorksheet = null;
//        //    //Create Excel Sheets
//        //    xlSheets = ExcelApp.Sheets;
//        //    xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
//        //                   Type.Missing, Type.Missing, Type.Missing);

//        //    // set cell format to text
//        //    //xlWorksheet.Cells.NumberFormat = "@";
//        //    // set cell format to General
//        //    xlWorksheet.Cells.NumberFormat = "General";

//        //    //Header
//        //    int columnNumberHeader = 1;
//        //    for (int k = 1; k < dgv.Columns.Count + 1; k++)
//        //    {
//        //        if (dgv.Columns[k - 1].Visible)
//        //        {
//        //            ExcelApp.Cells[1, columnNumberHeader] = dgv.Columns[k - 1].HeaderText;
//        //            columnNumberHeader++;
//        //        }
//        //    }

//        //    int rowNumber = 0;
//        //    int columnNumber = 0;

//        //    for (int i = 0; i < dgv.Rows.Count; i++)
//        //    {
//        //        if (dgv.Rows[i].Selected)
//        //        {
//        //            for (int j = 0; j < dgv.Columns.Count; j++)
//        //            {
//        //                if (dgv.Columns[j].Visible)
//        //                {
//        //                    ExcelApp.Range[ExcelApp.Cells[rowNumber + 2, columnNumber + 1], ExcelApp.Cells[rowNumber + 2, columnNumber + 1]].NumberFormat = datatype(dgv.Columns[j].ValueType);
//        //                    if((dgv.Columns[j].ValueType == Type.GetType("System.DateTime")) || (dgv.Columns[j].ValueType == Type.GetType("System.Guid")))
//        //                        ExcelApp.Cells[rowNumber + 2, columnNumber + 1] = dgv.Rows[i].Cells[j].Value.ToString();
//        //                    else
//        //                        ExcelApp.Cells[rowNumber + 2, columnNumber + 1] = dgv.Rows[i].Cells[j].Value;
//        //                    //ExcelApp.Cells[rowNumber + 2, columnNumber + 1] = (dgv.Columns[j].ValueType == Type.GetType("System.DateTime")) ? dgv.Rows[i].Cells[j].Value.ToString() : dgv.Rows[i].Cells[j].Value;

//        //                    columnNumber++;

//        //                }
//        //            }
//        //            rowNumber++;
//        //            columnNumber = 0;
//        //        }
//        //    }
//        //    ExcelApp.Columns.AutoFit();

//        //    //((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
//        //    ExcelApp.Visible = true;

//        //    ExcelApp = null;
//        //    xlWorkbook = null;
//        //    GC.Collect();
//        //    GC.WaitForPendingFinalizers();
//        //}

//        //public static void ExportSelectedRowsVisibleColumnsToExcelInventura(this DataGridView dgv, string fileName)
//        //{
//        //    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
//        //    Workbook xlWorkbook = ExcelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);

//        //    // Loop over DataTables in DataSet.
//        //    //DataTableCollection collection = this.ukolovaniDataset1.Tables;

//        //    //for (int i = collection.Count; i > 0; i--)
//        //    //{
//        //    Sheets xlSheets = null;
//        //    Worksheet xlWorksheet = null;
//        //    //Create Excel Sheets
//        //    xlSheets = ExcelApp.Sheets;
//        //    xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
//        //                   Type.Missing, Type.Missing, Type.Missing);

//        //    // set cell format to text
//        //    //xlWorksheet.Cells.NumberFormat = "@";
//        //    // set cell format to General
//        //    xlWorksheet.Cells.NumberFormat = "General";

//        //    //Header
//        //    int columnNumberHeader = 1;
//        //    for (int k = 1; k < dgv.Columns.Count + 1; k++)
//        //    {
//        //        if (dgv.Columns[k - 1].Visible)
//        //        {
//        //            ExcelApp.Cells[1, columnNumberHeader] = dgv.Columns[k - 1].HeaderText;
//        //            columnNumberHeader++;
//        //        }
//        //    }

//        //    int rowNumber = 0;
//        //    int columnNumber = 0;

//        //    for (int i = 0; i < dgv.Rows.Count; i++)
//        //    {
//        //        //if (dgv.Rows[i].Selected)
//        //        if (Convert.ToBoolean(dgv.Rows[i].Cells["Vybrano"].Value))
//        //        {
//        //            for (int j = 0; j < dgv.Columns.Count; j++)
//        //            {
//        //                if (dgv.Columns[j].Visible)
//        //                {
//        //                    ExcelApp.Range[ExcelApp.Cells[rowNumber + 2, columnNumber + 1], ExcelApp.Cells[rowNumber + 2, columnNumber + 1]].NumberFormat = datatype(dgv.Columns[j].ValueType);
//        //                    if ((dgv.Columns[j].ValueType == Type.GetType("System.DateTime")) || (dgv.Columns[j].ValueType == Type.GetType("System.Guid")))
//        //                        ExcelApp.Cells[rowNumber + 2, columnNumber + 1] = dgv.Rows[i].Cells[j].Value.ToString();
//        //                    else
//        //                        ExcelApp.Cells[rowNumber + 2, columnNumber + 1] = dgv.Rows[i].Cells[j].Value;
//        //                    //ExcelApp.Cells[rowNumber + 2, columnNumber + 1] = (dgv.Columns[j].ValueType == Type.GetType("System.DateTime")) ? dgv.Rows[i].Cells[j].Value.ToString() : dgv.Rows[i].Cells[j].Value;

//        //                    columnNumber++;

//        //                }
//        //            }
//        //            rowNumber++;
//        //            columnNumber = 0;
//        //        }
//        //    }
//        //    ExcelApp.Columns.AutoFit();

//        //    //((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
//        //    ExcelApp.Visible = true;

//        //    ExcelApp = null;
//        //    xlWorkbook = null;
//        //    GC.Collect();
//        //    GC.WaitForPendingFinalizers();
//        //}

//        /// <summary>
//        /// Vrací formát textu (číslo, text, ...)
//        /// </summary>
//        /// <param name="columntype"></param>
//        /// <returns></returns>
//        //private static string datatype(Type columntype)
//        //{
//        //    if (columntype == Type.GetType("System.Decimal"))
//        //    {
//        //        return "0,00";
//        //    }
//        //    else if (columntype == Type.GetType("System.DateTime"))
//        //    {
//        //        //return "hh:mm:ss DD MM YYYY";
//        //        return "@";
//        //    }
//        //    else if (columntype == Type.GetType("System.Single"))
//        //    {
//        //        return "@";
//        //    }//System.Guid
//        //    else if (columntype == Type.GetType("System.Guid"))
//        //    {
//        //        return "@";
//        //    }
//        //    else // zbytek string
//        //    {
//        //        return "@";
//        //    }
//        //}
//        #endregion

//    }

//    #region DataGridViewColumnSelector (skryti a preusporadani sloupcu)
//    class DataGridViewColumnSelector
//    {
//        // the DataGridView to which the DataGridViewColumnSelector is attached
//        private DataGridView mDataGridView = null;
//        // a CheckedListBox containing the column header text and checkboxes
//        private CheckedListBox mCheckedListBox;
//        // a ToolStripDropDown object used to show the popup
//        private ToolStripDropDown mPopup;

//        /// <summary>
//        /// The max height of the popup
//        /// </summary>
//        public int MaxHeight = 300;
//        /// <summary>
//        /// The width of the popup
//        /// </summary>
//        public int Width = 200;

//        /// <summary>
//        /// Gets or sets the DataGridView to which the DataGridViewColumnSelector is attached
//        /// </summary>
//        public DataGridView DataGridView
//        {
//            get { return mDataGridView; }
//            set
//            {
//                // If any, remove handler from current DataGridView 
//                if (mDataGridView != null) mDataGridView.CellMouseClick -= new DataGridViewCellMouseEventHandler(mDataGridView_CellMouseClick);
//                // Set the new DataGridView
//                mDataGridView = value;
//                // Attach CellMouseClick handler to DataGridView
//                if (mDataGridView != null) mDataGridView.CellMouseClick += new DataGridViewCellMouseEventHandler(mDataGridView_CellMouseClick);
//            }
//        }

//        // When user right-clicks the cell origin, it clears and fill the CheckedListBox with
//        // columns header text. Then it shows the popup. 
//        // In this way the CheckedListBox items are always refreshed to reflect changes occurred in 
//        // DataGridView columns (column additions or name changes and so on).
//        void mDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
//        {
//            //if (e.Button == MouseButtons.Right && e.RowIndex == -1 && e.ColumnIndex == -1)
//            if ((e.Button == MouseButtons.Right && e.RowIndex == -1))
//            {
//                mCheckedListBox.Items.Clear();
//                foreach (DataGridViewColumn c in mDataGridView.Columns)
//                {
//                    mCheckedListBox.Items.Add(c.HeaderText, c.Visible);
//                }
//                int PreferredHeight = (mCheckedListBox.Items.Count * 16) + 7;
//                mCheckedListBox.Height = (PreferredHeight < MaxHeight) ? PreferredHeight : MaxHeight;
//                mCheckedListBox.Width = this.Width;
//                mPopup.Show(mDataGridView.PointToScreen(new System.Drawing.Point(e.X, e.Y)));
//            }
//        }

//        // The constructor creates an instance of CheckedListBox and ToolStripDropDown.
//        // the CheckedListBox is hosted by ToolStripControlHost, which in turn is
//        // added to ToolStripDropDown.
//        public DataGridViewColumnSelector()
//        {
//            mCheckedListBox = new CheckedListBox();
//            mCheckedListBox.CheckOnClick = true;
//            mCheckedListBox.ItemCheck += new ItemCheckEventHandler(mCheckedListBox_ItemCheck);

//            ToolStripControlHost mControlHost = new ToolStripControlHost(mCheckedListBox);
//            mControlHost.Padding = Padding.Empty;
//            mControlHost.Margin = Padding.Empty;
//            mControlHost.AutoSize = false;

//            mPopup = new ToolStripDropDown();
//            mPopup.Padding = Padding.Empty;
//            mPopup.Items.Add(mControlHost);
//        }

//        public DataGridViewColumnSelector(DataGridView dgv)
//            : this()
//        {
//            this.DataGridView = dgv;
//        }

//        // When user checks / unchecks a checkbox, the related column visibility is 
//        // switched.
//        void mCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
//        {
//            // uprava, aby byl minimalne jeden sloupec videt (kvuli moznosti zobrazeni ostatnich dalsich sloupcu)
//            int checkedCount = mCheckedListBox.CheckedItems.Count;
//            if (e.NewValue == CheckState.Unchecked)
//                --checkedCount;

//            if (checkedCount == 0)
//                e.NewValue = CheckState.Checked;
//            else
//                mDataGridView.Columns[e.Index].Visible = (e.NewValue == CheckState.Checked);
//        }
//    }
//    #endregion
//}
