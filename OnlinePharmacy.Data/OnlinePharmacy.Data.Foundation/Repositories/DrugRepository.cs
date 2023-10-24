using Microsoft.Data.SqlClient;
using ScriptEase.OnlinePharmacy.Data.DataModels;
using ScriptEase.OnlinePharmacy.Data.Interfaces.Repositories;

namespace ScriptEase.OnlinePharmacy.Data.Repositories
{
    public class DrugRepository : IBaseRepository<Drug>
    {
        private readonly string _connectionString;

        public DrugRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IEnumerable<Drug>> GetAllAsync()
        {
            var drugs = new List<Drug>();
            var query = "SELECT * FROM Drugs";

            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand(query, con))
                {
                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await rdr.ReadAsync())
                        {
                            var drug = new Drug(
                                drugId: Convert.ToInt32(rdr["DrugId"]),
                                name: rdr["Name"].ToString(),
                                description: rdr["Description"].ToString(),
                                stock: Convert.ToInt32(rdr["Stock"]),
                                createdDate: Convert.ToDateTime(rdr["CreatedDate"]),
                                createdBy: rdr["CreatedBy"].ToString(),
                                lastModified: rdr["LastModified"] as DateTime?,
                                lastModifiedBy: rdr["LastModifiedBy"].ToString()
                            );
                            drugs.Add(drug);
                        }
                    }
                }
            }
            return drugs;
        }

        public async Task<Drug> GetByIdAsync(int id)
        {
            Drug drug = null;
            var query = "SELECT * FROM Drugs WHERE DrugId = @Id";

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
                            drug = new Drug(
                                drugId: Convert.ToInt32(rdr["DrugId"]),
                                name: rdr["Name"].ToString(),
                                description: rdr["Description"].ToString(),
                                stock: Convert.ToInt32(rdr["Stock"]),
                                createdDate: Convert.ToDateTime(rdr["CreatedDate"]),
                                createdBy: rdr["CreatedBy"].ToString(),
                                lastModified: rdr["LastModified"] as DateTime?,
                                lastModifiedBy: rdr["LastModifiedBy"].ToString()
                            );
                        }
                    }
                }
            }
            return drug;
        }

        public async Task CreateAsync(Drug entity)
        {
            var query = "INSERT INTO Drugs (Name, Description, Stock, CreatedBy) VALUES (@Name, @Description, @Stock, @CreatedBy)";

            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", entity.Name);
                    cmd.Parameters.AddWithValue("@Description", entity.Description);
                    cmd.Parameters.AddWithValue("@Stock", entity.Stock);
                    cmd.Parameters.AddWithValue("@CreatedBy", entity.CreatedBy);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Drug entity)
        {
            var query = "UPDATE Drugs SET Name = @Name, Description = @Description, Stock = @Stock, LastModified = @LastModified, LastModifiedBy = @LastModifiedBy WHERE DrugId = @DrugId";

            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DrugId", entity.DrugId);
                    cmd.Parameters.AddWithValue("@Name", entity.Name);
                    cmd.Parameters.AddWithValue("@Description", entity.Description);
                    cmd.Parameters.AddWithValue("@Stock", entity.Stock);
                    cmd.Parameters.AddWithValue("@LastModified", entity.LastModified.HasValue ? (object)entity.LastModified.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@LastModifiedBy", entity.LastModifiedBy);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM Drugs WHERE DrugId = @Id";

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
