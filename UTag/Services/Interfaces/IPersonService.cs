using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTag.Models;

namespace UTag.Services.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<Person> GetAll();
        Task<Person> GetById(int id);
        Task<Person> Create(User user);
        void Update(Person user);
        void Delete(int id);
    }
}
