using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class BadRequestException: Exception
    {
        public string? Details { get; }

        public BadRequestException(string message, string? Details): base(message)
        {
            this.Details = Details;
        }
        public BadRequestException(string message) : base(message)
        {
            
        }
    }
}
