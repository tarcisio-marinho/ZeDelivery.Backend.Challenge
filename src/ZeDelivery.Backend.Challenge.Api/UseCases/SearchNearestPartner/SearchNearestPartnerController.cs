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
using ZeDelivery.Backend.Challenge.Application.UseCases.SearchNearestPartner.Input;

namespace ZeDelivery.Backend.Challenge.Api.UseCases.SearchNearestPartner
{
    [ApiController]
    [ExcludeFromCodeCoverage]
    [Route("[controller]")]
    public class SearchNearestPartnerController : Controller
    {
        private readonly ILogger<SearchNearestPartnerController> logger;
        private readonly SearchNearestPartnerPresenter presenter;
        private readonly IUseCase<SearchNearestPartnerInput> useCase;
        public SearchNearestPartnerController(ILogger<SearchNearestPartnerController> logger, SearchNearestPartnerPresenter presenter, IUseCase<SearchNearestPartnerInput> useCase)
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
        public async Task<IActionResult> CreatePartnerAsync(float latitude, float longitude)
        {
            logger.LogInformation($"Starting use case for lat: {latitude} and long: {longitude}");

            var input = new SearchNearestPartnerInput { };

            await useCase.ExecuteAsync(input);

            return presenter.Result;
        }
    }
}
