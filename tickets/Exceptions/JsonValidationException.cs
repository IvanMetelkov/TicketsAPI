using System;
using System.Globalization;

namespace tickets.Exceptions
{
    public class JsonValidationException : Exception
    {
        public JsonValidationException() : base() { }

        public JsonValidationException(string message) : base(message) { }

        public JsonValidationException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
