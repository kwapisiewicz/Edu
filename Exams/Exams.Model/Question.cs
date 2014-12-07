using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.Model
{
    public class Question
    {
        public Question()
        {
            Answers = new List<Answer>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public int MaxPoints { get; set; }
        public QuestionType Type { get; set; }
    }
}
