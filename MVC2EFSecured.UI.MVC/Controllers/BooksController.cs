using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC2EFSecured.DATA.EF;

namespace MVC2EFSecured.UI.MVC.Controllers
{
    public class BooksController : Controller
    {
        private BookStoreEntities db = new BookStoreEntities();

        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.BookRoyalty).Include(b => b.BookStatus).Include(b => b.Genre).Include(b => b.Publisher);
            return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.BookID = new SelectList(db.BookRoyalties, "BookID", "BookID");
            ViewBag.BookStatusID = new SelectList(db.BookStatuses, "BookStatusID", "BookStatusName");
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName");
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,ISBN,BookTitle,Description,GenreID,Price,UnitsSold,PublishDate,PublisherID,BookImage,IsSiteFeature,IsGenreFeature,BookStatusID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookID = new SelectList(db.BookRoyalties, "BookID", "BookID", book.BookID);
            ViewBag.BookStatusID = new SelectList(db.BookStatuses, "BookStatusID", "BookStatusName", book.BookStatusID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", book.GenreID);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName", book.PublisherID);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookID = new SelectList(db.BookRoyalties, "BookID", "BookID", book.BookID);
            ViewBag.BookStatusID = new SelectList(db.BookStatuses, "BookStatusID", "BookStatusName", book.BookStatusID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", book.GenreID);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName", book.PublisherID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,ISBN,BookTitle,Description,GenreID,Price,UnitsSold,PublishDate,PublisherID,BookImage,IsSiteFeature,IsGenreFeature,BookStatusID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookID = new SelectList(db.BookRoyalties, "BookID", "BookID", book.BookID);
            ViewBag.BookStatusID = new SelectList(db.BookStatuses, "BookStatusID", "BookStatusName", book.BookStatusID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", book.GenreID);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName", book.PublisherID);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
