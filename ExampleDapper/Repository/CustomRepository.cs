using System.Data;
using Dapper;
using ExampleDapper.Models;
using Microsoft.Data.SqlClient;

namespace ExampleDapper.Repository
{
    public class CustomRepository : ICustomRepository
    {
        private readonly IDbConnection connection;

        public CustomRepository(IConfiguration configuration)
        {
            this.connection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Employee>> GetEmployeeWithCompany(int id)
        {
            var sql = @"SELECT
                            E.*, C.*
                        FROM
                            Employee E INNER JOIN Company C ON E.CompanyId = C.CompanyId";

            if (id != 0)
                sql += @" WHERE
                            E.CompanyId = @CompanyId";

            var employees = await connection.QueryAsync<Employee, Company, Employee>(sql, (e, c) =>
            {
                e.Company = c;
                return e;
            }, splitOn: "CompanyId", param: new { CompanyId = id });

            return employees.ToList();
        }
    }
}