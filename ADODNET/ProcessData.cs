using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ADODNET
{
    public class ProcessData
    {
        protected string ConnectionString { get; set; }

        public ProcessData()
        {
        }

        public ProcessData(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }

        public void CloseConnection(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        public Product usp_getproduct(SqlConnection conn,int ProductId)
        {
            Product product = new Product();
            using (var command =  conn.CreateCommand())
            {
                command.CommandText = "usp_getproduct";
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("ProductId", ProductId);
                command.Parameters.Add(param);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product.ProductId = reader.GetInt32("ProductId");
                        product.Name = reader.GetString("Name");
                        product.Description = reader.GetString("Description");
                        product.UnitPrice = reader.GetDecimal("UnitPrice");
                        product.CategoryId = reader.GetInt32("CategoryId");
                    }
                }
                CloseConnection(conn);
            }

            return product;
        }
        public Product getproduct(SqlConnection conn,int ProductId)
        {
            Product product = new Product();
            
            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select ProductID,Name,Description,UnitPrice,CategoryId from Product where ProductID=@ProductId";
            cmd.CommandType = CommandType.Text;
            SqlParameter param = new SqlParameter("ProductId", ProductId);
            cmd.Parameters.Add(param);

            
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product.ProductId = reader.GetInt32("ProductId");
                        product.Name = reader.GetString("Name");
                        product.Description = reader.GetString("Description");
                        product.UnitPrice = reader.GetDecimal("UnitPrice");
                        product.CategoryId = reader.GetInt32("CategoryId");
                    }
                }

            CloseConnection(conn);
            return product;
        }

        public void UpdateProduct(SqlConnection sqlconnection, Product p)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlconnection;
            cmd.CommandText = "Update Product set Name=@Name,Description=@Description,UnitPrice=@UnitPrice,CategoryId=@CategoryId where ProductID=@ProductID";

            SqlParameter param = new SqlParameter("Name", p.Name);
            cmd.Parameters.Add(param);
            SqlParameter param1 = new SqlParameter("Description", p.Description);
            cmd.Parameters.Add(param1);
            SqlParameter param2 = new SqlParameter("UnitPrice", p.UnitPrice);
            cmd.Parameters.Add(param2);
            SqlParameter param3 = new SqlParameter("CategoryId", p.CategoryId);
            cmd.Parameters.Add(param3);
            SqlParameter param4 = new SqlParameter("ProductID", p.ProductId);
            cmd.Parameters.Add(param4);

            var q = cmd.ExecuteNonQuery();


            CloseConnection(sqlconnection);

        }

        public void deleteproduct(SqlConnection sqlconnection, int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlconnection;
            cmd.CommandText = "Delete from Product where ProductID=@ProductID";

            SqlParameter param = new SqlParameter("ProductID", id);
            cmd.Parameters.Add(param);
    
            var q = cmd.ExecuteNonQuery();


            CloseConnection(sqlconnection);

        }

        public void saveProduct(SqlConnection sqlconnection, Product p)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlconnection;
            cmd.CommandText = "Insert into Product Values(@Name,@Description,@UnitPrice,@CategoryId)";

            SqlParameter param = new SqlParameter("Name", p.Name);
            cmd.Parameters.Add(param);
            SqlParameter param1 = new SqlParameter("Description", p.Description);
            cmd.Parameters.Add(param1);
            SqlParameter param2 = new SqlParameter("UnitPrice", p.UnitPrice);
            cmd.Parameters.Add(param2);
            SqlParameter param3 = new SqlParameter("CategoryId", p.CategoryId);
            cmd.Parameters.Add(param3);

            var q= cmd.ExecuteNonQuery();
            

            CloseConnection(sqlconnection);
        }

        public List<Product> getListProduct(SqlConnection conn)
        {
            List<Product> product = new List<Product>();
            //Product p = new Product();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select ProductID,Name,Description,UnitPrice,CategoryId from Product";
     


            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product p = new Product();

                    p.ProductId = reader.GetInt32("ProductId");
                    p.Name = reader.GetString("Name");
                    p.Description = reader.GetString("Description");
                    p.UnitPrice = reader.GetDecimal("UnitPrice");
                    p.CategoryId = reader.GetInt32("CategoryId");

                    product.Add(p);
                }
            }

            CloseConnection(conn);
            return product;
        }

    }
}
