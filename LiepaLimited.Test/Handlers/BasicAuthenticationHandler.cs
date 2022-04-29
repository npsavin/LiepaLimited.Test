using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using LiepaLimited.Test.Application.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LiepaLimited.Test.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string RealmHeaderName = "WWW-Authenticate";
        private const string RealmHeader = "Basic realm=Liepa Limited Test";
        private const string AuthorizationHeader = "Authorization";
        private const string Basic = "Basic";
        private const string InvalidHeader = "Invalid Authorization Header";
        private const string InvalidLoginOrPassword = "Invalid username or password";
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authHeader = Request.Headers[AuthorizationHeader].ToString();
            if (string.IsNullOrEmpty(authHeader))
            {
                return Fail(InvalidHeader);
            }

            var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

            if (!authHeaderVal.Scheme.Equals(Basic,
                    StringComparison.OrdinalIgnoreCase) ||
                authHeaderVal.Parameter == null)
            {
                return Fail(InvalidHeader);
            }

            var credentials = authHeaderVal.Parameter;
            try
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                credentials = encoding.GetString(Convert.FromBase64String(credentials));

                var separator = credentials.IndexOf(':');
                var name = credentials.Substring(0, separator);
                var password = credentials.Substring(separator + 1);

                if (CheckPassword(name, password))
                {
                    var claims = new[] { new Claim("name", name) };
                    var identity = new ClaimsIdentity(claims, Basic);
                    var claimsPrincipal = new ClaimsPrincipal(identity);
                    return Task.FromResult(
                        AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }

                return Fail(InvalidLoginOrPassword);
            }
            catch
            {
                return Fail(InvalidHeader);
            }

        }

        private Task<AuthenticateResult> Fail(string message)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            Response.Headers.Add(RealmHeaderName, RealmHeader);
            return Task.FromResult(AuthenticateResult.Fail(message));
        }

        private static bool CheckPassword(string username, string password)
        {
            return username == "user" && password == "password";
        }
    }
}
