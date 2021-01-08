namespace Headup.Popup
{
    partial class NewTemplate
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
            this.autoLabelTemplateName = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.sfButton1 = new Syncfusion.WinForms.Controls.SfButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // autoLabelTemplateName
            // 
            this.autoLabelTemplateName.DX = -100;
            this.autoLabelTemplateName.DY = 4;
            this.autoLabelTemplateName.LabeledControl = this.textBox1;
            this.autoLabelTemplateName.Location = new System.Drawing.Point(15, 17);
            this.autoLabelTemplateName.Name = "autoLabelTemplateName";
            this.autoLabelTemplateName.Size = new System.Drawing.Size(96, 12);
            this.autoLabelTemplateName.TabIndex = 0;
            this.autoLabelTemplateName.Text = "New Template :";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.sfButton1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.autoLabelTemplateName);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(425, 52);
            this.panel1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(115, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(186, 21);
            this.textBox1.TabIndex = 1;
            // 
            // sfButton1
            // 
            this.sfButton1.AccessibleName = "Button";
            this.sfButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.sfButton1.Location = new System.Drawing.Point(319, 13);
            this.sfButton1.Name = "sfButton1";
            this.sfButton1.Size = new System.Drawing.Size(78, 21);
            this.sfButton1.TabIndex = 2;
            this.sfButton1.Text = "OK";
            this.sfButton1.Click += new System.EventHandler(this.sfButton1_Click);
            // 
            // NewTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 73);
            this.Controls.Add(this.panel1);
            this.Name = "NewTemplate";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NewTemplate";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabelTemplateName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private Syncfusion.WinForms.Controls.SfButton sfButton1;
    }
}