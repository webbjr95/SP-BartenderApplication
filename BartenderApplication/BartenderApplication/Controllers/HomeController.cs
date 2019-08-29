using BartenderApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace BartenderApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly DrinkMenuDbContext menuContext;

        public HomeController(DrinkMenuDbContext context)
        {
            menuContext = context;
        }

        public IActionResult Index()
        {
            var drinksMenu = menuContext.Drinks.ToList();

            return View(drinksMenu);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
