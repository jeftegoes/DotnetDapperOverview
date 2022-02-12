using System.Data;
using Dapper;
using ExampleDapper.Data;
using ExampleDapper.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ExampleDapper.Repository
{
    public class EmployeeRepositoryDapper : IEmployeeRepository
    {
        private IDbConnection connection;

        public EmployeeRepositoryDapper(IConfiguration configuration)
        {
            this.connection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Employee>> GetAll()
        {
            var sql = @"SELECT
                            *
                        FROM
                            Employee";

            return (await connection.QueryAsync<Employee>(sql)).ToList();
        }

        public async Task<Employee> Find(int? id)
        {
            var sql = @"SELECT
                            *
                        FROM
                            Employee
                        WHERE
                            EmployeeId = @EmployeeId";

            return (await connection.QueryAsync<Employee>(sql, new { EmployeeId = id })).FirstOrDefault();
        }

        public async Task<Employee> Add(Employee employee)
        {
            var sql = @"INSERT INTO Employee
                            (Name,
                             Email,
                             Phone,
                             Title,
                             CompanyId)
                        VALUES 
                            (@Name,
                             @Email,
                             @Phone,
                             @Title,
                             @CompanyId)";

            // var param = new
            // {
            //     Name = Employee.Name,
            //     Address = Employee.Address,
            //     City = Employee.City,
            //     State = Employee.State,
            //     PostalCode = Employee.PostalCode
            // };

            // return await db.ExecuteScalarAsync<Employee>(sql, param);
            return await connection.ExecuteScalarAsync<Employee>(sql, employee);
        }

        public async Task<Employee> Update(Employee employee)
        {
            var sql = @"UPDATE
                            Employee
                        SET
                            Name = @Name,
                            Email = @Email,
                            Phone = @Phone,
                            Title = @Title,
                            CompanyId = @CompanyId
                        WHERE
                            EmployeeId = @EmployeeId";

            return await connection.ExecuteScalarAsync<Employee>(sql, employee);;
        }

        public async Task Remove(int? id)
        {
            var sql = "DELETE FROM Employee WHERE EmployeeId = @EmployeeId";

            await connection.QueryAsync(sql, new { EmployeeId = id });
        }

        public async Task<bool> EmployeeExists(int? id)
        {
            return false;
        }
    }
}