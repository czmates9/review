using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Konzola.Vyroba
{
    public partial class FormVyrobniPrikaz_VPP_GenerovaniSN : Form
    {

        #region Parametre

        private string _kodOD = string.Empty;
        /// <summary>
        /// Hodnota vstupniho pole
        /// </summary>
        public string Kod_OD
        {
            get { return tb_Od.Text.Trim(); }
            set
            {
                tb_Od.Text = value.Trim();
                _kodOD = tb_Od.Text;
            }
        }

        private string _kodDO = string.Empty;
        /// <summary>
        /// Hodnota vstupniho pole
        /// </summary>
        public string Kod_DO
        {
            get { return tb_Do.Text.Trim(); }
            set
            {
                tb_Do.Text = value.Trim();
                _kodDO = tb_Do.Text;
            }
        }

        private string _kodPrefix = string.Empty;
        /// <summary>
        /// Hodnota Prefixu
        /// </summary>
        public string KodPrefix
        {
            get { return _kodPrefix.Trim(); }
            set
            {
                _kodPrefix = value.Trim();
            }
        }

        private int _kodOd_Start;
        /// <summary>
        /// Hodnota Prefixu
        /// </summary>
        public int KodOd_Start
        {
            get { return _kodOd_Start; }
            set
            {
                _kodOd_Start = value;
                //_kodOD = tb_Od.Text;
            }
        }

        private string _kod_Pocet = string.Empty;
        /// <summary>
        /// Hodnota vstupniho pole
        /// </summary>
        public string Kod_Pocet
        {
            get { return tb_Pocet.Text.Trim(); }
            set
            {
                tb_Pocet.Text = value.Trim();
                _kod_Pocet = tb_Pocet.Text;
            }
        }


        #endregion

        public FormVyrobniPrikaz_VPP_GenerovaniSN(decimal QTY)
        {
            InitializeComponent();

            Kod_Pocet = QTY.ToString("#.#####");
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            btnOK.Size = nsize;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Vypocitaj();

            if (!ValidateData())
                return;

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// Kontrola vyplněných dat.
        /// </summary>
        /// <returns>True, pokud je vše v pořádku, jinak False</returns>
        private bool ValidateData()
        {
            try
            {
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(tb_Do.Text.Trim()))
                    errorProvider1.SetError(tb_Do, "Musíte zadat id skladu");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return IsAllValid();
        }

        private bool IsAllValid()
        {

            foreach (Control c in panelLeft.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;

            foreach (Control c in panelRight.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;

            foreach (Control c in panel1.Controls)
                if (errorProvider1.GetError(c) != "")
                    return false;

            return true;
        }


        #region Vypočet logika

        private void Vypocitaj()
        {

            ///Promenna v konfiguraci ktera urcuje pocet v SN od konce
            int N = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Vyroba_Ostatni[0].FormVyrobniPrikaz_VPP_GenerovaniSN_N;

            string Format = string.Empty;

            for (int i = 0; i < N; i++)
            {
                Format += "0";
            }


            try
            {

                if (string.IsNullOrEmpty(tb_Od.Text))
                {
                    throw new Exception("Nenalezeno počáteční SN");
                }

                string sub_start;
                try
                {
                    sub_start = Kod_OD.Substring(Kod_OD.Length - N);

                }
                catch (Exception)
                {
                    throw new Exception(string.Format("Min. počet znaků je '{0}'", N));
                }

                KodPrefix = tb_Od.Text.Substring(0, tb_Od.Text.Length - N);

                int start;
                int end;

                try
                {
                    start = int.Parse(sub_start);
                    KodOd_Start = start;
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    throw new Exception(string.Format("Posledních '{0}' znaků není číslo", N));
                }

                if (!string.IsNullOrEmpty(tb_Do.Text))
                {
                    string sub_end;
                    try
                    {
                        sub_end = Kod_DO.Substring(Kod_DO.Length - N);

                    }
                    catch (Exception)
                    {
                        throw new Exception(string.Format("Min. počet znaků je '{0}'", N));
                    }

                    try
                    {
                        end = int.Parse(sub_end);
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        throw new Exception(string.Format("Posledních {0} znaků není číslo", N));
                    }

                    string prefixEND = tb_Do.Text.Substring(0, tb_Od.Text.Length - N);

                    if (KodPrefix.Trim() != prefixEND.Trim())
                        throw new Exception("Prefix SN Od a SN Do musí byt totožne");


                    int count = (end - start) + 1;

                    if (count <= 0)
                    {
                        throw new Exception("Počáteční SN je větší než koncové SN");
                    }
                    
                    Kod_Pocet = count.ToString();
                    

                }
                else if (!string.IsNullOrEmpty(tb_Pocet.Text))
                {
                    try
                    {
                        end = int.Parse(tb_Pocet.Text);
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        throw new Exception("počet není číslo");
                    }

                    if (end == 0)
                    {
                        throw new Exception("Nelze zadat počet Nula!");
                    }

                    int count = (end + start) - 1;

                    Kod_DO = KodPrefix + count.ToString(Format);

                }
                else
                {
                    //throw new Exception("neni zadany koncovy dopočet");
                    throw new Exception("Zadej počet nebo koncové SN");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }


        #endregion

    }
}
