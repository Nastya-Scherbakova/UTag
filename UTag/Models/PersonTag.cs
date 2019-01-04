using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTag.Models
{
    public class PersonTag
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int TagId { get; set; }
        public virtual Person Person { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
