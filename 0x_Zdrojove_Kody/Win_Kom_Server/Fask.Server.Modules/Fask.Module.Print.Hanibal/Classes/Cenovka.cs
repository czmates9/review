using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Drawing;
using System.Data;
using System.Drawing.Printing;

namespace Fask.Module.Print.Hanibal.Classes
{
    public class Cenovka
    {
        // Fields
        public bool akce;
        public double cena;
        public double cena1;
        public string cena1text;
        public double cena2;
        public string cena2text;
        public double cenaDoporucena;
        public bool doprodej;
        public string EAN;
        public int id;
        public bool lzeDoobjednat;
        public string mena;
        public string nazev;
        public double sleva;
        public bool zobrazDveCeny;

        // Methods
        public Cenovka()
        {
            this.id = -1;
            this.EAN = string.Empty;
            this.nazev = string.Empty;
            this.akce = false;
            this.doprodej = false;
            this.cena = 0.0;
            this.cenaDoporucena = 0.0;
            this.sleva = 0.0;
            this.mena = "Kč";
            this.zobrazDveCeny = false;
            this.lzeDoobjednat = false;
            this.cena1 = 0.0;
            this.cena2 = 0.0;
            this.cena1text = string.Empty;
            this.cena2text = string.Empty;
        }




        #region Puvodne kody z aplikace od hanibal


        //public Cenovka(string ean):this()
        //{
        //    try
        //    {
        //        using (SqlConnection connection = Utils.OpenDatabase(Utils.getConnectionString("pohoda")))
        //        {
        //            SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM SKz WHERE EAN = @EAN AND RefSklad IN (" + HanibalEAN_Settings.Default.PohodaSklady + ")", connection);
        //            command.Parameters.Add("@EAN", SqlDbType.VarChar).Value = ean;
        //            SqlDataReader reader = command.ExecuteReader();
        //            if (reader.Read())
        //            {
        //                this.EAN = ean;
        //                this.id = Convert.ToInt32(reader["ID"]);
        //                this.nazev = Convert.ToString(reader["Nazev"]);
        //                this.akce = Convert.ToBoolean(reader["Akce"]);
        //                this.doprodej = Convert.ToBoolean(reader["Doprodej"]);
        //                this.cena = Convert.ToDouble(reader["ProdejDPH"]);
        //                if (Utils.ColumnExists(reader, "VPrMOC"))
        //                {
        //                    this.cenaDoporucena = Convert.ToDouble(reader["VPrMOC"]);
        //                }
        //                if (Utils.ColumnExists(reader, "VPrSlevaVypr2"))
        //                {
        //                    this.sleva = (reader["VPrSlevaVypr2"] != DBNull.Value) ? Convert.ToDouble(reader["VPrSlevaVypr2"]) : 0.0;
        //                }
        //                if (Utils.ColumnExists(reader, "VPrLzeDoobjednat"))
        //                {
        //                    this.lzeDoobjednat = (reader["VPrLzeDoobjednat"] != DBNull.Value) ? Convert.ToBoolean(reader["VPrLzeDoobjednat"]) : false;
        //                }
        //            }
        //            else
        //            {
        //                Logging.Log.writeErrorLog("### ERROR - zbož\x00ed s EAN " + ean + " nebylo v DB nalezeno");
        //            }
        //            reader.Close();
        //            command.Connection.Close();
        //            this.SetCena12();
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Logging.Log.writeErrorLog("### DB ERROR - probl\x00e9m se spojen\x00edm do datab\x00e1ze POHODA - " + exception.Message);
        //    }
        //}



        //public Cenovka(string typ, string hodnota): this()
        //{
        //    try
        //    {
        //        using (SqlConnection connection = Utils.OpenDatabase(Utils.getConnectionString("pohoda")))
        //        {
        //            if (typ == "kod")
        //            {
        //                SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM SKz WHERE ObjNazev = @KOD AND RefSklad IN (" + HanibalEAN_Settings.Default.PohodaSklady + ")", connection);
        //                command.Parameters.Add("@KOD", SqlDbType.VarChar).Value = hodnota;
        //                SqlDataReader reader = command.ExecuteReader();
        //                if (reader.Read())
        //                {
        //                    this.id = Convert.ToInt32(reader["ID"]);
        //                    this.EAN = Convert.ToString(reader["EAN"]);
        //                    this.nazev = Convert.ToString(reader["Nazev"]);
        //                    this.akce = Convert.ToBoolean(reader["Akce"]);
        //                    this.doprodej = Convert.ToBoolean(reader["Doprodej"]);
        //                    this.cena = Convert.ToDouble(reader["ProdejDPH"]);
        //                    if (Utils.ColumnExists(reader, "VPrMOC"))
        //                    {
        //                        this.cenaDoporucena = Convert.ToDouble(reader["VPrMOC"]);
        //                    }
        //                    if (Utils.ColumnExists(reader, "VPrSlevaVypr2"))
        //                    {
        //                        this.sleva = (reader["VPrSlevaVypr2"] != DBNull.Value) ? Convert.ToDouble(reader["VPrSlevaVypr2"]) : 0.0;
        //                    }
        //                    if (Utils.ColumnExists(reader, "VPrLzeDoobjednat"))
        //                    {
        //                        this.lzeDoobjednat = (reader["VPrLzeDoobjednat"] != DBNull.Value) ? Convert.ToBoolean(reader["VPrLzeDoobjednat"]) : false;
        //                    }
        //                }
        //                else
        //                {
        //                    Logging.Log.writeErrorLog("### ERROR - zbož\x00ed s k\x00f3dem " + hodnota + " nebylo v DB nalezeno");
        //                }
        //                reader.Close();
        //                command.Connection.Close();
        //                this.SetCena12();
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Logging.Log.writeErrorLog("### DB ERROR - probl\x00e9m se spojen\x00edm do datab\x00e1ze POHODA [kod] - " + exception.Message);
        //    }
        //}

        #endregion

        #region Upravene kody pro nase potreby

        public void InitCenovka(int ID)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB)) //Utils.OpenDatabase(Utils.getConnectionString("pohoda")))
                {
                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand("SELECT TOP 1 * FROM SKz WHERE ID = ? ", connection); //AND RefSklad IN (" + string.Empty + ")"
                    command.Parameters.Add("?", System.Data.OleDb.OleDbType.Integer).Value = ID;
                    command.Connection.Open();
                    System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        this.EAN = Convert.ToString(reader["EAN"]);// ean;
                        this.id = ID;
                        this.nazev = Convert.ToString(reader["Nazev"]);
                        this.akce = Convert.ToBoolean(reader["Akce"]);
                        this.doprodej = Convert.ToBoolean(reader["Doprodej"]);
                        this.cena = Convert.ToDouble(reader["ProdejDPH"]);
                        if (ColumnExists(reader, "VPrMOC"))
                        {
                            this.cenaDoporucena = Convert.ToDouble(reader["VPrMOC"]);
                        }
                        if (ColumnExists(reader, "VPrSlevaVypr2"))
                        {
                            this.sleva = (reader["VPrSlevaVypr2"] != DBNull.Value) ? Convert.ToDouble(reader["VPrSlevaVypr2"]) : 0.0;
                        }
                        if (ColumnExists(reader, "VPrLzeDoobjednat"))
                        {
                            this.lzeDoobjednat = (reader["VPrLzeDoobjednat"] != DBNull.Value) ? Convert.ToBoolean(reader["VPrLzeDoobjednat"]) : false;
                        }
                    }
                    else
                    {
						Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error ,"### ERROR - zbož\x00ed s ID " + ID + " nebylo v DB nalezeno");
                    }
                    reader.Close();
                    command.Connection.Close();
                    this.SetCena12();
                }
            }
            catch (Exception exception)
            {

				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error ,"### DB ERROR - probl\x00e9m se spojen\x00edm do datab\x00e1ze POHODA");
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }



        #endregion

        public double GetNaseCena()
        {
            double cena = this.cena;
            if (this.sleva > 0.0)
            {
                cena = Math.Round((double)(this.cena - ((this.cena * this.sleva) / 100.0)), 0);
            }
            return cena;
        }

        public string GetTextCena()
        {
            Globals_V1.LoadConfiguration();
            string str = Globals_V1.Konfigurace.PrintParams[0].CenovkaCena1 + " / ";
            if ((this.akce || this.doprodej) || this.lzeDoobjednat)
            {
                return (str + Globals_V1.Konfigurace.PrintParams[0].CenovkaCena2);
            }
            return (str + Globals_V1.Konfigurace.PrintParams[0].CenovkaCena3);
        }

        public string GetTextCena1()
        {
            return (this.cena1.ToString("### ###") + " " + this.mena);
        }

        public string GetTextCena2()
        {
            return (this.cena2.ToString("### ###") + " " + this.mena);
        }

        public string GetTextCenaBezDoporucena()
        {
            Globals_V1.LoadConfiguration();
            string str = string.Empty;
            if ((this.akce || this.doprodej) || this.lzeDoobjednat)
            {
                return (str + Globals_V1.Konfigurace.PrintParams[0].CenovkaCena2);
            }
            return (str + Globals_V1.Konfigurace.PrintParams[0].CenovkaCena3);
        }






        //public void Print(PrintPageEventArgs e)
        //{
        //    //


        //    new Pen(Color.Black, 1f);
        //    float cenovkaWidth = 250;  //HanibalEAN_Settings.Default.CenovkaWidth;
        //    float x = 5f;
        //    float y = 9f;
        //    StringFormat format = new StringFormat();
        //    format.LineAlignment = StringAlignment.Center;
        //    format.Alignment = StringAlignment.Center;
        //    Image image = BarcodeDrawFactory.Code128WithChecksum.Draw(this.EAN, 39, 2); //HanibalEAN_Settings.Default.CenovkaEANSize, 2);
        //    float num4 = x;
        //    e.Graphics.DrawImage(image, num4, y, cenovkaWidth - x, (float)39); //HanibalEAN_Settings.Default.CenovkaEANSize);
        //    y += 43; //HanibalEAN_Settings.Default.CenovkaEANSpace;
        //    if (this.nazev.Length < 55)
        //    {
        //        //e.Graphics.DrawString(this.nazev, new Font("Arial", (float)HanibalEAN_Settings.Default.CenovkaNazevSize, FontStyle.Bold), Brushes.Black, new RectangleF(x, y, cenovkaWidth, (float)HanibalEAN_Settings.Default.CenovkaNazevBox), format);
        //        e.Graphics.DrawString(this.nazev, new Font("Arial", (float)8, FontStyle.Bold), Brushes.Black, new RectangleF(x, y, cenovkaWidth, (float)14), format);
        //    }
        //    else if (this.nazev.Length > 75)
        //    {
        //        if (this.nazev.Length > 120)
        //        {
        //            this.nazev = this.nazev.Substring(0, 120);
        //        }
        //        //e.Graphics.DrawString(this.nazev, new Font("Arial", (float)(HanibalEAN_Settings.Default.CenovkaNazevSize - 2), FontStyle.Bold), Brushes.Black, new RectangleF(0f, y, cenovkaWidth + x, (float)HanibalEAN_Settings.Default.CenovkaNazevBox), format);
        //        e.Graphics.DrawString(this.nazev, new Font("Arial", (float)(8 - 2), FontStyle.Bold), Brushes.Black, new RectangleF(0f, y, cenovkaWidth + x, (float)14), format);
        //    }
        //    else
        //    {
        //        //e.Graphics.DrawString(this.nazev, new Font("Arial", (float)(HanibalEAN_Settings.Default.CenovkaNazevSize - 1), FontStyle.Bold), Brushes.Black, new RectangleF(0f, y, cenovkaWidth + x, (float)HanibalEAN_Settings.Default.CenovkaNazevBox), format);
        //        e.Graphics.DrawString(this.nazev, new Font("Arial", (float)(8 - 1), FontStyle.Bold), Brushes.Black, new RectangleF(0f, y, cenovkaWidth + x, (float)14), format);
        //    }
        //    y += 12; //HanibalEAN_Settings.Default.CenovkaNazevSpace;
        //    if (this.zobrazDveCeny)
        //    {
        //        //e.Graphics.DrawString(this.cena1text + " / " + this.cena2text, new Font("Arial", (float)HanibalEAN_Settings.Default.CenovkaCTextSize), Brushes.Black, new RectangleF(x, y, cenovkaWidth, (float)HanibalEAN_Settings.Default.CenovkaCTextBox), format);
        //        e.Graphics.DrawString(this.cena1text + " / " + this.cena2text, new Font("Arial", (float)10), Brushes.Black, new RectangleF(x, y, cenovkaWidth, (float)18), format);
        //    }
        //    else
        //    {
        //        //e.Graphics.DrawString(this.cena1text, new Font("Arial", (float)HanibalEAN_Settings.Default.CenovkaCTextSize), Brushes.Black, new RectangleF(x, y, cenovkaWidth, (float)HanibalEAN_Settings.Default.CenovkaCTextBox), format);
        //        e.Graphics.DrawString(this.cena1text, new Font("Arial", (float)10), Brushes.Black, new RectangleF(x, y, cenovkaWidth, (float)18), format);
        //    }
        //    y += 0; //HanibalEAN_Settings.Default.CenovkaCTextSpace;
        //    float width = Convert.ToSingle(Math.Round((double)(cenovkaWidth / 2f), 0));
        //    if (this.zobrazDveCeny)
        //    {
        //        //e.Graphics.DrawString(this.GetTextCena1(), new Font("Arial", (float)HanibalEAN_Settings.Default.CenovkaCenySize, FontStyle.Strikeout | FontStyle.Bold), Brushes.Black, new RectangleF(x, y, width, (float)HanibalEAN_Settings.Default.CenovkaCenyBox), format);
        //        //e.Graphics.DrawString(this.GetTextCena2(), new Font("Arial", (float)HanibalEAN_Settings.Default.CenovkaCenySize, FontStyle.Bold), Brushes.Black, new RectangleF(x + width, y, width, (float)HanibalEAN_Settings.Default.CenovkaCenyBox), format);
        //        e.Graphics.DrawString(this.GetTextCena1(), new Font("Arial", (float)10, FontStyle.Strikeout | FontStyle.Bold), Brushes.Black, new RectangleF(x, y, width, (float)18), format);
        //        e.Graphics.DrawString(this.GetTextCena2(), new Font("Arial", (float)10, FontStyle.Bold), Brushes.Black, new RectangleF(x + width, y, width, (float)18), format);
            
        //    }
        //    else
        //    {
        //        //e.Graphics.DrawString(this.GetTextCena1(), new Font("Arial", (float)HanibalEAN_Settings.Default.CenovkaCenySize, FontStyle.Bold), Brushes.Black, new RectangleF(x, y, cenovkaWidth, (float)HanibalEAN_Settings.Default.CenovkaCenyBox), format);
        //        e.Graphics.DrawString(this.GetTextCena1(), new Font("Arial", (float)10, FontStyle.Bold), Brushes.Black, new RectangleF(x, y, cenovkaWidth, (float)18), format);
        //    }
        //}



        public void SetCena12()
        {
            Globals_V1.LoadConfiguration();
            if (this.akce)
            {
                this.cena1 = this.cenaDoporucena;
                this.cena2 = this.GetNaseCena();
                this.cena1text = Globals_V1.Konfigurace.PrintParams[0].CenovkaCena1;
                this.cena2text = Globals_V1.Konfigurace.PrintParams[0].CenovkaCena2;
                if (this.cena1 > this.cena2)
                {
                    this.zobrazDveCeny = true;
                }
                else
                {
                    this.zobrazDveCeny = false;
                }
            }
            else if (this.doprodej || this.lzeDoobjednat)
            {
                if (this.sleva > 0.0)
                {
                    this.cena1 = this.cenaDoporucena;
                    this.cena2 = this.GetNaseCena();
                    this.cena1text = Globals_V1.Konfigurace.PrintParams[0].CenovkaCena1;
                    this.cena2text = Globals_V1.Konfigurace.PrintParams[0].CenovkaCena2;
                    if (this.cena1 > this.cena2)
                    {
                        this.zobrazDveCeny = true;
                    }
                    else
                    {
                        this.zobrazDveCeny = false;
                    }
                }
                else if (this.cenaDoporucena <= this.cena)
                {
                    this.cena1 = this.cena;
                    this.cena1text = Globals_V1.Konfigurace.PrintParams[0].CenovkaCena3;
                    this.zobrazDveCeny = false;
                }
                else
                {
                    this.cena1 = this.cenaDoporucena;
                    this.cena2 = this.cena;
                    this.cena1text = Globals_V1.Konfigurace.PrintParams[0].CenovkaCena1;
                    this.cena2text = Globals_V1.Konfigurace.PrintParams[0].CenovkaCena3;
                    this.zobrazDveCeny = true;
                }
            }
            else if (this.cenaDoporucena <= this.cena)
            {
                this.cena1 = this.cena;
                this.cena1text = Globals_V1.Konfigurace.PrintParams[0].CenovkaCena3;
                this.zobrazDveCeny = false;
            }
            else
            {
                this.cena1 = this.cenaDoporucena;
                this.cena2 = this.cena;
                this.cena1text = Globals_V1.Konfigurace.PrintParams[0].CenovkaCena1;
                this.cena2text = Globals_V1.Konfigurace.PrintParams[0].CenovkaCena3;
                this.zobrazDveCeny = true;
            }
        }

 

 

    }
}
