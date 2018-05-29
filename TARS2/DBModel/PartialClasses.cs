using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TARS.DBModels;

namespace TARS2.DBModel
{
    [MetadataType(typeof(InstrumentMetaData))]
    public partial class Instrument
    {

    }
    [MetadataType(typeof(CompanyEarningMetaData))]
    public partial class CompanyEarning
    {

    }
    [MetadataType(typeof(FinancialDueDateMetaData))]
    public partial class FinancialDueDate
    {

    }

    [MetadataType(typeof(usp_GetSymbols_ResultMetaData))]
    public partial class usp_GetSymbols_Result
    {

    }

    [MetadataType(typeof(CorporateActionMetaData))]
    public partial class CorporateAction
    {

    }

    [MetadataType(typeof(DividendMetaData))]
    public partial class Dividend
    {

    }

    [MetadataType(typeof(BondIndexDetailMetaData))]
    public partial class BondIndexDetail
    {

    }

    [MetadataType(typeof(BondIndexMetaData))]
    public partial class BondIndex
    {

    }
}