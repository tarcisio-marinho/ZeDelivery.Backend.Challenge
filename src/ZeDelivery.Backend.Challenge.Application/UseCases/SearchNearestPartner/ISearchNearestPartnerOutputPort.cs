using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.Shared;
using ZeDelivery.Backend.Challenge.Domain.Entities;

namespace ZeDelivery.Backend.Challenge.Application.UseCases.SearchNearestPartner
{
    public interface ISearchNearestPartnerOutputPort
    {
        void PublishNearestPartner(Partner partner);
        void PublishNoNearestPartnerFound();
        void PublishInternalServerError();
        void PublishValidationErros(Notification notification);
    }
}
