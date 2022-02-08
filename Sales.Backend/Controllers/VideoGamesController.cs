using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sales.Backend.Models;
using Sales.Common.Models;
using Sales.Backend.Helpers;

namespace Sales.Backend.Controllers
{
    public class VideoGamesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: VideoGames
        public async Task<ActionResult> Index()
        {
            var videoGames = db.VideoGames.Include(v => v.VideoGameConsole);
            return View(await videoGames.ToListAsync());
        }

        // GET: VideoGames/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoGames videoGames = await db.VideoGames.FindAsync(id);
            if (videoGames == null)
            {
                return HttpNotFound();
            }
            return View(videoGames);
        }

        public ActionResult Create()
        {
            ViewBag.ID_VideoGameConsole = new SelectList(db.VideoGameConsoles, "ID_VideoGameConsole", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VideoGameView objvideogame)
        {
            if (ModelState.IsValid)
            {
                //Save image file
                var pic = string.Empty;
                var folder = "~/Content/Videogames";
                if (objvideogame.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(objvideogame.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }
                VideoGames videoGames = ToVideoGame(objvideogame, pic);


                db.VideoGames.Add(videoGames);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_VideoGameConsole = new SelectList(db.VideoGameConsoles, "ID_VideoGameConsole", "Description", objvideogame.ID_VideoGameConsole);
            return View(objvideogame);
        }

        private VideoGames ToVideoGame(VideoGameView objvideogame, string pic)
        {
            return new VideoGames
            {
                ID_VideoGameConsole = objvideogame.ID_VideoGameConsole,
                ID_VideoGames = objvideogame.ID_VideoGames,
                Description = objvideogame.Description,
                IsAvailable = objvideogame.IsAvailable,
                Price = objvideogame.Price,
                PublishOn = objvideogame.PublishOn,
                Remarks = objvideogame.Remarks,
                ImagePath = pic
            };
        }



        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoGames videoGames = await db.VideoGames.FindAsync(id);
            if (videoGames == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_VideoGameConsole = new SelectList(db.VideoGameConsoles, "ID_VideoGameConsole", "Description", videoGames.ID_VideoGameConsole);

            //ConvertVideoGames to VideoGamesView
            VideoGameView videoGameView = ToVideoGameView(videoGames);

            return View(videoGameView);
        }

        private VideoGameView ToVideoGameView(VideoGames videoGames)
        {
            return new VideoGameView
            {
                ID_VideoGameConsole = videoGames.ID_VideoGameConsole,
                ID_VideoGames = videoGames.ID_VideoGames,
                Description = videoGames.Description,
                IsAvailable = videoGames.IsAvailable,
                Price = videoGames.Price,
                PublishOn = videoGames.PublishOn,
                Remarks = videoGames.Remarks,
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VideoGameView objvideogame)
        {
            if (ModelState.IsValid)
            {
                //Save image file
                var pic = string.Empty;
                var folder = "~/Content/Videogames";
                if (objvideogame.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(objvideogame.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                //VideoGamesView to ConvertVideoGames
                VideoGames videoGames = ToVideoGame(objvideogame, pic);

                db.Entry(videoGames).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_VideoGameConsole = new SelectList(db.VideoGameConsoles, "ID_VideoGameConsole", "Description", objvideogame.ID_VideoGameConsole);
            return View(objvideogame);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoGames videoGames = await db.VideoGames.FindAsync(id);
            if (videoGames == null)
            {
                return HttpNotFound();
            }
            return View(videoGames);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VideoGames videoGames = await db.VideoGames.FindAsync(id);
            db.VideoGames.Remove(videoGames);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
