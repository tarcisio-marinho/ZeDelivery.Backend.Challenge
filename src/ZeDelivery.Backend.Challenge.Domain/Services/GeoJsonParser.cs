using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Domain.Entities;

namespace ZeDelivery.Backend.Challenge.Domain.Services
{
    public static class GeoJsonParser
    {
        public static Point ParsePoint(IEnumerable<float> point)
        {
            return new Point(point.ToList()[0], point.ToList()[1]);
        }

        public static Polygon ParsePolygon(IEnumerable<IEnumerable<IEnumerable<IEnumerable<float>>>> point)
        {
            IList<Point> points = new List<Point>();
            point.FirstOrDefault().FirstOrDefault().ToList().ForEach(list => points.Add(new Point(list.ToList()[0], list.ToList()[1])));

            return new Polygon(points);
        }
    }
}
