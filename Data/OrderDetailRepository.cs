using API_DEMO.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_DEMO.Data
{
    public class OrderDetailRepository
    {
        private readonly string _connectionString;
        public OrderDetailRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        #region SelectAll
        public IEnumerable<OrderDetailModel> SelectAll()
        {
            var orderdetails = new List<OrderDetailModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderDetail_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    orderdetails.Add(new OrderDetailModel
                    {
                        OrderDetailID = Convert.ToInt32(reader["OrderDetailID"]),
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Amount = Convert.ToDecimal(reader["Amount"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        UserID = Convert.ToInt32(reader["UserID"])
                    });
                }
                return orderdetails;
            }
        }
        #endregion

        #region SelectByPK
        public OrderDetailModel SelectByPK(int OrderDetailID)
        {
            OrderDetailModel orderdetail = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderDetail_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                cmd.Parameters.AddWithValue("@OrderDetailID", OrderDetailID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    orderdetail = new OrderDetailModel
                    {
                        OrderDetailID = Convert.ToInt32(reader["OrderDetailID"]),
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Amount = Convert.ToDecimal(reader["Amount"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        UserID = Convert.ToInt32(reader["UserID"])
                    };
                }
            }
            return orderdetail;
        }
        #endregion

        #region Delete
        public bool Delete(int OrderDetailID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderDetail_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                cmd.Parameters.AddWithValue("@OrderDetailID", OrderDetailID);
                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Insert
        public bool Insert(OrderDetailModel orderdetailModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderDetail_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                cmd.Parameters.AddWithValue("@OrderID", orderdetailModel.OrderID);
                cmd.Parameters.AddWithValue("@ProductID", orderdetailModel.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", orderdetailModel.Quantity);
                cmd.Parameters.AddWithValue("@Amount", orderdetailModel.Amount);
                cmd.Parameters.AddWithValue("@TotalAmount", orderdetailModel.TotalAmount);
                cmd.Parameters.AddWithValue("@UserID", orderdetailModel.UserID);

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(OrderDetailModel orderdetailModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderDetail_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                cmd.Parameters.AddWithValue("@OrderDetailID", orderdetailModel.OrderDetailID);
                cmd.Parameters.AddWithValue("@Quantity", orderdetailModel.Quantity);
                cmd.Parameters.AddWithValue("@Amount", orderdetailModel.Amount);
                cmd.Parameters.AddWithValue("@TotalAmount", orderdetailModel.TotalAmount);

                int rowAffected = cmd.ExecuteNonQuery();
                //Console.WriteLine(cmd.ExecuteNonQuery());
                //Console.WriteLine("rowAffected:- " + rowAffected);
                return rowAffected > 0;
            }
        }
        #endregion

        #region OrderDropDown()
        public IEnumerable<OrderDropDownModel> OrderDropDown()
        {
            var order = new List<OrderDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_DropDown", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    order.Add(new OrderDropDownModel
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        OrderNumber = Convert.ToInt32(reader["OrderNumber"])
                    });
                }
            }
            return order;
        }
        #endregion

        #region ProductDropDown()
        public IEnumerable<ProductDropDownModel> ProductDropDown()
        {
            var product = new List<ProductDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_DropDown", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    product.Add(new ProductDropDownModel
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString()
                    });
                }
            }
            return product;
        }
        #endregion
    }
}