using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Prijem_4
{
    public partial class DetailItem : System.Windows.Forms.Form
    {
        private string ponumber = "";
        private string sklid = "";
        private string itemnumber = "";
        private string itemorder = "";
        
        private DataSet data = null;
        private delegate void DelegateNoParam();
        private string message = string.Empty;

        public DetailItem(string ponumber, string sklid, string itemnumber, string itemorder)
        {
            InitializeComponent();

            this.ponumber = ponumber;
            this.sklid = sklid;
            this.itemnumber = itemnumber;
            this.itemorder = itemorder;
        }

        private void Detail_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.Size = Forms.FormLocation.ScreenResolution;
            try
            {
                //prijemservice.BeginDetailItem(ponumber.Trim(), sklid.Trim(), itemnumber.Trim(), itemorder.Trim(), string.Empty, new AsyncCallback(PrijemkaDetailItem), null);
                Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.BeginDetailItem(ponumber.Trim(), sklid.Trim(), itemnumber.Trim(), itemorder.Trim(), string.Empty, new AsyncCallback(PrijemkaDetailItem), null);
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
            }
        }

        private void PrijemkaDetailItem(IAsyncResult ares)
        {
            try
            {
                data = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.EndDetailItem(ares);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                try
                {
                    this.BeginInvoke(new DelegateNoParam(this.Invalidate));
                }
                catch { }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            SolidBrush solid = new SolidBrush(Color.Black);

            try
            {
                if (data == null)
                {
                    if (Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem != null)
                    {
                        if (message == string.Empty)
                            message = "Loading data ...";
                        throw new Exception();
                    }
                    else
                    {
                        if (message == string.Empty)
                            message = "No data";
                        else
                            message += "\n" + "No data";
                        throw new Exception();
                    }
                }

                Font fnt = new Font(this.Font.Name, this.Font.Size, FontStyle.Bold);
                SizeF maxsize = SizeF.Empty;
                float x, y;
                x = 10; y = 10;
                if (data.Tables.Count==0 || (data.Tables.Count > 0 && data.Tables[0].Rows.Count == 0))
                {
                    e.Graphics.DrawString("Žádná data k dispozici", fnt, solid, x, y);
                }
                else
                {
                    foreach (DataRow dr in data.Tables[0].Rows)
                    {
                        for (int i = 0; i < dr.Table.Columns.Count; i++)
                        {
                            SizeF size = e.Graphics.MeasureString(dr.Table.Columns[i].Caption, fnt);
                            if (size.Width > maxsize.Width)
                                maxsize = size;
                        }

                        for (int i = 0; i < dr.Table.Columns.Count; i++)
                        {
                            e.Graphics.DrawString(dr.Table.Columns[i].Caption, fnt, solid, new RectangleF(x, y, maxsize.Width, maxsize.Height));
                            e.Graphics.DrawString(" : " + dr[i].ToString(), this.Font, solid, x + maxsize.Width, y);
                            y += 20;
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
                e.Graphics.DrawString(message, this.Font, solid, 10, 10);
            }
        }

        private void Detail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else
            {
                e.Handled = false;
                return;
            }

            e.Handled = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void PerformOK()
        {
            DialogResult = DialogResult.OK;
        }

        private void Detail_Closing(object sender, CancelEventArgs e)
        {
            //if (this.prijemservice != null)
            //{
            //    try
            //    {
            //        this.prijemservice.Abort();
            //    }
            //    catch
            //    {
            //    }
            //}
        }
    }
}