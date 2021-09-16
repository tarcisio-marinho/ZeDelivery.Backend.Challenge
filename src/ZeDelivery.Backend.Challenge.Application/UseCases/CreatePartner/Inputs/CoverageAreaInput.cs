using System.Collections.Generic;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner
{
    public class CoverageAreaInput
    {
        public string Type { get; set; }
        public IEnumerable<IEnumerable<IEnumerable<IEnumerable<float>>>> Coordinates { get; set; }
    }
}