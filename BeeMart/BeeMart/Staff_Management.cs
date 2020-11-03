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
    public partial class Staff_Management : Form
    {
        string cnn_str = ConfigurationManager.ConnectionStrings["BeeMartConnectionString"].ConnectionString;
        DataSet ds = new DataSet();
        SqlDataAdapter da;

        public Staff_Management()
        {
            InitializeComponent();
        }

        private void Staff_Management_Load(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(cnn_str);
            da = new SqlDataAdapter("SELECT * FROM Staff WHERE position = 0", cnn_str);
            da.Fill(ds, "tblStaff");
            if(ds.Tables["tblStaff"].Rows.Count > 0)
            {
                dgvStaff.DataSource = ds.Tables["tblStaff"];
            }

            string sql = "SELECT * FROM Staff WHERE position = 1";
            da = new SqlDataAdapter(sql, cnn_str);
            da.Fill(ds, "tblAdmin");
            if(ds.Tables["tblAdmin"].Rows.Count > 0)
            {
                dgvAdmin.DataSource = ds.Tables["tblAdmin"];
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Signup signup = new Signup();
            signup.ShowDialog();
        }

        private void dgvStaff_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Update_Staff_Form form = new Update_Staff_Form();
            form.ShowDialog();
        }

        private void dgvAdmin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Update_Staff_Form form = new Update_Staff_Form();
            form.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Update_Staff_Form form = new Update_Staff_Form();
            form.ShowDialog();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            this.Hide();
            Update_Staff_Password update_Staff_Password = new Update_Staff_Password();
            update_Staff_Password.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomePage homePage = new HomePage();
            homePage.ShowDialog();
        }
    }
}
