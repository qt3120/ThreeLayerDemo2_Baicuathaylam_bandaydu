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
    public partial class frmInsertCategory : Form
    {
        public frmInsertCategory()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Category c = new Category();
            c.CategoryID = int.Parse(textBox1.Text);
            c.CategoryName = textBox2.Text;
            Boolean success = new CategoryBUS().InsertCategory(c);
            if (!success)
            {
                MessageBox.Show("Can not insert " + textBox1.Text, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Successful", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmInsertCategory_Load(object sender, EventArgs e)
        {

        }
    }
}
