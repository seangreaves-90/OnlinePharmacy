using Microsoft.Data.SqlClient;
using ScriptEase.OnlinePharmacy.Data.DataModels;
using ScriptEase.OnlinePharmacy.Data.Interfaces.Repositories;

namespace ScriptEase.OnlinePharmacy.Data.Repositories
{
    public class OrderRepository : IBaseRepository<Order>
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var orders = new List<Order>();
            var query = "SELECT * FROM Orders";

            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand(query, con))
                {
                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await rdr.ReadAsync())
                        {
                            var order = new Order(
                                orderId: Convert.ToInt32(rdr["OrderId"]),
                                userId: Convert.ToInt32(rdr["UserId"]),
                                createdDate: Convert.ToDateTime(rdr["CreatedDate"]),
                                createdBy: rdr["CreatedBy"].ToString(),
                                lastModified: rdr["LastModified"] as DateTime?,
                                lastModifiedBy: rdr["LastModifiedBy"].ToString()
                            );
                            orders.Add(order);
                        }
                    }
                }
            }
            return orders;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            Order order = null;
            var query = "SELECT * FROM Orders WHERE OrderId = @Id";

            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        if (await rdr.ReadAsync())
                        {
                            order = new Order(
                                orderId: Convert.ToInt32(rdr["OrderId"]),
                                userId: Convert.ToInt32(rdr["UserId"]),
                                createdDate: Convert.ToDateTime(rdr["CreatedDate"]),
                                createdBy: rdr["CreatedBy"].ToString(),
                                lastModified: rdr["LastModified"] as DateTime?,
                                lastModifiedBy: rdr["LastModifiedBy"].ToString()
                            );
                        }
                    }
                }
            }
            return order;
        }

        public async Task CreateAsync(Order entity)
        {
            var query = "INSERT INTO Orders (UserId, CreatedBy) VALUES (@UserId, @CreatedBy)";

            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", entity.UserId);
                    cmd.Parameters.AddWithValue("@CreatedBy", entity.CreatedBy);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Order entity)
        {
            var query = "UPDATE Orders SET UserId = @UserId, LastModified = @LastModified, LastModifiedBy = @LastModifiedBy WHERE OrderId = @OrderId";

            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@OrderId", entity.OrderId);
                    cmd.Parameters.AddWithValue("@UserId", entity.UserId);
                    cmd.Parameters.AddWithValue("@LastModified", entity.LastModified.HasValue ? (object)entity.LastModified.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@LastModifiedBy", entity.LastModifiedBy);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM Orders WHERE OrderId = @Id";

            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}