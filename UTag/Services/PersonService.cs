using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTag.Helpers;
using UTag.Models;
using UTag.Services.Interfaces;

namespace UTag.Services
{
    public class PersonService : IPersonService
    {
        private readonly UTagContext _context;

        public PersonService(UTagContext context)
        {
            _context = context;
        }
        public async Task<Person> Create(User user)
        {
            var person = new Person()
            {
                UserId = user.Id
            };
            await _context.AddAsync(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async void Delete(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            }
            
        }

        public IEnumerable<Person> GetAll()
        {
            return _context.Persons;
        }

        public async Task<Person> GetById(int id)
        {
            return await _context.Persons.FindAsync(id);
        }

        public async void Update(Person user)
        {
            _context.Persons.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
