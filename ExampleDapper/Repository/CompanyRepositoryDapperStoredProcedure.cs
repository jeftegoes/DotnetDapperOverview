using System.Data;
using Dapper;
using ExampleDapper.Data;
using ExampleDapper.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ExampleDapper.Repository
{
    public class CompanyRepositoryDapperStoredProcedure : ICompanyRepository
    {
        private IDbConnection connection;

        public CompanyRepositoryDapperStoredProcedure(IConfiguration configuration)
        {
            this.connection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Company>> GetAll()
        {
            return (await connection.QueryAsync<Company>("StpGetALLCompany", CommandType.StoredProcedure)).ToList();
        }

        public async Task<Company> Find(int? id)
        {
            return (await connection.QueryAsync<Company>("StpGetCompany", new { CompanyId = id }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
        }

        public async Task<Company> Add(Company company)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", company.CompanyId, DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Name", company.Name, DbType.String);
            parameters.Add("@Address", company.Address, DbType.String);
            parameters.Add("@City", company.City, DbType.String);
            parameters.Add("@State", company.State, DbType.String);
            parameters.Add("@PostalCode", company.PostalCode, DbType.String);

            await connection.ExecuteAsync("StpAddCompany", parameters, commandType: CommandType.StoredProcedure);
            company.CompanyId = parameters.Get<int>("CompanyId");

            return company;
        }

        public async Task<Company> Update(Company company)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", company.CompanyId, DbType.Int32);
            parameters.Add("@Name", company.Name, DbType.String);
            parameters.Add("@Address", company.Address, DbType.String);
            parameters.Add("@City", company.City, DbType.String);
            parameters.Add("@State", company.State, DbType.String);
            parameters.Add("@PostalCode", company.PostalCode, DbType.String);

            await connection.ExecuteAsync("StpAddCompany", parameters, commandType: CommandType.StoredProcedure);

            return company;
        }

        public async Task Remove(int? id)
        {
            await connection.ExecuteAsync("StpRemoveCompany", new { CompanyId = id }, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> CompanyExists(int? id)
        {
            return false;
        }
    }
}