using System;
using CubeSYSVN_Intern_anhthai.Models;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using DataTables.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CubeSYSVN_Intern_anhthai.Controllers
{
    public class SVR_VERSIONController : Controller
    {

        //private ApplicationDbContext _dbContext;

        //public ApplicationDbContext DbContext
        //{
        //    get
        //    {
        //        return _dbContext ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>();
        //    }
        //    private set
        //    {
        //        _dbContext = value;
        //    }

        //}

        //public SVR_VERSIONController()
        //{

        //}

        //public SVR_VERSIONController(ApplicationDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        //// GET: Asset
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        //{
        //    IQueryable<SVR_VERSION> query = DbContext.SVR_VERSION;
        //    var totalCount = query.Count();

        //    if (requestModel.Search.Value != string.Empty)
        //    {
        //        var value = requestModel.Search.Value.Trim();
        //        query = query.Where(p => p.SVR_BUMON.BUMON_NAME.Contains(value) ||
        //                                 p.INSERT_USER.Contains(value) ||
        //                                 p.UPDATE_USER.Contains(value)
        //                           );
        //    }

        //    // searching and sorting
        //    query = SearchVersion(requestModel, query);
        //    var filteredCount = query.Count();

        //    // Paging
        //    query = query.Skip(requestModel.Start).Take(requestModel.Length);

        //    var data = query.Select(ver => new
        //    {
        //        BUMON_ID = ver.BUMON_ID,
        //        VERSION_NO = ver.VERSION_NO,
        //        TYPE_SEND = ver.TYPE_SEND,
        //        FROM_DATE = ver.FROM_DATE,
        //        TO_DATE = ver.TO_DATE,
        //        INSERT_DATE = ver.INSERT_DATE,
        //        INSERT_USER = ver.INSERT_USER,
        //        UPDATE_DATE = ver.UPDATE_DATE,
        //        UPDATE_USER = ver.UPDATE_USER,
        //        DEL_FLAG = ver.DEL_FLAG,
        //        BUMON_NAME = ver.SVR_BUMON.BUMON_NAME
        //    }).ToList();

        //    return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

        //}


        //// GET: Asset/Create
        //public ActionResult Create()
        //{
        //    var model = new SVR_VERSION();
        //    model.BumonSelectList = GetBumonSelectList();
        //    return View("_CreatePartial", model);
        //}

        //// POST: Asset/Create
        //[HttpPost]
        //public async Task<ActionResult> Create(SVR_VERSION assetVM)
        //{

        //    assetVM.BumonSelectList = GetBumonSelectList();

        //    if (!ModelState.IsValid)
        //        return View("_CreatePartial", assetVM);

        //    SVR_VERSION sVR_VERSION = MaptoModel(assetVM);

        //    DbContext.SVR_VERSION.Add(sVR_VERSION);
        //    var task = DbContext.SaveChangesAsync();
        //    await task;

        //    if (task.Exception != null)
        //    {
        //        ModelState.AddModelError("", "Unable to add the Asset");
        //        return View("_CreatePartial", assetVM);
        //    }

        //    return Content("success");
        //}

        //// GET: Asset/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    var sVR_VERSION = DbContext.SVR_VERSION.FirstOrDefault(x => x.BUMON_ID == id);

        //    SVR_VERSION assetViewModel = MapToViewModel(sVR_VERSION);

        //    if (Request.IsAjaxRequest())
        //        return PartialView("_EditPartial", assetViewModel);
        //    return View(assetViewModel);
        //}

        //// POST: Asset/Edit/5
        //[HttpPost]
        //public async Task<ActionResult> Edit(SVR_VERSION assetVM)
        //{

        //    assetVM.BumonSelectList = GetBumonSelectList(assetVM.BUMON_ID);

        //    if (!ModelState.IsValid)
        //    {
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //        return View(Request.IsAjaxRequest() ? "_EditPartial" : "Edit", assetVM);
        //    }

        //    SVR_VERSION sVR_VERSION = MaptoModel(assetVM);

        //    DbContext.SVR_VERSION.Attach(sVR_VERSION);
        //    DbContext.Entry(sVR_VERSION).State = EntityState.Modified;
        //    var task = DbContext.SaveChangesAsync();
        //    await task;

        //    if (task.Exception != null)
        //    {
        //        ModelState.AddModelError("", "Unable to update the Asset");
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //        return View(Request.IsAjaxRequest() ? "_EditPartial" : "Edit", assetVM);
        //    }

        //    if (Request.IsAjaxRequest())
        //    {
        //        return Content("success");
        //    }

        //    return RedirectToAction("Index");

        //}

        //public async Task<ActionResult> Details(int id)
        //{
        //    var sVR_VERSION = await DbContext.SVR_VERSION.FirstOrDefaultAsync(x => x.BUMON_ID == id);
        //    var assetVM = MapToViewModel(sVR_VERSION);

        //    if (Request.IsAjaxRequest())
        //        return PartialView("_detailsPartial", assetVM);

        //    return View(assetVM);
        //}

        //// GET: Asset/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    var sVR_VERSION = DbContext.SVR_VERSION.FirstOrDefault(x => x.BUMON_ID == id);

        //    SVR_VERSION assetViewModel = MapToViewModel(sVR_VERSION);

        //    if (Request.IsAjaxRequest())
        //        return PartialView("_DeletePartial", assetViewModel);
        //    return View(assetViewModel);
        //}

        //// POST: Asset/Delete/5
        //[HttpPost, ActionName("Delete")]
        //public async Task<ActionResult> DeleteAsset(int BUMON_ID)
        //{
        //    var sVR_VERSION = new SVR_VERSION { BUMON_ID = BUMON_ID };
        //    DbContext.SVR_VERSION.Attach(sVR_VERSION);
        //    DbContext.SVR_VERSION.Remove(sVR_VERSION);

        //    var task = DbContext.SaveChangesAsync();
        //    await task;

        //    if (task.Exception != null)
        //    {
        //        ModelState.AddModelError("", "Unable to Delete the Asset");
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //        SVR_VERSION assetVM = MapToViewModel(sVR_VERSION);
        //        return View(Request.IsAjaxRequest() ? "_DeletePartial" : "Delete", assetVM);
        //    }

        //    if (Request.IsAjaxRequest())
        //    {
        //        return Content("success");
        //    }

        //    return RedirectToAction("Index");

        //}



        //private IQueryable<SVR_VERSION> SearchVersion(IDataTablesRequest requestModel, IQueryable<SVR_VERSION> query)
        //{
        //    // Apply filters
        //    if (requestModel.Search.Value != string.Empty)
        //    {
        //        var value = requestModel.Search.Value.Trim();
        //        query = query.Where(p => p.SVR_BUMON.BUMON_NAME.Contains(value) ||
        //                                 p.INSERT_USER.Contains(value) ||
        //                                 p.UPDATE_USER.Contains(value)
        //                           );
        //    }

        //    var filteredCount = query.Count();

        //    // Sort
        //    var sortedColumns = requestModel.Columns.GetSortedColumns();
        //    var orderByString = String.Empty;

        //    foreach (var column in sortedColumns)
        //    {
        //        orderByString += orderByString != String.Empty ? "," : "";
        //        orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
        //    }

        //    query = query.OrderBy(orderByString == string.Empty ? "BUMON_NAME asc" : orderByString);

        //    return query;

        //}

        //private SelectList GetBumonSelectList(object selectedValue = null)
        //{
        //    return new SelectList(DbContext.SVR_BUMON.Select(x => new { x.BUMON_ID, x.BUMON_NAME }),
        //                                                         "BUMON_ID",
        //                                                         "BUMON_NAME", selectedValue);
        //}

        //private SVR_VERSION MapToViewModel(SVR_VERSION sVR_VERSION)
        //{
        //    var facilitySite = DbContext.SVR_VERSION.Where(x => x.BUMON_ID == sVR_VERSION.BUMON_ID).FirstOrDefault();

        //    SVR_VERSION assetViewModel = new SVR_VERSION()
        //    {
        //        BUMON_ID = sVR_VERSION.BUMON_ID,
        //        VERSION_NO = sVR_VERSION.VERSION_NO,
        //        TYPE_SEND = sVR_VERSION.TYPE_SEND,
        //        FROM_DATE = sVR_VERSION.FROM_DATE,
        //        TO_DATE = sVR_VERSION.TO_DATE,
        //        INSERT_DATE = sVR_VERSION.INSERT_DATE,
        //        INSERT_USER = sVR_VERSION.INSERT_USER,
        //        UPDATE_DATE = sVR_VERSION.UPDATE_DATE,
        //        UPDATE_USER = sVR_VERSION.UPDATE_USER,
        //        DEL_FLAG = sVR_VERSION.DEL_FLAG,
        //        BUMON_NAME = sVR_VERSION.BUMON_NAME
        //    };

        //    return assetViewModel;
        //}

        //private SVR_VERSION MaptoModel(SVR_VERSION assetVM)
        //{
        //    SVR_VERSION sVR_VERSION = new SVR_VERSION()
        //    {
        //        BUMON_ID = assetVM.BUMON_ID,
        //        VERSION_NO = assetVM.VERSION_NO,
        //        TYPE_SEND = assetVM.TYPE_SEND,
        //        FROM_DATE = assetVM.FROM_DATE,
        //        TO_DATE = assetVM.TO_DATE,
        //        INSERT_DATE = assetVM.INSERT_DATE,
        //        INSERT_USER = assetVM.INSERT_USER,
        //        UPDATE_DATE = assetVM.UPDATE_DATE,
        //        UPDATE_USER = assetVM.UPDATE_USER,
        //        DEL_FLAG = assetVM.DEL_FLAG,
        //        BUMON_NAME = assetVM.BUMON_NAME
        //    };

        //    return sVR_VERSION;
        //}
    }
}