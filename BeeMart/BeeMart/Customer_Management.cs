using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeeMart
{
    public partial class Customer_Management : Form
    {
        string cnn_str = ConfigurationManager.ConnectionStrings["BeeMartConnectionString"].ConnectionString;
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        public Customer_Management()
        {
            InitializeComponent();
        }

        private void Customer_Management_Load(object sender, EventArgs e)
        {
            ccbCusGender.Items.Add("Male");
            ccbCusGender.Items.Add("Female");
            SqlConnection cnn = new SqlConnection(cnn_str);
            da = new SqlDataAdapter("SELECT * FROM Customer", cnn);
            da.Fill(ds, "tblCustomer");
            if (ds.Tables["tblCustomer"].Rows.Count > 0)
            {
                dgvCustomer.DataSource = ds.Tables["tblCustomer"];
            }
            else
            {
                //MessageBox.Show("Không có dữ liệu");
            }    
        }

        private void btnAddCus_Click(object sender, EventArgs e)
        {
            string CustomerID = txtCustomerID.Text;
            string CusName = txtCusName.Text;
            DateTime DOB = dtpCusDOB.Value;
            string Gender = ccbCusGender.Text;
            string PhoneNum = txtCusPhone.Text;
            check_dup(CustomerID);
            check_CusID(CustomerID);
            bool check = check_dup(CustomerID);
            bool checkCusID = check_CusID(CustomerID);
            if(check == false && checkCusID == false)
            {
                MessageBox.Show("Khách hàng đã thêm tại hoặc sai mã");
            }    
            else
            {
                SqlConnection cnn = new SqlConnection(cnn_str);
                SqlCommand cmd_addCus = new SqlCommand();
                SqlParameter pa_name = new SqlParameter("@CusName", CusName);
                SqlParameter pa_DOB = new SqlParameter("@DOB", DOB);
                SqlParameter pa_Gender = new SqlParameter("@Gender", Gender);
                SqlParameter pa_PhoneNum = new SqlParameter("@PhoneNum", PhoneNum);
                cmd_addCus.Connection = cnn;
                cmd_addCus.CommandText = "add_customer";
                cmd_addCus.CommandType = CommandType.StoredProcedure;
                cmd_addCus.Parameters.Add(pa_name);
                cmd_addCus.Parameters.Add(pa_DOB);
                cmd_addCus.Parameters.Add(pa_Gender);
                cmd_addCus.Parameters.Add(pa_PhoneNum);
                SqlCommandBuilder sql = new SqlCommandBuilder();
                da.SelectCommand = cmd_addCus;
                da.Update(ds, "tblCustomer_Search");
                da.Fill(ds, "tblCustomer_Search");
                if(ds.Tables["tblCustomer_Search"].Rows.Count > 0)
                {
                    MessageBox.Show("Thêm thành công");
                }
                else
                {
                    MessageBox.Show("Thêm thất bại");
                }    
            }                
        }

        private void btnUpdateCus_Click(object sender, EventArgs e)
        {
            string CustomerID = txtCustomerID.Text;
            string CusName = txtCusName.Text;
            DateTime DOB = dtpCusDOB.Value;
            string Gender = ccbCusGender.Text;
            string PhoneNum = txtCusPhone.Text;
            check_dup(CustomerID);
            if (check_dup(CustomerID) == false && check_CusID(CustomerID) && check_CusID(PhoneNum))
            {
                SqlConnection cnn = new SqlConnection(cnn_str);
                SqlCommand cmd_updateCus = new SqlCommand();
                int CusID = Int32.Parse(CustomerID);
                SqlParameter pa_upCusID = new SqlParameter("@CusID", CusID);
                SqlParameter pa_upName = new SqlParameter("@CusName", CusName);
                SqlParameter pa_upDOB = new SqlParameter("@DOB", DOB);
                SqlParameter pa_upGender = new SqlParameter("@Gender", Gender);
                SqlParameter pa_upPhoneNum = new SqlParameter("@PhoneNum", PhoneNum);
                cmd_updateCus.Connection = cnn;
                cmd_updateCus.CommandText = "update_customer";
                cmd_updateCus.CommandType = CommandType.StoredProcedure;
                cmd_updateCus.Parameters.Add(pa_upCusID);
                cmd_updateCus.Parameters.Add(pa_upName);
                cmd_updateCus.Parameters.Add(pa_upDOB);
                cmd_updateCus.Parameters.Add(pa_upGender);
                cmd_updateCus.Parameters.Add(pa_upPhoneNum);
                SqlCommandBuilder sql = new SqlCommandBuilder();
                da.SelectCommand = cmd_updateCus;
                da.Update(ds, "tblCustomer_Search");
                da.Fill(ds, "tblCustomer_Search");
                if (ds.Tables["tblCustomer_Search"].Rows.Count > 0)
                {
                    dgvCustomer.DataSource = ds.Tables["tblCustomer"];
                    MessageBox.Show("Cập nhật thành công");
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại");
                }
            }
            else
            {
                MessageBox.Show("Chưa có thông tin khách hàng này");
            }
        }

        private void btnDeleteCus_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Trading_Management trading_Management = new Trading_Management();
            this.Hide();
            trading_Management.ShowDialog();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private bool check_dup (string customerID)
        {
            bool result = false;
            if (check_CusID(customerID))
            {
                SqlConnection cnn = new SqlConnection(cnn_str);
                SqlCommand cmd = new SqlCommand();
                SqlParameter pa1 = new SqlParameter("@CusNameID", customerID);
                cmd.Connection = cnn;
                cmd.CommandText = "check_customer";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(pa1);
                da.SelectCommand = cmd;
                da.Fill(ds, "tblCustomer_Search");
                if (ds.Tables["tblCustomer_Search"].Rows.Count <= 0)
                {
                    return result = true;
                }
                else
                {
                    return result;
                }
            }    
            else
            {
                return result;
            }          
        }

        private bool check_CusID(string PhoneNum)
        {
            int test;
            return int.TryParse(PhoneNum, out test);
        }

        private void ccbCusGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
