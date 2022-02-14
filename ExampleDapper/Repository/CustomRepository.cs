using System.Data;
using System.Transactions;
using Dapper;
using Dapper.Contrib.Extensions;
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

        public async Task<Company> GetCompanyWithEmployees(int id)
        {
            var p = new
            {
                CompanyId = id
            };

            var sql = @"SELECT
                            *
                        FROM
                            Company
                        WHERE
                            CompanyId = @CompanyId
                            
                        SELECT
                            *
                        FROM
                            Employee
                        WHERE
                            CompanyId = @CompanyId";

            Company company = null;

            using (var list = await connection.QueryMultipleAsync(sql, p))
            {
                company = list.Read<Company>().ToList().FirstOrDefault();
                company.Employee = list.Read<Employee>().ToList();
            }

            return company;
        }

        public async Task<List<Company>> GetAllCompanyWithExployees()
        {
            var sql = @"SELECT
                             C.*, E.*
                        FROM
                            Employee E INNER JOIN Company C ON E.CompanyId = C.CompanyId";

            var companyDic = new Dictionary<int, Company>();

            var company = await connection.QueryAsync<Company, Employee, Company>(sql, (c, e) =>
            {
                if (!companyDic.TryGetValue(c.CompanyId, out var currentCompany))
                {
                    currentCompany = c;
                    companyDic.Add(currentCompany.CompanyId, currentCompany);
                }

                currentCompany.Employee.Add(e);

                return currentCompany;
            }, splitOn: "EmployeeId");

            return company.Distinct().ToList();
        }

        public async Task AddTestCompanyWithEmployees(Company company)
        {
            var companyId = (int)await connection.InsertAsync(company);

            // foreach (var employee in company.Employee)
            // {
            //     employee.CompanyId = companyId;
            //     await connection.InsertAsync(employee);
            // }

            // Bulk insert.
            company.Employee.Select(e => { e.CompanyId = companyId; return e; }).ToList();
            await connection.InsertAsync(company.Employee);
        }

        public async Task AddTestCompanyWithEmployeeWithTransaction(Company company)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
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
                                     @PostalCode);

                                SELECT CAST(SCOPE_IDENTITY() as int); ";

                    var id = (await connection.QueryAsync<int>(sql, company)).FirstOrDefault();
                    company.CompanyId = id;

                    company.Employee.Select(c => { c.CompanyId = id; return c; }).ToList();

                    var sqlEmp = @" INSERT INTO Employee
                                        (Name,
                                         Title,
                                         Email,
                                         Phone,
                                         CompanyId)
                                    VALUES
                                        (@Name,
                                         @Title,
                                         @Email,
                                         @Phone,
                                         @CompanyId);

                                    SELECT CAST(SCOPE_IDENTITY() as int); ";

                    await connection.ExecuteAsync(sqlEmp, company.Employee);

                    transaction.Complete();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task RemoveRange(int[] companyId)
        {
            var sql = @"DELETE FROM
                            Company
                        WHERE
                            CompanyId IN @companyId";

            await connection.QueryAsync(sql, new { companyId });
        }

        public async Task<List<Company>> FilterCompanyByName(string name)
        {
            var sql = @"SELECT
                            *
                        FROM
                            Company
                        WHERE
                            Name LIKE '%'+@name+'%'";

            return (await connection.QueryAsync<Company>(sql, new { name })).ToList();
        }
    }
}