using Exams.Model;
using Exams.UI.Context;
using Microsoft.OData.Client;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.UI.Views
{
    public class QuestionsViewModel : BindableBase
    {
        ODataClient _client;
        public QuestionsViewModel(ODataClient client)
        {
            _client = client;
            Questions = _client.Context
                .Questions
                .Expand("Answers")
                .ToArray();

            foreach (var item in Questions.SelectMany(a=>a.Answers))
            {

                client.Context.IgnoreResourceNotFoundException = true;
                try
                {
                    client.Context.LoadProperty(item, "Score");                
                }
                catch (DataServiceClientException ex)
                {
                    //Not sure why, but when NotFound then DataServiceClient throws exception
                    //It is normal thing when navigation property = null ergo is NotFound
                    if(!(ex.Message=="NotFound"))
                    {
                        throw;
                    }
                }
            }        
        }

        public IEnumerable<Question> Questions { get; set; }
    }
}
