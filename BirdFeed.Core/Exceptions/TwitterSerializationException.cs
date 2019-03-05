using BirdFeed.Core.Exceptions;
using System;

namespace BirdFeed.Core.Exceptions
{
  public class TwitterSerializationException : TwitterException
  {
    public TwitterSerializationException(ExceptionCode code, Exception innerException)
        : base(code, innerException) { }
  }
}