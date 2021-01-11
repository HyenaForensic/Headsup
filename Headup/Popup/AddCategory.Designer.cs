namespace Headup.Popup
{
    partial class AddCategory
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
            this.sfButtonOK = new Syncfusion.WinForms.Controls.SfButton();
            this.sfButtonClose = new Syncfusion.WinForms.Controls.SfButton();
            this.textBoxCategoryName = new System.Windows.Forms.TextBox();
            this.colorPickerButton = new Syncfusion.Windows.Forms.ColorPickerButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.autoLabelColor = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabelCategoryName = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // sfButtonOK
            // 
            this.sfButtonOK.AccessibleName = "Button";
            this.sfButtonOK.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.sfButtonOK.Location = new System.Drawing.Point(106, 71);
            this.sfButtonOK.Name = "sfButtonOK";
            this.sfButtonOK.Size = new System.Drawing.Size(96, 28);
            this.sfButtonOK.TabIndex = 3;
            this.sfButtonOK.Text = "OK";
            this.sfButtonOK.Click += new System.EventHandler(this.sfButtonOK_Click);
            // 
            // sfButtonClose
            // 
            this.sfButtonClose.AccessibleName = "Button";
            this.sfButtonClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.sfButtonClose.Location = new System.Drawing.Point(264, 71);
            this.sfButtonClose.Name = "sfButtonClose";
            this.sfButtonClose.Size = new System.Drawing.Size(96, 28);
            this.sfButtonClose.TabIndex = 4;
            this.sfButtonClose.Text = "CLOSE";
            this.sfButtonClose.Click += new System.EventHandler(this.sfButtonClose_Click);
            // 
            // textBoxCategoryName
            // 
            this.textBoxCategoryName.Location = new System.Drawing.Point(115, 16);
            this.textBoxCategoryName.Name = "textBoxCategoryName";
            this.textBoxCategoryName.Size = new System.Drawing.Size(131, 21);
            this.textBoxCategoryName.TabIndex = 1;
            // 
            // colorPickerButton
            // 
            this.colorPickerButton.BeforeTouchSize = new System.Drawing.Size(120, 23);
            this.colorPickerButton.Location = new System.Drawing.Point(299, 16);
            this.colorPickerButton.Name = "colorPickerButton";
            this.colorPickerButton.Size = new System.Drawing.Size(120, 23);
            this.colorPickerButton.TabIndex = 2;
            this.colorPickerButton.Text = "Select a Color";
            this.colorPickerButton.ColorSelected += new System.EventHandler(this.colorPickerButton_ColorSelected);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.sfButtonClose);
            this.panel1.Controls.Add(this.sfButtonOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(457, 110);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.autoLabelColor);
            this.panel2.Controls.Add(this.autoLabelCategoryName);
            this.panel2.Controls.Add(this.textBoxCategoryName);
            this.panel2.Controls.Add(this.colorPickerButton);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(432, 53);
            this.panel2.TabIndex = 0;
            // 
            // autoLabelColor
            // 
            this.autoLabelColor.DX = -47;
            this.autoLabelColor.DY = 5;
            this.autoLabelColor.LabeledControl = this.colorPickerButton;
            this.autoLabelColor.Location = new System.Drawing.Point(252, 21);
            this.autoLabelColor.Name = "autoLabelColor";
            this.autoLabelColor.Size = new System.Drawing.Size(43, 12);
            this.autoLabelColor.TabIndex = 0;
            this.autoLabelColor.Text = "Color :";
            // 
            // autoLabelCategoryName
            // 
            this.autoLabelCategoryName.DX = -106;
            this.autoLabelCategoryName.DY = 4;
            this.autoLabelCategoryName.LabeledControl = this.textBoxCategoryName;
            this.autoLabelCategoryName.Location = new System.Drawing.Point(9, 20);
            this.autoLabelCategoryName.Name = "autoLabelCategoryName";
            this.autoLabelCategoryName.Size = new System.Drawing.Size(102, 12);
            this.autoLabelCategoryName.TabIndex = 0;
            this.autoLabelCategoryName.Text = "Category Name :";
            // 
            // AddCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 110);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddCategory";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton sfButtonOK;
        private Syncfusion.WinForms.Controls.SfButton sfButtonClose;
        private System.Windows.Forms.TextBox textBoxCategoryName;
        private Syncfusion.Windows.Forms.ColorPickerButton colorPickerButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabelCategoryName;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabelColor;
    }
}