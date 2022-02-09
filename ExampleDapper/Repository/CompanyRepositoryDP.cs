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
            var sql = @"SELECT
                            *
                        FROM
                            Company";

            return (await db.QueryAsync<Company>(sql)).ToList();
        }

        public async Task<Company> Find(int? id)
        {
            var sql = @"SELECT
                            *
                        FROM
                            Company
                        WHERE
                            CompanyId = @CompanyId";

            return (await db.QueryAsync<Company>(sql, new { CompanyId = id })).FirstOrDefault();
        }

        public async Task<Company> Add(Company company)
        {
            var sql = @"INSERT INTO Company
                            (Name,
                             Address,
                             City,
                             State,
                             PostalCode)
                        VALUES 
                            (@Name,
                             @Address,
                             @City,
                             @State,
                             @PostalCode)";

            // var param = new
            // {
            //     Name = company.Name,
            //     Address = company.Address,
            //     City = company.City,
            //     State = company.State,
            //     PostalCode = company.PostalCode
            // };

            // return await db.ExecuteScalarAsync<Company>(sql, param);
            return await db.ExecuteScalarAsync<Company>(sql, company);
        }

        public async Task<Company> Update(Company company)
        {
            var sql = @"UPDATE
                            Company
                        SET
                            Name = @Name,
                            Address = @Address,
                            City = @City,
                            State = @State,
                            PostalCode = @PostalCode
                        WHERE
                            CompanyId = @CompanyId";

            return await db.ExecuteScalarAsync<Company>(sql, company);;
        }

        public async Task Remove(int? id)
        {
            var sql = "DELETE FROM Company WHERE CompanyId = @CompanyId";

            await db.QueryAsync(sql, new { CompanyId = id });
        }

        public async Task<bool> CompanyExists(int? id)
        {
            return false;
        }
    }
}