using ExampleDapper.Models;

namespace ExampleDapper.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAll();
        Task<Employee> Find(int? id);
        Task<Employee> Add(Employee employee);
        Task<Employee> Update(Employee employee);
        Task Remove(int? id);
        Task<bool> EmployeeExists(int? id);
    }
}