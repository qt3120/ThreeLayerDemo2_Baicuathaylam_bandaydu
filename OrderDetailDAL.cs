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
    class OrderDetailDAL
    {

        private DBConnection conn = null;
        public OrderDetailDAL() {
            this.conn = new DBConnection("localhost", "Products");
        }
        public bool InsertOrderDetail(OrderDetail o) {
            string cmd = "INSERT INTO OrderDetail(ProductID, OrderID, Quantity, Price) " +
                " VALUES(@productid, @orderid, @quantity, @price)";
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@productid", SqlDbType.Int);
            sqlParams[0].Value = o.ProductID;
            sqlParams[1] = new SqlParameter("@orderid", SqlDbType.Int);
            sqlParams[1].Value = o.OrderID;
            sqlParams[2] = new SqlParameter("@quantity", SqlDbType.Int);
            sqlParams[2].Value = o.Quantity;
            sqlParams[3] = new SqlParameter("@price", SqlDbType.Decimal);
            sqlParams[3].Value = o.Price;
            return this.conn.executeInsertQuery(cmd, sqlParams);
        }
        public bool UpdateOrderDetail(OrderDetail p) {
            string cmd = "UPDATE OrderDetail SET ProductID=@id" + 
                "Price=@price, Quantity=@quantity" +
                " WHERE OrderID=@orderid";
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = p.ProductID;
            sqlParams[1] = new SqlParameter("@price", SqlDbType.Decimal);
            sqlParams[1].Value = p.Price;
            sqlParams[2] = new SqlParameter("@quantity", SqlDbType.Int);
            sqlParams[2].Value = p.Quantity;
            sqlParams[3] = new SqlParameter("@orderid", SqlDbType.Int);
            sqlParams[3].Value = p.OrderID;
            return this.conn.executeUpdateQuery(cmd, sqlParams);
        }
        public List<OrderDetail> GetOrderDetails() {
            string cmd = "Select *from OrderDetail";
            DataTable table = this.conn.executeSelectQuery(cmd, null);
            List<OrderDetail> list = new List<OrderDetail>();
            foreach (DataRow dr in table.Rows)
            {
                OrderDetail  p = new OrderDetail();
                p.ProductID = int.Parse(dr["ProductID"].ToString());
                p.OrderID = int.Parse(dr["ProductName"].ToString());
                p.Price = Decimal.Parse(dr["Price"].ToString());
                p.Quantity = int.Parse(dr["Quantity"].ToString());
                list.Add(p);
            }
            return list;
        }
        public List<OrderDetail> Find(int orderId) {
            List<OrderDetail> list = this.GetOrderDetails();
            return list.FindAll(p => p.OrderID == orderId);
        }
        public bool DeleteOrderDetail(int id)
        {
            String cmd = "Delete from OrderDetail WHERE OrderID = @id";
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = id;
            return this.conn.executeInsertQuery(cmd, sqlParams);
        }
    }
}
