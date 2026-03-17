using System;
using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace AutoLot.Api.Security;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly SecuritySettings _securitySettings;
    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IOptionsMonitor<SecuritySettings> securitySettingsMonitor) : base(options, logger, encoder, clock)
    {
        _securitySettings=securitySettingsMonitor.CurrentValue;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var endpoint = Context.GetEndpoint();
        //skip authentication if iallowanonymous attribute exists
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
        {
            return AuthenticateResult.NoResult();
        }
        // check for authorization header key, if not found, fail the authentication
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Missing Authorization Header");
        }
        try
        {
            AuthenticationHeaderValue authHeader = AuthenticationHeaderValue.Parse(Request.Headers.Authorization);
            byte[] credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split([':'],2);
            string userName=credentials[0];
            string password = credentials[1];
            if (userName.Equals(_securitySettings.UserName, StringComparison.OrdinalIgnoreCase) && password.Equals(_securitySettings.Password, StringComparison.OrdinalIgnoreCase))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userName),
                    new Claim(ClaimTypes.Name, userName)
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principle = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principle, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            return AuthenticateResult.Fail("Invalid authorization header");
        }
        catch 
        {
            return AuthenticateResult.Fail("Invalid authorization header");
        }
    }
}

