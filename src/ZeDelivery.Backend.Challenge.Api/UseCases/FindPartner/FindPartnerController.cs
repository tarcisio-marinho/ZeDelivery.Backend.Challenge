using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Api.Dtos;
using ZeDelivery.Backend.Challenge.Application;
using ZeDelivery.Backend.Challenge.Application.UseCases.FindPartner.Input;

namespace ZeDelivery.Backend.Challenge.Api.UseCases.FindPartner
{

    [ApiController]
    [ExcludeFromCodeCoverage]
    [Route("[controller]")]
    public class FindPartnerController : Controller
    {
        private readonly ILogger<FindPartnerController> logger;
        private readonly FindPartnerPresenter presenter;
        private readonly IUseCase<FindPartnerInput> useCase;
        public FindPartnerController(ILogger<FindPartnerController> logger, FindPartnerPresenter presenter, IUseCase<FindPartnerInput> useCase)
        {
            this.logger = logger;
            this.presenter = presenter;
            this.useCase = useCase;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationErrors), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(InternalServerError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FindPartnerAsync(string id)
        {
            logger.LogInformation($"Starting use case for Id: {id}");

            var input = new FindPartnerInput { Id = id };
            await useCase.ExecuteAsync(input);

            return presenter.Result;
        }
    }
}
