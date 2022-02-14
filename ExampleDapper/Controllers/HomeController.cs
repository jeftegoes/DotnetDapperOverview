using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExampleDapper.Models;
using ExampleDapper.Repository;

namespace ExampleDapper.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICustomRepository _customRepository;

    public HomeController(ILogger<HomeController> logger,
                          ICustomRepository customRepository)
    {
        _customRepository = customRepository;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var companies = await _customRepository.GetAllCompanyWithExployees();

        return View(companies);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult AddTestRecords()
    {
        var company = new Company()
        {
            Name = "Test" + Guid.NewGuid().ToString(),
            Address = "test address",
            City = "Fairfax",
            PostalCode = "44044688",
            State = "Bahia",
            Employee = new List<Employee>()
            {
                new Employee()
                {
                    Email = "test@test.com",
                    Name = "Test Name " + Guid.NewGuid(),
                    Phone = "+55 71 987996544",
                    Title = "Test Manager"
                },
                new Employee()
                {
                    Email = "test@test.com",
                    Name = "Test Name 2" + Guid.NewGuid(),
                    Phone = "+55 75 988123345",
                    Title = "Test Manager 2"
                }
            }
        };

        _customRepository.AddTestCompanyWithEmployeeWithTransaction(company);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RemoveTestRecordsAsync()
    {
        int[] companyIdToRemove = (await _customRepository.FilterCompanyByName("Test"))
            .Select(i => i.CompanyId)
            .ToArray();

        await _customRepository.RemoveRange(companyIdToRemove);

        return RedirectToAction(nameof(Index));
    }
}
