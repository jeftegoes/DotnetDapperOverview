using ExampleDapper.Models;

namespace ExampleDapper.Repository
{
    public interface ICustomRepository
    {
        Task<List<Employee>> GetEmployeeWithCompany(int id);
        Task<Company> GetCompanyWithEmployees(int id);
        Task<List<Company>> GetAllCompanyWithExployees();
        Task AddTestCompanyWithEmployees(Company company);
        Task AddTestCompanyWithEmployeeWithTransaction(Company company);
        Task RemoveRange(int[] companyIdToRemove);
        Task<List<Company>> FilterCompanyByName(string name);
    }
}