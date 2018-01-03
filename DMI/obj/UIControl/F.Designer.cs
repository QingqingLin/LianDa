namespace MMI.UIControl
{
    partial class F
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
            this.lbl_F = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_F
            // 
            this.lbl_F.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_F.AutoSize = true;
            this.lbl_F.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_F.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_F.Location = new System.Drawing.Point(13, 64);
            this.lbl_F.Name = "lbl_F";
            this.lbl_F.Size = new System.Drawing.Size(197, 31);
            this.lbl_F.TabIndex = 0;
            this.lbl_F.Text = "要求进入RM模式";
            // 
            // F
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_F);
            this.Name = "F";
            this.Size = new System.Drawing.Size(250, 150);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.F_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_F;
    }
}
