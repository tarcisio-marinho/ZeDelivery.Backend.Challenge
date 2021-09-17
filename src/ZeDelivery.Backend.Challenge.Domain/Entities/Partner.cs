using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Domain.Entities.Dtos;
using ZeDelivery.Backend.Challenge.Domain.Services;

namespace ZeDelivery.Backend.Challenge.Domain.Entities
{
    public class Partner
    {
        public Partner(string id, string tradingName, string ownerName, string document, string CoverageAreaType, string CoverageAreaCoordinates, string AddressType, string AddressCoordinates)
        {
            Id = id;
            TradingName = tradingName;
            OwnerName = ownerName;
            Document = document;
            CoverageArea = new CoverageArea(CoverageAreaType, GeoJsonParser.ParsePolygonFromText(CoverageAreaCoordinates));
            Address = new Address(GeoJsonParser.ParsePointFromText(AddressCoordinates), AddressType);
        }
        
        public Partner(string id, string tradingName, string ownerName, string document, CoverageArea coverageArea, Address address)
        {
            Id = id;
            TradingName = tradingName;
            OwnerName = ownerName;
            Document = document;
            CoverageArea = coverageArea;
            Address = address;
        }

        public string Id { get; private set; }
        public string TradingName { get; private set; }
        public string OwnerName { get; private set; }
        public string Document { get; private set; }
        public CoverageArea CoverageArea { get; private set; }
        public Address Address { get; private set; }

        public PartnerDto ToDto()
        {
            return new PartnerDto
            {
                Id = Id,
                TradingName = TradingName,
                OwnerName = OwnerName,
                Document = Document,
                CoverageAreaCoordinates = CoverageArea.Coordinates,
                CoverageAreaType = CoverageArea.Type,
                AddressCoordinates = Address.Coordinates,
                AddressType = Address.Type
            };
        }
    }
}
