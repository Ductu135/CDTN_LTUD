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
    public partial class Update_Staff_Password : Form
    {
        string cnn_str = ConfigurationManager.ConnectionStrings["BeeMartConnectionString"].ConnectionString;
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        public Update_Staff_Password()
        {
            InitializeComponent();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string userName = txtUsername.Text;
            string passWord = txtPass.Text;
            string newPass = txtNewPass.Text;

            if(userName != "" && passWord != "" && newPass != "" && passWord != newPass)
            {
                SqlConnection cnn = new SqlConnection(cnn_str);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Logins";
                SqlParameter pa = new SqlParameter("@username", userName);
                SqlParameter pa2 = new SqlParameter("@password", passWord);
                cmd.Parameters.Add(pa);
                cmd.Parameters.Add(pa2);
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "tblUsername");
                if (ds.Tables["tblUsername"].Rows.Count > 0)
                {
                    string sql = "UPDATE Staff SET Passwords = @newpass WHERE Username = @username";
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = cnn;
                    cmd1.CommandText = sql;
                    cmd1.CommandType = CommandType.Text;
                    SqlParameter pa1 = new SqlParameter("@newpass", newPass);
                    SqlParameter pa3 = new SqlParameter("@username", userName);
                    cmd1.Parameters.Add(pa1);
                    cmd1.Parameters.Add(pa3);
                    da.SelectCommand = cmd1;
                    SqlCommandBuilder sqls = new SqlCommandBuilder();
                    da.Update(ds, "tblUsername");
                    da.Fill(ds, "tblUsername");
                    MessageBox.Show("Thay đổi thành công");
                    this.Hide();
                    Staff_Management staff_Management = new Staff_Management();
                    staff_Management.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Tài khoản và mật khẩu này không tồn tại");
                }
            }    
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Staff_Management staff_Management = new Staff_Management();
            staff_Management.ShowDialog();
        }
    }
}
