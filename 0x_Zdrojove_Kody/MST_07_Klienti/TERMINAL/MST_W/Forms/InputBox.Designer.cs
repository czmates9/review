namespace Fask.MST_W.Forms
{
    partial class InputBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputBox));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonOK = new Fask.Graphic.GraphicButton();
            this.buttonStorno = new Fask.Graphic.GraphicButton();
            this.labelText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.BitmapNormal = null;
            this.buttonOK.FocusMargin = 5;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Pressed = false;
            this.buttonOK.Transparent = System.Drawing.Color.White;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            resources.ApplyResources(this.buttonStorno, "buttonStorno");
            this.buttonStorno.BitmapNormal = null;
            this.buttonStorno.FocusMargin = 5;
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Pressed = false;
            this.buttonStorno.Transparent = System.Drawing.Color.White;
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // labelText
            // 
            resources.ApplyResources(this.labelText, "labelText");
            this.labelText.Name = "labelText";
            // 
            // InputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.buttonStorno);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBox1);
            this.KeyPreview = true;
            this.Name = "InputBox";
            this.Load += new System.EventHandler(this.InputBox_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputBox_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private Fask.Graphic.GraphicButton buttonOK;
        private Fask.Graphic.GraphicButton buttonStorno;
        private System.Windows.Forms.Label labelText;
    }
}