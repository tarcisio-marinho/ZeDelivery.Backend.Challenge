using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Domain.Entities
{
    public class Polygon
    {
        public IList<Point> Points { get; private set; }
        public Polygon(IList<Point> points)
        {
            Points = points;
        }

        // TODO: adjust ToString
        public override string ToString()
        {
            return $@"POLYGON(
                    ({string.Join(",", Points)})
                    )";
        }
    }
}
