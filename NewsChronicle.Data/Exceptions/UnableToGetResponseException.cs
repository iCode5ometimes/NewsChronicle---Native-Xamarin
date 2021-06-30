using System;
namespace NewsChronicle.Data.Exceptions
{
    public class UnableToGetResponseException : Exception
    {
        public UnableToGetResponseException()
        {
        }

        public UnableToGetResponseException(string message) : base(message)
        {
        }

        public UnableToGetResponseException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
