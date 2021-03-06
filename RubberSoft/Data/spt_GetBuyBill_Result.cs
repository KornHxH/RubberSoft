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
    
    public partial class spt_GetBuyBill_Result
    {
        public int BuyId { get; set; }
        public string BuyNumber { get; set; }
        public Nullable<System.DateTime> BuyDate { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string LicensePlate { get; set; }
        public string Phone { get; set; }
        public Nullable<decimal> BeginBalance { get; set; }
        public Nullable<decimal> ValueBalance { get; set; }
        public Nullable<decimal> SubTotal { get; set; }
        public Nullable<decimal> DownValue { get; set; }
        public Nullable<decimal> SetOffValue { get; set; }
        public Nullable<decimal> NetTotal { get; set; }
        public Nullable<int> ProductTypeId { get; set; }
        public Nullable<bool> IsFinalze { get; set; }
        public Nullable<bool> Active { get; set; }
        public int BuyProductId { get; set; }
        public Nullable<int> PriceId { get; set; }
        public Nullable<double> Percentage { get; set; }
        public Nullable<double> WeightAmount { get; set; }
        public Nullable<double> WeightAmount_Plate { get; set; }
        public Nullable<double> Drc { get; set; }
        public Nullable<decimal> TotalPrice_Smoke { get; set; }
        public Nullable<double> WeightAmount_Raw { get; set; }
        public Nullable<decimal> TotalPrice_Raw { get; set; }
        public Nullable<decimal> CalRubber { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public string Remark { get; set; }
    }
}
