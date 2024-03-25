using System;
using System.Runtime.Serialization;

namespace CakeMachine
{
    public class CakeFactoryException : Exception
    {
        public CakeFactoryException()
        {
        }

        public CakeFactoryException(string message) : base(message)
        {
        }

        public CakeFactoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CakeFactoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
