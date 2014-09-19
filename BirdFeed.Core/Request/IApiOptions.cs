using System.Collections.Generic;

namespace BirdFeed.Core.Request
{
    public interface IApiOptions
    {
        IDictionary<string, string> Parameters { get; }
    }
}