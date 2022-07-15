using System.Runtime.Serialization;

namespace BBDownGUI
{
    [Serializable]
    internal class UnsupportedArchitectureException : Exception
    {
        public UnsupportedArchitectureException()
        {
        }

        public UnsupportedArchitectureException(string message) : base(message)
        {
        }

        public UnsupportedArchitectureException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsupportedArchitectureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    internal class UnsupportedOSException : Exception
    {
        public UnsupportedOSException()
        {
        }

        public UnsupportedOSException(string message) : base(message)
        {
        }

        public UnsupportedOSException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsupportedOSException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}