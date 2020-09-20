using System;

namespace WebApi.Helpers
{
    public class ItemNotAvailableException : Exception
    {
        public ItemNotAvailableException() : base() {}

        public ItemNotAvailableException(string message) : base(message) { }
    }
}
