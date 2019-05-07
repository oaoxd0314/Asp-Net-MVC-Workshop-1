using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Book.Models;
namespace Book.Controllers
{
    public class HomeController : Controller
    {
        DataBaseEntities db = new DataBaseEntities();
        // GET: Home
        public ActionResult Index()
        {
            var HBookData = db.BookData.OrderBy(m => m.BOOK_ID).ToList();
            return View(HBookData);
        }

        public ActionResult Creat()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Creat(BookData CBookData)
        {
            db.BookData.Add(CBookData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int bookid)
        {
            var BOOKID = db.BookData.Where(m => m.BOOK_ID == bookid).FirstOrDefault();
            db.BookData.Remove(BOOKID);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int bookid=0)
        {
            var book_id = db.BookData.Where(m => m.BOOK_ID == bookid).FirstOrDefault();
            return View(book_id);
        }

        [HttpPost]
        public ActionResult Edit(BookData eBookData)
        {
            DateTime thisDay = DateTime.Today;
            int bookid = eBookData.BOOK_ID;

            var Datas = db.BookData.Where(m => m.BOOK_ID == bookid).FirstOrDefault();

            Datas.BOOK_NAME = eBookData.BOOK_NAME;
            Datas.BOOK_AUTHOR = eBookData.BOOK_AUTHOR;
            Datas.BOOK_PUBLISHER = eBookData.BOOK_PUBLISHER;
            Datas.BOOK_NOTE = eBookData.BOOK_NOTE;
            Datas.BOOK_BOUGHT_DATE = eBookData.BOOK_BOUGHT_DATE;
            Datas.BOOK_CLASS_ID = eBookData.BOOK_CLASS_ID;
            Datas.BOOK_STATUS = eBookData.BOOK_STATUS;
            Datas.BOOK_KEEPER = eBookData.BOOK_KEEPER;
            Datas.MODIFY_DATE = thisDay;
            Datas.MODIFY_USER = eBookData.MODIFY_USER;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult search(string searching)
        {
            //show Book_data 內的資料 遞減排序
            var book = db.BookData.Where(x => x.BOOK_NAME.Contains(searching) || searching == null).ToList();
            return View(book);
        }

    }
}