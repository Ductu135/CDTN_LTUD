using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeeMart
{
    public partial class Add_Product : Form
    {
        string cnn_str = ConfigurationManager.ConnectionStrings["BeeMartConnectionString"].ConnectionString;
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        public Add_Product()
        {
            InitializeComponent();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            //check duplicate
            
            string ProductName = txtProductName.Text;
            decimal Price = numPrice.Value;
            string CategoryID = txtCategoryID.Text;
            if(ProductName != "" && Price > 0 && CategoryID != "")
            {
                SqlConnection cnn = new SqlConnection(cnn_str);
                SqlCommand cmd = new SqlCommand();
                string sql = "SELECT * FROM Product WHERE ProductName = @ProductName AND Price = @Price AND Pro_CategoryID = @CategoryID";
                SqlParameter pa1 = new SqlParameter("@ProductName", ProductName);
                SqlParameter pa2 = new SqlParameter("@Price", Price);
                SqlParameter pa4 = new SqlParameter("@CategoryID", CategoryID);
                cmd.Connection = cnn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(pa1);
                cmd.Parameters.Add(pa2);
                cmd.Parameters.Add(pa4);
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "tblProduct_Search");
                if (ds.Tables["tblProduct_Search"].Rows.Count <= 0)
                {
                    SqlCommand cmdAddProduct = new SqlCommand();
                    cmdAddProduct.Connection = cnn;
                    cmdAddProduct.CommandText = "add_product";
                    cmdAddProduct.CommandType = CommandType.StoredProcedure;
                    SqlParameter pAdd1 = new SqlParameter("@ProductName", ProductName);
                    SqlParameter pAdd2 = new SqlParameter("@Price", Price);
                    SqlParameter pAdd3 = new SqlParameter("@CategoryID", CategoryID);
                    cmdAddProduct.Parameters.Add(pAdd1);
                    cmdAddProduct.Parameters.Add(pAdd2);
                    cmdAddProduct.Parameters.Add(pAdd3);
                    da.SelectCommand = cmdAddProduct;
                    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder();
                    da.Update(ds, "tblProduct_Search");
                    da.Fill(ds, "tblProduct_Search");
                    if(ds.Tables["tblProduct_Search"].Rows.Count > 0)
                    {
                        MessageBox.Show("Thêm thành công");
                    }
                    else
                    {
                        MessageBox.Show("Thêm không thành công");
                    }    
                }
            }   
            else
            {
                MessageBox.Show("Giá và Số lượng phải lớn hơn 0");
            }                
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Product_Management product_Management = new Product_Management();
            this.Hide();
            product_Management.ShowDialog();
        }

        private void Add_Product_Load(object sender, EventArgs e)
        {

        }
    }
}
