using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeLayerDemo2.Core.Entity
{
    class Product
    {
        [DisplayName("Product ID")]
        public int ProductID { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [DisplayName("Product Description")]
        public string ProductDescriptions { get; set; }
        [DisplayName("Quantity")]
        public int Quantity { get; set; }
        [DisplayName("Price")]
        public Decimal Price { get; set; }
        [DisplayName("Category ID")]
        public int CategoryID { get; set; }
        public Category _Category { get; set; }
        
        public Product() {
            this.ProductID = 0;
            this.ProductName = "";
            this.ProductDescriptions = "";
            this.CategoryID = 0;
            this.Price = 0;
            this.Quantity = 0;
            this._Category = new Category();
        }
        public String ToString() => this.ProductName;
    }
}
