namespace Vyroba_Konzola.Ciselniky
{
    partial class FormZboziList2
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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dsZbozi = new Fask.Console.Interfaces.DataSets.Zbozi();
            this.bwLoadZbozi = new System.ComponentModel.BackgroundWorker();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsZbozi)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Location = new System.Drawing.Point(495, 46);
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(267, 302);
            this.progressIndicator1.Size = new System.Drawing.Size(40, 40);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "CZMST095";
            this.bindingSource1.DataSource = this.dsZbozi;
            // 
            // dsZbozi
            // 
            this.dsZbozi.DataSetName = "Zbozi";
            this.dsZbozi.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bwLoadZbozi
            // 
            this.bwLoadZbozi.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadZbozi_DoWork);
            this.bwLoadZbozi.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadZbozi_RunWorkerCompleted);
            // 
            // FormZboziList2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 468);
            this.Name = "FormZboziList2";
            this.ShowIcon = false;
            this.Text = "FormZboziList2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormZboziList2_FormClosing);
            this.Load += new System.EventHandler(this.FormZboziList2_Load);
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsZbozi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private Fask.Console.Interfaces.DataSets.Zbozi dsZbozi;
        private System.ComponentModel.BackgroundWorker bwLoadZbozi;
    }
}