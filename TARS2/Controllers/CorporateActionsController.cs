using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TARS.DBModels;
using TARS2.DBModel;

namespace TARS2.Controllers
{
    [Authorize]
    public class CorporateActionsController : Controller
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


            System.Collections.IEnumerable CAT = db.usp_GetCorporateActionsTypes().ToList().Select(s => new SelectListItem
            {
                Text = s.Description.ToString(),
                Value = s.ActionCode.ToString()
            });

            /*System.Collections.IEnumerable Country = db.usp_GetCountries().ToList().Select(s => new SelectListItem
            {
                Text = s.Country,
                Value = s.CountryCode
            });*/


            ViewData["Symbols"] = Symbols;
            ViewData["CorporateActionTypes"] = CAT;
            //ViewData["Country"] = Country;
        }
        // GET: /CorporateActions/

        public ActionResult Index()
        {
            GetViewDropDownData();
            return View(db.CorporateActions.OrderByDescending(x => x.RecordDate).ToList());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(string Symbols)
        {
            GetViewDropDownData();
            var ca = from c in db.CorporateActions select c;
            if (!String.IsNullOrEmpty(Symbols))
            {
                ca = ca.Where(c => c.InstrumentCode == (Symbols)
                                       || c.Instrument.InstrumentName == (Symbols)).OrderByDescending(c => c.RecordDate);
            }
            return View(ca.OrderByDescending(x => x.RecordDate).ToList());
        }

        //
        // GET: /CorporateActions/Details/5

        public ActionResult Details(int id = 0)
        {
            CorporateAction corporateaction = db.CorporateActions.Find(id);
            if (corporateaction == null)
            {
                return HttpNotFound();
            }
            return View(corporateaction);
        }

        //
        // GET: /CorporateActions/Create
        public ActionResult Create()
        {
            GetViewDropDownData();
            return View();
        }

        //
        // POST: /CorporateActions/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CorporateAction corporateaction)
        {
            GetViewDropDownData();

            var CorpAct = db.CorporateActions.Where(f => f.ActionCode == corporateaction.ActionCode && f.RecordDate == corporateaction.RecordDate).SingleOrDefault();


            if (CorpAct != null)
            {
                ModelState.AddModelError("Code", "Action Code  already exist for the selected Symbol on that date.");
            }
            else
            {
                //ModelValidation(financialduedate);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.CorporateActions.Add(corporateaction);
                    db.SaveChanges();
                    TempData["Message"] = "Corporate Action Added Successfully.";
                    return RedirectToAction("Index", new { Symbols = corporateaction.InstrumentCode});

                }
                catch (Exception exp)
                {
                    log.Error(exp);
                    ViewData["Message"] = "Error Adding Corporate Action code.";
                    ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                    return View(corporateaction);
                }
            }
            return View(corporateaction);
        }

        //
        // GET: /CorporateActions/Edit/5
        public ActionResult Edit(int id = 0)
        {
            CorporateAction corporateaction = db.CorporateActions.Find(id);
            GetViewDropDownData();
            if (corporateaction == null)
            {
                return HttpNotFound();
            }
            return View(corporateaction);
        }

        //
        // POST: /CorporateActions/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CorporateAction corporateaction)
        {
            GetViewDropDownData();
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(corporateaction).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Corporate Action was Successfully Edited.";
                    return RedirectToAction("Index", new { Symbols = corporateaction.InstrumentCode });
                }
                catch (Exception exp)
                {
                    ViewData["Message"] = "Error Editing Corporate Actions";
                    ModelState.AddModelError("Error", "An error has occured please contact Administrator");
                    return View(corporateaction);
                }
            }
            return View(corporateaction);
        }

        //
        // GET: /CorporateActions/Delete/5

        public ActionResult Delete(int id = 0)
        {

            GetViewDropDownData();
            CorporateAction corporateaction = db.CorporateActions.Find(id);
            if (corporateaction == null)
            {
                return HttpNotFound();
            }
            return View(corporateaction);
        }

        //
        // POST: /CorporateActions/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            CorporateAction corporateaction = db.CorporateActions.Find(id);
            try   
            {
                db.CorporateActions.Remove(corporateaction);
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