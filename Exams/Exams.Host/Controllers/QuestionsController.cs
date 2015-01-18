using Exams.Host.Security;
using Exams.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;

namespace Exams.Host.Controllers
{
    public class QuestionsController : ODataController
    {
        ExamContext context;
        public QuestionsController()
        {
            context = new ExamContext();
        }

        protected override void Dispose(bool disposing)
        {
            if (context != null)
            {
                context.Dispose();
                context = null;
            }

            base.Dispose(disposing);
        }

        [EnableQuery]
        public IQueryable<Question> Get()
        {
            var toReturn = context.Questions
                .Include("Answers")
                .Include("Categories");
                
            return toReturn;
        }

        [EnableQuery]
        public SingleResult<Question> Get([FromODataUri] int key)
        {            
            IQueryable<Question> result = Get()
                .Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

        [Authorize(Roles = Roles.Elevated)]
        public async Task<IHttpActionResult> Post(Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Questions.Add(question);
            await context.SaveChangesAsync();
            return Created(question);
        }

        [AcceptVerbs("POST", "PUT")]
        [Authorize(Roles = Roles.Elevated)]
        public async Task<IHttpActionResult> CreateRef(
            [FromODataUri] int key,
            string navigationProperty,
            [FromBody] Uri link)
        {
            var question = context.Questions.SingleOrDefault(p => p.Id == key);
            if (question == null)
            {
                return NotFound();
            }
                    
            var relatedKey = ODataUri.GetKeyFromUri<int>(Request, link);
            switch (navigationProperty)
            {
                case "Answer":
                    var answer = context.Answers.SingleOrDefault(f => f.Id == relatedKey);
                    if (answer == null) return NotFound();
                    question.Answers.Add(answer);
                    break;
                case "Category":
                    var category = context.Categories.SingleOrDefault(f => f.Id == relatedKey);
                    if (category == null) return NotFound();
                    question.Categories.Add(category);
                    break;
                default:
                    return StatusCode(HttpStatusCode.NotImplemented);
            }
            await context.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [EnableQuery]
        public IQueryable<Answer> GetAnswers([FromODataUri] int key)
        {
            return context.Questions
                .Where(m => m.Id.Equals(key))
                .SelectMany(m => m.Answers);
        }
    }
}
