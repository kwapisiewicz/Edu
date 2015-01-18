using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.Model
{
    public class ExamContextInitializer : DropCreateDatabaseIfModelChanges<ExamContext>
    {
        protected override void Seed(ExamContext context)
        {
            this.Init(context);
            base.Seed(context);
        }

        private void Init(ExamContext context)
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
        }
    }
}
