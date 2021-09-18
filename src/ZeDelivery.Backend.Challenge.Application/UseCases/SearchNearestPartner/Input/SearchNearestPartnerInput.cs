using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.SearchNearestPartner.Input
{
    public class SearchNearestPartnerInput : TInput
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
