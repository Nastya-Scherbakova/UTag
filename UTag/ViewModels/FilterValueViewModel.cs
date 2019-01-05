using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTag.Models;

namespace UTag.ViewModels
{
    public class FilterValueViewModel
    {
        public int Id { get; set; }
        public int FilterId { get; set; }
        public int ProductId { get; set; }
        public string Value { get; set; }
        public virtual FilterViewModel Filter { get; set; }
    }
}
