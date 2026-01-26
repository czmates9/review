using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Data;
using System.Runtime.CompilerServices;

namespace Konzola.Extensions
{
    [Serializable]
    public sealed class ColumnInfo
    {
        public string Name { get; set; }
        public int DisplayIndex { get; set; }
        public int Width { get; set; }
        public bool Visible { get; set; }
        public string HeaderText { get; set; }
    }

    public static class AdvanceDataGridViewExtenstions
    {
        /// <summary>
        /// Loads columns information from the specified XML file
        /// </summary>
        /// <param name="dgv">DataGridView control instance</param>
        /// <param name="fileName">XML configuration file</param>
        public static void LoadConfiguration(this Zuby.ADGV.AdvancedDataGridView dgv, string fileName)
        {
            if (dgv == null)
                return;

            Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();

            if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].DataGridVazatNaUzivatele && FASK.Logins.Uzivatel.Instance.UserID != null)
                fileName = FASK.Logins.Uzivatel.Instance.UserID.Trim() + fileName;

            DataGridViewColumnSelector ds = new DataGridViewColumnSelector(dgv);
            fileName = Path.Combine(MySystem.MyPath.ConfigDirectory, fileName + ".xml");
            // zapnuti DoubleBuffered kvuli zrychleni datagridu
            typeof(Zuby.ADGV.AdvancedDataGridView).InvokeMember(
                        "DoubleBuffered",
                        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                        null,
                        dgv,
                        new object[] { true });

            if (!File.Exists(fileName))
                return;
            List<ColumnInfo> columns;
            using (var streamReader = new StreamReader(fileName))
            {
                
                //var xmlSerializer = new XmlSerializer(typeof(List<ColumnInfo>));
                XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(List<ColumnInfo>) })[0];

                columns = (List<ColumnInfo>)xmlSerializer.Deserialize(streamReader);
            }
            foreach (var column in columns)
            {
                dgv.Columns[column.Name].DisplayIndex = column.DisplayIndex;
                dgv.Columns[column.Name].Width = column.Width;
                dgv.Columns[column.Name].Visible = column.Visible;
                if (!string.IsNullOrEmpty(column.HeaderText))
                    dgv.Columns[column.Name].HeaderText = column.HeaderText;
            }
        }

        /// <summary>
        /// Saves columns information to the specified XML file
        /// </summary>
        /// <param name="dgv">DataGridView control instance</param>
        /// <param name="fileName">XML configuration file</param>
        public static void SaveConfiguration(this Zuby.ADGV.AdvancedDataGridView dgv, string fileName)
        {
            try
            {
                Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].DataGridVazatNaUzivatele && FASK.Logins.Uzivatel.Instance.UserID != null)
                    fileName = FASK.Logins.Uzivatel.Instance.UserID.Trim() + fileName;

                fileName = Path.Combine(MySystem.MyPath.ConfigDirectory, fileName + ".xml");

                var columns = new List<ColumnInfo>();
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    var column = new ColumnInfo();
                    column.Name = dgv.Columns[i].Name;
                    column.DisplayIndex = dgv.Columns[i].DisplayIndex;
                    column.Width = dgv.Columns[i].Width;
                    column.Visible = dgv.Columns[i].Visible;
                    column.HeaderText = dgv.Columns[i].HeaderText;
                    columns.Add(column);
                }
                using (var streamWriter = new StreamWriter(fileName))
                {
                    //var xmlSerializer = new XmlSerializer(typeof(List<ColumnInfo>));
                    XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(List<ColumnInfo>) })[0];
                    xmlSerializer.Serialize(streamWriter, columns);
                }
            }
            catch (System.UnauthorizedAccessException ex)
            {
                string msg = string.Format("Uživatel přihlášen od Windows, nemá dostatnečná práva zápisu uživatelských nastavení." + 
                    Environment.NewLine +
                    Environment.NewLine +
                    "Pro vyřešení tohoto problému, kontaktuj vašeho správce IT! " + 
                    Environment.NewLine +
                    Environment.NewLine +
                    "Chyba nastala u souboru: '{0}'", fileName);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex,true);
            }
        }


    }

    #region DataGridViewColumnSelector (skryti a preusporadani sloupcu)
    class DataGridViewColumnSelector
    {
        // the DataGridView to which the DataGridViewColumnSelector is attached
        private Zuby.ADGV.AdvancedDataGridView mDataGridView = null;
        // a CheckedListBox containing the column header text and checkboxes
        private CheckedListBox mCheckedListBox;
        // a ToolStripDropDown object used to show the popup
        private ToolStripDropDown mPopup;

        /// <summary>
        /// The max height of the popup
        /// </summary>
        public int MaxHeight = 300;
        /// <summary>
        /// The width of the popup
        /// </summary>
        public int Width = 200;

        /// <summary>
        /// Gets or sets the DataGridView to which the DataGridViewColumnSelector is attached
        /// </summary>
        public Zuby.ADGV.AdvancedDataGridView DataGridView
        {
            get { return mDataGridView; }
            set
            {
                // If any, remove handler from current DataGridView 
                if (mDataGridView != null) mDataGridView.CellMouseClick -= new DataGridViewCellMouseEventHandler(mDataGridView_CellMouseClick);
                // Set the new DataGridView
                mDataGridView = value;
                // Attach CellMouseClick handler to DataGridView
                if (mDataGridView != null) mDataGridView.CellMouseClick += new DataGridViewCellMouseEventHandler(mDataGridView_CellMouseClick);
            }
        }

        // When user right-clicks the cell origin, it clears and fill the CheckedListBox with
        // columns header text. Then it shows the popup. 
        // In this way the CheckedListBox items are always refreshed to reflect changes occurred in 
        // DataGridView columns (column additions or name changes and so on).
        void mDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right && e.RowIndex == -1 && e.ColumnIndex == -1)
            if ((e.Button == MouseButtons.Right && e.RowIndex == -1))
            {
                mCheckedListBox.Items.Clear();
                foreach (DataGridViewColumn c in mDataGridView.Columns)
                {
                    mCheckedListBox.Items.Add(c.HeaderText, c.Visible);
                }
                int PreferredHeight = (mCheckedListBox.Items.Count * 16) + 7;
                mCheckedListBox.Height = (PreferredHeight < MaxHeight) ? PreferredHeight : MaxHeight;
                mCheckedListBox.Width = this.Width;
                mPopup.Show(mDataGridView.PointToScreen(new System.Drawing.Point(e.X, e.Y)));
            }
        }

        // The constructor creates an instance of CheckedListBox and ToolStripDropDown.
        // the CheckedListBox is hosted by ToolStripControlHost, which in turn is
        // added to ToolStripDropDown.
        public DataGridViewColumnSelector()
        {
            mCheckedListBox = new CheckedListBox();
            mCheckedListBox.CheckOnClick = true;
            mCheckedListBox.ItemCheck += new ItemCheckEventHandler(mCheckedListBox_ItemCheck);

            ToolStripControlHost mControlHost = new ToolStripControlHost(mCheckedListBox);
            mControlHost.Padding = Padding.Empty;
            mControlHost.Margin = Padding.Empty;
            mControlHost.AutoSize = false;

            mPopup = new ToolStripDropDown();
            mPopup.Padding = Padding.Empty;
            mPopup.Items.Add(mControlHost);
        }

        public DataGridViewColumnSelector(Zuby.ADGV.AdvancedDataGridView dgv)
            : this()
        {
            this.DataGridView = dgv;
        }

        // When user checks / unchecks a checkbox, the related column visibility is 
        // switched.
        void mCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // uprava, aby byl minimalne jeden sloupec videt (kvuli moznosti zobrazeni ostatnich dalsich sloupcu)
            int checkedCount = mCheckedListBox.CheckedItems.Count;
            if (e.NewValue == CheckState.Unchecked)
                --checkedCount;

            if (checkedCount == 0)
                e.NewValue = CheckState.Checked;
            else
                mDataGridView.Columns[e.Index].Visible = (e.NewValue == CheckState.Checked);
        }
    }
    #endregion
}
