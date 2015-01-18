using Exams.Model;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exams.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";
            HttpClient client = new HttpClient();

            // Start OWIN host 
            using (WebApp.Start<WebApiConfig>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                ExecuteCall(client, baseAddress);
                ExecuteCall(client, baseAddress + "Questions");
                ExecuteCall(client, baseAddress + "Categories");
                ExecuteCall(client, baseAddress + "Answers");

                Console.ReadKey(); 
            }
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
