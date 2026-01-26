namespace Fask.Aktualizace_API.Odvadeni
{
    partial class FormBlokace
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonPokracovat = new System.Windows.Forms.Button();
            this.buttonOdblokovat = new System.Windows.Forms.Button();
            this.buttonUkoncit = new System.Windows.Forms.Button();
            this.ucDetail1 = new Fask.Aktualizace_API.Controls.ucDetail();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.buttonPokracovat, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonOdblokovat, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonUkoncit, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(334, 457);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonPokracovat
            // 
            this.buttonPokracovat.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.buttonPokracovat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPokracovat.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonPokracovat.Location = new System.Drawing.Point(3, 307);
            this.buttonPokracovat.Name = "buttonPokracovat";
            this.buttonPokracovat.Size = new System.Drawing.Size(328, 147);
            this.buttonPokracovat.TabIndex = 0;
            this.buttonPokracovat.Text = "Pokraèovat (F1)";
            this.buttonPokracovat.UseVisualStyleBackColor = false;
            this.buttonPokracovat.Click += new System.EventHandler(this.buttonPokracovat_Click);
            // 
            // buttonOdblokovat
            // 
            this.buttonOdblokovat.BackColor = System.Drawing.Color.LightBlue;
            this.buttonOdblokovat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOdblokovat.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOdblokovat.Location = new System.Drawing.Point(3, 3);
            this.buttonOdblokovat.Name = "buttonOdblokovat";
            this.buttonOdblokovat.Size = new System.Drawing.Size(328, 146);
            this.buttonOdblokovat.TabIndex = 1;
            this.buttonOdblokovat.Text = "Odblokovat (F5)";
            this.buttonOdblokovat.UseVisualStyleBackColor = false;
            this.buttonOdblokovat.Click += new System.EventHandler(this.buttonOdblokovat_Click);
            // 
            // buttonUkoncit
            // 
            this.buttonUkoncit.BackColor = System.Drawing.Color.Salmon;
            this.buttonUkoncit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonUkoncit.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonUkoncit.Location = new System.Drawing.Point(3, 155);
            this.buttonUkoncit.Name = "buttonUkoncit";
            this.buttonUkoncit.Size = new System.Drawing.Size(328, 146);
            this.buttonUkoncit.TabIndex = 2;
            this.buttonUkoncit.Text = "Ukonèit (F9)";
            this.buttonUkoncit.UseVisualStyleBackColor = false;
            this.buttonUkoncit.Click += new System.EventHandler(this.buttonUkoncit_Click);
            // 
            // ucDetail1
            // 
            this.ucDetail1.DetialObject = null;
            this.ucDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDetail1.Location = new System.Drawing.Point(0, 21);
            this.ucDetail1.Name = "ucDetail1";
            this.ucDetail1.Size = new System.Drawing.Size(234, 436);
            this.ucDetail1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ucDetail1);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(572, 457);
            this.splitContainer1.SplitterDistance = 334;
            this.splitContainer1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pøíkaz";
            // 
            // FormBlokace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(572, 457);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.KeyPreview = true;
            this.Name = "FormBlokace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Blokace výrobního pøíkazu";
            this.Load += new System.EventHandler(this.FormBase_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormInputKod_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Button buttonOdblokovat;
        public System.Windows.Forms.Button buttonUkoncit;
        public System.Windows.Forms.Button buttonPokracovat;
        private Fask.Aktualizace_API.Controls.ucDetail ucDetail1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
    }
}