using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeLayerDemo2.Core.Entity
{
    class OrderDetail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }

        public OrderDetail() {
            this.OrderID = 0;
            this.ProductID = 0;
            this.Quantity = 0;
            this.Price = 0;
        }
        
    }
}
