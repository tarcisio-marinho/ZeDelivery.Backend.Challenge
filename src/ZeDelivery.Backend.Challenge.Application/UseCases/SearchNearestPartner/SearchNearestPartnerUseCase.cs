using FluentValidation;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.Services.Caching;
using ZeDelivery.Backend.Challenge.Application.Shared;
using ZeDelivery.Backend.Challenge.Application.UseCases.SearchNearestPartner.Input;
using ZeDelivery.Backend.Challenge.Domain.Entities;
using ZeDelivery.Backend.Challenge.Domain.Queries;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.SearchNearestPartner
{
    public class SearchNearestPartnerUseCase : IUseCase<SearchNearestPartnerInput>
    {
        private readonly ISearchNearestPartnerOutputPort outputPort;
        private readonly ICacheService cacheService;
        private readonly IValidator<SearchNearestPartnerInput> validator;
        private readonly IGetNearestPartnerInMyAreaQuery nearestPartnerQuery;
        public SearchNearestPartnerUseCase(
            ISearchNearestPartnerOutputPort outputPort,
            ICacheService cacheService,
            IValidator<SearchNearestPartnerInput> validator, 
            IGetNearestPartnerInMyAreaQuery nearestPartnerQuery)
        {
            this.outputPort = outputPort;
            this.cacheService = cacheService;
            this.validator = validator;
            this.nearestPartnerQuery = nearestPartnerQuery;
        }

        public async Task ExecuteAsync(SearchNearestPartnerInput input)
        {
            var validated = await validator.ValidateAsync(input);

            if (!validated.IsValid)
            {
                var notification = new Notification(validated.Errors);
                outputPort.PublishValidationErros(notification);
                return;
            }
            
            var ret = await nearestPartnerQuery.ExecuteAsync(new Point(input.Latitude, input.Longitude));


            outputPort.PublishNearestPartner(ret);
        }
    }
}
