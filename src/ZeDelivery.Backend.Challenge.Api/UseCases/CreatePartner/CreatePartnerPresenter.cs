using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.Shared;
using ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner;

namespace ZeDelivery.Backend.Challenge.Api.UseCases.CreatePartner
{
    public class CreatePartnerPresenter : ICreatePartnerOutputPort
    {

        private readonly ILogger<CreatePartnerPresenter> logger;
        public IActionResult Result { get; set; }

        public CreatePartnerPresenter(ILogger<CreatePartnerPresenter> logger)
        {
            this.logger = logger;
            logger.LogInformation($"instanciado presenter: {Guid.NewGuid().ToString()}");
        }

        public async void PublishPartnerCreated()
        {
            logger.LogInformation("Partner created !");
            Result = new OkResult();
        }

        public async void PublishValidationErros(Notification notification) 
        {
            Result = new UnprocessableEntityObjectResult(new { Errors = notification.GetErrors() });
        }

        public async void PublishDuplicatedPartner()
        {
            Result = new BadRequestObjectResult(new { });
        }

        public async void PublishInternalServerError()
        {
            logger.LogInformation("Unknown Error occurred!");
            Result = new BadRequestObjectResult(new { Error = "Unknown Server Error"})
            {
                StatusCode = 500
            };
        }
    }
}
