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
            Result = new OkObjectResult(new { Partner = JsonConvert.SerializeObject(partner) });
        }

        public async void PublishValidationErros(Notification notification)
        {
            Result = new UnprocessableEntityObjectResult(new { Errors = notification.GetErrors() });
        }

        public async void PublishInternalServerError()
        {
            logger.LogInformation("Unknown Error occurred!");
            Result = new BadRequestObjectResult(new { Error = "Unknown Server Error" })
            {
                StatusCode = 500
            };
        }

        public void PublishPartnerNotFound()
        {
            Result = new NotFoundResult();
        }
    }
}
