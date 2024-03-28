using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_CORE.Data;
using MVC_CORE.Models;

namespace MVC_CORE.Controllers
{
    public class AuthUserController : Controller
    {
        private readonly ExampleDbContext? _context;

        /// <inheritdoc />
        public AuthUserController(ExampleDbContext context)
        {
            _context = context;
        }

        // GET: AuthUserController
        public IActionResult AuthUser(List<AuthUser> authUsers)
        {
            //:TODO: Implement the logic to get the list of AuthUser
            return View(new AuthUser());
        }

        // GET: AuthUserController
        public IActionResult Index()
        {
            var authUsers = _context?.AuthUser?.ToList();
            return View(authUsers);
        }
        // GET: AuthUserController/Details/5
        public IActionResult Details(List<string> selectedUsers)
        {
            var authUsers = _context?.AuthUser?.Where(x => selectedUsers.Contains(x.Id.ToString())).ToList();
            return RedirectToAction("authUser", "AuthUser", new { authUsers = authUsers });
        }

        // GET: AuthUserController/Create
        // POST: AuthUser/Create
        [HttpPost]
        public IActionResult Create(AuthUser authUser)
        {
            if (ModelState.IsValid)
            {
                _context?.AuthUser?.Add(authUser);
                _context?.SaveChanges();
                return RedirectToAction("Index", "authUser"); // Redirect to home or any other action
            }
            return View(authUser); // Return to the view if model state is not valid
        }


    }
}
