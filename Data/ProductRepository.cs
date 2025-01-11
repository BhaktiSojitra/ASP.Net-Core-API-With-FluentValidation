using API_DEMO.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_DEMO.Data
{
    public class ProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        #region SelectAll
        public IEnumerable<ProductModel> SelectAll()
        {
            var products = new List<ProductModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new ProductModel
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        ProductPrice = Convert.ToDouble(reader["ProductPrice"]),
                        ProductCode = reader["ProductCode"].ToString(),
                        Description = reader["Description"].ToString(),
                        UserID = Convert.ToInt32(reader["UserID"])
                    });
                }
                return products;
            }
        }
        #endregion

        #region SelectByPK
        public ProductModel SelectByPK(int ProductID)
        {
            ProductModel product = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product = new ProductModel
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        ProductPrice = Convert.ToDouble(reader["ProductPrice"]),
                        ProductCode = reader["ProductCode"].ToString(),
                        Description = reader["Description"].ToString(),
                        UserID = Convert.ToInt32(reader["UserID"])
                    };
                }
            }
            return product;
        }
        #endregion

        #region Delete
        public bool Delete(int ProductID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Insert
        public bool Insert(ProductModel productModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                cmd.Parameters.AddWithValue("@ProductPrice", productModel.ProductPrice);
                cmd.Parameters.AddWithValue("@ProductCode", productModel.ProductCode);
                cmd.Parameters.AddWithValue("@Description", productModel.Description);
                cmd.Parameters.AddWithValue("@UserID", productModel.UserID);

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(ProductModel productModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                cmd.Parameters.AddWithValue("@ProductID", productModel.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                cmd.Parameters.AddWithValue("@ProductPrice", productModel.ProductPrice);
                cmd.Parameters.AddWithValue("@ProductCode", productModel.ProductCode);
                cmd.Parameters.AddWithValue("@Description", productModel.Description);

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region UserDropDown()
        public IEnumerable<UserDropDownModel> UserDropDown()
        {
            var user = new List<UserDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_User_DropDown", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.Add(new UserDropDownModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString()
                    });
                }
            }
            return user;
        }
        #endregion
    }
}
