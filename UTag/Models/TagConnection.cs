using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTag.Models
{
    public class TagConnection
    {
        public int Id { get; set; }
        public int ConnectToId { get; set; }
        public int TagId { get; set; }
        public ConnectToType ConnectToType { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual object ConnectedTo { get; set; }
    }

    public enum ConnectToType
    {
        Person,
        Product
    }
}
