//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TARS2.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class FinancialDueDate
    {
        public int ID { get; set; }
        public Nullable<int> InstrumentID { get; set; }
        public Nullable<int> StockTypeID { get; set; }
        public string WebAddress { get; set; }
        public string CountryCode { get; set; }
        public Nullable<System.DateTime> EndFiscalYear { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<System.DateTime> Q1Due { get; set; }
        public Nullable<System.DateTime> Q2Due { get; set; }
        public Nullable<System.DateTime> Q3Due { get; set; }
        public Nullable<System.DateTime> Q4Due { get; set; }
        public Nullable<System.DateTime> AuditedDue { get; set; }
        public Nullable<System.DateTime> AnnualDue { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string InstrumentCode { get; set; }
    
        public virtual Instrument Instrument { get; set; }
    }
}
