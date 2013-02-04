namespace PLCDeviceMonitorGUI
{
    partial class MainForm
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
            this.GlobalLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonLayout = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OperationButtonLayout = new System.Windows.Forms.TableLayoutPanel();
            this.StopMonitorButton = new System.Windows.Forms.Button();
            this.StartMonitorButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FileButtonLayout = new System.Windows.Forms.TableLayoutPanel();
            this.LogFileButton = new System.Windows.Forms.Button();
            this.BackupFileButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LogTextBox = new System.Windows.Forms.RichTextBox();
            this.GlobalLayout.SuspendLayout();
            this.ButtonLayout.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.OperationButtonLayout.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.FileButtonLayout.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // GlobalLayout
            // 
            this.GlobalLayout.ColumnCount = 1;
            this.GlobalLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.GlobalLayout.Controls.Add(this.ButtonLayout, 0, 0);
            this.GlobalLayout.Controls.Add(this.groupBox3, 0, 1);
            this.GlobalLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GlobalLayout.Location = new System.Drawing.Point(0, 0);
            this.GlobalLayout.Name = "GlobalLayout";
            this.GlobalLayout.RowCount = 2;
            this.GlobalLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.GlobalLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.GlobalLayout.Size = new System.Drawing.Size(1264, 762);
            this.GlobalLayout.TabIndex = 0;
            // 
            // ButtonLayout
            // 
            this.ButtonLayout.ColumnCount = 2;
            this.ButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.ButtonLayout.Controls.Add(this.groupBox1, 0, 0);
            this.ButtonLayout.Controls.Add(this.groupBox2, 1, 0);
            this.ButtonLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonLayout.Location = new System.Drawing.Point(3, 3);
            this.ButtonLayout.Name = "ButtonLayout";
            this.ButtonLayout.RowCount = 1;
            this.ButtonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ButtonLayout.Size = new System.Drawing.Size(1258, 84);
            this.ButtonLayout.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OperationButtonLayout);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(902, 78);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作";
            // 
            // OperationButtonLayout
            // 
            this.OperationButtonLayout.ColumnCount = 2;
            this.OperationButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.OperationButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.OperationButtonLayout.Controls.Add(this.StopMonitorButton, 1, 0);
            this.OperationButtonLayout.Controls.Add(this.StartMonitorButton, 0, 0);
            this.OperationButtonLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OperationButtonLayout.Location = new System.Drawing.Point(3, 21);
            this.OperationButtonLayout.Name = "OperationButtonLayout";
            this.OperationButtonLayout.RowCount = 1;
            this.OperationButtonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.OperationButtonLayout.Size = new System.Drawing.Size(896, 54);
            this.OperationButtonLayout.TabIndex = 0;
            // 
            // StopMonitorButton
            // 
            this.StopMonitorButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StopMonitorButton.Enabled = false;
            this.StopMonitorButton.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StopMonitorButton.Location = new System.Drawing.Point(451, 3);
            this.StopMonitorButton.Name = "StopMonitorButton";
            this.StopMonitorButton.Size = new System.Drawing.Size(442, 48);
            this.StopMonitorButton.TabIndex = 1;
            this.StopMonitorButton.Text = "停止监控";
            this.StopMonitorButton.UseVisualStyleBackColor = true;
            this.StopMonitorButton.Click += new System.EventHandler(this.StopMonitorButton_Click);
            // 
            // StartMonitorButton
            // 
            this.StartMonitorButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StartMonitorButton.Enabled = false;
            this.StartMonitorButton.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartMonitorButton.Location = new System.Drawing.Point(3, 3);
            this.StartMonitorButton.Name = "StartMonitorButton";
            this.StartMonitorButton.Size = new System.Drawing.Size(442, 48);
            this.StartMonitorButton.TabIndex = 0;
            this.StartMonitorButton.Text = "开始监控";
            this.StartMonitorButton.UseVisualStyleBackColor = true;
            this.StartMonitorButton.Click += new System.EventHandler(this.StartMonitorButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FileButtonLayout);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(911, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(344, 78);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "文件";
            // 
            // FileButtonLayout
            // 
            this.FileButtonLayout.ColumnCount = 2;
            this.FileButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.FileButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.FileButtonLayout.Controls.Add(this.LogFileButton, 0, 0);
            this.FileButtonLayout.Controls.Add(this.BackupFileButton, 0, 0);
            this.FileButtonLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileButtonLayout.Location = new System.Drawing.Point(3, 21);
            this.FileButtonLayout.Name = "FileButtonLayout";
            this.FileButtonLayout.RowCount = 1;
            this.FileButtonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.FileButtonLayout.Size = new System.Drawing.Size(338, 54);
            this.FileButtonLayout.TabIndex = 0;
            // 
            // LogFileButton
            // 
            this.LogFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogFileButton.Enabled = false;
            this.LogFileButton.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LogFileButton.Location = new System.Drawing.Point(172, 3);
            this.LogFileButton.Name = "LogFileButton";
            this.LogFileButton.Size = new System.Drawing.Size(163, 48);
            this.LogFileButton.TabIndex = 2;
            this.LogFileButton.Text = "日志文件";
            this.LogFileButton.UseVisualStyleBackColor = true;
            this.LogFileButton.Click += new System.EventHandler(this.LogFileButton_Click);
            // 
            // BackupFileButton
            // 
            this.BackupFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackupFileButton.Enabled = false;
            this.BackupFileButton.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BackupFileButton.Location = new System.Drawing.Point(3, 3);
            this.BackupFileButton.Name = "BackupFileButton";
            this.BackupFileButton.Size = new System.Drawing.Size(163, 48);
            this.BackupFileButton.TabIndex = 1;
            this.BackupFileButton.Text = "备份文件";
            this.BackupFileButton.UseVisualStyleBackColor = true;
            this.BackupFileButton.Click += new System.EventHandler(this.BackupFileButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Controls.Add(this.LogTextBox);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(3, 93);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1258, 666);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "日志";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::PLCDeviceMonitorGUI.Properties.Resources.logo1;
            this.pictureBox1.Location = new System.Drawing.Point(1074, 584);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 73);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // LogTextBox
            // 
            this.LogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogTextBox.Location = new System.Drawing.Point(3, 21);
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.Size = new System.Drawing.Size(1252, 642);
            this.LogTextBox.TabIndex = 0;
            this.LogTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 762);
            this.Controls.Add(this.GlobalLayout);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1280, 800);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PLC设备监控";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.GlobalLayout.ResumeLayout(false);
            this.ButtonLayout.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.OperationButtonLayout.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.FileButtonLayout.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel GlobalLayout;
        private System.Windows.Forms.TableLayoutPanel ButtonLayout;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel OperationButtonLayout;
        private System.Windows.Forms.Button StopMonitorButton;
        private System.Windows.Forms.Button StartMonitorButton;
        private System.Windows.Forms.TableLayoutPanel FileButtonLayout;
        private System.Windows.Forms.Button LogFileButton;
        private System.Windows.Forms.Button BackupFileButton;
        private System.Windows.Forms.RichTextBox LogTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}

