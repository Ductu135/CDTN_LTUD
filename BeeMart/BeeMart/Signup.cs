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
    public partial class Signup : Form
    {
        string cnn_str = ConfigurationManager.ConnectionStrings["BeeMartConnectionString"].ConnectionString;
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        public Signup()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Staff_Management staff_Management = new Staff_Management();
            staff_Management.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string username = txtusersname.Text;
            string password = txtpass.Text;
            string name = txtname.Text;
            DateTime DOB = dtpDOB.Value;
            string position = cbbPosition.Text;
            string gender = cbbGender.Text;

            if (username != "" && password != "" && name != "" && DOB !=  null && position != "" && gender != "")
            {
                //check duppicated
                SqlConnection cnn = new SqlConnection(cnn_str);
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = cnn;
                cmd1.CommandText = "Check_Staff";
                cmd1.CommandType = CommandType.StoredProcedure;
                SqlParameter pa = new SqlParameter("@username", username);
                cmd1.Parameters.Add(pa);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds, "tblStaff");
                //save to Database
                if (ds.Tables["tblStaff"].Rows.Count <= 0)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = "Sign_up";
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pa1 = new SqlParameter("@username", username);
                    SqlParameter pa2 = new SqlParameter("@password", password);
                    SqlParameter pa3 = new SqlParameter("@name", name);
                    SqlParameter pa4 = new SqlParameter("@DOB", DOB);
                    SqlParameter pa5 = new SqlParameter("@position", position);
                    SqlParameter pa6 = new SqlParameter("@gender", gender);
                    cmd.Parameters.Add(pa1);
                    cmd.Parameters.Add(pa2);
                    cmd.Parameters.Add(pa3);
                    cmd.Parameters.Add(pa4);
                    cmd.Parameters.Add(pa5);
                    cmd.Parameters.Add(pa6);
                    da.SelectCommand = cmd;
                    SqlCommandBuilder sql = new SqlCommandBuilder();
                    da.Update(ds, "tblStaff");
                    da.Fill(ds, "tblStaff");
                    MessageBox.Show("Tạo tài khoản thành công");
                    this.Hide();
                    Staff_Management staff_Management = new Staff_Management();
                    staff_Management.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Username này đã tồn tại!");
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin");
            }

            
          
            
        }

        private void cbbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Signup_Load(object sender, EventArgs e)
        {
            cbbGender.Items.Add("Male");
            cbbGender.Items.Add("Female");
            cbbPosition.Items.Add("Admin");
            cbbPosition.Items.Add("Staff");

        }
    }
}
