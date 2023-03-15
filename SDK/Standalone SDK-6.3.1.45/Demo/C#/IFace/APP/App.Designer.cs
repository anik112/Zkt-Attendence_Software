namespace APP
{
    partial class App
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnRsConnect = new System.Windows.Forms.Button();
            this.txtMachineSN = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMachineSN2 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnUSBConnect = new System.Windows.Forms.Button();
            this.lblState = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonGetFunOfRole = new System.Windows.Forms.Button();
            this.buttonGetAppOfRole = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBoxFuncOfRole = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxAPPOfRole = new System.Windows.Forms.ComboBox();
            this.buttonGetAllRole = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxPermissionsname = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxRoleName = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxFuncName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxAPPName = new System.Windows.Forms.ComboBox();
            this.buttonGetAllAppFun = new System.Windows.Forms.Button();
            this.buttonSetPermOfAppFun = new System.Windows.Forms.Button();
            this.buttonDeletePermOfAppFun = new System.Windows.Forms.Button();
            this.buttonIsUserDefRoleEnable = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Controls.Add(this.lblState);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 146);
            this.groupBox2.TabIndex = 75;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Communication with Device";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(6, 20);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(449, 102);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtPort);
            this.tabPage1.Controls.Add(this.txtIP);
            this.tabPage1.Controls.Add(this.btnConnect);
            this.tabPage1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage1.ForeColor = System.Drawing.Color.DarkBlue;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(441, 76);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TCP/IP";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "IP";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(300, 11);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(53, 21);
            this.txtPort.TabIndex = 7;
            this.txtPort.Text = "4370";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(118, 11);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(95, 21);
            this.txtIP.TabIndex = 6;
            this.txtIP.Text = "192.168.51.110";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(181, 42);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(78, 23);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.btnRsConnect);
            this.tabPage2.Controls.Add(this.txtMachineSN);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.cbBaudRate);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.cbPort);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.ForeColor = System.Drawing.Color.DarkBlue;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(441, 76);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "RS232/485";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnRsConnect
            // 
            this.btnRsConnect.Location = new System.Drawing.Point(183, 43);
            this.btnRsConnect.Name = "btnRsConnect";
            this.btnRsConnect.Size = new System.Drawing.Size(75, 23);
            this.btnRsConnect.TabIndex = 11;
            this.btnRsConnect.Text = "Connect";
            this.btnRsConnect.UseVisualStyleBackColor = true;
            this.btnRsConnect.Click += new System.EventHandler(this.btnRsConnect_Click);
            // 
            // txtMachineSN
            // 
            this.txtMachineSN.Location = new System.Drawing.Point(356, 10);
            this.txtMachineSN.Name = "txtMachineSN";
            this.txtMachineSN.Size = new System.Drawing.Size(56, 21);
            this.txtMachineSN.TabIndex = 10;
            this.txtMachineSN.Text = "1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(284, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "MachineSN";
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "115200"});
            this.cbBaudRate.Location = new System.Drawing.Point(206, 10);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(65, 20);
            this.cbBaudRate.TabIndex = 6;
            this.cbBaudRate.Text = "115200";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(140, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "BaudRate";
            // 
            // cbPort
            // 
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9"});
            this.cbPort.Location = new System.Drawing.Point(71, 10);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(56, 20);
            this.cbPort.TabIndex = 5;
            this.cbPort.Text = "COM1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "Port";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.txtMachineSN2);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.btnUSBConnect);
            this.tabPage3.ForeColor = System.Drawing.Color.DarkBlue;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(441, 76);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "USBClient";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(233, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "MachineSN";
            // 
            // txtMachineSN2
            // 
            this.txtMachineSN2.BackColor = System.Drawing.Color.AliceBlue;
            this.txtMachineSN2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMachineSN2.Location = new System.Drawing.Point(294, 11);
            this.txtMachineSN2.Name = "txtMachineSN2";
            this.txtMachineSN2.Size = new System.Drawing.Size(27, 21);
            this.txtMachineSN2.TabIndex = 9;
            this.txtMachineSN2.Text = "1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Crimson;
            this.label14.Location = new System.Drawing.Point(120, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 12);
            this.label14.TabIndex = 8;
            this.label14.Text = "Virtual USBClient";
            // 
            // btnUSBConnect
            // 
            this.btnUSBConnect.Location = new System.Drawing.Point(183, 42);
            this.btnUSBConnect.Name = "btnUSBConnect";
            this.btnUSBConnect.Size = new System.Drawing.Size(75, 23);
            this.btnUSBConnect.TabIndex = 0;
            this.btnUSBConnect.Text = "Connect";
            this.btnUSBConnect.UseVisualStyleBackColor = true;
            this.btnUSBConnect.Click += new System.EventHandler(this.btnUSBConnect_Click);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.ForeColor = System.Drawing.Color.Crimson;
            this.lblState.Location = new System.Drawing.Point(150, 125);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(161, 12);
            this.lblState.TabIndex = 2;
            this.lblState.Text = "Current State:Disconnected";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonIsUserDefRoleEnable);
            this.groupBox1.Controls.Add(this.buttonDeletePermOfAppFun);
            this.groupBox1.Controls.Add(this.buttonSetPermOfAppFun);
            this.groupBox1.Controls.Add(this.buttonGetFunOfRole);
            this.groupBox1.Controls.Add(this.buttonGetAppOfRole);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.comboBoxFuncOfRole);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.comboBoxAPPOfRole);
            this.groupBox1.Controls.Add(this.buttonGetAllRole);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.comboBoxPermissionsname);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.comboBoxRoleName);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.comboBoxFuncName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxAPPName);
            this.groupBox1.Controls.Add(this.buttonGetAllAppFun);
            this.groupBox1.Location = new System.Drawing.Point(13, 179);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(459, 240);
            this.groupBox1.TabIndex = 76;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "App Info";
            // 
            // buttonGetFunOfRole
            // 
            this.buttonGetFunOfRole.Location = new System.Drawing.Point(115, 206);
            this.buttonGetFunOfRole.Name = "buttonGetFunOfRole";
            this.buttonGetFunOfRole.Size = new System.Drawing.Size(85, 25);
            this.buttonGetFunOfRole.TabIndex = 15;
            this.buttonGetFunOfRole.Text = "GetFunOfRole ";
            this.buttonGetFunOfRole.UseVisualStyleBackColor = true;
            this.buttonGetFunOfRole.Click += new System.EventHandler(this.buttonGetFunOfRole_Click);
            // 
            // buttonGetAppOfRole
            // 
            this.buttonGetAppOfRole.Location = new System.Drawing.Point(10, 206);
            this.buttonGetAppOfRole.Name = "buttonGetAppOfRole";
            this.buttonGetAppOfRole.Size = new System.Drawing.Size(85, 25);
            this.buttonGetAppOfRole.TabIndex = 14;
            this.buttonGetAppOfRole.Text = "GetAppOfRole";
            this.buttonGetAppOfRole.UseVisualStyleBackColor = true;
            this.buttonGetAppOfRole.Click += new System.EventHandler(this.buttonGetAppOfRole_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(227, 180);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 13;
            this.label12.Text = "Func Of Role";
            // 
            // comboBoxFuncOfRole
            // 
            this.comboBoxFuncOfRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFuncOfRole.FormattingEnabled = true;
            this.comboBoxFuncOfRole.Location = new System.Drawing.Point(309, 177);
            this.comboBoxFuncOfRole.Name = "comboBoxFuncOfRole";
            this.comboBoxFuncOfRole.Size = new System.Drawing.Size(85, 20);
            this.comboBoxFuncOfRole.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 180);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 12);
            this.label11.TabIndex = 11;
            this.label11.Text = "APP Of Role";
            // 
            // comboBoxAPPOfRole
            // 
            this.comboBoxAPPOfRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAPPOfRole.FormattingEnabled = true;
            this.comboBoxAPPOfRole.Location = new System.Drawing.Point(115, 177);
            this.comboBoxAPPOfRole.Name = "comboBoxAPPOfRole";
            this.comboBoxAPPOfRole.Size = new System.Drawing.Size(85, 20);
            this.comboBoxAPPOfRole.TabIndex = 10;
            // 
            // buttonGetAllRole
            // 
            this.buttonGetAllRole.Location = new System.Drawing.Point(10, 133);
            this.buttonGetAllRole.Name = "buttonGetAllRole";
            this.buttonGetAllRole.Size = new System.Drawing.Size(85, 25);
            this.buttonGetAllRole.TabIndex = 9;
            this.buttonGetAllRole.Text = "GetAllRole";
            this.buttonGetAllRole.UseVisualStyleBackColor = true;
            this.buttonGetAllRole.Click += new System.EventHandler(this.buttonGetAllRole_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 106);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 12);
            this.label10.TabIndex = 8;
            this.label10.Text = "Permissions name";
            // 
            // comboBoxPermissionsname
            // 
            this.comboBoxPermissionsname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPermissionsname.FormattingEnabled = true;
            this.comboBoxPermissionsname.Location = new System.Drawing.Point(115, 103);
            this.comboBoxPermissionsname.Name = "comboBoxPermissionsname";
            this.comboBoxPermissionsname.Size = new System.Drawing.Size(85, 20);
            this.comboBoxPermissionsname.TabIndex = 7;
            this.comboBoxPermissionsname.SelectedIndexChanged += new System.EventHandler(this.comboBoxPermissionsname_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(227, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "Role Name";
            // 
            // comboBoxRoleName
            // 
            this.comboBoxRoleName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRoleName.FormattingEnabled = true;
            this.comboBoxRoleName.Location = new System.Drawing.Point(309, 103);
            this.comboBoxRoleName.Name = "comboBoxRoleName";
            this.comboBoxRoleName.Size = new System.Drawing.Size(85, 20);
            this.comboBoxRoleName.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(227, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "Func Name";
            // 
            // comboBoxFuncName
            // 
            this.comboBoxFuncName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFuncName.FormattingEnabled = true;
            this.comboBoxFuncName.Location = new System.Drawing.Point(309, 35);
            this.comboBoxFuncName.Name = "comboBoxFuncName";
            this.comboBoxFuncName.Size = new System.Drawing.Size(85, 20);
            this.comboBoxFuncName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "APP Name";
            // 
            // comboBoxAPPName
            // 
            this.comboBoxAPPName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAPPName.FormattingEnabled = true;
            this.comboBoxAPPName.Location = new System.Drawing.Point(115, 35);
            this.comboBoxAPPName.Name = "comboBoxAPPName";
            this.comboBoxAPPName.Size = new System.Drawing.Size(85, 20);
            this.comboBoxAPPName.TabIndex = 1;
            this.comboBoxAPPName.SelectedIndexChanged += new System.EventHandler(this.comboBoxAPPName_SelectedIndexChanged);
            // 
            // buttonGetAllAppFun
            // 
            this.buttonGetAllAppFun.Location = new System.Drawing.Point(9, 61);
            this.buttonGetAllAppFun.Name = "buttonGetAllAppFun";
            this.buttonGetAllAppFun.Size = new System.Drawing.Size(85, 25);
            this.buttonGetAllAppFun.TabIndex = 0;
            this.buttonGetAllAppFun.Text = "GetAllAppFun";
            this.buttonGetAllAppFun.UseVisualStyleBackColor = true;
            this.buttonGetAllAppFun.Click += new System.EventHandler(this.buttonGetAllAppFun_Click);
            // 
            // buttonSetPermOfAppFun
            // 
            this.buttonSetPermOfAppFun.Location = new System.Drawing.Point(219, 206);
            this.buttonSetPermOfAppFun.Name = "buttonSetPermOfAppFun";
            this.buttonSetPermOfAppFun.Size = new System.Drawing.Size(85, 25);
            this.buttonSetPermOfAppFun.TabIndex = 16;
            this.buttonSetPermOfAppFun.Text = "SetPermOfAppFun";
            this.buttonSetPermOfAppFun.UseVisualStyleBackColor = true;
            this.buttonSetPermOfAppFun.Click += new System.EventHandler(this.buttonSetPermOfAppFun_Click);
            // 
            // buttonDeletePermOfAppFun
            // 
            this.buttonDeletePermOfAppFun.Location = new System.Drawing.Point(321, 206);
            this.buttonDeletePermOfAppFun.Name = "buttonDeletePermOfAppFun";
            this.buttonDeletePermOfAppFun.Size = new System.Drawing.Size(106, 25);
            this.buttonDeletePermOfAppFun.TabIndex = 17;
            this.buttonDeletePermOfAppFun.Text = "DeletePermOfAppFun";
            this.buttonDeletePermOfAppFun.UseVisualStyleBackColor = true;
            this.buttonDeletePermOfAppFun.Click += new System.EventHandler(this.buttonDeletePermOfAppFun_Click);
            // 
            // buttonIsUserDefRoleEnable
            // 
            this.buttonIsUserDefRoleEnable.Location = new System.Drawing.Point(115, 133);
            this.buttonIsUserDefRoleEnable.Name = "buttonIsUserDefRoleEnable";
            this.buttonIsUserDefRoleEnable.Size = new System.Drawing.Size(132, 25);
            this.buttonIsUserDefRoleEnable.TabIndex = 18;
            this.buttonIsUserDefRoleEnable.Text = "IsUserDefRoleEnable";
            this.buttonIsUserDefRoleEnable.UseVisualStyleBackColor = true;
            this.buttonIsUserDefRoleEnable.Click += new System.EventHandler(this.buttonIsUserDefRoleEnable_Click);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 427);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "App";
            this.Text = "Form1";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnRsConnect;
        private System.Windows.Forms.TextBox txtMachineSN;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMachineSN2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnUSBConnect;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonGetAllAppFun;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxAPPName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxFuncName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxPermissionsname;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxRoleName;
        private System.Windows.Forms.Button buttonGetAllRole;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxAPPOfRole;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBoxFuncOfRole;
        private System.Windows.Forms.Button buttonGetAppOfRole;
        private System.Windows.Forms.Button buttonGetFunOfRole;
        private System.Windows.Forms.Button buttonSetPermOfAppFun;
        private System.Windows.Forms.Button buttonDeletePermOfAppFun;
        private System.Windows.Forms.Button buttonIsUserDefRoleEnable;
    }
}

