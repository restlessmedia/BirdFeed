using System;

namespace BirdFeed.Core.Exceptions
{
  public class TwitterHttpException : TwitterException
  {
    public TwitterHttpException(ExceptionCode code, Exception innerException)
      : base(code, innerException)
    {
    }
  }
}