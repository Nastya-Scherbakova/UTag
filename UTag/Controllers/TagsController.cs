﻿using System;
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
    public class TagsController : ControllerBase
    {
        private readonly UTagContext _context;
        private readonly IMapper _mapper;

        public TagsController(UTagContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Tags
        [HttpGet]
        public IEnumerable<TagViewModel> GetTags()
        {
            return _mapper.Map<List<TagViewModel>>(_context.Tags);
        }
        
        // PUT: api/Tags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag([FromRoute] int id, [FromBody] TagViewModel tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tag.Id)
            {
                return BadRequest();
            }

            _context.Entry(_mapper.Map<Tag>(tag)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
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

        // POST: api/Tags
        [HttpPost]
        public async Task<IActionResult> PostTag([FromBody] TagViewModel tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tags.Add(_mapper.Map<Tag>(tag));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTag", new { id = tag.Id }, tag);
        }

        // POST: api/Tags/TagProduct
        [HttpPost("TagProduct")]
        public async Task<IActionResult> TagProduct([FromBody] ProductTagViewModel tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProductTagConnections.Add(_mapper.Map<ProductTag>(tag));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductTag", new { id = tag.Id }, tag);
        }

        // POST: api/Tags/TagPerson
        [HttpPost("TagPerson")]
        public async Task<IActionResult> TagPerson([FromBody] PersonTagViewModel tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PersonTagConnections.Add(_mapper.Map<PersonTag>(tag));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonTag", new { id = tag.Id }, tag);
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Tags/UntagProduct/3
        [HttpDelete("UntagProduct/{id}")]
        public async Task<IActionResult> UntagProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tag = await _context.ProductTagConnections.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.ProductTagConnections.Remove(tag);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Tags/UntagPerson/3
        [HttpDelete("UntagPerson/{id}")]
        public async Task<IActionResult> UntagPerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tag = await _context.PersonTagConnections.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.PersonTagConnections.Remove(tag);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
}