using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task PublishPartnerCreated()
        {
            logger.LogInformation("Partner created !");
            Result = new OkResult();
        }

        // TODO: Receber objeto -> Notification -> genérico, idependente do caso de uso
        // TODO: Criar Default Presenter
        public async Task PublishValidationErros() 
        {
            Result = new UnprocessableEntityObjectResult(new { });
        }

        public async Task PublishDuplicatedPartner()
        {
            Result = new BadRequestObjectResult(new { });
        }

        public async Task PublishInternalServerError()
        {
            logger.LogInformation("Unknown Error occurred!");
            Result = new BadRequestObjectResult(new { Error = "Unknown Server Error"})
            {
                StatusCode = 500
            };
        }
    }
}
