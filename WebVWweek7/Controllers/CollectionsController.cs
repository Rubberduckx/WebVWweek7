using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebVWweek7.Data;
using WebVWweek7.Models;

namespace WebVWweek7.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly AppDbContext _context;

        public CollectionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Collections
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.collections.Include(c => c.Category);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Collections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.collections == null)
            {
                return NotFound();
            }

            var collection = await _context.collections
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // GET: Collections/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Sort");
            return View();
        }

        // POST: Collections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateOfCollecting,CategoryId")] Collection collection)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(collection);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            _context.Add(collection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Sort", collection.CategoryId);
            return View(collection);
        }

        // GET: Collections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.collections == null)
            {
                return NotFound();
            }

            var collection = await _context.collections.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Sort", collection.CategoryId);
            return View(collection);
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateOfCollecting,CategoryId")] Collection collection)
        {
            if (id != collection.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionExists(collection.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Sort", collection.CategoryId);
            return View(collection);
        }

        // GET: Collections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.collections == null)
            {
                return NotFound();
            }

            var collection = await _context.collections
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.collections == null)
            {
                return Problem("Entity set 'AppDbContext.collections'  is null.");
            }
            var collection = await _context.collections.FindAsync(id);
            if (collection != null)
            {
                _context.collections.Remove(collection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectionExists(int id)
        {
          return (_context.collections?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
