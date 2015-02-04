using Exams.Model;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Exams.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            ExamsWindowsServiceDefinition srv = new ExamsWindowsServiceDefinition();
            if (Environment.UserInteractive)
            {
                srv.Start();
                TestReady();
                Console.ReadKey();
                srv.Stop();
            }
            else
            {
                ServiceBase.Run(srv);
            }
        }

        private static void TestReady()
        {
            string hostAddress = ConfigurationManager.AppSettings["HostAddress"]; ;
            HttpClient client = new HttpClient();
            ExecuteCall(client, hostAddress);
            ExecuteCall(client, hostAddress + "Questions");
            ExecuteCall(client, hostAddress + "Categories");
            ExecuteCall(client, hostAddress + "Answers");
        }

        private static HttpResponseMessage ExecuteCall(HttpClient client, string address)
        {
            var response = client.GetAsync(address).Result;

            Console.WriteLine(response);
            string content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
            return response;
        }
    }
}
