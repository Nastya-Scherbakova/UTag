using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTag.ViewModels
{
    public class ProductTagViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int TagId { get; set; }
        public virtual TagViewModel Tag { get; set; }
    }
}
