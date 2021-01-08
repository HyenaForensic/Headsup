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
            diagram1.DragDrop += Diagram1_DragDrop;
            InitControls(); //각 컨트롤러 초기화
        }

        #region 초기 세팅 관련
        private void InitControls()
        {
            InitButtonStyle(); //버튼 스타일 초기화
            InitTree(); //트리에 루트 추가
            InitToolStripComboBox(); //템플릿을 초기화한다.
            //InitEditControl(); //Editor 초기화
            //InitDiagram(); //다이어그램 초기화
        }
        private void InitToolStripComboBox() //템플릿 콤보 박스 초기화
        {
            CategoryTemplate = new Dictionary<string, Dictionary<string, int>>();
            TemplateFilePath = new FileInfo(Application.StartupPath + @"\category_template");
            if (File.Exists(TemplateFilePath.FullName)) //템플릿 파일이 있으면
            {
                CategoryTemplate = ConvertDic.FileToDic(TemplateFilePath.FullName);
                foreach (KeyValuePair<string, Dictionary<string, int>> tmp in CategoryTemplate)
                {
                    toolStripComboBoxExTemplate.Items.Add(tmp.Key);

                }
            }
        }
        private void InitButtonStyle() //button style 초기화
        {
            AddButton = new SfButton();
            AddButton.Click += sfButtonAdd_Click;
            AddButton.Text = "Add";
            AddButton.Style.Border = new Pen(Color.Black, 1);
            CategoryView();
            
            sfButtonDrawDiagram.Style.Border = new Pen(Color.Black, 1); //다이어그램 노드 추가 버튼 테두리 설정
            
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
            TableLayoutManager tlLayout = new TableLayoutManager(this.diagram1.Model, 7, 7);
            tlLayout.VerticalSpacing = 20;
            tlLayout.HorizontalSpacing = 20;
            tlLayout.CellSizeMode = CellSizeMode.EqualToMaxNode;
            tlLayout.Orientation = Orientation.Horizontal;
            tlLayout.MaxSize = new SizeF(500, 600);

            this.diagram1.LayoutManager = tlLayout;
            //this.diagram1.LayoutManager.UpdateLayout(null);.AttachModel(model1);
        }
        #endregion

        #region 다이어그램 관련

        private void Diagram1_DragDrop(object sender, DragEventArgs e) //Diagram으로 드래그 앤 드랍을 했을 경우 처리
        {
            if(CurrentSelectedColor.Name != "0") //카테고리 색을 선택한 경우만 처리
            {
                //다이어그램 노드에 추가하기 위해 textbox 객체를 만든다.
                TextBox txtBox = new TextBox();
                txtBox.Multiline = true;
                txtBox.Text = editControl.SelectedText;

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
                FileInfo file = new FileInfo(openFile1.FileName);
                OpenTextFileToEditControl(file.FullName);

                //아래것을 하면 셀렉트 컬러가 개판이 된다.
                //IBackgroundFormat format = editControl.RegisterBackColorFormat(Color.WhiteSmoke, Color.White);
                //for (int i = 1; i <= editControl.CurrentLine; i++)
                //{
                //    if (i % 2 == 0)
                //    {
                //        editControl.SetLineBackColor(i, true, format);
                //    }
                //}

                TreeNode node = new TreeNode(file.Name);
                node.ToolTipText = file.FullName;
                RootNode.Nodes.AddRange(new TreeNode[] { node }); //트리를 추가한다.
            }
        }
        #endregion

        private bool OpenTextFileToEditControl(string filePath)
        {
            CurrentFilePath = filePath;
            string readText = File.ReadAllText(filePath);
            ////EditControl 설정
            editControl.Text = readText; // editcontrol.loadfile로 하면 인코딩이 깨진다.
            //editControl.ScrollPosition = new Point(Top);
            return true;
        }

        #region 트리뷰 관련 이벤트
        private void documentExplorer1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) //노드를 더블클릭한 경우
        {
            if(e.Node.Level == 1) //루트가 아니면 (자식은 한 레벨 뿐)
            {
                OpenTextFileToEditControl(e.Node.ToolTipText);
            }
        }
        #endregion

        #region 카테고리 관련 이벤트
        private void CategoryView()
        {
            flowLayoutPanelCategory.Controls.Clear(); //먼저 전체를 지운다.
            flowLayoutPanelCategory.Controls.Add(AddButton);
            foreach (SfButton sfButtonTmp in CategoryBtns)
            {
                flowLayoutPanelCategory.Controls.Add(sfButtonTmp);
            }
        }
        private void sfButtonAdd_Click(object sender, EventArgs e) //카테고리에서 add button 클릭 시
        {
            if (toolStripComboBoxExTemplate.SelectedIndex != -1)
            { //템플릿이 선택되어있다면
                AddCategory AddForm = new AddCategory();
                AddForm.ShowDialog();

                if (selectedColorFlagFromAddForm)
                {
                    MakeCategoryBtn(CategoryNameFromAddForm, BlockColorFromAddForm);
                    CategoryView(); //버튼을 다시 뿌려준다.

                    //템플릿 딕셔너리에 저장한다. [템플릿 이름][버튼이름] = int형컬러 형태로 들어간다.
                    categoryTemplate[toolStripComboBoxExTemplate.Text].Add(CategoryNameFromAddForm, BlockColorFromAddForm.ToArgb());
                    selectedColorFlagFromAddForm = false; //처리가 다 끝나면 다시 false를 해주어야 다음에 동일 처리가 가능하다.
                }
            }
            else
            {
                MessageBox.Show("템플릿을 먼저 선택해주세요");
            }
        }
        private void MakeCategoryBtn(string name, Color color) //카테고리 버튼 만들기
        {
            SfButton sfButtonTmp = new SfButton();
            sfButtonTmp.Click += CategoryBtnClick;
            sfButtonTmp.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            sfButtonTmp.Size = new System.Drawing.Size(96, 28);
            sfButtonTmp.Text = name;
            sfButtonTmp.Style.BackColor = color; //기본 바탕색 변경
            sfButtonTmp.Style.FocusedBackColor = color; //포커싱 됬을 때 바탕색 변경
            sfButtonTmp.Style.HoverBackColor = color; //마우스 올렸을 때 바탕색 변경
            sfButtonTmp.Style.PressedBackColor = color; //클릭한 상태 바탕색 변경
            CategoryBtns.Add(sfButtonTmp);
        }
        private void CategoryBtnClick(object sender, EventArgs e) //카테고리 버튼 클릭 이벤트
        {
            SfButton sfButton = (SfButton)sender;
            CurrentSelectedColor = sfButton.BackColor;
            labelCurrentColor.BackColor = sfButton.BackColor;

        }
        private void toolStripButtonNew_Click(object sender, EventArgs e) //카테고리 new 버튼 클릭
        {
            NewTemplateName = null; //먼저 초기화를 한다.
            NewTemplate newTemplateForm = new NewTemplate();
            newTemplateForm.ShowDialog();
            if (NewTemplateName != null)
            {
                CategoryTemplate.Add(NewTemplateName, new Dictionary<string, int>());
                CategoryBtns.Clear(); //버튼 리스트를 다 지운다.
                CategoryView();
                toolStripComboBoxExTemplate.SelectedIndex = toolStripComboBoxExTemplate.Items.Add(NewTemplateName); ; //템플릿 리스트를 추가하면서 선택한다.
            }
        }
        private void toolStripButtonSave_Click(object sender, EventArgs e) //카테고리 저장을 클릭한 경우
        {
            if (ConvertDic.DicToFile(CategoryTemplate, TemplateFilePath.FullName))
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
                    CategoryBtns.Clear(); //버튼 리스트를 전부 삭제
                    if (categoryTemplate.ContainsKey(toolStripComboBoxExTemplate.Text)) //키가 있는 경우
                    {
                        categoryTemplate.Remove(toolStripComboBoxExTemplate.Text);
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
            CategoryBtns.Clear(); //버튼 리스트를 다 지운다.
            foreach (KeyValuePair<string, int> btn in CategoryTemplate[toolStripComboBoxEx.Text])
            {
                MakeCategoryBtn(btn.Key, Color.FromArgb(btn.Value));
            }
            CategoryView();
        }

        #endregion

        #region 변수 선언
        private Color currentSelectedColor;
        private static Color blockColorFromAddForm; //텍스트의 블록색 저장 변수
        private static string categoryNameFromAddForm; //AddCategory 폼에서 이름을 가져오기 위한 static 변수
        private static bool selectedColorFlagFromAddForm = false;
        private static string newTemplateName;
        private List<SfButton> categoryBtns = new List<SfButton>(); //카테고리 버튼 리스트
        private TreeNode rootNode; //트리의 루트
        private string currentFilePath;
        private static Dictionary<string, Dictionary<string, int>> categoryTemplate;
        private FileInfo templateFilePath;
        private SfButton addButton;

        public SfButton AddButton
        {
            get { return addButton; }
            set { addButton = value; }
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
        public static Dictionary<string, Dictionary<string, int>> CategoryTemplate
        {
            get { return categoryTemplate; }
            set { categoryTemplate = value; }
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
        public TreeNode RootNode
        {
            get { return rootNode; }
            set { rootNode = value; }
        }
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
