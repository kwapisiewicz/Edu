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
    public class AnswersController : ODataController
    {
        ExamContext context = new ExamContext();

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
        public IQueryable<Answer> Get()
        {
            return context.Answers;
        }

        [EnableQuery]
        public SingleResult<Answer> Get([FromODataUri] int key)
        {
            IQueryable<Answer> result = Get()
                .Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

        [Authorize(Roles = Roles.Elevated)]
        public async Task<IHttpActionResult> Post(Answer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Answers.Add(answer);
            await context.SaveChangesAsync();
            return Created(answer);
        }

        [AcceptVerbs("POST", "PUT")]
        [Authorize(Roles = Roles.Elevated)]
        public async Task<IHttpActionResult> CreateRef(
            [FromODataUri] int key,
            string navigationProperty,
            [FromBody] Uri link)
        {
            var answer = context.Answers.SingleOrDefault(p => p.Id == key);
            if (answer == null)
            {
                return NotFound();
            }

            var relatedKey = ODataUri.GetKeyFromUri<int>(Request, link);
            switch (navigationProperty)
            {
                case "Answer":
                    var score = context.Scores.SingleOrDefault(f => f.Id == relatedKey);
                    if (answer == null) return NotFound();
                    answer.Score = score;
                    break;
                default:
                    return StatusCode(HttpStatusCode.NotImplemented);
            }
            await context.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
