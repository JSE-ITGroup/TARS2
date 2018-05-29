using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using TARS2.DBModel;
using System.Collections.Generic;

namespace TARS2.DBModel
{
    public class CompanyEarningMetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Symbol")]
        public string InstrumentCode { get; set; }
        public int InstrumentID { get; set; }

        //[Range(typeof(int), "1969", "2015")]
        public int Year { get; set; }

       
        [DataType(DataType.Currency)]
        [Display(Name = "Annual Earnings")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public Nullable<decimal> Annual { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Quarter 1 Earnings")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public Nullable<decimal> Q1 { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Quarter 2 Earnings")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public Nullable<decimal> Q2 { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Quarter 3 Earnings")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public Nullable<decimal> Q3 { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Quarter 4 Earnings")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public Nullable<decimal> Q4 { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public int Currency { get; set; }

        
        [Display(Name = "Listed")]
        public Nullable<bool> Status { get; set; }


        [DataType(DataType.DateTime)]
       
        public Nullable<System.DateTime> CreatedOn { get; set; }
        
        [Display(Name = "Created By")]
       
        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> ModifiedOn {get;set;}

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        //public virtual ICollection<Stock> Stock { get; set; }

    }

    public class CorporateActionMetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required] 
        [Display(Name ="Symbol")]      
        public string InstrumentCode { get; set; }

        [Display(Name ="Record Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> RecordDate { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ExDate { get; set; }

        [Display(Name = "Action Code")]
        public string ActionCode { get; set; }

        [Display(Name = "Every")]
        public Nullable<decimal> SplitRatio { get; set; }
        public Nullable<decimal> Issue { get; set; }
        public Nullable<decimal> Share { get; set; }
        public string Status { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public Nullable<System.DateTime> CreatedOn { get; set; }

        [Display(Name = "Created By")]
        [Required]
        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }
    }

    public class DividendMetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Display(Name = "Instrument Code")]
        public string InstrumentCode { get; set; }
        
        [Display(Name = "Cash Dividend")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C4}")]
        public Nullable<decimal> CashDividend { get; set; }

        [Display(Name = "Record Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> RecordDate { get; set; }

        [Display(Name = "X Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> XDate { get; set; }

        [Display(Name = "Pay Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> PayDate { get; set; }

        public string Status { get; set; }

        [Display(Name = "Share Type")]
        public string ShareType { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public int Currency { get; set; }

        public virtual Instrument Instrument { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created ON")]
        [Required]
        public Nullable<System.DateTime> CreatedOn { get; set; }

        [Display(Name = "Created By")]
        [Required]
        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }
        // public virtual Currency Currency1 { get; set; }
    }
    public partial class InstrumentMetaData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InstrumentMetaData()
        {
            this.CompanyEarnings = new HashSet<CompanyEarning>();
            this.CorporateActions = new HashSet<CorporateAction>();
            this.Currencies = new HashSet<Currency>();
            this.Dividends = new HashSet<Dividend>();
            this.FinancialDueDates = new HashSet<FinancialDueDate>();
        }

        public int IdInstrument { get; set; }
        public string ISIN { get; set; }
        public Nullable<int> IdInstrumentType { get; set; }
        public Nullable<int> IdSector { get; set; }
        public Nullable<int> IdMarketSegment { get; set; }
        public Nullable<int> InstrumentTypeId { get; set; }
        public Nullable<decimal> DaysClose { get; set; }
        public Nullable<int> SectorId { get; set; }
        public Nullable<int> MarketId { get; set; }
        public string WebAddress { get; set; }

        [Display(Name = "Symbol")]
        [Required]
        public string InstrumentCode { get; set; }
        public string InstrumentName { get; set; }
        public string InstrumentShortName { get; set; }
        public Nullable<decimal> InterestRate { get; set; }
        public Nullable<System.DateTime> MaturityDate { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string Status { get; set; }
        public string Suspended { get; set; }
        public string Tradeable { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> DateDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyEarning> CompanyEarnings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CorporateAction> CorporateActions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Currency> Currencies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dividend> Dividends { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinancialDueDate> FinancialDueDates { get; set; }
    }
    public class FinancialDueDateMetaData
    {
         [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Nullable<int> InstrumentID { get; set; }
        [Display(Name = "Stock")]
        public Nullable<int> StockTypeID { get; set; }

        [Display(Name = "Website")]
        [DataType(DataType.Url)]
        public string WebAddress { get; set; }

        [Display(Name = "Country")]
        public string CountryCode { get; set; }

        [Display(Name = "End Of Financial Year")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> EndFiscalYear { get; set; }
        public Nullable<int> Year { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("First Quarter")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Q1Due { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Second Quarter")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Q2Due { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Third Quarter")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Q3Due { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Fourth Quarter")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Q4Due { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Aduit Due Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> AuditedDue { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Annual Due Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> AnnualDue { get; set; }

        [Required]
        [DisplayName("Listed")]
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }

        [Display(Name = "Created By")]
        [Required]
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Instrument Code")]
        [Required]
        public string InstrumentCode { get; set; }

        public virtual Instrument Instrument { get; set; }
       
    }

    public partial class usp_GetSymbols_ResultMetaData
    {
        public int InstrumentID { get; set; }
        public string InstrumentCode { get; set; }
        public string InstrumentName { get; set; }
    }
    public partial class usp_GetDividend_ResultMetaData
    {
        public string InstrumentCode { get; set; }
        public decimal CashDividend { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime XDate { get; set; }
        public DateTime PayDate { get; set; }
        public int InstrumentID { get; set; }
        public string Status { get; set; }
        public string ShareType { get; set; }
        public string Currency { get; set; }
    }

    public  class BondIndexMetaData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BondIndexMetaData()
        {
            this.BondIndexDetails = new HashSet<BondIndexDetail>();
        }

        public int BondIndexID { get; set; }

        [Required]
        [DisplayName("Bond Index")]
        public string BondIndexName { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        //[DataType(DataType.DateTime)]
        //[Required]
        //public Nullable<System.DateTime> CreatedOn { get; set; }

        [Display(Name = "Created By")]
        [Required]
        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        public virtual ICollection<BondIndexDetail> BondIndexDetails { get; set; }
    }

    public partial class BondIndexDetailMetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BondIndexDetailID { get; set; }

        
        public int BondIndexID { get; set; }

        [Required]
        [DisplayName("Value Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ValueDate { get; set; }

        [Required]
        [DisplayName("CreatedOn")]
        [DataType(DataType.DateTime)]
       
        public Nullable<System.DateTime> CreatedOn { get; set; }

        [Display(Name = "Created By")]
        [Required]
        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }
        public Nullable<decimal> Value { get; set; }

        [Required]
        [DisplayName("Previous Value")]
        public Nullable<decimal> PreviousValue { get; set; }

        [Required]
        [DisplayName("Value Change")]
        public Nullable<decimal> ValueChange { get; set; }

        [Required]
        [DisplayName("Percentage Change")]
        public Nullable<decimal> PercentageChange { get; set; }

        public virtual BondIndex BondIndex { get; set; }
    }
}