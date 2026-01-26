using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Informations
{
    public partial class OnLineItemumberGrid : System.Windows.Forms.Form
    {
        private delegate void DelegateNoParam();

        private _WebRefernces_Globals.InformationServiceSession infoS = null;
        private string _itemnumber = null;
        private string _doklad = string.Empty;
        private DataSet ds = null;

        //Aktualni zobrazeni
        //0 = list
        //1 = detail
        private int _tab_index = 0;

        public OnLineItemumberGrid(string itemnumber, string doklad)
        {
            InitializeComponent();

            this._itemnumber = itemnumber;
            this._doklad = doklad;
            this._tab_index = Settings.OnlineItemnumberPageNumber;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void OnLineItemumberGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformOK();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        private void PerformOK()
        {
            Settings.OnlineItemnumberPageNumber = _tab_index;
            DialogResult = DialogResult.OK;
        }

        private void OnLineItemumberGrid_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            try
            {
                infoS = new _WebRefernces_Globals.InformationServiceSession();
                infoS.Url = MST_Global.ServerAddress + "Informations.asmx";
                infoS.Timeout = MST_Global.ServiceTimeOut;
                infoS.UpdateWebServiceCredentials();

                infoS.BeginDetailItemnumber(this._itemnumber, this._doklad, new AsyncCallback(KonecDetailItemnumber), null);
                Cursor.Current = Cursors.WaitCursor; 
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBoxBig.Show(ex.Message, Color.Red);
            }
        }

        private void performUpdateUI()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (listGrid.TableStyles.Count == 0 && ds.Tables[0].Columns.Count > 0)
                    {
                        DataGridTableStyle ts = new DataGridTableStyle();
                        ts.MappingName = ds.Tables[0].TableName;
                        int w = listGrid.Width / ds.Tables[0].Columns.Count;
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            DataGridTextBoxColumn tbc = new DataGridTextBoxColumn();
                            tbc.MappingName = ds.Tables[0].Columns[i].ColumnName;
                            tbc.HeaderText = ds.Tables[0].Columns[i].Caption;
                            tbc.NullText = "-";
                            tbc.Width = w;
                            ts.GridColumnStyles.Add(tbc);
                        }
                        listGrid.TableStyles.Add(ts);
                    }
                    listGrid.DataSource = ds.Tables[0];
                    listGrid.Refresh();

                    for (int i = ds.Tables[0].Columns.Count - 1; i >= 0; i--)
                    {
                        //Vytvoreni a naplneni fieldu
                        Fask.Graphic.DataField df = new Fask.Graphic.DataField();
                        df.Name = ds.Tables[0].Columns[i].ColumnName; ;
                        df.Popis = ds.Tables[0].Columns[i].ColumnName;
                        if (ds.Tables[0].Rows.Count > 0) df.Data = ds.Tables[0].Rows[0][i].ToString();
                        else df.Data = "-";
                        df.Dock = DockStyle.Top;
                        df.ReadOnly = true;
                        df.BorderStyle = BorderStyle.FixedSingle;
                        df.Font = new System.Drawing.Font("Arial", 8, FontStyle.Regular);

                        //Pridani do panelu    
                        detailPanel.Controls.Add(df);
                    }

                    ////Pruchod radku
                    //for (int r = ds.Tables[0].Rows.Count - 1; r >= 0; r--)
                    //{
                    //    for (int i = ds.Tables[0].Columns.Count - 1; i >= 0; i--)
                    //    {
                    //        //Vytvoreni a naplneni fieldu
                    //        Fask.Graphic.DataField df = new Fask.Graphic.DataField();
                    //        df.Popis = ds.Tables[0].Columns[i].ColumnName;
                    //        df.Data = ds.Tables[0].Rows[r][i].ToString().Trim();
                    //        df.Dock = DockStyle.Top;
                    //        df.ReadOnly = true;
                    //        df.BorderStyle = BorderStyle.FixedSingle;
                    //        //Testovaci zarovnani
                    //        //df.PopisTextAlign = ContentAlignment.TopLeft;
                    //        //TODO: nastavit nejak barvu
                    //        //Z barev nic nezabralo
                    //        //df.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
                    //        //df.DataBackColor = System.Drawing.Color.White;
                    //        //df.BackColor = SystemColors.Control;
                    //        //df.ForeColor = SystemColors.ControlText;
                    //        //df.DataBackColor = SystemColors.Window;
                    //        df.Font = new System.Drawing.Font("Arial", 8, FontStyle.Regular);

                    //        //Pridani do panelu    
                    //        detailPanel.Controls.Add(df);
                    //    }
                    //}
                }
                else
                {
                    MessageBoxBig.Show("Žádná data k dispozici");
                }

                //Zobrazeni vzdy
                showView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void updateUI()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                //Aktualni radek
                DataRow dr = listGrid.CurrentRow;

                //Pruchod radku
                int length = dr.ItemArray.Length;
                for (int i = 0; i < length; i++)
                {
                    ((Fask.Graphic.DataField)detailPanel.Controls[length - i - 1]).Data = dr.ItemArray[i].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void KonecDetailItemnumber(IAsyncResult ares)
        {
            try
            {
                ds = infoS.EndDetailItemnumber(ares);
                this.BeginInvoke(new DelegateNoParam(performUpdateUI));
                //this.BeginInvoke(new DelegateNoParam(UpdateUI));
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, Color.Red);
            }
            finally
            {
                infoS.Dispose();
                infoS = null;
            }
        }

        private void OnLineItemumberGrid_Closing(object sender, CancelEventArgs e)
        {
            Cursor.Current = Cursors.Default;

            if (infoS != null)
            {
                try
                {
                    infoS.Abort();
                }
                catch { }
            }            
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            updateUI();
        }

        private void menuListDetail_Click(object sender, EventArgs e)
        {
            changeView();
        }

        //Zmena zobrazeni
        private void changeView()
        {
            //Pokud je aktualne zobrazen list
            if (_tab_index == 0)
            {
                //Index detailu
                _tab_index = 1;
                //Zobrazim detail
                showView();
            }
            //Pokud je aktualne zobrazen detail
            else if (_tab_index == 1)
            {
                //Index listu
                _tab_index = 0;
                //Zobrazim list
                showView();
            }
        }

        //Zobrazeni
        private void showView()
        {
            //Pokud je aktualne zobrazen list
            if (_tab_index == 0)
            {
                //Zobrazim list
                listGrid.Visible = true;
                listGrid.Dock = DockStyle.Fill;   

                //Schovam detail
                detailPanel.Visible = false;
            }
            //Pokud je aktualne zobrazen detail
            else if (_tab_index == 1)
            {
                //Zobrazim panel
                detailPanel.Visible = true;
                detailPanel.Dock = DockStyle.Fill;

                //Schovam list
                listGrid.Visible = false;
            }

            //List vzdy focus, aby fakcilo posunovani
            listGrid.Focus();
        }
    }
}