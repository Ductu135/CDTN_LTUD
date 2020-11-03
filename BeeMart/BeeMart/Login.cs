using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeeMart
{
    public partial class Login : Form
    {

        string cnn_str = ConfigurationManager.ConnectionStrings["BeeMartConnectionString"].ConnectionString;
        DataSet ds = new DataSet();
        SqlDataAdapter da;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
           
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            SqlConnection cnn = new SqlConnection(cnn_str);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Logins";
            SqlParameter pa = new SqlParameter("@username", username);
            SqlParameter pa2 = new SqlParameter("@password", password);
            cmd.Parameters.Add(pa);
            cmd.Parameters.Add(pa2);
            da = new SqlDataAdapter(cmd);
            da.Fill(ds, "tblUsername");
            if(ds.Tables["tblUsername"].Rows.Count > 0)
            {
                HomePage homePage = new HomePage();
                homePage.userName = ds.Tables["tblUsername"].Rows[0][5].ToString();
                homePage.position = (bool)ds.Tables["tblUsername"].Rows[0][4];
                this.Hide();
                homePage.ShowDialog();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại");
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
