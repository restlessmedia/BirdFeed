using System;

namespace BirdFeed.Core.Exceptions
{
  public class TwitterException : Exception
  {
    public TwitterException(ExceptionCode code, Exception innerException)
      : base(string.Format("Twitter exception code: ({0})", (int)code, code), innerException)
    {
      Code = code;
    }

    public readonly ExceptionCode Code;
  }
}