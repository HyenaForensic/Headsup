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
    public partial class AddCategory : MetroForm
    {
        public AddCategory()
        {
            InitializeComponent();
        }

        private void sfButtonClose_Click(object sender, EventArgs e) //닫기 버튼을 클릭한 경우
        {
            Close();
        }

        private void colorPickerButton_ColorSelected(object sender, EventArgs e) //컬러를 선택한 경우
        {
            ColorPickerButton colorBtn = (ColorPickerButton)sender;
            colorPickerButton.Text = colorBtn.SelectedColor.Name;
            colorPickerButton.BackColor = colorBtn.SelectedColor;
        }

        private void sfButtonOK_Click(object sender, EventArgs e) //OK를 클릭한 경우
        {
            if (textBoxCategoryName.Text.Length == 0) //카테고리 이름을 넣은 경우
            {
                MessageBox.Show("카테고리 이름을 입력하세요", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (colorPickerButton.SelectedColor.Name == "0") //색을 선택하지 않았다면
            {
                MessageBox.Show("색을 선택해주세요", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (colorPickerButton.SelectedColor.Name == "White") //흰색은 어차피 보이지 않기 때문에 처리하면 안됨
            {
                MessageBox.Show("흰색은 선택할 수 없습니다", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else //정상이면
            {
                Main.BlockColorFromAddForm = colorPickerButton.SelectedColor; //컬러를 저장한다.
                Main.CategoryNameFromAddForm = textBoxCategoryName.Text; //카테고리 이름을 저장한다.
                Main.SelectedColorFlagFromAddForm = true;
                Close();
            }
        }
    }
}
