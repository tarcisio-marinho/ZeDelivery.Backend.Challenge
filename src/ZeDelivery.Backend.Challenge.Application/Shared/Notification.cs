using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeDelivery.Backend.Challenge.Application.Shared
{
    public class Notification
    {
        private readonly IList<ValidationFailure> Errors;
        public Notification(IList<ValidationFailure> errors)
        {
            Errors = errors;
        }

        public IList<string> GetErrors()
        {
            IList<string> messages = new List<string>();

            foreach(var error in Errors)
            {
                messages.Add(error.ErrorMessage);
            }

            return messages;
        }
    }
}
