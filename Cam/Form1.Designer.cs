namespace Cam
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBoxPreImg = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LabelGetCount = new System.Windows.Forms.Label();
            this.comboBoxBaudrate = new System.Windows.Forms.ComboBox();
            this.RichTextBoxRE = new System.Windows.Forms.RichTextBox();
            this.comboBoxPortName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOpenClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonImgStartStop = new System.Windows.Forms.Button();
            this.labelImgHeight = new System.Windows.Forms.Label();
            this.labelImgWidth = new System.Windows.Forms.Label();
            this.buttonImgSet = new System.Windows.Forms.Button();
            this.buttonImgClr = new System.Windows.Forms.Button();
            this.labelEnlargeTimes = new System.Windows.Forms.Label();
            this.trackBarImgEnlarge = new System.Windows.Forms.TrackBar();
            this.labelpixCnt = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelImgSize = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonImgDeal = new System.Windows.Forms.Button();
            this.buttonImgSave = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonImgLoad = new System.Windows.Forms.Button();
            this.pictureBoxCurImg = new System.Windows.Forms.PictureBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonImgLoadPre = new System.Windows.Forms.Button();
            this.comm = new System.IO.Ports.SerialPort(this.components);
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.buttonfilter = new System.Windows.Forms.Button();
            this.buttonDrawDeal = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxFlowWidth = new System.Windows.Forms.TextBox();
            this.textBoxFlowHeight = new System.Windows.Forms.TextBox();
            this.textBoxXNum = new System.Windows.Forms.TextBox();
            this.textBoxYNum = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxValueThreshold = new System.Windows.Forms.TextBox();
            this.labelFlow_y = new System.Windows.Forms.Label();
            this.labelFlow_x = new System.Windows.Forms.Label();
            this.checkBoxDefault = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreImg)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarImgEnlarge)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCurImg)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxPreImg
            // 
            this.pictureBoxPreImg.BackColor = System.Drawing.Color.White;
            this.pictureBoxPreImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPreImg.Location = new System.Drawing.Point(6, 6);
            this.pictureBoxPreImg.Name = "pictureBoxPreImg";
            this.pictureBoxPreImg.Size = new System.Drawing.Size(696, 369);
            this.pictureBoxPreImg.TabIndex = 0;
            this.pictureBoxPreImg.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LabelGetCount);
            this.groupBox1.Controls.Add(this.comboBoxBaudrate);
            this.groupBox1.Controls.Add(this.RichTextBoxRE);
            this.groupBox1.Controls.Add(this.comboBoxPortName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonOpenClose);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 238);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "COM设置";
            // 
            // LabelGetCount
            // 
            this.LabelGetCount.AutoSize = true;
            this.LabelGetCount.Location = new System.Drawing.Point(74, 71);
            this.LabelGetCount.Name = "LabelGetCount";
            this.LabelGetCount.Size = new System.Drawing.Size(35, 12);
            this.LabelGetCount.TabIndex = 4;
            this.LabelGetCount.Text = "Get:0";
            // 
            // comboBoxBaudrate
            // 
            this.comboBoxBaudrate.FormattingEnabled = true;
            this.comboBoxBaudrate.Location = new System.Drawing.Point(41, 42);
            this.comboBoxBaudrate.Name = "comboBoxBaudrate";
            this.comboBoxBaudrate.Size = new System.Drawing.Size(82, 20);
            this.comboBoxBaudrate.TabIndex = 3;
            // 
            // RichTextBoxRE
            // 
            this.RichTextBoxRE.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextBoxRE.Location = new System.Drawing.Point(16, 86);
            this.RichTextBoxRE.Name = "RichTextBoxRE";
            this.RichTextBoxRE.Size = new System.Drawing.Size(178, 145);
            this.RichTextBoxRE.TabIndex = 1;
            this.RichTextBoxRE.Text = "";
            // 
            // comboBoxPortName
            // 
            this.comboBoxPortName.FormattingEnabled = true;
            this.comboBoxPortName.Location = new System.Drawing.Point(41, 16);
            this.comboBoxPortName.Name = "comboBoxPortName";
            this.comboBoxPortName.Size = new System.Drawing.Size(82, 20);
            this.comboBoxPortName.TabIndex = 3;
            this.comboBoxPortName.Click += new System.EventHandler(this.comboBoxPortName_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "BAUD";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "COM";
            // 
            // buttonOpenClose
            // 
            this.buttonOpenClose.Location = new System.Drawing.Point(129, 16);
            this.buttonOpenClose.Name = "buttonOpenClose";
            this.buttonOpenClose.Size = new System.Drawing.Size(60, 46);
            this.buttonOpenClose.TabIndex = 0;
            this.buttonOpenClose.Text = "Open";
            this.buttonOpenClose.UseVisualStyleBackColor = true;
            this.buttonOpenClose.Click += new System.EventHandler(this.buttonOpenClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonImgStartStop);
            this.groupBox2.Controls.Add(this.labelImgHeight);
            this.groupBox2.Controls.Add(this.labelImgWidth);
            this.groupBox2.Controls.Add(this.buttonImgSet);
            this.groupBox2.Controls.Add(this.buttonImgClr);
            this.groupBox2.Controls.Add(this.labelEnlargeTimes);
            this.groupBox2.Controls.Add(this.trackBarImgEnlarge);
            this.groupBox2.Controls.Add(this.labelpixCnt);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.labelImgSize);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 256);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 173);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图像属性";
            // 
            // buttonImgStartStop
            // 
            this.buttonImgStartStop.Location = new System.Drawing.Point(76, 111);
            this.buttonImgStartStop.Name = "buttonImgStartStop";
            this.buttonImgStartStop.Size = new System.Drawing.Size(51, 23);
            this.buttonImgStartStop.TabIndex = 13;
            this.buttonImgStartStop.Text = "开始";
            this.buttonImgStartStop.UseVisualStyleBackColor = true;
            this.buttonImgStartStop.Click += new System.EventHandler(this.buttonImgStartStop_Click);
            // 
            // labelImgHeight
            // 
            this.labelImgHeight.AutoSize = true;
            this.labelImgHeight.Location = new System.Drawing.Point(133, 28);
            this.labelImgHeight.Name = "labelImgHeight";
            this.labelImgHeight.Size = new System.Drawing.Size(11, 12);
            this.labelImgHeight.TabIndex = 12;
            this.labelImgHeight.Text = "0";
            // 
            // labelImgWidth
            // 
            this.labelImgWidth.AutoSize = true;
            this.labelImgWidth.Location = new System.Drawing.Point(53, 28);
            this.labelImgWidth.Name = "labelImgWidth";
            this.labelImgWidth.Size = new System.Drawing.Size(11, 12);
            this.labelImgWidth.TabIndex = 11;
            this.labelImgWidth.Text = "0";
            // 
            // buttonImgSet
            // 
            this.buttonImgSet.Location = new System.Drawing.Point(5, 111);
            this.buttonImgSet.Name = "buttonImgSet";
            this.buttonImgSet.Size = new System.Drawing.Size(59, 23);
            this.buttonImgSet.TabIndex = 10;
            this.buttonImgSet.Text = "设置";
            this.buttonImgSet.UseVisualStyleBackColor = true;
            this.buttonImgSet.Click += new System.EventHandler(this.buttonImgSet_Click);
            // 
            // buttonImgClr
            // 
            this.buttonImgClr.Location = new System.Drawing.Point(5, 140);
            this.buttonImgClr.Name = "buttonImgClr";
            this.buttonImgClr.Size = new System.Drawing.Size(59, 23);
            this.buttonImgClr.TabIndex = 9;
            this.buttonImgClr.Text = "清除";
            this.buttonImgClr.UseVisualStyleBackColor = true;
            this.buttonImgClr.Click += new System.EventHandler(this.buttonImgClr_Click);
            // 
            // labelEnlargeTimes
            // 
            this.labelEnlargeTimes.AutoSize = true;
            this.labelEnlargeTimes.Location = new System.Drawing.Point(141, 151);
            this.labelEnlargeTimes.Name = "labelEnlargeTimes";
            this.labelEnlargeTimes.Size = new System.Drawing.Size(53, 12);
            this.labelEnlargeTimes.TabIndex = 5;
            this.labelEnlargeTimes.Text = "放大：x1";
            // 
            // trackBarImgEnlarge
            // 
            this.trackBarImgEnlarge.Location = new System.Drawing.Point(149, 51);
            this.trackBarImgEnlarge.Minimum = 1;
            this.trackBarImgEnlarge.Name = "trackBarImgEnlarge";
            this.trackBarImgEnlarge.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarImgEnlarge.Size = new System.Drawing.Size(45, 104);
            this.trackBarImgEnlarge.TabIndex = 5;
            this.trackBarImgEnlarge.Value = 1;
            this.trackBarImgEnlarge.Scroll += new System.EventHandler(this.trackBarImgEnlarge_Scroll);
            // 
            // labelpixCnt
            // 
            this.labelpixCnt.AutoSize = true;
            this.labelpixCnt.Location = new System.Drawing.Point(74, 82);
            this.labelpixCnt.Name = "labelpixCnt";
            this.labelpixCnt.Size = new System.Drawing.Size(11, 12);
            this.labelpixCnt.TabIndex = 6;
            this.labelpixCnt.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "像素点数：";
            // 
            // labelImgSize
            // 
            this.labelImgSize.AutoSize = true;
            this.labelImgSize.Location = new System.Drawing.Point(74, 58);
            this.labelImgSize.Name = "labelImgSize";
            this.labelImgSize.Size = new System.Drawing.Size(11, 12);
            this.labelImgSize.TabIndex = 5;
            this.labelImgSize.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "图像大小：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(86, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "行数：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "列数：";
            // 
            // buttonImgDeal
            // 
            this.buttonImgDeal.Location = new System.Drawing.Point(948, 367);
            this.buttonImgDeal.Name = "buttonImgDeal";
            this.buttonImgDeal.Size = new System.Drawing.Size(75, 23);
            this.buttonImgDeal.TabIndex = 14;
            this.buttonImgDeal.Text = "处理";
            this.buttonImgDeal.UseVisualStyleBackColor = true;
            this.buttonImgDeal.Click += new System.EventHandler(this.buttonImgDeal_Click);
            // 
            // buttonImgSave
            // 
            this.buttonImgSave.Location = new System.Drawing.Point(558, 333);
            this.buttonImgSave.Name = "buttonImgSave";
            this.buttonImgSave.Size = new System.Drawing.Size(51, 23);
            this.buttonImgSave.TabIndex = 14;
            this.buttonImgSave.Text = "保存";
            this.buttonImgSave.UseVisualStyleBackColor = true;
            this.buttonImgSave.Click += new System.EventHandler(this.buttonImgSave_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(230, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(716, 407);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonImgLoad);
            this.tabPage2.Controls.Add(this.buttonImgSave);
            this.tabPage2.Controls.Add(this.pictureBoxCurImg);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(708, 381);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "curImage";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonImgLoad
            // 
            this.buttonImgLoad.Location = new System.Drawing.Point(615, 333);
            this.buttonImgLoad.Name = "buttonImgLoad";
            this.buttonImgLoad.Size = new System.Drawing.Size(58, 23);
            this.buttonImgLoad.TabIndex = 15;
            this.buttonImgLoad.Text = "读取";
            this.buttonImgLoad.UseVisualStyleBackColor = true;
            this.buttonImgLoad.Click += new System.EventHandler(this.buttonImgLoad_Click);
            // 
            // pictureBoxCurImg
            // 
            this.pictureBoxCurImg.BackColor = System.Drawing.Color.White;
            this.pictureBoxCurImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxCurImg.Location = new System.Drawing.Point(6, 6);
            this.pictureBoxCurImg.Name = "pictureBoxCurImg";
            this.pictureBoxCurImg.Size = new System.Drawing.Size(696, 369);
            this.pictureBoxCurImg.TabIndex = 1;
            this.pictureBoxCurImg.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonImgLoadPre);
            this.tabPage1.Controls.Add(this.pictureBoxPreImg);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(708, 381);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "preImage";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonImgLoadPre
            // 
            this.buttonImgLoadPre.Location = new System.Drawing.Point(600, 333);
            this.buttonImgLoadPre.Name = "buttonImgLoadPre";
            this.buttonImgLoadPre.Size = new System.Drawing.Size(75, 23);
            this.buttonImgLoadPre.TabIndex = 16;
            this.buttonImgLoadPre.Text = "读取";
            this.buttonImgLoadPre.UseVisualStyleBackColor = true;
            this.buttonImgLoadPre.Click += new System.EventHandler(this.buttonImgLoadPre_Click);
            // 
            // comm
            // 
            this.comm.ReadBufferSize = 40960;
            this.comm.WriteBufferSize = 4096;
            this.comm.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.comm_DataReceived);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // buttonfilter
            // 
            this.buttonfilter.Location = new System.Drawing.Point(948, 338);
            this.buttonfilter.Name = "buttonfilter";
            this.buttonfilter.Size = new System.Drawing.Size(75, 23);
            this.buttonfilter.TabIndex = 7;
            this.buttonfilter.Text = "中值滤波";
            this.buttonfilter.UseVisualStyleBackColor = true;
            this.buttonfilter.Click += new System.EventHandler(this.buttonfilter_Click);
            // 
            // buttonDrawDeal
            // 
            this.buttonDrawDeal.Location = new System.Drawing.Point(948, 396);
            this.buttonDrawDeal.Name = "buttonDrawDeal";
            this.buttonDrawDeal.Size = new System.Drawing.Size(75, 23);
            this.buttonDrawDeal.TabIndex = 15;
            this.buttonDrawDeal.Text = "显示处理点";
            this.buttonDrawDeal.UseVisualStyleBackColor = true;
            this.buttonDrawDeal.Click += new System.EventHandler(this.buttonDrawDeal_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(958, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "flow_x:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(958, 298);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "flow_y:";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(954, 116);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "flowWidth:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(948, 146);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "flowHeight:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(982, 171);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 12);
            this.label11.TabIndex = 22;
            this.label11.Text = "XNum:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(982, 196);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 12);
            this.label12.TabIndex = 23;
            this.label12.Text = "YNum:";
            // 
            // textBoxFlowWidth
            // 
            this.textBoxFlowWidth.Location = new System.Drawing.Point(1019, 113);
            this.textBoxFlowWidth.Name = "textBoxFlowWidth";
            this.textBoxFlowWidth.Size = new System.Drawing.Size(32, 21);
            this.textBoxFlowWidth.TabIndex = 24;
            this.textBoxFlowWidth.Text = "140";
            // 
            // textBoxFlowHeight
            // 
            this.textBoxFlowHeight.Location = new System.Drawing.Point(1019, 143);
            this.textBoxFlowHeight.Name = "textBoxFlowHeight";
            this.textBoxFlowHeight.Size = new System.Drawing.Size(32, 21);
            this.textBoxFlowHeight.TabIndex = 25;
            this.textBoxFlowHeight.Text = "70";
            // 
            // textBoxXNum
            // 
            this.textBoxXNum.Location = new System.Drawing.Point(1019, 168);
            this.textBoxXNum.Name = "textBoxXNum";
            this.textBoxXNum.Size = new System.Drawing.Size(32, 21);
            this.textBoxXNum.TabIndex = 26;
            this.textBoxXNum.Text = "5";
            // 
            // textBoxYNum
            // 
            this.textBoxYNum.Location = new System.Drawing.Point(1019, 193);
            this.textBoxYNum.Name = "textBoxYNum";
            this.textBoxYNum.Size = new System.Drawing.Size(32, 21);
            this.textBoxYNum.TabIndex = 27;
            this.textBoxYNum.Text = "5";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(953, 230);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 28;
            this.label13.Text = "threshold:";
            // 
            // textBoxValueThreshold
            // 
            this.textBoxValueThreshold.Location = new System.Drawing.Point(1019, 227);
            this.textBoxValueThreshold.Name = "textBoxValueThreshold";
            this.textBoxValueThreshold.Size = new System.Drawing.Size(32, 21);
            this.textBoxValueThreshold.TabIndex = 29;
            this.textBoxValueThreshold.Text = "60";
            // 
            // labelFlow_y
            // 
            this.labelFlow_y.AutoSize = true;
            this.labelFlow_y.Location = new System.Drawing.Point(1008, 298);
            this.labelFlow_y.Name = "labelFlow_y";
            this.labelFlow_y.Size = new System.Drawing.Size(11, 12);
            this.labelFlow_y.TabIndex = 19;
            this.labelFlow_y.Text = "0";
            // 
            // labelFlow_x
            // 
            this.labelFlow_x.AutoSize = true;
            this.labelFlow_x.Location = new System.Drawing.Point(1008, 270);
            this.labelFlow_x.Name = "labelFlow_x";
            this.labelFlow_x.Size = new System.Drawing.Size(11, 12);
            this.labelFlow_x.TabIndex = 18;
            this.labelFlow_x.Text = "0";
            // 
            // checkBoxDefault
            // 
            this.checkBoxDefault.AutoSize = true;
            this.checkBoxDefault.Location = new System.Drawing.Point(960, 82);
            this.checkBoxDefault.Name = "checkBoxDefault";
            this.checkBoxDefault.Size = new System.Drawing.Size(72, 16);
            this.checkBoxDefault.TabIndex = 31;
            this.checkBoxDefault.Text = "默认设置";
            this.checkBoxDefault.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 20F);
            this.label14.Location = new System.Drawing.Point(955, 28);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 27);
            this.label14.TabIndex = 32;
            this.label14.Text = "Hnu-FQ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 437);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.checkBoxDefault);
            this.Controls.Add(this.textBoxValueThreshold);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBoxYNum);
            this.Controls.Add(this.textBoxXNum);
            this.Controls.Add(this.textBoxFlowHeight);
            this.Controls.Add(this.textBoxFlowWidth);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.labelFlow_y);
            this.Controls.Add(this.labelFlow_x);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonDrawDeal);
            this.Controls.Add(this.buttonImgDeal);
            this.Controls.Add(this.buttonfilter);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreImg)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarImgEnlarge)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCurImg)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxPreImg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label LabelGetCount;
        private System.Windows.Forms.ComboBox comboBoxBaudrate;
        private System.Windows.Forms.RichTextBox RichTextBoxRE;
        private System.Windows.Forms.ComboBox comboBoxPortName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOpenClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelEnlargeTimes;
        private System.Windows.Forms.TrackBar trackBarImgEnlarge;
        private System.Windows.Forms.Label labelpixCnt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelImgSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBoxCurImg;
        private System.Windows.Forms.Button buttonImgClr;
        private System.IO.Ports.SerialPort comm;
        private System.Windows.Forms.Button buttonImgSet;
        private System.Windows.Forms.Label labelImgHeight;
        private System.Windows.Forms.Label labelImgWidth;
        private System.Windows.Forms.Button buttonImgStartStop;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button buttonImgSave;
        private System.Windows.Forms.Button buttonImgLoad;
        private System.Windows.Forms.Button buttonImgDeal;
        private System.Windows.Forms.Button buttonfilter;
        private System.Windows.Forms.Button buttonDrawDeal;
        private System.Windows.Forms.Button buttonImgLoadPre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxFlowWidth;
        private System.Windows.Forms.TextBox textBoxFlowHeight;
        private System.Windows.Forms.TextBox textBoxXNum;
        private System.Windows.Forms.TextBox textBoxYNum;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxValueThreshold;
        private System.Windows.Forms.Label labelFlow_y;
        private System.Windows.Forms.Label labelFlow_x;
        private System.Windows.Forms.CheckBox checkBoxDefault;
        private System.Windows.Forms.Label label14;
    }
}

