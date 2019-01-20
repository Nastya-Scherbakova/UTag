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
using UTag.Services.Interfaces;
using UTag.ViewModels;

namespace UTag.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PeopleController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        // GET: api/People
        [HttpGet]
        public IEnumerable<PersonViewModel> GetPersons()
        {
            return _mapper.Map<List<PersonViewModel>>(_personService.GetAll());
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

            return Ok(_mapper.Map<PersonViewModel>(person));
        }

        // PUT: api/People/5
        [HttpPut]
        public IActionResult PutPerson([FromBody] PersonViewModel person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _personService.Update(_mapper.Map<Person>(person));

            return NoContent();
        }

        
    }
}