using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TARS2.DBModel;

namespace TARS2.Controllers
{
    [Authorize]
    public class CompanyEarningsController : Controller
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
            System.Collections.IEnumerable Currencies = db.usp_GetCurrencies().ToList().Select(s => new SelectListItem
            {
                Text = s.Currency.ToString(),
                Value = s.CurrencyID.ToString()
            });
            ViewData["Symbols"] = Symbols;
            ViewData["Currencies"] = Currencies;
        }

        // GET: CompanyEarnings
        public ActionResult Index()
        {
            GetViewDropDownData();
            var companyEarnings = db.CompanyEarnings.Include(c => c.Instrument);
            return View(companyEarnings.OrderByDescending(x => x.Year).ToList());
        }

        [HttpPost]
        public ActionResult Index(string Symbols)
        {
            GetViewDropDownData();
            var companyEarnings = db.CompanyEarnings.Include(c => c.Instrument);
            if (!String.IsNullOrEmpty(Symbols))
            {
                companyEarnings = companyEarnings.Where(d => d.InstrumentCode == (Symbols)
                                       || d.Instrument.InstrumentName == Symbols);
            }
            return View(companyEarnings.OrderByDescending(x => x.Year).ToList());
        }
        // GET: CompanyEarnings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyEarning companyEarning = db.CompanyEarnings.Find(id);
            if (companyEarning == null)
            {
                return HttpNotFound();
            }
            return View(companyEarning);
        }

        // GET: CompanyEarnings/Create
        public ActionResult Create()
        {
            GetViewDropDownData();
            //ViewBag.InstrumentCode = new SelectList(db.Instruments, "InstrumentCode", "ISIN");
            return View();
        }

        // POST: CompanyEarnings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,InstrumentID,Year,Annual,Q1,Q2,Q3,Q4,Currency,InstrumentCode,CreatedOn,CreatedBy")] CompanyEarning companyEarning)
        {
            GetViewDropDownData();

            var companyEarnings = db.usp_GetCompanyEarnings().Where(ce => ce.InstrumentCode == companyEarning.InstrumentCode && ce.Year == companyEarning.Year).ToList();

            if (companyEarnings.Count() > 0)
            {
                ModelState.AddModelError("Company Earnings", "Company Earnings Information has already been updated for the selected Symbol.");
            }
            else
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        db.CompanyEarnings.Add(companyEarning);
                        db.SaveChanges();
                        TempData["Message"] = "Company Earning Successfully Added";
                        return RedirectToAction("Index", new { Symbols = companyEarning.InstrumentCode });
                    }
                    catch (Exception exp)
                    {
                        ViewData["Message"] = "Error Adding Company Earnings.";
                        ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                        log.Error(exp);
                        return View(companyEarning);
                    }
                }
            }

            //ViewBag.InstrumentCode = new SelectList(db.Instruments, "InstrumentCode", "ISIN", companyEarning.InstrumentCode);
            return View(companyEarning);
        }

        // GET: CompanyEarnings/Edit/5
        public ActionResult Edit(int? id)
        {
            GetViewDropDownData();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyEarning companyEarning = db.CompanyEarnings.Find(id);
            if (companyEarning == null)
            {
                return HttpNotFound();
            }
           // ViewBag.InstrumentCode = new SelectList(db.Instruments, "InstrumentCode", "ISIN", companyEarning.InstrumentCode);
            return View(companyEarning);
        }

        // POST: CompanyEarnings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InstrumentID,Year,Annual,Q1,Q2,Q3,Q4,Currency,InstrumentCode,ModifiedOn,ModifiedBy")]  CompanyEarning companyEarning)
        {
            GetViewDropDownData();
           // if(companyEarning.Currency1.CurrencyID)
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(companyEarning).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Company Earning was Successfully Edited.";
                    return RedirectToAction("Index", new { Symbols = companyEarning.InstrumentCode});
                }
                catch(Exception exp)
                {
                    ViewData["Message"] = "Error Editing Company Earnings.";
                    ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                    return View(companyEarning);
                }
            }
            //ViewBag.InstrumentCode = new SelectList(db.Instruments, "InstrumentCode", "ISIN", companyEarning.InstrumentCode);
            return View(companyEarning);
        }

        // GET: CompanyEarnings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyEarning companyEarning = db.CompanyEarnings.Find(id);
            if (companyEarning == null)
            {
                return HttpNotFound();
            }
            return View(companyEarning);
        }

        // POST: CompanyEarnings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                CompanyEarning companyEarning = db.CompanyEarnings.Find(id);
                db.CompanyEarnings.Remove(companyEarning);
                db.SaveChanges();
                TempData["Message"] = "Company Earning was successfully deleted.";
                return RedirectToAction("Index");
            }
            catch(Exception exp)
            {
                TempData["Message"] = "Error Editing Company Earnings.";
                ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                return RedirectToAction("Index");
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
