using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeeMart
{
    public partial class Trading_Management : Form
    {
        string cnn_str = ConfigurationManager.ConnectionStrings["BeeMartConnectionString"].ConnectionString;
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        public Trading_Management()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer_Management customer_Management = new Customer_Management();
            this.Hide();
            customer_Management.ShowDialog();
        }

        private void Trading_Management_Load(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(cnn_str);
            da = new SqlDataAdapter("SELECT * FROM Bill", cnn);
            da.Fill(ds, "tblBill");
            int total = 0;
            if(ds.Tables["tblBill"].Rows.Count > 0)
            {
                dgvBill.DataSource = ds.Tables["tblBill"];
                for(int i = 0; i < ds.Tables["tblBill"].Rows.Count; i++)
                {
                    total += int.Parse(ds.Tables["tblBill"].Rows[i][4].ToString());
                }
                lblTurnover.Text = total.ToString();
            }    
            else
            {

            }                
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            this.Hide();
            homePage.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dateFrom = dtpdateFrom.Value;
            DateTime dateTo = dtpdateTo.Value;
            string staffID = txtStaffID.Text;
            string customerID = txtCustomerID.Text;
            if (check_numeric(staffID) && check_numeric(customerID) || staffID == "" && customerID == "" || staffID != "" && check_numeric(staffID) && customerID == "" || staffID == "" && check_numeric(customerID) && customerID != "")
            {
                SqlConnection cnn = new SqlConnection(cnn_str);
                SqlCommand cmd = new SqlCommand();
                SqlParameter pa_dateFrom = new SqlParameter("@dateFrom", dateFrom);
                SqlParameter pa_dateTo = new SqlParameter("@dateTo", dateTo);
                SqlParameter pa_staffID = new SqlParameter("@staffID", staffID);
                SqlParameter pa_customerID = new SqlParameter("@customerID", customerID);
                cmd.CommandText = "search_bill";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cnn;
                cmd.Parameters.Add(pa_dateFrom);
                cmd.Parameters.Add(pa_dateTo);
                cmd.Parameters.Add(pa_staffID);
                cmd.Parameters.Add(pa_customerID);
                da.SelectCommand = cmd;
                DataTable table = new DataTable();
                da.Fill(table);
                if (table.Rows.Count > 0)
                {
                    dgvBill.DataSource = table;       
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu");
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateBIll_Click(object sender, EventArgs e)
        {
            Create_Bill create_Bill = new Create_Bill();
            this.Hide();
            create_Bill.ShowDialog();
        }

        private bool check_numeric(string PhoneNum)
        {
            int test;
            return int.TryParse(PhoneNum, out test);
        }
    }
}
