using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OKX.Api.Extensions;
public static class AuthenticationProviderExtensions
{
    /// <summary>
    /// Authenticate with Web Socket Parameters
    /// </summary>
    /// <param name="authenticationProvider"></param>
    /// <param name="providedParameters"></param>
    /// <returns></returns>
    public static Dictionary<string, string> AuthenticateWithSocketParameters(this OkxAuthenticationProvider authenticationProvider, Dictionary<string, string> providedParameters)
    {
        authenticationProvider.inputParameters = providedParameters;
        authenticationProvider.AuthenticateWebSocketApi();

        return authenticationProvider.outputParameters;
    }
}
