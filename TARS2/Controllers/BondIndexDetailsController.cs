using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using TARS2.DBModel;
using log4net;

namespace TARS2.Controllers
{

    [Authorize]
    public class BondIndexDetailsController : Controller
    {
        private readonly WebFeedRepositoryEntities db = new WebFeedRepositoryEntities();
        static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private void GetViewDropDownData()
        {
            System.Collections.IEnumerable BondIndex = db.usp_GetBondIndex().ToList().Select(s => new SelectListItem
            {
                Text = s.BondIndexName.ToString(),
                Value = s.BondIndexID.ToString()
            });

            ViewData["BondIndex"] = BondIndex;
        }
        // GET: BondIndexDetails
        public ActionResult Index()
        {
          
            GetViewDropDownData();
            var bondIndexDetails = db.BondIndexDetails.ToList();

            return View(bondIndexDetails);
        }

        [HttpPost]
        public ActionResult Index(string ValueStartDate,string ValueEndDate, string BondIndex)
        {
            GetViewDropDownData();



            if (string.IsNullOrEmpty(ValueStartDate) && string.IsNullOrEmpty(ValueEndDate))
            {
                var bondindexdetails = db.BondIndexDetails.Include(b => b.BondIndex);

                return View(bondindexdetails.ToList());
            }
            else
            {

                int? BondIndx = (BondIndex == null || BondIndex.Trim() == "") ? 0 : Convert.ToInt32(BondIndex);

                DateTime? ValStartDate = null;
                DateTime? ValEndDate = null;
                try
                {
                    ValStartDate = (ValueStartDate == null || ValueStartDate.Trim() == "") ? DateTime.MinValue : Convert.ToDateTime(ValueStartDate);
                    ValEndDate = (ValueEndDate == null || ValueEndDate.Trim() == "") ? DateTime.MinValue : Convert.ToDateTime(ValueEndDate);
                }
                catch (Exception exp)
                {
                    log.Error(exp);
                    ValStartDate = null;
                    ValEndDate = null;
                }
                var bondindexdetails = db.BondIndexDetails.Where(b => ((((b.BondIndexID == null) || (b.BondIndexID == BondIndx)) &&((b.ValueDate == null) || (b.ValueDate >= ValStartDate && b.ValueDate <= ValEndDate))))).ToList();
               
                if (bondindexdetails.Count == 0 || bondindexdetails == null)
                {
                    ViewData["NoRecords"] = "No Records found.";
                    //return View(model);
                }
                return View(bondindexdetails.ToList());
            }
            }

        // GET: BondIndexDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BondIndexDetail bondIndexDetail = db.BondIndexDetails.Find(id);
            if (bondIndexDetail == null)
            {
                return HttpNotFound();
            }
            return View(bondIndexDetail);
        }

        // GET: BondIndexDetails/Create
        public ActionResult Create()
        {
            GetViewDropDownData();
            return View();
        }

        // POST: BondIndexDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BondIndexDetailID,BondIndexID,ValueDate,Value,PreviousValue,ValueChange,PercentageChange,CreatedOn,CreatedBy")] BondIndexDetail bondIndexDetail)
        {
            GetViewDropDownData();
            if (ModelState.IsValid)
            {
                try
                {
                    db.BondIndexDetails.Add(bondIndexDetail);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {
                    log.Error(exp);

                }
             }
            return View(bondIndexDetail);
        }

        // GET: BondIndexDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            GetViewDropDownData();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BondIndexDetail bondIndexDetail = db.BondIndexDetails.Find(id);
            if (bondIndexDetail == null)
            {
                return HttpNotFound();
            }
            return View(bondIndexDetail);
        }

        // POST: BondIndexDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BondIndexDetailID,BondIndexID,ValueDate,Value,PreviousValue,ValueChange,PercentageChange,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] BondIndexDetail bondIndexDetail)
        {
            GetViewDropDownData();
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(bondIndexDetail).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                log.Error(ex);
            }
            return View(bondIndexDetail);
        }

        // GET: BondIndexDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BondIndexDetail bondIndexDetail = db.BondIndexDetails.Find(id);
            if (bondIndexDetail == null)
            {
                return HttpNotFound();
            }
            return View(bondIndexDetail);
        }

        // POST: BondIndexDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BondIndexDetail bondIndexDetail = db.BondIndexDetails.Find(id);
            db.BondIndexDetails.Remove(bondIndexDetail);
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
