using System.Collections.Generic;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner
{
    public class AddressRequestInput
    {
        public string Type { get; set; }
        public IEnumerable<float> Coordinates { get; set; }
    }
}