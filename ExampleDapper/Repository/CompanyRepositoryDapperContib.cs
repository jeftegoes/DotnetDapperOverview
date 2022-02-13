using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using ExampleDapper.Data;
using ExampleDapper.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ExampleDapper.Repository
{
    public class CompanyRepositoryDapperContib : ICompanyRepository
    {
        private IDbConnection connection;

        public CompanyRepositoryDapperContib(IConfiguration configuration)
        {
            this.connection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Company>> GetAll()
        {
            return (await connection.GetAllAsync<Company>()).ToList();
        }

        public async Task<Company> Find(int? id)
        {
            return (await connection.GetAsync<Company>(id));
        }

        public async Task<Company> Add(Company company)
        {
            var companyId = await connection.InsertAsync(company);
            company.CompanyId = companyId;

            return company;
        }

        public async Task<Company> Update(Company company)
        {
            await connection.UpdateAsync(company);

            return company;
        }

        public async Task Remove(int? id)
        {
            await connection.DeleteAsync(new Company { CompanyId = id.GetValueOrDefault(0) });
        }

        public async Task<bool> CompanyExists(int? id)
        {
            return false;
        }
    }
}