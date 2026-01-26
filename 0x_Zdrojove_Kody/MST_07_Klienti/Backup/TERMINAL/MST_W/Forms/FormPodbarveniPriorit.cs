using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fask.MST_W.Forms
{
    public partial class FormPodbarveniPriorit : System.Windows.Forms.Form
    {
        public FormPodbarveniPriorit()
        {
            InitializeComponent();

            MyCreateGridStyles();

            MyInitializeGrid();
        }

        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private Fask.MST_W.Forms.PodbarveniPriorit.DataGrid2TextBoxColumnColor dataGridTextBoxColumn2;

        private void MyCreateGridStyles()
        {
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new Fask.MST_W.Forms.PodbarveniPriorit.DataGrid2TextBoxColumnColor();

            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.MappingName = "PriorityColor";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Priorita";
            this.dataGridTextBoxColumn1.MappingName = "Priority";
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "Podbarvení";
            this.dataGridTextBoxColumn2.MappingName = "Color";
            this.dataGridTextBoxColumn2.Width = 100;

            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);

        }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.WrkDir, this.GetType().ToString()));
        }


        private void FormPodbarveniPriorit_Resize(object sender, EventArgs e)
        {
        }

        private void FormPodbarveniPriorit_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = FormLocation.ScreenResolution;
            panelButtons_Resize(null, null);

            ColorsLoad();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            try
            {
                Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
                buttonStorno.Size = nsize;
                //buttonOK.Size = nsize;
            }
            catch
            {
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void finalize()
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;

            this.dataGrid1.Save(Path.Combine(Main.WrkDir, this.GetType().ToString()));
        }

        private void PerformOK()
        {
            ColorsSave();
            finalize();
            DialogResult = DialogResult.OK;
        }

        private void PerformStorno()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        private void ColorsSave()
        {
            //Ulozit do Config adresare do prioritycolors.xml 
            try
            {
                priorityColors.WriteXml(Main.ConfigPriorityColors, XmlWriteMode.IgnoreSchema);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            MST_Global.PriorityColors = priorityColors;            
        }

        private void ColorsLoad()
        {
            //nacist barvicky
            try
            {
                priorityColors.ReadXml(Main.ConfigPriorityColors, XmlReadMode.IgnoreSchema);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void dataGrid1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (priorityColorsBindingSource.Current == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.FormsFormPodbarveniPrioritNeniVybratRadek, Fask.Localization.Localization.FormsFormPodbarveniPrioritNeniZmenaBarvyPriority, MessageBoxButtons.OK);
                    return;
                }

                Config.PriorityColors.PriorityColorRow prow = (priorityColorsBindingSource.Current as System.Data.DataRowView).Row as Config.PriorityColors.PriorityColorRow;
                using (ColorPicker.ColorPicker c = new ColorPicker.ColorPicker())
                {
                    if (c.ShowDialog() == DialogResult.Cancel)
                        return;
                    prow.Color = c.ReturnColor.ToArgb();
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            PerformStorno();
        }

        private void menuItemSmazat_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataRowView drowview = priorityColorsBindingSource.Current as System.Data.DataRowView;
                Config.PriorityColors.PriorityColorRow prow = drowview.Row as Config.PriorityColors.PriorityColorRow;
                if (prow != null)
                    prow.Delete();

                priorityColors.AcceptChanges();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void menuItemPridat_Click(object sender, EventArgs e)
        {
            try
            {
                Color prioritaColor = Color.Empty;
                int prioritaKey = 0;
                string priorita = string.Empty;
                try
                {
                    prioritaKey = (int)priorityColors.PriorityColor.Compute("MAX(Priority)", null) + 1;
                    priorita = prioritaKey.ToString();
                }
                catch { }

                while (true)
                {
                    if (DialogResult.Cancel == Forms.InputBox.Show(Fask.Localization.Localization.FormsFormPodbarveniPrioritZadejteCisloPriority, priorita, out priorita, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric))
                        return;

                    try
                    {
                        prioritaKey = int.Parse(priorita);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxBig.Show(ex.Message, Fask.Localization.Localization.FormsFormPodbarveniPrioritKontrolaVstupu, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        continue;
                    }

                    try
                    {
                        using (ColorPicker.ColorPicker c = new ColorPicker.ColorPicker())
                        {
                            if (c.ShowDialog() == DialogResult.OK)
                            {
                                prioritaColor = c.ReturnColor;
                            }
                            else
                            {
                                prioritaColor = Color.Empty;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxBig.Show(ex.Message, Fask.Localization.Localization.FormsFormPodbarveniPrioritVyberBarvy, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        continue;
                    }

                    try
                    {
                        priorityColors.PriorityColor.AddPriorityColorRow(prioritaKey, prioritaColor.ToArgb());
                    }
                    catch (Exception ex)
                    {
                        MessageBoxBig.Show(ex.Message, Fask.Localization.Localization.FormsFormPodbarveniPrioritVlozeniZaznamu, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        continue;
                    }
                    break;
                }                
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void FormPodbarveniPriorit_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void FormPodbarveniPriorit_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }


    }

}