using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fask.MST_W.Config
{
    public partial class FormAdminAccess : System.Windows.Forms.Form
    {
        public enum AccessType
        {
            Admin,
            ProdejEditaceNaplnenaDavka
        }
        private AccessType _accessType;

        public FormAdminAccess(AccessType accessType)
        {
            InitializeComponent();
            this._accessType = accessType;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PerformEnter();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PerformEsc();
        }

        private void FormAdminAccess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PerformEnter();
            else if (e.KeyCode == Keys.Escape)
                PerformEsc();
            else
                return;

            e.Handled = true;
        }

        private void PerformEnter()
        {
            if (_accessType == AccessType.Admin && textBox1.Text == Settings.AdminPwd)
                DialogResult = DialogResult.OK;

            if (_accessType == AccessType.ProdejEditaceNaplnenaDavka && textBox1.Text == Prodej.Globals.HesloEditaceNaplnenaDavka)
                DialogResult = DialogResult.OK;
        }

        private void PerformEsc()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FormAdminAccess_Load(object sender, EventArgs e)
        {
            this.Location = Forms.FormLocation.GetFormLocation(this.Size);
        }
    }
}