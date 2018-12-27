using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTag.Models
{
    public class PersonConnection
    {
        public int Id { get; set; }
        public int FromPersonId { get; set; }
        public int ToPersonId { get; set; }
        public virtual Person FromPerson { get; set; }
        public virtual Person ToPerson { get; set; }
        public bool PersonToExists { get; set; }
        public FriendStatus FriendStatus { get; set; }
    }

    public enum FriendStatus
    {
        Waiting, //ToPerson didn't answered yet
        Accepted, //Accepted friend invite
        Subscriber, //ToPerson declined, so FromPerson connection type is subscriber
        Declined //Connection when for FromPerson Subscriber, for ToPerson - declined
    }
}
