using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_CORE.Data;
using MVC_CORE.Models.Employee;

namespace MVC_CORE.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ExampleDbContext _context;

        /// <inheritdoc />
        public EmployeesController(ExampleDbContext context)
        {
            _context = context;
        }
        
        // Please display a distinct list of all the employees
        // including their address and phone numbers in the database,
        // sorted by first name, then by last name.
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees!
                .Include(e => e.EmployeePhones)
                .Include(e => e.EmployeeAddresses)
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToListAsync();
                
            employees = employees.Distinct().ToList();

            return View(employees);
        }
        
        // Take the user’s input of a phone number and/or ZIP code and display any employees,
        // their addresses, and their phone numbers where the phone number
        // and/or ZIP code matches the user’s input.
        [HttpPost]
        public IActionResult Filter(string phone, string zipCode)
        {
            var employees = _context.Employees!
                .Include(e => e.EmployeePhones)
                .Include(e => e.EmployeeAddresses)
                .Where(e => e.EmployeePhones.Any(p => p.PhoneNumber.Contains(phone)))
                .Where(e => e.EmployeeAddresses.Any(a => a.ZipCode.Contains(zipCode)))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            return View("Index", employees);
        }

        // display another list of employees with the full name, earliest hire date,
        // latest hire date, and average length of employment in years.
        // No filters are needed for this list.
        public Task<IActionResult> DisplayList()
        {
            var employeeInfo = _context.Employees!
                .Select(e => new
                {
                    FullName = $"{e.FirstName} {e.LastName}" ,
                    EarliestHireDate = e.HireDate,
                    LatestHireDate = e.HireDate,
                    AverageLengthOfEmployment = (DateTime.Now - e.HireDate).TotalDays / 365
                })
                .ToList();

            return View(employeeInfo);
        }

        //=======================================================================================================

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees.FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,HireDate")] Employees employees)
        {
            //Todo: Add IsValid check to middle where follow D.R.Y best practices
   //         if (ModelState.IsValid)
    //        {
                _context.Add(employees);
                await _context.SaveChangesAsync();

                var saveData = await _context.Employees!
                    .Include(e => e.EmployeePhones)
                    .Include(e => e.EmployeeAddresses)
                    .FirstOrDefaultAsync(m => m.EmployeeId == employees.EmployeeId);

                if (saveData == null)
                {
                    return NotFound();
                }

                foreach (var number in employees.EmployeePhones)
                {
                    saveData.EmployeePhones.Add(number);
                }

                foreach (var addy in employees.EmployeeAddresses)
                {
                    saveData.EmployeeAddresses.Add(addy);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
   //         }
            return View(employees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,HireDate")] Employees employees)
        {
            if (id != employees.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employees);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employees = await _context.Employees.FindAsync(id);
            if (employees != null)
            {
                _context.Employees.Remove(employees);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
