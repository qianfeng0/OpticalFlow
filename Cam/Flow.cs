using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FlowClass
{
    class Flow
    {
        //private const byte FRAME_SIZE =64;
        
        public byte flowWidth = 120;
        public byte flowHeight = 68;

        private const byte numBlocksX = 9;
        private const byte numBlocksY = 7;

        public float pixFlow_x=0;
        public float pixFlow_y=0;
             
        private const byte SEARCH_SIZE=4;
        private const byte TILE_SIZE = 8;
        private const byte NUM_BLOCKS= 5;
        private const UInt16 PARAM_IMAGE_WIDTH = 140;
        private const UInt32 PARAM_BOTTOM_FLOW_FEATURE_THRESHOLD = 60;
        private const UInt32 PARAM_BOTTOM_FLOW_VALUE_THRESHOLD = 5000;
        private const bool PARAM_BOTTOM_FLOW_HIST_FILTER = true;
        private const bool PARAM_BOTTOM_FLOW_GYRO_COMPENSATION = true;
        private const float PARAM_FOCAL_LENGTH_MM = 16.0f;
        private const float PARAM_GYRO_COMPENSATION_THRESHOLD = 0.01f;

        public Point[] prePix;
        public Point[] curPix;
        public UInt16 meancount;

        public Flow()
        {
            prePix = new Point[numBlocksX * numBlocksY];
            curPix = new Point[numBlocksX * numBlocksY];

        }


        //求a,b连续4字节差的绝对值的累积和，返回累积和
        public static unsafe UInt32 USAD8(byte* a, byte* b)
        {
            UInt32 result = 0;
            result += *a > *b ? (byte)(*a - *b) : (byte)(*b - *a);
            a++; b++;
            result += *a > *b ? (byte)(*a - *b) : (byte)(*b - *a);
            a++; b++;
            result += *a > *b ? (byte)(*a - *b) : (byte)(*b - *a);
            a++; b++;
            result += *a > *b ? (byte)(*a - *b) : (byte)(*b - *a);

            return result;
        }
        //求a,b连续4字节差的绝对值的累积和,返回 累积和+accumulate
        public static unsafe UInt32 USADA8(byte* a, byte* b, UInt32 accumulate)
        {
            accumulate += *a > *b ? (byte)(*a - *b) : (byte)(*b - *a);
            a++; b++;
            accumulate += *a > *b ? (byte)(*a - *b) : (byte)(*b - *a);
            a++; b++;
            accumulate += *a > *b ? (byte)(*a - *b) : (byte)(*b - *a);
            a++; b++;
            accumulate += *a > *b ? (byte)(*a - *b) : (byte)(*b - *a);

            return accumulate;
        }

        public static unsafe void UHADD8(byte* a, byte* b, byte* result)
        {
            *result = (byte)((*a + *b) / 2);
            result++; a++; b++;
            *result = (byte)((*a + *b) / 2);
            result++; a++; b++;
            *result = (byte)((*a + *b) / 2);
            result++; a++; b++;
            *result = (byte)((*a + *b) / 2);
        }

        /**
         * @brief Compute the average pixel gradient of all horizontal and vertical steps
         *
         * TODO compute_diff is not appropriate for low-light mode images
         *
         * @param image ...
         * @param offX x coordinate of upper left corner of 8x8 pattern in image
         * @param offY y coordinate of upper left corner of 8x8 pattern in image
         */
        public static unsafe UInt32 compute_diff(byte* image, UInt16 offX, UInt16 offY, UInt16 row_size)
        {
	        /* calculate position in image buffer */
            UInt16 off = (UInt16)((offY + 2) * row_size + (offX + 2)); // we calc only the 4x4 pattern
            UInt32 acc;

	        /* calc row diff */
	        acc = USAD8 (&image[off + 0 + 0 * row_size], &image[off + 0 + 1 * row_size]);

	        acc = USADA8(&image[off + 0 + 1 * row_size], &image[off + 0 + 2 * row_size], acc);
	        acc = USADA8(&image[off + 0 + 2 * row_size], &image[off + 0 + 3 * row_size], acc);

	        /* we need to get columns */
            /*
            UInt32 col1 = (image[off + 0 + 0 * row_size] << 24) | image[off + 0 + 1 * row_size] << 16 | image[off + 0 + 2 * row_size] << 8 | image[off + 0 + 3 * row_size];
            UInt32 col2 = (image[off + 1 + 0 * row_size] << 24) | image[off + 1 + 1 * row_size] << 16 | image[off + 1 + 2 * row_size] << 8 | image[off + 1 + 3 * row_size];
            UInt32 col3 = (image[off + 2 + 0 * row_size] << 24) | image[off + 2 + 1 * row_size] << 16 | image[off + 2 + 2 * row_size] << 8 | image[off + 2 + 3 * row_size];
            UInt32 col4 = (image[off + 3 + 0 * row_size] << 24) | image[off + 3 + 1 * row_size] << 16 | image[off + 3 + 2 * row_size] << 8 | image[off + 3 + 3 * row_size];
            */
            byte[] col11 = new byte[4] { image[off + 0 + 0 * row_size], image[off + 0 + 1 * row_size], image[off + 0 + 2 * row_size],image[off + 0 + 3 * row_size]};
            byte[] col22 = new byte[4] { image[off + 1 + 0 * row_size], image[off + 1 + 1 * row_size], image[off + 1 + 2 * row_size],image[off + 1 + 3 * row_size]};
            byte[] col33 = new byte[4] { image[off + 2 + 0 * row_size], image[off + 2 + 1 * row_size], image[off + 2 + 2 * row_size],image[off + 2 + 3 * row_size]};
            byte[] col44 = new byte[4] { image[off + 3 + 0 * row_size], image[off + 3 + 1 * row_size], image[off + 3 + 2 * row_size],image[off + 3 + 3 * row_size]};

            fixed (byte* col1 = col11, col2 = col22, col3 = col33, col4 = col44)
            /* calc column diff */
            {
                acc = USADA8(col1, col2, acc);
                acc = USADA8(col2, col3, acc);
                acc = USADA8(col3, col4, acc);
            }
	        return acc;
        }

         /**
         * @brief Compute SAD distances of subpixel shift of two 8x8 pixel patterns.
         *
         * @param image1 ...
         * @param image2 ...
         * @param off1X x coordinate of upper left corner of pattern in image1
         * @param off1Y y coordinate of upper left corner of pattern in image1
         * @param off2X x coordinate of upper left corner of pattern in image2
         * @param off2Y y coordinate of upper left corner of pattern in image2
         * @param acc array to store SAD distances for shift in every direction
         */
        public static unsafe UInt32 compute_subpixel(byte* image1, byte* image2, UInt16 off1X, UInt16 off1Y, UInt16 off2X, UInt16 off2Y, UInt32* acc, UInt16 row_size)
        {
	        /* calculate position in image buffer */
	        UInt16 off1 = (UInt16)(off1Y * row_size + off1X); // image1
            UInt16 off2 = (UInt16)(off2Y * row_size + off2X); // image2

	        //UInt32 s0, s1, s2, s3, s4, s5, s6, s7, t1, t3, t5, t7;
            byte[] s00 = new byte[4];
            byte[] s11 = new byte[4];
            byte[] s22 = new byte[4];
            byte[] s33 = new byte[4];
            byte[] s44 = new byte[4];
            byte[] s55 = new byte[4];
            byte[] s66 = new byte[4];
            byte[] s77 = new byte[4];
            byte[] t11 = new byte[4];
            byte[] t33 = new byte[4];
            byte[] t55 = new byte[4];
            byte[] t77 = new byte[4];

            fixed (byte* s0 = s00, s1 = s11, s2 = s22, s3 = s33, s4 = s44, s5 = s55, s6 = s66, s7 = s77,
                    t1 = t11, t3 = t33, t5 = t55, t7 = t77)
            {
                for (UInt16 i = 0; i < 8; i++)
                {
                    acc[i] = 0;
                }


                /*
                 * calculate for each pixel in the 8x8 field with upper left corner (off1X / off1Y)
                 * every iteration is one line of the 8x8 field.
                 *
                 *  + - + - + - + - + - + - + - + - +
                 *  |   |   |   |   |   |   |   |   |
                 *  + - + - + - + - + - + - + - + - +
                 *
                 *
                 */

                for (UInt16 i = 0; i < 8; i++)
                {
                    /*
                     * first column of 4 pixels:
                     *
                     *  + - + - + - + - + - + - + - + - +
                     *  | x | x | x | x |   |   |   |   |
                     *  + - + - + - + - + - + - + - + - +
                     *
                     * the 8 s values are from following positions for each pixel (X):
                     *  + - + - + - +
                     *  +   5   7   +
                     *  + - + 6 + - +
                     *  +   4 X 0   +
                     *  + - + 2 + - +
                     *  +   3   1   +
                     *  + - + - + - +
                     *
                     *  variables (s1, ...) contains all 4 results (32bit -> 4 * 8bit values)
                     *
                     */

                    /* compute average of two pixel values */
                    /*
                    s0 = (__UHADD8(*((uint32_t*) &image2[off2 +  0 + (i+0) * row_size]), *((uint32_t*) &image2[off2 + 1 + (i+0) * row_size])));
                    s1 = (__UHADD8(*((uint32_t*) &image2[off2 +  0 + (i+1) * row_size]), *((uint32_t*) &image2[off2 + 1 + (i+1) * row_size])));
                    s2 = (__UHADD8(*((uint32_t*) &image2[off2 +  0 + (i+0) * row_size]), *((uint32_t*) &image2[off2 + 0 + (i+1) * row_size])));
                    s3 = (__UHADD8(*((uint32_t*) &image2[off2 +  0 + (i+1) * row_size]), *((uint32_t*) &image2[off2 - 1 + (i+1) * row_size])));
                     * 
                    s4 = (__UHADD8(*((uint32_t*) &image2[off2 +  0 + (i+0) * row_size]), *((uint32_t*) &image2[off2 - 1 + (i+0) * row_size])));
                    s5 = (__UHADD8(*((uint32_t*) &image2[off2 +  0 + (i-1) * row_size]), *((uint32_t*) &image2[off2 - 1 + (i-1) * row_size])));
                    s6 = (__UHADD8(*((uint32_t*) &image2[off2 +  0 + (i+0) * row_size]), *((uint32_t*) &image2[off2 + 0 + (i-1) * row_size])));
                    s7 = (__UHADD8(*((uint32_t*) &image2[off2 +  0 + (i-1) * row_size]), *((uint32_t*) &image2[off2 + 1 + (i-1) * row_size])));
                    */
                    UHADD8(&image2[off2 + 0 + (i + 0) * row_size], &image2[off2 + 1 + (i + 0) * row_size], s0);
                    UHADD8(&image2[off2 + 0 + (i + 1) * row_size], &image2[off2 + 1 + (i + 1) * row_size], s1);
                    UHADD8(&image2[off2 + 0 + (i + 0) * row_size], &image2[off2 + 0 + (i + 1) * row_size], s2);
                    UHADD8(&image2[off2 + 0 + (i + 1) * row_size], &image2[off2 - 1 + (i + 1) * row_size], s3);
                    UHADD8(&image2[off2 + 0 + (i + 0) * row_size], &image2[off2 - 1 + (i + 0) * row_size], s4);
                    UHADD8(&image2[off2 + 0 + (i - 1) * row_size], &image2[off2 - 1 + (i - 1) * row_size], s5);
                    UHADD8(&image2[off2 + 0 + (i + 0) * row_size], &image2[off2 + 0 + (i - 1) * row_size], s6);
                    UHADD8(&image2[off2 + 0 + (i - 1) * row_size], &image2[off2 + 1 + (i - 1) * row_size], s7);

                    /* these 4 t values are from the corners around the center pixel */
                    /*
                    t1 = (__UHADD8(s0, s1));
                    t3 = (__UHADD8(s3, s4));
                    t5 = (__UHADD8(s4, s5));
                    t7 = (__UHADD8(s7, s0));
                    */
                    UHADD8(s0, s1, t1);
                    UHADD8(s3, s4, t3);
                    UHADD8(s4, s5, t5);
                    UHADD8(s7, s0, t7);


                    /*
                     * finally we got all 8 subpixels (s0, t1, s2, t3, s4, t5, s6, t7):
                     *  + - + - + - +
                     *  |   |   |   |
                     *  + - 5 6 7 - +
                     *  |   4 X 0   |
                     *  + - 3 2 1 - +
                     *  |   |   |   |
                     *  + - + - + - +
                     */

                    /* fill accumulation vector */
                    acc[0] = USADA8(&image1[off1 + 0 + i * row_size], s0, acc[0]);
                    acc[1] = USADA8(&image1[off1 + 0 + i * row_size], t1, acc[1]);
                    acc[2] = USADA8(&image1[off1 + 0 + i * row_size], s2, acc[2]);
                    acc[3] = USADA8(&image1[off1 + 0 + i * row_size], t3, acc[3]);
                    acc[4] = USADA8(&image1[off1 + 0 + i * row_size], s4, acc[4]);
                    acc[5] = USADA8(&image1[off1 + 0 + i * row_size], t5, acc[5]);
                    acc[6] = USADA8(&image1[off1 + 0 + i * row_size], s6, acc[6]);
                    acc[7] = USADA8(&image1[off1 + 0 + i * row_size], t7, acc[7]);

                    /*
                     * same for second column of 4 pixels:
                     *
                     *  + - + - + - + - + - + - + - + - +
                     *  |   |   |   |   | x | x | x | x |
                     *  + - + - + - + - + - + - + - + - +
                     *
                     */
                    /*
                    s0 = (__UHADD8(*((uint32_t*)&image2[off2 + 4 + (i + 0) * row_size]), *((uint32_t*)&image2[off2 + 5 + (i + 0) * row_size])));
                    s1 = (__UHADD8(*((uint32_t*)&image2[off2 + 4 + (i + 1) * row_size]), *((uint32_t*)&image2[off2 + 5 + (i + 1) * row_size])));
                    s2 = (__UHADD8(*((uint32_t*)&image2[off2 + 4 + (i + 0) * row_size]), *((uint32_t*)&image2[off2 + 4 + (i + 1) * row_size])));
                    s3 = (__UHADD8(*((uint32_t*)&image2[off2 + 4 + (i + 1) * row_size]), *((uint32_t*)&image2[off2 + 3 + (i + 1) * row_size])));
                    s4 = (__UHADD8(*((uint32_t*)&image2[off2 + 4 + (i + 0) * row_size]), *((uint32_t*)&image2[off2 + 3 + (i + 0) * row_size])));
                    s5 = (__UHADD8(*((uint32_t*)&image2[off2 + 4 + (i - 1) * row_size]), *((uint32_t*)&image2[off2 + 3 + (i - 1) * row_size])));
                    s6 = (__UHADD8(*((uint32_t*)&image2[off2 + 4 + (i + 0) * row_size]), *((uint32_t*)&image2[off2 + 4 + (i - 1) * row_size])));
                    s7 = (__UHADD8(*((uint32_t*)&image2[off2 + 4 + (i - 1) * row_size]), *((uint32_t*)&image2[off2 + 5 + (i - 1) * row_size])));
                    */
                    UHADD8(&image2[off2 + 4 + (i + 0) * row_size], &image2[off2 + 5 + (i + 0) * row_size], s0);
                    UHADD8(&image2[off2 + 4 + (i + 1) * row_size], &image2[off2 + 5 + (i + 1) * row_size], s1);
                    UHADD8(&image2[off2 + 4 + (i + 0) * row_size], &image2[off2 + 4 + (i + 1) * row_size], s2);
                    UHADD8(&image2[off2 + 4 + (i + 1) * row_size], &image2[off2 + 3 + (i + 1) * row_size], s3);
                    UHADD8(&image2[off2 + 4 + (i + 0) * row_size], &image2[off2 + 3 + (i + 0) * row_size], s4);
                    UHADD8(&image2[off2 + 4 + (i - 1) * row_size], &image2[off2 + 3 + (i - 1) * row_size], s5);
                    UHADD8(&image2[off2 + 4 + (i + 0) * row_size], &image2[off2 + 4 + (i - 1) * row_size], s6);
                    UHADD8(&image2[off2 + 4 + (i - 1) * row_size], &image2[off2 + 5 + (i - 1) * row_size], s7);

                    /*
                    t1 = (__UHADD8(s0, s1));
                    t3 = (__UHADD8(s3, s4));
                    t5 = (__UHADD8(s4, s5));
                    t7 = (__UHADD8(s7, s0));
                    */
                    UHADD8(s0, s1, t1);
                    UHADD8(s3, s4, t3);
                    UHADD8(s4, s5, t5);
                    UHADD8(s7, s0, t7);

                    acc[0] = USADA8(&image1[off1 + 4 + i * row_size], s0, acc[0]);
                    acc[1] = USADA8(&image1[off1 + 4 + i * row_size], t1, acc[1]);
                    acc[2] = USADA8(&image1[off1 + 4 + i * row_size], s2, acc[2]);
                    acc[3] = USADA8(&image1[off1 + 4 + i * row_size], t3, acc[3]);
                    acc[4] = USADA8(&image1[off1 + 4 + i * row_size], s4, acc[4]);
                    acc[5] = USADA8(&image1[off1 + 4 + i * row_size], t5, acc[5]);
                    acc[6] = USADA8(&image1[off1 + 4 + i * row_size], s6, acc[6]);
                    acc[7] = USADA8(&image1[off1 + 4 + i * row_size], t7, acc[7]);
                }
            }
	        return 0;
        }

         /**
         * @brief Compute SAD of two 8x8 pixel windows.
         *
         * @param image1 ...
         * @param image2 ...
         * @param off1X x coordinate of upper left corner of pattern in image1
         * @param off1Y y coordinate of upper left corner of pattern in image1
         * @param off2X x coordinate of upper left corner of pattern in image2
         * @param off2Y y coordinate of upper left corner of pattern in image2
         */
        public static unsafe UInt32 compute_sad_8x8(byte* image1, byte* image2, UInt16 off1X, UInt16 off1Y, UInt16 off2X, UInt16 off2Y, UInt16 row_size)
        {
	        /* calculate position in image buffer */
	        UInt16 off1 = (UInt16)(off1Y * row_size + off1X); // image1
	        UInt16 off2 = (UInt16)(off2Y * row_size + off2X); // image2

	        UInt32 acc;
	        acc = USAD8 (&image1[off1 + 0 + 0 * row_size], &image2[off2 + 0 + 0 * row_size]);
	        acc = USADA8(&image1[off1 + 4 + 0 * row_size],&image2[off2 + 4 + 0 * row_size], acc);

	        acc = USADA8(&image1[off1 + 0 + 1 * row_size], &image2[off2 + 0 + 1 * row_size], acc);
	        acc = USADA8(&image1[off1 + 4 + 1 * row_size], &image2[off2 + 4 + 1 * row_size], acc);

	        acc = USADA8(&image1[off1 + 0 + 2 * row_size], &image2[off2 + 0 + 2 * row_size], acc);
	        acc = USADA8(&image1[off1 + 4 + 2 * row_size], &image2[off2 + 4 + 2 * row_size], acc);

	        acc = USADA8(&image1[off1 + 0 + 3 * row_size], &image2[off2 + 0 + 3 * row_size], acc);
	        acc = USADA8(&image1[off1 + 4 + 3 * row_size], &image2[off2 + 4 + 3 * row_size], acc);

	        acc = USADA8(&image1[off1 + 0 + 4 * row_size], &image2[off2 + 0 + 4 * row_size], acc);
	        acc = USADA8(&image1[off1 + 4 + 4 * row_size], &image2[off2 + 4 + 4 * row_size], acc);

	        acc = USADA8(&image1[off1 + 0 + 5 * row_size], &image2[off2 + 0 + 5 * row_size], acc);
	        acc = USADA8(&image1[off1 + 4 + 5 * row_size], &image2[off2 + 4 + 5 * row_size], acc);

	        acc = USADA8(&image1[off1 + 0 + 6 * row_size], &image2[off2 + 0 + 6 * row_size], acc);
	        acc = USADA8(&image1[off1 + 4 + 6 * row_size], &image2[off2 + 4 + 6 * row_size], acc);

	        acc = USADA8(&image1[off1 + 0 + 7 * row_size], &image2[off2 + 0 + 7 * row_size], acc);
	        acc = USADA8(&image1[off1 + 4 + 7 * row_size], &image2[off2 + 4 + 7 * row_size], acc);

	        return acc;
        }

            /**
     * @brief Computes pixel flow from image1 to image2
     *
     * Searches the corresponding position in the new image (image2) of max. 64 pixels from the old image (image1)
     * and calculates the average offset of all.
     *
     * @param image1 previous image buffer
     * @param image2 current image buffer (new)
     * @param x_rate gyro x rate
     * @param y_rate gyro y rate
     * @param z_rate gyro z rate
     *
     * @return quality of flow calculation
     */
        public unsafe byte compute_flow(byte* image1, byte* image2, float x_rate, float y_rate, float z_rate, float* pixel_flow_x, float* pixel_flow_y) 
        {
	        /* constants */
	        const Int16 winmin = -SEARCH_SIZE;		//winmin=-4
            const Int16 winmax = SEARCH_SIZE;			//winmax=4
	        const Int16 hist_size = 2*(winmax-winmin+1)+1;		//hist_size=19

	        /* variables */
            UInt16 pixLo_x = SEARCH_SIZE + 1;				//pixLo=5
            UInt16 pixHi_x = (UInt16)(flowWidth - (SEARCH_SIZE + 1) - TILE_SIZE);	//pixHi = 51
            UInt16 pixStep_x = (UInt16)((pixHi_x - pixLo_x) / numBlocksX + 1);			//10?

            UInt16 pixLo_y = SEARCH_SIZE + 1;
            UInt16 pixHi_y = (UInt16)(flowHeight - (SEARCH_SIZE + 1) - TILE_SIZE);
            UInt16 pixStep_y = (UInt16)((pixHi_y - pixLo_y) / numBlocksY + 1);


            UInt16 i, j;
	        UInt32[] acc1=new UInt32[8]; // subpixels
	        UInt16[] histx=new UInt16[hist_size]; // counter for x shift
	        UInt16[] histy=new UInt16[hist_size]; // counter for y shift
            sbyte[] dirsx = new sbyte[numBlocksX * numBlocksY]; // shift directions in x
            sbyte[] dirsy = new sbyte[numBlocksX * numBlocksY]; // shift directions in y
            //byte[] subdirs = new byte[64]; // shift directions of best subpixels
            byte[] subdirs = new byte[140];
	        float meanflowx = 0.0f;
	        float meanflowy = 0.0f;
            //UInt16 meancount = 0;
            this.meancount = 0;
	        float histflowx = 0.0f;
	        float histflowy = 0.0f;

            fixed (UInt32* acc = acc1)
            {
                /* initialize with 0 */
                for (j = 0; j < hist_size; j++) { histx[j] = 0; histy[j] = 0; }

                /* iterate over all patterns
                 */
                for (j = pixLo_y; j < pixHi_y; j += pixStep_y)
                {
                    for (i = pixLo_x; i < pixHi_x; i += pixStep_x)
                    {
                        /* test pixel if it is suitable for flow tracking */
                        UInt32 diff = compute_diff(image1, i, j, (UInt16)PARAM_IMAGE_WIDTH);
                        if (diff < PARAM_BOTTOM_FLOW_FEATURE_THRESHOLD)
                        {
                            continue;
                        }

                        UInt32 dist = 0xFFFFFFFF; // set initial distance to "infinity"
                        sbyte sumx = 0;
                        sbyte sumy = 0;
                        sbyte ii, jj;

                        byte* base1 = image1 + j * (UInt16)PARAM_IMAGE_WIDTH + i;

                        for (jj = (sbyte)winmin; jj <= winmax; jj++)		//(-4~4)
                        {
                            byte* base2 = image2 + (j + jj) * (UInt16)PARAM_IMAGE_WIDTH + i;

                            for (ii = (sbyte)winmin; ii <= winmax; ii++)
                            {
                                UInt32 temp_dist = compute_sad_8x8(image1, image2, i, j, (UInt16)(i + ii), (UInt16)(j + jj), (UInt16)PARAM_IMAGE_WIDTH);
                                //uint32_t temp_dist = ABSDIFF(base1, base2 + ii);
                                if (temp_dist < dist)
                                {
                                    sumx = ii;
                                    sumy = jj;
                                    dist = temp_dist;
                                }
                            }
                        }

                        prePix[meancount].X = i;
                        prePix[meancount].Y = j;
                        curPix[meancount].X = i + sumx;
                        curPix[meancount].Y = j + sumy;


                        /* acceptance SAD distance threshhold */
                        if (dist < PARAM_BOTTOM_FLOW_VALUE_THRESHOLD)
                        {
                            meanflowx += (float)sumx;
                            meanflowy += (float)sumy;

                            compute_subpixel(image1, image2, i, j, (UInt16)(i + sumx), (UInt16)(j + sumy), acc, (UInt16)PARAM_IMAGE_WIDTH);
                            UInt32 mindist = dist; // best SAD until now
                            byte mindir = 8; // direction 8 for no direction
                            for (byte k = 0; k < 8; k++)
                            {
                                if (acc[k] < mindist)
                                {
                                    // SAD becomes better in direction k
                                    mindist = acc[k];
                                    mindir = k;
                                }
                            }
                            dirsx[meancount] = sumx;
                            dirsy[meancount] = sumy;
                            subdirs[meancount] = mindir;
                            meancount++;

                            /* feed histogram filter*/
                            sbyte hist_index_x = (sbyte)(2 * sumx + (winmax - winmin + 1));
                            if (subdirs[i] == 0 || subdirs[i] == 1 || subdirs[i] == 7) 
                                hist_index_x += 1;
                            if (subdirs[i] == 3 || subdirs[i] == 4 || subdirs[i] == 5) 
                                hist_index_x += -1;
                            sbyte hist_index_y = (sbyte)(2 * sumy + (winmax - winmin + 1));
                            if (subdirs[i] == 5 || subdirs[i] == 6 || subdirs[i] == 7) hist_index_y += -1;
                            if (subdirs[i] == 1 || subdirs[i] == 2 || subdirs[i] == 3) hist_index_y += 1;

                            histx[hist_index_x]++;
                            histy[hist_index_y]++;

                        }
                    }
                }

                /* create flow image if needed (image1 is not needed anymore)
                 * -> can be used for debugging purpose
                 */
                //	if (global_data.param[PARAM_USB_SEND_VIDEO] )//&& global_data.param[PARAM_VIDEO_USB_MODE] == FLOW_VIDEO)
                //	{
                //
                //		for (j = pixLo; j < pixHi; j += pixStep)
                //		{
                //			for (i = pixLo; i < pixHi; i += pixStep)
                //			{
                //
                //				uint32_t diff = compute_diff(image1, i, j, (uint16_t) global_data.param[PARAM_IMAGE_WIDTH]);
                //				if (diff > global_data.param[PARAM_BOTTOM_FLOW_FEATURE_THRESHOLD])
                //				{
                //					image1[j * ((uint16_t) global_data.param[PARAM_IMAGE_WIDTH]) + i] = 255;
                //				}
                //
                //			}
                //		}
                //	}

                /* evaluate flow calculation */
                //if (meancount > 10)
                if(true)
                {
                    meanflowx /= meancount;
                    meanflowy /= meancount;

                    Int16 maxpositionx = 0;
                    Int16 maxpositiony = 0;
                    UInt16 maxvaluex = 0;
                    UInt16 maxvaluey = 0;

                    /* position of maximal histogram peek */
                    for (j = 0; j < hist_size; j++)
                    {
                        if (histx[j] > maxvaluex)
                        {
                            maxvaluex = histx[j];
                            maxpositionx = (Int16)j;
                        }
                        if (histy[j] > maxvaluey)
                        {
                            maxvaluey = histy[j];
                            maxpositiony = (Int16)j;
                        }
                    }

                    /* check if there is a peak value in histogram */
                    if (true) //(histx[maxpositionx] > meancount / 6 && histy[maxpositiony] > meancount / 6)
                    {
                        if (PARAM_BOTTOM_FLOW_HIST_FILTER)
                        {

                            /* use histogram filter peek value */
                            UInt16 hist_x_min = (UInt16)maxpositionx;
                            UInt16 hist_x_max = (UInt16)maxpositionx;
                            UInt16 hist_y_min = (UInt16)maxpositiony;
                            UInt16 hist_y_max = (UInt16)maxpositiony;

                            /* x direction */
                            if (maxpositionx > 1 && maxpositionx < hist_size - 2)
                            {
                                hist_x_min = (UInt16)(maxpositionx - 2);
                                hist_x_max = (UInt16)(maxpositionx + 2);
                            }
                            else if (maxpositionx == 0)
                            {
                                hist_x_min = (UInt16)maxpositionx;
                                hist_x_max = (UInt16)(maxpositionx + 2);
                            }
                            else if (maxpositionx == hist_size - 1)
                            {
                                hist_x_min = (UInt16)(maxpositionx - 2);
                                hist_x_max = (UInt16)(maxpositionx);
                            }
                            else if (maxpositionx == 1)
                            {
                                hist_x_min = (UInt16)(maxpositionx - 1);
                                hist_x_max = (UInt16)(maxpositionx + 2);
                            }
                            else if (maxpositionx == hist_size - 2)
                            {
                                hist_x_min = (UInt16)(maxpositionx - 2);
                                hist_x_max = (UInt16)(maxpositionx + 1);
                            }

                            /* y direction */
                            if (maxpositiony > 1 && maxpositiony < hist_size - 2)
                            {
                                hist_y_min = (UInt16)(maxpositiony - 2);
                                hist_y_max = (UInt16)(maxpositiony + 2);
                            }
                            else if (maxpositiony == 0)
                            {
                                hist_y_min = (UInt16)maxpositiony;
                                hist_y_max = (UInt16)(maxpositiony + 2);
                            }
                            else if (maxpositiony == hist_size - 1)
                            {
                                hist_y_min = (UInt16)(maxpositiony - 2);
                                hist_y_max = (UInt16)(maxpositiony);
                            }
                            else if (maxpositiony == 1)
                            {
                                hist_y_min = (UInt16)(maxpositiony - 1);
                                hist_y_max = (UInt16)(maxpositiony + 2);
                            }
                            else if (maxpositiony == hist_size - 2)
                            {
                                hist_y_min = (UInt16)(maxpositiony - 2);
                                hist_y_max = (UInt16)(maxpositiony + 1);
                            }

                            float hist_x_value = 0.0f;
                            float hist_x_weight = 0.0f;

                            float hist_y_value = 0.0f;
                            float hist_y_weight = 0.0f;

                            for (i = hist_x_min; i < hist_x_max + 1; i++)
                            {
                                hist_x_value += (float)(i * histx[i]);
                                hist_x_weight += (float)histx[i];
                            }

                            for (i = hist_y_min; i < hist_y_max + 1; i++)
                            {
                                hist_y_value += (float)(i * histy[i]);
                                hist_y_weight += (float)histy[i];
                            }

                            histflowx = (hist_x_value / hist_x_weight - (winmax - winmin + 1)) / 2.0f;
                            histflowy = (hist_y_value / hist_y_weight - (winmax - winmin + 1)) / 2.0f;

                        }
                        else
                        {

                            /* use average of accepted flow values */
                            UInt32 meancount_x = 0;
                            UInt32 meancount_y = 0;

                            for (i = 0; i < meancount; i++)
                            {
                                float subdirx = 0.0f;
                                if (subdirs[i] == 0 || subdirs[i] == 1 || subdirs[i] == 7) subdirx = 0.5f;
                                if (subdirs[i] == 3 || subdirs[i] == 4 || subdirs[i] == 5) subdirx = -0.5f;
                                histflowx += (float)dirsx[i] + subdirx;
                                meancount_x++;

                                float subdiry = 0.0f;
                                if (subdirs[i] == 5 || subdirs[i] == 6 || subdirs[i] == 7) subdiry = -0.5f;
                                if (subdirs[i] == 1 || subdirs[i] == 2 || subdirs[i] == 3) subdiry = 0.5f;
                                histflowy += (float)dirsy[i] + subdiry;
                                meancount_y++;
                            }

                            histflowx /= meancount_x;
                            histflowy /= meancount_y;

                        }
#if false
                        /* compensate rotation */
                        /* calculate focal_length in pixel */
                        const float focal_length_px = PARAM_FOCAL_LENGTH_MM / (4.0f * 6.0f) * 1000.0f; //original focal lenght: 12mm pixelsize: 6um, binning 4 enabled

                        /*
                         * gyro compensation
                         * the compensated value is clamped to
                         * the maximum measurable flow value (param BFLOW_MAX_PIX) +0.5
                         * (sub pixel flow can add half pixel to the value)
                         *
                         * -y_rate gives x flow
                         * x_rates gives y_flow
                         */

                        
                        if (PARAM_BOTTOM_FLOW_GYRO_COMPENSATION)
                        {
                            if (fabsf(y_rate) > global_data.param[PARAM_GYRO_COMPENSATION_THRESHOLD])
                            {
                                /* calc pixel of gyro */
                                float y_rate_pixel = y_rate * (get_time_between_images() / 1000000.0f) * focal_length_px;
                                float comp_x = histflowx + y_rate_pixel;

                                /* clamp value to maximum search window size plus half pixel from subpixel search */
                                if (comp_x < (-SEARCH_SIZE - 0.5f))
                                    *pixel_flow_x = (-SEARCH_SIZE - 0.5f);
                                else if (comp_x > (SEARCH_SIZE + 0.5f))
                                    *pixel_flow_x = (SEARCH_SIZE + 0.5f);
                                else
                                    *pixel_flow_x = comp_x;
                            }
                            else
                            {
                                *pixel_flow_x = histflowx;
                            }

                            if (fabsf(x_rate) > global_data.param[PARAM_GYRO_COMPENSATION_THRESHOLD])
                            {
                                /* calc pixel of gyro */
                                float x_rate_pixel = x_rate * (get_time_between_images() / 1000000.0f) * focal_length_px;
                                float comp_y = histflowy - x_rate_pixel;

                                /* clamp value to maximum search window size plus/minus half pixel from subpixel search */
                                if (comp_y < (-SEARCH_SIZE - 0.5f))
                                    *pixel_flow_y = (-SEARCH_SIZE - 0.5f);
                                else if (comp_y > (SEARCH_SIZE + 0.5f))
                                    *pixel_flow_y = (SEARCH_SIZE + 0.5f);
                                else
                                    *pixel_flow_y = comp_y;
                            }
                            else
                            {
                                *pixel_flow_y = histflowy;
                            }

                            /* alternative compensation */
                            //				/* compensate y rotation */
                            //				*pixel_flow_x = histflowx + y_rate_pixel;
                            //
                            //				/* compensate x rotation */
                            //				*pixel_flow_y = histflowy - x_rate_pixel;

                        }
                        else
                        {
                            /* without gyro compensation */
                            *pixel_flow_x = histflowx;
                            *pixel_flow_y = histflowy;
                        }
#endif

#if true
                        *pixel_flow_x = histflowx;
                        *pixel_flow_y = histflowy;

                        this.pixFlow_x = histflowx;
                        this.pixFlow_y = histflowy;
#endif
                    }
                    else
                    {
                        *pixel_flow_x = 0.0f;
                        *pixel_flow_y = 0.0f;
                        return 0;
                    }
                }
                else
                {
                    *pixel_flow_x = 0.0f;
                    *pixel_flow_y = 0.0f;
                    return 0;
                }

                /* calc quality */
                byte qual = (byte)(meancount * 255 / (NUM_BLOCKS * NUM_BLOCKS));
                return qual;
            }
        }
    


    }

}
