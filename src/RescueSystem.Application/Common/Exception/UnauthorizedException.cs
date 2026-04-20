using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Common.Exception
{
    public class UnauthorizedException : System.Exception
    {
        public int StatusCode { get; } = 401;
        public UnauthorizedException(string message) : base(message)
        { }
    }
}
