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
    public partial class frmInsert : Form
    {
        List<Product> listProducts = new List<Product>();
        ProductBUS productBUS = new ProductBUS();
        public frmInsert()
        {
            InitializeComponent();
        }
        private void LoadComboBox() { 
            List<Category> list = new CategoryBUS().GetCategories();
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "CategoryName";
            comboBox1.ValueMember = "CategoryID";
        }
        private void frmInsert_Load(object sender, EventArgs e)
        {
            this.LoadComboBox();
        }
        private void ResetForm() {
            this.LoadComboBox();
            txtDescription.Text = "";
            txtProductName.Text = "";
            txtProductID.Text = "";
            listProducts.Clear();
            dataGridView1.DataSource = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.CategoryID = int.Parse(comboBox1.SelectedValue.ToString());
            p.ProductName = txtProductName.Text;
            p.ProductID = int.Parse(txtProductID.Text);
            p.ProductDescriptions = txtDescription.Text;
            var found = listProducts.FindAll(product => product.ProductID == p.ProductID);
            if (found.Count > 0)
            {
                MessageBox.Show("Duplicate Product ID. Try again!!!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProductID.Focus();
                txtProductID.SelectAll();
            }
            else if (productBUS.CheckProductID(p.ProductID)) {
                MessageBox.Show("There are " + p.ProductID.ToString() + " in the database.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else{
                listProducts.Add(p);
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = listProducts;
                this.dataGridView1.Refresh();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var p in listProducts) {
                if(!productBUS.InsertProduct(p))
                {
                    MessageBox.Show("Error to insert " + p.ProductName);
                    break;
                }
            }
            this.ResetForm();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmInsertCategory f = new frmInsertCategory();
            f.ShowDialog();
            this.LoadComboBox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listProducts.Count > 0)
            {
                if (dataGridView1.CurrentCell.RowIndex == -1)
                {
                    MessageBox.Show("Hey, choose the row that you want to delete");
                    dataGridView1.Focus();
                    dataGridView1.Select();
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure?", "Question",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        int rowIndex = dataGridView1.CurrentCell.RowIndex;
                        listProducts.RemoveAt(rowIndex);
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = listProducts;
                        dataGridView1.Refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Enter some data", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProductName.Focus();
                txtProductName.SelectAll();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmInsert_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain fMain = (frmMain)this.MdiParent;
            fMain.RemoveWindows(this.Text);
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
