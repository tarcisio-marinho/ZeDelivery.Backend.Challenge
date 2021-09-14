using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner
{
    public interface ICreatePartnerOutputPort
    {
        public Task PublishPartnerCreated();
        public Task PublishValidationErros();
        public Task PublishDuplicatedPartner();
        public Task PublishInternalServerError();
    }
}
