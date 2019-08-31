using BartenderApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace BartenderApplication.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly DrinkMenuDbContext menuContext;
        private readonly OrdersPlacedDbContext orderContext;

        public HomeController(DrinkMenuDbContext dmContext)
        {
            menuContext = dmContext;
        }

        public IActionResult Index()
        {
            var drinksMenu = menuContext.Drinks.ToList();

            if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Bartender"))
            {
                return RedirectToAction("Index", "Orders");
            }
            else
            {
                return View(drinksMenu);
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
