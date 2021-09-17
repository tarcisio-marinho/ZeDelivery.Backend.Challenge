using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Domain.Entities;

namespace ZeDelivery.Backend.Challenge.Infrastructure.Database.Queries
{
    public class CheckIfPointIsInsidePolygonQuery
    {
        public CheckIfPointIsInsidePolygonQuery()
        {

        }

        public async Task<bool> ExecuteAsync(Point point)
        {
        //https://stackoverflow.com/questions/5105035/get-polygon-points-mysql
            //SELECT ST_Contains(PolygonFromText('POLYGON((
            //    9.190586853 45.464518970,
            //    9.190602686 45.463993916,
            //    9.191572471 45.464001929,
            //    9.191613325 45.463884676,
            //    9.192136130 45.463880767,
            //    9.192111509 45.464095594,
            //    9.192427961 45.464117804,
            //    9.192417811 45.464112862,
            //    9.192509035 45.464225851,
            //    9.192493139 45.464371079,
            //    9.192448471 45.464439002,
            //    9.192387444 45.464477861,
            //    9.192051402 45.464483037,
            //    9.192012814 45.464643592,
            //    9.191640825 45.464647090,
            //    9.191622331 45.464506215,
            //    9.190586853 45.464518970))'), PointFromText("POINT(10 42)")
            //    );
            return true;

        }
    }
}
