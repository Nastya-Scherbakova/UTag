using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTag.Models
{
    public class Person
    {
        public int Id { get; set; }
        public DateTime BirthDate { get; set; }
        public string About { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<PersonConnection> ConnectedPersons { get; set; }
        public virtual ICollection<ProductConnection> LikedProducts { get; set; }
        public virtual ICollection<ProductConnection> LikedForPersonProducts { get; set; }
        public virtual ICollection<TagConnection> ConnectedTags { get; set; }

    }
}
