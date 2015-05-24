using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reservaton.Service;
using ReservationDomain.Model;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BookYourFood.Controllers
{
    public class TableController : Controller
    {
        private readonly ITableService tableService;

        public TableController(ITableService tableService)
        {
            this.tableService = tableService;
        }
        public ActionResult Show()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult GetTables([DataSourceRequest] DataSourceRequest request)
        {
            var tables = tableService.GetTables();
            return Json(tables.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateTable([DataSourceRequest] DataSourceRequest request, Table table)
        {
            if (table != null && ModelState.IsValid)
            {
                tableService.CreateTable(table);
            }
            return Json(new[] { table }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult UpdateTable([DataSourceRequest] DataSourceRequest request, Table table)
        {
            if (table != null && ModelState.IsValid)
            {
                tableService.UpdateTable(table);
            }

            return Json(new[] { table }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult DestroyTable([DataSourceRequest] DataSourceRequest request, Table table)
        {
            if (table != null)
            {
                tableService.RemoveTable(table);
            }

            return Json(new[] { table }.ToDataSourceResult(request, ModelState));
        }
    }
}