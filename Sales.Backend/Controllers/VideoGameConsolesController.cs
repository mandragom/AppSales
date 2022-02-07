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
    public class VideoGameConsolesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: VideoGameConsoles
        public async Task<ActionResult> Index()
        {
            return View(await db.VideoGameConsoles.ToListAsync());
        }

        // GET: VideoGameConsoles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoGameConsole videoGameConsole = await db.VideoGameConsoles.FindAsync(id);
            if (videoGameConsole == null)
            {
                return HttpNotFound();
            }
            return View(videoGameConsole);
        }

        // GET: VideoGameConsoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VideoGameConsoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_VideoGameConsole,Description,IsAvailable,PublishOn")] VideoGameConsole videoGameConsole)
        {
            if (ModelState.IsValid)
            {
                db.VideoGameConsoles.Add(videoGameConsole);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(videoGameConsole);
        }

        // GET: VideoGameConsoles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoGameConsole videoGameConsole = await db.VideoGameConsoles.FindAsync(id);
            if (videoGameConsole == null)
            {
                return HttpNotFound();
            }
            return View(videoGameConsole);
        }

        // POST: VideoGameConsoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_VideoGameConsole,Description,IsAvailable,PublishOn")] VideoGameConsole videoGameConsole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videoGameConsole).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(videoGameConsole);
        }

        // GET: VideoGameConsoles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoGameConsole videoGameConsole = await db.VideoGameConsoles.FindAsync(id);
            if (videoGameConsole == null)
            {
                return HttpNotFound();
            }
            return View(videoGameConsole);
        }

        // POST: VideoGameConsoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VideoGameConsole videoGameConsole = await db.VideoGameConsoles.FindAsync(id);
            db.VideoGameConsoles.Remove(videoGameConsole);
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
