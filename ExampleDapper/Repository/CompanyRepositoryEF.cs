using ExampleDapper.Data;
using ExampleDapper.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleDapper.Repository
{
    public class CompanyRepositoryEF : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
        
        public CompanyRepositoryEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Company>> GetAll()
        {
            return await _context.Company.ToListAsync();
        }

        public async Task<Company> Find(int? id)
        {
            return await _context.Company.FirstOrDefaultAsync(c => c.CompanyId == id);
        }

        public async Task<Company> Add(Company company)
        {
            await _context.Company.AddAsync(company);
            await _context.SaveChangesAsync();

            return company;
        }

        public async Task<Company> Update(Company company)
        {
            _context.Company.Update(company);
            await _context.SaveChangesAsync();

            return company;
        }

        public async Task Remove(int? id)
        {
            _context.Company.Remove(_context.Company.FirstOrDefault(c => c.CompanyId == id));
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CompanyExists(int? id)
        {
            return await _context.Company.AnyAsync(e => e.CompanyId == id);
        }
    }
}