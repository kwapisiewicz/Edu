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
            #region
            Init();
            #endregion
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

        private static void Init()
        {
            using (var context = new ExamContext())
            {
                context.Categories.Add(new Category() { Name = "Ogolne" });
                context.Categories.Add(new Category() { Name = "Chemia" });
                Category math = new Category() { Name = "Matematyka" };
                context.Categories.Add(math);

                Question q1 = new Question()
                {
                    Type = QuestionType.SingleClause,
                    Text = "Ile to 2 + 2?",
                    MaxPoints = 10,
                };
                q1.Categories.Add(math);
                q1.Answers.Add(new Answer() { Text = "4", Score = new Score() { Points = 10 } });
                q1.Answers.Add(new Answer() { Text = "1" });
                q1.Answers.Add(new Answer() { Text = "0" });

                Question q2 = new Question()
                {
                    Type = QuestionType.SingleClause,
                    Text = "Ile to 3 * 2?",
                    MaxPoints = 10,
                };
                q2.Categories.Add(math);
                q2.Answers.Add(new Answer() { Text = "6", Score = new Score() { Points = 10 } });
                q2.Answers.Add(new Answer() { Text = "2" });
                q2.Answers.Add(new Answer() { Text = "3" });

                context.Questions.Add(q1);
                context.Questions.Add(q2);
                var succeded = context.SaveChanges();
                Console.WriteLine(succeded);
            }
        }
    }
}
