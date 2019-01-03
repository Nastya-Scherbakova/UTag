using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTag.Helpers;
using UTag.Models;

namespace UTag.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltersController : ControllerBase
    {
        private readonly UTagContext _context;

        public FiltersController(UTagContext context)
        {
            _context = context;
        }

        // GET: api/Filters
        [HttpGet]
        public IEnumerable<Filter> GetFilters()
        {
            return _context.Filters;
        }

        // GET: api/Filters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilter([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var filter = await _context.Filters.FindAsync(id);

            if (filter == null)
            {
                return NotFound();
            }

            return Ok(filter);
        }

        // PUT: api/Filters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilter([FromRoute] int id, [FromBody] Filter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != filter.Id)
            {
                return BadRequest();
            }

            _context.Entry(filter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilterExists(id))
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

        // POST: api/Filters
        [HttpPost]
        public async Task<IActionResult> PostFilter([FromBody] Filter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Filters.Add(filter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilter", new { id = filter.Id }, filter);
        }

        // DELETE: api/Filters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilter([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var filter = await _context.Filters.FindAsync(id);
            if (filter == null)
            {
                return NotFound();
            }

            _context.Filters.Remove(filter);
            await _context.SaveChangesAsync();

            return Ok(filter);
        }

        private bool FilterExists(int id)
        {
            return _context.Filters.Any(e => e.Id == id);
        }
    }
}