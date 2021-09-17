using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.Shared;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.CreatePartner
{
    public interface ICreatePartnerOutputPort
    {
        public void PublishPartnerCreated();
        public void PublishValidationErros(Notification notification);
        public void PublishDuplicatedPartner();
        public void PublishInternalServerError();
    }
}
