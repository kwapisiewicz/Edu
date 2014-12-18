using Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.UI.Context
{
    public class ODataClient
    {
        private static string serverUri = @"http://localhost:9000/";
        private static string masterPassword = "abc";

        private LoginContext _loginContext;

        public Container Context { get; private set; }

        public ODataClient(LoginContext loginCuntext)
        {
            _loginContext = loginCuntext;
            Context = new Default.Container(new Uri(serverUri));
            Context.SendingRequest2 += SetMasterPasswordHeader;
        }

        void SetMasterPasswordHeader(object sender, Microsoft.OData.Client.SendingRequest2EventArgs e)
        {
            if (_loginContext.UserType==UserType.Teacher)
            {
                e.RequestMessage.SetHeader(
                    "MasterPassword",
                    _loginContext.PasswordHash);
            }
        }
    }
}
