using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TARS2.DBModel;
using log4net;
namespace TARS2.Controllers
{
    [Authorize]
    public class FinancialDueDatesController : Controller
    {
        private readonly WebFeedRepositoryEntities db = new WebFeedRepositoryEntities();

        static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private void GetViewDropDownData()
        {
            System.Collections.IEnumerable Symbols = db.usp_GetSymbols().ToList().Select(s => new SelectListItem
            {
                Text = s.InstrumentName.ToString(),
                Value = s.InstrumentCode.ToString()
            });
            
            System.Collections.IEnumerable Stocks = db.usp_GetStockTypes().ToList().Select(s => new SelectListItem
            {
                Text = s.StockType.ToString(),
                Value = s.StockTypeID.ToString()
            });

            System.Collections.IEnumerable Country = db.usp_GetCountries().ToList().Select(s => new SelectListItem
            {
                Text = s.Country,
                Value = s.CountryCode
            });
            
            ViewData["Symbols"] = Symbols;
            ViewData["Stocks"] = Stocks;
            ViewData["Country"] = Country;
        }

        // GET: FinancialDueDates
        public ActionResult Index()
        {
            GetViewDropDownData();
            var financialDueDates = db.FinancialDueDates.Include(f => f.Instrument);
            return View(financialDueDates.ToList());
        }

        [HttpPost]
        public ActionResult Index(string Symbols)
        {
            GetViewDropDownData();
            var div = from d in db.FinancialDueDates select d;
            if (!String.IsNullOrEmpty(Symbols))
            {
                div = div.Where(d => d.InstrumentCode == (Symbols)
                                       || d.Instrument.InstrumentName.Contains(Symbols)).OrderByDescending(d => d.EndFiscalYear);
            }
            return View(div.ToList());
        }
        // GET: FinancialDueDates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialDueDate financialDueDate = db.FinancialDueDates.Find(id);
            if (financialDueDate == null)
            {
                return HttpNotFound();
            }
            return View(financialDueDate);
        }

        // GET: FinancialDueDates/Create
        public ActionResult Create()
        {
           GetViewDropDownData();
            return View();
        }

        // POST: FinancialDueDates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,InstrumentID,StockTypeID,WebAddress,CountryCode,EndFiscalYear,Q1Due,Q2Due,Q3Due,Q4Due,AuditedDue,AnnualDue,Status,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,InstrumentCode")] FinancialDueDate financialDueDate)
        {
            GetViewDropDownData();
            int tempyr = Convert.ToDateTime(financialDueDate.EndFiscalYear).Year;
            var FinDD = db.FinancialDueDates.Where(f => f.InstrumentCode == financialDueDate.InstrumentCode && f.Year == tempyr).SingleOrDefault();

            if (FinDD != null)
            {
                ModelState.AddModelError("Symbols", "Financial due dates already exist for the selected Symbol.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.FinancialDueDates.Add(financialDueDate);
                    db.SaveChanges();
                    TempData["Message"] = "Financial Due Date Added Successfully.";
                    return RedirectToAction("Index",new { Symbols = financialDueDate.InstrumentCode});
                }
                catch(Exception exp)
                {
                    log.Error(exp);
                    ViewData["Message"] = "Error Adding Financial Due Date.";
                    ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                    return View(financialDueDate);
                }
            }

            ViewBag.InstrumentCode = new SelectList(db.Instruments, "InstrumentCode", "ISIN", financialDueDate.InstrumentCode);
            return View(financialDueDate);
        }

        // GET: FinancialDueDates/Edit/5
        public ActionResult Edit(int? id)
        {
            FinancialDueDate financialDueDate = db.FinancialDueDates.Find(id);
            GetViewDropDownData();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            if (financialDueDate == null)
            {
                return HttpNotFound();
            }
            //ViewBag.InstrumentCode = new SelectList(db.Instruments, "InstrumentCode", "ISIN", financialDueDate.InstrumentCode);
            return View(financialDueDate);
        }

        // POST: FinancialDueDates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InstrumentID,StockTypeID,WebAddress,CountryCode,EndFiscalYear,Q1Due,Q2Due,Q3Due,Q4Due,AuditedDue,AnnualDue,Status,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,InstrumentCode")] FinancialDueDate financialDueDate)
        {
            GetViewDropDownData();

            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(financialDueDate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Financial Due Date was Successfully Edited.";
                    return RedirectToAction("Index", new { Symbols = financialDueDate.InstrumentCode});
                }
                catch(Exception exp)
                {
                    log.Error(exp);
                    ViewData["Message"] = "Error Editing Financial Due Date.";
                    ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                    return View(financialDueDate);
                }
            }
          //  ViewBag.InstrumentCode = new SelectList(db.Instruments, "InstrumentCode", "ISIN", financialDueDate.InstrumentCode);
            return View(financialDueDate);
        }

        // GET: FinancialDueDates/Delete/5
        public ActionResult Delete(int? id)
        {
            FinancialDueDate financialDueDate = db.FinancialDueDates.Find(id);
            GetViewDropDownData();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (financialDueDate == null)
            {
                return HttpNotFound();
            }
            return View(financialDueDate);
        }

        // POST: FinancialDueDates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FinancialDueDate financialDueDate = db.FinancialDueDates.Find(id);
            GetViewDropDownData();

            try
            {
                db.FinancialDueDates.Remove(financialDueDate);
                db.SaveChanges();
                TempData["Message"] = "Financial Due Date was successfully Deleted.";
                return RedirectToAction("Index");
            }
            catch(Exception exp)
            {
                log.Error(exp);
                TempData["Message"] = "Error Financial Due Date.";
                ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                return RedirectToAction("Index");
            }
        }
        private void ModelValidation(FinancialDueDate financialduedate)
        {
            if (financialduedate.EndFiscalYear >= financialduedate.Q1Due)
            {
                ModelState.AddModelError("AuditedDated", "The financial year end date must be before first quarter due date");
            }

            if (financialduedate.Q1Due > DateTime.Today)
            {
                ModelState.AddModelError("First Quarter", "The first quarter date can not be a future date");
            }

            if (financialduedate.Q1Due >= financialduedate.Q2Due)
            {
                ModelState.AddModelError("First Quarter", "The first quarter due date must be before second quarter due date");
            }
            if (financialduedate.Q2Due >= financialduedate.Q3Due)
            {
                ModelState.AddModelError("Second Quarter", "The second quarter due date must be before third quarter due date");
            }
            if (financialduedate.Q3Due >= financialduedate.Q4Due)
            {
                ModelState.AddModelError("Third Quarter", "The third quarter due date must be before fourth quarter due date");
            }
            if (financialduedate.AuditedDue < financialduedate.Q3Due)
            {
                ModelState.AddModelError("Audited Dated", "The audited financials due date must be after third quarter due date");
            }

            if (financialduedate.AnnualDue <= financialduedate.AuditedDue)
            {
                ModelState.AddModelError("Audited Dated", "The annual due must be after the audited due date");
            }

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
