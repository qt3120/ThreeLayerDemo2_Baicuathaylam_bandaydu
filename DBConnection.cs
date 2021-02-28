using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace ThreeLayerDemo2.Core
{
    class DBConnection
    {
        private SqlConnection sqlConnection = null;
        string connectionString = "";
        public DBConnection(string server, string Final) {
            /*this.connectionString = "Data Source=" + server
               + ";Initial Catalog=" + Final + ";"
               + "Integrated Security=true;";*/
           this.connectionString = "Integrated Security=SSPI;" + "Initial Catalog=Final ;Data Source=localhost";
            this.sqlConnection = this.getConnection();
        }
        public SqlConnection getConnection()
        {
            try
            {
                sqlConnection = new SqlConnection(this.connectionString);
                return sqlConnection;
            }
            catch (Exception se)
            {
                MessageBox.Show(se.Message);
                return null;
            }
        }
        public void CloseConnection()
        {
            if (this.sqlConnection != null && this.sqlConnection.State == ConnectionState.Open)
                this.sqlConnection.Close();
        }
        public SqlConnection openConnection()
        {
            if (this.sqlConnection == null)
            {
                this.sqlConnection = this.getConnection();
                sqlConnection.Open();
                return sqlConnection;
            }
            else
            {
                if (this.sqlConnection.State == ConnectionState.Closed)
                {
                    this.sqlConnection.Open();
                    return this.sqlConnection;
                }
                return this.sqlConnection;
            }
        }
        public DataTable executeSelectQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                if (sqlParameter != null)
                    myCommand.Parameters.AddRange(sqlParameter);
                myCommand.ExecuteNonQuery();
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds, "Final");
                dataTable = ds.Tables[0];
                this.CloseConnection();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
                this.CloseConnection();
                return null;
            }
            return dataTable;
        }
        public bool executeInsertQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.InsertCommand = myCommand;
                myCommand.ExecuteNonQuery();
                this.CloseConnection();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                this.CloseConnection();
            }
            return true;
        }
        public bool executeUpdateQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.UpdateCommand = myCommand;
                myCommand.ExecuteNonQuery();
                this.CloseConnection();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                this.CloseConnection();
            }
            return true;
        }
    }
}
