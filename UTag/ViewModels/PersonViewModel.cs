using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTag.Models;

namespace UTag.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public DateTime BirthDate { get; set; }
        public string About { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public virtual ICollection<PersonConnectionViewModel> ConnectedPersons { get; set; }
        public virtual ICollection<ProductConnectionViewModel> LikedProducts { get; set; }
        public virtual ICollection<ProductConnectionViewModel> LikedForPersonProducts { get; set; }
        public virtual ICollection<PersonTagViewModel> ConnectedTags { get; set; }
    }
}
