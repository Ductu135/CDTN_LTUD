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
        DataTable dataTable = new DataTable();
        DataTable table_deBill = new DataTable();

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
                //Thêm product
                List_Product list_Product = new List_Product(productID, productName, productQuantity, price);
                //Add to List
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

                //Calculate Total of Bill
                cal_billTotal();
            }    
        }

        private void btnDelProduct_Click(object sender, EventArgs e)
        {
            string productID = dgvBill.CurrentRow.Cells["ProductID"].Value.ToString();
            for(int i = 0; i < table.Rows.Count; i++)
            {
                if(table.Rows[i]["ProductID"].ToString() == productID)
                {
                    DataRow row = table.Rows[i];
                    table.Rows.Remove(row);
                }    
            }
            dgvBill.DataSource = table;
            cal_billTotal();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            DateTime DOP = DateTime.Now;
            string BCusID = txtCustomerID.Text;
            string BStaffID = txtStaffID.Text;
            int Total = Int32.Parse(txtTotal.Text);
            if (DOP != null && check_numeric(BCusID) && check_numeric(BStaffID))
            {
                int Bill_CusID = Int32.Parse(BCusID);
                int Bill_StaffID = Int32.Parse(BStaffID);
                add_Bill(DOP, Bill_CusID, Bill_StaffID, Total);
                if(dataTable.Rows.Count > 0)
                {
                    for(int i = 0; i < table.Rows.Count; i++)
                    {
                        int Bill_ProductID = Int32.Parse(table.Rows[i][0].ToString());
                        int Quantity = Int32.Parse(table.Rows[i][2].ToString());
                        int Price = Int32.Parse(table.Rows[i][3].ToString());
                        string Bill_ProductName = table.Rows[i][1].ToString();

                        add_Bill_Detail(Bill_ProductID, Quantity, Price, Bill_ProductName);
                    }    
                   
                    if(table_deBill.Rows.Count > 0)
                    {
                        MessageBox.Show("Tạo hóa đơn thành công");
                    }    
                    else
                    {
                        MessageBox.Show("Không tạo được hóa đơn");
                    }    
                }    
            }    
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

        public int cal_billTotal()
        {
            int bill_Total = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                bill_Total += Int32.Parse(table.Rows[i]["Total"].ToString());
            }
            txtTotal.Text = bill_Total.ToString();
            return bill_Total;
        }

        public void add_Bill(DateTime DateOfPrint, int CustomerID, int StaffID, int Total)
        {
            SqlConnection cnn = new SqlConnection(cnn_str);
            SqlCommand cmd = new SqlCommand();
            SqlParameter pa_DOP = new SqlParameter("@DateOfPrint", DateOfPrint);
            SqlParameter pa_CustomerID = new SqlParameter("@Bill_CustomerID", CustomerID);
            SqlParameter pa_StaffID = new SqlParameter("@Bill_StaffID", StaffID);
            SqlParameter pa_Total = new SqlParameter("@Total", Total);
            cmd.CommandText = "create_bill";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            cmd.Parameters.Add(pa_DOP);
            cmd.Parameters.Add(pa_CustomerID);
            cmd.Parameters.Add(pa_StaffID);
            cmd.Parameters.Add(pa_Total);
            da.SelectCommand = cmd;
            SqlCommandBuilder sql = new SqlCommandBuilder();
            da.Update(dataTable);
            da.Fill(dataTable);
        }

        public void add_Bill_Detail(int Details_ProductID, int Quantity, int Price, string Details_ProductName)
        {
            SqlConnection cnn = new SqlConnection(cnn_str);
            SqlCommand cmd = new SqlCommand();
            SqlParameter pa_Details_ProductID = new SqlParameter("@Details_ProductID", Details_ProductID);
            SqlParameter pa_Quantity = new SqlParameter("@Quantity", Quantity);
            SqlParameter pa_Price = new SqlParameter("@Price", Price);
            SqlParameter pa_Details_ProductName = new SqlParameter("@Details_ProductName", Details_ProductName);
            cmd.CommandText = "add_Bill_Detail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            cmd.Parameters.Add(pa_Details_ProductID);
            cmd.Parameters.Add(pa_Quantity);
            cmd.Parameters.Add(pa_Price);
            cmd.Parameters.Add(pa_Details_ProductName);
            da.SelectCommand = cmd;
            SqlCommandBuilder sql = new SqlCommandBuilder();          
            da.Update(table_deBill);
            da.Fill(table_deBill);
        }
    }
}
