using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThreeLayerDemo2.Core;

namespace ThreeLayerDemo2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private bool CheckOpenningForm(Form f) { 
            bool check = false;
            foreach (var openedForm in windowsToolStripMenuItem.DropDownItems)
                if (openedForm.ToString() == f.Text)
                {
                    check = true;
                    break;
                }
            return check;
        }
        public void RemoveWindows(String text) {
            foreach (var item in windowsToolStripMenuItem.DropDownItems) {
                if (item.ToString().Equals(text))
                {
                    windowsToolStripMenuItem.DropDownItems.Remove((ToolStripItem)item);
                    break;
                } 
            }
        }
        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInsert f = new frmInsert();
            if (!CheckOpenningForm(f))
            {
                f.MdiParent = this;
                windowsToolStripMenuItem.DropDownItems.Add(f.Text);
                f.Show();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmUpdate f = new frmUpdate();
            if (!CheckOpenningForm(f))
            {
                f.MdiParent = this;
                windowsToolStripMenuItem.DropDownItems.Add(f.Text);
                f.Show();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(windowsToolStripMenuItem.DropDownItems.Count > 0)
            {
                MessageBox.Show("Please close all the opening windows", "information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.Dispose();
        }

        private void insertOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInsertOrder f = new frmInsertOrder();
            f.MdiParent = this;
            f.Show();
        }

        private void updateOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateOrder f = new UpdateOrder();
            if (!CheckOpenningForm(f))
            {
                f.MdiParent = this;
                windowsToolStripMenuItem.DropDownItems.Add(f.Text);
                f.Show();
            }
        }
    }
}
