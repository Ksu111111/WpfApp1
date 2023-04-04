using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace workingWithSql
{
    internal class Database
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-CD3JPIF\SQLEXPRESS;Initial Catalog=VegetablesAndFruits;Integrated Security=SSPI");
        public SqlConnection OpenConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
                return connection;
            }
            else
                return null;
        }
        public void CloseConnection()
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
        public void OutputAllTitle()
        {
            Console.WriteLine("-Отображение всех названий овощей и фруктов-");
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select title_k from Table_f group by title_k", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    for(int i = 0; i < reader.FieldCount; i++)
                        Console.WriteLine(String.Format("\t|{0,11}|", reader[i]));            
                Console.WriteLine();
                reader?.Close();
            }
            else
                Console.WriteLine("Соединение с бд не установленно");
            Console.WriteLine();
        }
        public void OutputAllColor()
        {
            Console.WriteLine("-Отображение всех цветов-");
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select color_k from Table_f  group by color_k", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    for (int i = 0; i < reader.FieldCount; i++)
                        Console.WriteLine(String.Format("\t|{0,11}|", reader[i]));
                Console.WriteLine();
                reader?.Close();
            }
            else
                Console.WriteLine("Соединение с бд не установленно");
            Console.WriteLine();
        }
        public void MaximumCaloric()
        {
            Console.WriteLine("-Показать максимальную калорийность-");
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select max(caloric_k) from Table_f", connection);
                Console.WriteLine($"max = {cmd.ExecuteScalar()}");
            }
            else
                Console.WriteLine("Соединение с бд не установленно");
            Console.WriteLine();
        }
        public void MinimumCaloric()
        {
            Console.WriteLine("-Показать минимальную калорийность-");
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select min(caloric_k) from Table_f", connection);
                Console.WriteLine($"min = {cmd.ExecuteScalar()}");
            }
            else
                Console.WriteLine("Соединение с бд не установленно");
            Console.WriteLine();
        }
        public void AverageCaloric()
        {
            Console.WriteLine("-Показать среднюю калорийность-");
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select avg(caloric_k) from Table_f", connection);
                Console.WriteLine($"avg = {cmd.ExecuteScalar()}");
            }
            else
                Console.WriteLine("Соединение с бд не установленно");
            Console.WriteLine();
        }
        public void NumberVegetables()
        {
            Console.WriteLine("-Показать количество овощей-");
            if (connection != null) 
            {
                SqlCommand cmd = new SqlCommand("select count(type_k) from Table_f as T where T.type_k = 'овощ';", connection);
                Console.WriteLine($"count = {cmd.ExecuteScalar()}");
            }
            else
                Console.WriteLine("Соединение с бд не установленно");
            Console.WriteLine();
        }
        public void NumberFruits()
        {
            Console.WriteLine("-Показать количество фруктов-");
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select count(type_k) from Table_f as T where T.type_k = 'фрукт';", connection);
                Console.WriteLine($"count = {cmd.ExecuteScalar()}");
            }
            else
                Console.WriteLine("Соединение с бд не установленно");
            Console.WriteLine();
        }
        public void CountVegetablesAndFruits()
        {
            Console.WriteLine("-Показать количество овощей и фруктов заданного цвета-");
            if (connection != null)
            {
                Console.Write("color - ");
                string color = Console.ReadLine();
                SqlCommand cmd = new SqlCommand($"select count(id) " +
                    $"from Table_f where color_k = '{color}'", connection);
                Console.WriteLine($"count = {cmd.ExecuteScalar()}");
            }
            else
                Console.WriteLine("Соединение с бд не установленно");
            Console.WriteLine();
        }
        public void PrintCountVAF()
        {
            Console.WriteLine("-Показать количество овощей фруктов каждого цвета-");
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select color_k, count(id) from Table_f group by color_k;", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine(String.Format("|{0,11}|{1,6}|", reader[0], reader[1]));
                reader?.Close();
            }
            else
                Console.WriteLine("Соединение с бд не установленно");
            Console.WriteLine();
        }
        public void PrintLessCaloric()
        {
            Console.WriteLine("-Показать овощи и фрукты с калорийностью ниже yказанной-");
            if (connection != null)
            {
                Console.Write("caloric - ");
                int caloric = Convert.ToInt32(Console.ReadLine());
                SqlCommand cmd = new SqlCommand($"select title_k, caloric_k from Table_f where caloric_k < {caloric};", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine(String.Format("|{0,11}|{1, 4}|", reader[0], reader[1]));
                reader?.Close();
            }
            else
                Console.WriteLine("Соединение с бд не установленно");
            Console.WriteLine();
        }
        public void PrintMoreCaloric()
        {
            Console.WriteLine("-Показать овощи и фрукты с калорийностью выше указанной-");
            if (connection != null)
            {
                Console.Write("caloric - ");
                int caloric = Convert.ToInt32(Console.ReadLine());
                SqlCommand cmd = new SqlCommand($"select title_k, caloric_k from Table_f where caloric_k > {caloric};", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine(String.Format("|{0,11}|{1, 4}|", reader[0], reader[1]));
                reader?.Close();
            }
            else
                Console.WriteLine("Соединение с бд не установленно");
            Console.WriteLine();
        }
        public void PrintBetweenCaloric()
        {
            Console.WriteLine("-Показать овощи и фрукты с калорийностью в указанном диапазоне-");
            if (connection != null)
            {
                Console.Write("caloric - ");
                int caloric1 = Convert.ToInt32(Console.ReadLine());
                Console.Write("caloric - ");
                int caloric2 = Convert.ToInt32(Console.ReadLine());
                SqlCommand cmd = new SqlCommand($"select title_k, caloric_k  from Table_f where caloric_k between {caloric1} and {caloric2};", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine(String.Format("|{0,11}|{1, 4}|", reader[0], reader[1]));
                reader?.Close();
            }
            else
                Console.WriteLine("Соединение с бд не установленно");
            Console.WriteLine();
        }
        public void Print()
        {
            Console.WriteLine("-Показать все овощи и фрукты, у которых цвет желтый или красный-");
            if (connection != null)
            {
                SqlCommand cmd = new SqlCommand("select title_k from Table_f where color_k = 'желтый' or color_k = 'красный'", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine(String.Format("|{0,11}|", reader[0]));
                reader?.Close();
            }
            else
                Console.WriteLine("Соединение с бд не установленно");
            Console.WriteLine();
        }
    }
}
