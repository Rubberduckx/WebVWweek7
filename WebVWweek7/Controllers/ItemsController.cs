﻿using System;
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
    public class ItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(int? CollectionId)
        {
            Counter();

            if (CollectionId != null)
            {
                var AppDbContext = _context.items
                    .Include(v => v.Collection)
                    .Where(o => o.CollectionId == CollectionId);

                return View(await AppDbContext.ToListAsync());
            }
            else
            {
                var appDbContext = _context.items.Include(c => c.Collection);
                return View(await appDbContext.ToListAsync());
            }
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.items == null)
            {
                return NotFound();
            }

            var item = await _context.items
                .Include(i => i.Collection)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CollectionId"] = new SelectList(_context.collections, "Id", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemName,ItemDescription,Price,Worth,BuyDate,CollectionId")] Item item)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(item);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            _context.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            ViewData["CollectionId"] = new SelectList(_context.collections, "Id", "Name", item.CollectionId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.items == null)
            {
                return NotFound();
            }

            var item = await _context.items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CollectionId"] = new SelectList(_context.collections, "Id", "Name", item.CollectionId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemName,ItemDescription,Price,Worth,BuyDate,CollectionId")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            ViewData["CollectionId"] = new SelectList(_context.collections, "Id", "Name", item.CollectionId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.items == null)
            {
                return NotFound();
            }

            var item = await _context.items
                .Include(i => i.Collection)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.items == null)
            {
                return Problem("Entity set 'AppDbContext.items'  is null.");
            }
            var item = await _context.items.FindAsync(id);
            if (item != null)
            {
                _context.items.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
          return (_context.items?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public void Counter()
        {
            ViewBag.Count = _context.items.Count();
        }
    }
}
