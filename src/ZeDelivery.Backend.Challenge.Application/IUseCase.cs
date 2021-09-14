using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Application
{
    public interface IUseCase <IInput> where IInput : TInput
    {
        Task ExecuteAsync(IInput input);
    }
}
