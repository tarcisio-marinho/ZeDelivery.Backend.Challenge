using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner
{
    public class CreatePartnerUseCase : IUseCase<CreatePartnerInput>
    {
        private readonly ICreatePartnerOutputPort outputPort;
        public CreatePartnerUseCase(ICreatePartnerOutputPort outputPort)
        {
            this.outputPort = outputPort;
        }

        public async Task ExecuteAsync(CreatePartnerInput input)
        {

            outputPort.PublishPartnerCreated();
        }
    }
}
