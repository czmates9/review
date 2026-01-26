using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.IO;

namespace Fask.MST_W.Vydej_3
{
    public partial class ListNasnimForm3 : System.Windows.Forms.Form
    {
        private Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow vydejActual;

        private int countItems = 0;
        private int indexItem = 1;

		#region Event formu

		public ListNasnimForm3()
        {
            InitializeComponent();
            this.KeyPreview = true;

            miTypOznaceniZmena.Enabled = MST_Global.VydejTypOznaceniPalety; //zakaze zmenu oznaceni palety
		}

		private void ListNasnimForm_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.KeyCode == System.Windows.Forms.Keys.Up))
			{
				indexItem = 1;
				LoadRow(indexItem);
			}
			else if ((e.KeyCode == System.Windows.Forms.Keys.Down))
			{
				indexItem = countItems;
				LoadRow(indexItem);
			}
			else if ((e.KeyCode == System.Windows.Forms.Keys.Left))
			{
				if (indexItem > 1)
					indexItem--;
				LoadRow(indexItem);
			}
			else if ((e.KeyCode == System.Windows.Forms.Keys.Right))
			{
				if (indexItem < countItems)
					indexItem++;
				LoadRow(indexItem);
			}
			//else if ((e.KeyCode == System.Windows.Forms.Keys.Delete))
			//else if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
			else if ((e.KeyCode == System.Windows.Forms.Keys.Back))
			{
				// Enter
				smaz();
			}
			else if (e.KeyCode == Keys.Escape)
			{
				PerformOK();
				return;
			}
			else if (e.KeyCode == Keys.D3)
			{
				var vydejDataParametry = Vydej.vydejInstance.globalObject.vydejData.Parametry;

				if ((MST_Global.Vydej_TypSPrelokovanim) && (!vydejDataParametry[0].IsCONFIG_LOKACE_POVOLITNull()&& vydejDataParametry[0].CONFIG_LOKACE_POVOLIT))
				{
					MessageBoxBig.Show("Nelze upravit množství!", "Upozornìní", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
				}
				else 
				{
					PerformZmenaMnozstvi();
				}
			}
			else if (e.KeyCode == Keys.D9)
			{
				TypOznaceniPaletyZmena();
			}

			updateForm();
		}

		private void ListNasnimForm_Load(object sender, EventArgs e)
		{
			// nacteni lokalizace ze souboru
			Fask.Localization.LocalizationExtensionForm.Localize(this);

			this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
			this.Size = Forms.FormLocation.ScreenResolution;

			indexItem = 1;
			LoadRow(indexItem);
			//updateForm(); <= deje se v LoadRow...
		}


		private void ListNasnimForm3_Activated(object sender, EventArgs e)
		{
			// aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
			Components.KeyboardManager.LoadDefaultKeyboardMode();
		}

		private void ListNasnimForm3_Deactivate(object sender, EventArgs e)
		{
			// deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
			Components.KeyboardManager.SaveDefaultKeyboardMode();
		}

		private void ListNasnimForm3_Closing(object sender, CancelEventArgs e)
		{
			// prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
			Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;
		}


		#endregion

		private void updateForm()
        {
            try
            {
                //df_Countentries.Data = "-";
                df_Itemnmbr.Data = "-";
                df_Itemdesc.Data = "-";
                df_Sopnumbe.Data = "-";
                df_Serialnumber.Data = "-";
				df_expirace.Data = "-";
                df_Quantity.Data = "-";
                //df_Qtypack.Data = "-";
                df_Odberatel.Data = "-";
                df_Locncode.Data = "-";
                df_Kodsw.Data = "-";
                df_Datvyroby.Data = "-";
                df_Rez1.Data = "-";

                df_MJ.Data = "-";


                df_Itemnmbr.Data = vydejActual.ITEMNMBR.Trim();

                df_Sopnumbe.Data = vydejActual.SOPNUMBE.Trim();
                df_Serialnumber.Data = (String.IsNullOrEmpty(vydejActual.SERLTNUM)) ? "N/A" : vydejActual.SERLTNUM.Trim();
				df_expirace.Data = vydejActual.IsExpiraceNull() ? "N/A" : vydejActual.Expirace.ToShortDateString();
                df_Quantity.Data = vydejActual.QTYSHPPD.ToString(Settings.UIFormatDesCisel);
                //df_Qtypack.Data = vydejActual.QTYPACK.ToString();

                df_Locncode.Data = vydejActual.LOCNCODE.Trim();
                df_Kodsw.Data = vydejActual.KOD_SW.Trim();
                df_Datvyroby.Data = vydejActual.DAT_VYROBY.Trim();
                df_Rez1.Data = vydejActual.REZ_1.Trim();
                df_Rez2.Data = vydejActual.REZ_2.Trim();
                df_MJ.Data = vydejActual.MJ.Trim();
                
                
                
                string skl_id = vydejActual.SKL_ID.Trim();
                //df_Countentries.Data = Path.GetFileNameWithoutExtension(sqlfilename);

				df_Itemdesc.Data = Vydej.vydejInstance.globalObject.controller_vydej.Get_ITEMDESC_SE(df_Sopnumbe.Data.Trim(), df_Itemnmbr.Data.Trim());
                
                statusBar1.Text = indexItem.ToString() + " / " + countItems.ToString();

                try
                {
                    string odbid = string.Empty;
                    string odbPopis = string.Empty;
                    if (vydejActual.IsODBER_IDNull() || String.IsNullOrEmpty(vydejActual.ODBER_ID))                    
                    {
                        odbPopis = Fask.Localization.Localization.Vydej3ListNasnimForm3Nezadano;
                    }
                    else
                    {
                        odbid = vydejActual.ODBER_ID.Trim();
                        object o = Vydej.vydejInstance.globalObject.controller_odberatele.Get_OdbDesc(odbid);
                        if (o != null && (o is string)) 
                            odbPopis = ((string)o).Trim();
                        else
                            odbPopis = Fask.Localization.Localization.Vydej3ListNasnimForm3OdberatelNenalezen;
                    }
                    df_Odberatel.Data = odbPopis;
                }
                catch //(Exception ex)
                {
                    //Logging.Log.Write(ex);
                }

                try
                {
                    if (MST_Global.VydejLocationPouzitCiselnik)
                    {
						var obj = Vydej.vydejInstance.globalObject.controller_lokace.CZMST094_GetLocDesc(df_Locncode.Data, skl_id);
                        string lokacenazev = obj as string;
                        if (obj == null)
                            throw new Exception(String.Format("Popis lokace '{0}' nenalezen", df_Locncode.Data.Trim()));
                        if (lokacenazev == null) lokacenazev = "-";
                        else lokacenazev = lokacenazev.Trim();
                        df_Locncode.Data += " : " + lokacenazev;
                    }
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                //MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void SeznamJePrazdny()
        {
            MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListNasnimForm3SeznamJePrazdny);
            PerformOK();
        }

        private void hlavniMenu_but_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void LoadRow(int poradi)
        {
            countItems = Vydej.vydejInstance.globalObject.controller_vydej.CZMST_SI_Count_All();

            if (countItems <= 0)
            {
                SeznamJePrazdny();
                return;
            }

            vydejActual = Vydej.vydejInstance.globalObject.controller_vydej.CZMST_SI_Get_One(poradi - 1); //poradi - 1 = index

            updateForm();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            smaz();
            updateForm();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void TypOznaceniPaletyZmena()
        {
            using (TypOznaceniPaletyFormOld topf = new TypOznaceniPaletyFormOld())
            {
                topf.TypOznaceni = vydejActual.REZ_1;
                if (topf.ShowDialog() == DialogResult.Cancel)
                    return;

                vydejActual.REZ_1 = topf.TypOznaceni.Trim();
            }

            int affected = Vydej.vydejInstance.globalObject.controller_vydej.CZMST_SI_Update_One(vydejActual);
            if (affected <= 0) //=> neco se nepovedlo ...
            {
                MessageBoxBig.Show("Aktualizace se nezdaøila", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
                
        }

        private void miTypOznaceniZmena_Click(object sender, EventArgs e)
        {
            TypOznaceniPaletyZmena();
            updateForm();
        }

        private void finalize()
        {

        }

        private void PerformOK()
        {
            this.finalize();
            DialogResult = DialogResult.OK;
        }

		private void miZmenaMnozstvi_Click(object sender, EventArgs e)
		{
			try
			{
				var vydejDataParametry = Vydej.vydejInstance.globalObject.vydejData.Parametry;

				if ((MST_Global.Vydej_TypSPrelokovanim) && (!vydejDataParametry[0].IsCONFIG_LOKACE_POVOLITNull() && vydejDataParametry[0].CONFIG_LOKACE_POVOLIT))
				{
					MessageBoxBig.Show("Nelze upravit množství!", "Upozornìní", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
				}
				else
				{
					PerformZmenaMnozstvi();
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		private void PerformZmenaMnozstvi()
		{
			try
			{
				if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListNasnimForm3UpravitMnozstviPolozkuDotaz, Fask.Localization.Localization.Vydej3ListNasnimForm3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
						== DialogResult.No)
					return;

				var vydejDataParametry = Vydej.vydejInstance.globalObject.vydejData.Parametry;

				if (!vydejDataParametry[0].IsCONFIG_LOKACE_POVOLITNull() && vydejDataParametry[0].CONFIG_LOKACE_POVOLIT)
				{
					MessageBoxBig.Show("Z dùvodu aktivního lokaèního mechanizmu, nelze zmìnit množství!", "Oznam", MessageBoxButtons.OK, MessageBoxBigIcon.Information);
					return;
				}

				if (MST_Global.Vydej_FIFOFEFO_Online)
				{
					MessageBoxBig.Show("Z dùvodu aktivního FIFO/FEFO, nelze zmìnit množství!", "Oznam", MessageBoxButtons.OK, MessageBoxBigIcon.Information);
					return;
				}

				 Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable dtSE = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable();

				Vydej.vydejInstance.globalObject.controller_vydej.SE_FillByI_SopnumbeItemnmbrOrd(dtSE, vydejActual.SOPNUMBE,vydejActual.ITEMNMBR,vydejActual.ORD);

				if (dtSE.Count > 0)
				{
					var row = dtSE[0];

					if (row.CZ_SerNum_Track != 0)
					{
						MessageBoxBig.Show("Z dùvodu že položka neni sledována na množství, nelze zmìnit množství!", "Oznam", MessageBoxButtons.OK, MessageBoxBigIcon.Information);
						return;
					}
				}
				else
				{
					MessageBoxBig.Show("Z dùvodu nenalezene položky , nelze zmìnit množství!", "Oznam", MessageBoxButtons.OK, MessageBoxBigIcon.Information);
					return;
				}

				string itemnmbr = vydejActual.ITEMNMBR.Trim();
				string sopnumbe = vydejActual.SOPNUMBE.Trim();
				int ord = vydejActual.ORD;

				decimal tmp = 0;

				while (true)
				{
					using (Forms.SejmiKodForm frm = new SejmiKodForm("Množství", SejmiKodForm.TypeOfCode.Numeric, 0, false, false, vydejActual.QTYSHPPD.ToString()))
					{
						if (frm.ShowDialog() == DialogResult.Cancel)
						{
							return;
						}
						else
						{
							try
							{
								tmp = decimal.Parse(frm.Kod);
								break;
							}
							catch
							{
							}
						}
					}
				}

				//TODO 15.10.2020 TaD Poznamka, tady je asi špatnì logika, ohledne QTYSHPPD a QTYSHPPDMJ

				vydejActual.QTYSHPPD = tmp;
				vydejActual.QTYSHPPDMJ = tmp;

				Vydej.vydejInstance.globalObject.controller_vydej.CZMST_SI_Update_One(vydejActual);

				decimal cnt = Vydej.vydejInstance.globalObject.controller_vydej.SUM_QTYSHPPD_SI(sopnumbe, itemnmbr, ord);

				ListPolozek3.Instance.UpdateDataGrid_Zmena(cnt, itemnmbr, sopnumbe, ord);
				LoadRow(indexItem);
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
		}
    }
}