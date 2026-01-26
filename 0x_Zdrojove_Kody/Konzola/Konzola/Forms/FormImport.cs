using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konzola.Forms
{
    public partial class FormImport : Form
    {
        // Property to hold the selected value
        public string SelectedOption { get; private set; }

        public FormImport()
        {
            InitializeComponent();
        }

        // Constructor that accepts parameters for the form title and radio button labels
        public FormImport(string formTitle, string radioButton1Text, string radioButton2Text)
        {
            InitializeComponent();

            // Set the form title
            this.Text = formTitle;

            // Set the text of the RadioButtons
            radioButton1.Text = radioButton1Text;
            radioButton2.Text = radioButton2Text;
        }

        private void btn_Vybrat_Click(object sender, EventArgs e)
        {
            // Check which RadioButton is selected
            if (radioButton1.Checked)
            {
                SelectedOption = "SQL_Procedura"; // You can set this value based on the selection
            }
            else if (radioButton2.Checked)
            {
                SelectedOption = "CSV"; // Change this to reflect the choice
            }

            // Close the form and return DialogResult.OK to indicate success
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Zpet_Click(object sender, EventArgs e)
        {
            // Close the form without returning a result
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormImport_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }
    }
}
