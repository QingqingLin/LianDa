namespace MMI
{
    partial class A2_TargetDistance
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
            this.pnl_distance = new System.Windows.Forms.Panel();
            this.pnl_targetSpeed = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnl_str
            // 
            this.pnl_str.Location = new System.Drawing.Point(2, 39);
            this.pnl_str.Name = "pnl_str";
            this.pnl_str.Size = new System.Drawing.Size(40, 260);
            this.pnl_str.TabIndex = 0;
            this.pnl_str.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_str_Paint);
            // 
            // pnl_ruling
            // 
            this.pnl_ruling.Location = new System.Drawing.Point(44, 38);
            this.pnl_ruling.Name = "pnl_ruling";
            this.pnl_ruling.Size = new System.Drawing.Size(20, 260);
            this.pnl_ruling.TabIndex = 1;
            this.pnl_ruling.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_ruling_Paint);
            // 
            // pnl_distance
            // 
            this.pnl_distance.Location = new System.Drawing.Point(66, 38);
            this.pnl_distance.Name = "pnl_distance";
            this.pnl_distance.Size = new System.Drawing.Size(30, 260);
            this.pnl_distance.TabIndex = 2;
            this.pnl_distance.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_distance_Paint);
            // 
            // pnl_targetSpeed
            // 
            this.pnl_targetSpeed.Location = new System.Drawing.Point(2, 2);
            this.pnl_targetSpeed.Name = "pnl_targetSpeed";
            this.pnl_targetSpeed.Size = new System.Drawing.Size(95, 35);
            this.pnl_targetSpeed.TabIndex = 3;
            this.pnl_targetSpeed.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_targetSpeed_Paint);
            // 
            // A2_TargetDistance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_targetSpeed);
            this.Controls.Add(this.pnl_distance);
            this.Controls.Add(this.pnl_ruling);
            this.Controls.Add(this.pnl_str);
            this.Name = "A2_TargetDistance";
            this.Size = new System.Drawing.Size(100, 300);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_str;
        private System.Windows.Forms.Panel pnl_ruling;
        private System.Windows.Forms.Panel pnl_distance;
        private System.Windows.Forms.Panel pnl_targetSpeed;
    }
}
