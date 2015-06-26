using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YoYoBookStore.Models;

namespace YoYoBookStore.Controllers
{
    public class BookInController : Controller
    {
        private BookStoreEntities db = new BookStoreEntities();

        // GET: /BookIn/
        public ActionResult Index()
        {
            
            //if(db.book.status=="在館")
            string query = "SELECT [id] , [name] , [author] , [status] FROM [book] WHERE [status] = '在館'";
            List<book> data = db.Database.SqlQuery<book>(query).ToList();
            
            if (data.Count() == 0) {
                var book = db.book.Include(b => b.bookDetail);
                return View(book.ToList());
            }
            return View(data);
        }

        // GET: /BookIn/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: /BookIn/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.bookDetail, "id", "id");
            return View();
        }

        // POST: /BookIn/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,name,author,status")] book book)
        {
            if (ModelState.IsValid)
            {
                db.book.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.bookDetail, "id", "id", book.id);
            return View(book);
        }

        // GET: /BookIn/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.bookDetail, "id", "id", book.id);
            return View(book);
        }

        // POST: /BookIn/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,name,author,status")] book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.bookDetail, "id", "id", book.id);
            return View(book);
        }

        // GET: /BookIn/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: /BookIn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            book book = db.book.Find(id);
            db.book.Remove(book);
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
