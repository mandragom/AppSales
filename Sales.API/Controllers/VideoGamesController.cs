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
    public class VideoGamesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/VideoGames
        public IQueryable<VideoGames> GetVideoGames()
        {
            return db.VideoGames;
        }

        // GET: api/VideoGames/5
        [ResponseType(typeof(VideoGames))]
        public async Task<IHttpActionResult> GetVideoGames(int id)
        {
            VideoGames videoGames = await db.VideoGames.FindAsync(id);
            if (videoGames == null)
            {
                return NotFound();
            }

            return Ok(videoGames);
        }

        // PUT: api/VideoGames/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVideoGames(int id, VideoGames videoGames)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != videoGames.ID_VideoGames)
            {
                return BadRequest();
            }

            db.Entry(videoGames).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoGamesExists(id))
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

        // POST: api/VideoGames
        [ResponseType(typeof(VideoGames))]
        public async Task<IHttpActionResult> PostVideoGames(VideoGames videoGames)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VideoGames.Add(videoGames);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = videoGames.ID_VideoGames }, videoGames);
        }

        // DELETE: api/VideoGames/5
        [ResponseType(typeof(VideoGames))]
        public async Task<IHttpActionResult> DeleteVideoGames(int id)
        {
            VideoGames videoGames = await db.VideoGames.FindAsync(id);
            if (videoGames == null)
            {
                return NotFound();
            }

            db.VideoGames.Remove(videoGames);
            await db.SaveChangesAsync();

            return Ok(videoGames);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VideoGamesExists(int id)
        {
            return db.VideoGames.Count(e => e.ID_VideoGames == id) > 0;
        }
    }
}