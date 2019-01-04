using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTag.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageBase64 { get; set; }
        public string Link { get; set; }
        public string About { get; set; }
        public virtual ICollection<FilterValue> FilterValues { get; set; }
        public virtual ICollection<TagConnection> ConnectedTags { get; set; }
    }
}