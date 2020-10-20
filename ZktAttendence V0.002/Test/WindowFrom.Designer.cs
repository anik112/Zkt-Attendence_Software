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
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgnBtnProcess = new System.Windows.Forms.Button();
            this.txtToDate = new System.Windows.Forms.TextBox();
            this.lblToDate = new System.Windows.Forms.Label();
            this.txtFromDate = new System.Windows.Forms.TextBox();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.txtShowMsg = new System.Windows.Forms.RichTextBox();
            btnClear = new System.Windows.Forms.Button();
            this.groupPan01.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            btnClear.AccessibleName = "btnClear";
            btnClear.BackColor = System.Drawing.SystemColors.Control;
            btnClear.Font = new System.Drawing.Font("Lucida Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnClear.ForeColor = System.Drawing.Color.Red;
            btnClear.Location = new System.Drawing.Point(228, 62);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(81, 33);
            btnClear.TabIndex = 5;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // groupPan01
            // 
            this.groupPan01.Controls.Add(this.checkBoxSelected);
            this.groupPan01.Controls.Add(btnClear);
            this.groupPan01.Controls.Add(this.txtDateForClear);
            this.groupPan01.Controls.Add(this.label2);
            this.groupPan01.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPan01.Location = new System.Drawing.Point(12, 135);
            this.groupPan01.Name = "groupPan01";
            this.groupPan01.Size = new System.Drawing.Size(315, 103);
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
            this.checkBoxSelected.Location = new System.Drawing.Point(6, 70);
            this.checkBoxSelected.Name = "checkBoxSelected";
            this.checkBoxSelected.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBoxSelected.Size = new System.Drawing.Size(177, 17);
            this.checkBoxSelected.TabIndex = 6;
            this.checkBoxSelected.Text = "Do you want to Clear? ";
            this.checkBoxSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxSelected.UseVisualStyleBackColor = false;
            // 
            // txtDateForClear
            // 
            this.txtDateForClear.AccessibleName = "txtFromDateForClear";
            this.txtDateForClear.Enabled = false;
            this.txtDateForClear.Location = new System.Drawing.Point(61, 21);
            this.txtDateForClear.Name = "txtDateForClear";
            this.txtDateForClear.Size = new System.Drawing.Size(115, 22);
            this.txtDateForClear.TabIndex = 4;
            this.txtDateForClear.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtFromDateForClear_MouseClick);
            this.txtDateForClear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromDateForClear_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Date <=";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.txtToDate);
            this.groupBox1.Controls.Add(this.lblToDate);
            this.groupBox1.Controls.Add(this.txtFromDate);
            this.groupBox1.Controls.Add(this.lblFromDate);
            this.groupBox1.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 121);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Download Data";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dgnBtnProcess);
            this.panel2.Location = new System.Drawing.Point(106, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(110, 51);
            this.panel2.TabIndex = 16;
            // 
            // dgnBtnProcess
            // 
            this.dgnBtnProcess.AccessibleName = "btnProcess";
            this.dgnBtnProcess.BackColor = System.Drawing.SystemColors.Control;
            this.dgnBtnProcess.Font = new System.Drawing.Font("Lucida Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgnBtnProcess.Location = new System.Drawing.Point(3, 9);
            this.dgnBtnProcess.Name = "dgnBtnProcess";
            this.dgnBtnProcess.Size = new System.Drawing.Size(102, 32);
            this.dgnBtnProcess.TabIndex = 3;
            this.dgnBtnProcess.Text = "Process";
            this.dgnBtnProcess.UseVisualStyleBackColor = false;
            this.dgnBtnProcess.Click += new System.EventHandler(this.dgnBtnProcess_Click);
            // 
            // txtToDate
            // 
            this.txtToDate.AccessibleName = "";
            this.txtToDate.Location = new System.Drawing.Point(219, 23);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(86, 22);
            this.txtToDate.TabIndex = 2;
            this.txtToDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtToDate_MouseClick);
            this.txtToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtToDate_KeyPress);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Lucida Sans", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblToDate.Location = new System.Drawing.Point(164, 26);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(52, 14);
            this.lblToDate.TabIndex = 14;
            this.lblToDate.Text = "To Date:";
            this.lblToDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFromDate
            // 
            this.txtFromDate.AccessibleName = "txtFromDate";
            this.txtFromDate.Location = new System.Drawing.Point(71, 23);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(88, 22);
            this.txtFromDate.TabIndex = 1;
            this.txtFromDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtFromDate_MouseClick);
            this.txtFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromDate_KeyPress);
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Lucida Sans", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Location = new System.Drawing.Point(6, 26);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(64, 14);
            this.lblFromDate.TabIndex = 12;
            this.lblFromDate.Text = "From Date:";
            // 
            // txtShowMsg
            // 
            this.txtShowMsg.BackColor = System.Drawing.SystemColors.Info;
            this.txtShowMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtShowMsg.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShowMsg.Location = new System.Drawing.Point(12, 242);
            this.txtShowMsg.Name = "txtShowMsg";
            this.txtShowMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtShowMsg.Size = new System.Drawing.Size(316, 151);
            this.txtShowMsg.TabIndex = 13;
            this.txtShowMsg.Text = "";
            // 
            // WindowFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 404);
            this.Controls.Add(this.txtShowMsg);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupPan01);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(150, 50);
            this.Name = "WindowFrom";
            this.Text = "Vistasoft IT Bangladesh Ltd.";
            this.groupPan01.ResumeLayout(false);
            this.groupPan01.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupPan01;
        private System.Windows.Forms.TextBox txtDateForClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button dgnBtnProcess;
        private System.Windows.Forms.TextBox txtToDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.TextBox txtFromDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.RichTextBox txtShowMsg;
        private System.Windows.Forms.CheckBox checkBoxSelected;
    }
}