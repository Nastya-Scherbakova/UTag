using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTag.Models
{
    public class Filter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dimension { get; set; }
        public virtual ICollection<FilterValue> FilterValues { get; set; }
    }
}
