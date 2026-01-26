using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MSTW_Update
{
    public partial class Notification : Form
    {
        public Notification()
        {
            InitializeComponent();

            if (!(Program.commandLineArguments.Length <= 0))
            {

                this.Version_New = Program.commandLineArguments[0];
                this.Version_Old = Program.commandLineArguments[1];
                this.AppPath = Program.commandLineArguments[2];

                String updateFilePath = Path.Combine(this._appPath, this._nameXmlFile);

                MSTW_Update.DataSet.UpdateInfo ds = new MSTW_Update.DataSet.UpdateInfo();
                ds.ReadXml(updateFilePath);

                //mnelo by vzdy najit jenom jeden radek, ten ktery byl vybran na ctecke
                var rows = ds.UpdateData.Where(x => x.Version == this._version_new);

                MSTW_Update.DataSet.UpdateInfo.UpdateDataRow row = rows.First();

                //nacteni informaci do Label a Textbox pro uzivatele
                this.appname_label.Text = row.Name;
                this.version_label.Text = row.Version;
                this.message_textbox.Text = row.Message;
                this.ZipFileURL = row.Link;

                this.ActualVersion = new Version(this._version_old);
                this.NewVersion = new Version(this._version_new);

                #region Farebne porovnanie verzi a uprava textu

                if (this.NewVersion > this.ActualVersion)
                {
                    this.appname_label.ForeColor = Color.Green;
                    this.version_label.ForeColor = Color.Green;
                    this.label2.Text = this._text_NewVersion;
                }
                else if (this.NewVersion < this.ActualVersion)
                {
                    this.appname_label.ForeColor = Color.Red;
                    this.version_label.ForeColor = Color.Red;
                    this.label2.Text = this.Text_OldVersion;
                }
                else
                {
                    this.appname_label.ForeColor = Color.Black;
                    this.version_label.ForeColor = Color.Black;
                    this.label2.Text = this.Text_SameVersion;
                }

                #endregion

                //Sluzi pro prenašeni hodnoty staženi do progressbaru pomoci delegata(asi)
                trans = new TransferManager.TransferProgress(UpdateProgressBar);

                //Done event po uspesnem update
                this.UpdateDoneEvent += new UpdateDone(updater_UpdateDoneEvent);

                //error event pokud nastane nejaka chyba
                this.UpdateErrorEvent += new UpdateError(updater_UpdateErrorEvent);

                //start update event, nastartuje sa tym cely update
                this.StartUpdateEvent += new StartUpdate(startUpdate);



            }
            else
            {
                label1.Text = "DEMO VERZE";
            }
        }

        private void btn_Install_Click(object sender, EventArgs e)
        {
            try
            {
                if (label1.Text != "DEMO VERZE")
                {
                    if (StartUpdateEvent != null)
                    {
                        Button button = (Button)sender;
                        button.Visible = false;
                        button.Dock = DockStyle.None;
                        progressBar1.Dock = DockStyle.Top;
                        progressBar1.Visible = true;
                        StartUpdateEvent();
                        this._updateStarted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            if (label1.Text != "DEMO VERZE")
            {
                if (this._updateStarted)
                {
                    if (AbortUpdateEvent != null)
                        AbortUpdateEvent();
                }
            }

            //?? Co delat??
            //this.DialogResult = DialogResult.No;

            this.Close();
        }


    }
}
