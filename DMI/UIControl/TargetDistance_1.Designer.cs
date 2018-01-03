namespace DMI
{
    partial class TargetDistance_1
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
            this.pnl_str = new System.Windows.Forms.Panel();
            this.pnl_ruling = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnl_str
            // 
            this.pnl_str.Location = new System.Drawing.Point(3, 3);
            this.pnl_str.Name = "pnl_str";
            this.pnl_str.Size = new System.Drawing.Size(40, 260);
            this.pnl_str.TabIndex = 0;
            this.pnl_str.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_str_Paint);
            // 
            // pnl_ruling
            // 
            this.pnl_ruling.Location = new System.Drawing.Point(44, 3);
            this.pnl_ruling.Name = "pnl_ruling";
            this.pnl_ruling.Size = new System.Drawing.Size(20, 260);
            this.pnl_ruling.TabIndex = 1;
            this.pnl_ruling.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_ruling_Paint);
            // 
            // TargetDistance_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_ruling);
            this.Controls.Add(this.pnl_str);
            this.DoubleBuffered = true;
            this.Name = "TargetDistance_1";
            this.Size = new System.Drawing.Size(67, 265);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_str;
        private System.Windows.Forms.Panel pnl_ruling;
    }
}
