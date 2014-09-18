using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Core.Exceptions
{
    public class TwitterSerializationException : TwitterException
    {
        public TwitterSerializationException(ExceptionCode code, Exception innerException)
            : base(code, innerException)
        {
        }
    }
}