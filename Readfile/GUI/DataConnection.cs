using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;

namespace GUI
{
    public class DataConnection
    {
        private static DataConnection instance;
        private SqlConnection connection;

        public DataConnection()
        {
            // Khởi tạo đối tượng SqlConnection để kết nối tới SQL Server
            string connectionString = "Data Source=LAPTOP-TC1PJ34D\\LONG;Initial Catalog=readfile;Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }

        // Phương thức để trả về đối tượng DataConnection (Singleton Pattern)
        public static DataConnection GetInstance()
        {
            if (instance == null)
            {
                instance = new DataConnection();
            }
            return instance;
        }

        public int handleRecord(string query)
        {
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            int Ma = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            return Ma;
        }

        public void addSqlBuckcopy(DataTable dataTable)
        {
            connection.Open();
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName = "pav_condition";
                foreach (DataColumn column in dataTable.Columns)
                {
                    bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }
                bulkCopy.WriteToServer(dataTable);
            }
            connection.Close();
        }

        public void ExecuteNonQuery(string query)
        {
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public DataTable GetRecords(string query)
        {
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter reader = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            reader.Fill(dataTable);
            return dataTable;
        }
    }
}
