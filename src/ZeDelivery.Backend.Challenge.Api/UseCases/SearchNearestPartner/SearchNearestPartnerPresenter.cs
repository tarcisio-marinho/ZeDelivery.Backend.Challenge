using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using ZeDelivery.Backend.Challenge.Application.Shared;
using ZeDelivery.Backend.Challenge.Application.UseCases.SearchNearestPartner;
using ZeDelivery.Backend.Challenge.Domain.Entities;

namespace ZeDelivery.Backend.Challenge.Api.UseCases.SearchNearestPartner
{
    [ExcludeFromCodeCoverage]
    public class SearchNearestPartnerPresenter : ISearchNearestPartnerOutputPort
    {
        private readonly ILogger<SearchNearestPartnerPresenter> logger;
        public IActionResult Result { get; set; }
        public SearchNearestPartnerPresenter(ILogger<SearchNearestPartnerPresenter> logger)
        {
            this.logger = logger;
        }

        public void PublishNearestPartner(Partner partner)
        {
            logger.LogInformation("Presenter: nearest published!");
            var response = new
            {
                Id = partner.Id,
                TradingName = partner.TradingName,
                OwnerName = partner.OwnerName,
                Document = partner.Document,
                CoverageArea = new
                {
                    Type = partner.CoverageArea.Type,
                    Coordinates = new[] { new[] { partner.CoverageArea.Coordinates.ToList() } }
                },
                Address = new
                {
                    Type = partner.Address.Type,
                    Coordinates = new[] { partner.Address.Coordinates.Latitude, partner.Address.Coordinates.Longitude }
                }
            };

            Result = new OkObjectResult(response);
        }

        public async void PublishValidationErros(Notification notification)
        {
            logger.LogInformation("Presenter: Validation Errors occurred!");
            Result = new UnprocessableEntityObjectResult(new { Errors = notification.GetErrors() });
        }

        public async void PublishInternalServerError()
        {
            logger.LogInformation("Presenter: Unknown Error occurred!");
            Result = new BadRequestObjectResult(new { Error = "Unknown Server Error" })
            {
                StatusCode = 500
            };
        }

        public void PublishNoNearestPartnerFound()
        {
            logger.LogInformation("Presenter: No nearest partner found!");
            Result = new NotFoundResult();
        }
    }
}