using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Sales.Common.Models;
using Sales.Domain.Models;

namespace Sales.API.Controllers
{
    public class VideoGameConsolesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/VideoGameConsoles
        public IQueryable<VideoGameConsole> GetVideoGameConsoles()
        {
            return db.VideoGameConsoles;
        }

        // GET: api/VideoGameConsoles/5
        [ResponseType(typeof(VideoGameConsole))]
        public async Task<IHttpActionResult> GetVideoGameConsole(int id)
        {
            VideoGameConsole videoGameConsole = await db.VideoGameConsoles.FindAsync(id);
            if (videoGameConsole == null)
            {
                return NotFound();
            }

            return Ok(videoGameConsole);
        }

        // PUT: api/VideoGameConsoles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVideoGameConsole(int id, VideoGameConsole videoGameConsole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != videoGameConsole.ID_VideoGameConsole)
            {
                return BadRequest();
            }

            db.Entry(videoGameConsole).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoGameConsoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/VideoGameConsoles
        [ResponseType(typeof(VideoGameConsole))]
        public async Task<IHttpActionResult> PostVideoGameConsole(VideoGameConsole videoGameConsole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VideoGameConsoles.Add(videoGameConsole);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = videoGameConsole.ID_VideoGameConsole }, videoGameConsole);
        }

        // DELETE: api/VideoGameConsoles/5
        [ResponseType(typeof(VideoGameConsole))]
        public async Task<IHttpActionResult> DeleteVideoGameConsole(int id)
        {
            VideoGameConsole videoGameConsole = await db.VideoGameConsoles.FindAsync(id);
            if (videoGameConsole == null)
            {
                return NotFound();
            }

            db.VideoGameConsoles.Remove(videoGameConsole);
            await db.SaveChangesAsync();

            return Ok(videoGameConsole);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VideoGameConsoleExists(int id)
        {
            return db.VideoGameConsoles.Count(e => e.ID_VideoGameConsole == id) > 0;
        }
    }
}