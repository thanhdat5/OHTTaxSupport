using System;
using OHTTaxSupportApplication.Model.Models;

namespace OHTTaxSupportApplication.Model.ViewModels
{
    public class InvoiceDetailViewModel
    {
        public int ID { get; set; }
        public int InvoiceID { get; set; }
        public int ProductID { get; set; }
        public int UnitID { get; set; }
        public Nullable<decimal> Value { get; set; }
        public double Quanlity { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Product { get; set; }    
        public string Unit { get; set; }
        public bool InOut { get; set; }
        public int DepartmentID { get; set; }
        public string Department { get; set; }
        public int CategoryID { get; set; }
        public string Category { get; set; }
        public string TaxValue { get; set; }
    }
}