using Exams.Model;
using Exams.ODataClient.Context;
using Exams.UI.Core;
using Microsoft.OData.Client;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.UI.Teacher.Questions
{
    public class QuestionsViewModel : BindableBase
    {
        Client _client;
        public QuestionsViewModel(Client client, IRegionManager regionManager)
        {
            AddCategoryCommand = new DelegateCommand(() =>
        regionManager.RequestNavigate(Regions.MainContent, typeof(AddQuestionView).FullName));

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

        public DelegateCommand AddCategoryCommand { get; set; }
        public DelegateCommand DeleteCategoryCommand { get; set; }
    }
}
