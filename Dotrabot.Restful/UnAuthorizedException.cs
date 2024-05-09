using System.Runtime.Serialization;

namespace Dotrabot.Restful
{
    [Serializable]
    public class UnAuthorizedException : Exception
    {
       

        public UnAuthorizedException(string? message) : base(message)
        {
        }

        public UnAuthorizedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnAuthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}