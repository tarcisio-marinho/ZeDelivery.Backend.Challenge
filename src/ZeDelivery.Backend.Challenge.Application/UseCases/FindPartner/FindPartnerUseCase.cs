using FluentValidation;
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
        public FindPartnerUseCase(
            ICacheService cacheService,
            IValidator<FindPartnerInput> validator,
            IFindPartnerOutputPort outputPort,
            IFindPartnerByIdQuery findPartnerQuery)
        {
            this.cacheService = cacheService;
            this.validator = validator;
            this.outputPort = outputPort;
            this.findPartnerQuery = findPartnerQuery;
        }

        public async Task ExecuteAsync(FindPartnerInput input)
        {
            var inputValidatoin = await validator.ValidateAsync(input);

            if (!inputValidatoin.IsValid)
            {
                var notification = new Notification(inputValidatoin.Errors);
                outputPort.PublishValidationErros(notification);
                return;
            }

            var partnerDtoResponse = await cacheService.TryGetAsync<PartnerDto>(input.Id);

            if (partnerDtoResponse.Success)
            {
                outputPort.PublishPartnerFound(partnerDtoResponse.Value.ToPartner());
                return;
            }

            var partnerResponse = await findPartnerQuery.ExecuteAsync(input.Id);

            if(partnerResponse == null)
            {
                outputPort.PublishPartnerNotFound();
                return;
            }

            _ = cacheService.TrySetAsync(input.Id, partnerResponse.ToDto());

            outputPort.PublishPartnerFound(partnerResponse);

        }
    }
}
