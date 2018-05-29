using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TARS2.DBModel;

namespace TARS2.Controllers
{
    public class ReportController : Controller
    {
        private readonly WebFeedRepositoryEntities db = new WebFeedRepositoryEntities();

        //
        // GET: /Reports/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View("reports");
        }
        public ActionResult DownloadCorporateAction()
        {
            DateTime StartDate = new DateTime(1900, 01, 01);
            DateTime EndDate = new DateTime(2100, 01, 01);
            ViewBag.CorporateActionsList = db.usp_CorporateActionListing(StartDate.Date, EndDate.Date).ToList();
            return View();
        }

        public ActionResult DownloadDividend()
        {
            DateTime StartDate = new DateTime(1900, 01, 01);
            DateTime EndDate = new DateTime(2100, 01, 01);
            ViewBag.DividendList = db.usp_CorporateActionListing(StartDate.Date, EndDate.Date).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult DownloadCorporateAction(DateTime fromDate, DateTime toDate)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Server.MapPath("~/Rpts/CA Listing_GV.rpt"));
            rd.SetParameterValue("StartDate", fromDate.Date.ToString("yyyy-MM-dd"));
            rd.SetParameterValue("EndDate", toDate.Date.ToString("yyyy-MM-dd"));
            rd.SetDatabaseLogon("jse_webapps", "St0ckExch@ng3", "10.240.18.202", "WebFeedRepository");
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "CorporateActionsList.pdf");
        }

        [HttpPost]
        public ActionResult DownloadDividend(DateTime fromDate, DateTime toDate)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Server.MapPath("~/Rpts/Dividend Listing_GV.rpt"));
            rd.SetParameterValue("StartDate", fromDate.Date.ToString("yyyy-MM-dd"));
            rd.SetParameterValue("EndDate", toDate.Date.ToString("yyyy-MM-dd"));
            rd.SetDatabaseLogon("jse_webapps", "St0ckExch@ng3", "10.240.18.202", "WebFeedRepository");
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "DividendList.pdf");
        }
    }
}