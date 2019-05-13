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
        public ActionResult Index(string search, string BookStatusList, string BookKeeperList,string BookClassNameList)
        {
            var SearchData = db.BookData.OrderBy(m => m.BOOK_ID).ToList();
            var book = db.BookData.Where(x => x.BOOK_NAME.Contains(search) || search == null).ToList();

            List<SelectListItem> StatusList = new List<SelectListItem>();
            StatusList.Add(new SelectListItem { Text = "All", Value = "", Selected = true });
            StatusList.Add(new SelectListItem { Text = "可以借出", Value = "A" });
            StatusList.Add(new SelectListItem { Text = "已借出", Value = "B" });
            StatusList.Add(new SelectListItem { Text = "不可借出", Value = "U" });
            StatusList.Add(new SelectListItem { Text = "已借出(未領)", Value = "C" });

            ViewBag.StatusList = StatusList;

            List<SelectListItem> KeeperList = new List<SelectListItem>();
            StatusList.Add(new SelectListItem { Text = "All", Value = "", Selected = true });
            StatusList.Add(new SelectListItem { Text = "可以借出", Value = "A" });
            StatusList.Add(new SelectListItem { Text = "已借出", Value = "B" });
            StatusList.Add(new SelectListItem { Text = "不可借出", Value = "U" });
            StatusList.Add(new SelectListItem { Text = "已借出(未領)", Value = "C" });
            ViewBag.KeeperList = KeeperList;

            List<SelectListItem> ClassNameList = new List<SelectListItem>();
            StatusList.Add(new SelectListItem { Text = "All", Value = "", Selected = true });
            StatusList.Add(new SelectListItem { Text = "可以借出", Value = "A" });
            StatusList.Add(new SelectListItem { Text = "已借出", Value = "B" });
            StatusList.Add(new SelectListItem { Text = "不可借出", Value = "U" });
            StatusList.Add(new SelectListItem { Text = "已借出(未領)", Value = "C" });
            ViewBag.ClassNameList = ClassNameList;

            return View(book);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookData data)
        {
            db.BookData.Add(data);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Delete (int BOOK_ID)
        {
            var data = db.BookData.Where(m => m.BOOK_ID == BOOK_ID).FirstOrDefault();
            db.BookData.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int BOOK_ID)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(BookData data)
        {
            var time = DateTime.Today;
            int id = data.BOOK_ID;
            var bookdata = db.BookData.Where(m => m.BOOK_ID == id).FirstOrDefault();
            bookdata.BOOK_AUTHOR = data.BOOK_AUTHOR;
            bookdata.BOOK_BOUGHT_DATE = data.BOOK_BOUGHT_DATE;
            bookdata.BOOK_CLASS_ID = data.BOOK_CLASS_ID;
            bookdata.BOOK_KEEPER = data.BOOK_KEEPER;
            bookdata.BOOK_NAME= data.BOOK_NAME;
            bookdata.BOOK_NOTE= data.BOOK_NOTE;
            bookdata.BOOK_PUBLISHER= data.BOOK_PUBLISHER;
            bookdata.BOOK_STATUS= "a";
            bookdata.CREATE_DATE = time;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}