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
        }

        public async void PublishPartnerCreated()
        {
            logger.LogInformation("Presenter: Partner created !");
            Result = new OkResult();
        }

        public async void PublishValidationErros(Notification notification) 
        {
            logger.LogInformation("Presenter: Validation Errors !");
            Result = new UnprocessableEntityObjectResult(new { Errors = notification.GetErrors() });
        }

        public async void PublishDuplicatedPartner()
        {
            logger.LogInformation("Presenter: Duplicated partner !");
            Result = new BadRequestObjectResult(new { Error = "Partner already registered!" });
        }

        public async void PublishInternalServerError()
        {
            logger.LogInformation("Presenter: Unknown Error occurred!");
            Result = new BadRequestObjectResult(new { Error = "Unknown Server Error"})
            {
                StatusCode = 500
            };
        }
    }
}
