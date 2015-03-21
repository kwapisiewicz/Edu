using Exams.Model;
using Exams.ODataClient.Context;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.UI.Teacher.Questions
{
    public class AddQuestionViewModel : BindableBase
    {
        private Client _client;

        public ObservableCollection<Category> Categories { get; set; }

        public ObservableCollection<Category> SelectedCategories { get; set; }

        public ObservableCollection<Answer> Answers { get; set; }

        public Category SelectedCategory { get; set; }

        public string Text { get; set; }

        public AddQuestionViewModel(Client client)
        {
            _client = client;
                 
            SaveCommand = new DelegateCommand(DoSave);

            Categories = new ObservableCollection<Category>(
                _client.Context.Categories.ToList());
            SelectedCategories = new ObservableCollection<Category>();
            SelectedCategory = Categories.First();
            Text = "aaa";
            Answers = new ObservableCollection<Answer>();
            Answers.Add(new Answer() { Text = "Abc", Score = new Score() { Points = 10 } });
            Answers.Add(new Answer() { Text = "Bcd" });
            Answers.Add(new Answer() { Text = "Cde" });
        }

        public DelegateCommand SaveCommand { get; set; }

        public void DoSave()
        {
            bool isMultiple = Answers.Count(a => a.Score != null) > 1;
            Question question = new Question()
            {
                Text = Text,
                MaxPoints = Answers.Where(a => a.Score != null).Sum(b => b.Score.Points),
                Type = isMultiple ? QuestionType.MultupleClause : QuestionType.SingleClause
            };

            _client.Context.AddToQuestions(question);
            //_client.Context.SaveChanges();
            foreach (var answer in Answers)
            {
                _client.Context.AddToAnswers(answer);
                _client.Context.AddLink(question, "Answers", answer);
                //_client.Context.SaveChanges();
                if (answer.Score != null)
                {

                    _client.Context.AddToScores(answer.Score);
                    _client.Context.SetLink(answer, "Score", answer.Score);
                    //_client.Context.SaveChanges();
                }
            }

            _client.Context.SaveChanges();
        }
    }
}
