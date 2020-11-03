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
    public partial class Update_Staff_Form : Form
    {
        string cnn_str = ConfigurationManager.ConnectionStrings["BeeMartConnectionString"].ConnectionString;
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        public Update_Staff_Form()
        {
            InitializeComponent();
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel24_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string name = txtName.Text;
            DateTime DOB = dtpDOB.Value;
            string position = cbbPosition.Text;
            string gender = cbbGender.Text;

            if (username != "" && name != "" && DOB != null && position != "" && gender != "")
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
                da.Fill(ds, "tblStaff_Update");
                //save to Database
                if (ds.Tables["tblStaff_Update"].Rows.Count > 0)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = "Update_Staff";
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pa1 = new SqlParameter("@username", username);
                    //SqlParameter pa2 = new SqlParameter("@password", password);
                    SqlParameter pa3 = new SqlParameter("@name", name);
                    SqlParameter pa4 = new SqlParameter("@DOB", DOB);
                    SqlParameter pa5 = new SqlParameter("@position", position);
                    SqlParameter pa6 = new SqlParameter("@gender", gender);
                    cmd.Parameters.Add(pa1);
                    //cmd.Parameters.Add(pa2);
                    cmd.Parameters.Add(pa3);
                    cmd.Parameters.Add(pa4);
                    cmd.Parameters.Add(pa5);
                    cmd.Parameters.Add(pa6);
                    da.SelectCommand = cmd;
                    SqlCommandBuilder sql = new SqlCommandBuilder();
                    da.Update(ds, "tblStaff_Update");
                    da.Fill(ds, "tblStaff_Update");
                    MessageBox.Show("Cập nhật thành công");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Username này không tồn tại!");
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin");
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Update_Staff_Form_Load(object sender, EventArgs e)
        {
            cbbGender.Items.Add("Male");
            cbbGender.Items.Add("Female");
            cbbPosition.Items.Add("Admin");
            cbbPosition.Items.Add("Staff");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string name = txtName.Text;
            DateTime DOB = dtpDOB.Value;
            string position = cbbPosition.Text;
            string gender = cbbGender.Text;

            if(username != "")
            {
                SqlConnection cnn = new SqlConnection(cnn_str);
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = cnn;
                cmd1.CommandText = "Check_Staff";
                cmd1.CommandType = CommandType.StoredProcedure;
                SqlParameter pa = new SqlParameter("@username", username);
                cmd1.Parameters.Add(pa);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds, "tblStaff_Update");
                if(ds.Tables["tblStaff_Update"].Rows.Count > 0)
                {
                    string sql = "DELETE FROM Staff WHERE Username = @username";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    SqlParameter pa1 = new SqlParameter("@username", username);
                    cmd.Parameters.Add(pa1);
                    da.SelectCommand = cmd;
                    SqlCommandBuilder sqls = new SqlCommandBuilder();
                    da.Update(ds, "tblStaff_Update");
                    da.Fill(ds, "tblStaff_Update");
                    MessageBox.Show("Xóa thành công");
                    this.Hide();
                    Staff_Management staff_Management = new Staff_Management();
                    staff_Management.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Username này không tồn tại");
                }
            }
            else
            {
                MessageBox.Show("Chưa nhập Username");
            }    
        }

        private void cbbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
