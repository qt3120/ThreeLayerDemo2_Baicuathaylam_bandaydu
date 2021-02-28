using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeLayerDemo2.Core.DAL;
using ThreeLayerDemo2.Core.Entity;

namespace ThreeLayerDemo2.Core.BUS
{
    class OrderBUS
    {
        OrderDAL orderDAL = new OrderDAL();
        public OrderBUS() { }
        public List<Order> GetOrders() {
            return orderDAL.GetOrders();
        }
        public Boolean InsertOrder(Order c) {
            return orderDAL.InsertOrder(c);
        }
    }
}
