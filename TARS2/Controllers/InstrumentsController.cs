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
    public class InstrumentsController : Controller
    {
        private WebFeedRepositoryEntities db = new WebFeedRepositoryEntities();

        // GET: Instruments
        public ActionResult Index()
        {
            return View(db.Instruments.ToList());
        }

        // GET: Instruments/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrument instrument = db.Instruments.Find(id);
            if (instrument == null)
            {
                return HttpNotFound();
            }
            return View(instrument);
        }

        // GET: Instruments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instruments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdInstrument,ISIN,IdInstrumentType,IdSector,IdMarketSegment,InstrumentTypeId,DaysClose,SectorId,MarketId,WebAddress,InstrumentCode,InstrumentName,InterestRate,MaturityDate,ModifiedOn,Status,Suspended,Tradeable,CurrencyCode,CreatedOn,DateDeleted,InstrumentShortName")] Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                db.Instruments.Add(instrument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(instrument);
        }

        // GET: Instruments/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrument instrument = db.Instruments.Find(id);
            if (instrument == null)
            {
                return HttpNotFound();
            }
            return View(instrument);
        }

        // POST: Instruments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdInstrument,ISIN,IdInstrumentType,IdSector,IdMarketSegment,InstrumentTypeId,DaysClose,SectorId,MarketId,WebAddress,InstrumentCode,InstrumentName,InterestRate,MaturityDate,ModifiedOn,Status,Suspended,Tradeable,CurrencyCode,CreatedOn,DateDeleted,InstrumentShortName")] Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instrument).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(instrument);
        }

        // GET: Instruments/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrument instrument = db.Instruments.Find(id);
            if (instrument == null)
            {
                return HttpNotFound();
            }
            return View(instrument);
        }

        // POST: Instruments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Instrument instrument = db.Instruments.Find(id);
            db.Instruments.Remove(instrument);
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
