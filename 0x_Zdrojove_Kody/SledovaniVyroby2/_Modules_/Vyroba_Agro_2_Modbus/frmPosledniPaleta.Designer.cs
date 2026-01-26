
namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus
{
    partial class frmPosledniPaleta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus.KeyboardText keyboardText1 = new FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus.KeyboardText();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.lb_VPH_SOPNUMBE = new System.Windows.Forms.Label();
            this.tb_pocetPalet = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_pocetPytluNaPalete = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lb_VPP_pol = new System.Windows.Forms.Label();
            this.lb_ITEMDESC = new System.Windows.Forms.Label();
            this.lb_EAN = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.keyboardUC1 = new FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus.KeyboardUC();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lb_VPH_SOPNUMBE);
            this.panel1.Controls.Add(this.tb_pocetPalet);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.tb_pocetPytluNaPalete);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lb_VPP_pol);
            this.panel1.Controls.Add(this.lb_ITEMDESC);
            this.panel1.Controls.Add(this.lb_EAN);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 371);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.LimeGreen;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(657, 250);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 87);
            this.button1.TabIndex = 9;
            this.button1.Text = "UP";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb_VPH_SOPNUMBE
            // 
            this.lb_VPH_SOPNUMBE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_VPH_SOPNUMBE.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lb_VPH_SOPNUMBE.Location = new System.Drawing.Point(22, 154);
            this.lb_VPH_SOPNUMBE.Name = "lb_VPH_SOPNUMBE";
            this.lb_VPH_SOPNUMBE.Size = new System.Drawing.Size(750, 33);
            this.lb_VPH_SOPNUMBE.TabIndex = 3;
            this.lb_VPH_SOPNUMBE.Text = "číslo VP: 0123456789012";
            // 
            // tb_pocetPalet
            // 
            this.tb_pocetPalet.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold);
            this.tb_pocetPalet.Location = new System.Drawing.Point(342, 297);
            this.tb_pocetPalet.Name = "tb_pocetPalet";
            this.tb_pocetPalet.Size = new System.Drawing.Size(290, 40);
            this.tb_pocetPalet.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(8, 303);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(328, 29);
            this.label7.TabIndex = 7;
            this.label7.Text = "počet palet na paletizatoru:";
            // 
            // tb_pocetPytluNaPalete
            // 
            this.tb_pocetPytluNaPalete.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold);
            this.tb_pocetPytluNaPalete.Location = new System.Drawing.Point(342, 250);
            this.tb_pocetPytluNaPalete.Name = "tb_pocetPytluNaPalete";
            this.tb_pocetPytluNaPalete.Size = new System.Drawing.Size(290, 40);
            this.tb_pocetPytluNaPalete.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(74, 256);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(262, 29);
            this.label6.TabIndex = 5;
            this.label6.Text = "počet pytlů na paletě:";
            // 
            // lb_VPP_pol
            // 
            this.lb_VPP_pol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_VPP_pol.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lb_VPP_pol.Location = new System.Drawing.Point(22, 191);
            this.lb_VPP_pol.Name = "lb_VPP_pol";
            this.lb_VPP_pol.Size = new System.Drawing.Size(750, 33);
            this.lb_VPP_pol.TabIndex = 4;
            this.lb_VPP_pol.Text = "pol VP: 0123456789012";
            // 
            // lb_ITEMDESC
            // 
            this.lb_ITEMDESC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_ITEMDESC.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lb_ITEMDESC.Location = new System.Drawing.Point(22, 121);
            this.lb_ITEMDESC.Name = "lb_ITEMDESC";
            this.lb_ITEMDESC.Size = new System.Drawing.Size(750, 29);
            this.lb_ITEMDESC.TabIndex = 2;
            this.lb_ITEMDESC.Text = "Nazev Vyrobku: jabfajkdbfjasbdf absdjfbajsdf";
            // 
            // lb_EAN
            // 
            this.lb_EAN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_EAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lb_EAN.Location = new System.Drawing.Point(22, 88);
            this.lb_EAN.Name = "lb_EAN";
            this.lb_EAN.Size = new System.Drawing.Size(750, 29);
            this.lb_EAN.TabIndex = 1;
            this.lb_EAN.Text = "EAN: 0123456789012";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(750, 75);
            this.label1.TabIndex = 0;
            this.label1.Text = "\"Poslední paleta\"";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.Tag = "ERROR";
            // 
            // keyboardUC1
            // 
            this.keyboardUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            keyboardText1.Btn19 = "OK";
            keyboardText1.BtnEnter = "Enter";
            keyboardText1.BtnF1 = "F1";
            keyboardText1.BtnF2 = "F2";
            keyboardText1.BtnF3 = "F3";
            keyboardText1.BtnF4 = "F4";
            keyboardText1.BtnF5 = "F5";
            this.keyboardUC1.KeyboardText = keyboardText1;
            this.keyboardUC1.Location = new System.Drawing.Point(0, 371);
            this.keyboardUC1.Name = "keyboardUC1";
            this.keyboardUC1.Size = new System.Drawing.Size(800, 229);
            this.keyboardUC1.TabIndex = 6;
            // 
            // frmPosledniPaleta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.ControlBox = false;
            this.Controls.Add(this.keyboardUC1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPosledniPaleta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Poslední paleta";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPosledniPaleta_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_ITEMDESC;
        private System.Windows.Forms.Label lb_EAN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lb_VPP_pol;
        private System.Windows.Forms.Label lb_VPH_SOPNUMBE;
        private KeyboardUC keyboardUC1;
        private System.Windows.Forms.TextBox tb_pocetPalet;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_pocetPytluNaPalete;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button1;
    }
}