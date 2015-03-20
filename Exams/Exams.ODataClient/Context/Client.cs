using Default;
using Exams.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Exams.ODataClient.Context
{
    public class DecriptiveBool
    {
        public bool Result { get; set; }
        public string Description { get; set; }
    }

    public class Client
    {
        private const string MasterPasswordHeader = "MasterPassword";
        private string serverUri = @"http://localhost:9000/";

        private LoginContext _loginContext;

        public Container Context { get; private set; }

        public Client(LoginContext loginCuntext)
        {
            _loginContext = loginCuntext;
            Context = new Default.Container(new Uri(serverUri));
            Context.SendingRequest2 += SetMasterPasswordHeader;
        }

        public DecriptiveBool CheckConnection()
        {
            HttpWebRequest request = GetCheckRequest();
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return new DecriptiveBool() { Result = true };
                }
                else
                {
                    return GetFailureDetails(response);
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    return GetFailureDetails((HttpWebResponse)ex.Response);
                }
                else
                {
                    return new DecriptiveBool() { Result = false, Description = ex.Message };
                }
            }
        }

        private HttpWebRequest GetCheckRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUri);
            request.Method = "GET";
            if (_loginContext.UserType == UserType.Teacher)
            {
                request.Headers.Add(MasterPasswordHeader, _loginContext.PasswordHash);
            }
            return request;
        }

        private DecriptiveBool GetFailureDetails(HttpWebResponse response)
        {
            DecriptiveBool returnError = new DecriptiveBool() { Result = false };
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string content = sr.ReadToEnd();
                returnError.Description = content;
            }
            else
            {
                returnError.Description = response.StatusCode.ToString();
            }

            return returnError;
        }

        private void SetMasterPasswordHeader(object sender, Microsoft.OData.Client.SendingRequest2EventArgs e)
        {
            if (_loginContext.UserType == UserType.Teacher)
            {
                e.RequestMessage.SetHeader(MasterPasswordHeader, _loginContext.PasswordHash);
            }
        }
    }
}
