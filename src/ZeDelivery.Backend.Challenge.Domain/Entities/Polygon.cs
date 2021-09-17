using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Domain.Entities
{
    public class Polygon : IGeoJson
    {
        public IList<Point> Points { get; private set; }
        public Polygon(IList<Point> points)
        {
            Points = points;
        }

        public IList<IList<float>> ToList()
        {
            var list = new List<IList<float>>();

            Points.ToList().ForEach(point => {
                var pointList = new List<float>();
                pointList.Add(point.Latitude);
                pointList.Add(point.Longitude);
                list.Add(pointList);
            });

            return list;
        }

        public string ToGeometry()
        {
            return $@"POLYGON(({string.Join(",", Points)}))";
        }
    }
}
