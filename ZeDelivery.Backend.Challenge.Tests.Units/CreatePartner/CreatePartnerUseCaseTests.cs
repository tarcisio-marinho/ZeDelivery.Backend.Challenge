using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ZeDelivery.Backend.Challenge.Application.Services.Caching;
using ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner;
using ZeDelivery.Backend.Challenge.Domain.Queries;
using ZeDelivery.Backend.Challenge.Domain.Entities;

namespace ZeDelivery.Backend.Challenge.Tests.Units.CreatePartner
{
    [TestFixture]
    public class CreatePartnerUseCaseTests
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

        public CreatePartnerUseCaseTests()
        {
            outputPort = Substitute.For<ICreatePartnerOutputPort>();
            validator = Substitute.For<IValidator<CreatePartnerInput>>();
            logger = Substitute.For<ILogger<CreatePartnerUseCase>>();
            insertNewPartnerquery = Substitute.For<IInsertNewPartnerQuery>();
            checkIfPartnerExistsquery = Substitute.For<ICheckIfPartnerExistsQuery>();
            cacheService = Substitute.For<ICacheService>();

            useCase = new CreatePartnerUseCase(outputPort, validator, logger, insertNewPartnerquery, checkIfPartnerExistsquery, cacheService);
        }


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


        public void GivenNoErrorsOccursAtInputDataValidation()
        {
            var mock = new FluentValidation.Results.ValidationResult { };
            validator.ValidateAsync(Partner).Returns(mock);
        }


        public async Task GivenTheUseCaseIsExecuted()
        {
            await useCase.ExecuteAsync(Partner);
        }


        public void GivenThePartnerHasNotAlreadyRegistered()
        {
            checkIfPartnerExistsquery.ExecuteAsync(Arg.Any<string>()).Returns(false);
        }

        public void GivenThePartnerHasAlreadyRegistered()
        {
            checkIfPartnerExistsquery.ExecuteAsync(Arg.Any<string>()).Returns(true);
        }

        public void WhenThePartnerIsInsertedInTheDatabase()
        {
            insertNewPartnerquery.ExecuteAsync(Arg.Any<Partner>()).Returns(true);
        }

        public void WhenTheResultShouldBePartnerCreated()
        {
            outputPort.Received().PublishPartnerCreated();
        }

        public void WhenTheResultShouldBePartnerDuplicated()
        {
            outputPort.Received().PublishDuplicatedPartner();
        }
        

        [Test]
        public void Insert_new_partner_successfully()
        {
            GivenAValidPartnerAsInput();
            GivenNoErrorsOccursAtInputDataValidation();
            GivenThePartnerHasNotAlreadyRegistered();
            WhenThePartnerIsInsertedInTheDatabase();
            GivenTheUseCaseIsExecuted();
            WhenTheResultShouldBePartnerCreated();
        }

        [Test]
        public void ParnerAlreadyExists()
        {
            GivenAValidPartnerAsInput();
            GivenNoErrorsOccursAtInputDataValidation();
            GivenThePartnerHasAlreadyRegistered();
            WhenThePartnerIsInsertedInTheDatabase();
            GivenTheUseCaseIsExecuted();
            WhenTheResultShouldBePartnerDuplicated();
        }
    }
}