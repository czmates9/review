using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Konzola._Support_
{
    public class LinkProcess
    {
        public static void Show(LinkLabel linklabel, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string url;
                if (e.Link.LinkData != null)
                    url = e.Link.LinkData.ToString();
                else
                    url = linklabel.Text.Substring(e.Link.Start, e.Link.Length);

                if (!url.Contains("://"))
                    url = "http://" + url;

                var si = new ProcessStartInfo(url);
                Process.Start(si);
                linklabel.LinkVisited = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Show link", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
