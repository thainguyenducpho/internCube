using CubeSYSVN_Intern_anhthai.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace CubeSYSVN_Intern_anhthai.Controllers
{
    public class HomeController : Controller
    {
        // GET: /data/

        anhthaiEntities db = new anhthaiEntities();
        public ActionResult Index()
        {
            List<SVR_BUMON> DeptList = db.SVR_BUMON.ToList();
            ViewBag.ListOfBumon = new SelectList(DeptList, "BUMON_ID", "BUMON_NAME");
            return View();
        }

        public ActionResult GetData()
        {
            using (anhthaiEntities db = new anhthaiEntities())
            {
                List<Svr_VersionViewModel> verList = db.SVR_VERSION.Select(x => new Svr_VersionViewModel
                {
                    BUMON_ID = x.BUMON_ID,
                    VERSION_NO = x.VERSION_NO,
                    TYPE_SEND = x.TYPE_SEND,
                    FROM_DATE = x.FROM_DATE,
                    TO_DATE = x.TO_DATE,
                    INSERT_DATE = x.INSERT_DATE,
                    INSERT_USER = x.INSERT_USER,
                    UPDATE_DATE = x.UPDATE_DATE,
                    UPDATE_USER = x.UPDATE_USER,
                    DEL_FLAG = x.DEL_FLAG,
                    BUMON_NAME = x.SVR_BUMON.BUMON_NAME
                }).ToList();
                return Json(new { data = verList }, JsonRequestBehavior.AllowGet);
            }
        }
        ///////////////////getbyid/////////////////////////
        public JsonResult GetVersionById(int VERSION_NO)
        {
            SVR_VERSION model = db.SVR_VERSION.Where(x => x.VERSION_NO == VERSION_NO).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(SVR_VERSION model)
        {
            var result = false;
            try
            {
                String a = model.VERSION_NO.ToString();
                if (model.VERSION_NO > 0)
                {
                    SVR_VERSION ver = db.SVR_VERSION.SingleOrDefault(x => x.VERSION_NO == model.VERSION_NO);
                    ver.BUMON_ID = model.BUMON_ID;
                    ver.TYPE_SEND = model.TYPE_SEND;
                    ver.FROM_DATE = model.FROM_DATE;
                    ver.TO_DATE = model.TO_DATE;
                    ver.INSERT_DATE = model.INSERT_DATE;
                    ver.INSERT_USER = model.INSERT_USER;
                    ver.UPDATE_DATE = model.UPDATE_DATE;
                    ver.UPDATE_USER = model.UPDATE_USER;
                    ver.DEL_FLAG = model.DEL_FLAG;
                    //ver.BUMON_NAME = model.BUMON_NAME; /////code de phat sinh loi tai day
                    db.SaveChanges();
                    result = true;
                    return Json(new { result , success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    SVR_VERSION ver = new SVR_VERSION();
                    ver.BUMON_ID = model.BUMON_ID;
                    ver.TYPE_SEND = model.TYPE_SEND;
                    ver.FROM_DATE = model.FROM_DATE;
                    ver.TO_DATE = model.TO_DATE;
                    ver.INSERT_DATE = model.INSERT_DATE;
                    ver.INSERT_USER = model.INSERT_USER;
                    ver.UPDATE_DATE = model.UPDATE_DATE;
                    ver.UPDATE_USER = model.UPDATE_USER;
                    ver.DEL_FLAG = model.DEL_FLAG;
                    //ver.BUMON_NAME = model.BUMON_NAME;
                    db.SVR_VERSION.Add(ver);
                    db.SaveChanges();
                    result = true;
                    return Json(new { result, success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return Json(result, JsonRequestBehavior.AllowGet);
        }

        //////Delete/////
        [HttpPost]
        public JsonResult Delete(int id)
        {
            using (anhthaiEntities db = new anhthaiEntities())
            {
                SVR_VERSION emp = db.SVR_VERSION.Where(x => x.VERSION_NO == id).FirstOrDefault<SVR_VERSION>();
                db.SVR_VERSION.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}