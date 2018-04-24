using System;
using OHTTaxSupportApplication.Model.Models;

namespace OHTTaxSupportApplication.Model.ViewModels
{
    public class TaxDetailViewModel
    {
        public int ID { get; set; }
        public int TaxID { get; set; }
        public int ProductID { get; set; }
        public int UnitID { get; set; }
        public Nullable<decimal> Value { get; set; }
        public double Quanlity { get; set; }
        public string Note { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Product { get; set; }    
        public string Unit { get; set; }
    }
}