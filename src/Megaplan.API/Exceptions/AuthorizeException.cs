using System;

namespace Megaplan.API.Exceptions
{
    public class AuthorizeException :Exception
    {
        public AuthorizeException(string message)
            : base(message)
        {
        }

        public AuthorizeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
