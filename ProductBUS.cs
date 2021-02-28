using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeLayerDemo2.Core.DAL;
using ThreeLayerDemo2.Core.Entity;

namespace ThreeLayerDemo2.Core.BUS
{
    class ProductBUS
    {
        ProductDAL productDAL = new ProductDAL();
        public ProductBUS() { 
        }
        public Boolean InsertProduct(Product p) {
            Boolean success = productDAL.InsertProduct(p);
            return success;
        }
        public List<Product> GetProducts() {
            List<Product> list = this.productDAL.GetProducts();
            return list;
        }
        public bool CheckProductID(int id) {
            List<Product> list = this.GetProducts();
            var check = list.FindAll(p => p.ProductID == id);
            if (check.Count > 0)
                return true;
            else
                return false;
        }
        public Product GetProductByID(int id) {
            List<Product> list = this.GetProducts();
            return list.Find(p => p.ProductID == id);
        }
        public List<Product> GetProductByCategory(int catid) {
            List<Product> list = this.GetProducts();
            return list.FindAll(p => p.CategoryID == catid);
        }
        public List<Product> GetProductByName(string name) {
            List<Product> list = this.GetProducts();
            return list.FindAll(p => p.ProductName.Contains(name));
        }
        public bool DeleteProduct(int id) {
            return productDAL.DeleteProduct(id);
        }
        public bool UpdateProduct(Product p) {
            return productDAL.UpdateProduct(p);
        }
    }
}
