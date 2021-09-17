using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.Shared;
using ZeDelivery.Backend.Challenge.Domain.Entities;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.FindPartner
{
    public interface IFindPartnerOutputPort
    {
        public void PublishPartnerFound(Partner partner);
        public void PublishValidationErros(Notification notification);
        public void PublishInternalServerError();
        public void PublishPartnerNotFound();
    }
}
