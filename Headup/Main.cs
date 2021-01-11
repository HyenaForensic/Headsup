using Headup.Popup;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Syncfusion.Windows.Forms.Diagram;
using System.Text;
using Syncfusion.Windows.Forms.Edit.Interfaces;
using Headup.Util;
using Syncfusion.Windows.Forms.Tools;

namespace Headup
{
    public partial class Main : SfForm
    {
        public Main()
        {
            InitializeComponent();
            InitControls(); //각 컨트롤러 초기화
        }

        #region 초기 세팅 관련
        private void InitControls()
        {
            InitCategory(); //버튼 스타일 초기화
            InitTree(); //트리에 루트 추가
            InitToolStripComboBox(); //템플릿을 초기화한다.
            //InitEditControl(); //Editor 초기화
            InitDiagram(); //다이어그램 초기화
        }
        private void InitToolStripComboBox() //템플릿 콤보 박스 초기화
        {
            CategoryTemplateDic = new Dictionary<string, Dictionary<string, int>>();
            TemplateFilePath = new FileInfo(Application.StartupPath + @"\category_template");
            if (File.Exists(TemplateFilePath.FullName)) //템플릿 파일이 있으면
            {
                CategoryTemplateDic = ConvertDic.FileToDic(TemplateFilePath.FullName);
                foreach (KeyValuePair<string, Dictionary<string, int>> tmp in CategoryTemplateDic)
                {
                    toolStripComboBoxExTemplate.Items.Add(tmp.Key); //콤보박스 리스트에 템플릿 추가

                }
            }
        }
        private void InitCategory() //button style 초기화
        {
            CategoryView();
            sfButtonDrawDiagram.Style.Border = new Pen(Color.Black, 1); //다이어그램에 자동 노드 추가 버튼 테두리 설정
        }

        private void InitTree() //TreeControl 초기화
        {
            //루트 트리 구성
            RootNode = new TreeNode("목록"); //root tree 를 만든다.
            documentExplorer1.Nodes.AddRange(new TreeNode[] { RootNode }); //루트 트리를 추가한다.
            RootNode.Expand(); //루트 노드를 확장한다.
        }
        private void InitEditControl() //editControl 초기화
        {
            //이걸 하면 확대버튼이 사라진다.....ㅡㅡ
            IConfigLanguage currentConfigLanguage = this.editControl.Configurator.CreateLanguageConfiguration("New");
            editControl.ApplyConfiguration(currentConfigLanguage);


            //editControl의 상태표시줄 설정
            //this.editControl.StatusBarSettings.Visible = true; // Shows the built-in status bar.
            //this.editControl.StatusBarSettings.TextPanel.Visible = true; // Enable the TextPanel in the StatusBar.
            //this.editControl.StatusBarSettings.GripVisibility = Syncfusion.Windows.Forms.Edit.Enums.SizingGripVisibility.Visible; // Set the visibility of the status bar sizing grip.
        }
        private void InitDiagram() //diagram 초기화
        {
            diagram1.DragDrop += Diagram1_DragDrop;
            //TableLayoutManager tlLayout = new TableLayoutManager(this.diagram1.Model, 7, 7);
            //tlLayout.VerticalSpacing = 20;
            //tlLayout.HorizontalSpacing = 20;
            //tlLayout.CellSizeMode = CellSizeMode.EqualToMaxNode;
            //tlLayout.Orientation = Orientation.Horizontal;
            //tlLayout.MaxSize = new SizeF(500, 600);
            //this.diagram1.LayoutManager = tlLayout;

        }
        #endregion

        #region 다이어그램 관련

        private void Diagram1_DragDrop(object sender, DragEventArgs e) //Diagram으로 드래그 앤 드랍을 했을 경우 처리
        {
            if (CurrentSelectedColor.Name != "0") //카테고리 색을 선택한 경우만 처리
            {
                ChangeSelectBackColor(); //editControl의 텍스트 백그라운드 색을 변경한다.

                //다이어그램 노드에 추가하기 위해 textbox 객체를 만든다.
                TextBox txtBox = new TextBox();
                txtBox.Multiline = true;
                txtBox.Text = editControl.SelectedText;

                //DragEventArgs에 마우스 포인트가 화면 기준으로 되어 있기 때문에 컨트롤(다이어그램)의 위치를 찾아 그만큼 빼주어야 한다.
                //e는 화면 기준의 마우스 위치, diagram1.AccessibilityObject.Bounds.Location은 다이어그램 컨트롤 시작 위치, origin은 page위치
                //그래서 아래와 같이 계산하면 정확한 위치가 나온다.
                int x = e.X - diagram1.AccessibilityObject.Bounds.Location.X + (int)diagram1.Origin.X;
                int y = e.Y - diagram1.AccessibilityObject.Bounds.Location.Y + (int)diagram1.Origin.Y;

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
        private void barItemNew_Click(object sender, EventArgs e) //New 클릭 시
        {
            OpenFileDialog openFile1 = new OpenFileDialog();

            openFile1.Filter = "Text Files|*.txt";

            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileInfo file = new FileInfo(openFile1.FileName);
                OpenTextFileToEditControl(file.FullName);

                TreeNode node = new TreeNode(file.Name);
                node.ToolTipText = file.FullName;
                RootNode.Nodes.AddRange(new TreeNode[] { node }); //트리를 추가한다.
            }
        }
        #endregion

        #region editControls 관련
        private void ChangeSelectBackColor() //색을 선택하여 변경한 경우
        {
            IBackgroundFormat format = editControl.RegisterBackColorFormat(CurrentSelectedColor, Color.White);
            editControl.SetSelectionBackColor(format);
        }
        #endregion

        #region 트리뷰 관련 이벤트
        private void documentExplorer1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) //노드를 더블클릭한 경우
        {
            if (e.Node.Level == 1) //루트가 아니면 (자식은 한 레벨 뿐)
            {
                if (CurrentFilePath != e.Node.ToolTipText) //현재 열려있는 파일이 아니라면 (현재 열려있는 파일이라면 굳이 다시 열 필요 없다.)
                {
                    OpenTextFileToEditControl(e.Node.ToolTipText);
                }
            }
        }
        private bool OpenTextFileToEditControl(string filePath)
        {
            CurrentFilePath = filePath;
            Ducument = File.ReadAllText(filePath);

            //아래것을 하면 셀렉트 컬러가 개판이 된다.
            IBackgroundFormat format = editControl.RegisterBackColorFormat(Color.WhiteSmoke, Color.White);
            for (int i = 1; i <= editControl.CurrentLine; i++)
            {
                if (i % 2 == 0)
                {
                    editControl.SetLineBackColor(i, true, format);
                }
            }

            return true;
        }
        #endregion

        #region 카테고리 관련 이벤트
        private void CategoryView() //카테고리 버튼을 뿌려주는 함수
        {
            flowLayoutPanelCategory.Controls.Clear(); //먼저 전체를 지운다.
            System.Windows.Forms.Label addLabel = new System.Windows.Forms.Label();
            addLabel.MouseClick += AddLabel_MouseClick; //마우스 클릭 설정
            addLabel.Text = "Add"; //이름 설정
            addLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; //테두리 설정
            addLabel.TextAlign = ContentAlignment.MiddleCenter; //가운데 정렬
            addLabel.Margin = new System.Windows.Forms.Padding(3); //margin 설정 (라벨끼리 떨어뜨리기 위함, 너무 붙어있어서 보기싫어서)
            addLabel.AutoSize = true; //라벨 크기 자동 조절 (글자 길이에 따라 자동으로 조절되게 하기 위함)
            addLabel.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8); //패딩 설정, 글자와 테두리 사이 패딩
            
            flowLayoutPanelCategory.Controls.Add(addLabel);
            foreach (System.Windows.Forms.Label labelTmp in CategoryItemsLabelList) //라벨 리스트에 있는 요소를 flowlayoutpanel에 뿌린다.
            {
                flowLayoutPanelCategory.Controls.Add(labelTmp);
            }


        }
        private void AddLabel_MouseClick(object sender, MouseEventArgs e) //카테고리에서 Add를 클릭했을 경우
        {
            if (e.Button == MouseButtons.Right) //오른쪽 버튼 클릭
            {
                //우클릭은 아무것도 안함
            }
            else if (e.Button == MouseButtons.Left) //왼쪽 버튼 클릭
            {
                if (toolStripComboBoxExTemplate.SelectedIndex != -1)
                { //템플릿이 선택되어있다면
                    AddCategory AddForm = new AddCategory();
                    AddForm.ShowDialog();
                    if (CategoryTemplateDic[toolStripComboBoxExTemplate.Text].ContainsKey(CategoryNameFromAddForm))
                    {
                        MessageBox.Show("같은 이름이 존재합니다.");
                    }
                    else if (!selectedColorFlagFromAddForm) //색이 선택되어있지 않다면 (addCategory form에서 색을 선택하지 않으면 어차피 처리되지 않기 때문에 비정상 처리로 생각하면 됨)
                    {
                        MessageBox.Show("색이 선택되어 있지 않습니다.");
                    }
                    else
                    {
                        MakeCategoryItems(CategoryNameFromAddForm, BlockColorFromAddForm);
                        CategoryView(); //버튼을 다시 뿌려준다.

                        //템플릿 딕셔너리에 저장한다. [템플릿 이름][버튼이름] = int형컬러 형태로 들어간다.
                        CategoryTemplateDic[toolStripComboBoxExTemplate.Text].Add(CategoryNameFromAddForm, BlockColorFromAddForm.ToArgb());
                        selectedColorFlagFromAddForm = false; //처리가 다 끝나면 다시 false를 해주어야 다음에 동일 처리가 가능하다.
                    }
                }
                else
                {
                    MessageBox.Show("템플릿을 먼저 선택해주세요");
                }
            }
        }

        private void MakeCategoryItems(string name, Color color) //카테고리 버튼 만들기
        {
            System.Windows.Forms.Label labelTmp = new System.Windows.Forms.Label();
            labelTmp.MouseClick += LabelTmp_MouseClick;
            labelTmp.Text = name;
            labelTmp.BackColor = color;
            labelTmp.TextAlign = ContentAlignment.MiddleCenter;
            labelTmp.Margin = new System.Windows.Forms.Padding(3);
            labelTmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelTmp.AutoSize = true;
            labelTmp.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);
            CategoryItemsLabelList.Add(labelTmp);
        }
        private void ModifyCategoryItems(string name, Color color) //카테고리 버튼 수정하기
        {
            foreach (System.Windows.Forms.Label label in CategoryItemsLabelList)
            {
                if (label.Text == CurrentLabel.Text)
                {
                    CategoryTemplateDic[toolStripComboBoxExTemplate.Text].Remove(label.Text); //기존 딕셔너리 키는 변경이 어려우니 지운다. (호출부에서 다시 생성한다)
                    label.Text = name;
                    label.BackColor = color;
                    break;
                }
            }
        }
        private void DeleteCategoryItems(string name) //카테고리 버튼 삭제하기
        {
            foreach (System.Windows.Forms.Label label in CategoryItemsLabelList)
            {
                if (label.Text == CurrentLabel.Text)
                {
                    CategoryTemplateDic[toolStripComboBoxExTemplate.Text].Remove(label.Text); //딕셔너리 데이터를 지운다.
                    CategoryItemsLabelList.Remove(label); // 라벨 리스트에 삭제하고자 하는 라벨을 지운다.
                    CurrentSelectedColor = new Color(); //현재 선택되어있는 컬러를 초기화한다.
                    break;
                }
            }
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e) //카테고리 new 버튼 클릭
        {
            NewTemplateName = null; //먼저 초기화를 한다.
            NewTemplate newTemplateForm = new NewTemplate();
            newTemplateForm.ShowDialog();
            if (NewTemplateName != null)
            {
                CategoryTemplateDic.Add(NewTemplateName, new Dictionary<string, int>());
                CategoryItemsLabelList.Clear(); //버튼 리스트를 다 지운다.
                CategoryView();
                toolStripComboBoxExTemplate.SelectedIndex = toolStripComboBoxExTemplate.Items.Add(NewTemplateName); ; //템플릿 리스트를 추가하면서 선택한다.
            }
        }
        private void toolStripButtonSave_Click(object sender, EventArgs e) //카테고리 저장을 클릭한 경우
        {
            if (ConvertDic.DicToFile(CategoryTemplateDic, TemplateFilePath.FullName))
            {
                MessageBox.Show("저장 완료", "성공", MessageBoxButtons.OK);
            }
            else //저장이 실패한 경우
            {
                MessageBox.Show("저장 실패", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void toolStripButtonDelete_Click(object sender, EventArgs e) //카테고리의 delete를 클릭햇을 경우
        {
            if (toolStripComboBoxExTemplate.SelectedIndex != -1)
            { //선택된 템플릿이 있으면
                if (MessageBox.Show(toolStripComboBoxExTemplate.Text + "를 정말 삭제하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CategoryItemsLabelList.Clear(); //버튼 리스트를 전부 삭제
                    if (CategoryTemplateDic.ContainsKey(toolStripComboBoxExTemplate.Text)) //키가 있는 경우
                    {
                        CategoryTemplateDic.Remove(toolStripComboBoxExTemplate.Text);
                    }
                    else //키가 없다면 파일을 수동으로 처리하든 비정상적으로 처리되었든 비장성 결과이다.
                    {
                        //아무것도 안한다.
                    }
                    toolStripComboBoxExTemplate.Items.Remove(toolStripComboBoxExTemplate.Text);
                    CategoryView();
                }
                else
                {
                    //아무것도 안함
                }
            }
            else
            {
                MessageBox.Show("템플릿을 먼저 선택해주세요");
            }
        }
        private void toolStripComboBoxExTemplate_SelectedIndexChanged(object sender, EventArgs e) //카테고리의 템플릿을 변경했을 경우
        {
            ToolStripComboBoxEx toolStripComboBoxEx = (ToolStripComboBoxEx)sender;
            CategoryItemsLabelList.Clear(); //버튼 리스트를 다 지운다.
            foreach (KeyValuePair<string, int> btn in CategoryTemplateDic[toolStripComboBoxEx.Text])
            {
                MakeCategoryItems(btn.Key, Color.FromArgb(btn.Value));
            }
            CategoryView();
        }
        private void LabelTmp_MouseClick(object sender, MouseEventArgs e) //카테고리의 색을 선택할 경우
        {
            CurrentLabel = (System.Windows.Forms.Label)sender;

            if (e.Button == MouseButtons.Right) //오른쪽 버튼 클릭하면 메뉴 이벤트 발생
            {
                contextMenuStripExCategoryItemMenu.Show(CurrentLabel, new Point(e.X, e.Y));
            }
            else if (e.Button == MouseButtons.Left) //왼쪽 버튼 클릭하면 색 변경
            {
                CurrentSelectedColor = CurrentLabel.BackColor;
                
            }
        }
        private void toolStripMenuItemModify_Click(object sender, EventArgs e) //카테고리의 context메뉴 중 수정을 클릭한 경우
        {
            AddCategory AddForm = new AddCategory();
            AddForm.ShowDialog();
            if (!selectedColorFlagFromAddForm) //색이 선택되어있지 않다면 (addCategory form에서 색을 선택하지 않으면 어차피 처리되지 않기 때문에 비정상 처리로 생각하면 됨)
            {
                MessageBox.Show("색이 선택되어 있지 않습니다.");
            }
            else
            {
                ModifyCategoryItems(CategoryNameFromAddForm, BlockColorFromAddForm);
                CategoryView(); //버튼을 다시 뿌려준다.

                //템플릿 딕셔너리에 저장한다. [템플릿 이름][버튼이름] = int형컬러 형태로 들어간다.
                CategoryTemplateDic[toolStripComboBoxExTemplate.Text][CategoryNameFromAddForm] = BlockColorFromAddForm.ToArgb(); //기존 딕셔너리를 삭제했기 때문에 새로운 이름과 색값을 가지는 데이터로 다시 만든다.
            }
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e) //context메뉴 중 삭제를 클릭한 경우
        {
            DeleteCategoryItems(CategoryNameFromAddForm);
            CategoryView(); //버튼을 다시 뿌려준다.
        }
        #endregion

        #region 변수 선언
        
        private static Color blockColorFromAddForm; //텍스트의 블록색 저장 변수
        private static string categoryNameFromAddForm; //AddCategory 폼에서 이름을 가져오기 위한 static 변수
        private static bool selectedColorFlagFromAddForm = false;
        private static string newTemplateName;
        private List<System.Windows.Forms.Label> categoryItemsLabelList = new List<System.Windows.Forms.Label>(); //카테고리 버튼 리스트
        private TreeNode rootNode; //트리의 루트
        private static Dictionary<string, Dictionary<string, int>> categoryTemplateDic;
        private FileInfo templateFilePath; //template 파일 경로
        

        private string ducument;
        private string currentFilePath; //현재 열려있는 txt 파일 경로
        public string Ducument
        {
            get { return ducument; }
            set
            {
                ducument = value;
                editControl.Text = ducument; // editcontrol.loadfile로 하면 인코딩이 깨진다.
            }
        }
        public string CurrentFilePath
        {
            get { return currentFilePath; }
            set
            {
                currentFilePath = value;
                labelFilePath.Text = currentFilePath;
            }
        }

        private System.Windows.Forms.Label currentLabel; //다양한 이벤트 시 현재 선택되어 있는 라벨이 무엇인지 확인하기 위한 변수
        private Color currentSelectedColor;
        public System.Windows.Forms.Label CurrentLabel
        {
            get { return currentLabel; }
            set { currentLabel = value; }
        }
        public Color CurrentSelectedColor
        {
            get { return currentSelectedColor; }
            set
            {
                currentSelectedColor = value;
                labelCurrentColor.BackColor = currentSelectedColor; //currentselectedcolor가 변경되면 자동으로 labelcurrentcolor backcolor도 변경되게 한다.
            }
        }


        public FileInfo TemplateFilePath
        {
            get { return templateFilePath; }
            set { templateFilePath = value; }
        }
        public static string NewTemplateName
        {
            get { return newTemplateName; }
            set { newTemplateName = value; }
        }
        public static Dictionary<string, Dictionary<string, int>> CategoryTemplateDic
        {
            get { return categoryTemplateDic; }
            set { categoryTemplateDic = value; }
        }
        
        public TreeNode RootNode
        {
            get { return rootNode; }
            set { rootNode = value; }
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
        public List<System.Windows.Forms.Label> CategoryItemsLabelList
        {
            get { return categoryItemsLabelList; }
            set { categoryItemsLabelList = value; }
        }
        #endregion


    }
}
