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
    
    public partial class usp_GetDividend_Result
    {
        public string InstrumentCode { get; set; }
        public Nullable<decimal> CashDividend { get; set; }
        public Nullable<System.DateTime> RecordDate { get; set; }
        public Nullable<System.DateTime> XDate { get; set; }
        public Nullable<System.DateTime> PayDate { get; set; }
        public string InstrumentName { get; set; }
        public string Status { get; set; }
        public string ShareType { get; set; }
        public string Currency { get; set; }
    }
}