﻿using BirdFeed.Core.Configuration;
using System;

namespace BirdFeed.Core
{
  public class DefaultAuthProvider : IAuthProvider
  {
    public DefaultAuthProvider(IAuthCredentials authCredentials)
    {
      _authCredentials = authCredentials ?? throw new ArgumentNullException(nameof(authCredentials));
    }

    public IAuthCredentials Get()
    {
      return _authCredentials;
    }

    public void Set(IAccessToken accessToken)
    {
      _authCredentials.AccessToken = accessToken.AccessToken;
      _authCredentials.AccessTokenSecret = accessToken.AccessTokenSecret;
    }

    private readonly IAuthCredentials _authCredentials;
  }
}
