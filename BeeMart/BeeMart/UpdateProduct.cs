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
    public partial class UpdateProduct : Form
    {
        string cnn_str = ConfigurationManager.ConnectionStrings["BeeMartConnectionString"].ConnectionString;
        DataSet ds = new DataSet();
        SqlDataAdapter da; 
        public UpdateProduct()
        {
            InitializeComponent();
        }

        private void UpdateProduct_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //check duplicated
            string ProductID = txtProductID.Text;
            string ProductName = txtProductName.Text;
            decimal CategoryID = numCategoryID_Pro.Value;
            decimal Price = numPrice.Value;
            checkDuplicate(ProductID);
            if (checkDuplicate(ProductID))
            {
                SqlConnection cnn = new SqlConnection(cnn_str);
                SqlCommand cmd_update = new SqlCommand();
                SqlParameter pa_ProID = new SqlParameter("@ProID", ProductID);
                SqlParameter pa_ProName = new SqlParameter("@ProName", ProductName);
                SqlParameter pa_Price = new SqlParameter("@Price", Price);
                SqlParameter pa_CateID = new SqlParameter("@CateID", CategoryID);
                cmd_update.Connection = cnn;
                cmd_update.CommandText = "Update_Product";
                cmd_update.CommandType = CommandType.StoredProcedure;
                cmd_update.Parameters.Add(pa_ProID);
                cmd_update.Parameters.Add(pa_ProName);
                cmd_update.Parameters.Add(pa_Price);
                cmd_update.Parameters.Add(pa_CateID);
                SqlCommandBuilder sqlCommand = new SqlCommandBuilder();
                da.SelectCommand = cmd_update;
                da.Update(ds, "tblProduct");
                da.Fill(ds, "tblProduct");
                if(ds.Tables["tblProduct"].Rows.Count > 0)
                {
                    MessageBox.Show("Cập nhật thành công");
                    Product_Management product_Management = new Product_Management();
                    product_Management.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại");
                }    
            }   
            else
            {
                MessageBox.Show("Không tồn tại sản phẩm này");
            }    

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Product_Management product_Management = new Product_Management();
            this.Hide();
            product_Management.ShowDialog();
        }

        private bool checkDuplicate(string ProductID)
        {
            bool result = false;
            SqlConnection cnn = new SqlConnection(cnn_str);
            SqlCommand cmd = new SqlCommand();
            string sql = "SELECT * FROM Product WHERE ProductID = @ProductID";
            SqlParameter pa = new SqlParameter("@ProductID", ProductID);
            cmd.Parameters.Add(pa);
            cmd.Connection = cnn;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds, "tblProduct");
            if(ds.Tables["tblProduct"].Rows.Count > 0)
            {
                return result = true;
            }
            else
            {
                return result;
            }
        }
    }
}
