using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Exams.Host
{
    public static class Roles
    {
        public const string Elevated = "Elevated";
    }

    public class AuthorizationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if(request.Headers.Any(a=>a.Key == "MasterPassword"))
            {
                var passwordHeader = request.Headers.First(a => a.Key == "MasterPassword");
                var passwordHash = passwordHeader.Value.FirstOrDefault();
                if (passwordHash == "900150983cd24fb0d6963f7d28e17f72")
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
