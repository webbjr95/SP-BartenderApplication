using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BartenderApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BartenderApplication.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrdersPlacedDbContext ordersContext;

        public OrdersController(OrdersPlacedDbContext opContext)
        {
            ordersContext = opContext;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await ordersContext.Orders.ToListAsync());
        }

        [HttpGet]
        public IActionResult CreateOrder(string drinkId, string drinkTitle, string drinkPrice, int drinkQuantity)
        {
            var price = Double.Parse(drinkPrice);
            var totalCost = price * drinkQuantity;


            var order = new Orders
            {
                OrderId = Guid.NewGuid().ToString(),
                UserId = "GUEST",
                DrinkId = drinkId,
                DrinkTitle = drinkTitle,
                DrinkQuantity = drinkQuantity,
                TotalCost = totalCost,
                OpenStatus = true,
                AssignedTo = null,
                OrderTime = DateTime.Now,
                CloseTime = null
        };

            return View(order);
        }
        

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder([Bind("OrderId,UserId,DrinkId,DrinkTitle,DrinkQuantity,NameForOrder,UserRequest,OrderTime,CloseTime,OpenStatus,AssignedTo,TotalCost")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                ordersContext.Add(orders);
                await ordersContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await ordersContext.Orders.SingleOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrderId,UserId,DrinkId,DrinkTitle,DrinkQuantity,NameForOrder,UserRequest,OrderTime,CloseTime,OpenStatus,AssignedTo,TotalCost")] Orders orders)
        {
            if (id != orders.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ordersContext.Update(orders);
                    await ordersContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderId))
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
            return View(orders);
        }

        private bool OrdersExists(string id)
        {
            return ordersContext.Orders.Any(e => e.OrderId == id);
        }
    }
}
