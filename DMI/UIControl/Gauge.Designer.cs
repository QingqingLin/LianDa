namespace DMI.UIControl
{
    partial class Gauge
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.B_picBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.B_picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // B_picBox
            // 
            this.B_picBox.BackColor = System.Drawing.Color.Transparent;
            this.B_picBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.B_picBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_picBox.Location = new System.Drawing.Point(0, 0);
            this.B_picBox.Name = "B_picBox";
            this.B_picBox.Size = new System.Drawing.Size(300, 300);
            this.B_picBox.TabIndex = 0;
            this.B_picBox.TabStop = false;
            this.B_picBox.Paint += new System.Windows.Forms.PaintEventHandler(this.B_picBox_Paint);
            // 
            // Gauge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.B_picBox);
            this.Name = "Gauge";
            this.Size = new System.Drawing.Size(300, 300);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.B_AGauge_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.B_picBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox B_picBox;
    }
}
