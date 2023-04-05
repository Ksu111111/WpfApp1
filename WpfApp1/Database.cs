using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace workingWithSql
{
    internal class Database
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-CD3JPIF\SQLEXPRESS;Initial Catalog=VegetablesAndFruits;Integrated Security=SSPI");
        private SqlConnection OpenConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
                return connection;
            }
            else
                return null;
        }
        private void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
                connection.Close();            
        }
        public DataTable DatabaseOutput()
        {
            DataTable dataTable = new DataTable();
            connection = OpenConnection();
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select * from Table_f", connection);
                SqlDataReader reader = cmd.ExecuteReader();                
                for (int i = 0; i < reader.FieldCount; i++)                    
                    dataTable.Columns.Add(reader.GetName(i));
                while (reader.Read())
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                        dataRow[i] = reader[i];
                    dataTable.Rows.Add(dataRow);
                }
                reader?.Close();
                CloseConnection();
                return dataTable;
            }
            else
                return null;
        }
        public DataTable OutputAllTitle()
        {
            DataTable dataTable = new DataTable();
            connection = OpenConnection();
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select title_k from Table_f group by title_k", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                for (int i = 0; i < reader.FieldCount; i++)
                    dataTable.Columns.Add(reader.GetName(i));
                while (reader.Read())
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                        dataRow[i] = reader[i];
                    dataTable.Rows.Add(dataRow);
                }
                reader?.Close();
                CloseConnection();
                return dataTable;
            }
            else
                return null;
        }
        public DataTable OutputAllColor()
        {
            DataTable dataTable = new DataTable();
            connection = OpenConnection();
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select color_k from Table_f  group by color_k", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                for (int i = 0; i < reader.FieldCount; i++)
                    dataTable.Columns.Add(reader.GetName(i));
                while (reader.Read())
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                        dataRow[i] = reader[i];
                    dataTable.Rows.Add(dataRow);
                }
                reader?.Close();
                CloseConnection();
                return dataTable;
            }
            else
                return null;
        }
        public object MaximumCaloric()
        {
            connection = OpenConnection();
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select max(caloric_k) from Table_f", connection);
                var result = cmd.ExecuteScalar();
                CloseConnection();
                return result;
            }
            else
                return null;
        }
        public object MinimumCaloric()
        {
            connection = OpenConnection();
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select min(caloric_k) from Table_f", connection);
                var result = cmd.ExecuteScalar();
                CloseConnection();
                return result;
            }
            else
                return null;
        }
        public object AverageCaloric()
        {
            connection = OpenConnection();
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select avg(caloric_k) from Table_f", connection);
                var result = cmd.ExecuteScalar();
                CloseConnection();
                return result;
            }
            else
                return null;
        }
        public object NumberVegetables()
        {
            connection = OpenConnection();
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select count(type_k) from Table_f as T where T.type_k = 'овощ';", connection);
                var result = cmd.ExecuteScalar();
                CloseConnection();
                return result;
            }
            else
                return null;
        }
        public object NumberFruits()
        {
            connection = OpenConnection();
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select count(type_k) from Table_f as T where T.type_k = 'фрукт';", connection);
                var result = cmd.ExecuteScalar();
                CloseConnection();
                return result;
            }
            else
                return null;
        }
        public object CountVegetablesAndFruits(string color)
        {
            connection = OpenConnection();
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand($"select count(id) from Table_f where color_k = '{color}'", connection);
                var result = cmd.ExecuteScalar();
                CloseConnection();
                return result;
            }
            else
                return null;
        }
        public DataTable PrintCountVAF()
        {
            DataTable dataTable = new DataTable();
            connection = OpenConnection();
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select color_k, count(id) from Table_f group by color_k;", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                for (int i = 0; i < reader.FieldCount; i++)
                    dataTable.Columns.Add(reader.GetName(i));
                while (reader.Read())
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                        dataRow[i] = reader[i];
                    dataTable.Rows.Add(dataRow);
                }
                reader?.Close();
                CloseConnection();
                return dataTable;
            }
            else
                return null;
        }
        public DataTable PrintLessCaloric(int caloric)
        {
            DataTable dataTable = new DataTable();
            connection = OpenConnection();
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand($"select title_k, caloric_k from Table_f where caloric_k < {caloric};", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                for (int i = 0; i < reader.FieldCount; i++)
                    dataTable.Columns.Add(reader.GetName(i));
                while (reader.Read())
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                        dataRow[i] = reader[i];
                    dataTable.Rows.Add(dataRow);
                }
                reader?.Close();
                CloseConnection();
                return dataTable;
            }
            else
                return null;
        }
        public DataTable PrintMoreCaloric(int caloric)
        {
            DataTable dataTable = new DataTable();
            connection = OpenConnection();
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand($"select title_k, caloric_k from Table_f where caloric_k > {caloric};", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                for (int i = 0; i < reader.FieldCount; i++)
                    dataTable.Columns.Add(reader.GetName(i));
                while (reader.Read())
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                        dataRow[i] = reader[i];
                    dataTable.Rows.Add(dataRow);
                }
                reader?.Close();
                CloseConnection();
                return dataTable;
            }
            else
                return null;
        }
        public DataTable PrintBetweenCaloric(int caloric1, int caloric2)
        {
            DataTable dataTable = new DataTable();
            connection = OpenConnection();
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand($"select title_k, caloric_k  from Table_f where caloric_k between {caloric1} and {caloric2};", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                for (int i = 0; i < reader.FieldCount; i++)
                    dataTable.Columns.Add(reader.GetName(i));
                while (reader.Read())
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                        dataRow[i] = reader[i];
                    dataTable.Rows.Add(dataRow);
                }
                reader?.Close();
                CloseConnection();
                return dataTable;
            }
            else
                return null;
        }
        public DataTable Print()
        {
            DataTable dataTable = new DataTable();
            connection = OpenConnection();
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select title_k from Table_f where color_k = 'желтый' or color_k = 'красный'", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                for (int i = 0; i < reader.FieldCount; i++)
                    dataTable.Columns.Add(reader.GetName(i));
                while (reader.Read())
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                        dataRow[i] = reader[i];
                    dataTable.Rows.Add(dataRow);
                }
                reader?.Close();
                CloseConnection();
                return dataTable;
            }
            else
                return null;
        }
    }
}
