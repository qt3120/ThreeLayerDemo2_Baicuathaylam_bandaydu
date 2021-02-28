using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThreeLayerDemo2.Core.BUS;
using ThreeLayerDemo2.Core.Entity;

namespace ThreeLayerDemo2
{
    public partial class frmUpdate : Form
    {
        ProductBUS productBUS = new ProductBUS();
        CategoryBUS categoryBUS = new CategoryBUS();
        List<Product> listProducts = new List<Product>();
        public frmUpdate()
        {
            InitializeComponent();
            this.LoadComboBox();
            txtProductID.Enabled = false;
        }
        private void LoadComboBox() { 
            List<Category> list = new CategoryBUS().GetCategories();
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "CategoryName";
            comboBox1.ValueMember = "CategoryID";
        }
        private void ResetForm() {
            this.LoadComboBox();
            txtDescriptions.Text = "";
            txtName.Text = "";
            txtProductID.Text = "";
            txtSearch.Text = "";
            listProducts.Clear();
            dataGridView1.DataSource = null;
        }
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return) {
                listProducts = productBUS.GetProductByName(txtSearch.Text);
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = listProducts;
                this.dataGridView1.Refresh();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (listProducts.Count > 0) {
                int index = dataGridView1.CurrentCell.RowIndex;
                Product p = listProducts[index];
                comboBox1.SelectedValue = p.CategoryID;
                txtName.Text = p.ProductName;
                txtDescriptions.Text = p.ProductDescriptions;
                txtProductID.Text = p.ProductID.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listProducts.Count > 0) {
                DialogResult result = MessageBox.Show("Are you sure?", "Question",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int index = dataGridView1.CurrentCell.RowIndex;
                    Product p = listProducts[index];
                    productBUS.DeleteProduct(p.ProductID);
                    MessageBox.Show("Delete Product ID=" + p.ProductID.ToString() + " successfully",
                        "Information");
                    listProducts.RemoveAt(index);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = listProducts;
                    dataGridView1.Refresh();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Question",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Product p = new Product();
                p.ProductID = int.Parse(txtProductID.Text);
                p.CategoryID = (int)comboBox1.SelectedValue;
                p.ProductName = txtName.Text;
                p.ProductDescriptions = txtDescriptions.Text;
                if (productBUS.UpdateProduct(p))
                {
                    MessageBox.Show("Updated!", "information");
                    this.ResetForm();
                }
            }
        }

        private void frmUpdate_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
