using API_DEMO.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_DEMO.Data
{
    public class OrderRepository
    {
        private readonly string _connectionString;
        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        #region SelectAll
        public IEnumerable<OrderModel> SelectAll()
        {
            var orders = new List<OrderModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    orders.Add(new OrderModel
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        OrderNumber = Convert.ToInt32(reader["OrderNumber"]),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        PaymentMode = reader["PaymentMode"].ToString(),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        ShippingAddress = reader["ShippingAddress"].ToString(),
                        UserID = Convert.ToInt32(reader["UserID"])
                    });
                }
                return orders;
            }
        }
        #endregion

        #region SelectByPK
        public OrderModel SelectByPK(int OrderID)
        {
            OrderModel order = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                cmd.Parameters.AddWithValue("@OrderID", OrderID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    order = new OrderModel
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        OrderNumber = Convert.ToInt32(reader["OrderNumber"]),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        PaymentMode = reader["PaymentMode"].ToString(),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        ShippingAddress = reader["ShippingAddress"].ToString(),
                        UserID = Convert.ToInt32(reader["UserID"])
                    };
                }
            }
            return order;
        }
        #endregion

        #region Delete
        public bool Delete(int OrderID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                cmd.Parameters.AddWithValue("@OrderID", OrderID);
                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Insert
        public bool Insert(OrderModel orderModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                cmd.Parameters.AddWithValue("@OrderNumber", orderModel.OrderNumber);
                cmd.Parameters.AddWithValue("@OrderDate", orderModel.OrderDate);
                cmd.Parameters.AddWithValue("@CustomerID", orderModel.CustomerID);
                cmd.Parameters.AddWithValue("@PaymentMode", orderModel.PaymentMode);
                cmd.Parameters.AddWithValue("@TotalAmount", orderModel.TotalAmount);
                cmd.Parameters.AddWithValue("@ShippingAddress", orderModel.ShippingAddress);
                cmd.Parameters.AddWithValue("@UserID", orderModel.UserID);

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(OrderModel orderModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                cmd.Parameters.AddWithValue("@OrderID", orderModel.OrderID);
                cmd.Parameters.AddWithValue("@OrderDate", orderModel.OrderDate);
                cmd.Parameters.AddWithValue("@PaymentMode", orderModel.PaymentMode);
                cmd.Parameters.AddWithValue("@TotalAmount", orderModel.TotalAmount);
                cmd.Parameters.AddWithValue("@ShippingAddress", orderModel.ShippingAddress);

                int rowAffected = cmd.ExecuteNonQuery();
                Console.WriteLine("rowAffected:- " + rowAffected);
                return rowAffected > 0;
            }
        }
        #endregion

        #region CustomerDropDown()
        public IEnumerable<CustomerDropDownModel> CustomerDropDown()
        {
            var customer = new List<CustomerDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_DropDown", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customer.Add(new CustomerDropDownModel
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString()
                    });
                }
            }
            return customer;
        }
        #endregion
    }
}
