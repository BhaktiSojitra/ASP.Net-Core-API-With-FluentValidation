using API_DEMO.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_DEMO.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        #region SelectAll
        public IEnumerable<UserModel> SelectAll()
        {
            var users = new List<UserModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_User_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new UserModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        Address = reader["Address"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    });
                }
                return users;
            }
        }
        #endregion

        #region SelectByPK
        public UserModel SelectByPK(int UserID)
        {
            UserModel user = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_User_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                cmd.Parameters.AddWithValue("@UserID", UserID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new UserModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        Address = reader["Address"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    };
                }
            }
            return user;
        }
        #endregion

        #region Delete
        public bool Delete(int UserID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_User_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                cmd.Parameters.AddWithValue("@UserID", UserID);
                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Insert
        public bool Insert(UserModel userModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_User_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                cmd.Parameters.AddWithValue("@UserName", userModel.UserName);
                cmd.Parameters.AddWithValue("@Email", userModel.Email);
                cmd.Parameters.AddWithValue("@Password", userModel.Password);
                cmd.Parameters.AddWithValue("@MobileNo", userModel.MobileNo);
                cmd.Parameters.AddWithValue("@Address", userModel.Address);
                cmd.Parameters.AddWithValue("@IsActive", userModel.IsActive);

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(UserModel userModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_User_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                cmd.Parameters.AddWithValue("@UserID", userModel.UserID);
                cmd.Parameters.AddWithValue("@UserName", userModel.UserName);
                cmd.Parameters.AddWithValue("@Email", userModel.Email);
                cmd.Parameters.AddWithValue("@Password", userModel.Password);
                cmd.Parameters.AddWithValue("@MobileNo", userModel.MobileNo);
                cmd.Parameters.AddWithValue("@Address", userModel.Address);
                cmd.Parameters.AddWithValue("@IsActive", userModel.IsActive);

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion
    }
}
