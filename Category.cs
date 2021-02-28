using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeLayerDemo2.Core.Entity
{
    class Category
    {
        public int CategoryID { get; set; }
        public String CategoryName { get; set; }
        public Category() {
            this.CategoryID = 0;
            this.CategoryName = "";
        }
        public String ToString() => this.CategoryName;
    }
}
