using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Common.Exception
{
    public class InternalServerErrorException : System.Exception
    {
        public int statusCode { get; } = 500;
        public InternalServerErrorException(string message = "Internal Server Error") : base(message) { }

    }
}
