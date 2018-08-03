using System;
using System.Collections.Generic;
using System.Text;

namespace TouristEye.Exceptions
{
    public class BadRequestException : Exception
    {
        private const string _message = "You cannot proceed due to invalid data, please enter data again.";
        public BadRequestException() : base(_message)
        {
        }

        public BadRequestException(string message)
            : base(message)
        {
        }

        public BadRequestException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
