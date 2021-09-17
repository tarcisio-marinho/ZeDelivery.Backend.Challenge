using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Domain.Entities;

namespace ZeDelivery.Backend.Challenge.Domain.Entities.Dtos
{
    public class PartnerDto
    {
        public string Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public string CoverageAreaType { get; set; }
        public Polygon CoverageAreaCoordinates { get; set; }
        public string AddressType { get; set; }
        public Point AddressCoordinates { get; set; }

        public Partner ToPartner()
        {
            return new Partner(Id, TradingName, OwnerName, Document, new CoverageArea(CoverageAreaType, CoverageAreaCoordinates), new Address(AddressCoordinates, AddressType));
        }

    }
}
