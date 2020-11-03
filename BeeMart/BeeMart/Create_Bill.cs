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
    public partial class Create_Bill : Form
    {
        string cnn_str = ConfigurationManager.ConnectionStrings["BeeMartConnectionString"].ConnectionString;
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        List<List_Product> Products_list = new List<List_Product>();
        DataTable table = new DataTable();


        public Create_Bill()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Create_Bill_Load(object sender, EventArgs e)
        {
            load_Product();
            table.Columns.Add("ProductID");
            table.Columns.Add("ProductName");
            table.Columns.Add("Quantity");
            table.Columns.Add("Price");
            table.Columns.Add("Total");
        }

        private void txtAddPro_Click(object sender, EventArgs e)
        {
            if(check_numeric(txtQuantity.Text))
            {
                int productID = Int32.Parse(dgvProduct.CurrentRow.Cells[0].Value.ToString());
                string productName = dgvProduct.CurrentRow.Cells[1].Value.ToString();
                int price = Int32.Parse(dgvProduct.CurrentRow.Cells[2].Value.ToString());
                int productQuantity = Int32.Parse(txtQuantity.Text);
                //Thêm vào product
                List_Product list_Product = new List_Product(productID, productName, productQuantity, price);
                Products_list.Add(list_Product);
                DataRow row = table.NewRow();
                foreach (List_Product list in Products_list)
                {
                    row["ProductID"] = list.get_productID().ToString();
                    row["ProductName"] = list.get_productName().ToString();
                    row["Quantity"] = list.get_productQuantity().ToString();
                    row["Price"] = list.get_price().ToString();
                    row["Total"] = list.get_total().ToString();
                }
                table.Rows.Add(row);
                dgvBill.DataSource = table;
            }    
        }

        private void btnDelProduct_Click(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {

        }

        private void load_Product()
        {
            SqlConnection cnn = new SqlConnection(cnn_str);
            da = new SqlDataAdapter("SELECT * FROM Product", cnn);
            da.Fill(ds, "tblProduct");
            if (ds.Tables["tblProduct"].Rows.Count > 0)
            {
                dgvProduct.DataSource = ds.Tables["tblProduct"];
            }
            else
            {

            }
        }

        private bool check_numeric(string PhoneNum)
        {
            int test;
            return int.TryParse(PhoneNum, out test);
        }
    }
}
