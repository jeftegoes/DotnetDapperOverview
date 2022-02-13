using Dapper.Contrib.Extensions;

namespace ExampleDapper.Models
{
    [Table("Company")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}