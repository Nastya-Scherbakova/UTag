using System.Collections.Generic;
using System.Threading.Tasks;
using UTag.Models;
using UTag.ViewModels;

namespace UTag.Services.Interfaces
{
    public interface IMLService
    {
        Task<IEnumerable<Product>> GetReccomendedProducts(Person person);
    }
}
