using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ReservationDomain.Model;
using Reservaton.Service;

namespace BookYourFood.Controllers
{
    [Authorize(Roles = "Administrator,Restaurant")]
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