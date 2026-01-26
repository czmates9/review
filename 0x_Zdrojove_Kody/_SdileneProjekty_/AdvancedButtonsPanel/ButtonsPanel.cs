using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fask.AdvancedButtonsPanel
{
    [System.ComponentModel.DesignerCategory("")]
   public class ButtonsPanel : System.Windows.Forms.Panel
    {
        public System.Windows.Forms.MenuStrip Menu;

        public Dictionary<string, Button> ListButtons;
        public Dictionary<string, System.Windows.Forms.ToolStripMenuItem> MeniItems;
        public Dictionary<string, MenuItem> CMItem;
        public Dictionary<string, bool> VisibleBTN;

        private ContextMenu CM;

        private bool Flag_Init;


        /// <summary>
        /// konstruktor
        /// </summary>
        public ButtonsPanel()
            : base() 
        {
            ListButtons = new Dictionary<string,Button>();
            MeniItems = new Dictionary<string, ToolStripMenuItem>();
            CM = new ContextMenu();
            CMItem = new Dictionary<string, MenuItem>();
            VisibleBTN = new Dictionary<string, bool>();
            this.AutoScroll = true;
       }


        /// <summary>
        /// Inicializace slouží pro načteni všech tlačitek podle konfigu
        /// </summary>
        public void Init() 
        {
            try
            {
                if (VisibleBTN.Count == 0)
                    Flag_Init = true;
                else
                    Flag_Init = false;

                LoadButtons();
                AddControls();

                Flag_Init = false;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Začatek rekurzivniho prohledavani Menu aby se načetly všechny tlačitka
        /// </summary>
        private void LoadButtons()
        {
            try
            {
                foreach (var item in Menu.Items)
                {
                    string txt = ((ToolStripMenuItem)item).Text;
                    isButton(item, txt);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Rekurzivni metoda pro prohledavani menu
        /// </summary>
        /// <param name="item"></param>
        /// <param name="Text"></param>
        private void isButton(object item, string Text)
        {
            try
            {
                if (item is System.Windows.Forms.ToolStripSeparator)
                    return;

                if (((ToolStripMenuItem)item).DropDownItems.Count == 0)
                {
                    //if (Flag_Init)
                    //    VisibleBTN.Add(((ToolStripMenuItem)item).Name, true);

                    if (Flag_Init)
                        VisibleBTN.Add(((ToolStripMenuItem)item).Name, ((ToolStripMenuItem)item).Enabled);
                    
                    
                    MeniItems.Add(((ToolStripMenuItem)item).Name, (ToolStripMenuItem)item);

                    SetButton(((ToolStripMenuItem)item).Name, ((ToolStripMenuItem)item).Text);

                    //Text = Text + "->" + ((ToolStripMenuItem)item).Text;
                    SetCM(((ToolStripMenuItem)item).Name, Text);

                    return;
                }
                else
                {
                    foreach (var tmpitem in ((ToolStripMenuItem)item).DropDownItems)
                    {
                        if (tmpitem is ToolStripSeparator)
                            continue;

                        if (tmpitem is ToolStripMenuItem)
                        {
                            //if (((ToolStripMenuItem)tmpitem).Visible == false)
                            //    continue;

                            string text = Text.Trim() + "->" + ((ToolStripMenuItem)tmpitem).Text.Trim();

                            isButton(tmpitem, text);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

            return;
        }

        /// <summary>
        /// Metoda pro načteni pripravenych tlačitek do Panelu
        /// </summary>
        private void AddControls() 
        {
            try
            {
                foreach (var key in ListButtons)
                {
                    this.Controls.Add(ListButtons[key.Key]);
                }

                this.ContextMenu = CM;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Metoda pro vytvořeni ContextMenu položek
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Text"></param>
        private void SetCM(string Name, string Text)
        {
            try
            {
                MenuItem mi = new MenuItem();
                mi.Name = Name;
                mi.Text = Text;

                if (VisibleBTN.Count == 0)
                    mi.Checked = true;
                else
                    mi.Checked = VisibleBTN[Name]; 

                    //mi.Visible = V;

                //btn.Dock = DockStyle.Top;
                //btn.Width = this.Width;
                //btn.Height = this.Width;
                mi.Click += new EventHandler(MyMenuItemClick);

                CMItem.Add(Name, mi);
                CM.MenuItems.Add(mi);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Metoda pro vytvořeni Buttonu
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Text"></param>
        private void SetButton(string Name, string Text)
        {

            try
            {
                Button btn = new Button();
                btn.Name = Name;
                btn.Text = Text;
                btn.Dock = DockStyle.Top;
                btn.Width = this.Width;
                btn.Height = this.Width;

                //if (V)
                //{
                if (VisibleBTN.Count == 0)
                        btn.Visible = true;
                else
                    btn.Visible = VisibleBTN[Name];
                //{
                //    if(Name == "tsmiArchivaceVybrane")
                //    {

                //        //if (FASK.Logins.Uzivatel.Instance.GetPravaKonzole_IT_Arch())
                //        //{
                //            //MessageBox.Show("Uživatel nemá dostatečná oprávnění!", "Upozornení", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                //            btn.Visible = true;
                //        //}

                       
                //    }
                //    else
                //    {
                //        btn.Visible = VisibleBTN[Name];
                //    }
                //}
                        
                //}
                //else
                //    btn.Visible = false;


                btn.Click += new EventHandler(MyButtonClick);

                ListButtons.Add(Name, btn);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

        }

        /// <summary>
        /// Event pro tlačitka, je jeden a podle nazvu tlačitka ktere to poslalo se deje co se ma...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MyButtonClick(object sender,EventArgs e)
        {

            try
            {
                MeniItems[(sender as Button).Name].PerformClick();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Event pro ContextMenu, je jeden a podle nazvu ContextMenu ktere to poslalo se deje co se ma...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MyMenuItemClick(object sender, EventArgs e)
        {

            try
            {
                CMItem[(sender as MenuItem).Name].Checked = !CMItem[(sender as MenuItem).Name].Checked;

                ListButtons[(sender as MenuItem).Name].Visible = CMItem[(sender as MenuItem).Name].Checked;
                VisibleBTN[(sender as MenuItem).Name] = CMItem[(sender as MenuItem).Name].Checked;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        

    }
}
