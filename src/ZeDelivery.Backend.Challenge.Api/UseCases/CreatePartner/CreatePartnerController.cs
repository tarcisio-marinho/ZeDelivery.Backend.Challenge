﻿using Microsoft.AspNetCore.Http;
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
        private readonly ICacheService cache;
        private readonly IUseCase<CreatePartnerInput> useCase;
        public CreatePartnerController(ILogger<CreatePartnerController> logger, CreatePartnerPresenter presenter, ICacheService cache, IUseCase<CreatePartnerInput> useCase)
        {
            this.logger = logger;
            this.presenter = presenter;
            this.cache = cache;
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

            var input = new CreatePartnerInput();

            await useCase.ExecuteAsync(input);

            return presenter.Result;
        }
    }
}