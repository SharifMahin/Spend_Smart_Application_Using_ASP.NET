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

        //dispaly the retrive data
        public IActionResult Index()
        {
            var allExpenses = _context.Expenses.ToList(); //store the Data into variable
            var totalExpense = allExpenses.Sum(x => x.Value);
            ViewBag.Expenses = totalExpense;
            return View(allExpenses);
        }

        //Edit the data
        public IActionResult CreateEditExpense(int ? Id)
        {
            if (Id != null)
            {
                var CheckId = _context.Expenses.SingleOrDefault(x => x.Id == Id);
                return View(CheckId);
            }
            return View();
        }

        // delete the data
        public IActionResult DeleteExpense(int Id)
        {
            var CheckId = _context.Expenses.SingleOrDefault(x => x.Id == Id);
            if (CheckId == null) {
                TempData["Message"] = "The Id was not found.";   // Set a message in TempData to display on the next request.
            }
            else
            {
                _context.Expenses.Remove(CheckId);
                _context.SaveChanges();
                TempData["Message"] = "Do you delete the Expense data?";
            }
            return RedirectToAction ("Index");
        }

        //create or store the data
        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if(model.Id == 0)
            {
             _context.Expenses.Add(model);   // add the data into Expenses table of InMemory Db
            }
            else
            {
            _context.Expenses.Update(model);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
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
