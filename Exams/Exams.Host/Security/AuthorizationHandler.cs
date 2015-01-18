using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Exams.Host.Security
{
    public class AuthorizationHandler : DelegatingHandler
    {
        private const string MasterPasswordHeader = "MasterPassword";
        private const string MasterPasswordHashAppConfigKey = "MasterPasswordHash";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if(request.Headers.Any(a=>a.Key == MasterPasswordHeader))
            {
                var passwordHeader = request.Headers.First(a => a.Key == MasterPasswordHeader);
                var passwordHash = passwordHeader.Value.FirstOrDefault();
                if (passwordHash == ConfigurationManager.AppSettings[MasterPasswordHashAppConfigKey])
                {
                    IPrincipal principal = new GenericPrincipal(
                        new GenericIdentity(Roles.Elevated),
                        new string[] { Roles.Elevated });
                    Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current!=null)
                    {                        
                        HttpContext.Current.User = principal;
                    }
                }
                else
                {
                    return Task.Factory.StartNew(() => request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid password"));
                }

            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
