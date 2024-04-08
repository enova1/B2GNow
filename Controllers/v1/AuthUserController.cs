using Microsoft.AspNetCore.Http;
using MVC_CORE.Models;

namespace MVC_CORE.Controllers.v1
{

    /// <summary>
    /// AuthUserController class to handle the AuthUser records in the database.
    /// </summary>
    [AllowAnonymous]
    public class AuthUserController : Controller
    {
        private readonly ExampleDbContext? _context;

        /// <inheritdoc />
        public AuthUserController(ExampleDbContext context)
        {
            _context = context;
        }

        // GET: AuthUserController/AuthUser
        /// <summary>
        /// Display the list of AuthUser records in the database.
        /// </summary>
        /// <param name="authUsers"></param>
        /// <returns></returns>
        public IActionResult AuthUser(List<AuthUser> authUsers)
        {
            //:TODO: Implement the logic to get the list of AuthUser
            return View(new AuthUser());
        }

        // GET: AuthUserController
        /// <summary>
        /// Display the list of AuthUser records in the database. 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var authUsers = _context?.AuthUser?.ToList();
            return View(authUsers);
        }
        
        // GET: AuthUserController/Details
        /// <summary>
        /// Display the details of the selected AuthUser records in the database.
        /// </summary>
        /// <param name="selectedUsers"></param>
        /// <returns></returns>
        public IActionResult Details(List<string> selectedUsers)
        {
            var authUsers = _context?.AuthUser?.Where(x => selectedUsers.Contains(x.Id.ToString())).ToList();
            return RedirectToAction("authUser", "AuthUser", new { authUsers });
        }

        // GET: AuthUserController/Create
        /// <summary>
        /// Create a new AuthUser record in the database
        /// </summary>
        /// <param name="authUser"></param>
        /// <returns></returns>
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
