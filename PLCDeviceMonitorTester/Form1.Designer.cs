namespace PLCDeviceMonitorTester
{
    partial class MainFrom
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DeviceAddr = new System.Windows.Forms.TextBox();
            this.DeviceNum = new System.Windows.Forms.TextBox();
            this.DeviceData = new System.Windows.Forms.TextBox();
            this.ReadButton = new System.Windows.Forms.Button();
            this.WriteButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.LogicStationNum = new System.Windows.Forms.TextBox();
            this.LinkStatus = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ConnButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "软元件地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "软元件数量";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "软元件值";
            // 
            // DeviceAddr
            // 
            this.DeviceAddr.Location = new System.Drawing.Point(109, 74);
            this.DeviceAddr.Name = "DeviceAddr";
            this.DeviceAddr.Size = new System.Drawing.Size(100, 29);
            this.DeviceAddr.TabIndex = 3;
            this.DeviceAddr.Text = "D00000";
            // 
            // DeviceNum
            // 
            this.DeviceNum.Location = new System.Drawing.Point(340, 74);
            this.DeviceNum.Name = "DeviceNum";
            this.DeviceNum.Size = new System.Drawing.Size(100, 29);
            this.DeviceNum.TabIndex = 4;
            this.DeviceNum.Text = "1";
            // 
            // DeviceData
            // 
            this.DeviceData.Location = new System.Drawing.Point(109, 110);
            this.DeviceData.Multiline = true;
            this.DeviceData.Name = "DeviceData";
            this.DeviceData.Size = new System.Drawing.Size(331, 58);
            this.DeviceData.TabIndex = 5;
            // 
            // ReadButton
            // 
            this.ReadButton.Enabled = false;
            this.ReadButton.Location = new System.Drawing.Point(258, 203);
            this.ReadButton.Name = "ReadButton";
            this.ReadButton.Size = new System.Drawing.Size(88, 32);
            this.ReadButton.TabIndex = 6;
            this.ReadButton.Text = "读取";
            this.ReadButton.UseVisualStyleBackColor = true;
            this.ReadButton.Click += new System.EventHandler(this.ReadButton_Click);
            // 
            // WriteButton
            // 
            this.WriteButton.Enabled = false;
            this.WriteButton.Location = new System.Drawing.Point(352, 203);
            this.WriteButton.Name = "WriteButton";
            this.WriteButton.Size = new System.Drawing.Size(88, 32);
            this.WriteButton.TabIndex = 7;
            this.WriteButton.Text = "写入";
            this.WriteButton.UseVisualStyleBackColor = true;
            this.WriteButton.Click += new System.EventHandler(this.WriteButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 21);
            this.label5.TabIndex = 10;
            this.label5.Text = "逻辑站号";
            // 
            // LogicStationNum
            // 
            this.LogicStationNum.Location = new System.Drawing.Point(109, 10);
            this.LogicStationNum.Name = "LogicStationNum";
            this.LogicStationNum.Size = new System.Drawing.Size(44, 29);
            this.LogicStationNum.TabIndex = 11;
            this.LogicStationNum.Text = "1";
            // 
            // LinkStatus
            // 
            this.LinkStatus.Location = new System.Drawing.Point(258, 10);
            this.LinkStatus.Name = "LinkStatus";
            this.LinkStatus.ReadOnly = true;
            this.LinkStatus.Size = new System.Drawing.Size(76, 29);
            this.LinkStatus.TabIndex = 13;
            this.LinkStatus.Text = "断开";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(178, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 21);
            this.label6.TabIndex = 12;
            this.label6.Text = "链路状态";
            // 
            // ConnButton
            // 
            this.ConnButton.Location = new System.Drawing.Point(352, 8);
            this.ConnButton.Name = "ConnButton";
            this.ConnButton.Size = new System.Drawing.Size(88, 34);
            this.ConnButton.TabIndex = 14;
            this.ConnButton.Text = "连接";
            this.ConnButton.UseVisualStyleBackColor = true;
            this.ConnButton.Click += new System.EventHandler(this.ConnButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(430, 21);
            this.label7.TabIndex = 15;
            this.label7.Text = "------------------------------------------------------------";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(430, 21);
            this.label4.TabIndex = 16;
            this.label4.Text = "------------------------------------------------------------";
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 247);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ConnButton);
            this.Controls.Add(this.LinkStatus);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LogicStationNum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.WriteButton);
            this.Controls.Add(this.ReadButton);
            this.Controls.Add(this.DeviceData);
            this.Controls.Add(this.DeviceNum);
            this.Controls.Add(this.DeviceAddr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MainFrom";
            this.Text = "PLC Device Monitor Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DeviceAddr;
        private System.Windows.Forms.TextBox DeviceNum;
        private System.Windows.Forms.TextBox DeviceData;
        private System.Windows.Forms.Button ReadButton;
        private System.Windows.Forms.Button WriteButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox LogicStationNum;
        private System.Windows.Forms.TextBox LinkStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ConnButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
    }
}

