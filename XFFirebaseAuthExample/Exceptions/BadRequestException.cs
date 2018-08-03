using System;
using System.Collections.Generic;
using System.Text;

namespace TouristEye.Exceptions
{
    public class InternalServerException : Exception
    {
        private const string _message = "There is problem with system. Try again.";
        public InternalServerException() : base(_message)
        {
        }

        public InternalServerException(string message)
            : base(message)
        {
        }

        public InternalServerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
