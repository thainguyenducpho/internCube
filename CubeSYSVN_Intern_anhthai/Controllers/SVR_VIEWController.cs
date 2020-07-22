using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CubeSYSVN_Intern_anhthai.Models;
using System.Linq.Dynamic;
using PagedList;
using Newtonsoft.Json;
using System.Reflection;

namespace CubeSYSVN_Intern_anhthai.Controllers
{
    public class SVR_VIEWController : Controller
    {
        anhthaiEntities db = new anhthaiEntities();

        public class HttpParamActionAttribute : ActionNameSelectorAttribute
        {
            public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
            {
                if (actionName.Equals(methodInfo.Name, StringComparison.InvariantCultureIgnoreCase))
                    return true;

                var request = controllerContext.RequestContext.HttpContext.Request;
                return request[methodInfo.Name] != null;
            }
        }
        public ActionResult Index(int? size, int? page, string sortProperty, string sortOrder, string searchString)
        {
            // 1. Tạo biến ViewBag gồm sortOrder, searchValue, sortProperty và page
            if (sortOrder == "asc") ViewBag.sortOrder = "desc";
            if (sortOrder == "desc") ViewBag.sortOrder = "";
            if (sortOrder == "") ViewBag.sortOrder = "asc";
            ViewBag.searchValue = searchString;
            ViewBag.sortProperty = sortProperty;
            ViewBag.page = page;

            // 2. Tạo danh sách chọn số trang
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });

            // 2.1. Thiết lập số trang đang chọn vào danh sách List<SelectListItem> items
            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }
            ViewBag.size = items;
            ViewBag.currentSize = size;

            // 3. Lấy tất cả tên thuộc tính của lớp SVR_VIEW ()
            var properties = typeof(SVR_VIEW).GetProperties();
            List<Tuple<string, bool, int>> list = new List<Tuple<string, bool, int>>();
            foreach (var item in properties)
            {
                int order = 999;
                var isVirtual = item.GetAccessors()[0].IsVirtual;

                if (item.Name == "LOG_ID") continue; // Không hiển thị cột này
                if (item.Name == "IMEI") order = 1;
                if (item.Name == "BUMON_ID") continue;
                if (item.Name == "BUMON_NAME") order = 2;
                if (item.Name == "STORE_CD") continue;
                if (item.Name == "STORE_NAME") order = 3;
                if (item.Name == "PRODUCT_ID") continue;
                if (item.Name == "PRODUCT_NAME") order = 4;
                if (item.Name == "VIEW_DATE") order = 5;
                if (item.Name == "GENDER") continue;
                if (item.Name == "AGE") continue;
                if (item.Name == "VIEWS") order = 6;
                if (item.Name == "INSERT_DATE") order = 7;
                if (item.Name == "UPDATE_DATE") order = 8;
                if (item.Name == "SVR_BUMON") continue;
                if (item.Name == "SVR_STORE") continue;
                Tuple<string, bool, int> t = new Tuple<string, bool, int>(item.Name, isVirtual, order);
                list.Add(t);
            }
            list = list.OrderBy(x => x.Item3).ToList();

            // 3.1. Tạo Heading sắp xếp cho các cột
            foreach (var item in list)
            {
                if (!item.Item2)
                {
                    if (sortOrder == "desc" && sortProperty == item.Item1)
                    {
                        ViewBag.Headings += "<th><a href='?page=" + page + "&size=" + ViewBag.currentSize + "&sortProperty=" + item.Item1 + "&sortOrder=" +
                            ViewBag.sortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-desc'></i></th></a></th>";
                    }
                    else if (sortOrder == "asc" && sortProperty == item.Item1)
                    {
                        ViewBag.Headings += "<th><a href='?page=" + page + "&size=" + ViewBag.currentSize + "&sortProperty=" + item.Item1 + "&sortOrder=" +
                            ViewBag.sortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-asc'></a></th>";
                    }
                    else
                    {
                        ViewBag.Headings += "<th><a href='?page=" + page + "&size=" + ViewBag.currentSize + "&sortProperty=" + item.Item1 + "&sortOrder=" +
                           ViewBag.sortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort'></a></th>";
                    }

                }
                else ViewBag.Headings += "<th>" + item.Item1 + "</th>";
            }
            var links = from l in db.SVR_VIEW
                        select l;

            // 5. Tạo thuộc tính sắp xếp mặc định là "LOG_ID"
            if (String.IsNullOrEmpty(sortProperty)) sortProperty = "LOG_ID";

            // 5. Sắp xếp tăng/giảm bằng phương thức OrderBy sử dụng trong thư viện Dynamic LINQ
            if (sortOrder == "desc") links = links.OrderBy(sortProperty + " desc");
            else if (sortOrder == "asc") links = links.OrderBy(sortProperty);
            else links = links.OrderBy("LOG_ID");

            // 5.1. Thêm phần tìm kiếm
            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.IMEI.Contains(searchString) || s.STORE_CD == searchString || s.VIEW_DATE == searchString);
            }

            // 5.2. Nếu page = null thì đặt lại là 1.
            page = page ?? 1; //if (page == null) page = 1;

            // 5.3. Tạo kích thước trang (pageSize), mặc định là 5.
            int pageSize = (size ?? 5);

            ViewBag.pageSize = pageSize;

            // 6. Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber. --- dammio.com
            int pageNumber = (page ?? 1);

            // 6.2 Lấy tổng số record chia cho kích thước để biết bao nhiêu trang
            int checkTotal = (int)(links.ToList().Count / pageSize) + 1;
            // Nếu trang vượt qua tổng số trang thì thiết lập là 1 hoặc tổng số trang
            if (pageNumber > checkTotal) pageNumber = checkTotal;

            // 7. Trả về các Link được phân trang theo kích thước và số trang.
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost, HttpParamAction]
        public ActionResult Reset()
        {
            ViewBag.searchValue = "";
            Index(null, null, "", "", "");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DateTime? fromDate, DateTime? toDate)
        {

            var view = db.SVR_VIEW.Where(x => x.INSERT_DATE >= fromDate && x.INSERT_DATE <= toDate).ToList();

            return View(view);
        }

        // GET: SVR_VIEW/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SVR_VIEW sVR_VIEW = db.SVR_VIEW.Find(id);
            if (sVR_VIEW == null)
            {
                return HttpNotFound();
            }
            return View(sVR_VIEW);
        }

        // GET: SVR_VIEW/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SVR_VIEW/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LOG_ID,IMEI,BUMON_ID,STORE_CD,PRODUCT_ID,VIEW_DATE,GENDER,AGE,VIEWS,INSERT_DATE,UPDATE_DATE")] SVR_VIEW sVR_VIEW)
        {
            if (ModelState.IsValid)
            {
                db.SVR_VIEW.Add(sVR_VIEW);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sVR_VIEW);
        }

        // GET: SVR_VIEW/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SVR_VIEW sVR_VIEW = db.SVR_VIEW.Find(id);
            if (sVR_VIEW == null)
            {
                return HttpNotFound();
            }
            return View(sVR_VIEW);
        }

        // POST: SVR_VIEW/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LOG_ID,IMEI,BUMON_ID,STORE_CD,PRODUCT_ID,VIEW_DATE,GENDER,AGE,VIEWS,INSERT_DATE,UPDATE_DATE")] SVR_VIEW sVR_VIEW)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sVR_VIEW).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sVR_VIEW);
        }

        // GET: SVR_VIEW/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SVR_VIEW sVR_VIEW = db.SVR_VIEW.Find(id);
            if (sVR_VIEW == null)
            {
                return HttpNotFound();
            }
            return View(sVR_VIEW);
        }

        // POST: SVR_VIEW/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SVR_VIEW sVR_VIEW = db.SVR_VIEW.Find(id);
            db.SVR_VIEW.Remove(sVR_VIEW);
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
