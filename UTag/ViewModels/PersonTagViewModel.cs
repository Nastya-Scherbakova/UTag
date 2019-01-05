using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTag.ViewModels
{
    public class PersonTagViewModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int TagId { get; set; }
        public virtual TagViewModel Tag { get; set; }
    }
}
