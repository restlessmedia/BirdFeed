using System.Collections.Generic;
using System.Collections.Specialized;

namespace BirdFeed.Core
{
    internal interface IOAuth
    {
        void SetAuthorisationHeader(NameValueCollection headers);

        string Signature();

        string AuthHeader();

        string AuthHeaderValue();

        string SignatureBaseString();

        string ParameterString();

        string SigningKey();

        IDictionary<string, string> GetOrderedSigningParameters();

        IDictionary<string, string> GetOAuthParameters(string signature = null);
    }
}