namespace ZktAttendence.view
{
    partial class test
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
            System.Windows.Forms.Label check_data;
            this.get_name = new System.Windows.Forms.TextBox();
            this.btn_show = new System.Windows.Forms.Button();
            check_data = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // check_data
            // 
            check_data.AccessibleName = "lblShowMsg";
            check_data.BackColor = System.Drawing.SystemColors.ActiveCaption;
            check_data.Location = new System.Drawing.Point(51, 45);
            check_data.Name = "check_data";
            check_data.Size = new System.Drawing.Size(130, 19);
            check_data.TabIndex = 0;
            check_data.Click += new System.EventHandler(this.check_data_Click);
            // 
            // get_name
            // 
            this.get_name.Location = new System.Drawing.Point(54, 67);
            this.get_name.Name = "get_name";
            this.get_name.Size = new System.Drawing.Size(155, 20);
            this.get_name.TabIndex = 1;
            // 
            // btn_show
            // 
            this.btn_show.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_show.Location = new System.Drawing.Point(54, 93);
            this.btn_show.Name = "btn_show";
            this.btn_show.Size = new System.Drawing.Size(75, 23);
            this.btn_show.TabIndex = 2;
            this.btn_show.Text = "Show";
            this.btn_show.UseVisualStyleBackColor = false;
            this.btn_show.Click += new System.EventHandler(this.btn_show_Click);
            // 
            // test
            // 
            this.ClientSize = new System.Drawing.Size(340, 361);
            this.Controls.Add(this.btn_show);
            this.Controls.Add(this.get_name);
            this.Controls.Add(check_data);
            this.Name = "test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label empName;
        private System.Windows.Forms.TextBox get_name;
        private System.Windows.Forms.Button btn_show;
    }
}