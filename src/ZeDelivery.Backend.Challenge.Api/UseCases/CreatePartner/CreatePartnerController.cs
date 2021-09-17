using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Api.Dtos;
using ZeDelivery.Backend.Challenge.Api.UseCases.CreatePartner.Contract;
using ZeDelivery.Backend.Challenge.Application;
using ZeDelivery.Backend.Challenge.Application.Services.Caching;
using ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner;

namespace ZeDelivery.Backend.Challenge.Api.UseCases.CreatePartner
{

    [ApiController]
    [ExcludeFromCodeCoverage]
    [Route("[controller]")]
    public class CreatePartnerController : Controller
    {
        private readonly ILogger<CreatePartnerController> logger;
        private readonly CreatePartnerPresenter presenter;
        private readonly IUseCase<CreatePartnerInput> useCase;
        public CreatePartnerController(ILogger<CreatePartnerController> logger, CreatePartnerPresenter presenter, IUseCase<CreatePartnerInput> useCase)
        {
            this.logger = logger;
            this.presenter = presenter;
            this.useCase = useCase;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrors), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(InternalServerError), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePartnerAsync(CreatePartnerRequest request)
        {
            logger.LogInformation($"Starting use case for: { request.TradingName } and document: {request.Document}");

            var input = new CreatePartnerInput
            {
                Id = request?.Id,
                Document = request?.Document,
                OwnerName = request?.OwnerName,
                TradingName = request?.TradingName,
                Address = new AddressRequestInput
                {
                    Coordinates = request?.Address?.Coordinates,
                    Type = request?.Address?.Type
                },
                CoverageArea = new CoverageAreaInput
                {
                    Type = request?.CoverageArea?.Type,
                    Coordinates = request?.CoverageArea?.Coordinates
                }
            };

            await useCase.ExecuteAsync(input);

            return presenter.Result;
        }
    }
}
