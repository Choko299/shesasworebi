using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MyProject // !!! დარწმუნდი, რომ ეს namespace ზუსტად ემთხვევა შენი პროექტის სახელს (მაგ. 'MyProject') !!!
{
    public class DatabaseHelper
    {
        // !!! გამოცდაზე: შეცვალე 'Database1' ზუსტად იმ მონაცემთა ბაზის სახელით, რაც SQL-ში შექმენი.
        // !!! გამოცდაზე: 'Data Source' შეიძლება იყოს (localdb)\\MSSQLLocalDB ან .\\SQLEXPRESS - გამოცდის კომპიუტერზე შეამოწმე.
        //              შენს შემთხვევაში, სავარაუდოდ, 'CHOKO\\SQLEXPRESS'
        private readonly string _connectionString = "Data Source=CHOKO\\SQLEXPRESS;Initial Catalog=Database1;Integrated Security=True;";
        private readonly string _tableName;

        public DatabaseHelper(string tableName)
        {
            _tableName = tableName;
        }

        public List<MyObject> GetAllObjects()
        {
            List<MyObject> objects = new List<MyObject>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM {_tableName}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            objects.Add(MapToObject(reader));
                        }
                    }
                }
            }
            return objects;
        }

        public void AddObject(MyObject obj)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                // Property1, Property2, Property3, Property4 - ეს უნდა ემთხვეოდეს SQL-ში არსებულ სვეტებს!
                string query = $"INSERT INTO {_tableName} (Property1, Property2, Property3, Property4) VALUES (@Prop1, @Prop2, @Prop3, @Prop4)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Prop1", obj.Property1);
                    command.Parameters.AddWithValue("@Prop2", obj.Property2);
                    command.Parameters.AddWithValue("@Prop3", string.IsNullOrWhiteSpace(obj.Property3) ? (object)DBNull.Value : obj.Property3);
                    command.Parameters.AddWithValue("@Prop4", string.IsNullOrWhiteSpace(obj.Property4) ? (object)DBNull.Value : obj.Property4);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteObject(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"DELETE FROM {_tableName} WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        private MyObject MapToObject(SqlDataReader reader)
        {
            // Property1, Property2, Property3, Property4 - ეს უნდა ემთხვეოდეს SQL-ში არსებულ სვეტებს!
            return new MyObject
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Property1 = reader.GetString(reader.GetOrdinal("Property1")),
                Property2 = reader.GetString(reader.GetOrdinal("Property2")),
                Property3 = reader.IsDBNull(reader.GetOrdinal("Property3")) ? null : reader.GetString(reader.GetOrdinal("Property3")),
                Property4 = reader.IsDBNull(reader.GetOrdinal("Property4")) ? null : reader.GetString(reader.GetOrdinal("Property4"))
            };
        }
    }
}
