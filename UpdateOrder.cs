using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreeLayerDemo2
{
    public partial class UpdateOrder : Form
    {
        public UpdateOrder()
        {
            InitializeComponent();
        }

        private void UpdateOrder_Load(object sender, EventArgs e)
        {
            dataGridViewod.DataSource = bindingSource;
            GetData("select * from Product");
            dataGridView2.DataSource = bindingSource;
            GetData("select * from OrderDetail");

        }
        private BindingSource bindingSource = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        private String connectionString = "Integrated Security=SSPI;" + "Initial Catalog=Final ;Data Source=localhost";
        private void GetData(string cmd)
        {
            try
            {
                dataAdapter = new SqlDataAdapter(cmd, connectionString);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                DataTable table = new DataTable(); 
                dataAdapter.Fill(table);
                bindingSource.DataSource = table;
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GetData(dataAdapter.SelectCommand.CommandText);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
