using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Domain.Entities;

namespace ZeDelivery.Backend.Challenge.Domain.Queries
{
    public interface IGetNearestPartnerInMyAreaQuery
    {
        Task<Partner> ExecuteAsync(Point point);
    }
}
