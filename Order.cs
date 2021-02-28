using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeLayerDemo2.Core.Entity
{
    class Order
    {
        public int OrderID { get; set; }
        public DateTime PurchaseDate { get; set; }

        public List<OrderDetail> ListOrderDetail { get; set; }
        public Order()
        {
            this.OrderID = 0;
            this.PurchaseDate = DateTime.Now;
            ListOrderDetail = new List<OrderDetail>();
        }
    }
}
