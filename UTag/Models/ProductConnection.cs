using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTag.Models
{
    public class ProductConnection
    {
        public int Id { get; set; }
        public int PersonFromId { get; set; }
        public int PersonToId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Person PersonFrom { get; set; }
        public virtual Person PersonTo { get; set; }
    }
}
