using Exams.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;

namespace Exams.Host.Controllers
{
    public class ScoresController : ODataController
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
        public IQueryable<Score> Get()
        {
            return context.Scores;                
        }

        [EnableQuery]
        public SingleResult<Score> Get([FromODataUri] int key)
        {
            IQueryable<Score> result = Get()
                .Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(Score score)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Scores.Add(score);
            await context.SaveChangesAsync();
            return Created(score);
        }
    }
}
