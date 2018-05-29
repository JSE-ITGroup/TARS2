using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TARS2.DBModel;

namespace TARS2.Controllers
{
    [Authorize]
    public class DividendController : Controller
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
                Value = s.Currency.ToString()
            });

            System.Collections.IEnumerable ST = db.usp_GetCorporateActionsTypes().ToList().Select(s => new SelectListItem
            {
                Text = s.Description.ToString(),
                Value = s.ActionCode.ToString()
            });

            ViewData["Symbols"] = Symbols;
            ViewData["ShareTypes"] = ST;
            ViewData["Currencies"] = Currencies;
        }

        //
        // GET: /Dividend/

        public ActionResult Index()
        {
            GetViewDropDownData();
            return View(db.Dividends.OrderByDescending(x => x.RecordDate).ToList());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(string Symbols)
        {
            GetViewDropDownData();
            var div = from d in db.Dividends select d;
            if (!String.IsNullOrEmpty(Symbols))
            {
                div = div.Where(d => d.InstrumentCode == (Symbols)
                                       || d.Instrument.InstrumentName == (Symbols));
            }
            return View(div.OrderByDescending(x => x.RecordDate).ToList());
        }


        //
        // GET: /Dividend/Details/5

        public ActionResult Details(long id = 0)
        {
            Dividend dividend = db.Dividends.Find(id);
            if (dividend == null)
            {
                return HttpNotFound();
            }
            return View(dividend);
        }



        //
        // GET: /Dividend/Create

        public ActionResult Create()
        {
            GetViewDropDownData();
            return View();
        }

        //
        // POST: /Dividend/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dividend dividend)
        {
            GetViewDropDownData();

            var div = db.Dividends.Where(f => f.PayDate == dividend.PayDate && f.InstrumentCode == dividend.InstrumentCode && f.ShareType == dividend.ShareType && f.CashDividend == dividend.CashDividend).SingleOrDefault();


            if (div != null)
            {
                ModelState.AddModelError("Code", "Dividend  already exist for the selected Symbol on that date.");
            }
            else
            {
                //ModelValidation(financialduedate);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Dividends.Add(dividend);
                    db.SaveChanges();
                    TempData["Message"] = "Dividend Added Successfully.";
                    return RedirectToAction("Index", new { Symbols = dividend.InstrumentCode });

                }
                catch (Exception exp)
                {
                    log.Error(exp);
                    ViewData["Message"] = "Error Adding Corporate Action code.";
                    ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                    return View(dividend);
                }
            }
            return View(dividend);
        }

        //
        // GET: /Dividend/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Dividend dividend = db.Dividends.Find(id);
            GetViewDropDownData();
            if (dividend == null)
            {
                return HttpNotFound();
            }
            return View(dividend);
        }

        //
        // POST: /Dividend/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dividend dividend)
        {
            GetViewDropDownData();
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(dividend).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Dividend record was Successfully Edited.";
                    return RedirectToAction("Index", new { Symbols = dividend.InstrumentCode });
                }
                catch (Exception exp)
                {
                    ViewData["Message"] = "Error Editing Financial Due Date.";
                    ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                    return View(dividend);
                }
            }
            return View(dividend);
        }

        //
        // GET: /Dividend/Delete/5

        public ActionResult Delete(long id = 0)
        {
            GetViewDropDownData();
            Dividend dividend = db.Dividends.Find(id);
            if (dividend == null)
            {
                return HttpNotFound();
            }
            return View(dividend);
        }

        //
        // POST: /Dividend/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Dividend dividend = db.Dividends.Find(id);
            try
            {
                db.Dividends.Remove(dividend);
                db.SaveChanges();
                TempData["Message"] = "Corporate Action was successfully Deleted.";
                return RedirectToAction("Index");
            }
            catch (Exception exp)
            {
                TempData["Message"] = "Error Deleting Corporate Action.";
                ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}