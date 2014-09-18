namespace BirdFeed.Core
{
  public interface IOAuthSigning
  {
    string Sign(string key, string value);

    string Method { get; }
  }
}