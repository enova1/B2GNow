using MVC_CORE.Models;
using System.Diagnostics;

namespace MVC_CORE.Controllers
{
    /// <summary>
    /// HomeController class to handle the home page of the application
    /// </summary>
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <inheritdoc />
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: Home
        /// <summary>
        /// Display the home page of the application when the user clicks on the home link.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        // GET: Home/Privacy
        /// <summary>
        /// show the privacy page of the application when the user clicks on the privacy link.
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Home/Error
        /// <summary>
        /// Show the error page of the application when an error occurs in the application.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
