using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZktAttendence.Test
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void dgnBtnProcess_Click(object sender, EventArgs e)
        {

        }



        private void txtToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtToDate.Text.Length == 8)
            {
                String workToDate = txtToDate.Text.Substring(2, 2) + "/" + txtToDate.Text.Substring(0, 2) + "/" + txtToDate.Text.Substring(4, 4);
                txtToDate.Text = workToDate;
                dgnBtnProcess.Focus();
            }
        }
        
        private void txtFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtFromDate.Text.Length == 8)
            {
                String workFromDate = txtFromDate.Text.Substring(2, 2) + "/" + txtFromDate.Text.Substring(0, 2) + "/" + txtFromDate.Text.Substring(4, 4);
                txtFromDate.Text = workFromDate;
                txtToDate.Focus();

            }
        }

        private void txtFromDate_MouseClick(object sender, MouseEventArgs e)
        {
            txtFromDate.Text = String.Empty;
        }

        private void txtToDate_MouseClick(object sender, MouseEventArgs e)
        {
            txtToDate.Text = String.Empty;
        }

        private void txtFromDateForClear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(txtFromDate.Text.Length == 8)
            {
                String workFromDate = txtFromDateForClear.Text.Substring(2, 2) + "/" + txtFromDateForClear.Text.Substring(0, 2) + "/" + txtFromDateForClear.Text.Substring(4, 4);
                txtFromDateForClear.Text = workFromDate;
 

            }
        }

        private void txtFromDateForClear_MouseClick(object sender, MouseEventArgs e)
        {
            txtFromDateForClear.Text = String.Empty;
        }
    }
}
