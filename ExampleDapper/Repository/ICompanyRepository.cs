using ExampleDapper.Models;

namespace ExampleDapper.Repository
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAll();
        Task<Company> Find(int? id);
        Task<Company> Add(Company company);
        Task<Company> Update(Company company);
        Task Remove(int? id);
        Task<bool> CompanyExists(int? id);
    }
}