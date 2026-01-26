using Konzola.ImportniMustky.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konzola.ImportniMustky
{
    public partial class FormImport_SKzNC_ErrList : Form
    {

        public List<MyErrorEnum> MyErrorEnums = null;

        public FormImport_SKzNC_ErrList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event pro Button OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void PerformOK()
        {
            DialogResult = DialogResult.OK;
        }

        private void FormImport_SKzNC_ErrList_Load(object sender, EventArgs e)
        {
            try
            {
                if (MyErrorEnums != null)
                {

                    foreach (MyErrorEnum item in MyErrorEnums)
                    {
                        textBox1.AppendText(item.ToString() + Environment.NewLine);

                    }

                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }
    }
}
