namespace ZktAttendence.Test
{
    partial class TestForm
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
            this.groupPan01 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtFromDateForClear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgnBtnProcess = new System.Windows.Forms.Button();
            this.txtToDate = new System.Windows.Forms.TextBox();
            this.lblToDate = new System.Windows.Forms.Label();
            this.txtFromDate = new System.Windows.Forms.TextBox();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.groupPan01.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPan01
            // 
            this.groupPan01.Controls.Add(this.btnClear);
            this.groupPan01.Controls.Add(this.txtFromDateForClear);
            this.groupPan01.Controls.Add(this.label2);
            this.groupPan01.Location = new System.Drawing.Point(12, 167);
            this.groupPan01.Name = "groupPan01";
            this.groupPan01.Size = new System.Drawing.Size(321, 130);
            this.groupPan01.TabIndex = 5;
            this.groupPan01.TabStop = false;
            this.groupPan01.Text = "Clear Attendence Data";
            // 
            // btnClear
            // 
            this.btnClear.AccessibleName = "btnClear";
            this.btnClear.Location = new System.Drawing.Point(207, 86);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(104, 33);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // txtFromDateForClear
            // 
            this.txtFromDateForClear.AccessibleName = "txtFromDateForClear";
            this.txtFromDateForClear.Location = new System.Drawing.Point(75, 26);
            this.txtFromDateForClear.Name = "txtFromDateForClear";
            this.txtFromDateForClear.Size = new System.Drawing.Size(236, 20);
            this.txtFromDateForClear.TabIndex = 5;
            this.txtFromDateForClear.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtFromDateForClear_MouseClick);
            this.txtFromDateForClear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromDateForClear_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "From Date:";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Info;
            this.textBox3.Location = new System.Drawing.Point(12, 313);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(321, 35);
            this.textBox3.TabIndex = 6;
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromDate_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.txtToDate);
            this.groupBox1.Controls.Add(this.lblToDate);
            this.groupBox1.Controls.Add(this.txtFromDate);
            this.groupBox1.Controls.Add(this.lblFromDate);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 149);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Download Attendence";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dgnBtnProcess);
            this.panel2.Location = new System.Drawing.Point(207, 86);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(104, 51);
            this.panel2.TabIndex = 16;
            // 
            // dgnBtnProcess
            // 
            this.dgnBtnProcess.AccessibleName = "btnProcess";
            this.dgnBtnProcess.Location = new System.Drawing.Point(10, 4);
            this.dgnBtnProcess.Name = "dgnBtnProcess";
            this.dgnBtnProcess.Size = new System.Drawing.Size(83, 42);
            this.dgnBtnProcess.TabIndex = 0;
            this.dgnBtnProcess.Text = "Process";
            this.dgnBtnProcess.UseVisualStyleBackColor = true;
            this.dgnBtnProcess.Click += new System.EventHandler(this.dgnBtnProcess_Click);
            // 
            // txtToDate
            // 
            this.txtToDate.AccessibleName = "";
            this.txtToDate.Location = new System.Drawing.Point(75, 49);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(236, 20);
            this.txtToDate.TabIndex = 15;
            this.txtToDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtToDate_MouseClick);
            this.txtToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtToDate_KeyPress);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblToDate.Location = new System.Drawing.Point(20, 52);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(49, 13);
            this.lblToDate.TabIndex = 14;
            this.lblToDate.Text = "To Date:";
            this.lblToDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFromDate
            // 
            this.txtFromDate.AccessibleName = "txtFromDate";
            this.txtFromDate.Location = new System.Drawing.Point(75, 23);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(236, 20);
            this.txtFromDate.TabIndex = 13;
            this.txtFromDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtFromDate_MouseClick);
            this.txtFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromDate_KeyPress);
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(10, 26);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(59, 13);
            this.lblFromDate.TabIndex = 12;
            this.lblFromDate.Text = "From Date:";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 355);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.groupPan01);
            this.Name = "TestForm";
            this.Text = "Form1";
            this.groupPan01.ResumeLayout(false);
            this.groupPan01.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupPan01;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtFromDateForClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button dgnBtnProcess;
        private System.Windows.Forms.TextBox txtToDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.TextBox txtFromDate;
        private System.Windows.Forms.Label lblFromDate;
    }
}