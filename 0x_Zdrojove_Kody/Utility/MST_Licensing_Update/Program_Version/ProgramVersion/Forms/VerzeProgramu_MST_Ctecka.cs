using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace ProgramVersion
{
    public partial class VerzeProgramu_MST_Ctecka : Form
    {
        /// <summary>
        /// cesta s nactenemu souboru
        /// </summary>
        private string Path_actual;

        /// <summary>
        ///Kod pro vyber jednoho radku z datagridview a pak pretipovanz na jeden radek daneho datatable
        /// </summary>
        public ProgramVersion.DataSet.UpdateInfo.UpdateDataRow rowInfo
        {
            get
            {
                try
                {
                    return ((DataRowView)(dg_Info.BindingContext[bs_Info].Current)).Row as ProgramVersion.DataSet.UpdateInfo.UpdateDataRow;
                }
                catch
                {
                    return null;
                }
            }
        }


        public VerzeProgramu_MST_Ctecka()
        {
            InitializeComponent();
        }

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void OpenFile()
        {
            try
            {
                Stream myStream = null;

                using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
                {
                    string path = Environment.GetCommandLineArgs()[0];
                    string absolutepath = Path.GetDirectoryName(path);

                    openFileDialog1.InitialDirectory = absolutepath;
                    openFileDialog1.Filter = "xml files (*.xml)|*.xml";
                    openFileDialog1.FilterIndex = 1;
                    openFileDialog1.RestoreDirectory = true;

                    if ((openFileDialog1.ShowDialog() == DialogResult.OK))
                    {
                         Path_actual = openFileDialog1.FileName;

                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                ParseXML(myStream);
                            }
                        }
                    }
                }

                
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ParseXML(Stream stream)
        {
            try
            {
                ds_Info.Clear();
                ds_Info.ReadXml(stream);

                #region puvodne parsovani XML z original kodu pro update
                //XmlDocument xDoc = new XmlDocument();
                //xDoc.Load(stream);


                //XmlNodeList modules = xDoc.GetElementsByTagName("download");

                //ds_Info.Clear();

                //for (int i = 0; i <= modules.Count - 1; i++)
                //{
                //    ProgramVersion.DataSet.UpdateInfo.UpdateDataRow row = ds_Info.UpdateData.NewUpdateDataRow();

                //    row.Name = modules[i].Attributes["name"].Value;

                //    XmlNodeList childNodes = modules[i].ChildNodes;

                //    string major = childNodes.Item(0).Attributes["major"].Value;
                //    string minor = childNodes.Item(0).Attributes["minor"].Value;
                //    string build = childNodes.Item(0).Attributes["build"].Value;
                //    string revision = childNodes.Item(0).Attributes["revision"].Value;

                //    row.Version = major.Trim() + "." + minor.Trim() + "." + build.Trim() + "." + revision.Trim();

                //    row.Message = childNodes.Item(1).InnerText;

                //    row.Link = childNodes.Item(2).InnerText;


                //    ds_Info.UpdateData.AddUpdateDataRow(row);
                //}

                //ds_Info.UpdateData.AcceptChanges();
                #endregion
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void dg_Info_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (rowInfo != null)
                {
                    NactiInfo(rowInfo);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NactiInfo( ProgramVersion.DataSet.UpdateInfo.UpdateDataRow rowInfo)
        {
            //Načteni Informaci

            try
            {
                textBox_Name.Text = rowInfo.Name;
                textBox_Message.Text = rowInfo.Message;
                textBox_URL.Text = rowInfo.Link;


                var array_string = rowInfo.Version.Split('.');

                textBox_major.Text = array_string[0];
                textBox_minor.Text = array_string[1];
                textBox_build.Text = array_string[2];
                textBox_rev.Text = array_string[3];
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private void Open_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void Save_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ds_Info.WriteXml(Path_actual);

                
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Shift && !e.Control && !e.Alt)
                {
                    //if (e.KeyCode == Keys.Escape)
                    //{

                    //}
                    if (e.KeyCode == Keys.Enter)
                    {
                        //Change();
                    }
                    else
                        return;
                }
                else
                    return;

                e.Handled = true;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void change_button_Click(object sender, EventArgs e)
        {
           Change();
        }

        private void Change()
        {

            if (!ValidateData())
                return; 

            rowInfo.Name = textBox_Name.Text;
            rowInfo.Message = string.IsNullOrEmpty(textBox_Message.Text) ? String.Empty : textBox_Message.Text;
            rowInfo.Link = textBox_URL.Text;
            rowInfo.Version = textBox_major.Text + "." + textBox_minor.Text + "." + textBox_build.Text + "." + textBox_rev.Text;

            rowInfo.AcceptChanges();
            rowInfo.SetModified();

        }

        private void Add_button_Click(object sender, EventArgs e)
        {

            try
            {
                if (!ValidateData())
                    return;


                ProgramVersion.DataSet.UpdateInfo.UpdateDataRow row = ds_Info.UpdateData.NewUpdateDataRow();

                row.Name = textBox_Name.Text;
                row.Message = string.IsNullOrEmpty(textBox_Message.Text) ? String.Empty : textBox_Message.Text;
                row.Link = textBox_URL.Text;
                row.Version = textBox_major.Text + "." + textBox_minor.Text + "." + textBox_build.Text + "." + textBox_rev.Text;


                ds_Info.UpdateData.AddUpdateDataRow(row);



            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Validace dat
        /// <summary>
        /// Validace vyplnenich dat
        /// </summary>
        /// <returns></returns>

        private bool ValidateData()
        {
            try
            {
                errorProvider1.Clear();

                if (!string.IsNullOrEmpty(textBox_build.Text))
                {
                    int build;
                    if (!int.TryParse(textBox_build.Text, out build))
                        errorProvider1.SetError(textBox_build, "Build musí být celé číslo");
                }
                else
                    errorProvider1.SetError(textBox_build, "Build musí být vyplněn");


                if (!string.IsNullOrEmpty(textBox_major.Text))
                {
                    int major;
                    if (!int.TryParse(textBox_major.Text, out major))
                        errorProvider1.SetError(textBox_major, "Majoritná musí být celé číslo");
                }
                else
                    errorProvider1.SetError(textBox_major, "Majoritná musí být vyplněn");


                if (!string.IsNullOrEmpty(textBox_minor.Text))
                {
                    int min;
                    if (!int.TryParse(textBox_minor.Text, out min))
                        errorProvider1.SetError(textBox_minor, "Minoritná musí být celé číslo");
                }
                else
                    errorProvider1.SetError(textBox_minor, "Minortiná musí být vyplněn");


                if (!string.IsNullOrEmpty(textBox_rev.Text))
                {
                    int rev;
                    if (!int.TryParse(textBox_rev.Text, out rev))
                        errorProvider1.SetError(textBox_rev, "Revize musí být celé číslo");
                }
                else
                    errorProvider1.SetError(textBox_rev, "Revize musí být vyplněn");


                 if (string.IsNullOrEmpty(textBox_URL.Text))
                     errorProvider1.SetError(textBox_URL, "Cesta k souboru musí být vyplněna");

                if (string.IsNullOrEmpty(textBox_Name.Text))
                    errorProvider1.SetError(textBox_Name, "Název Programu musí být vyplněn");

                
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            return IsAllValid();
        }

        private bool IsAllValid()
        {
            //foreach (Control c in errorProvider1.ContainerControl.Controls)
            foreach (Control c in groupBox1.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;
            return true;
        }

        #endregion


        private void Delete_button_Click(object sender, EventArgs e)
        {
            if (rowInfo != null)
            {
                rowInfo.Delete();
            }
            
        }

        private void SaveAs_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {

                        ds_Info.WriteXml(myStream);
                        //ds_Info.WriteXmlSchema(myStream);

                        // Code to write the stream goes here.
                        myStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearedit_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox_Name.Text = String.Empty;
            textBox_Message.Text = String.Empty;
            textBox_URL.Text = String.Empty;
            textBox_major.Text = String.Empty;
            textBox_minor.Text = String.Empty;
            textBox_build.Text = String.Empty;
            textBox_rev.Text = String.Empty;
        }

        private void clear_all_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearedit_toolStripMenuItem_Click(null, null);

            ds_Info.Clear();
            Path_actual = String.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo_FASK1;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (about a = new about()) 
            {
                a.ShowDialog();
            }
        }


        #region DataSet untype test load schema



//System.Data.DataSet ds = new System.Data.DataSet();

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    #region Test Upload data to server

            //    ////Console.Write("\nPlease enter the URI to post data to : ");
            //    ////String uriString = Console.ReadLine();
            //    //String uriString = "http://localhost/MST_Win_Kom_Server_6_alfa/Upload.aspx";
            //    ////String uriString = "http://localhost/MST_Win_Kom_Server_6_alfa/SQLCeDBs/1/A.zip";


            //    //// Create a new WebClient instance.
            //    //System.Net.WebClient myWebClient = new System.Net.WebClient();

            //    ////Console.WriteLine("\nPlease enter the fully qualified path of the file to be uploaded to the URI");
            //    ////string fileName = Console.ReadLine();
            //    ////Console.WriteLine("Uploading {0} to {1} ...", fileName, uriString);
            //    //string fileName = "http://localhost/MST_Win_Kom_Server_4.01_Perlacasa/SqlCEDBs/Download/A.zip";


            //    //// Upload the file to the URI.
            //    //// The 'UploadFile(uriString,fileName)' method implicitly uses HTTP POST method.
            //    //byte[] responseArray = myWebClient.UploadFile(uriString, fileName);


            //    //// Decode and display the response.
            //    ////System.Diagnostics.Debug.WriteLine("Odpověď přijatá. Obsah nahraného souboru je:\n{0}",
            //    ////    System.Text.Encoding.ASCII.GetString(responseArray));

            //    #endregion


            //    #region Nacteni xml souboru test
            //    //ProgramVersion.testy_datasety.AAAdataSetAAATableAdapters.CZMST_I1TableAdapter ta = new testy_datasety.AAAdataSetAAATableAdapters.CZMST_I1TableAdapter();

            //    //ProgramVersion.testy_datasety.AAADataSetAAA.AAAdataSetAAA.CZMST_I1DataTable dataTable = new testy_datasety.AAADataSetAAA.AAAdataSetAAA.CZMST_I1DataTable();

            //    //ta.Fill(dataTable);

            //    //;

            //    //mydataset.MyDataTable.AddMyDataTableRow();

            //    //Stream myStream = null;

            //    //using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            //    //{
            //    //    string path = Environment.GetCommandLineArgs()[0];
            //    //    string absolutepath = Path.GetDirectoryName(path);

            //    //    openFileDialog1.InitialDirectory = absolutepath;
            //    //    openFileDialog1.Filter = "xml files (*.xml)|*.xml";
            //    //    openFileDialog1.FilterIndex = 1;
            //    //    openFileDialog1.RestoreDirectory = true;

            //    //    if ((openFileDialog1.ShowDialog() == DialogResult.OK))
            //    //    {
            //    //        Path_actual = openFileDialog1.FileName;

            //    //        if ((myStream = openFileDialog1.OpenFile()) != null)
            //    //        {
            //    //            using (myStream)
            //    //            {
            //    //                ds.ReadXmlSchema(myStream);
            //    //            }
            //    //        }
            //    //    }
            //    //}
            //    #endregion

            //}
            //catch (Exception ex)
            //{
            //    Log.Write(ex);
            //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }
        #endregion

        private DateTime? Start;
        private DateTime? Stop;

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Start = null;
            this.Stop = null;
            this.Start = DateTime.Now;

        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Stop = DateTime.Now;
            textBox_Message.Text = String.Empty;
           textBox_Message.Text = Write(this.Start, this.Stop);
        }

        public static string Write(DateTime? Start, DateTime? Stop)
        {
            if (Start == null || Stop == null)
                return "-1";

            TimeSpan time;
            time = (DateTime)Stop - (DateTime)Start;

            //int miliseconds = ((int)((time.TotalSeconds - (double)time.Seconds) * 10000000));

            return String.Format("{0}:{1}:{2}", time.Hours, time.Minutes, time.TotalSeconds);



        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String uriString = "http://localhost/MST_Win_Kom_Server_4.01_Perlacasa/Upload.aspx";
            String odkud = @"D:\_work_\_Vyvoj_Subversion_01\MST_06_alfa\SERVER\MST_Win_Kom_Server\SqlCEDBs\1\A.zip";
            String kam = "http://localhost/MST_Win_Kom_Server_4.01_Perlacasa/SqlCEDBs/Download/A.zip";

            Upload.Uploading.SendFile(uriString, odkud, kam);




        }

    }
}
