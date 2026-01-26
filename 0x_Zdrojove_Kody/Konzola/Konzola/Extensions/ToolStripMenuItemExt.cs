using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Konzola.Extensions
{
    public static class ToolStripMenuItemExt
    {
        /// <summary>
        /// Automaticky nastavi visible a enabled na vybranou hodnotu.
        /// </summary>
        /// <param name="item">Vybrana komponenta</param>
        /// <param name="visible"></param>
        public static void ResolveVisibleComponent(this ToolStripMenuItem item, bool visible)
        {
            try
            {
                item.Visible = visible;
                //item.Enabled = visible;
            }
            catch { }
        }

        /// <summary>
        /// Automaticky nastavi visible a enabled na vybranou hodnotu.
        /// </summary>
        /// <param name="item">Vybrana komponenta</param>
        /// <param name="visible"></param>
        public static void ResolveEnableComponent(this ToolStripMenuItem item, bool visible)
        {
            try
            {
                item.Enabled = visible;
            }
            catch { }
        }



        public static string GetPathToForm(this ToolStripItem item)
        {

            string PathToForm = string.Empty;

            try
            {

                PathToForm = IsOWNER(item.Text, item);

            }
            catch(Exception ex) 
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }

            return PathToForm;
        }



        private static string IsOWNER(string Text,System.Windows.Forms.ToolStripItem sender)
        {
            if (sender.OwnerItem != null)
            {
                return IsOWNER(sender.OwnerItem.Text + "." + Text, sender.OwnerItem);
            }
            else
            {
                return Text;
            }

        }
    }
}
