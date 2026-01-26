using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konzola.Vyroba.Transakce
{
    public partial class FormVyrobniPrikazList_Tisk : Form
    {

        // Uložení vybraného řádku do proměnné
       private DataGridViewRow selectedRow;

        // Metoda, která se zavolá při výběru řádku
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Uložení vybraného řádku do proměnné
            selectedRow = dGW_files.CurrentRow;


        }

        public FormVyrobniPrikazList_Tisk()
        {
            InitializeComponent();
            // Přidání event handleru pro výběr řádku
            dGW_files.SelectionChanged += DataGridView1_SelectionChanged;
        }

        private void FormVyrobniPrikazList_Tisk_Load(object sender, EventArgs e)
        {

            dGW_files.ColumnCount = 1;
            dGW_files.Columns[0].Name = "Název tiskové šablony";
            //dGW_files.Columns[0].MinimumWidth = 300;
            //dGW_files.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dGW_files.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dGW_files.Columns[1].Name = "Velikost";
            //dGW_files.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            string _dirlog = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); // KDe se nachazi EXE soubor aplikace


            string logDirectory = Path.Combine(_dirlog, "PrintTemplates");
            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);

            DirectoryInfo directory = new DirectoryInfo(logDirectory);

            foreach (FileInfo file in directory.GetFiles())
            {
                string[] row = new string[] { file.Name, (file.Length / 1024).ToString() + " KB" };
                dGW_files.Rows.Add(row);
            }

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {

            // Zkontrolování, zda byl vybrán řádek
            if (selectedRow != null)
            {
                // Ukončení dialogu s návratovou hodnotou (vybraný řádek)
                this.Tag = selectedRow;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btn_zrusit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
