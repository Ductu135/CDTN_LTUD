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
    public partial class Product_Management : Form
    {
        string cnn_str = ConfigurationManager.ConnectionStrings["BeeMartConnectionString"].ConnectionString;
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        public Product_Management()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateProduct updateProduct = new UpdateProduct();
            this.Hide();
            updateProduct.ShowDialog();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            Add_Category add_Category = new Add_Category();
            this.Hide();
            add_Category.ShowDialog();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            Add_Product add_Product = new Add_Product();
            this.Hide();
            add_Product.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.ShowDialog();
            this.Hide();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Product_Management_Load(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(cnn_str);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "show_product";
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds, "tblProduct");
            if(ds.Tables["tblProduct"].Rows.Count > 0)
            {
                dgvProduct.DataSource = ds.Tables["tblProduct"];
            }
        }
    }
}
