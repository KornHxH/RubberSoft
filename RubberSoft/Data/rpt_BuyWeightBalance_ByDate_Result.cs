//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RubberSoft.Data
{
    using System;
    
    public partial class rpt_BuyWeightBalance_ByDate_Result
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public Nullable<System.DateTime> SaleDate { get; set; }
        public Nullable<decimal> SalePriceAdvance { get; set; }
        public Nullable<double> CP_WeightAmount { get; set; }
        public Nullable<decimal> BL_WeightBalanceAmt { get; set; }
        public Nullable<double> WeightAmount_Raw { get; set; }
        public Nullable<double> WeightBalanceAmt { get; set; }
        public string LogName { get; set; }
        public string BuyNumber { get; set; }
        public Nullable<System.DateTime> BuyDate { get; set; }
    }
}
