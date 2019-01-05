using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTag.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageBase64 { get; set; }
        public string Link { get; set; }
        public string About { get; set; }
        public virtual ICollection<FilterValueViewModel> FilterValues { get; set; }
        public virtual ICollection<ProductTagViewModel> ConnectedTags { get; set; }
    }
}
