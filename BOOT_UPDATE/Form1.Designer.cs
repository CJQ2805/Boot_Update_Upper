
namespace BOOT_UPDATE
{
    partial class CJQ2805
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TabPage tabPage1;
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.UpDate_Btn = new System.Windows.Forms.Button();
            this.ReadFileBtn = new System.Windows.Forms.Button();
            this.lab_disp_up_state = new System.Windows.Forms.Label();
            this.listbox_hex = new System.Windows.Forms.ListBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Voltage_label = new System.Windows.Forms.Label();
            this.sComm = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.OpenBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmPort = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.AboutBtn = new System.Windows.Forms.ToolStripButton();
            this.CommStatus = new System.Windows.Forms.ToolStripStatusLabel();
            tabPage1 = new System.Windows.Forms.TabPage();
            tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            tabPage1.BackColor = System.Drawing.Color.DarkGray;
            tabPage1.Controls.Add(this.progressBar1);
            tabPage1.Controls.Add(this.UpDate_Btn);
            tabPage1.Controls.Add(this.ReadFileBtn);
            tabPage1.Controls.Add(this.lab_disp_up_state);
            tabPage1.Controls.Add(this.listbox_hex);
            tabPage1.Controls.Add(this.tabControl2);
            tabPage1.Cursor = System.Windows.Forms.Cursors.Default;
            tabPage1.Location = new System.Drawing.Point(4, 25);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(1039, 513);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "升级";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(35, 346);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(468, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 5;
            // 
            // UpDate_Btn
            // 
            this.UpDate_Btn.Enabled = false;
            this.UpDate_Btn.Location = new System.Drawing.Point(809, 166);
            this.UpDate_Btn.Name = "UpDate_Btn";
            this.UpDate_Btn.Size = new System.Drawing.Size(163, 43);
            this.UpDate_Btn.TabIndex = 4;
            this.UpDate_Btn.Text = "开始升级";
            this.UpDate_Btn.UseVisualStyleBackColor = true;
            this.UpDate_Btn.Click += new System.EventHandler(this.UpDate_Btn_Click);
            // 
            // ReadFileBtn
            // 
            this.ReadFileBtn.Enabled = false;
            this.ReadFileBtn.Location = new System.Drawing.Point(809, 59);
            this.ReadFileBtn.Name = "ReadFileBtn";
            this.ReadFileBtn.Size = new System.Drawing.Size(163, 43);
            this.ReadFileBtn.TabIndex = 3;
            this.ReadFileBtn.Text = "读取文件(.hex)";
            this.ReadFileBtn.UseVisualStyleBackColor = true;
            this.ReadFileBtn.Click += new System.EventHandler(this.ReadFileBtn_Click);
            // 
            // lab_disp_up_state
            // 
            this.lab_disp_up_state.BackColor = System.Drawing.Color.DarkGray;
            this.lab_disp_up_state.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_disp_up_state.Location = new System.Drawing.Point(31, 385);
            this.lab_disp_up_state.Name = "lab_disp_up_state";
            this.lab_disp_up_state.Size = new System.Drawing.Size(472, 28);
            this.lab_disp_up_state.TabIndex = 2;
            this.lab_disp_up_state.Text = "等待升级";
            this.lab_disp_up_state.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lab_disp_up_state.Click += new System.EventHandler(this.lab_disp_up_state_Click);
            // 
            // listbox_hex
            // 
            this.listbox_hex.Cursor = System.Windows.Forms.Cursors.Default;
            this.listbox_hex.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listbox_hex.FormattingEnabled = true;
            this.listbox_hex.ItemHeight = 15;
            this.listbox_hex.Location = new System.Drawing.Point(6, 7);
            this.listbox_hex.Name = "listbox_hex";
            this.listbox_hex.Size = new System.Drawing.Size(731, 319);
            this.listbox_hex.TabIndex = 1;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(107, 46);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(8, 8);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(0, 0);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(0, 0);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 46);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1047, 542);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.DarkGray;
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.Voltage_label);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1029, 496);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "预留";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(70, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 25);
            this.textBox1.TabIndex = 1;
            // 
            // Voltage_label
            // 
            this.Voltage_label.AutoSize = true;
            this.Voltage_label.Location = new System.Drawing.Point(30, 23);
            this.Voltage_label.Name = "Voltage_label";
            this.Voltage_label.Size = new System.Drawing.Size(37, 15);
            this.Voltage_label.TabIndex = 0;
            this.Voltage_label.Text = "电压";
            this.Voltage_label.Click += new System.EventHandler(this.label1_Click);
            // 
            // sComm
            // 
            this.sComm.BaudRate = 19200;
            this.sComm.ParityReplace = ((byte)(0));
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenBtn,
            this.toolStripSeparator1,
            this.cmPort,
            this.toolStripSeparator2,
            this.toolStripButton1,
            this.toolStripSeparator3,
            this.AboutBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1046, 28);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // OpenBtn
            // 
            this.OpenBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.OpenBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenBtn.Name = "OpenBtn";
            this.OpenBtn.Size = new System.Drawing.Size(73, 25);
            this.OpenBtn.Text = "打开串口";
            this.OpenBtn.ToolTipText = "dakai ";
            this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // cmPort
            // 
            this.cmPort.BackColor = System.Drawing.SystemColors.MenuBar;
            this.cmPort.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.cmPort.Name = "cmPort";
            this.cmPort.Size = new System.Drawing.Size(121, 28);
            this.cmPort.Text = "选择串口";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(73, 25);
            this.toolStripButton1.Text = "扫描串口";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 28);
            // 
            // AboutBtn
            // 
            this.AboutBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AboutBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AboutBtn.Name = "AboutBtn";
            this.AboutBtn.Size = new System.Drawing.Size(43, 25);
            this.AboutBtn.Text = "关于";
            this.AboutBtn.Click += new System.EventHandler(this.AboutBtn_Click);
            // 
            // CommStatus
            // 
            this.CommStatus.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CommStatus.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this.CommStatus.Name = "CommStatus";
            this.CommStatus.Size = new System.Drawing.Size(376, 17);
            this.CommStatus.Text = "打开串口：无";
            // 
            // CJQ2805
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 588);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.IsMdiContainer = true;
            this.Name = "CJQ2805";
            this.Text = "CJQ2805_test";
            this.Load += new System.EventHandler(this.CJQ2805_Load);
            tabPage1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ListBox listbox_hex;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage2;
        private System.IO.Ports.SerialPort sComm;
        private System.Windows.Forms.Button UpDate_Btn;
        private System.Windows.Forms.Button ReadFileBtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton OpenBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox cmPort;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton AboutBtn;
        private System.Windows.Forms.ToolStripStatusLabel CommStatus;
        private System.Windows.Forms.Label lab_disp_up_state;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.Label Voltage_label;
        private System.Windows.Forms.TextBox textBox1;
    }
}

