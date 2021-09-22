using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.Services.Caching;
using ZeDelivery.Backend.Challenge.Application.UseCases.FindPartner;
using ZeDelivery.Backend.Challenge.Application.UseCases.FindPartner.Input;
using ZeDelivery.Backend.Challenge.Domain.Entities;
using ZeDelivery.Backend.Challenge.Domain.Entities.Dtos;
using ZeDelivery.Backend.Challenge.Domain.Queries;

namespace ZeDelivery.Backend.Challenge.Tests.Units.FindPartner
{
    [TestFixture]
    public class FindPartnerTests
    {
        private readonly ICacheService cacheService;
        private readonly IValidator<FindPartnerInput> validator;
        private readonly IFindPartnerOutputPort outputPort;
        private readonly IFindPartnerByIdQuery findPartnerQuery;
        private readonly ILogger<FindPartnerUseCase> logger;
        private FindPartnerInput Input;
        private readonly FindPartnerUseCase useCase;
        public FindPartnerTests()
        {
            cacheService = Substitute.For<ICacheService>();
            validator = Substitute.For<IValidator<FindPartnerInput>>();
            outputPort = Substitute.For<IFindPartnerOutputPort>();
            findPartnerQuery = Substitute.For<IFindPartnerByIdQuery> ();
            logger = Substitute.For<ILogger<FindPartnerUseCase>>();
            useCase = new FindPartnerUseCase(cacheService, validator, outputPort, findPartnerQuery, logger);
        }


        public void GivenAValidInput()
        {
            Input = new FindPartnerInput { Id = "123" };
        }


        public void GivenNoErrorsOccursAtInputDataValidation()
        {
            var mock = new FluentValidation.Results.ValidationResult { };
            validator.ValidateAsync(Input).Returns(mock);
        }


        public async Task GivenTheUseCaseIsExecuted()
        {
            await useCase.ExecuteAsync(Input);
        }

        public void AndThePartnerFoundInDatabase()
        {
            var partner = new Partner("1243", "teste", "zezinho", "1243433",
                new CoverageArea("polygon", new Polygon(new List<Point>() { new Point(1, 2) })),
                new Address(new Point(1, 2), "Point"));

            findPartnerQuery.ExecuteAsync(Arg.Any<string>()).Returns(partner);
        }

        public void AndThePartnerNotFoundInDatabase()
        {
            findPartnerQuery.ExecuteAsync(Arg.Any<string>()).ReturnsNull();
        }

        public void GivenThePartnerIsInCache()
        {
            var response = new CacheEntry<PartnerDto>(
                new PartnerDto 
                {
                    Id = "13",
                    Document = "124313241243214",
                    OwnerName = "zezinho da entrega",
                    TradingName = "teste",
                    AddressCoordinates = new Domain.Entities.Point(1, 2),
                    AddressType = "Point",
                    CoverageAreaCoordinates = new Domain.Entities.Polygon(new List<Domain.Entities.Point>() { new Domain.Entities.Point(1, 2) }),
                    CoverageAreaType = "Polygon"
                }
                , true);

            cacheService.TryGetAsync<PartnerDto>(Arg.Any<string>()).Returns(response);
        }

        public void GivenThePartnerIsNotInCache()
        {
            var response = new CacheEntry<PartnerDto>(null, false);

            cacheService.TryGetAsync<PartnerDto>(Arg.Any<string>()).Returns(response);
        }


        public void WhenTheResultShouldBePartnerFound()
        {
            outputPort.Received().PublishPartnerFound(Arg.Any<Partner>());
        }

        public void WhenTheResultShouldBePartnerNotFound()
        {
            outputPort.Received().PublishPartnerNotFound();
        }
        

        [Test]
        public void PartnerFoundInCache()
        {
            GivenAValidInput();
            GivenNoErrorsOccursAtInputDataValidation();
            GivenThePartnerIsInCache();
            GivenTheUseCaseIsExecuted();
            WhenTheResultShouldBePartnerFound();
        }

        [Test]
        public void PartnerFoundInDatabase()
        {
            GivenAValidInput();
            GivenNoErrorsOccursAtInputDataValidation();
            GivenThePartnerIsNotInCache();
            AndThePartnerFoundInDatabase();
            GivenTheUseCaseIsExecuted();
            WhenTheResultShouldBePartnerFound();
        }

        [Test]
        public void PartnerFoundNotFound()
        {
            GivenAValidInput();
            GivenNoErrorsOccursAtInputDataValidation();
            GivenThePartnerIsNotInCache();
            AndThePartnerNotFoundInDatabase();
            GivenTheUseCaseIsExecuted();
            WhenTheResultShouldBePartnerNotFound();
        }
    }
}
