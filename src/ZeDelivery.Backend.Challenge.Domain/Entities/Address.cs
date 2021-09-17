using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Domain.Entities
{
    public class Address
    {
        public string Type { get; private set; }
        public Point Coordinates { get; private set; }

        public Address(Point coordinates, string type)
        {
            Coordinates = coordinates;
            Type = type;
        }
    }
}
