using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVWweek7.Data;
using WebVWweek7.Models;

namespace WebApplicationCatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet("Finds Item by status.")]
        public async Task<ActionResult<IEnumerable<Item>>> Getitems()
        {
          if (_context.items == null)
          {
              return NotFound();
          }
            return await _context.items.ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id} Finds item by Id.")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
          if (_context.items == null)
          {
              return NotFound();
          }
            var item = await _context.items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id} Update existing Item by Id.")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Updates a Item with data.")]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
          if (_context.items == null)
          {
              return Problem("Entity set 'AppDbContext.items'  is null.");
          }
            _context.items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id} Delets a Item by Id.")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            if (_context.items == null)
            {
                return NotFound();
            }
            var item = await _context.items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return (_context.items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
