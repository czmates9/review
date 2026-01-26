using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Vydej_3
{
    public partial class Detail : System.Windows.Forms.Form
    {
        private string sopnumber = "";
        private DataSet data = null;
        private string message = string.Empty;
        private int maxLength = 0;
        private int maxLengthColumn = 0;
        public Detail(string sopnumber)
        {
            InitializeComponent();

            this.sopnumber = sopnumber;
        }

        private void Detail_Load(object sender, EventArgs e)
        {
            this.Size = Forms.FormLocation.ScreenResolution;
            try
            {
                Vydej_3.Vydej.vydejInstance.globalObject.service_vydej.BeginDetail(sopnumber.Trim(), new AsyncCallback(VydejkaDetail), null);
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
            }
        }

        private void VydejkaDetail(IAsyncResult ares)
        {
            try
            {
                data = Vydej_3.Vydej.vydejInstance.globalObject.service_vydej.EndDetail(ares);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                try
                {
                    this.BeginInvoke((Action)delegate() { this.Invalidate(); });
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
                    if (Vydej_3.Vydej.vydejInstance.globalObject.service_vydej != null)
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
                    panelDetail.Visible = true;
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
                            if (maxLengthColumn < dr.Table.Columns[i].ToString().Trim().Length)
                            {
                                maxLengthColumn = dr.Table.Columns[i].ToString().Trim().Length;
                                labelDetail.Left = 7 * maxLengthColumn + (maxLengthColumn / 2);
                                labelDetailColumn.Width = 7 * dr.Table.Columns[i].ToString().Trim().Length + (dr.Table.Columns[i].ToString().Trim().Length / 2);
                            }
                            if (maxLength < 8 * dr[i].ToString().Length) //7*(dr.Table.Columns[i].ToString().Length + dr[i].ToString().Length + 3))
                            {
                                labelDetail.Width = 7 * (dr[i].ToString().Trim().Length) + (dr[i].ToString().Trim().Length / 2);
                                maxLength = labelDetail.Width;
                            }

                            //labelDetailColumn.Text = labelDetailColumn.Text + dr.Table.Columns[i].ToString().Trim() + "\n";
                            //labelDetail.Text = labelDetail.Text + ": " + dr[i].ToString().Trim() + "\n";
                            labelDetailColumn.Text = labelDetailColumn.Text + dr.Table.Columns[i].ToString().Replace(System.Environment.NewLine, "").Trim() + "\n";
                            labelDetail.Text = labelDetail.Text + ": " + dr[i].ToString().Replace(System.Environment.NewLine, "").Trim() + "\n";
                            labelDetail.Height = labelDetail.Height + 15;
                            labelDetailColumn.Height = labelDetailColumn.Height + 15;
                            // labelDetail.Text = labelDetail.Text+ labelDetail.Height.ToString() + "\n";
                            // e.Graphics.DrawString(dr.Table.Columns[i].Caption, fnt, solid, new RectangleF(x, y, maxsize.Width, maxsize.Height)); 
                            //e.Graphics.DrawString(" : " + dr[i].ToString(), this.Font, solid, x + maxsize.Width, y);
                            //y += 20;
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
                panelDetail.Visible = false;
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
            if (Vydej_3.Vydej.vydejInstance.globalObject.service_vydej != null)
            {
                try
                {
                    Vydej_3.Vydej.vydejInstance.globalObject.service_vydej.Abort();
                }
                catch
                {
                }
            }
        }
    }
}