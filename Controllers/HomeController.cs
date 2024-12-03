using Microsoft.AspNetCore.Mvc;
using spendSmart.Models;
using System.Diagnostics;

namespace spendSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SpendSmartDBContext _context;

        public HomeController(ILogger<HomeController> logger, SpendSmartDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expense()
        {
            var allExpenses = _context.Expenses.ToList(); //store the Data here
            return View(allExpenses);
        }

        public IActionResult CreateEditExpense()
        {
            return View();
        }

        public IActionResult CreateEditExpenseForm(Expense model)
        {
            _context.Expenses.Add(model);
            _context.SaveChanges();
            return RedirectToAction("CreateEditExpense");
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
    }
}
