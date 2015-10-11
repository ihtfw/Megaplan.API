using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
