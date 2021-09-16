namespace ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner
{
    public class CreatePartnerInput : TInput
    {
        public string Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public CoverageAreaInput CoverageArea { get; set; }
        public AddressRequestInput Address { get; set; }
    }
}