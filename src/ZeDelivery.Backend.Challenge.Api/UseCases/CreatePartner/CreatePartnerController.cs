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

namespace ZeDelivery.Backend.Challenge.Api.UseCases.CreatePartner
{

    [ApiController]
    [ExcludeFromCodeCoverage]
    [Route("[controller]")]
    public class CreatePartnerController : Controller
    {
        private readonly ILogger<CreatePartnerController> logger;
        private readonly CreatePartnerPresenter presenter;
        public CreatePartnerController(ILogger<CreatePartnerController> logger, CreatePartnerPresenter presenter)
        {
            this.logger = logger;
            this.presenter = presenter;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrors), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(InternalServerError), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePartnerAsync()
        {
            logger.LogInformation("asdfsd");

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost,port: 6379,password=mysecurepasswordhere"); // ^^^ store and re-use this!!!

            IDatabase db = redis.GetDatabase();

            string value = "abcdefg";

            db.StringSet("mykey", value);

            var output = db.StringGet("mykey");

            await presenter.PublishPartnerCreated();
            
            return presenter.Result;
        }
    }
}
