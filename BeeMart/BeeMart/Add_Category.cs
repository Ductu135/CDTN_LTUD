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
    public partial class Add_Category : Form
    {
        string cnn_str = ConfigurationManager.ConnectionStrings["BeeMartConnectionString"].ConnectionString;
        DataSet ds = new DataSet();
        public Add_Category()
        {
            InitializeComponent();
        }

        private void Add_Category_Load(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(cnn_str);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Categories", cnn);
            da.Fill(ds, "tblCategories");
            if(ds.Tables["tblCategories"].Rows.Count > 0)
            {
                dgvCategory.DataSource = ds.Tables["tblCategories"];
            }                
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Product_Management product_Management = new Product_Management();
            product_Management.ShowDialog();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string cateName = txtCategoryName.Text;
            SqlConnection cnn = new SqlConnection(cnn_str);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Categories WHERE CategoryName = @cateName";
            SqlParameter pa = new SqlParameter("@cateName", cateName);
            cmd.Parameters.Add(pa);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds, "NewCategory");
            if (ds.Tables["NewCategory"].Rows.Count <= 0)
            {
                SqlCommand cmd_addCate = new SqlCommand();
                cmd_addCate.Connection = cnn;
                cmd_addCate.CommandText = "add_category";
                cmd_addCate.CommandType = CommandType.StoredProcedure;
                SqlParameter pa1 = new SqlParameter("@category_name", cateName);
                cmd_addCate.Parameters.Add(pa1);
                //da = new SqlDataAdapter();
                da.SelectCommand = cmd_addCate;
                SqlCommandBuilder sql = new SqlCommandBuilder();
                da.Update(ds, "NewCategory");
                da.Fill(ds, "NewCategory");
                this.Hide();
            }
            else
            {
                MessageBox.Show("Loại hàng này đã tồn tại");
            }                
        }

        /*private void checkCategory (string cName)
        {       
            string CateName = cName;
            SqlConnection cnn = new SqlConnection(cnn_str);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Categories WHERE CategoryName = @cateName";
            SqlParameter pa = new SqlParameter("@cateName", CateName);
            cmd.Parameters.Add(pa);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "NewCategory");
        }*/
    }
}
