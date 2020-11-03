using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BeeMart
{
    class List_Product
    {
        int productID;
        string productName;
        int productQuantity;
        int price;
        int total;

        public List_Product(int productID, string productName, int productQuantity, int price)
        {
            this.productID = productID;
            this.productName = productName;
            this.productQuantity = productQuantity;
            this.price = price;
            this.total = productQuantity * price;
        }

        //Method set
        public void set_productID(int productID)
        {
            this.productID = productID;
        }

        public void set_productName(string productName)
        {
            this.productName = productName;
        }

        public void set_productQuantity(int productQuantity)
        {
            this.productQuantity = productQuantity;
        }

        public void set_price(int price)
        {
            this.price = price;
        }
        public void set_total(int total)
        {
            this.total = total;
        }

        //Method get
        public int get_productID()
        {
            return this.productID;
        }
        public string get_productName()
        {
            return this.productName;
        }
        public int get_productQuantity()
        {
            return this.productQuantity;
        }
        public int get_price()
        {
            return this.price;
        }
        public int get_total()
        {
            return this.total;
        }
    }
}
