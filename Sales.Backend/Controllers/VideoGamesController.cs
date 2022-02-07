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

        // GET: VideoGames/Create
        public ActionResult Create()
        {
            ViewBag.ID_VideoGameConsole = new SelectList(db.VideoGameConsoles, "ID_VideoGameConsole", "Description");
            return View();
        }

        // POST: VideoGames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_VideoGames,ID_VideoGameConsole,Description,Remarks,ImagePath,Price,IsAvailable,PublishOn")] VideoGames videoGames)
        {
            if (ModelState.IsValid)
            {
                db.VideoGames.Add(videoGames);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_VideoGameConsole = new SelectList(db.VideoGameConsoles, "ID_VideoGameConsole", "Description", videoGames.ID_VideoGameConsole);
            return View(videoGames);
        }

        // GET: VideoGames/Edit/5
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
            return View(videoGames);
        }

        // POST: VideoGames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_VideoGames,ID_VideoGameConsole,Description,Remarks,ImagePath,Price,IsAvailable,PublishOn")] VideoGames videoGames)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videoGames).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_VideoGameConsole = new SelectList(db.VideoGameConsoles, "ID_VideoGameConsole", "Description", videoGames.ID_VideoGameConsole);
            return View(videoGames);
        }

        // GET: VideoGames/Delete/5
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

        // POST: VideoGames/Delete/5
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
