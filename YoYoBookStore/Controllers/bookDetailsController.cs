using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YoYoBookStore.Models;

namespace YoYoBookStore.Controllers
{
    public class bookDetailsController : Controller
    {
        private BookStoreEntities db = new BookStoreEntities();

        // GET: bookDetails
        public ActionResult Index()
        {
            var bookDetail = db.bookDetail.Include(b => b.book).Include(b => b.member);
            return View(bookDetail.ToList());
        }

        // GET: bookDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookDetail bookDetail = db.bookDetail.Find(id);
            if (bookDetail == null)
            {
                return HttpNotFound();
            }
            return View(bookDetail);
        }

        // GET: bookDetails/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.book, "id", "name");
            ViewBag.id = new SelectList(db.member, "id", "name");
            return View();
        }

        // POST: bookDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,member_id,book_id,date")] bookDetail bookDetail)
        {
            if (ModelState.IsValid)
            {
                db.bookDetail.Add(bookDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.book, "id", "name", bookDetail.id);
            ViewBag.id = new SelectList(db.member, "id", "name", bookDetail.id);
            return View(bookDetail);
        }

        // GET: bookDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookDetail bookDetail = db.bookDetail.Find(id);
            if (bookDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.book, "id", "name", bookDetail.id);
            ViewBag.id = new SelectList(db.member, "id", "name", bookDetail.id);
            return View(bookDetail);
        }

        // POST: bookDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,member_id,book_id,date")] bookDetail bookDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.book, "id", "name", bookDetail.id);
            ViewBag.id = new SelectList(db.member, "id", "name", bookDetail.id);
            return View(bookDetail);
        }

        // GET: bookDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookDetail bookDetail = db.bookDetail.Find(id);
            if (bookDetail == null)
            {
                return HttpNotFound();
            }
            return View(bookDetail);
        }

        // POST: bookDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bookDetail bookDetail = db.bookDetail.Find(id);
            db.bookDetail.Remove(bookDetail);
            db.SaveChanges();
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
