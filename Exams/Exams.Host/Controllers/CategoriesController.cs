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
    public class CategoriesController : ODataController
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
        public IQueryable<Category> Get()
        {
            return context.Categories;
        }

        [EnableQuery]
        public SingleResult<Category> Get([FromODataUri] int key)
        {
            IQueryable<Category> result = Get()
                .Where(p => p.Id == key);
            return SingleResult.Create(result);
        }


        public async Task<IHttpActionResult> Post(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return Created(category);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var category = await context.Categories.FindAsync(key);
            if (category == null)
            {
                return NotFound();
            }
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
