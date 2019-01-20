using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTag.Helpers;
using UTag.Models;
using UTag.ViewModels;

namespace UTag.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FiltersController : ControllerBase
    {
        private readonly UTagContext _context;
        private readonly IMapper _mapper;

        public FiltersController(UTagContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Filters
        [HttpGet]
        public IEnumerable<FilterViewModel> GetFilters()
        {
            return _mapper.Map<List<FilterViewModel>>(_context.Filters);
        }

        // PUT: api/Filters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilter([FromRoute] int id, [FromBody] FilterViewModel filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != filter.Id)
            {
                return BadRequest();
            }



            _context.Entry(_mapper.Map<Filter>(filter)).State = EntityState.Modified;

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
        public async Task<IActionResult> PostFilter([FromBody] FilterViewModel filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Filters.Add(_mapper.Map<Filter>(filter));
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

            return Ok();
        }

        private bool FilterExists(int id)
        {
            return _context.Filters.Any(e => e.Id == id);
        }
    }
}