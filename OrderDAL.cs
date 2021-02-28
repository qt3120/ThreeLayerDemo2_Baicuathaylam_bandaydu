using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeLayerDemo2.Core.Entity;

namespace ThreeLayerDemo2.Core.DAL
{
    class OrderDAL
    {
        DBConnection conn = null;
        OrderDetailDAL orderDetailDAL;
        public OrderDAL() {
            conn = new DBConnection("localhost", "Products");
            orderDetailDAL = new OrderDetailDAL();
        }
        public List<Order> GetOrders() {
            List<Order> list = new List<Order>();
            string querry = "Select *from [Order]";
            DataTable table = conn.executeSelectQuery(querry, null);
            foreach (DataRow dr in table.Rows)
            {
                Order o = new Order();
                o.OrderID = int.Parse(dr["OrderID"].ToString());
                o.PurchaseDate = DateTime.Parse(dr["PurchaseDate"].ToString());
                o.ListOrderDetail = orderDetailDAL.Find(o.OrderID);
                list.Add(o);
            }
            return list;
        }

        public Order GetOrderByID(int orderID) {
            List<Order> list = this.GetOrders();
            return list.Find(o => o.OrderID == orderID);
        }

        public Boolean InsertOrder(Order o) {
            string cmd = "INSERT INTO [Order](OrderID, PurchaseDate) " +
                " VALUES(@id, @purchaseDate)";
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = o.OrderID;
            sqlParams[1] = new SqlParameter("@purchaseDate", SqlDbType.DateTime);
            sqlParams[1].Value = o.PurchaseDate;
            bool success = this.conn.executeInsertQuery(cmd, sqlParams);
            if (success)
            {
                foreach (OrderDetail orderDetail in o.ListOrderDetail)
                    this.orderDetailDAL.InsertOrderDetail(orderDetail);
                return true;
            }
            else return false;
        }
    }
}
