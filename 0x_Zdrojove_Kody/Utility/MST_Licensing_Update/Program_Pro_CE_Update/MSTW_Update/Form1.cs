using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MSTW_Update
{
    public partial class Form1 : Form
    {
        private MSTW_Update.Updater updater;
        string cfgTerminalServerAddress = "http://192.168.1.101/MST_Win_Kom_Server_6_alfa/";
        private string zipFileURL;
        private Notification notification;
        private volatile bool abortUpdate = false;
        
        private String appPath;
        private string Version_Old;
        private string Version_New;


        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            if (Program.commandLineArguments.Length <= 0)
                return;

            Version_New = Program.commandLineArguments[0];
            Version_Old = Program.commandLineArguments[1];
            appPath = Program.commandLineArguments[2];


            lbl_path.Text = appPath;
            lbl_VerzeNew.Text = Version_New;
            lbl_VerzeOld.Text = Version_Old;

            /*

            String updateFilePath = Path.Combine(lbl_path.Text, "update.xml");


            // TODO Dataset načteni z xml do datasetu
            //updater = new MSTW_Update.Updater(cfgTerminalServerAddress + "upgrade/update.xml", updateFilePath);
            

            MSTW_Update.DataSet.UpdateInfo ds = new MSTW_Update.DataSet.UpdateInfo();

            ds.ReadXml(updateFilePath);

            var rows = ds.UpdateData.Where(x => x.Version == lbl_Verze.Text);

            MSTW_Update.DataSet.UpdateInfo.UpdateDataRow row  = rows.First();

                            this.zipFileURL = row.Link;

                using (notification = new Notification(row.Name, row.Message, row.Version, ver, this))
                {
                    notification.AbortUpdateEvent += new Notification.AbortUpdate(notification_AbortUpdateEvent);
                    notification.StartUpdateEvent += new Notification.StartUpdate(startUpdate);
                    DialogResult res = notification.ShowDialog();

                    if (res == DialogResult.Yes)
                    {
                        string backupDir = appPath + "\\" + BACKUP_FOLDER_NAME;
                        File.Create(backupDir + "\\" + "success");
                        //return res;
                    }
                    else
                    {
                        //return res;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            
            */

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void notification_AbortUpdateEvent()
        {
            this.abortUpdate = true;
        }

        private void startUpdate()
        {
            try
            {
                //message_from_thread = null;
                //t = new Thread(new ThreadStart(performUpdate));
                //t.Start();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}