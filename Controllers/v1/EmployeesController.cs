namespace MVC_CORE.Controllers.v1
{
    /// <summary>
    /// Employees Controller for the Employees view.
    /// </summary>
    [AllowAnonymous]
    public class EmployeesController : Controller
    {
        private readonly ExampleDbContext _context;

        /// <inheritdoc />
        public EmployeesController(ExampleDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        /// <summary>
        /// Display the list of employees with their addresses and phone numbers in the database sorted by first name, then by last name.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            // Please display a distinct list of all the employees
            // including their address and phone numbers in the database,
            // sorted by first name, then by last name.

            var employees = await _context.Employees!
                .Include(e => e.EmployeePhones)
                .Include(e => e.EmployeeAddresses)
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToListAsync();

            employees = employees.Distinct().ToList();
            return View(employees);
        }

        // POST: Employees/Filter
        /// <summary>
        /// Filter the employees by phone number and zip code and display the results in the view Index view.
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Filter(string phone, string zipCode)
        {

            // Take the user’s input of a phone number and/or ZIP code and display any employees,
            // their addresses, and their phone numbers where the phone number
            // and/or ZIP code matches the user’s input.

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

        // GET: Employees/DisplayList
        /// <summary>
        /// Display the list of employees with their full name, earliest hire date, latest hire date, and average length of employment in years. 
        /// </summary>
        /// <returns></returns>
        public Task<IActionResult> DisplayList()
        {
            // display another list of employees with the full name, earliest hire date,
            // latest hire date, and average length of employment in years.
            // No filters are needed for this list.

            var employeeInfo = _context.Employees!
                .Select(e => new
                {
                    FullName = $"{e.FirstName} {e.LastName}",
                    EarliestHireDate = e.HireDate,
                    LatestHireDate = e.HireDate,
                    AverageLengthOfEmployment = (DateTime.Now - e.HireDate).TotalDays / 365
                })
                .ToList();

            return Task.FromResult<IActionResult>(View("Details"));
        }

        // GET: Employees/Details/5
        /// <summary>
        /// Display the Details of an employee with their addresses and phone numbers.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        // GET: Employees/Create
        /// <summary>
        /// Create a new employee with their addresses and phone numbers.
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        public async Task<IActionResult> Create(Employees employees)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

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

            if (employees.EmployeePhones != null)
                foreach (var number in employees.EmployeePhones)
                {
                    saveData.EmployeePhones!.Add(number);
                }

            if (employees.EmployeeAddresses != null)
                foreach (var addy in employees.EmployeeAddresses)
                {
                    saveData.EmployeeAddresses!.Add(addy);
                }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        // GET: Employees/Edit/5
        /// <summary>
        /// get the employee to edit in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// save the edited employee in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employees"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employees employees)
        {
            if (ModelState.IsValid)
            {
                if (id != employees.EmployeeId)
                {
                    return NotFound();
                }

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
        /// <summary>
        /// get the employee to delete from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// delete the employee from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// check if the employee exists in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
