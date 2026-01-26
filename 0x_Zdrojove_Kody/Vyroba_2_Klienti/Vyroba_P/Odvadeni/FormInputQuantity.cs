using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Fask.Vyroba_P.Forms;
using JR.Utils.GUI.Forms;

namespace Fask.Vyroba_P.Odvadeni
{
    public partial class FormInputQuantity : Form
    {
        string textform = string.Empty;
        public string nazev = string.Empty;
        public bool sarzeEnable = false;
        public float mereniPapouch1 = 0;
        public float mereniPapouch2 = 0;
        public float mereniPapouch3 = 0;
        public float mereniPapouch4 = 0;
        public bool mereniPapouch = false;

        public string Kod
        {
            get { return this.textBoxKod.Text; }
            set
            {
                this.textBoxKod.Text = value;
                this.textBoxKod.SelectAll();
            }
        }

        public bool Podbarveni
        {
           // get { return this.textBoxKod.Text; }
            set
            {
                if (value)
                    panelComponents.BackColor = Color.GreenYellow;

                if(textBoxKod.ReadOnly)
                {
                    textBoxKod.BackColor = Color.DarkGray;
                }
                if (textBoxCelkemVBaleni.ReadOnly)
                {
                    textBoxCelkemVBaleni.BackColor = Color.DarkGray;
                }
                if(textBoxBaleni.ReadOnly)
                {
                    textBoxBaleni.BackColor = Color.DarkGray;
                }
                if(textBoxSarze.ReadOnly)
                {
                    textBoxSarze.BackColor = Color.DarkGray;
                }
                //this.textBoxKod.Text = value;
                //this.textBoxKod.SelectAll();
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

        //podbarveni MaR 2.9.2024

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
                textBoxSarze.BackColor = Color.DarkGray;

            }


            if (Settings.Main_Mereni_Papouch && mereniPapouch)
            {
                btn_Mereni_Papouch.Visible = true;
                tB_Mer_1.Visible = true;
                tB_Mer_2.Visible = true;
                tB_Mer_3.Visible = true;
                tB_Mer_4.Visible = true;


                mereniPapouch1 = 0;
                mereniPapouch2 = 0;
                mereniPapouch3 = 0;
                mereniPapouch4 = 0;
            }
            else
            {
                btn_Mereni_Papouch.Visible = false;
                tB_Mer_1.Visible = false;
                tB_Mer_2.Visible = false;
                tB_Mer_3.Visible = false;
                tB_Mer_4.Visible = false;
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
                FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.DataReady += new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
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
                FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }

        private void ScannerEventHandlerMethod(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
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

        void Scanner_DataReady(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.Vyroba_P.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
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

        private void btn_Mereni_Papouch_Click(object sender, EventArgs e)
        {

            Mereni();



        }

        private void Mereni()
        {
            try
            {
                //string url = "http://192.168.1.254/fresh.xml";


                string url = Settings.PapouchURL;


                using (HttpClient client = new HttpClient())
                {



                    string xmlContent = string.Empty;

                    try
                    {
                        xmlContent = client.GetStringAsync(url).Result;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Chyba pøi mìøení, špatnì zadaná URL adresa Papouch: ", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }



                    XDocument xmlDoc = XDocument.Parse(xmlContent);

                    List<InputValue> inputs = new List<InputValue>();
                    foreach (var input in xmlDoc.Descendants("input"))
                    {
                        inputs.Add(new InputValue
                        {
                            Id = int.Parse(input.Attribute("id")?.Value ?? "0"),
                            Name = input.Attribute("name")?.Value?.Trim(),
                            Unit = input.Attribute("unit")?.Value?.Trim(),
                            Value = float.TryParse(input.Attribute("val")?.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out float v) ? v : 0,
                            Status = int.Parse(input.Attribute("stat")?.Value ?? "0")
                        });
                    }

                    // Získání hodnot z kanálù 1–4
                    var kanal1 = inputs.Find(x => x.Id == 1);
                    var kanal2 = inputs.Find(x => x.Id == 2);
                    var kanal3 = inputs.Find(x => x.Id == 3);
                    var kanal4 = inputs.Find(x => x.Id == 4);

                    // Zobrazení TextBoxù
                    tB_Mer_1.Visible = true;
                    tB_Mer_2.Visible = true;
                    tB_Mer_3.Visible = true;
                    tB_Mer_4.Visible = true;

                    // Uložení do promìnných a výpis do TextBoxù
                    mereniPapouch1 = kanal1?.Value ?? 0;
                    mereniPapouch2 = kanal2?.Value ?? 0;
                    mereniPapouch3 = kanal3?.Value ?? 0;
                    mereniPapouch4 = kanal4?.Value ?? 0;

                    tB_Mer_1.Text = $"{mereniPapouch1}";
                    tB_Mer_2.Text = $"{mereniPapouch2}";
                    tB_Mer_3.Text = $"{mereniPapouch3}";
                    tB_Mer_4.Text = $"{mereniPapouch4}";


                    //tB_Mer_1.Text = $"{mereniPapouch1} {kanal1?.Unit}";
                    //tB_Mer_2.Text = $"{mereniPapouch2} {kanal2?.Unit}";
                    //tB_Mer_3.Text = $"{mereniPapouch3} {kanal3?.Unit}";
                    //tB_Mer_4.Text = $"{mereniPapouch4} {kanal4?.Unit}";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba pøi naèítání hodnot mìøení Papouch: " + ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            try
            {

            }
            catch (Exception ex)
            {

                //if (vpp != null)
                //{
                //    vpp.CZ_REZ1_Track = mereniPapouch1;
                //}

                MessageBox.Show("Chyba pøi zápisu hodnot mìøení Papouch: " + ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }



        #region MaR 15.4.2025 OLD
        //private void Mereni()
        //{
        //    try
        //    {
        //        string url = "http://192.168.1.254/fresh.xml";

        //        using (HttpClient client = new HttpClient())
        //        {
        //            string xmlContent = client.GetStringAsync(url).Result;
        //            XDocument xmlDoc = XDocument.Parse(xmlContent);

        //            List<InputValue> inputs = new List<InputValue>();
        //            foreach (var input in xmlDoc.Descendants("input"))
        //            {
        //                inputs.Add(new InputValue
        //                {
        //                    Id = int.Parse(input.Attribute("id")?.Value ?? "0"),
        //                    Name = input.Attribute("name")?.Value?.Trim(),
        //                    Unit = input.Attribute("unit")?.Value?.Trim(),
        //                    Value = float.TryParse(input.Attribute("val")?.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out float v) ? v : 0,
        //                    Status = int.Parse(input.Attribute("stat")?.Value ?? "0")
        //                });
        //            }

        //            string vystup = "";
        //            for (int i = 1; i <= 4; i++)
        //            {
        //                var kanal = inputs.Find(x => x.Id == i);
        //                if (kanal != null)
        //                {
        //                    vystup += $"Kanál {kanal.Id} - {kanal.Name.Trim()}: {kanal.Value} {kanal.Unit}\n";
        //                }
        //                else
        //                {
        //                    vystup += $"Kanál {i} nebyl nalezen.\n";
        //                }
        //            }

        //            MessageBox.Show(vystup, "Výsledky mìøení", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Chyba pøi naèítání: " + ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //} 
        #endregion


    }



}


