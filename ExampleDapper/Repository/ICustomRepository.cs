using ExampleDapper.Models;

namespace ExampleDapper.Repository
{
    public interface ICustomRepository
    {
         Task<List<Employee>> GetEmployeeWithCompany(int id);
    }
}