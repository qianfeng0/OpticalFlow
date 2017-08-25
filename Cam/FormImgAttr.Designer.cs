namespace Cam
{
    partial class FormImgAttr
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonImgAttrSave = new System.Windows.Forms.Button();
            this.buttonImgAttrCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxImgWidth = new System.Windows.Forms.TextBox();
            this.textBoxImgHeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonImgAttrSave
            // 
            this.buttonImgAttrSave.Location = new System.Drawing.Point(57, 98);
            this.buttonImgAttrSave.Name = "buttonImgAttrSave";
            this.buttonImgAttrSave.Size = new System.Drawing.Size(51, 23);
            this.buttonImgAttrSave.TabIndex = 0;
            this.buttonImgAttrSave.Text = "保存";
            this.buttonImgAttrSave.UseVisualStyleBackColor = true;
            this.buttonImgAttrSave.Click += new System.EventHandler(this.buttonImgAttrSave_Click);
            // 
            // buttonImgAttrCancel
            // 
            this.buttonImgAttrCancel.Location = new System.Drawing.Point(131, 100);
            this.buttonImgAttrCancel.Name = "buttonImgAttrCancel";
            this.buttonImgAttrCancel.Size = new System.Drawing.Size(51, 21);
            this.buttonImgAttrCancel.TabIndex = 1;
            this.buttonImgAttrCancel.Text = "取消";
            this.buttonImgAttrCancel.UseVisualStyleBackColor = true;
            this.buttonImgAttrCancel.Click += new System.EventHandler(this.buttonImgAttrCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "图像宽度";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(129, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "图像高度";
            // 
            // textBoxImgWidth
            // 
            this.textBoxImgWidth.Location = new System.Drawing.Point(56, 56);
            this.textBoxImgWidth.Name = "textBoxImgWidth";
            this.textBoxImgWidth.Size = new System.Drawing.Size(52, 21);
            this.textBoxImgWidth.TabIndex = 4;
            // 
            // textBoxImgHeight
            // 
            this.textBoxImgHeight.Location = new System.Drawing.Point(131, 56);
            this.textBoxImgHeight.Name = "textBoxImgHeight";
            this.textBoxImgHeight.Size = new System.Drawing.Size(51, 21);
            this.textBoxImgHeight.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "x";
            // 
            // FormImgAttr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 154);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxImgHeight);
            this.Controls.Add(this.textBoxImgWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonImgAttrCancel);
            this.Controls.Add(this.buttonImgAttrSave);
            this.Name = "FormImgAttr";
            this.Text = "图像属性";
            this.Load += new System.EventHandler(this.FormImgAttr_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonImgAttrSave;
        private System.Windows.Forms.Button buttonImgAttrCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxImgWidth;
        private System.Windows.Forms.TextBox textBoxImgHeight;
        private System.Windows.Forms.Label label3;
    }
}