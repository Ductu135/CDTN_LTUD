using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeeMart
{
    public partial class HomePage : Form
    {

        public string userName;
        public bool position;
        public HomePage()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            lblUsername.Text = userName;
            if(position == false)
            {
                btnStaff.Visible = true;
                btnReport.Visible = true;
            }
            else
            {
                btnStaff.Visible = true;
                btnReport.Visible = true;
            }
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            
            Staff_Management staff_Management = new Staff_Management();
            staff_Management.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            //HomePage homePage = new HomePage();
            //homePage.Hide();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            Product_Management product_Management = new Product_Management();
            product_Management.ShowDialog();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            Trading_Management trading_Management = new Trading_Management();
            trading_Management.ShowDialog();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Report_Management report_Management = new Report_Management();
            report_Management.ShowDialog();
        }
    }
}
