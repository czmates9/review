using Fask.Logging;
using FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku.Properties;
using FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku.Vizualizace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku
{
    public partial class pojezdUC : UserControl
    {
        #region Variables

        public TridaDataProKresleni Trida = null;
        public TridaDataProKresleni_vykladka Trida_vykladka = null;

        Graphics GRPH; // grafika
     
        #endregion

        #region Konstruktor
        public pojezdUC()
        {
            InitializeComponent();

            #region buttons
            //btn_linka1.Location = new System.Drawing.Point(53, 12);
            //btn_linka1.Size = new System.Drawing.Size(112, 60);
            //btn_linka1.Text = Common.BTN_Linka1;
            ////btn_linka1.Font = new Font(btn_linka1.Font, btn_linka1.Font.Style | FontStyle.Bold);
            //btn_linka1.Font = new Font(btn_linka1.Font, btn_linka1.Font.Style | FontStyle.Bold);

            //btn_linka2.Location = new System.Drawing.Point(53, 205);
            //btn_linka2.Size = new System.Drawing.Size(112, 60);
            //btn_linka2.Text = Common.BTN_Linka2;

            //btn_linka3.Location = new System.Drawing.Point(53, 270);
            //btn_linka3.Size = new System.Drawing.Size(112, 60);
            //btn_linka3.Text = Common.BTN_Linka3;

            //btn_rv.Location = new System.Drawing.Point(270, 335);
            //btn_rv.Size = new System.Drawing.Size(112, 60);
            //btn_rv.Text = Common.BTN_RV;
            //btn_rv.Font = new Font(btn_rv.Font, btn_rv.Font.Style | FontStyle.Bold); 
            #endregion
        }
        #endregion

        #region Metoda ktera Vykresluje

        public void Draw()
        {
            try
            {

                if (Trida == null)
                    return;


                var bmp = new Bitmap(Resource1.LinkySchema);
                int sirka = bmp.Width;
                int vyska = bmp.Height;

                GRPH = Graphics.FromImage(bmp);

                #region vykresleni ctverec
                #region Parametry
                //Promenna
                int Y0 = 20;
                int Y1 = 110;
                int Y2 = 201;
                int Y3 = 291;
                int Y4 = 381;
                int Y5 = 471;

                //Konstanty
                int X = 200;
                int size = 80;

                Color c = Color.Green;
                int Xa = 205;
                int Ya = 60;
                int offset_Ya = 180;

                int Xa2 = 320;
                int Ya2 = 140;
                int offset_Ya2 = 360;

                Color c_out = Color.Red;
                int Xa_out = 305;
                int Ya_out = 140;
                //int offset_Ya_out = 180;
                int offset_Ya_out = 90;


                Color cp = Color.Orange;
                int Xp = 240;
                int Yp = 190;
                int offset_Yp = 90;

                Color cpd = Color.Orange;
                int Xpd = 240;
                int Ypd = 85;
                int offset_Ypd = 90;

                //Promenna
                string tagText = Trida.tagText;
                int Ytt1 = 40;
                int Ytt2 = 150;
                int Ytt3 = 240;
                int Ytt4 = 330;
                int Ytt5 = 420;
                int Ytt6 = 510;

                //Konstanty
                //int Xtt = 202;
                int Xtt = 220;

                #endregion


                if (Trida.Linka1_run)
                {
                   // SumerMegaMetoda_Linka1(Trida.color, Y0, X, size, c, Xa, Ya, offset_Ya, cpd, Xpd, Ypd, offset_Ypd, tagText, Ytt1, Xtt);

                    DrawCtverec(Trida.color, X, Y0, size);
                    //DrawTextTag(tagText, Xtt, Ytt1);
                    if (Trida.Zapis)
                        DrawSipkaPrava(c, Xa, Ya, 0 * offset_Ya);
                    if (Trida.Pojezd)
                    DrawSipkaDolni(cpd, Xpd, Ypd, 0 * offset_Ypd);
                }
                else if (Trida.strec1_run)
                {
                    DrawCtverec(Trida.color, X, Y1, size);
                }
                else if (Trida.strec2_run)
                {
                    DrawCtverec(Trida.color, X, Y2, size);
                }
                else if (Trida.Linka2_run)
                {
                    DrawCtverec(Trida.color, X, Y3, size);
                }
                else if (Trida.Linka3_run)
                {
                    DrawCtverec(Trida.color, X, Y4, size);
                }
                else if (Trida.rucVst_run)
                {
                    DrawCtverec(Trida.color, X, Y5, size);
                }

                //if(ctverec_enable == true)
                //DrawCtverec(barvaCtverec, ctverec_x, ctverec_y, ctverec_size);





                #endregion

                #region vykresleni sipek
                #region sipky vstup



                if (Trida.Linka2_run && Trida.Zapis)
                {
                    DrawSipkaPrava(c, Xa, Ya, offset_Ya + 90);
                }
                if (Trida.Linka3_run && Trida.Zapis)
                {
                    DrawSipkaPrava(c, Xa, Ya, 2 * offset_Ya);
                }

                


                if (Trida.rucVst_run && Trida.Zapis)
                {
                    DrawSipkaLeva(c, Xa2, Ya2, offset_Ya2);
                }
                #endregion
                #region sipky vystup


                if (Trida.strec1_run)
                {
                    DrawSipkaPrava(c_out, Xa_out, Ya_out, 0 * offset_Ya_out);
                }
                if (Trida.strec2_run)
                {
                    DrawSipkaPrava(c_out, Xa_out, Ya_out, offset_Ya_out);
                }
                #endregion


                #region sipky pojezd
                #region pojezd nahoru


                if (Trida.Linka2_run && Trida.Pojezd) // && Trida.PojezdStrec
                {
                    DrawSipkaHorni(cp, Xp, Yp, 1 * offset_Yp);
                }
                if (Trida.Linka3_run && Trida.Pojezd)
                {
                    DrawSipkaHorni(cp, Xp, Yp, 2 * offset_Yp);
                }
                if (Trida.rucVst_run && Trida.Pojezd)
                {
                    DrawSipkaHorni(cp, Xp, Yp, 3 * offset_Yp);
                }
                
                #endregion
                #region pojezd dolu



                //if (Trida.Linka2_run && Trida.Pojezd && !Trida.PojezdStrec)
                //{
                //    DrawSipkaDolni(cpd, Xpd, Ypd, 2 * offset_Ypd);
                //}
                #endregion

                #endregion


                #endregion

                #region vykresleni popisky
                //Promenna
                int Ys0 = 50;
                //string popis0 = "Linka 1";
                string popis0 = Fask.Constants.AGRO.BTN_Linka1;

                int Ys1 = 230;
                //string popis1 = "Linka 2";
                string popis1 = Fask.Constants.AGRO.BTN_Linka2;
                int Ys2 = 410;
                //string popis2 = "Linka 3";
                string popis2 = Fask.Constants.AGRO.BTN_Linka3;
                string popis3 = "Bocedi 1";
                string popis4 = "Bocedi 2";
                //string popis5 = "Linka 4";
                string popis5 = Fask.Constants.AGRO.BTN_RV;
                string popis6 = "Tisk 1";
                string popis7 = "Tisk 2";

                //Konstanty
                //int Xps = 400; //text old
                ////int Xps = 350;
                //int Yps =140; //text old

                int Xps = 400; //text new
                int Xpt = 500; //text new
                int Yps = 120; //text new
                int offset_Yps =90;


                int Xs = 80;
                //int offset_Xs = 300;
                int offset_XsR = 240;
                int offset_Ys = 90;

                DrawText(popis0, Xs, Ys0);
                DrawText(popis1, Xs, Ys1 + offset_Ys);
                DrawText(popis2, Xs, Ys2);
                DrawTextSTR(popis3, 9, Xps, Yps);
                DrawTextSTR(popis4, 9, Xps, Yps + offset_Yps);
                DrawText(popis5, Xs + offset_XsR, Ys2 + offset_Ys);
                DrawTextSTR(popis6, 9, Xpt, Yps);
                DrawTextSTR(popis7, 9, Xpt, Yps + offset_Yps);
                #endregion

                #region vykresleni cislo tagu

               
                if (Trida.Linka1_run)
                {
                    DrawTextTag(tagText, Xtt, Ytt1);
                }
                else if (Trida.strec1_run)
                {
                    DrawTextTag(tagText, Xtt, Ytt2);
                }
                else if (Trida.strec2_run)
                {
                    DrawTextTag(tagText, Xtt, Ytt3);
                }
                else if (Trida.Linka2_run)
                {
                    DrawTextTag(tagText, Xtt, Ytt4);
                }
                else if (Trida.Linka3_run)
                {
                    DrawTextTag(tagText, Xtt, Ytt5);
                }
                else if (Trida.rucVst_run)
                {
                    DrawTextTag(tagText, Xtt, Ytt6);
                }
                #endregion

                #region Streckovacky ctverce
                //Konstanty
                int ctverec_size = 80;
                int ctverec_x = 70;
                int ctverec_y1 = 110;
                int ctverec_y2 = 201;
                Color barvaSSCC = Color.Brown;
                int textSize = 9;

                if(Trida_vykladka != null) //odkomentovat 17.1. 2022 pri nasazeni do AGRO !!
                {
                    int x_str1_popis_1 = 305;
                    int y_str1_popis_1 = 150;
                    string str1_popis_1 = Trida_vykladka.strec1_Text_1;
                    //string str1_popis_1 = "Text_1";

                    int x_str1_sscc_1 = 310;
                    int y_str1_sscc_1 = 170;
                    string str1_sscc_1 = Trida_vykladka.strec1_SSCC_1;
                    //string str1_sscc_1 = "SSCC_1";

                    int x_str1_popis_2 = 395;
                    int y_str1_popis_2 = 150;
                    string str1_popis_2 = Trida_vykladka.strec1_Text_2;
                    //string str1_popis_2 = "Text_2";

                    int x_str1_sscc_2 = 400;
                    int y_str1_sscc_2 = 170;
                    string str1_sscc_2 = Trida_vykladka.strec1_SSCC_2;
                    //string str1_sscc_2 = "SSCC_2";

                    int x_str1_popis_3 = 485;
                    int y_str1_popis_3 = 150;
                    string str1_popis_3 = Trida_vykladka.strec1_Text_3;
                    //string str1_popis_3 = "Text_3";

                    int x_str1_sscc_3 = 490;
                    int y_str1_sscc_3 = 170;
                    string str1_sscc_3 = Trida_vykladka.strec1_SSCC_3;
                    //string str1_sscc_3 = "SSCC_3";

                    //int x_str1_popis_4 = 575;
                    //int y_str1_popis_4 = 150;
                    string str1_popis_4 = Trida_vykladka.strec1_Text_4;
                    //string str1_popis_4 = "Text_4";

                    //int x_str1_sscc_4 = 580;
                    //int y_str1_sscc_4 = 170;
                    string str1_sscc_4 = Trida_vykladka.strec1_SSCC_4;
                    //string str1_sscc_4 = "SSCC_4";

                    int x_str2_popis_1 = 305;
                    int y_str2_popis_1 = 240;
                    string str2_popis_1 = Trida_vykladka.strec2_Text_1;
                    //string str2_popis_1 = "Text_1";

                    int x_str2_sscc_1 = 310;
                    int y_str2_sscc_1 = 260;
                    string str2_sscc_1 = Trida_vykladka.strec2_SSCC_1;
                    //string str2_sscc_1 = "SSCC_1";

                    int x_str2_popis_2 = 395;
                    int y_str2_popis_2 = 240;
                    string str2_popis_2 = Trida_vykladka.strec2_Text_2;
                    //string str2_popis_2 = "Text_2";

                    int x_str2_sscc_2 = 400;
                    int y_str2_sscc_2 = 260;
                    string str2_sscc_2 = Trida_vykladka.strec2_SSCC_2;
                    //string str2_sscc_2 = "SSCC_2";

                    int x_str2_popis_3 = 485;
                    int y_str2_popis_3 = 240;
                    string str2_popis_3 = Trida_vykladka.strec2_Text_3;
                    //string str2_popis_3 = "Text_3";

                    int x_str2_sscc_3 = 490;
                    int y_str2_sscc_3 = 260;
                    string str2_sscc_3 = Trida_vykladka.strec2_SSCC_3;
                    //string str2_sscc_3 = "SSCC_3";

                    //int x_str2_popis_4 = 575;
                    //int y_str2_popis_4 = 240;
                    string str2_popis_4 = Trida_vykladka.strec2_Text_4;
                    //string str2_popis_4 = "Text_4";

                    //int x_str2_sscc_4 = 580;
                    //int y_str2_sscc_4 = 260;
                    string str2_sscc_4 = Trida_vykladka.strec2_SSCC_4;
                    //string str2_sscc_4 = "SSCC_4";





                    if (true)
                    {
                        //int Xps1 = 400; //text
                        //               //int Xps = 350;
                        //int Yps1 = 140;
                        //int offset_Yps1 = 90;
                        //DrawTextSTR(popis3, 10, Xps1, Yps1);
                        //DrawTextSTR(popis4, 10, Xps1, Yps1 + offset_Yps1);



                        int offset_c = 230;
                        int offset_a = 320;
                        int offset_d = 410;
                        //int offset_b = 500;
                        

                        //strec1 start
                        DrawCtverec(Color.Brown, ctverec_x + offset_c, ctverec_y1, ctverec_size);
                        DrawTextPaleta(str1_popis_1, textSize, barvaSSCC, x_str1_popis_1, y_str1_popis_1);
                        DrawTextPaleta(str1_sscc_1, textSize, barvaSSCC, x_str1_sscc_1, y_str1_sscc_1);

                        DrawCtverec(Color.Brown, ctverec_x + offset_a, ctverec_y1, ctverec_size);
                        DrawTextPaleta(str1_popis_2, textSize, barvaSSCC, x_str1_popis_2, y_str1_popis_2);
                        DrawTextPaleta(str1_sscc_2, textSize, barvaSSCC, x_str1_sscc_2, y_str1_sscc_2);

                        DrawCtverec(Color.Brown, ctverec_x + offset_d, ctverec_y1, ctverec_size);
                        DrawTextPaleta(str1_popis_3, textSize, barvaSSCC, x_str1_popis_3, y_str1_popis_3);
                        DrawTextPaleta(str1_sscc_3, textSize, barvaSSCC, x_str1_sscc_3, y_str1_sscc_3);

                        //pridat-odkomentovat az se vyresi co po vytisknuti 17.1. 2022
                        //DrawCtverec(Color.Brown, ctverec_x + offset_b, ctverec_y1, ctverec_size);
                        //DrawTextPaleta(str1_popis_4, textSize, barvaSSCC, x_str1_popis_4, y_str1_popis_4);
                        //DrawTextPaleta(str1_sscc_4, textSize, barvaSSCC, x_str1_sscc_4, y_str1_sscc_4);

                        //strec2 start
                        DrawCtverec(Color.Brown, ctverec_x + offset_c, ctverec_y2, ctverec_size);
                        DrawTextPaleta(str2_popis_1, textSize, barvaSSCC, x_str2_popis_1, y_str2_popis_1);
                        DrawTextPaleta(str2_sscc_1, textSize, barvaSSCC, x_str2_sscc_1, y_str2_sscc_1);

                        DrawCtverec(Color.Brown, ctverec_x + offset_a, ctverec_y2, ctverec_size);
                        DrawTextPaleta(str2_popis_2, textSize, barvaSSCC, x_str2_popis_2, y_str2_popis_2);
                        DrawTextPaleta(str2_sscc_2, textSize, barvaSSCC, x_str2_sscc_2, y_str2_sscc_2);

                        DrawCtverec(Color.Brown, ctverec_x + offset_d, ctverec_y2, ctverec_size);
                        DrawTextPaleta(str2_popis_3, textSize, barvaSSCC, x_str2_popis_3, y_str2_popis_3);
                        DrawTextPaleta(str2_sscc_3, textSize, barvaSSCC, x_str2_sscc_3, y_str2_sscc_3);

                        //pridat-odkomentovat az se vyresi co po vytisknuti 17.1. 2022
                        //DrawCtverec(Color.Brown, ctverec_x + offset_b, ctverec_y2, ctverec_size);
                        //DrawTextPaleta(str2_popis_4, textSize, barvaSSCC, x_str2_popis_4, y_str2_popis_4);
                        //DrawTextPaleta(str2_sscc_4, textSize, barvaSSCC, x_str2_sscc_4, y_str2_sscc_4);
                    }
                }


                #endregion
                pictureBox1.Image = bmp; // načtení obrazku do picterboxu
                this.GRPH.Dispose(); // pouzit kresleni

            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message.ToString());
            }
        }

        private void SumerMegaMetoda_Linka1(Color colct, int Y0, int X, int size, Color c, int Xa, int Ya, int offset_Ya, Color cpd, int Xpd, int Ypd, int offset_Ypd, string tagText, int Ytt1, int Xtt)
        {
            DrawCtverec(colct, X, Y0, size);
            DrawTextTag(tagText, Xtt, Ytt1);
            DrawSipkaPrava(c, Xa, Ya, 0 * offset_Ya);
            DrawSipkaDolni(cpd, Xpd, Ypd, 0 * offset_Ypd);
        }

        #endregion

        #region Pomocne metody na kresleni
        private void DrawSipkaPrava(Color c, int X, int Y, int offset)
        {
            List<Point> list = new List<Point>();
            list.Add(new Point(X, offset + Y - 10));
            list.Add(new Point(X + 10, offset + Y));
            list.Add(new Point(X, offset + Y + 10));

            GRPH.DrawLine(new Pen(c, 5), X - 45, Y + offset, X, Y + offset);
            SolidBrush drawBrush = new SolidBrush(c);
            GRPH.FillPolygon(drawBrush, list.ToArray());
        }

        private void DrawSipkaLeva(Color c, int X, int Y, int offset)
        {
            List<Point> list = new List<Point>();
            list.Add(new Point(X - 45, offset + Y - 10));
            list.Add(new Point(X - 45 - 10, offset + Y));
            list.Add(new Point(X - 45, offset + Y + 10));

            GRPH.DrawLine(new Pen(c, 5), X - 45, Y + offset, X, Y + offset);
            SolidBrush drawBrush = new SolidBrush(c);
            GRPH.FillPolygon(drawBrush, list.ToArray());
        }

        private void DrawSipkaHorni(Color c, int X, int Y, int offset)
        {
            List<Point> list = new List<Point>();
            list.Add(new Point(X - 10, Y + offset));
            list.Add(new Point(X, Y + offset - 10));
            list.Add(new Point(X + 10, Y + offset));

            GRPH.DrawLine(new Pen(c, 5), X, Y + 30 + offset, X, Y + offset);
            SolidBrush drawBrush = new SolidBrush(c);
            GRPH.FillPolygon(drawBrush, list.ToArray());
        }
        private void DrawSipkaDolni(Color c, int X, int Y, int offset)
        {
            List<Point> list = new List<Point>();
            list.Add(new Point(X - 10, Y + offset + 30));
            list.Add(new Point(X, Y + offset + 40));
            list.Add(new Point(X + 10, Y + offset + 30));

            GRPH.DrawLine(new Pen(c, 5), X, Y + 30 + offset, X, Y + offset);
            SolidBrush drawBrush = new SolidBrush(c);
            GRPH.FillPolygon(drawBrush, list.ToArray());
        }

        private void DrawTextSTR(string popis, int X, int Y)
        {
            Font drawFont = new Font("Arial", 15);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            GRPH.DrawString(popis, drawFont, drawBrush, X, Y);
        }

        private void DrawTextPaleta(string popis, int size, Color clText , int X, int Y)
        {
            Font drawFont = new Font("Arial", size);
            SolidBrush drawBrush = new SolidBrush(clText);
            GRPH.DrawString(popis, drawFont, drawBrush, X, Y);
        }

        private void DrawTextSTR(string popis,int size, int X, int Y)
        {
            Font drawFont = new Font("Arial", size);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            GRPH.DrawString(popis, drawFont, drawBrush, X, Y);
        }

        private void DrawText(string popis, int X, int Y)
        {
            Font drawFont = new Font("Arial", 15);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            GRPH.DrawString(popis, drawFont, drawBrush, X, Y);
        }

        private void DrawTextTag(string popis, int X, int Y)
        {
            //Font drawFont = new Font("Arial", 5);
            Font drawFont = new Font("Arial", 15);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            GRPH.DrawString(popis, drawFont, drawBrush, X, Y);
        }

        private void DrawCtverec(Color c, int X, int Y, int size)
        {
            GRPH.DrawLine(new Pen(c, 5), X, Y, X, size + Y);
            GRPH.DrawLine(new Pen(c, 5), X, size + Y, size + X, size + Y);
            GRPH.DrawLine(new Pen(c, 5), size + X, size + Y, size + X, Y);
            GRPH.DrawLine(new Pen(c, 5), size + X, Y, X, Y);
        }

        #endregion

        #region Archivace
        //private void bt_archivace_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (sender is Button)
        //        {
        //            Button btn = sender as Button;
        //            if (btn.Text.Trim() == Fask.Constants.AGRO.BTN_Linka1)
        //            {
        //                Archivace(1, Fask.Constants.AGRO.BTN_Linka1);
        //            }
        //            else if (btn.Text.Trim() == Fask.Constants.AGRO.BTN_Linka2)
        //            {
        //                Archivace(2, Fask.Constants.AGRO.BTN_Linka2);
        //            }
        //            else if (btn.Text.Trim() == Fask.Constants.AGRO.BTN_Linka3)
        //            {
        //                Archivace(3, Fask.Constants.AGRO.BTN_Linka3);
        //            }
        //            else if (btn.Text.Trim() == Fask.Constants.AGRO.BTN_RV)
        //            {
        //                Archivace(4, Fask.Constants.AGRO.BTN_RV);
        //            }
        //            else if (btn.Text.Trim() == Fask.Constants.AGRO.BTN_Bocedi1)
        //            {
        //                Archivace(21, Fask.Constants.AGRO.BTN_Bocedi1);
        //            }
        //            else if (btn.Text.Trim() == Fask.Constants.AGRO.BTN_Bocedi2)
        //            {
        //                Archivace(22, Fask.Constants.AGRO.BTN_Bocedi2);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        //throw ex;
        //        ExceptionHandler2.Handle(ex);
        //    }
        //}

        //private void Archivace(int cisloTlacitka, string text)
        //{

        //    using (frmArchivaceZaznamu frm = new frmArchivaceZaznamu(cisloTlacitka, text))
        //    {
        //        frm.WindowState = FormWindowState.Maximized;
        //        var dr = frm.ShowDialog();
        //        if (dr == DialogResult.OK)
        //        {
        //        }
        //    }
        //}

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }

    #endregion

}

public class TridaDataProKresleni
    {

        public Color color;

        /// <summary>
        /// Jedná private parametr pro linku 1 pro RUN
        /// </summary>
        private bool _zapis = false;
        /// <summary>
        /// Jedná public parametr pro linku 1 pro RUN
        /// </summary>
        public bool Zapis
        {
            set { _zapis = value; }
            get { return _zapis; }
        }

        /// <summary>
        /// Jedná private parametr pro linku 1 pro RUN
        /// </summary>
        private bool _pojezd = false;
        /// <summary>
        /// Jedná public parametr pro linku 1 pro RUN
        /// </summary>
        public bool Pojezd
        {
            set { _pojezd = value; }
            get { return _pojezd; }
        }

        /// <summary>
        /// Jedná private parametr pro linku 1 pro RUN
        /// </summary>
        private bool _pojezdStrec = false;
        /// <summary>
        /// Jedná public parametr pro linku 1 pro RUN
        /// </summary>
        public bool PojezdStrec
        {
            set { _pojezdStrec = value; }
            get { return _pojezdStrec; }
        }

        /// <summary>
        /// Jedná private parametr pro linku 1 pro RUN
        /// </summary>
        private bool _linka1_run = false;
        /// <summary>
        /// Jedná public parametr pro linku 1 pro RUN
        /// </summary>
        public bool Linka1_run
        {
            set { _linka1_run = value; }
            get { return _linka1_run; }
        }

        /// <summary>
        /// Jedná private parametr pro linku 2 pro RUN
        /// </summary>
        private bool _linka2_run = false;
        /// <summary>
        /// Jedná public parametr pro linku 2 pro RUN
        /// </summary>
        public bool Linka2_run
        {
            set { _linka2_run = value; }
            get { return _linka2_run; }
        }

        /// <summary>
        /// Jedná private parametr pro linku 3 pro RUN
        /// </summary>
        private bool _linka3_run = false;
        /// <summary>
        /// Jedná public parametr pro linku 3 pro RUN
        /// </summary>
        public bool Linka3_run
        {
            set { _linka3_run = value; }
            get { return _linka3_run; }
        }

        /// <summary>
        /// Jedná private parametr pro strec 1 pro RUN
        /// </summary>
        private bool _strec1_run = false;
        /// <summary>
        /// Jedná public parametr pro strec 1 pro RUN
        /// </summary>
        public bool strec1_run
        {
            set { _strec1_run = value; }
            get { return _strec1_run; }
        }

        /// <summary>
        /// Jedná private parametr pro strec 2 pro RUN
        /// </summary>
        private bool _strec2_run = false;
        /// <summary>
        /// Jedná public parametr pro strec 2 pro RUN
        /// </summary>
        public bool strec2_run
        {
            set { _strec2_run = value; }
            get { return _strec2_run; }
        }

        /// <summary>
        /// Jedná private parametr pro rucni vst. pro RUN
        /// </summary>
        private bool _rucVst_run = false;
        /// <summary>
        /// Jedná public parametr pro rucni vst. pro RUN
        /// </summary>
        public bool rucVst_run
        {
            set { _rucVst_run = value; }
            get { return _rucVst_run; }
        }

        /// <summary>
        /// Jedná private parametr pro tagText
        /// </summary>
        private string _tagText = string.Empty;
        /// <summary>
        /// Jedná public parametr pro tagText
        /// </summary>
        public string tagText
        {
            set { _tagText = value; }
            get { return _tagText; }
        }




        public void Clear()
        {
            Zapis = false;
            Pojezd = false;
            Linka1_run = false;
            Linka2_run = false;
            Linka3_run = false;
            strec1_run = false;
            strec2_run = false;
            rucVst_run = false;
            color = Color.Blue;
            PojezdStrec = false;
            //tagText = string.Empty;
        }
    }

    public class TridaDataProKresleni_vykladka
    {

        public Color color;

        /// <summary>
        /// Jedná private parametr pro linku 1 pro RUN
        /// </summary>
        private bool _zapis = false;
        /// <summary>
        /// Jedná public parametr pro linku 1 pro RUN
        /// </summary>
        public bool Zapis
        {
            set { _zapis = value; }
            get { return _zapis; }
        }

        /// <summary>
        /// Jedná private parametr pro linku 1 pro RUN
        /// </summary>
        private bool _pojezd = false;
        /// <summary>
        /// Jedná public parametr pro linku 1 pro RUN
        /// </summary>
        public bool Pojezd
        {
            set { _pojezd = value; }
            get { return _pojezd; }
        }

        /// <summary>
        /// Jedná private parametr pro linku 1 pro RUN
        /// </summary>
        private bool _pojezdStrec = false;
        /// <summary>
        /// Jedná public parametr pro linku 1 pro RUN
        /// </summary>
        public bool PojezdStrec
        {
            set { _pojezdStrec = value; }
            get { return _pojezdStrec; }
        }

        /// <summary>
        /// Jedná private parametr pro linku 1 pro RUN
        /// </summary>
        private bool _linka1_run = false;
        /// <summary>
        /// Jedná public parametr pro linku 1 pro RUN
        /// </summary>
        public bool Linka1_run
        {
            set { _linka1_run = value; }
            get { return _linka1_run; }
        }

        /// <summary>
        /// Jedná private parametr pro linku 2 pro RUN
        /// </summary>
        private bool _linka2_run = false;
        /// <summary>
        /// Jedná public parametr pro linku 2 pro RUN
        /// </summary>
        public bool Linka2_run
        {
            set { _linka2_run = value; }
            get { return _linka2_run; }
        }

        /// <summary>
        /// Jedná private parametr pro linku 3 pro RUN
        /// </summary>
        private bool _linka3_run = false;
        /// <summary>
        /// Jedná public parametr pro linku 3 pro RUN
        /// </summary>
        public bool Linka3_run
        {
            set { _linka3_run = value; }
            get { return _linka3_run; }
        }

        /// <summary>
        /// Jedná private parametr pro strec 1 pro RUN
        /// </summary>
        private bool _strec1_run = false;
        /// <summary>
        /// Jedná public parametr pro strec 1 pro RUN
        /// </summary>
        public bool strec1_run
        {
            set { _strec1_run = value; }
            get { return _strec1_run; }
        }

        /// <summary>
        /// Jedná private parametr pro strec 2 pro RUN
        /// </summary>
        private bool _strec2_run = false;
        /// <summary>
        /// Jedná public parametr pro strec 2 pro RUN
        /// </summary>
        public bool strec2_run
        {
            set { _strec2_run = value; }
            get { return _strec2_run; }
        }

        /// <summary>
        /// Jedná private parametr pro rucni vst. pro RUN
        /// </summary>
        private bool _rucVst_run = false;
        /// <summary>
        /// Jedná public parametr pro rucni vst. pro RUN
        /// </summary>
        public bool rucVst_run
        {
            set { _rucVst_run = value; }
            get { return _rucVst_run; }
        }

        /// <summary>
        /// Jedná private parametr pro tagText
        /// </summary>
        private string _tagText = string.Empty;
        /// <summary>
        /// Jedná public parametr pro tagText
        /// </summary>
        public string tagText
        {
            set { _tagText = value; }
            get { return _tagText; }
        }


        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec1_Text_1 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec1_Text_1
        {
            set { _strec1_Text_1 = value; }
            get { return _strec1_Text_1; }
        }

        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec1_SSCC_1 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec1_SSCC_1
        {
            set { _strec1_SSCC_1 = value; }
            get { return _strec1_SSCC_1; }
        }

        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec1_Text_2 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec1_Text_2
        {
            set { _strec1_Text_2 = value; }
            get { return _strec1_Text_2; }
        }

        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec1_SSCC_2 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec1_SSCC_2
        {
            set { _strec1_SSCC_2 = value; }
            get { return _strec1_SSCC_2; }
        }

        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec1_Text_3 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec1_Text_3
        {
            set { _strec1_Text_3 = value; }
            get { return _strec1_Text_3; }
        }

        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec1_SSCC_3 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec1_SSCC_3
        {
            set { _strec1_SSCC_3 = value; }
            get { return _strec1_SSCC_3; }
        }

        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec1_Text_4 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec1_Text_4
        {
            set { _strec1_Text_4 = value; }
            get { return _strec1_Text_4; }
        }

        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec1_SSCC_4 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec1_SSCC_4
        {
            set { _strec1_SSCC_4 = value; }
            get { return _strec1_SSCC_4; }
        }

        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec2_Text_1 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec2_Text_1
        {
            set { _strec2_Text_1 = value; }
            get { return _strec2_Text_1; }
        }

        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec2_SSCC_1 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec2_SSCC_1
        {
            set { _strec2_SSCC_1 = value; }
            get { return _strec2_SSCC_1; }
        }

        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec2_Text_2 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec2_Text_2
        {
            set { _strec2_Text_2 = value; }
            get { return _strec2_Text_2; }
        }

        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec2_SSCC_2 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec2_SSCC_2
        {
            set { _strec2_SSCC_2 = value; }
            get { return _strec2_SSCC_2; }
        }

        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec2_Text_3 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec2_Text_3
        {
            set { _strec2_Text_3 = value; }
            get { return _strec2_Text_3; }
        }

        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec2_SSCC_3 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec2_SSCC_3
        {
            set { _strec2_SSCC_3 = value; }
            get { return _strec2_SSCC_3; }
        }

        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec2_Text_4 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec2_Text_4
        {
            set { _strec2_Text_4 = value; }
            get { return _strec2_Text_4; }
        }

        /// <summary>
        /// Jedná private parametr pro nazev produktu palety
        /// </summary>
        private string _strec2_SSCC_4 = string.Empty;
        /// <summary>
        /// Jedná public parametr pro nazev produktu palety
        /// </summary>
        public string strec2_SSCC_4
        {
            set { _strec2_SSCC_4 = value; }
            get { return _strec2_SSCC_4; }
        }



        public void Clear_strec1()
        {
            Zapis = false;
            Pojezd = false;
            Linka1_run = false;
            Linka2_run = false;
            Linka3_run = false;
            strec1_run = false;
            strec2_run = false;
            rucVst_run = false;
            color = Color.Blue;
            PojezdStrec = false;
            //tagText = string.Empty;
            strec1_Text_1 = string.Empty;
            strec1_SSCC_1 = string.Empty;
            strec1_Text_2 = string.Empty;
            strec1_SSCC_2 = string.Empty;
            //strec1_Text_3 = string.Empty;
            //strec1_SSCC_3 = string.Empty;
            //strec1_Text_4 = string.Empty;
            //strec1_SSCC_4 = string.Empty;

        }

        public void Clear_strec2()
        {
            Zapis = false;
            Pojezd = false;
            Linka1_run = false;
            Linka2_run = false;
            Linka3_run = false;
            strec1_run = false;
            strec2_run = false;
            rucVst_run = false;
            color = Color.Blue;
            PojezdStrec = false;
            //tagText = string.Empty;
           
            strec2_Text_1 = string.Empty;
            strec2_SSCC_1 = string.Empty;
            strec2_Text_2 = string.Empty;
            strec2_SSCC_2 = string.Empty;
            //strec2_Text_3 = string.Empty;
            //strec2_SSCC_3 = string.Empty;
            //strec2_Text_4 = string.Empty;
            //strec2_SSCC_4 = string.Empty;

        }
    }

