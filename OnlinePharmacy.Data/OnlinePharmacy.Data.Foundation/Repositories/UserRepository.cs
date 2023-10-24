using Microsoft.Data.SqlClient;
using ScriptEase.OnlinePharmacy.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScriptEase.OnlinePharmacy.Data.Interfaces.Repositories;

namespace ScriptEase.OnlinePharmacy.Data.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = new List<User>();
            var query = "SELECT * FROM Users";

            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand(query, con))
                {
                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await rdr.ReadAsync())
                        {
                            var user = new User(
                                userId: Convert.ToInt32(rdr["UserId"]),
                                name: rdr["Name"].ToString(),
                                email: rdr["Email"].ToString(),
                                role: rdr["Role"].ToString(),
                                createdDate: Convert.ToDateTime(rdr["CreatedDate"]),
                                createdBy: rdr["CreatedBy"].ToString(),
                                lastModified: rdr["LastModified"] as DateTime?,
                                lastModifiedBy: rdr["LastModifiedBy"].ToString()
                            );
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            User user = null;
            var query = "SELECT * FROM Users WHERE UserId = @Id";

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
                            user = new User(
                                userId: Convert.ToInt32(rdr["UserId"]),
                                name: rdr["Name"].ToString(),
                                email: rdr["Email"].ToString(),
                                role: rdr["Role"].ToString(),
                                createdDate: Convert.ToDateTime(rdr["CreatedDate"]),
                                createdBy: rdr["CreatedBy"].ToString(),
                                lastModified: rdr["LastModified"] as DateTime?,
                                lastModifiedBy: rdr["LastModifiedBy"].ToString()
                            );
                        }
                    }
                }
            }
            return user;
        }

        public async Task CreateAsync(User entity)
        {
            var query = "INSERT INTO Users (Name, Email, Role, CreatedBy) VALUES (@Name, @Email, @Role, @CreatedBy)";

            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", entity.Name);
                    cmd.Parameters.AddWithValue("@Email", entity.Email);
                    cmd.Parameters.AddWithValue("@Role", entity.Role);
                    cmd.Parameters.AddWithValue("@CreatedBy", entity.CreatedBy);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(User entity)
        {
            var query = "UPDATE Users SET Name = @Name, Email = @Email, Role = @Role, LastModified = @LastModified, LastModifiedBy = @LastModifiedBy WHERE UserId = @UserId";

            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", entity.UserId);
                    cmd.Parameters.AddWithValue("@Name", entity.Name);
                    cmd.Parameters.AddWithValue("@Email", entity.Email);
                    cmd.Parameters.AddWithValue("@Role", entity.Role);
                    cmd.Parameters.AddWithValue("@LastModified", entity.LastModified.HasValue ? (object)entity.LastModified.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@LastModifiedBy", entity.LastModifiedBy);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM Users WHERE UserId = @Id";

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
