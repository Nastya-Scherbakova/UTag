using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTag.Helpers;
using UTag.Models;
using UTag.Services.Interfaces;

namespace UTag.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PeopleController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET: api/People
        [HttpGet]
        public IEnumerable<Person> GetPersons()
        {
            return _personService.GetAll();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _personService.GetById(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/People/5
        [HttpPut]
        public IActionResult PutPerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            _personService.Update(person);

            

            return NoContent();
        }

        
    }
}