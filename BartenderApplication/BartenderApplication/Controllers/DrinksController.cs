using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BartenderApplication.Models;

namespace BartenderApplication.Controllers
{
    public class DrinksController : Controller
    {
        private readonly DrinkMenuDbContext _context;

        public DrinksController(DrinkMenuDbContext context)
        {
            _context = context;
        }

        // GET: Drinks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Drinks.ToListAsync());
        }

        // GET: Drinks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinks = await _context.Drinks
                .SingleOrDefaultAsync(m => m.DrinkId == id);
            if (drinks == null)
            {
                return NotFound();
            }

            return View(drinks);
        }

        // GET: Drinks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DrinkId,Title,Description,Price,Ingredients")] Drinks drinks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drinks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drinks);
        }

        // GET: Drinks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinks = await _context.Drinks.SingleOrDefaultAsync(m => m.DrinkId == id);
            if (drinks == null)
            {
                return NotFound();
            }
            return View(drinks);
        }

        // POST: Drinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DrinkId,Title,Description,Price,Ingredients")] Drinks drinks)
        {
            if (id != drinks.DrinkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drinks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinksExists(drinks.DrinkId))
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
            return View(drinks);
        }

        // GET: Drinks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinks = await _context.Drinks
                .SingleOrDefaultAsync(m => m.DrinkId == id);
            if (drinks == null)
            {
                return NotFound();
            }

            return View(drinks);
        }

        // POST: Drinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var drinks = await _context.Drinks.SingleOrDefaultAsync(m => m.DrinkId == id);
            _context.Drinks.Remove(drinks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinksExists(string id)
        {
            return _context.Drinks.Any(e => e.DrinkId == id);
        }
    }
}
