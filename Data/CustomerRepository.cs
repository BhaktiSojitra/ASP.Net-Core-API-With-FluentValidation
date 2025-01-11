using API_DEMO.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_DEMO.Data
{
    public class CustomerRepository
    {
        private readonly string _connectionString;
        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        #region SelectAll
        public IEnumerable<CustomerModel> SelectAll()
        {
            var customers = new List<CustomerModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(new CustomerModel
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        HomeAddress = reader["HomeAddress"].ToString(),
                        Email = reader["Email"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        GSTNo = reader["GSTNo"].ToString(),
                        CityName = reader["CityName"].ToString(),
                        PinCode = reader["PinCode"].ToString(),
                        NetAmount = Convert.ToDecimal(reader["NetAmount"]),
                        UserID = Convert.ToInt32(reader["UserID"])
                    });
                }
                return customers;
            }
        }
        #endregion

        #region SelectByPK
        public CustomerModel SelectByPK(int CustomerID)
        {
            CustomerModel coustomer = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    coustomer = new CustomerModel
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        HomeAddress = reader["HomeAddress"].ToString(),
                        Email = reader["Email"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        GSTNo = reader["GSTNo"].ToString(),
                        CityName = reader["CityName"].ToString(),
                        PinCode = reader["PinCode"].ToString(),
                        NetAmount = Convert.ToDecimal(reader["NetAmount"]),
                        UserID = Convert.ToInt32(reader["UserID"])
                    };
                }
            }
            return coustomer;
        }
        #endregion

        #region Delete
        public bool Delete(int CustomerID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Insert
        public bool Insert(CustomerModel customerModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                cmd.Parameters.AddWithValue("@CustomerName", customerModel.CustomerName);
                cmd.Parameters.AddWithValue("@HomeAddress", customerModel.HomeAddress);
                cmd.Parameters.AddWithValue("@Email", customerModel.Email);
                cmd.Parameters.AddWithValue("@MobileNo", customerModel.MobileNo);
                cmd.Parameters.AddWithValue("@GSTNo", customerModel.GSTNo);
                cmd.Parameters.AddWithValue("@CityName", customerModel.CityName);
                cmd.Parameters.AddWithValue("@PinCode", customerModel.PinCode);
                cmd.Parameters.AddWithValue("@NetAmount", customerModel.NetAmount);
                cmd.Parameters.AddWithValue("@UserID", customerModel.UserID);

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(CustomerModel customerModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                cmd.Parameters.AddWithValue("@CustomerID", customerModel.CustomerID);
                cmd.Parameters.AddWithValue("@CustomerName", customerModel.CustomerName);
                cmd.Parameters.AddWithValue("@HomeAddress", customerModel.HomeAddress);
                cmd.Parameters.AddWithValue("@Email", customerModel.Email);
                cmd.Parameters.AddWithValue("@MobileNo", customerModel.MobileNo);
                cmd.Parameters.AddWithValue("@GSTNo", customerModel.GSTNo);
                cmd.Parameters.AddWithValue("@CityName", customerModel.CityName);
                cmd.Parameters.AddWithValue("@PinCode", customerModel.PinCode);
                cmd.Parameters.AddWithValue("@NetAmount", customerModel.NetAmount);

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion
    }
}
