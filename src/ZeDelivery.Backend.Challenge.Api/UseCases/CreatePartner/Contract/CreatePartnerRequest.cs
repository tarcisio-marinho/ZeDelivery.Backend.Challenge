using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Api.UseCases.CreatePartner.Contract
{
    //{
    //  "id": 1, 
    //  "tradingName": "Adega da Cerveja - Pinheiros",
    //  "ownerName": "Zé da Silva",
    //  "document": "1432132123891/0001",
    //  "coverageArea": { 
    //    "type": "MultiPolygon", 
    //    "coordinates": [
    //      [[[30, 20], [45, 40], [10, 40], [30, 20]]], 
    //      [[[15, 5], [40, 10], [10, 20], [5, 10], [15, 5]]]
    //    ]
    //  },
    //  "address": {
    //    "type": "Point",
    //    "coordinates": [-46.57421, -21.785741]
    //  }
    //}


    public class CreatePartnerRequest
    {
        public string Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public CoverageAreaRequest CoverageArea { get; set; }
        public AddressRequest Address { get; set; }
    }
}
