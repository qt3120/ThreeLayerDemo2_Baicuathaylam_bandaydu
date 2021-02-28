using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThreeLayerDemo2.Core.BUS;
using ThreeLayerDemo2.Core.Entity;

namespace ThreeLayerDemo2
{
    public partial class frmInsertOrder : Form
    {
        CategoryBUS categoryBUS = new CategoryBUS();
        ProductBUS productBUS = new ProductBUS();
        OrderDetailBUS orderDetailBUS = new OrderDetailBUS();
        OrderBUS orderBUS = new OrderBUS();
        List<Product> listProducts;
        List<Category> listCategory;
        Order order = new Order();
        public frmInsertOrder()
        {
            InitializeComponent();
            listCategory = categoryBUS.GetCategories();
        }
        private void BindCategoryComboBox() {
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "CategoryName";
            cbCategory.ValueMember = "CategoryID";
            if (cbCategory.Items.Count > 0)
            {
                cbCategory.SelectedIndex = 0;
            }
        }
        private void BindProductComboBox() {
            if (cbCategory.SelectedIndex > -1)
            {
                int catid = int.Parse(cbCategory.SelectedValue.ToString());
                listProducts = productBUS.GetProductByCategory(catid);
                cbProduct.DataSource = listProducts;
                cbProduct.DisplayMember = "ProductName";
                cbProduct.ValueMember = "ProductID";
            }
        }
        private void InsertOrder_Load(object sender, EventArgs e)
        {
            this.BindCategoryComboBox();
            this.BindProductComboBox();
            order.OrderID = new Random(500000).Next();
            order.PurchaseDate = dateTimePicker1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int pid = int.Parse(cbProduct.SelectedValue.ToString());
            int quantity = int.Parse(txtQuantity.Text);
            int index = order.ListOrderDetail.FindIndex(o => o.ProductID == pid);
            if (index>= 0)
            {
                OrderDetail od = order.ListOrderDetail[index];
                od.Quantity += quantity;
            }
            else
            {
                Product p = productBUS.GetProductByID(pid);
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderID = order.OrderID;
                orderDetail.ProductID = p.ProductID;
                orderDetail.Price = p.Price;
                orderDetail.Quantity = quantity;

                order.ListOrderDetail.Add(orderDetail);
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = order.ListOrderDetail;
            dataGridView1.Refresh();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.BindProductComboBox();
            }
            catch (Exception se) {
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dataGridView1.CurrentCell.RowIndex;
                if (index > -1)
                    if (order.ListOrderDetail.Count > 0)
                        order.ListOrderDetail.RemoveAt(index);
                    else
                        MessageBox.Show("There is no data to delete", "Error");
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = order.ListOrderDetail;
                this.dataGridView1.Refresh();
            }
            catch (Exception se) { 
            
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                order.PurchaseDate = this.dateTimePicker1.Value;
                bool success = orderBUS.InsertOrder(order);
                if (success)
                {
                    MessageBox.Show("Insert successfully", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    order = new Order();
                    order.OrderID = new Random(1000).Next();
                    order.PurchaseDate = dateTimePicker1.Value;
                    dataGridView1.DataSource = null; 
                }
            }
            catch (Exception se) {
                MessageBox.Show(se.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
