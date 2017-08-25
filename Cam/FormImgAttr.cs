using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cam
{
    public partial class FormImgAttr : Form
    {
        private int width = 0;
        private int height = 0;

        public FormImgAttr()
        {
            InitializeComponent();
        }

        private void FormImgAttr_Load(object sender, EventArgs e)
        {

        }

        private void buttonImgAttrSave_Click(object sender, EventArgs e)
        {
            try
            {
                width = int.Parse(textBoxImgWidth.Text);
                height = int.Parse(textBoxImgHeight.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();

        }

        private void buttonImgAttrCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int getWidth()
        {
            return width;
        }
        public int getHeight()
        {
            return height;
        }

    }
}
