using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Konzola._Printers_
{
    public class PrinterLeitz
    {
        /// <summary>
        /// Overi, zda nazev tiskarny je tiskarna typu Leitz ...
        /// </summary>
        /// <param name="printerName"></param>
        /// <returns></returns>
        public static bool IsLeitz(string printerName)
        {
            // TODO : Konfiguracne podminit pouzivani externiho ovladace specialnich tiskaren jako je napr tato debilni tiskarna ... 
            if (!Konfigurace.Globals_Konfig_Konzola.Konfigurace.Tisk[0].Leitz_Pouzivat)
                return false;

            string[] printersLeitz = LeitzIconLabelStudioCore.SDK.LIPrinterController.GetPrinters();
            return printersLeitz.Contains(printerName);
        }


        public static void Print_ZdrojStav(string printerName, string pathEtiketa, int pocetVytisku, Fask.Interfaces.DataSets.Servis dsServisZdrojStavSelected)
        {
            // provede tisk ...

            try
            {

                LeitzIconLabelStudioCore.SDK.LIPrinterController.SetActivePrinter(printerName);

                bool online = false;
                while (!online)
                {
                    online = LeitzIconLabelStudioCore.SDK.LIPrinterController.IsPrinterOnline();
                    if (!online)
                    {
                        DialogResult drOnlineLietz = System.Windows.Forms.MessageBox.Show("Tiskárna '" + printerName + "' není online!", "Leitz Print", System.Windows.Forms.MessageBoxButtons.AbortRetryIgnore, System.Windows.Forms.MessageBoxIcon.Warning);
                        if (drOnlineLietz == DialogResult.Abort)
                            return;
                        else if (drOnlineLietz == DialogResult.Ignore)
                            break;
                        else //if (drOnlineLietz == DialogResult.Retry)
                            continue;
                    }
                }
                
                LeitzIconSDK.LILabel label = LeitzIconSDK.LILabel.Open(pathEtiketa);
                
                var columns = dsServisZdrojStavSelected.CZMST_Servis_ZdrojStav.Columns;
                foreach (var row in dsServisZdrojStavSelected.CZMST_Servis_ZdrojStav)
                {
                    var z = dsServisZdrojStavSelected.CZMST_Servis_Zdroj.FindByID(row.IDZdroj);
                    if (z != null)
                    {
                        foreach (System.Data.DataColumn zcol in z.Table.Columns)
                        {
                            try
                            {
                                var el = label.Elements.Find(x => x.Id == zcol.ColumnName);
                                if (el is LeitzIconSDK.LITextLabelElement)
                                    ((LeitzIconSDK.LITextLabelElement)el).Value = z[zcol].ToString();
                                else if (el is LeitzIconSDK.LIBarcodeLabelElement)
                                    ((LeitzIconSDK.LIBarcodeLabelElement)el).Value = z[zcol].ToString();
                            }
                            catch (Exception eLeitz) 
                            {
                                System.Windows.Forms.MessageBox.Show(eLeitz.Message, "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                            }
                        }
                    }

                    foreach (System.Data.DataColumn col in columns)
                    {
                        try
                        {
                            var el = label.Elements.Find(x => x.Id == col.ColumnName);
                            if (el is LeitzIconSDK.LITextLabelElement)
                                ((LeitzIconSDK.LITextLabelElement)el).Value = row[col].ToString();
                            else if (el is LeitzIconSDK.LIBarcodeLabelElement)
                                ((LeitzIconSDK.LIBarcodeLabelElement)el).Value = row[col].ToString();
                        }
                        catch (Exception eLeitz) 
                        {
                            System.Windows.Forms.MessageBox.Show(eLeitz.Message, "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        }
                    }

#if DEBUG
                    var labelBitMap = label.GetLabelBitMap();
                    string pathLogEtikety = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogEtikety");
                    if (!Directory.Exists(pathLogEtikety))
                        Directory.CreateDirectory(pathLogEtikety);
                    labelBitMap.Save(Path.Combine(pathLogEtikety, DateTime.Now.ToFileTime().ToString() + ".png"), System.Drawing.Imaging.ImageFormat.Png);
#endif

                    try
                    {
                        LeitzIconLabelStudioCore.SDK.LIPrinterController.Print(label, (uint)pocetVytisku);
                    }
                    catch (Exception exLeitzPrint)
                    {
                        DialogResult drExLeitzPrint = MessageBox.Show(exLeitzPrint.Message + "\n\n" + "Pokračovat v tisku další etiketou?", "Leitz Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                        if (drExLeitzPrint == DialogResult.Cancel)
                            return;
                    }
                }

                System.Windows.Forms.MessageBox.Show("Všechny etikety odeslány na tiskárnu", "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                // LeitzIconLabelStudioCore.SDK.LIPrinterController.Print

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }

        public static void Print_Zdroj(string printerName, string pathEtiketa, int pocetVytisku, Fask.Interfaces.DataSets.Servis dsServisZdrojStavSelected)
        {
            // provede tisk ...

            try
            {

                LeitzIconLabelStudioCore.SDK.LIPrinterController.SetActivePrinter(printerName);

                bool online = false;
                while (!online)
                {
                    online = LeitzIconLabelStudioCore.SDK.LIPrinterController.IsPrinterOnline();
                    if (!online)
                    {
                        DialogResult drOnlineLietz = System.Windows.Forms.MessageBox.Show("Tiskárna '" + printerName + "' není online!", "Leitz Print", System.Windows.Forms.MessageBoxButtons.AbortRetryIgnore, System.Windows.Forms.MessageBoxIcon.Warning);
                        if (drOnlineLietz == DialogResult.Abort)
                            return;
                        else if (drOnlineLietz == DialogResult.Ignore)
                            break;
                        else //if (drOnlineLietz == DialogResult.Retry)
                            continue;
                    }
                }

                LeitzIconSDK.LILabel label = LeitzIconSDK.LILabel.Open(pathEtiketa);

                var columns = dsServisZdrojStavSelected.CZMST_Servis_Zdroj.Columns;
                foreach (System.Data.DataRow row in dsServisZdrojStavSelected.CZMST_Servis_Zdroj)
                {
                    foreach (System.Data.DataColumn col in columns)
                    {
                        try
                        {
                            var el = label.Elements.Find(x => x.Id == col.ColumnName);
                            if (el is LeitzIconSDK.LITextLabelElement)
                                ((LeitzIconSDK.LITextLabelElement)el).Value = row[col].ToString();
                            else if (el is LeitzIconSDK.LIBarcodeLabelElement)
                                ((LeitzIconSDK.LIBarcodeLabelElement)el).Value = row[col].ToString();
                        }
                        catch (Exception eLeitz) 
                        {
                            System.Windows.Forms.MessageBox.Show(eLeitz.Message, "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        }
                    }

#if DEBUG
                    var labelBitMap = label.GetLabelBitMap();
                    string pathLogEtikety = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogEtikety");
                    if (!Directory.Exists(pathLogEtikety))
                        Directory.CreateDirectory(pathLogEtikety);
                    labelBitMap.Save(Path.Combine(pathLogEtikety, DateTime.Now.ToFileTime().ToString() + ".png"), System.Drawing.Imaging.ImageFormat.Png);
#endif

                    try
                    {
                        LeitzIconLabelStudioCore.SDK.LIPrinterController.Print(label, (uint)pocetVytisku);
                    }
                    catch (Exception exLeitzPrint)
                    {
                        DialogResult drExLeitzPrint = MessageBox.Show(exLeitzPrint.Message + "\n\n" + "Pokračovat v tisku další etiketou?", "Leitz Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                        if (drExLeitzPrint == DialogResult.Cancel)
                            return;
                    }
                }

                System.Windows.Forms.MessageBox.Show("Všechny etikety odeslány na tiskárnu", "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                // LeitzIconLabelStudioCore.SDK.LIPrinterController.Print

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }

        public static void Print_Odberatel(string printerName, string pathEtiketa, int pocetVytisku, Fask.Interfaces.DataSets.Odberatele dsOdberatleSelected)
        {
            // provede tisk ...

            try
            {

                LeitzIconLabelStudioCore.SDK.LIPrinterController.SetActivePrinter(printerName);

                bool online = false;
                while (!online)
                {
                    online = LeitzIconLabelStudioCore.SDK.LIPrinterController.IsPrinterOnline();
                    if (!online)
                    {
                        DialogResult drOnlineLietz = System.Windows.Forms.MessageBox.Show("Tiskárna '" + printerName + "' není online!", "Leitz Print", System.Windows.Forms.MessageBoxButtons.AbortRetryIgnore, System.Windows.Forms.MessageBoxIcon.Warning);
                        if (drOnlineLietz == DialogResult.Abort)
                            return;
                        else if (drOnlineLietz == DialogResult.Ignore)
                            break;
                        else //if (drOnlineLietz == DialogResult.Retry)
                            continue;
                    }
                }

                LeitzIconSDK.LILabel label = LeitzIconSDK.LILabel.Open(pathEtiketa);

                var columns = dsOdberatleSelected.CZMST090.Columns;
                foreach (System.Data.DataRow row in dsOdberatleSelected.CZMST090)
                {
                    foreach (System.Data.DataColumn col in columns)
                    {
                        try
                        {
                            var el = label.Elements.Find(x => x.Id == col.ColumnName);
                            if (el is LeitzIconSDK.LITextLabelElement)
                                ((LeitzIconSDK.LITextLabelElement)el).Value = row[col].ToString();
                            else if (el is LeitzIconSDK.LIBarcodeLabelElement)
                                ((LeitzIconSDK.LIBarcodeLabelElement)el).Value = row[col].ToString();
                        }
                        catch (Exception eLeitz) 
                        {
                            System.Windows.Forms.MessageBox.Show(eLeitz.Message, "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        }
                    }


#if DEBUG
                    var labelBitMap = label.GetLabelBitMap();
                    string pathLogEtikety = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogEtikety");
                    if (!Directory.Exists(pathLogEtikety))
                        Directory.CreateDirectory(pathLogEtikety);
                    labelBitMap.Save(Path.Combine(pathLogEtikety, DateTime.Now.ToFileTime().ToString() + ".png"), System.Drawing.Imaging.ImageFormat.Png);
#endif

                    try
                    {
                        LeitzIconLabelStudioCore.SDK.LIPrinterController.Print(label, (uint)pocetVytisku);
                    }
                    catch (Exception exLeitzPrint)
                    {
                        DialogResult drExLeitzPrint = MessageBox.Show(exLeitzPrint.Message + "\n\n" + "Pokračovat v tisku další etiketou?", "Leitz Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                        if (drExLeitzPrint == DialogResult.Cancel)
                            return;
                    }
                }

                System.Windows.Forms.MessageBox.Show("Všechny etikety odeslány na tiskárnu", "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                // LeitzIconLabelStudioCore.SDK.LIPrinterController.Print

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }

        public static void Print_Okruh(string printerName, string pathEtiketa, int pocetVytisku, Fask.Interfaces.DataSets.Servis dsServisOkruhSelected)
        {
            // provede tisk ...

            try
            {

                LeitzIconLabelStudioCore.SDK.LIPrinterController.SetActivePrinter(printerName);

                bool online = false;
                while (!online)
                {
                    online = LeitzIconLabelStudioCore.SDK.LIPrinterController.IsPrinterOnline();
                    if (!online)
                    {
                        DialogResult drOnlineLietz = System.Windows.Forms.MessageBox.Show("Tiskárna '" + printerName + "' není online!", "Leitz Print", System.Windows.Forms.MessageBoxButtons.AbortRetryIgnore, System.Windows.Forms.MessageBoxIcon.Warning);
                        if (drOnlineLietz == DialogResult.Abort)
                            return;
                        else if (drOnlineLietz == DialogResult.Ignore)
                            break;
                        else //if (drOnlineLietz == DialogResult.Retry)
                            continue;
                    }
                }

                LeitzIconSDK.LILabel label = LeitzIconSDK.LILabel.Open(pathEtiketa);

                var columns = dsServisOkruhSelected.CZMST_Servis_Okruh.Columns;
                foreach (System.Data.DataRow row in dsServisOkruhSelected.CZMST_Servis_Okruh)
                {
                    foreach (System.Data.DataColumn col in columns)
                    {
                        try
                        {
                            var el = label.Elements.Find(x => x.Id == col.ColumnName);
                            if (el is LeitzIconSDK.LITextLabelElement)
                                ((LeitzIconSDK.LITextLabelElement)el).Value = row[col].ToString();
                            else if (el is LeitzIconSDK.LIBarcodeLabelElement)
                                ((LeitzIconSDK.LIBarcodeLabelElement)el).Value = row[col].ToString();
                        }
                        catch (Exception eLeitz) 
                        {
                            System.Windows.Forms.MessageBox.Show(eLeitz.Message, "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        }
                    }


#if DEBUG
                    var labelBitMap = label.GetLabelBitMap();
                    string pathLogEtikety = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogEtikety");
                    if (!Directory.Exists(pathLogEtikety))
                        Directory.CreateDirectory(pathLogEtikety);
                    labelBitMap.Save(Path.Combine(pathLogEtikety, DateTime.Now.ToFileTime().ToString() + ".png"), System.Drawing.Imaging.ImageFormat.Png);
#endif

                    try
                    {
                        LeitzIconLabelStudioCore.SDK.LIPrinterController.Print(label, (uint)pocetVytisku);
                    }
                    catch (Exception exLeitzPrint)
                    {
                        DialogResult drExLeitzPrint = MessageBox.Show(exLeitzPrint.Message + "\n\n" + "Pokračovat v tisku další etiketou?", "Leitz Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                        if (drExLeitzPrint == DialogResult.Cancel)
                            return;
                    }
                }

                System.Windows.Forms.MessageBox.Show("Všechny etikety odeslány na tiskárnu", "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                // LeitzIconLabelStudioCore.SDK.LIPrinterController.Print

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }

        public static void Print_Uzivatel(string printerName, string pathEtiketa, int pocetVytisku, FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable dtUzivateleSelected)
        {
            // provede tisk ...

            try
            {

                LeitzIconLabelStudioCore.SDK.LIPrinterController.SetActivePrinter(printerName);

                bool online = false;
                while (!online)
                {
                    online = LeitzIconLabelStudioCore.SDK.LIPrinterController.IsPrinterOnline();
                    if (!online)
                    {
                        DialogResult drOnlineLietz = System.Windows.Forms.MessageBox.Show("Tiskárna '" + printerName + "' není online!", "Leitz Print", System.Windows.Forms.MessageBoxButtons.AbortRetryIgnore, System.Windows.Forms.MessageBoxIcon.Warning);
                        if (drOnlineLietz == DialogResult.Abort)
                            return;
                        else if (drOnlineLietz == DialogResult.Ignore)
                            break;
                        else //if (drOnlineLietz == DialogResult.Retry)
                            continue;
                    }
                }

                LeitzIconSDK.LILabel label = LeitzIconSDK.LILabel.Open(pathEtiketa);

                var columns = dtUzivateleSelected.Columns;
                foreach (System.Data.DataRow row in dtUzivateleSelected)
                {
                    foreach (System.Data.DataColumn col in columns)
                    {
                        try
                        {
                            var el = label.Elements.Find(x => x.Id == col.ColumnName);
                            if (el is LeitzIconSDK.LITextLabelElement)
                                ((LeitzIconSDK.LITextLabelElement)el).Value = row[col].ToString();
                            else if (el is LeitzIconSDK.LIBarcodeLabelElement)
                                ((LeitzIconSDK.LIBarcodeLabelElement)el).Value = row[col].ToString();
                        }
                        catch (Exception eLeitz) 
                        {
                            System.Windows.Forms.MessageBox.Show(eLeitz.Message, "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        }
                    }


#if DEBUG
                    var labelBitMap = label.GetLabelBitMap();
                    string pathLogEtikety = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogEtikety");
                    if (!Directory.Exists(pathLogEtikety))
                        Directory.CreateDirectory(pathLogEtikety);
                    labelBitMap.Save(Path.Combine(pathLogEtikety, DateTime.Now.ToFileTime().ToString() + ".png"), System.Drawing.Imaging.ImageFormat.Png);
#endif

                    try
                    {
                        LeitzIconLabelStudioCore.SDK.LIPrinterController.Print(label, (uint)pocetVytisku);
                    }
                    catch (Exception exLeitzPrint)
                    {
                        DialogResult drExLeitzPrint = MessageBox.Show(exLeitzPrint.Message + "\n\n" + "Pokračovat v tisku další etiketou?", "Leitz Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                        if (drExLeitzPrint == DialogResult.Cancel)
                            return;
                    }
                }

                System.Windows.Forms.MessageBox.Show("Všechny etikety odeslány na tiskárnu", "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                // LeitzIconLabelStudioCore.SDK.LIPrinterController.Print

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Leitz Print", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }

    }
}
