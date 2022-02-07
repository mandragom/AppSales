using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
        public List<VideoGames> GetVideoGames()
        {
            string query;
            VideoGames objvideogames = new VideoGames();
            List<VideoGames> listvideogames = new List<VideoGames>();

            using (SqlConnection connection = new SqlConnection())
            {

                connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                connection.Open();

                query = "SELECT ga.ID_VideoGames, ga.Description, isnull(ga.Remarks, '') as Remarks, isnull(ga.ImagePath, '') as ImagePath, ga.Price, ga.IsAvailable, ga.PublishOn, " +
                        "ga.ID_VideoGameConsole, co.Description as DescriptionConsole, co.IsAvailable as IsAvailableConsole, co.PublishOn as " +
                        "PublishOnConsole from dbo.VideoGames ga inner join VideoGameConsoles co on (co.ID_VideoGameConsole = ga.ID_VideoGameConsole)";

                using (SqlCommand varcommand = new SqlCommand(query, connection))
                {
                    //Exec the command
                    using (SqlDataReader reader = varcommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Get the Customer info
                            objvideogames = new VideoGames()
                            {
                                ID_VideoGames = (int)reader["ID_VideoGames"],
                                Description = (string)reader["Description"],
                                Remarks = (string)reader["Remarks"],
                                ImagePath = (string)reader["ImagePath"],
                                Price = (decimal)reader["Price"],
                                IsAvailable = (bool)reader["IsAvailable"],
                                PublishOn = (DateTime)reader["PublishOn"],
                                ID_VideoGameConsole = (int)reader["ID_VideoGameConsole"],
                                VideoGameConsole = new VideoGameConsole()
                                {
                                    Description = (string)reader["DescriptionConsole"],
                                    IsAvailable = (bool)reader["IsAvailableConsole"],
                                    PublishOn = (DateTime)reader["PublishOnConsole"],
                                }
                            };

                            listvideogames.Add(objvideogames);
                        }
                    }
                }

            }

            return listvideogames;
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