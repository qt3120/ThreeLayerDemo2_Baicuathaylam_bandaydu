using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeLayerDemo2.Core.DAL;
using ThreeLayerDemo2.Core.Entity;

namespace ThreeLayerDemo2.Core.BUS
{
    class OrderDetailBUS
    {
        OrderDetailDAL orderDetailDAL = new OrderDetailDAL();
        public OrderDetailBUS() { 
        }
        public Boolean InsertOrderDetail(OrderDetail o) {
            Boolean success = orderDetailDAL.InsertOrderDetail(o);
            return success;
        }
        public List<OrderDetail> GetOrderDetails() {
            List<OrderDetail> list = this.orderDetailDAL.GetOrderDetails();
            return list;
        }
        public bool CheckOrderDetailID(int id) {
            List<OrderDetail> list = this.GetOrderDetails();
            var check = list.FindAll(p => p.OrderID == id);
            if (check.Count > 0)
                return true;
            else
                return false;
        }
        public OrderDetail GetOrderDetailByID(int id) {
            List<OrderDetail> list = this.orderDetailDAL.GetOrderDetails();
            return list.Find(p => p.OrderID == id);
        }
        public bool DeleteOrderDetail(int id) {
            return this.orderDetailDAL.DeleteOrderDetail(id);
        }
        public bool UpdateOrderDetail(OrderDetail p) {
            return orderDetailDAL.UpdateOrderDetail(p);
        }
    }
}
