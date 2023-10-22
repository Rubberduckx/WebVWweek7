using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVWweek7.Data;
using WebVWweek7.Models;

namespace WebVWweek7.Controllers.NewFolder
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsControllerAPI : ControllerBase
    {
        private readonly AppDbContext _context;

        public CollectionsControllerAPI(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CollectionsControllerAPI
        [HttpGet("Finds Collection by status.")]
        public async Task<ActionResult<IEnumerable<Collection>>> Getcollections()
        {
          if (_context.collections == null)
          {
              return NotFound();
          }
            return await _context.collections.ToListAsync();
        }

        // GET: api/CollectionsControllerAPI/5
        [HttpGet("{id} Finds Collection by Id.")]
        public async Task<ActionResult<Collection>> GetCollection(int id)
        {
          if (_context.collections == null)
          {
              return NotFound();
          }
            var collection = await _context.collections.FindAsync(id);

            if (collection == null)
            {
                return NotFound();
            }

            return collection;
        }

        // PUT: api/CollectionsControllerAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id} Update existing Collection by Id.")]
        public async Task<IActionResult> PutCollection(int id, Collection collection)
        {
            if (id != collection.Id)
            {
                return BadRequest();
            }

            _context.Entry(collection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectionExists(id))
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

        // POST: api/CollectionsControllerAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Updates a Collection with data.")]
        public async Task<ActionResult<Collection>> PostCollection(Collection collection)
        {
          if (_context.collections == null)
          {
              return Problem("Entity set 'AppDbContext.collections'  is null.");
          }
            _context.collections.Add(collection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollection", new { id = collection.Id }, collection);
        }

        // DELETE: api/CollectionsControllerAPI/5
        [HttpDelete("{id} Delets a Collection by Id.")]
        public async Task<IActionResult> DeleteCollection(int id)
        {
            if (_context.collections == null)
            {
                return NotFound();
            }
            var collection = await _context.collections.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }

            _context.collections.Remove(collection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CollectionExists(int id)
        {
            return (_context.collections?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
