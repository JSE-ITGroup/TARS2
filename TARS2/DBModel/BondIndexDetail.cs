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
    
    public partial class BondIndexDetail
    {
        public int BondIndexDetailID { get; set; }
        public int BondIndexID { get; set; }
        public System.DateTime ValueDate { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<decimal> PreviousValue { get; set; }
        public Nullable<decimal> ValueChange { get; set; }
        public Nullable<decimal> PercentageChange { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string Status { get; set; }
    
        public virtual BondIndex BondIndex { get; set; }
    }
}
