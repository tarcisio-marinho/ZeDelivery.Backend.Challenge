﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Domain.Entities
{
    public class Partner
    {
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
    }
}