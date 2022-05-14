namespace ZktAttendence.Test
{
    partial class WindowFrom
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
            System.Windows.Forms.Button btnClear;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowFrom));
            this.groupPan01 = new System.Windows.Forms.GroupBox();
            this.checkBoxSelected = new System.Windows.Forms.CheckBox();
            this.txtDateForClear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnProcessRTA = new System.Windows.Forms.Button();
            this.txtDateOfRTA = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgnBtnProcess = new System.Windows.Forms.Button();
            this.txtToDate = new System.Windows.Forms.TextBox();
            this.lblToDate = new System.Windows.Forms.Label();
            this.txtFromDate = new System.Windows.Forms.TextBox();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.txtShowMsg = new System.Windows.Forms.RichTextBox();
            this.tablePan = new System.Windows.Forms.Panel();
            this.isDeselectAll = new System.Windows.Forms.CheckBox();
            this.isSelectAll = new System.Windows.Forms.CheckBox();
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.fileHeadMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileAddDevice = new System.Windows.Forms.ToolStripMenuItem();
            btnClear = new System.Windows.Forms.Button();
            this.groupPan01.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            btnClear.AccessibleName = "btnClear";
            btnClear.BackColor = System.Drawing.SystemColors.Control;
            btnClear.Font = new System.Drawing.Font("Lucida Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnClear.ForeColor = System.Drawing.Color.Red;
            btnClear.Location = new System.Drawing.Point(65, 113);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(122, 33);
            btnClear.TabIndex = 5;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // groupPan01
            // 
            this.groupPan01.BackColor = System.Drawing.SystemColors.Control;
            this.groupPan01.Controls.Add(this.checkBoxSelected);
            this.groupPan01.Controls.Add(btnClear);
            this.groupPan01.Controls.Add(this.txtDateForClear);
            this.groupPan01.Controls.Add(this.label2);
            this.groupPan01.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPan01.Location = new System.Drawing.Point(346, 38);
            this.groupPan01.Name = "groupPan01";
            this.groupPan01.Size = new System.Drawing.Size(201, 167);
            this.groupPan01.TabIndex = 5;
            this.groupPan01.TabStop = false;
            this.groupPan01.Text = "Clear Data";
            // 
            // checkBoxSelected
            // 
            this.checkBoxSelected.AutoSize = true;
            this.checkBoxSelected.BackColor = System.Drawing.SystemColors.Control;
            this.checkBoxSelected.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxSelected.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSelected.ForeColor = System.Drawing.Color.Firebrick;
            this.checkBoxSelected.Location = new System.Drawing.Point(6, 66);
            this.checkBoxSelected.Name = "checkBoxSelected";
            this.checkBoxSelected.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBoxSelected.Size = new System.Drawing.Size(181, 17);
            this.checkBoxSelected.TabIndex = 6;
            this.checkBoxSelected.Text = "Do you want to Clear ? ";
            this.checkBoxSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxSelected.UseVisualStyleBackColor = false;
            // 
            // txtDateForClear
            // 
            this.txtDateForClear.AccessibleName = "txtFromDateForClear";
            this.txtDateForClear.Enabled = false;
            this.txtDateForClear.Location = new System.Drawing.Point(65, 31);
            this.txtDateForClear.Name = "txtDateForClear";
            this.txtDateForClear.Size = new System.Drawing.Size(122, 22);
            this.txtDateForClear.TabIndex = 4;
            this.txtDateForClear.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtFromDateForClear_MouseClick);
            this.txtDateForClear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromDateForClear_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Date <=";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 167);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Download";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Controls.Add(this.txtDateOfRTA);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(197, 27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(124, 130);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "From RTA 600";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.btnProcessRTA);
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel1.Location = new System.Drawing.Point(8, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(109, 43);
            this.panel1.TabIndex = 8;
            // 
            // btnProcessRTA
            // 
            this.btnProcessRTA.BackColor = System.Drawing.SystemColors.Control;
            this.btnProcessRTA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnProcessRTA.Font = new System.Drawing.Font("Lucida Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessRTA.Location = new System.Drawing.Point(8, 5);
            this.btnProcessRTA.Name = "btnProcessRTA";
            this.btnProcessRTA.Size = new System.Drawing.Size(93, 33);
            this.btnProcessRTA.TabIndex = 8;
            this.btnProcessRTA.Text = "Process";
            this.btnProcessRTA.UseVisualStyleBackColor = false;
            this.btnProcessRTA.Click += new System.EventHandler(this.btnProcessRTA_Click);
            // 
            // txtDateOfRTA
            // 
            this.txtDateOfRTA.AccessibleName = "txtDateOfRTA";
            this.txtDateOfRTA.Enabled = false;
            this.txtDateOfRTA.Location = new System.Drawing.Point(8, 37);
            this.txtDateOfRTA.Name = "txtDateOfRTA";
            this.txtDateOfRTA.Size = new System.Drawing.Size(109, 22);
            this.txtDateOfRTA.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "Date <=";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.txtToDate);
            this.groupBox2.Controls.Add(this.lblToDate);
            this.groupBox2.Controls.Add(this.txtFromDate);
            this.groupBox2.Controls.Add(this.lblFromDate);
            this.groupBox2.Location = new System.Drawing.Point(8, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(173, 130);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "From ZKT";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel2.Controls.Add(this.dgnBtnProcess);
            this.panel2.Location = new System.Drawing.Point(13, 81);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(150, 43);
            this.panel2.TabIndex = 21;
            // 
            // dgnBtnProcess
            // 
            this.dgnBtnProcess.AccessibleName = "btnProcess";
            this.dgnBtnProcess.BackColor = System.Drawing.SystemColors.Control;
            this.dgnBtnProcess.Font = new System.Drawing.Font("Lucida Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgnBtnProcess.Location = new System.Drawing.Point(19, 5);
            this.dgnBtnProcess.Name = "dgnBtnProcess";
            this.dgnBtnProcess.Size = new System.Drawing.Size(111, 33);
            this.dgnBtnProcess.TabIndex = 3;
            this.dgnBtnProcess.Text = "Process";
            this.dgnBtnProcess.UseVisualStyleBackColor = false;
            this.dgnBtnProcess.Click += new System.EventHandler(this.dgnBtnProcess_Click);
            // 
            // txtToDate
            // 
            this.txtToDate.AccessibleName = "";
            this.txtToDate.Location = new System.Drawing.Point(75, 53);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(88, 22);
            this.txtToDate.TabIndex = 18;
            this.txtToDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtToDate_MouseClick);
            this.txtToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtToDate_KeyPress);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Lucida Sans", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblToDate.Location = new System.Drawing.Point(20, 56);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(52, 14);
            this.lblToDate.TabIndex = 20;
            this.lblToDate.Text = "To Date:";
            this.lblToDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFromDate
            // 
            this.txtFromDate.AccessibleName = "txtFromDate";
            this.txtFromDate.Location = new System.Drawing.Point(75, 25);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(88, 22);
            this.txtFromDate.TabIndex = 17;
            this.txtFromDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtFromDate_MouseClick);
            this.txtFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromDate_KeyPress);
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Lucida Sans", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Location = new System.Drawing.Point(10, 28);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(64, 14);
            this.lblFromDate.TabIndex = 19;
            this.lblFromDate.Text = "From Date:";
            // 
            // txtShowMsg
            // 
            this.txtShowMsg.BackColor = System.Drawing.Color.LightYellow;
            this.txtShowMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtShowMsg.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShowMsg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtShowMsg.Location = new System.Drawing.Point(553, 38);
            this.txtShowMsg.Name = "txtShowMsg";
            this.txtShowMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtShowMsg.Size = new System.Drawing.Size(354, 412);
            this.txtShowMsg.TabIndex = 13;
            this.txtShowMsg.Text = "Console";
            // 
            // tablePan
            // 
            this.tablePan.AutoScroll = true;
            this.tablePan.BackColor = System.Drawing.Color.LightYellow;
            this.tablePan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tablePan.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tablePan.Location = new System.Drawing.Point(12, 237);
            this.tablePan.Name = "tablePan";
            this.tablePan.Padding = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.tablePan.Size = new System.Drawing.Size(535, 213);
            this.tablePan.TabIndex = 17;
            this.tablePan.MouseEnter += new System.EventHandler(this.tablePan_MouseEnter);
            // 
            // isDeselectAll
            // 
            this.isDeselectAll.AccessibleName = "deselectCheckBox";
            this.isDeselectAll.BackColor = System.Drawing.Color.Transparent;
            this.isDeselectAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.isDeselectAll.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isDeselectAll.ForeColor = System.Drawing.Color.Firebrick;
            this.isDeselectAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.isDeselectAll.Location = new System.Drawing.Point(423, 211);
            this.isDeselectAll.Name = "isDeselectAll";
            this.isDeselectAll.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.isDeselectAll.Size = new System.Drawing.Size(124, 21);
            this.isDeselectAll.TabIndex = 18;
            this.isDeselectAll.Text = "Deselect All";
            this.isDeselectAll.UseVisualStyleBackColor = false;
            this.isDeselectAll.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // isSelectAll
            // 
            this.isSelectAll.AccessibleName = "selectCheckBox";
            this.isSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.isSelectAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.isSelectAll.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isSelectAll.ForeColor = System.Drawing.Color.Firebrick;
            this.isSelectAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.isSelectAll.Location = new System.Drawing.Point(12, 211);
            this.isSelectAll.Name = "isSelectAll";
            this.isSelectAll.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.isSelectAll.Size = new System.Drawing.Size(110, 21);
            this.isSelectAll.TabIndex = 19;
            this.isSelectAll.Text = "Select All";
            this.isSelectAll.UseVisualStyleBackColor = false;
            this.isSelectAll.Click += new System.EventHandler(this.isSelectAll_Click);
            // 
            // menuBar
            // 
            this.menuBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileHeadMenu});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(911, 24);
            this.menuBar.TabIndex = 20;
            this.menuBar.Text = "menuStrip1";
            // 
            // fileHeadMenu
            // 
            this.fileHeadMenu.AccessibleName = "fileHeadMenu";
            this.fileHeadMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileAddDevice});
            this.fileHeadMenu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileHeadMenu.Name = "fileHeadMenu";
            this.fileHeadMenu.Size = new System.Drawing.Size(38, 20);
            this.fileHeadMenu.Text = "&File";
            // 
            // menuFileAddDevice
            // 
            this.menuFileAddDevice.AccessibleName = "menuFileAddDevice";
            this.menuFileAddDevice.Name = "menuFileAddDevice";
            this.menuFileAddDevice.Size = new System.Drawing.Size(138, 22);
            this.menuFileAddDevice.Text = "&Add Device";
            this.menuFileAddDevice.Click += new System.EventHandler(this.menuFileAddDevice_Click);
            // 
            // WindowFrom
            // 
            this.AccessibleName = "deselectCheckBox";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 459);
            this.Controls.Add(this.isSelectAll);
            this.Controls.Add(this.isDeselectAll);
            this.Controls.Add(this.tablePan);
            this.Controls.Add(this.txtShowMsg);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupPan01);
            this.Controls.Add(this.menuBar);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(150, 50);
            this.Name = "WindowFrom";
            this.Text = "Copyright @ 2009 Maintenance By DATADSS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WindowFrom_FormClosing);
            this.groupPan01.ResumeLayout(false);
            this.groupPan01.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupPan01;
        private System.Windows.Forms.TextBox txtDateForClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox txtShowMsg;
        private System.Windows.Forms.CheckBox checkBoxSelected;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtDateOfRTA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button dgnBtnProcess;
        private System.Windows.Forms.TextBox txtToDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.TextBox txtFromDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnProcessRTA;
        private System.Windows.Forms.Panel tablePan;
        private System.Windows.Forms.CheckBox isDeselectAll;
        private System.Windows.Forms.CheckBox isSelectAll;
        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolStripMenuItem fileHeadMenu;
        private System.Windows.Forms.ToolStripMenuItem menuFileAddDevice;
    }
}