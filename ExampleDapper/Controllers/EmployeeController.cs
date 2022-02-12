using ExampleDapper.Data;
using ExampleDapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExampleDapper.Repository;

namespace ExampleDapper.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(ICompanyRepository companyRepository,
                                  IEmployeeRepository employeeRepository)
        {
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            return View(await _employeeRepository.GetAll());
        }

        // GET: Employee/Create
        public async Task<IActionResult> Create()
        {
            var companyList = (await _companyRepository.GetAll()).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CompanyId.ToString()
            });

            ViewBag.CompanyList = companyList;

            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            ModelState.Remove("Company");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _employeeRepository.Add(employee);
            return RedirectToAction(nameof(Index));
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.Find(id);

            var companyList = (await _companyRepository.GetAll()).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CompanyId.ToString()
            });

            ViewBag.CompanyList = companyList;

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            ModelState.Remove("Company");

            if (employee == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _employeeRepository.Update(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
