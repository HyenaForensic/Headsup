﻿using Headup.Popup;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Syncfusion.Windows.Forms.Diagram;

namespace Headup
{
    public partial class Main : SfForm
    {
        public Main()
        {
            InitializeComponent();
            //BlockColor = panelColor.BackColor; //컬러 객체 선언
            CategoryBtns = new List<SfButton>(); //버튼 리스트 선언
            diagram1.DragDrop += Diagram1_DragDrop;
            //InitDiagram();
        }

        #region 다이어그램 관련
        private void InitDiagram()
        {
            TableLayoutManager tlLayout = new TableLayoutManager(this.diagram1.Model, 7, 7);
            tlLayout.VerticalSpacing = 20;
            tlLayout.HorizontalSpacing = 20;
            tlLayout.CellSizeMode = CellSizeMode.EqualToMaxNode;
            tlLayout.Orientation = Orientation.Horizontal;
            tlLayout.MaxSize = new SizeF(500, 600);

            this.diagram1.LayoutManager = tlLayout;
            //this.diagram1.LayoutManager.UpdateLayout(null);.AttachModel(model1);
        }
        private void Diagram1_DragDrop(object sender, DragEventArgs e) //Diagram으로 드래그 앤 드랍을 했을 경우 처리
        {
            if(CurrentSelectedColor.Name != "0") //카테고리 색을 선택한 경우만 처리
            {
                //다이어그램 노드에 추가하기 위해 textbox 객체를 만든다.
                TextBox txtBox = new TextBox();
                txtBox.Multiline = true;
                txtBox.Text = richTextBoxName.SelectedText;

                richTextBoxName.SelectionBackColor = CurrentSelectedColor; //richtextbox의 글 바탕색을 변경한다.

                //DragEventArgs에 마우스 포인트가 화면 기준으로 되어 있기 때문에 컨트롤(다이어그램)의 위치를 찾아 그만큼 빼주어야 한다.
                int x = e.X - diagram1.AccessibilityObject.Bounds.Location.X;
                int y = e.Y - diagram1.AccessibilityObject.Bounds.Location.Y;

                ControlNode ctrlnode = new ControlNode(txtBox, new RectangleF(x, y, 140, 50));
                ctrlnode.HostingControl.BackColor = CurrentSelectedColor;
                ctrlnode.ActivateStyle = Syncfusion.Windows.Forms.Diagram.ActivateStyle.ClickPassThrough;
                
                diagram1.Model.AppendChild(ctrlnode);
            }
            else
            {
                MessageBox.Show("카테고리를 선택해주세요", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        #endregion


        #region 메뉴 관련
        private void barItemNew_Click(object sender, EventArgs e) //메뉴 클릭 시
        {
            OpenFileDialog openFile1 = new OpenFileDialog();

            openFile1.Filter = "Text Files|*.txt";

            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string readText = File.ReadAllText(openFile1.FileName);
                labelFilePath.Text = openFile1.FileName;
                richTextBoxName.Text = readText;
            }
        }
        #endregion

        #region 각종 이벤트 관련
        private void FontComboBox1_SelectedIndexChanged(object sender, EventArgs e) //폰트 변경 시
        {
            richTextBoxName.Font = new Font(fontComboBox.SelectedItem.ToString(), 11, System.Drawing.FontStyle.Regular);
        }

        private void sfButtonAdd_Click(object sender, EventArgs e) //add button 클릭 시
        {
            AddCategory AddForm = new AddCategory();
            AddForm.ShowDialog();
            if (selectedColorFlagFromAddForm) {
                SfButton sfButton = new SfButton();
                sfButton.Click += CategoryBtnClick;
                //Initialize the font, location, name, size and text for the SfButton.
                sfButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
                sfButton.Size = new System.Drawing.Size(96, 28);
                sfButton.Text = CategoryNameFromAddForm;
                sfButton.Style.BackColor = BlockColorFromAddForm; //기본 바탕색 변경
                sfButton.Style.FocusedBackColor = BlockColorFromAddForm; //포커싱 됬을 때 바탕색 변경
                sfButton.Style.HoverBackColor = BlockColorFromAddForm; //마우스 올렸을 때 바탕색 변경
                sfButton.Style.PressedBackColor = BlockColorFromAddForm; //클릭한 상태 바탕색 변경

                //Add the SfButton into the form.
                flowLayoutPanelCategory.Controls.Add(sfButton);
                CategoryBtns.Add(sfButton);
                selectedColorFlagFromAddForm = false; //처리가 다 끝나면 다시 false를 해주어야 다음에 동일 처리가 가능하다.
            }
        }
        private void CategoryBtnClick(object sender, EventArgs e) //카테고리 버튼 클릭 이벤트
        {
            SfButton sfButton = (SfButton)sender;
            CurrentSelectedColor = sfButton.BackColor;
            labelCurrentColor.BackColor = sfButton.BackColor;

        }
        #endregion

        //GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        //{
        //    float r2 = radius / 2f;
        //    GraphicsPath GraphPath = new GraphicsPath();
        //    GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
        //    GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
        //    GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
        //    GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
        //    GraphPath.AddArc(Rect.X + Rect.Width - radius,
        //                     Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
        //    GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
        //    GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
        //    GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);
        //    GraphPath.CloseFigure();
        //    return GraphPath;
        //}
        //private void sfButtonAdd_Paint(object sender, PaintEventArgs e)
        //{
        //    SfButton btnTmp = (SfButton)sender;
        //    //Rounded rectangle corder radius. The radius must be less than 10.
        //    int radius = 5;
        //    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        //    Rectangle rect = new Rectangle(btnTmp.ClientRectangle.X + 1,
        //                                   btnTmp.ClientRectangle.Y + 1,
        //                                   btnTmp.ClientRectangle.Width - 2,
        //                                   btnTmp.ClientRectangle.Height - 2);
        //    btnTmp.Region = new Region(GetRoundPath(rect, radius));
        //    rect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);
        //    e.Graphics.DrawPath(new Pen(Color.Red), GetRoundPath(rect, radius));
        //}

        #region 변수 선언
        private Color currentSelectedColor;
        private static Color blockColorFromAddForm; //텍스트의 블록색 저장 변수
        private static string categoryNameFromAddForm; //AddCategory 폼에서 이름을 가져오기 위한 static 변수
        private static bool selectedColorFlagFromAddForm = false;
        private List<SfButton> categoryBtns; //카테고리 버튼 리스트
        public Color CurrentSelectedColor
        {
            get { return currentSelectedColor; }
            set { currentSelectedColor = value; }
        }
        public static Color BlockColorFromAddForm
        {
            get { return blockColorFromAddForm; }
            set { blockColorFromAddForm = value; }
        }
        public static string CategoryNameFromAddForm
        {
            get { return categoryNameFromAddForm; }
            set { categoryNameFromAddForm = value; }
        }
        public static bool SelectedColorFlagFromAddForm
        {
            get { return selectedColorFlagFromAddForm; }
            set { selectedColorFlagFromAddForm = value; }
        }
        public List<SfButton> CategoryBtns
        {
            get { return categoryBtns; }
            set { categoryBtns = value; }
        }





        #endregion
    }
}
