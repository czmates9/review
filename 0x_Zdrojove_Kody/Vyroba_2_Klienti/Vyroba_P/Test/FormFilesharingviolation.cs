using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fask.Vyroba_P.Test
{
    public partial class FormFilesharingviolation : Form
    {
        public static string filename = string.Empty;

        public FormFilesharingviolation()
        {
            InitializeComponent();
        }

        System.Threading.Thread t0;
        System.Threading.Thread t1;
        System.Threading.Thread t2;

        private void button1_Click(object sender, EventArgs e)
        {
            filename = textBox1.Text;

            t0 = new System.Threading.Thread(new System.Threading.ThreadStart(tstart0));
            t1 = new System.Threading.Thread(new System.Threading.ThreadStart(tstart1));
            t2 = new System.Threading.Thread(new System.Threading.ThreadStart(tstart2));

            t0.Start();
            t1.Start();
            t2.Start();
        }

        private void tstart0()
        {
            updatetextbox(gettimestring() + " t0 start");
            System.Threading.Thread.Sleep(2000);
            updatetextbox(gettimestring() + " t0 end");
        }

        private void tstart1()
        {
            try
            {
                t0.Join();
                updatetextbox(gettimestring() + " t1 - after t0.join");
                updatetextbox(gettimestring() + " t1 - file copy start");
                File.Copy(filename, filename + Constants.TMP, true);
                updatetextbox(gettimestring() + " t1 - file copy end");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                updatetextbox(" t1 - " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private int xmilis = 1;
        private void tstart2()
        {
            try
            {
                t0.Join();
                System.Threading.Thread.Sleep(xmilis);
                updatetextbox(gettimestring() + " t2 - after t0.join");
                Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable ds = new Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable();

                using (SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyr = new SQLiteDBs.Controllers.SQLite_Controller_Vyroba(filename))
                {
                    updatetextbox(gettimestring() + " t2 - dataset fill start");
                    ConVyr.Fill_CZPRO_VPH(ds);
                    updatetextbox(gettimestring() + " t2 - dataset fill end");
                }


                //System.Data.SqlServerCe.SqlCeDataAdapter sda = new System.Data.SqlServerCe.SqlCeDataAdapter();
                //sda.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
                //    "select * from czpro_vph", 
                //    new System.Data.SqlServerCe.SqlCeConnection(
                //        "Data source=" + filename
                //        ));
                //DataSet ds = new DataSet();
                //updatetextbox(gettimestring() + " t2 - dataset connection open");
                //sda.SelectCommand.Connection.Open();
                //updatetextbox(gettimestring() + " t2 - dataset fill start");
                //sda.Fill(ds);
                //updatetextbox(gettimestring() + " t2 - dataset fill end");
                //updatetextbox(gettimestring() + " t2 - dataset connection close");
                //sda.SelectCommand.Connection.Close();
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                updatetextbox(" t2 - " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private static string gettimestring()
        {
            return DateTime.Now.ToString("o");
        }

        private void updatetextbox(string text)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate() { updatetextbox(text); });
                return;
            }

            this.listBox1.Items.Add(text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

    }
}
