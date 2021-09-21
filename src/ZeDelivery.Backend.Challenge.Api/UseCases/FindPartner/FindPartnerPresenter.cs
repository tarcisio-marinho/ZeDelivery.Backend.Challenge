using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Api.UseCases.CreatePartner;
using ZeDelivery.Backend.Challenge.Application.Shared;
using ZeDelivery.Backend.Challenge.Application.UseCases.FindPartner;
using ZeDelivery.Backend.Challenge.Domain.Entities;

namespace ZeDelivery.Backend.Challenge.Api.UseCases.FindPartner
{
    public class FindPartnerPresenter : IFindPartnerOutputPort
    {
        private readonly ILogger<FindPartnerPresenter> logger;
        public IActionResult Result { get; set; }
        public FindPartnerPresenter(ILogger<FindPartnerPresenter> logger)
        {
            this.logger = logger;
        }

        public void PublishPartnerFound(Partner partner)
        {
            logger.LogInformation("Presenter: Parnet found!");

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

        public void PublishPartnerNotFound()
        {
            logger.LogInformation("Presenter: Partner Not Found");
            Result = new NotFoundResult();
        }
    }
}
