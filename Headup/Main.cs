using Headup.Popup;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Edit.Interfaces;
using Headup.Util;
using Syncfusion.Windows.Forms.Tools;
using System.Data;
using Syncfusion.Windows.Forms.Edit.Utils;
using System.Drawing.Drawing2D;

namespace Headup
{
    public partial class Main : SfForm
    {
        private int diagramLayerHeight = 180;
        public Main()
        {
            InitializeComponent();
            Init(); //각 컨트롤러 및 파일 초기화
            EventSet(); //이벤트 생성
        }

        #region 초기 세팅 관련
        private void EventSet()
        {
            diagram1.DragDrop += Diagram1_DragDrop;
            editControl.MouseUp += EditControl_MouseUp;
            #region 이벤트 테스트를 위함
            editControl.ContextChoiceRightClick += EditControl_ContextChoiceRightClick;
            editControl.SelectionChanged += EditControl_SelectionChanged;
            editControl.ContextPromptSelectionChanged += EditControl_ContextPromptSelectionChanged;
            editControl.ContextChoiceItemSelected += EditControl_ContextChoiceItemSelected;
            editControl.ContextChoiceSelectedTextInsert += EditControl_ContextChoiceSelectedTextInsert;
            editControl.MouseCaptureChanged += EditControl_MouseCaptureChanged;
            editControl.MouseClick += EditControl_MouseClick;
            editControl.MouseDoubleClick += EditControl_MouseDoubleClick;
            editControl.MouseDown += EditControl_MouseDown;
            editControl.MouseEnter += EditControl_MouseEnter;
            editControl.MouseHover += EditControl_MouseHover;
            editControl.MouseLeave += EditControl_MouseLeave;
            editControl.MouseWheel += EditControl_MouseWheel;
            #endregion
        }




        #region 이벤트 테스트를 위함
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
        private void EditControl_MouseWheel(object sender, MouseEventArgs e)
        {
            listBox1.Items.Add("\n EditControl_MouseWheel\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void EditControl_MouseLeave(object sender, EventArgs e)
        {
            listBox1.Items.Add("\n EditControl_MouseLeave\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void EditControl_MouseHover(object sender, EventArgs e)
        {
            listBox1.Items.Add("\n EditControl_MouseHover\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void EditControl_MouseEnter(object sender, EventArgs e)
        {
            listBox1.Items.Add("\n EditControl_MouseEnter\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void EditControl_MouseDown(object sender, MouseEventArgs e)
        {
            listBox1.Items.Add("\n EditControl_MouseDown\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void EditControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listBox1.Items.Add("\n EditControl_MouseDoubleClick\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void EditControl_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Items.Add("\n EditControl_MouseClick\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void EditControl_MouseCaptureChanged(object sender, EventArgs e)
        {
            listBox1.Items.Add("\n EditControl_MouseCaptureChanged\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void EditControl_ContextChoiceRightClick(IContextChoiceController sender, Syncfusion.Windows.Forms.Edit.ContextChoiceItemEventArgs e)
        {
            listBox1.Items.Add("\nEditControl_ContextChoiceRightClick\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void EditControl_SelectionChanged(object sender, EventArgs e)
        {
            listBox1.Items.Add("\nEditControl_SelectionChanged\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
        private void EditControl_ContextPromptSelectionChanged(Syncfusion.Windows.Forms.Edit.Forms.Popup.ContextPrompt sender, Syncfusion.Windows.Forms.Edit.ContextPromptSelectionChangedEventArgs e)
        {
            listBox1.Items.Add("\nEditControl_ContextPromptSelectionChanged\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
        private void EditControl_ContextChoiceSelectedTextInsert(IContextChoiceController sender, Syncfusion.Windows.Forms.Edit.ContextChoiceTextInsertEventArgs e)
        {
            listBox1.Items.Add("\nEditControl_ContextChoiceSelectedTextInsert\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
        private void EditControl_ContextChoiceItemSelected(IContextChoiceController sender, Syncfusion.Windows.Forms.Edit.ContextChoiceItemSelectedEventArgs e)
        {
            listBox1.Items.Add("\nEditControl_ContextChoiceItemSelected\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
        #endregion

        private void Init()
        {
            InitData(); // 각종 데이터 초기화 , 초기 DataSet에 초기 필요한 테이블을 만든다.
            InitCategory(); //버튼 스타일 초기화
            InitTree(); //트리에 루트 추가
            InitToolStripComboBox(); //템플릿을 초기화한다.
            InitEditControl(); //Editor 초기화
            InitDiagram(); //다이어그램 초기화
        }
        private void InitToolStripComboBox() //템플릿 콤보 박스 초기화
        {
            toolStripComboBoxExTemplate.Text = "템플릿 선택";
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
            CategoryItemsLabelList = new List<System.Windows.Forms.Label>();
            CategoryView();
            sfButtonDrawDiagram.Style.Border = new Pen(Color.Black, 1); //다이어그램에 자동 노드 추가 버튼 테두리 설정
        }

        private void InitTree() //TreeControl 초기화
        {
            //루트 트리 구성
            RootNode = new TreeNode("목록"); //root tree 를 만든다.
            documentExplorer1.Nodes.AddRange(new TreeNode[] { RootNode }); //루트 트리를 추가한다.
            SetDsToTree();
        }
        private void SetDsToTree()
        {
            foreach (DataTable dt in SelectedStatusDs.Tables)
            {
                if (dt.TableName == "CaseFileList") //CaseList 테이블만 처리한다.
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        FileInfo file = new FileInfo(row["path"].ToString());
                        TreeNode node = new TreeNode(file.Name); //노드를 추가한다.
                        node.ToolTipText = file.FullName;
                        RootNode.Nodes.AddRange(new TreeNode[] { node }); //트리를 추가한다.
                    }
                }
            }
        }
        private void InitEditControl() //editControl 초기화
        {
            //이걸 하면 확대버튼이 사라진다.....ㅡㅡ
            //IConfigLanguage currentConfigLanguage = this.editControl.Configurator.CreateLanguageConfiguration("New");
            //editControl.ApplyConfiguration(currentConfigLanguage);
            if (File.Exists(@"./config.xml"))
            {
                editControl.Configurator.Open(@"./config.xml");
                editControl.ApplyConfiguration("NamFont");
            }
            else
            {
                MessageBox.Show("xml 파일이 없습니다.");
            }


            //editControl의 상태표시줄 설정
            this.editControl.StatusBarSettings.Visible = true; // Shows the built-in status bar.
            this.editControl.StatusBarSettings.TextPanel.Visible = true; // Enable the TextPanel in the StatusBar.
            this.editControl.StatusBarSettings.GripVisibility = Syncfusion.Windows.Forms.Edit.Enums.SizingGripVisibility.Visible; // Set the visibility of the status bar sizing grip.
        }

        private void InitDiagram() //diagram 초기화
        {
            diagram1.Model.BoundaryConstraintsEnabled = false; //노드가 밖으로 나갈 수 있게 해줌
            diagram1.Model.SizeToContent = true; //노드 위치에 따라 document 크기를 자동으로 늘어나게 해줌
            diagram1.Model.MinimumSize = new SizeF(800, 600); //최소 크기를 지정해야된다.

            //document 크기가 자동으로 움직이기 때문에 최초 노드를 추가하면 0,0 포인트에 들어가서 모양 만들기가 어렵다.
            //그래서 node하나를 추가하고 visible을 false로 하여 보이지 않는 노드를 최초 하나만들어 자리만 잡는다.
            TextNode subject = new TextNode("");
            subject.Visible = false; //이렇게 해야 보이지 않고 자리만 잡힌다.
            diagram1.Model.AppendChild(subject);
            diagram1.View.SelectionList.Clear(); //Clear를 안하면 마지막 노드가 선택되어 있는 것처럼 보여서 보기 싫게 보인다.

            //배경을 구현하기 위한 코드
            for (int i = 0; i < 4; i++)
            {
                //배경 색 넣는 부분 (도형을 이용)
                Syncfusion.Windows.Forms.Diagram.Rectangle rectangleTmp = new Syncfusion.Windows.Forms.Diagram.Rectangle(0, i * diagramLayerHeight, diagram1.View.Width, diagramLayerHeight); //x, y시작점 , 가로,세로 길이
                //백그라운드 색 지정을 나중에 하면 투명도가 생략되기 때문에 색 지정을 먼저 한다.
                if (i == 0) { rectangleTmp.FillStyle.Color = System.Drawing.Color.LightBlue; } // 색지정
                else if (i == 1) { rectangleTmp.FillStyle.Color = System.Drawing.Color.MistyRose; } // 색지정
                else if (i == 2) { rectangleTmp.FillStyle.Color = System.Drawing.Color.LightYellow; } // 색지정
                else if (i == 3) { rectangleTmp.FillStyle.Color = System.Drawing.Color.LightGreen; } // 색지정
                rectangleTmp.FillStyle.ColorAlphaFactor = 100; //투명도 주기
                rectangleTmp.LineStyle.LineColor = Color.Aqua; //테두리 색 변경
                rectangleTmp.EditStyle.AllowSelect = false; //선택을 못하게 한다. (그래야 배경이 그대로니까)
                diagramLayerBackgroundList.Add(rectangleTmp); //나중에 수정을 위해 리스트에 저장
                diagram1.Model.AppendChild(rectangleTmp);

                //layer에 라벨을 붙이기 위한 코드
                RectangleF point = new RectangleF(0, i * diagramLayerHeight, 60, 20); //자리 배치
                TextNode textNodeTmp = new TextNode("Layer " + (i + 1), point);
                textNodeTmp.FontStyle.Size = 12; //글자 크기 변경
                textNodeTmp.LineStyle.LineColor = Color.Transparent; //테두리 선 제거
                textNodeTmp.EditStyle.AllowSelect = false; //선택을 못하게 한다.
                diagram1.Model.AppendChild(textNodeTmp);
            }
        }


        #endregion

        #region 메뉴 관련
        private void barItemNew_Click(object sender, EventArgs e) //New 클릭 시
        {
            ClearAllControls();
            Init();
        }
        private void barItemOpen_Click(object sender, EventArgs e) //open 클릭시
        {
            OpenFileDialog openFile1 = new OpenFileDialog();
            openFile1.Filter = "HM Status Files|*.hsf";

            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK) //파일 오픈
            {
                ClearAllControls(); //모든 컨트롤 정보 삭제
                Init(); //모든 데이터 및 컨트롤 초기화
                SelectedStatusDs.Tables.Remove("CaseFileList"); // Init과정에서 CaseFileList가 만들어지기 때문에 여기에서는 지워준다.

                CurrentCasePath = new FileInfo(openFile1.FileName);
                DatabaseRelate db = new DatabaseRelate(CurrentCasePath.FullName);
                List<string> tableList = db.GetTableList();
                foreach (string tableName in tableList)
                {
                    if (tableName == "CaseFileList") //casefilelist 라면
                    { //list는 넣지 않는다.
                        SelectedStatusDs.Tables.Add(db.GetDataTableFromDb(tableName, new string[] { "fileName", "caseTableName", "path" }));
                    }
                    else // 테이블 이름이 casefilelist가 아니면 케이스 테이블이다.
                    {
                        SelectedStatusDs.Tables.Add(db.GetDataTableFromDb(tableName, new string[] { "startLine", "endLine", "startColumn", "endColumn", "text", "templateName", "categoryName", "categoryIndex", "color", "IsGoToDiagram" }));
                        CaseCnt++; //가져온 만큼 cnt를 올린다.
                    }
                }
                diagram1.LoadBinary(CurrentCasePath.DirectoryName + @"\" + CurrentCasePath.Name + ".edd");
                diagram1.Refresh();

                SetDsToTree(); //트리를 만든다.
            }
        }
        private void barItemSave_Click(object sender, EventArgs e) //save 클릭시
        {
            if (CurrentCasePath == null) //케이스 Db 파일이 연결되어있지 않은 경우
            {
                SaveCaseDbFile(true);
            }
            else //케이스 파일이 연결되어 있는 경우
            {
                SaveCaseDbFile(false);
            }
        }
        private void barItemSaveAs_Click(object sender, EventArgs e) //Save As 클릭 시
        {
            SaveCaseDbFile(true);
        }

        private void SaveCaseDbFile(bool type) //true면 새로 지정, false면 기존 지정
        {
            if (type) //새로 지정하는 경우
            {
                SaveFileDialog saveFile = new SaveFileDialog(); //있던 파일을 덮어쓰기 하면 파일이 삭제되지 않고 데이터까지 덮어써진다. 나중에 수정해야함.
                saveFile.Filter = "HM Status Files|*.hsf";
                if (saveFile.ShowDialog() == DialogResult.OK) //dialog 오픈
                {
                    CurrentCasePath = new FileInfo(saveFile.FileName);
                    AllDataSetToDb();
                }
            }
            else
            {
                AllDataSetToDb();
            }
            diagram1.SaveBinary(CurrentCasePath.DirectoryName + @"\" + CurrentCasePath.Name + ".edd"); //무조건 edd파일은 저장한다. 그래서 Save//Save As 상관없는 자리인 여기에서 처리한다.
        }
        private void AllDataSetToDb()
        {
            //저장 코드 작성
            DatabaseRelate db = new DatabaseRelate(CurrentCasePath.FullName);
            foreach (string tableName in db.GetTableList()) //테이블 전체를 지우고 시작한다. 리셋 느낌으로 간다.
            {
                db.dropTable(tableName);
            }
            foreach (DataTable dt in SelectedStatusDs.Tables)
            {
                Dictionary<string, string> colList = new Dictionary<string, string>();
                foreach (DataColumn col in dt.Columns) //컬럼 구조 파악
                {
                    string type = "";
                    if (col.DataType.Name == "Int32")
                    {
                        type = "integer";
                    }
                    else if (col.DataType.Name == "String")
                    {
                        type = "text";
                    }
                    else if (col.DataType.Name == "Boolean")
                    {
                        type = "boolean";
                    }
                    colList.Add(col.ColumnName, type);
                }
                db.CreateTable(dt.TableName, colList); //테이블 만들기
                db.InsertAllDataSetToDb(selectedStatusDs, dt.TableName, new List<string>(colList.Keys));
            }
            db.SqlClose();
        }
        private void barItemImportFile_Click(object sender, EventArgs e) //Import TXT 클릭 시
        {
            OpenFileDialog openFile1 = new OpenFileDialog();
            openFile1.Filter = "TEXT File|*.txt";
            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK) //파일 오픈
            {
                FileInfo file = new FileInfo(openFile1.FileName);
                if (ExistsCase(file.FullName)) //동일한 케이스 파일이 이미 추가되어 있다면
                {
                    MessageBox.Show("이미 추가된 파일입니다.");
                }
                else
                {
                    string caseName = "Case" + CaseCnt.ToString(); //케이스 이름 설정

                    TreeNode node = new TreeNode(file.Name); //노드를 추가한다.
                    node.ToolTipText = file.FullName;
                    RootNode.Nodes.AddRange(new TreeNode[] { node }); //트리를 추가한다.

                    //ds에 케이스 테이블 추가
                    DataTable dt = new DataTable();
                    dt.TableName = caseName;
                    dt.Columns.Add("startLine", typeof(Int32));
                    dt.Columns.Add("endLine", typeof(Int32));
                    dt.Columns.Add("startColumn", typeof(Int32));
                    dt.Columns.Add("endColumn", typeof(Int32));
                    dt.Columns.Add("text", typeof(String));
                    dt.Columns.Add("templateName", typeof(String));
                    dt.Columns.Add("categoryName", typeof(String));
                    dt.Columns.Add("categoryIndex", typeof(Int32));
                    dt.Columns.Add("color", typeof(Int32));
                    dt.Columns.Add("IsGoToDiagram", typeof(bool));
                    SelectedStatusDs.Tables.Add(dt);

                    OpenTextFileToEditControl(file.FullName); //파일의 텍스트를 모두 읽어온다. 이때 색도 변경됨

                    //ds의 casefilelist 테이블에 데이터 추가
                    selectedStatusDs.Tables["CaseFileList"].Rows.Add(file.Name, caseName, file.FullName);
                    CaseCnt++;
                    CurrentDsTableName = caseName;
                }
            }
        }

        #endregion

        #region 파일 및 각종 컨트롤 핸들링 관련
        private bool ExistsCase(string originalPath) //ds를 넣으면 케이스가 존재하는지 찾는다.
        {
            foreach (DataRow row in selectedStatusDs.Tables["CaseFileList"].Rows)
            {
                if (row["path"].ToString() == originalPath) //같은것이 있다면
                {
                    return true;
                }
            }

            return false;
        }

        private void ClearAllControls()
        {
            SelectedStatusDs.Clear(); //DS 삭제
            RootNode.Remove(); //노드 삭제
            editControl.Text = ""; //editcontrol 삭제
            //다이어그램 초기화 부분 넣어야 함
            CurrentFilePath = ""; //labelfilepath 삭제
            toolStripComboBoxExTemplate.Items.Clear(); //콤보박스리스트 아이템 삭제
            CurrentSelectedColor = new Color(); //현재 선택되어있는 컬러를 초기화한다.
        }

        //private void ResetDbTmpFile() //tmp 파일을 지우고 초기화한다.
        //{
        //    if (File.Exists("status.tmp")) //tmp db 파일이 있으면 지운다.
        //    {
        //        File.Delete("status.tmp");
        //    }
        //    InitStatusDb("status.tmp"); // DB에 기본적인 테이블 추가
        //    InitDataSet(); // 기본적인 테이블 추가
        //}
        //private void InitStatusDb(string dbPath)
        //{
        //    DatabaseRelate db = new DatabaseRelate(dbPath);

        //    //db에 추가
        //    Dictionary<string, string> colList = new Dictionary<string, string>();
        //    colList.Add("rowId", "int");
        //    colList.Add("caseTableName", "text");
        //    colList.Add("path", "text");
        //    db.CreateTable("CaseFileList", colList);

        //    db.SqlClose();
        //}
        private void InitData()
        {
            SelectedStatusDs = new DataSet();
            CaseCnt = 1;
            //ds에 추가
            DataTable dt = new DataTable();
            dt.TableName = "CaseFileList";
            dt.Columns.Add("fileName", typeof(string));
            dt.Columns.Add("caseTableName", typeof(string));
            dt.Columns.Add("path", typeof(string));
            SelectedStatusDs.Tables.Add(dt);
            CurrentCasePath = null;
            NodeXCnt = new List<int>() { 1, 1, 1, 1 }; //다이어그램 레이어는 4단계이기 때문에 4개를 초기화한다.
            diagramLayerBackgroundList = new List<Syncfusion.Windows.Forms.Diagram.Rectangle>();
        }
        #endregion

        #region 트리뷰 관련 이벤트
        private void documentExplorer1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) //노드를 더블클릭한 경우
        {
            if (e.Node.Level == 1) //루트가 아니면 (자식은 한 레벨 뿐)
            {
                if (CurrentFilePath != e.Node.ToolTipText) //현재 열려있는 파일이 아니라면 (현재 열려있는 파일이라면 굳이 다시 열 필요 없다.)
                {
                    CurrentFilePath = e.Node.ToolTipText; //currentfilepath를 먼저 변경한다.
                    SetCurrentPageTableName(); //오픈한 페이지의 테이블 이름을 변경한다.
                    OpenTextFileToEditControl(CurrentFilePath); //파일을 열어 editControl에 넣는다.
                }
            }
        }
        private bool OpenTextFileToEditControl(string filePath) //파일을 오픈할때 이벤트
        {
            CurrentFilePath = filePath;
            Ducument = File.ReadAllText(filePath);

            //교차로 배경색을 주는 부분 , 아래것을 하면 셀렉트 컬러가 개판이 된다.
            IBackgroundFormat format = editControl.RegisterBackColorFormat(Color.WhiteSmoke, Color.White);
            for (int i = 1; i <= editControl.CurrentLine; i++)
            {
                if (i % 2 == 0)
                {
                    editControl.SetLineBackColor(i, true, format);
                }
            }

            //저장되어있는 상태가 있다면 적용한다.
            foreach (DataTable dt in SelectedStatusDs.Tables)
            {
                if (dt.TableName == CurrentDsTableName) //현재 페이지에 해당하는 테이블을 찾으면
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        editControl.SetSelection(Convert.ToInt32(row["startColumn"]), Convert.ToInt32(row["startLine"]), Convert.ToInt32(row["endColumn"]), Convert.ToInt32(row["endLine"]));
                        format = editControl.RegisterBackColorFormat(Color.FromArgb(Convert.ToInt32(row["color"])), Color.White);
                        editControl.SetSelectionBackColor(format);
                    }
                    editControl.SelectionCancel(); //취소를 안하면 마지막에 셀렉트한 텍스트가 그대로 셀렉트 되어 있는 상태이기 때문에 취소를 한다.
                }
            }

            return true;
        }
        private void SetCurrentPageTableName() //현재 페이지의 테이블 이름을 알아와서 세팅함
        {
            DataRow[] rows = SelectedStatusDs.Tables["CaseFileList"].Select("path='" + CurrentFilePath + "'");
            CurrentDsTableName = rows[0][1].ToString(); //테이블 이름을 알아내서 저장
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
        private void ModifyCategoryItems(string name, Color color) //카테고리 우클릭 메뉴 버튼 수정하기
        {
            foreach (System.Windows.Forms.Label label in CategoryItemsLabelList)
            {
                if (label.Text == CurrentCategoryLabel.Text)
                {
                    CategoryTemplateDic[toolStripComboBoxExTemplate.Text].Remove(label.Text); //기존 딕셔너리 키는 변경이 어려우니 지운다. (호출부에서 다시 생성한다)
                    label.Text = name;
                    label.BackColor = color;
                    break;
                }
            }
        }
        private void DeleteCategoryItems(string name) //카테고리 우클릭 메뉴 버튼 삭제하기
        {
            foreach (System.Windows.Forms.Label label in CategoryItemsLabelList)
            {
                if (label.Text == CurrentCategoryLabel.Text)
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
        private void toolStripButtonLoad_Click(object sender, EventArgs e) //카테고리 불러오기 클릭 시
        {
            OpenFileDialog openFile1 = new OpenFileDialog();
            openFile1.Filter = "All Files|*.*";

            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK) //파일 오픈
            {
                Dictionary<string, Dictionary<string, int>> tmpDic = new Dictionary<string, Dictionary<string, int>>();
                tmpDic = ConvertDic.FileToDic(openFile1.FileName);
                foreach (KeyValuePair<string, Dictionary<string, int>> tmp in tmpDic)
                {
                    CategoryTemplateDic.Add(tmp.Key, tmp.Value);
                    toolStripComboBoxExTemplate.Items.Add(tmp.Key); //콤보박스 리스트에 템플릿 추가
                }
                MessageBox.Show("템플릿 불러오기 성공", "성공", MessageBoxButtons.OK);
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
            if (toolStripComboBoxEx.Text != "")
            { //선택된 아이템이 잇을 경우만 처리
                foreach (KeyValuePair<string, int> btn in CategoryTemplateDic[toolStripComboBoxEx.Text])
                {
                    MakeCategoryItems(btn.Key, Color.FromArgb(btn.Value));
                }
                CategoryView();
            }
        }
        private void LabelTmp_MouseClick(object sender, MouseEventArgs e) //카테고리의 색을 선택할 경우
        {
            CurrentCategoryLabel = (System.Windows.Forms.Label)sender;

            if (e.Button == MouseButtons.Right) //오른쪽 버튼 클릭하면 메뉴 이벤트 발생
            {
                contextMenuStripExCategoryItemMenu.Show(CurrentCategoryLabel, new Point(e.X, e.Y));
            }
            else if (e.Button == MouseButtons.Left) //왼쪽 버튼 클릭하면 색 변경
            {
                CurrentSelectedColor = CurrentCategoryLabel.BackColor;

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
        private bool IsChoiceCategory()
        {
            if (CurrentSelectedColor.Name != "0")
            {
                return true;
            }
            else
            {
                MessageBox.Show("카테고리를 선택해주세요", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        #endregion

        #region 다이어그램 관련 이벤트

        private void Diagram1_DragDrop(object sender, DragEventArgs e) //Diagram으로 드래그 앤 드랍을 했을 경우 처리
        {
            if (IsChoiceCategory()) //카테고리 색을 선택한 경우만 처리
            {
                //DragEventArgs에 마우스 포인트가 화면 기준으로 되어 있기 때문에 컨트롤(다이어그램)의 위치를 찾아 그만큼 빼주어야 한다.
                //e는 화면 기준의 마우스 위치, diagram1.AccessibilityObject.Bounds.Location은 다이어그램 컨트롤 시작 위치, origin은 page위치
                //그리고 확대 축소를 할 때마다 데이터가 달라지기 때문에 아래와 같이 계산하면 정확한 위치가 나온다.
                float scale = diagram1.Magnification / 100; //현재 줌 상태를 확인하여 이렇게 계산한다. (라고 했는데 잘 안됨...ㅡㅡ)
                int x = (int)((e.X - diagram1.AccessibilityObject.Bounds.Location.X + (int)diagram1.Origin.X) * scale); //x값을 구한다.
                int y = (int)((e.Y - diagram1.AccessibilityObject.Bounds.Location.Y + (int)diagram1.Origin.Y) * scale); //y값을 구한다.
                AddDiagramNode(editControl.SelectedText, x, y, CurrentSelectedColor); //노드를 추가한다.
                DataRow row = SelectedStatusDs.Tables[currentDsTableName].Rows[SelectedStatusDs.Tables[currentDsTableName].Rows.Count - 1]; //드래그 드랍으로 추가한 노드는 마지막 값을 알아와 IsGoToDiagram을 true로 변경해야 하기 때문에 마지막 row를 가져옴
                row["IsGoToDiagram"] = true;
            }
        }

        private void AddDiagramNode(string text, int x, int y, Color color)
        {
            //다이어그램 노드에 추가하기 위해 textbox 객체를 만든다.
            TextBox txtBox = new TextBox();
            txtBox.Multiline = true;
            txtBox.Text = text;

            ControlNode ctrlnode = new ControlNode(txtBox, new RectangleF(x, y, 140, 50));
            ctrlnode.HostingControl.BackColor = color;
            ctrlnode.ActivateStyle = Syncfusion.Windows.Forms.Diagram.ActivateStyle.ClickPassThrough;

            diagram1.Model.AppendChild(ctrlnode);
        }
        private void sfButtonDrawDiagram_Click(object sender, EventArgs e) //한꺼번에 다이어그램에 올리는 버튼 클릭 이벤트
        {
            if (currentFilePath == "") //로드된 파일이 없는 경우
            {
                MessageBox.Show("파일을 먼저 로드하세요");
            }
            else //로드된 파일이 있는 경우
            {
                if (CurrentDsTableName == "") //연결된 DB가 없는 경우
                {
                    MessageBox.Show("연결된 데이터베이스가 없습니다.");
                }
                else //연결된 DB가 있는 경우
                {
                    if (SelectedStatusDs.Tables[CurrentDsTableName].Rows.Count == 0) //row데이터가 없다면
                    {
                        MessageBox.Show("추가할 데이터가 없습니다.");
                    }
                    else
                    { //row데이터가 있다면
                        foreach (DataRow row in SelectedStatusDs.Tables[CurrentDsTableName].Rows) //row데이터만큼 처리
                        {
                            if (row["IsGoToDiagram"].ToString() == "False") //아직 다이어그램에 추가되지 않은 데이터라면
                            {
                                int categoryIndex = Convert.ToInt32(row["categoryIndex"]);
                                if (categoryIndex < 5)
                                {
                                    int x = 200 * (NodeXCnt[categoryIndex - 1] - 1) + 40; //categoryIndex는 1부터 시작하기 때문에 -1을 해준다.
                                    // 200은 노드끼리의 거리, NodeXCnt[categoryIndex - 1] 마지막 노드, -1 0부터 해야 200*0으로 시작됨, +40 시작점은 40부터 시작
                                    NodeXCnt[categoryIndex - 1]++;
                                    int y = (diagramLayerHeight * (categoryIndex - 1)) + 40;
                                    AddDiagramNode(row["text"].ToString(), x, y, Color.FromArgb(Convert.ToInt32(row["color"])));
                                    row["IsGoToDiagram"] = true;
                                }
                                else
                                {
                                    MessageBox.Show("선택된 카테고리는 유효하지 않습니다.(1-4번째 카테고리만 처리 가능");
                                }
                            }
                        }
                    }
                }
            }
        }
        private void toolStripButtonBezierTool_Click(object sender, EventArgs e)
        {
            SetActiveTool("BezierTool");
        }

        private void toolStripButtonDirectedLineLink_Click(object sender, EventArgs e)
        {
            SetActiveTool("DirectedLineLinkTool");
        }

        private void toolStripButtonLineLink_Click(object sender, EventArgs e)
        {
            SetActiveTool("LineLinkTool");
        }

        private void toolStripButtonOrgLineTool_Click(object sender, EventArgs e)
        {
            SetActiveTool("OrgLineConnectorTool");
        }

        private void toolStripButtonOrthoTool_Click(object sender, EventArgs e)
        {
            SetActiveTool("OrthogonalLinkTool");
        }

        private void toolStripButtonPolyTool_Click(object sender, EventArgs e)
        {
            SetActiveTool("PolylineLinkTool");
        }

        private void toolStripButtonSplineTool_Click(object sender, EventArgs e)
        {
            SetActiveTool("SplineTool");
        }
        private void SetActiveTool(string toolName)
        {
            diagram1.Controller.ActivateTool(toolName);
        }
        #endregion

        #region EditControl 관련 및 이벤트 처리
        private void ChangeSelectBackColor() //색을 선택하여 변경한 경우
        {
            IBackgroundFormat format = editControl.RegisterBackColorFormat(CurrentSelectedColor, Color.White);
            editControl.SetSelectionBackColor(format);
        }
        private void EditControl_MouseUp(object sender, MouseEventArgs e) //텍스트 선택 시 이벤트 처리
        {
            listBox1.Items.Add("\n EditControl_MouseUp\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;

            if (editControl.SelectedText.Length > 0 && IsChoiceCategory()) //선택한 영역이 있고, 색을 선택했다면
            {
                ChangeSelectBackColor(); //먼저 하이라이트된 글자의 백그라운드 색을 변경한다.
                if (editControl.Selection != null) //선택영역이 있다면, 정상적이라면 그럴일은 없지만 그래도 체크한다.
                {
                    //DS에 저장한다.
                    SelectedStatusDs.Tables[CurrentDsTableName].Rows.Add(editControl.Selection.Start.VirtualLine, editControl.Selection.End.VirtualLine, editControl.Selection.Start.VirtualColumn, editControl.Selection.End.VirtualColumn, editControl.SelectedText, toolStripComboBoxExTemplate.Text, CurrentCategoryLabel.Text, GetCategoryIndex(CurrentCategoryLabel.Text), CurrentSelectedColor.ToArgb(), false);
                }

            }
        }
        private int GetCategoryIndex(string categoryName) //index는 1부터 시작이고 0이 리턴되면 잘못된 결과이다.
        {
            int i = 1;
            foreach (System.Windows.Forms.Label categoryNameTmp in categoryItemsLabelList)
            {
                if (categoryNameTmp.Text == categoryName)
                {
                    return i;
                }
                i++;
            }
            return 0;
        }

        #endregion


        #region 변수 선언

        private static Color blockColorFromAddForm; //텍스트의 블록색 저장 변수
        private static string categoryNameFromAddForm; //AddCategory 폼에서 이름을 가져오기 위한 static 변수
        private static bool selectedColorFlagFromAddForm = false;
        private static string newTemplateName;
        private List<System.Windows.Forms.Label> categoryItemsLabelList; //카테고리 버튼 리스트
        private TreeNode rootNode; //트리의 루트
        private static Dictionary<string, Dictionary<string, int>> categoryTemplateDic;
        private FileInfo templateFilePath; //template 파일 경로
        private int caseCnt = 1; //케이스 번호
        private DataSet selectedStatusDs; //상태 저장 Ds
        private string ducument; //읽어드린 파일 저장 변수
        private string currentFilePath; //현재 열려있는 txt 파일 경로
        private FileInfo currentCasePath; //연결되어있는 케이스 DB 파일 경로
        private string currentDsTableName;
        private System.Windows.Forms.Label currentCategoryLabel; //다양한 이벤트 시 현재 선택되어 있는 라벨이 무엇인지 확인하기 위한 변수
        private Color currentSelectedColor; //현재 선택된 카테고리 색
        private List<int> nodeXCnt; //자동으로 노드를 추가할 때 마지막 위치를 알아내기 위한 리스트(레이어는 4단계이기 때문에 4개 리스트로 초기화한다.)
        private List<Syncfusion.Windows.Forms.Diagram.Rectangle> diagramLayerBackgroundList; //다이어그램의 백그라운드를 구분하기 위한 네모 노드 리스트

        public List<Syncfusion.Windows.Forms.Diagram.Rectangle> DiagramLayerBackgroundList
        {
            get { return diagramLayerBackgroundList; }
            set { diagramLayerBackgroundList = value; }
        }


        public List<int> NodeXCnt
        {
            get { return nodeXCnt; }
            set { nodeXCnt = value; }
        }


        public string CurrentDsTableName
        {
            get { return currentDsTableName; }
            set { currentDsTableName = value; }
        }
        public FileInfo CurrentCasePath
        {
            get { return currentCasePath; }
            set { currentCasePath = value; }
        }

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
        public System.Windows.Forms.Label CurrentCategoryLabel
        {
            get { return currentCategoryLabel; }
            set { currentCategoryLabel = value; }
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
        public DataSet SelectedStatusDs
        {
            get { return selectedStatusDs; }
            set { selectedStatusDs = value; }
        }
        public int CaseCnt
        {
            get { return caseCnt; }
            set { caseCnt = value; }
        }






        #endregion


    }
}
