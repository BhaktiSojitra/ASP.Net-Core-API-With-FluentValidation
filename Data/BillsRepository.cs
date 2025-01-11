using API_DEMO.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_DEMO.Data
{
    public class BillsRepository
    {
        private readonly string _connectionString;
        public BillsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        #region SelectAll
        public IEnumerable<BillsModel> SelectAll()
        {
            var bills = new List<BillsModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Bills_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bills.Add(new BillsModel
                    {
                        BillID = Convert.ToInt32(reader["BillID"]),
                        BillNumber = reader["BillNumber"].ToString(),
                        BillDate = Convert.ToDateTime(reader["BillDate"]),
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        Discount = Convert.ToDecimal(reader["Discount"]),
                        NetAmount = Convert.ToDecimal(reader["NetAmount"]),
                        UserID = Convert.ToInt32(reader["UserID"])
                    });
                }
                return bills;
            }
        }
        #endregion

        #region SelectByPK
        public BillsModel SelectByPK(int BillID)
        {
            BillsModel bill = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Bills_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                cmd.Parameters.AddWithValue("@BillID", BillID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    bill = new BillsModel
                    {
                        BillID = Convert.ToInt32(reader["BillID"]),
                        BillNumber = reader["BillNumber"].ToString(),
                        BillDate = Convert.ToDateTime(reader["BillDate"]),
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        Discount = Convert.ToDecimal(reader["Discount"]),
                        NetAmount = Convert.ToDecimal(reader["NetAmount"]),
                        UserID = Convert.ToInt32(reader["UserID"])
                    };
                }
            }
            return bill;
        }
        #endregion

        #region Delete
        public bool Delete(int BillID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Bills_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                cmd.Parameters.AddWithValue("@BillID", BillID);
                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Insert
        public bool Insert(BillsModel billsModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Bills_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                cmd.Parameters.AddWithValue("@BillNumber", billsModel.BillNumber);
                cmd.Parameters.AddWithValue("@BillDate", billsModel.BillDate);
                cmd.Parameters.AddWithValue("@OrderID", billsModel.OrderID);
                cmd.Parameters.AddWithValue("@TotalAmount", billsModel.TotalAmount);
                cmd.Parameters.AddWithValue("@Discount", billsModel.Discount);
                cmd.Parameters.AddWithValue("@NetAmount", billsModel.NetAmount);
                cmd.Parameters.AddWithValue("@UserID", billsModel.UserID);

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(BillsModel billsModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Bills_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                cmd.Parameters.AddWithValue("@BillID", billsModel.BillID);
                cmd.Parameters.AddWithValue("@BillNumber", billsModel.BillNumber);
                cmd.Parameters.AddWithValue("@BillDate", billsModel.BillDate);
                cmd.Parameters.AddWithValue("@TotalAmount", billsModel.TotalAmount);
                cmd.Parameters.AddWithValue("@Discount", billsModel.Discount);
                cmd.Parameters.AddWithValue("@NetAmount", billsModel.NetAmount);

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion
    }
}