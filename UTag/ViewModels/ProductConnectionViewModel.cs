using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTag.ViewModels
{
    public class ProductConnectionViewModel
    {
        public int Id { get; set; }
        public int PersonFromId { get; set; }
        public int PersonToId { get; set; }
        public int ProductId { get; set; }
        public virtual ProductViewModel Product { get; set; }
    }
}
