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
        static string serverUri = @"http://localhost:9000/";
        static string masterPassword = "abc";
        public Container Context { get; private set; }

        public ODataClient()
        {
            Context = new Default.Container(new Uri(serverUri));
            Context.SendingRequest2 += SetMasterPasswordHeader;
        }

        void SetMasterPasswordHeader(object sender, Microsoft.OData.Client.SendingRequest2EventArgs e)
        {
            e.RequestMessage.SetHeader(
                "MasterPassword",
                new Md5PasswordHash().Evaluate(masterPassword));
        }
    }
}
