using FluentValidation;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.Services.Caching;
using ZeDelivery.Backend.Challenge.Application.Shared;
using ZeDelivery.Backend.Challenge.Application.UseCases.FindPartner.Input;
using ZeDelivery.Backend.Challenge.Domain.Entities;
using ZeDelivery.Backend.Challenge.Domain.Entities.Dtos;
using ZeDelivery.Backend.Challenge.Domain.Queries;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.FindPartner
{
    public class FindPartnerUseCase : IUseCase<FindPartnerInput>
    {
        private readonly ICacheService cacheService;
        private readonly IValidator<FindPartnerInput> validator;
        private readonly IFindPartnerOutputPort outputPort;
        private readonly IFindPartnerByIdQuery findPartnerQuery;
        private readonly ILogger<FindPartnerUseCase> logger;
        public FindPartnerUseCase(
            ICacheService cacheService,
            IValidator<FindPartnerInput> validator,
            IFindPartnerOutputPort outputPort,
            IFindPartnerByIdQuery findPartnerQuery, 
            ILogger<FindPartnerUseCase> logger)
        {
            this.cacheService = cacheService;
            this.validator = validator;
            this.outputPort = outputPort;
            this.findPartnerQuery = findPartnerQuery;
            this.logger = logger;
        }

        public async Task ExecuteAsync(FindPartnerInput input)
        {
            var inputValidation = await validator.ValidateAsync(input);

            if (!inputValidation.IsValid)
            {
                var notification = new Notification(inputValidation.Errors);
                outputPort.PublishValidationErros(notification);
                return;
            }


            var partnerDtoResponse = await cacheService.TryGetAsync<PartnerDto>(input.Id);

            if (partnerDtoResponse.Success)
            {
                logger.LogInformation($"Recovered from cache, document: {partnerDtoResponse.Value.Document}");
                outputPort.PublishPartnerFound(partnerDtoResponse.Value.ToPartner());
                return;
            }

            var partnerResponse = await findPartnerQuery.ExecuteAsync(input.Id);

            if(partnerResponse is null)
            {
                outputPort.PublishPartnerNotFound();
                return;
            }

            _ = cacheService.TrySetAsync(input.Id, partnerResponse.ToDto());

            outputPort.PublishPartnerFound(partnerResponse);

        }
    }
}
