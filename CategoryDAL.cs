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
    class CategoryDAL
    {
        DBConnection conn = null;
        public CategoryDAL() {
            conn = new DBConnection("localhost", "Products");
        }
        public List<Category> GetCategories() {
            List<Category> list = new List<Category>();
            string querry = "Select *from Category";
            DataTable table = conn.executeSelectQuery(querry, null);
            foreach (DataRow dr in table.Rows)
            {
                Category c = new Category();
                c.CategoryID = int.Parse(dr["CategoryID"].ToString());
                c.CategoryName = dr["CategoryName"].ToString();
                list.Add(c);
            }
            return list;
        }
        public Category GetCategoryByID(int cateID) {
            List<Category> list = this.GetCategories();
            return list.Find(cate => cate.CategoryID == cateID);
        }

        public Boolean InsertCategory(Category c) {
            string cmd = "INSERT INTO Category(CategoryID, CategoryName) " +
                " VALUES(@id, @name)";
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = c.CategoryID;
            sqlParams[1] = new SqlParameter("@name", SqlDbType.NVarChar);
            sqlParams[1].Value = c.CategoryName;
            return this.conn.executeInsertQuery(cmd, sqlParams);
        }
    }
}
