namespace TCPApp1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Send = new System.Windows.Forms.Button();
            this.IPtxt = new System.Windows.Forms.TextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.txtPoint = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnSendPic = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(341, 212);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(75, 23);
            this.btn_Send.TabIndex = 0;
            this.btn_Send.Text = "发送";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // IPtxt
            // 
            this.IPtxt.Location = new System.Drawing.Point(13, 12);
            this.IPtxt.Name = "IPtxt";
            this.IPtxt.Size = new System.Drawing.Size(137, 21);
            this.IPtxt.TabIndex = 1;
            this.IPtxt.Text = "192.168.1.3";
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(217, 10);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 2;
            this.btn_Start.Text = "开始监听";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // txtPoint
            // 
            this.txtPoint.Location = new System.Drawing.Point(156, 12);
            this.txtPoint.Name = "txtPoint";
            this.txtPoint.Size = new System.Drawing.Size(55, 21);
            this.txtPoint.TabIndex = 3;
            this.txtPoint.Text = "7788";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(13, 39);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(404, 158);
            this.txtLog.TabIndex = 5;
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(13, 212);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(323, 21);
            this.txtMsg.TabIndex = 6;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(13, 241);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(323, 21);
            this.txtPath.TabIndex = 8;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(341, 241);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 7;
            this.btnSelect.Text = "选择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnSendPic
            // 
            this.btnSendPic.Location = new System.Drawing.Point(422, 241);
            this.btnSendPic.Name = "btnSendPic";
            this.btnSendPic.Size = new System.Drawing.Size(75, 23);
            this.btnSendPic.TabIndex = 9;
            this.btnSendPic.Text = "发送图片";
            this.btnSendPic.UseVisualStyleBackColor = true;
            this.btnSendPic.Click += new System.EventHandler(this.btnSendPic_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 294);
            this.Controls.Add(this.btnSendPic);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtPoint);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.IPtxt);
            this.Controls.Add(this.btn_Send);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.TextBox IPtxt;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.TextBox txtPoint;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnSendPic;
    }
}

