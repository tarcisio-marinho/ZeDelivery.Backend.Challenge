using System.Collections.Generic;

namespace ZeDelivery.Backend.Challenge.Api.UseCases.CreatePartner.Contract
{
    //"coverageArea": { 
    //    "type": "MultiPolygon", 
    //    "coordinates": [
    //      [[[30, 20], [45, 40], [10, 40], [30, 20]]], 
    //      [[[15, 5], [40, 10], [10, 20], [5, 10], [15, 5]]]
    //    ]
    //  }

    public class CoverageAreaRequest
    {
        public string Type { get; set; }
        public IEnumerable<IEnumerable<IEnumerable<IEnumerable<float>>>> Coordinates { get; set; }
    }
}