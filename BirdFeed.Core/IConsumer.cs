namespace BirdFeed.Core
{
  public interface IConsumer
  {
    string ConsumerKey { get; }

    string ConsumerSecret { get; }
  }
}