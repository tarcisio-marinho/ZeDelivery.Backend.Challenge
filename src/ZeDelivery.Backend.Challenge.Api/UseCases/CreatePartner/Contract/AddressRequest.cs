using System.Collections.Generic;

namespace ZeDelivery.Backend.Challenge.Api.UseCases.CreatePartner.Contract
{
    //"address": {
    //    "type": "Point",
    //    "coordinates": [-46.57421, -21.785741]
    //  }
    public class AddressRequest
    {
        public string Type { get; set; }
        public IEnumerable<float> Coordinates { get; set; }
    }
}