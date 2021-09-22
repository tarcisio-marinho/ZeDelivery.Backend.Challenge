using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Api.Dtos
{
    [ExcludeFromCodeCoverage]
    public class InternalServerError
    {
        public string Message { get; set; }
    }
}
