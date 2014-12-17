using Exams.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void A()
        {
            using (var context = new ExamContext())
            {
                Question q = new Question();
                q.Type = QuestionType.SingleClause;
                q.Text = "Question text";
                q.Answers.Add(new Answer()
                {
                    Text = "Correct",
                    Score = new Score() { Points = 10 }
                });
                q.Answers.Add(new Answer() { Text = "Wrong1" });
                q.Answers.Add(new Answer() { Text = "Wrong2" });
                context.Questions.Add(q);
                context.SaveChanges();
            }
        }
    }
}
