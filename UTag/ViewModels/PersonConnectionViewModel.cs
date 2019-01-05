using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTag.Models;

namespace UTag.ViewModels
{
    public class PersonConnectionViewModel
    {
        public int Id { get; set; }
        public int FromPersonId { get; set; }
        public int ToPersonId { get; set; }
        public virtual PersonViewModel ToPerson { get; set; }
        public bool PersonToExists { get; set; }
        public FriendStatus FriendStatus { get; set; }
    }
}
