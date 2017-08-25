using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO.Ports;

using GrayBitmapClass;
using FlowClass;



namespace Cam
{
    public partial class Form1 : Form
    {

        //************串口通讯定义**************************************************************************
        //private StringBuilder builder = new StringBuilder();//避免在事件处理方法中反复的创建，定义到外面。
        private long received_count = 0;//接收计数
        private bool Listening = false;//是否没有执行完invoke相关操作
        private bool commClosing = false;//是否正在关闭串口，执行Application.DoEvents，并阻止再次invoke
        private List<byte> Buffer = new List<byte>(40960);//默认分配1页内存，并始终限制不允许超过
        private byte[] InByte = new byte[100];

        //************接收图片定义************************************************************************
        private bool imgRecving = false;
        private byte[] imgBuf; //= new byte[20 * 1024];
        private int imgWidth = 0;
        private int imgHeight = 0;
        private GrayBitmap preImage;
        private GrayBitmap curImage;
        
        //****************委托定义************************************************************************
        private delegate void del_updataRichText(RichTextBox richTextBox, string str);
        private delegate void del_updataTextBox(TextBox textbox,string str);
        private delegate void del_updataLable(Label label, string str);
        private delegate void del_updataImg();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            backgroundWorker.RunWorkerAsync();
            
            byte[] bytes = new byte[140 * 80];

                int k = 0;
                byte val = 0;

                for (int i = 0; i < 140 * 80; i++)
                {
                    bytes[k++] = val++;
                }

                curImage = new GrayBitmap(bytes, 140, 80);

                //pictureBoxCurImg.Image = curImage.enlargeBmp(3);
            
            //grayBmp.bmp.Save("test.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        private void updataImg()
        {
            if (curImage != null)
            {
                preImage = curImage;
                pictureBoxPreImg.Image = preImage.enlargeBmp(1);
            }
            curImage = new GrayBitmap(imgBuf, imgWidth, imgHeight);
            
            pictureBoxCurImg.Image = curImage.enlargeBmp(1);

        }

        private void updataRichText(RichTextBox richTextBox, string str)
        {
            richTextBox.AppendText(str);
        }

        private void updataTextBox(TextBox textBox, string str)
        {
            textBox.Text = str;
        }
        private void updataLable(Label label, string str)
        {
            label.Text = str;
        }

        //Combo初始化
        private void Combo_Init()                                       
        {
            comboBoxBaudrate.Items.Add("9600");
            comboBoxBaudrate.Items.Add("14400");
            comboBoxBaudrate.Items.Add("19200");
            comboBoxBaudrate.Items.Add("38400");
            comboBoxBaudrate.Items.Add("56000");
            comboBoxBaudrate.Items.Add("115200");
            comboBoxBaudrate.Items.Add("265000");
        }

        //Comm初始化
        private void SerialPort_Init()                                        
        {
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            comboBoxPortName.Items.AddRange(ports);
            //ComboPortName.SelectedIndex = ComboPortName.Items.Count > 0 ? 0 : -1;
            //ComboBaudrate.SelectedIndex = ComboBaudrate.Items.IndexOf("19200");
            //初始化SerialPort对象
            comm.NewLine = "/r/n";
            comm.RtsEnable = true;//根据实际情况吧。
            //添加事件注册
            comm.DataReceived += comm_DataReceived;
        }
        
        //串口接收事件
        int cnt = 0;
        void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        { 
            if (commClosing) return;//如果正在关闭，忽略操作，直接返回，尽快的完成串口监听线程的一次循环
            try
            {
                Listening = true;//设置标记，说明我已经开始处理数据，一会儿要使用系统UI的。
                int n = comm.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                cnt += n;
                if (n != 0)
                {
                    byte[] Buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                    received_count += n;//增加接收计数
                    comm.Read(Buf, 0, n);//读取缓冲数据
                    Buffer.AddRange(Buf);
                }
                BeginInvoke(new del_updataLable(updataLable), LabelGetCount, "Get:" + cnt.ToString());
            }
            finally
            {
                Listening = false;//我用完了，ui可以关闭串口了。
            }
        
        }

        




        int imgRecvState = 0;
        int pixSize = 0;
        int pImg = 0;
        int imgCnt = 0;

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string str = "";
            while (true)
            {
                bool Data_Catched = false;//缓存记录数据是否捕获到


                if (imgRecving == true)
                { 
                    while (Buffer.Count > 0)
                    {
                        switch (imgRecvState)
                        {
                            case 0:
                                if (Buffer[0] != 0xff)
                                {
                                    Buffer.RemoveAt(0);
                                }
                                else
                                {
                                    imgBuf = new byte[imgWidth * imgHeight];
                                    Buffer.RemoveAt(0);
                                    imgRecvState = 1;
                                    pixSize = imgWidth * imgHeight;
                                    pImg = 0;
                                }
                                break;
                            case 1:
                                int len = Buffer.Count;
                                if (pImg + len <= pixSize)
                                {
                                    Buffer.CopyTo(0, imgBuf, pImg, len);
                                    Buffer.RemoveRange(0, len);
                                    pImg += len;
                                    BeginInvoke(new del_updataLable(updataLable), labelpixCnt, pImg.ToString());
                                }
                                else
                                {
                                    Buffer.CopyTo(0, imgBuf, pImg, pixSize - pImg);
                                    pImg += pixSize - pImg;
                                }

                                if (pImg == pixSize)
                                {
                                    
                                    //curImage = new GrayBitmap(imgBuf, imgWidth, imgHeight);
                                    //BeginInvoke(new del_updataImg(updataImg));
                                    Invoke(new del_updataImg(updataImg));
                                    imgRecvState = 0;
                                }
                                break;
                        }
                    }
        
                }
                else
                {
                    //Invoke(UpData_Text, RichTextBoxRE, str);
                    while (Buffer.Count >= 3)//至少要包含头（1字节）+长度（1字节）+校验（1字节）
                    {
                        if (false)
                        //if (CheckReCmd())
                        {
                            //探测缓存数据是否有一条数据的字节，如果不够，就不用费劲的做其他验证了
                            //前面已经限定了剩余长度>=3，那我们这里一定能访问到buffer[2]这个长度
                            int len = Buffer[1];//数据长度
                            //数据完整判断第一步，长度是否足够
                            //len是数据段长度,4个字节是while行注释的3部分长度
                            if (Buffer.Count < len + 3) break;//数据不够的时候什么都不做
                            //这里确保数据长度足够，数据头标志找到，我们开始计算校验
                            //2.3 校验数据，确认数据正确
                            //异或校验，逐个字节异或得到校验码
                            byte checksum = 0;
                            for (int i = 0; i < len + 2; i++)//len+2表示校验之前的位置
                            {
                                checksum ^= Buffer[i];
                            }
                            if (checksum != Buffer[len + 2]) //如果数据校验失败，丢弃这一包数据
                            {
                                for (int i = 0; i < len + 3; i++)
                                {
                                    str += Buffer[i].ToString("X2") + " ";
                                    BeginInvoke(new del_updataRichText(updataRichText), RichTextBoxRE, str);
                                }
                                Buffer.RemoveRange(0, len + 3);//从缓存中删除错误数据
                                continue;//继续下一次循环
                            }
                            //至此，已经被找到了一条完整数据。我们将数据直接分析，或是缓存起来一起分析
                            //我们这里采用的办法是缓存一次，好处就是如果你某种原因，数据堆积在缓存buffer中
                            //已经很多了，那你需要循环的找到最后一组，只分析最新数据，过往数据你已经处理不及时
                            //了，就不要浪费更多时间了，这也是考虑到系统负载能够降低。
                            Buffer.CopyTo(0, InByte, 0, len + 3);//复制一条完整数据到具体的数据缓存
                            Data_Catched = true;
                            Buffer.RemoveRange(0, len + 3);//正确分析一条数据，从缓存中移除数据。
                            break;

                        }
                        else
                        {
                            //这里是很重要的，如果数据开始不是头，则删除数据
                            str = Buffer[0].ToString("X2") + " ";
                            BeginInvoke(new del_updataRichText(updataRichText), RichTextBoxRE, str);
                            Buffer.RemoveAt(0);
                        }
                    }

                    if (Data_Catched)
                    {
                        //更新界面
                        string data = "";
                        for (int i = 0; i < InByte[1] + 3; i++)
                        {
                            data += InByte[i].ToString("X2") + " ";
                        }
                        data += "\n";
                        BeginInvoke(new del_updataRichText(updataRichText), RichTextBoxRE, data);
                        BeginInvoke(new del_updataLable(updataLable), LabelGetCount, "Get:" + received_count.ToString());
                        //RichTextBoxRE.SelectionStart = RichTextBoxRE.Text.Length;
                        //RichTextBoxRE.ScrollToCaret();
                        //CmdReceive();
                    }
                }
            }
        }
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }



        /************************************
         *界面操作事件
         **************************** */


        //打开/关闭串口按钮
        private void buttonOpenClose_Click(object sender, EventArgs e)
        {
            if (comm.IsOpen)
            {
                commClosing = true;
                while (Listening) Application.DoEvents();
                //打开时点击，则关闭串口  
                comm.Close();
                commClosing = false;
            }
            else
            {
                //关闭时点击，则设置好端口，波特率后打开  
                comm.PortName = comboBoxPortName.Text;
                comm.BaudRate = int.Parse(comboBoxBaudrate.Text);
                try
                {
                    comm.Open();
                }
                catch (Exception ex)
                {
                    //捕获到异常信息，创建一个新的comm对象，之前的不能用了。  
                    comm = new SerialPort();
                    //现实异常信息给客户。  
                    MessageBox.Show(ex.Message);
                }
            }
            //设置按钮的状态  
            buttonOpenClose.Text = comm.IsOpen ? "Close" : "Open";
            //buttonSend.Enabled = comm.IsOpen;  
        }

        //图像放大滑动条
        private void trackBarImgEnlarge_Scroll(object sender, EventArgs e)
        {
            if (curImage != null)
            {
                pictureBoxCurImg.Image = curImage.enlargeBmp(trackBarImgEnlarge.Value);
            }
            if (preImage != null)
            {
                pictureBoxPreImg.Image = preImage.enlargeBmp(trackBarImgEnlarge.Value);
            }

            labelEnlargeTimes.Text = "放大：x" + trackBarImgEnlarge.Value.ToString();
        }

        //图像大小设置按钮
        private void buttonImgSet_Click(object sender, EventArgs e)
        {
            /*
            Form f2 = new Form();
            this.Visible = false;
            f2.ShowDialog();
            * */
            FormImgAttr f2 = new FormImgAttr();
            f2.StartPosition = FormStartPosition.CenterParent;
            f2.ShowDialog(this);

            imgWidth = f2.getWidth();
            imgHeight = f2.getHeight();
            labelImgWidth.Text = f2.getWidth().ToString();
            labelImgHeight.Text = f2.getHeight().ToString();
            labelImgSize.Text = (f2.getWidth() * f2.getHeight()).ToString();
            //imgBuf = new byte[f2.getWidth() * f2.getHeight()];
            //byte[] a=new byte[0];
        }

        //图像接收开始/停止按钮
        private void buttonImgStartStop_Click(object sender, EventArgs e)
        {
            if (imgRecving == false)
            {
                imgRecving = true;
                buttonImgStartStop.Text = "停止";
            }
            else
            {
                imgRecving = false;
                buttonImgStartStop.Text = "开始";
            }
        }

        //按下串口选择栏更新串口
        private void comboBoxPortName_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            comboBoxPortName.Items.Clear();
            comboBoxPortName.Items.AddRange(ports);
        }

        //图像清除按钮
        private void buttonImgClr_Click(object sender, EventArgs e)
        {
            pictureBoxCurImg.Image = null;
        }

        //图像读取按钮
        private void buttonImgLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog opndlg = new OpenFileDialog();
            opndlg.Filter = "所有文件|*.bmp;*.jpg";
            opndlg.Title = "打开图形文件";
            opndlg.ShowHelp = true;
            if (opndlg.ShowDialog() == DialogResult.OK)
            {
                string curFileName = opndlg.FileName;
                try
                {
                    Bitmap curBitmap = (Bitmap)Image.FromFile(curFileName);
                    curImage = new GrayBitmap(curBitmap);
                    pictureBoxCurImg.Image = curImage.enlargeBmp(1);
                }
                catch(Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }
        //图像保存按钮
        private void buttonImgSave_Click(object sender, EventArgs e)
        {
            //curImage.bmp.Save("test.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            if (curImage.bmp == null)
            {
                return;
            }
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Title = "保存为";
            saveDlg.OverwritePrompt = true;
            saveDlg.Filter = "BMP文件(*.bmp)|*.bmp";
            saveDlg.FileName = System.DateTime.Now.ToString("MM-dd(HH.mm.ss)");// +".bmp";
            saveDlg.ShowHelp = true;
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveDlg.FileName;
                string strFilExtn = fileName.Remove(0, fileName.Length - 3);

                curImage.bmp.Save(fileName + "(cur)" + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                preImage.bmp.Save(fileName + "(pre)" + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);

                /*
                switch (strFilExtn)
                {
                    case "bmp":
                        
                        break;
                    default:
                        break;
                }
                 * */
            }
        }

        private void buttonImgLoadPre_Click(object sender, EventArgs e)
        {
            OpenFileDialog opndlg = new OpenFileDialog();
            opndlg.Filter = "所有文件|*.bmp;*.jpg";
            opndlg.Title = "打开图形文件";
            opndlg.ShowHelp = true;
            if (opndlg.ShowDialog() == DialogResult.OK)
            {
                string curFileName = opndlg.FileName;
                try
                {
                    Bitmap curBitmap = (Bitmap)Image.FromFile(curFileName);
                    preImage = new GrayBitmap(curBitmap);
                    pictureBoxPreImg.Image = preImage.enlargeBmp(1);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        //*******************************************************************************************************



        //进行中值滤波
        private unsafe void buttonfilter_Click(object sender, EventArgs e)
        {
            fixed (byte* curImageArray = curImage.bmpDataArray, preImageArray = preImage.bmpDataArray)
            {
                //curImage.MedianFilter(curImageArray, curImage.bmp.Width, curImage.bmp.Height, 3);
                //preImage.MedianFilter(preImageArray, preImage.bmp.Width, preImage.bmp.Height, 3);
                curImage.MedianFilter(curImageArray, curImage.bmp.Width, curImage.bmp.Height);
                preImage.MedianFilter(preImageArray, preImage.bmp.Width, preImage.bmp.Height);
            }
            pictureBoxCurImg.Image = curImage.enlargeBmp(trackBarImgEnlarge.Value);
            pictureBoxPreImg.Image = preImage.enlargeBmp(trackBarImgEnlarge.Value);
        }

        float flowx=0, flowy=0;
        //int startX = 37;
        //int startY = 7;
        int startX = 0;
        int startY = 0;
        Flow flowtest;
        unsafe private void buttonImgDeal_Click(object sender, EventArgs e)
        {


            //drawArea(widthStart, heightStart, curImage.bmp.Width, curImage.bmp.Height, trackBarImgEnlarge.Value);
            
#if true
            fixed(byte* curImageArray = curImage.bmpDataArray, preImageArray = preImage.bmpDataArray)
            {
                fixed(float* pflowx =&flowx,pflowy=&flowy)
                {
                    flowtest =new Flow();
                    flowtest.compute_flow(preImageArray + startX + (preImage.bmp.Width * startY), curImageArray + startX + (curImage.bmp.Width * startY), 0, 0, 0, pflowx, pflowy);
                    //Flow.compute_flow(preImageArray , curImageArray , 0, 0, 0, pflowx, pflowy);
                }
            }
#endif
        }

        //绘制光流处理点
        void drawFlow(PictureBox Img,int times)
        {
            Graphics g = Img.CreateGraphics();
            for (int i = 0; i < flowtest.meancount; i++)
            {
                Point p1 = new Point((flowtest.curPix[i].X + startX) * times, (flowtest.curPix[i].Y + startY)*times);
                Point p2 = new Point((flowtest.prePix[i].X + startX) * times, (flowtest.prePix[i].Y + startY) * times);
                g.DrawLine(new Pen(Color.Blue, times), p1, p2);
                g.DrawLine(new Pen(Color.Red, times), p2.X, p2.Y, p2.X + 1, p2.Y + 1);
            }
        }

        //绘制处理区域
        void drawArea(PictureBox Img,int startX, int StartY, int width, int height, int times)
        {
            Point p1 = new Point(startX * times, StartY * times);
            Point p2 = new Point((startX + width) * times, StartY * times);
            Point p3 = new Point((startX + width) * times, (StartY + height) * times);
            Point p4 = new Point(startX * times, (StartY + height) * times);
            Graphics g = Img.CreateGraphics();

            g.DrawLine(new Pen(Color.Blue, times), p1, p2);
            g.DrawLine(new Pen(Color.Blue, times), p2, p3);
            g.DrawLine(new Pen(Color.Blue, times), p3, p4);
            g.DrawLine(new Pen(Color.Blue, times), p4, p1);
        }
        //绘制按钮
        private void buttonDrawDeal_Click(object sender, EventArgs e)
        {
            drawFlow(pictureBoxCurImg,trackBarImgEnlarge.Value);
            drawArea(pictureBoxCurImg,startX, startY, flowtest.flowWidth, flowtest.flowHeight, trackBarImgEnlarge.Value);

            drawFlow(pictureBoxPreImg, trackBarImgEnlarge.Value);
            drawArea(pictureBoxPreImg, startX, startY, flowtest.flowWidth, flowtest.flowHeight, trackBarImgEnlarge.Value);

            labelFlow_x.Text = flowtest.pixFlow_x.ToString();
            labelFlow_y.Text = flowtest.pixFlow_y.ToString();

            //pictureBoxCurImg.Image = null;
            //pictureBoxPreImg.Image = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flowtest == null)
                return;

            drawFlow(pictureBoxCurImg, trackBarImgEnlarge.Value);
            drawArea(pictureBoxCurImg, startX, startY, flowtest.flowWidth, flowtest.flowHeight, trackBarImgEnlarge.Value);
            drawFlow(pictureBoxPreImg, trackBarImgEnlarge.Value);
            drawArea(pictureBoxPreImg, startX, startY, flowtest.flowWidth, flowtest.flowHeight, trackBarImgEnlarge.Value);
        }
    /*
        private void pictureBoxPreImg_Paint(object sender, PaintEventArgs e)
        {
            if (flowtest == null)
                return;
            drawFlow(pictureBoxPreImg, trackBarImgEnlarge.Value);
            drawArea(pictureBoxPreImg, startX, startY, 64, 64, trackBarImgEnlarge.Value);
        }

        private void pictureBoxCurImg_Paint(object sender, PaintEventArgs e)
        {
            if (flowtest == null)
                return;
            drawFlow(pictureBoxCurImg, trackBarImgEnlarge.Value);
            drawArea(pictureBoxCurImg, startX, startY, 64, 64, trackBarImgEnlarge.Value);
        }
     * */

    }
}
