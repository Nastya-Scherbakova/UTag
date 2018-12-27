using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTag.Models
{
    public class FilterValue
    {
        public int Id { get; set; }
        public int FilterId { get; set; }
        public int ProductId { get; set; }
        public string Value { get; set; }

        public virtual Filter Filter { get; set; }
        public virtual Product Product { get; set; }
    }
}
