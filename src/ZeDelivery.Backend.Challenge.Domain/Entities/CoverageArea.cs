using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Domain.Entities
{
    public class CoverageArea
    {
        public CoverageArea(string type, Polygon coordinates)
        {
            Type = type;
            Coordinates = coordinates;
        }

        public string Type { get; private set; }
        public Polygon Coordinates { get; private set; }
    }
}
