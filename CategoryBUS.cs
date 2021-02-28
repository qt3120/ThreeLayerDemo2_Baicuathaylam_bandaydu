using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeLayerDemo2.Core.DAL;
using ThreeLayerDemo2.Core.Entity;

namespace ThreeLayerDemo2.Core.BUS
{
    class CategoryBUS
    {
        CategoryDAL categoryDAL = new CategoryDAL();
        public CategoryBUS() { 
        
        }
        public List<Category> GetCategories() {
            return categoryDAL.GetCategories(); 
        }
        public Boolean InsertCategory(Category c) {
            return categoryDAL.InsertCategory(c);
        }
    }
}
