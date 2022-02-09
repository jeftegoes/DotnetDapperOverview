using System.Data;
using Dapper;
using ExampleDapper.Data;
using ExampleDapper.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ExampleDapper.Repository
{
    public class CompanyRepositoryDP : ICompanyRepository
    {
        private IDbConnection db;

        public CompanyRepositoryDP(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Company>> GetAll()
        {
            var sql = "SELECT * FROM Company";

            return (await db.QueryAsync<Company>(sql)).ToList();
        }

        public async Task<Company> Find(int? id)
        {
            var sql = "SELECT * FROM Company WHERE CompanyId = @CompanyId";

            return (await db.QueryAsync<Company>(sql, new { CompanyId = id })).FirstOrDefault();
        }

        public async Task<Company> Add(Company company)
        {
            return await db.add
        }

        public async Task<Company> Update(Company company)
        {
        }

        public async Task Remove(int? id)
        {
        }

        public async Task<bool> CompanyExists(int? id)
        {
        }
    }
}