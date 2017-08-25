#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;


namespace GrayBitmapClass
{
    class GrayBitmap
    {
        public byte[] bmpDataArray;
        public Bitmap bmp;


        //public Bitmap
        public GrayBitmap(byte[] rawValues, int width, int height)
        {
            //// 申请目标位图的变量，并将其内存区域锁定
            bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height),
                                            ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            bmpDataArray = rawValues;
            //// 获取图像参数
            int stride = bmpData.Stride;  // 扫描线的宽度
            int offset = stride - width;  // 显示宽度与扫描线宽度的间隙
            IntPtr iptr = bmpData.Scan0;  // 获取bmpData的内存起始位置
            int scanBytes = stride * height;   // 用stride宽度，表示这是内存区域的大小

            //// 下面把原始的显示大小字节数组转换为内存中实际存放的字节数组
            int posScan = 0, posReal = 0;   // 分别设置两个位置指针，指向源数组和目标数组
            byte[] pixelValues = new byte[scanBytes];  //为目标数组分配内存
            for (int x = 0; x < height; x++)
            {
                //// 下面的循环节是模拟行扫描
                for (int y = 0; y < width; y++)
                {
                    pixelValues[posScan++] = rawValues[posReal++];
                }
                posScan += offset;  //行扫描结束，要将目标位置指针移过那段“间隙”
            }

            //// 用Marshal的Copy方法，将刚才得到的内存字节数组复制到BitmapData中
            System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, iptr, scanBytes);
            bmp.UnlockBits(bmpData);  // 解锁内存区域

            //// 下面的代码是为了修改生成位图的索引表，从伪彩修改为灰度
            ColorPalette tempPalette;
            using (Bitmap tempBmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                tempPalette = tempBmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                tempPalette.Entries[i] = Color.FromArgb(i, i, i);
            }

            bmp.Palette = tempPalette;

            //// 算法到此结束，返回结果
        }

        public GrayBitmap(Bitmap bm)
        {
            this.bmp = bm;
            this.bmpToArray();
        }


        public Bitmap GetSmall(Bitmap bm, double times)
        {
            int nowWidth = (int)(bm.Width / times);
            int nowHeight = (int)(bm.Height / times);
            Bitmap newbm = new Bitmap(nowWidth, nowHeight);//新建一个放大后大小的图片

            if (times >= 1 && times <= 1.1)
            {
                newbm = bm;
            }
            else
            {
                Graphics g = Graphics.FromImage(newbm);
                
                g.InterpolationMode = InterpolationMode.Low;
                g.SmoothingMode = SmoothingMode.None;
                g.CompositingQuality = CompositingQuality.Invalid;
                
                g.DrawImage(bm, new Rectangle(0, 0, nowWidth, nowHeight), new Rectangle(0, 0, bm.Width, bm.Height), GraphicsUnit.Pixel);
                g.Dispose();
            }
            return newbm;
        }

        public Bitmap enlargeBmp(int multiple)
        {
            int newWidth = bmp.Width * multiple;
            int newHeight = bmp.Height * multiple;

            byte[] pixelValues = bmpDataArray;
            //从像素字节数组pixelValues中放大图像，放入数组rawValues中
            byte[] rawValues = new byte[newWidth * newHeight];
            int pSrc = 0, pDst = 0;   // 分别设置两个位置指针，指向源数组和目标数组
            //进行行扫描
            for (int h = 0; h < bmp.Height; h++)
            {
                for (int w = 0; w < bmp.Width; w++)
                {
                    //放大
                    for (int hh = 0; hh < multiple; hh++)
                    {
                        for (int ww = 0; ww < multiple; ww++)
                        {
                            rawValues[pDst + ww + hh * newWidth] = pixelValues[pSrc];
                        }
                    }
                    pSrc++;
                    pDst += multiple;   //跳过放大的像素点
                }
                //pSrc += offset;     //行扫描结束，跳过“间隙”
                pDst += (multiple - 1) * newWidth;  //跳过放大的像素行
            }
            //生成灰度图
            GrayBitmap grayBmp = new GrayBitmap(rawValues, newWidth, newHeight);
            return grayBmp.bmp;
        }
        
        
        public bool bmpToArray()
        {
            if (this.bmp == null)
            {
                return false;
            }
            else
            {
                this.bmpDataArray = new byte[bmp.Width * bmp.Height];
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                           ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
                int stride = bmpData.Stride;  // 扫描线的宽度
                int offset = stride - bmp.Width;  // 显示宽度与扫描线宽度的间隙
                IntPtr iptr = bmpData.Scan0;  // 获取bmpData的内存起始位置
                int scanBytes = stride * bmp.Height;   // 用stride宽度，表示这是内存区域的大小
                byte[] pixelValues = new byte[scanBytes];  //为目标数组分配内存
                System.Runtime.InteropServices.Marshal.Copy(iptr, pixelValues, 0, scanBytes);
                bmp.UnlockBits(bmpData);

                int pSrc = 0, pDst = 0;   // 分别设置两个位置指针，指向源数组和目标数组
                for (int x = 0; x < this.bmp.Height; x++)
                {
                    //// 下面的循环节是模拟行扫描
                    for (int y = 0; y < this.bmp.Width; y++)
                    {
                        this.bmpDataArray[pDst++] = pixelValues[pSrc++];
                    }
                    pSrc += offset;  //行扫描结束，要将目标位置指针移过那段“间隙”
                }
                return true;
            }
        }

        public unsafe void MedianFilter(byte* pGrayMat, int nWidth, int nHeight, int nWindows)
        {
            ////////////////////////参数说明///////////////////////////////////  
            //pGrayMat:待处理图像数组  
            //nWidth:图像宽度  
            //nHeight:图像高度  
            //nWindows:滤波窗口大小 
            if ((nWindows % 2) == 0)
            {
                //MessageBox("此函数必须设置邻域为奇数矩阵");
                return;
            }  

            int nNumData = nWindows/2;  
            byte[] nData = new byte[nWindows*nWindows]; //保存邻域中的数据  
            if((nWindows>nHeight) && (nWindows>nWidth))  
            {  
                return;  
            }  

            for(int i=nNumData; i<(nHeight-nNumData); i++)  
            {  
                for(int j=nNumData; j<(nWidth-nNumData); j++)  
                {  
                    int nIndex = 0;  
                    for(int m=-nNumData; m<=nNumData; m++)  
                    {  
                        for(int n=-nNumData; n<=nNumData; n++)  
                        {   
                                //nData[nIndex++] = (unsigned char)cvmGet(pGrayMat, i+m, j+n);  
                            nData[nIndex++] = *(pGrayMat+j+n+(i+m)*nWidth);
                        }     
                    }  
                    //InsertSort(nData, nIndex);      //排序  
                    Array.Sort(nData);
                    *(pGrayMat+j+i*nWidth)=nData[nWindows*nWindows/2];
                }  
            }  

        }

        public unsafe void MedianFilter(byte* pGrayMat, int nWidth, int nHeight)
        {
            int nWindows = 3;
            ////////////////////////参数说明///////////////////////////////////  
            //pGrayMat:待处理图像数组  
            //nWidth:图像宽度  
            //nHeight:图像高度  
            //nWindows:滤波窗口大小 
            if ((nWindows % 2) == 0)
            {
                //MessageBox("此函数必须设置邻域为奇数矩阵");
                return;
            }

            int nNumData = nWindows / 2;
            byte[] nData = new byte[nWindows * nWindows]; //保存邻域中的数据  
            if ((nWindows > nHeight) && (nWindows > nWidth))
            {
                return;
            }

            for (int i = nNumData; i < (nHeight - nNumData); i++)
            {
                for (int j = nNumData; j < (nWidth - nNumData); j++)
                {
                    int nIndex = 0;

                    nData[nIndex++] = *(pGrayMat + j + 0 + (i + 0) * nWidth);
                    nData[nIndex++] = *(pGrayMat + j - 1 + (i + 0) * nWidth);
                    nData[nIndex++] = *(pGrayMat + j + 1 + (i + 0) * nWidth);
                    nData[nIndex++] = *(pGrayMat + j + 0 + (i - 1) * nWidth);
                    nData[nIndex++] = *(pGrayMat + j + 0 + (i + 1) * nWidth);

                    Array.Sort(nData);
                    *(pGrayMat + j + i * nWidth) = nData[2];
                }
            }  
        }

    }
}

