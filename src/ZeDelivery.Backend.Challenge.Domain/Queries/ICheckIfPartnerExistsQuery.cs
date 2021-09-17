using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Domain.Queries
{
    public interface ICheckIfPartnerExistsQuery
    {
        Task<bool> ExecuteAsync(string Id);
    }
}
