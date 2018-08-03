using System;
using System.Collections.Generic;
using System.Text;

namespace TouristEye.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        private const string _message = "You are not authorized to proceed, please login again.";
        public UnAuthorizedException() : base(_message)
        {
        }

        public UnAuthorizedException(string message)
            : base(message)
        {
        }

        public UnAuthorizedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
