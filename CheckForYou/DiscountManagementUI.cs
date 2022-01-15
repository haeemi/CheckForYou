using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckForYou2
{
    public partial class DiscountManagementUI : Form
    {
        public DiscountManagementUI()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            StudentManagementUI sr = new StudentManagementUI(); sr.Show();
            this.Hide();
            sr.Owner = this;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            TeacherManagementUI sr = new TeacherManagementUI(); sr.Show();
            this.Hide();
            sr.Owner = this;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            OperationStatusUI sr = new OperationStatusUI(); sr.Show();
            this.Hide();
            sr.Owner = this;
        }
    }
}
