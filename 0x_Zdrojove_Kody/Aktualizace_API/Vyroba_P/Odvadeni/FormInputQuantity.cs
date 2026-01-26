using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.Aktualizace_API.Forms;
using JR.Utils.GUI.Forms;

namespace Fask.Aktualizace_API.Odvadeni
{
    public partial class FormInputQuantity : Form
    {
        string textform = string.Empty;
        public string nazev = string.Empty;
        public bool sarzeEnable = false;

        public string Kod
        {
            get { return this.textBoxKod.Text; }
            set
            {
                this.textBoxKod.Text = value;
                this.textBoxKod.SelectAll();
            }
        }


        public string SarzeText
        {
            get { return this.textBoxSarze.Text; }
            set
            {
                this.textBoxSarze.Text = value;
                this.textBoxSarze.SelectAll();
            }
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow _pracovnik = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik
        {
            set
            {
                _pracovnik = value;
                UpdateTextForm();
            }
        }
        private void UpdateTextForm()
        {
            this.Text = textform;
            if (_pracovnik != null)
                this.Text += ", " + _pracovnik.ToString();

            if (vph != null)
                this.Text += ", " + vph.SOPNUMBE.Trim() + ":" + vph.SOPTYPE.Trim();

            if (vpp != null)
            {

                this.Text += ", " + vpp.ITEMDESC.Trim();
                nazev = vpp.ITEMDESC.Trim();
            }
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow vph = null;
        private Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow vpp = null;
        public FormInputQuantity()
        {
            InitializeComponent();

            this.textform = this.Text;
            l_nazev.Text = nazev;
        }
            
        public FormInputQuantity(Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow vph, Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow vpp) : this()
        {
            this.vph = vph;
            this.vpp = vpp;

            if(vpp.SerNumT == 2)
            {
                sarzeEnable = true;
            }
            
            UpdateTextForm();
            l_nazev.Text = nazev;
        }


        private decimal _mnozstvi = 0;
        public decimal Mnozstvi
        {
            get { return _mnozstvi; }
            set
            {
                _mnozstvi = value;
                this.textBoxKod.Text = _mnozstvi.ToString("0.####");
                this.textBoxKod.SelectAll();
            }
        }

        private string _sarze = string.Empty;
        public string Sarze
        {
            get { return _sarze; }
            set
            {
                _sarze = value;
                this.textBoxSarze.Text = _sarze;
                this.textBoxSarze.SelectAll();
            }
        }


        private string _sarzeMAT_ID = string.Empty;
        public string SarzeMAT_ID
        {
            get { return _sarzeMAT_ID; }
            set
            {
                _sarzeMAT_ID = value;
            }
        }

        private void FormInputKod_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            l_nazev.Text = nazev;

            textBoxSarze.Text = string.Empty;

            if (sarzeEnable)
            {
                textBoxSarze.Enabled = true;
            }
            else
            {

                textBoxSarze.Enabled = false;
            }
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);
            textBoxKod_Activate();
            ScannerStart();
        }

        private void textBoxKod_Activate()
        {
            textBoxKod.SelectAll();
            textBoxKod.Focus();
        }

        private void textBoxSarze_Activate()
        {
            textBoxSarze.SelectAll();
            textBoxSarze.Focus();
        }

        private void ScannerStart()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.DataReady += new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Enable();
            }
            catch
            {
            }
        }

        private void ScannerStop()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }

        private void ScannerEventHandlerMethod(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length == 0)
                return;

            if(sarzeEnable)
            {
                this.SarzeText = e.BarcodeData.Trim();
            }
            else
            this.Kod = e.BarcodeData.Trim();
            
            this.PerformOK();
        }

        void Scanner_DataReady(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.Aktualizace_API.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

        public void PerformCancel()
        {
            ScannerStop();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            string nazev = string.Empty;
            if (vpp != null)
                nazev = vpp.ITEMDESC + Environment.NewLine + Environment.NewLine;

            if (this.textBoxKod.Text.Trim().Length == 0)
            {
                FlexibleMessageBox.Show(this, nazev + "Musíte zadat hodnotu množství!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                textBoxKod_Activate();
                return;
            }

            if(sarzeEnable)
            {
                if (this.textBoxSarze.Text.Trim().Length == 0)
                {
                    FlexibleMessageBox.Show(this, nazev + "Musíte zadat hodnotu šarže!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    textBoxSarze_Activate();
                    return;
                }
            }

            try
            {
                _mnozstvi = decimal.Parse(this.textBoxKod.Text);
            }
            catch (Exception ex)
            {
                FlexibleMessageBox.Show(this, ex.Message, "Množství", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (sarzeEnable)
            {
                try
                {
                    _sarze = this.textBoxSarze.Text.Trim();
                    //9.2.2024 MaR Šarže kontrola
                    if (Settings.OdvadeniKontrolaSarze & vpp == null)
                    {
                        (int returnValue, string outputMessage) = CheckSerial(_sarze, _sarzeMAT_ID);

                        if (returnValue != 0)
                        {
                            FlexibleMessageBox.Show(outputMessage, "Ovìøení šarže", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }


                        //if (!OverSarzi(_sarze))
                        //{
                        //    FlexibleMessageBox.Show("Šarže po ovìøení neprošla, zadejte jinou!", "Ovìøení šarže", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    return;
                        //}
                    }


                    
                }
                catch (Exception ex)
                {
                    FlexibleMessageBox.Show(this, ex.Message, "Šarže", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ScannerStop();
            DialogResult = DialogResult.OK;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormInputKod_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        public void Handle_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {

                    if (sarzeEnable)
                    {



                        if (!string.IsNullOrEmpty(textBoxKod.Text) && !string.IsNullOrEmpty(textBoxSarze.Text))
                        {
                            PerformOK();
                        }
                        else if (!string.IsNullOrEmpty(textBoxKod.Text))
                        {
                            Control activeControl = this.ActiveControl;

                            if (activeControl is TextBox)
                            {
                                this.SelectNextControl(activeControl, true, true, true, true);
                            }
                        }
                    }
                    else
                    {
                        PerformOK();
                    }


                   
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void textBoxKod_TextChanged(object sender, EventArgs e)
        {

        }

        private void ucKeyboard1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }


        private string connectionString; // Místo tohoto vložte skuteèný pøipojovací øetìzec k vaší databázi

        public (int, string) CheckSerial(string serialId, string sarzeMAT_ID)
        {
            try
            {
                connectionString = Settings.Connection_DB;

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Chyba pøipojení do databáze, nezadáno správné pøipojení!");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("checkSerial", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Pøidání parametrù k proceduøe
                        command.Parameters.AddWithValue("@serialId", serialId);
                        command.Parameters.AddWithValue("@matid", sarzeMAT_ID);

                        // Vytvoøení parametru pro návratovou hodnotu (int)
                        SqlParameter returnParameter = command.Parameters.Add("@returnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // Otevøení spojení a provedení procedury
                        connection.Open();
                        command.ExecuteNonQuery();

                        // Zpracování výsledkù procedury
                       // int returnValue = (int)returnParameter.Value;

                        // Výsledky SELECT budou dostupné pomocí SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int isBlocked = reader.GetInt32(0); // pøedpokládám, že první sloupec je @isBlocked
                                    string message = reader.GetString(1); // pøedpokládám, že druhý sloupec je zpráva
                                    return (isBlocked, message);
                                }
                            }
                        }

                        // Pokud procedura nevrátila žádné výsledky, vrátíme pouze hodnotu procedury
                        return (-1, "Procedura ovìøení šarže nevrátila žádné výsledky");
                    }
                }

            }
            catch (Exception ex)
            {
                // Zde mùžete zachytit a zpracovat chyby
                //Console.WriteLine("Chyba pøi provádìní procedury: " + ex.Message);
                return (-1, "Chyba pøi provádìní procedury ovìøení šarže" + ex.Message);
            }

        }


    }



}


