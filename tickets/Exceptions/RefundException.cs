using System;
using System.Globalization;

namespace tickets.Exceptions
{
    public class RefundException : Exception
    {
        public RefundException() : base() { }

        public RefundException(string message) : base(message) { }

        public RefundException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
