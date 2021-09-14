using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Application
{
    public abstract class BaseUseCase
    {
        public abstract Task RunAsync();

        public async Task ExecuteAsync()
        {

            await RunAsync();
        }
    }
}
