using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using ZeDelivery.Backend.Challenge.Application.Services.Caching;
using ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner;
using ZeDelivery.Backend.Challenge.Domain.Entities;
using ZeDelivery.Backend.Challenge.Domain.Queries;

namespace ZeDelivery.Backend.Challenge.Tests.Unit.CreatePartner
{
    [Binding]
    public class CreatePartnerUseCaseSteps
    {
        private readonly CreatePartnerUseCase useCase;
        private readonly ICreatePartnerOutputPort outputPort;
        private readonly IValidator<CreatePartnerInput> validator;
        private readonly ILogger<CreatePartnerUseCase> logger;
        private readonly IInsertNewPartnerQuery insertNewPartnerquery;
        private readonly ICheckIfPartnerExistsQuery checkIfPartnerExistsquery;
        private readonly ICacheService cacheService;
        private CreatePartnerInput Partner;
        private bool Success;
        public CreatePartnerUseCaseSteps()
        {
            outputPort = Substitute.For<ICreatePartnerOutputPort>();
            validator = Substitute.For<IValidator<CreatePartnerInput>>();
            logger = Substitute.For<ILogger<CreatePartnerUseCase>>();
            insertNewPartnerquery = Substitute.For<IInsertNewPartnerQuery>();
            checkIfPartnerExistsquery = Substitute.For<ICheckIfPartnerExistsQuery>();
            cacheService = Substitute.For<ICacheService>();

            useCase = new CreatePartnerUseCase(outputPort, validator, logger, insertNewPartnerquery, checkIfPartnerExistsquery, cacheService);
        }

        [Given(@"a valid partner as input")]
        public void GivenAValidPartnerAsInput()
        {

            Partner = new CreatePartnerInput 
            {
                Id = "13",
                Document = "124313241243214",
                OwnerName = "zezinho da entrega",
                TradingName = "teste",
                Address = new AddressRequestInput
                {
                    Coordinates = new float[] { 12, 43 },
                    Type = "Point"
                },
                CoverageArea = new CoverageAreaInput
                {
                    Type = "MultiPolygon",
                    Coordinates = new[] { new[] { new[] { new float[] { 13, 43 }, new float[] { 15, 21 } } } }
                }
            };
        }

        [Given(@"no errors occurs at input data validation")]
        public void GivenNoErrorsOccursAtInputDataValidation()
        {
            var mock = new FluentValidation.Results.ValidationResult { };
            validator.ValidateAsync(Partner).Returns(mock);
        }

        [Given(@"the use case is executed")]
        public async Task GivenTheUseCaseIsExecuted()
        {
            await useCase.ExecuteAsync(Partner);
        }

        [Given(@"the partner has not already registered")]
        public void GivenThePartnerHasNotAlreadyRegistered()
        {
            checkIfPartnerExistsquery.ExecuteAsync(Arg.Any<string>()).Returns(false);
        }

        [When(@"the partner is inserted in the database")]
        public void WhenThePartnerIsInsertedInTheDatabase()
        {
            insertNewPartnerquery.ExecuteAsync(Arg.Any<Partner>()).Returns(true);
        }


        [Then(@"the result should be partner created")]
        public void ThenTheResultShouldBePartnerCreated()
        {
            outputPort.Received().PublishPartnerCreated();
        }
    }
}
