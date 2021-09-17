using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Domain.Entities
{
    public class Point
    {
        public float Latitude { get; private set; }
        public float Longitude { get; private set; }

        public Point(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return $"POINT({Latitude} {Longitude})";
        }
    }
}
