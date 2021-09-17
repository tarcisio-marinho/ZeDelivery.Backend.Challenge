using Newtonsoft.Json;
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


        public static Point ParsePointFromText(string coordinates)
        {
            try
            {
                var points = coordinates.Replace("POINT", "").Replace("(", "").Replace(")", "").Split(" ");
            
                float.TryParse(points[0], System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out var lat);

                float.TryParse(points[1], System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out var @long);
                return new Point(lat, @long);
            }catch(Exception e)
            {
                return null;
            }
        }

        public static Polygon ParsePolygonFromText(string coordinates)
        {
            try
            {
                var newCoordinates = coordinates.Replace("POLYGON", "").Replace("((", "").Replace("))", "");

                IList<Point> points = new List<Point>();

                newCoordinates.Split(",").ToList().ForEach(element => {
                    var splited = element.Split(" ");

                    float.TryParse(splited[0], System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out var lat);
                    float.TryParse(splited[1], System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out var @long);

                    points.Add(new Point(lat, @long));
                });

                return new Polygon(points);
            }
            catch(Exception e)
            {
                return null;
            }
            
        }
    }
}
