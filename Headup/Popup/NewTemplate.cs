using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Headup.Popup
{
    public partial class NewTemplate : MetroForm
    {
        public NewTemplate()
        {
            InitializeComponent();
        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("템블릿 이름을 입력하세요");
            }
            else if (Main.CategoryTemplate.ContainsKey(textBox1.Text))
            {
                MessageBox.Show("이미 존재합니다");
            }
            else
            {
                Main.NewTemplateName = textBox1.Text;
                Close();
            }
        }
    }
}
