using System;
using System.Globalization;

namespace WebApi.Helpers
{
    // custom exception class for throwing exceptions that should be sent to developer
    public class ShouldNotifyException : Exception
    {
        public ShouldNotifyException() : base() {}

        public ShouldNotifyException(string message) : base(message) { }
    }
}
