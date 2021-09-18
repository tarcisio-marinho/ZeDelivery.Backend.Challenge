using FluentValidation;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.Services.Caching;
using ZeDelivery.Backend.Challenge.Application.Shared;
using ZeDelivery.Backend.Challenge.Application.UseCases.SearchNearestPartner.Input;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.SearchNearestPartner
{
    public class SearchNearestPartnerUseCase : IUseCase<SearchNearestPartnerInput>
    {
        private readonly ISearchNearestPartnerOutputPort outputPort;
        private readonly ICacheService cacheService;
        private readonly IValidator<SearchNearestPartnerInput> validator;
        public SearchNearestPartnerUseCase(
            ISearchNearestPartnerOutputPort outputPort,
            ICacheService cacheService,
            IValidator<SearchNearestPartnerInput> validator)
        {
            this.outputPort = outputPort;
            this.cacheService = cacheService;
            this.validator = validator;
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



            //outputPort.PublishNearestPartner(new Partner());
        }
    }
}
