using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Headup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            richTextBox2.AllowDrop = true;
            this.richTextBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.richTextBox_DragEnter);
            this.richTextBox2.DragDrop += new System.Windows.Forms.DragEventHandler(this.richTextBox_DragDrop);

        }
        private void richTextBox_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                richTextBox1.SelectionBackColor = Color.Red;
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void richTextBox_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            rtb.Text = e.Data.GetData(DataFormats.Text).ToString();
        }
    }
}
