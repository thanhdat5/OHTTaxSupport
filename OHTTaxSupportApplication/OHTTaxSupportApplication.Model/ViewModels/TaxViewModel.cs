using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.ViewModels
{
    public class TaxViewModel
    {
        public int ID { get; set; }
        public Nullable<int> TaxTypeID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public decimal Value { get; set; }
        public int CustomerID { get; set; }
        public int TaxValueID { get; set; }
        public int DepartmentID { get; set; }
        public int TaxCategoryID { get; set; }
        public bool InOut { get; set; }
        public int Status { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Department { get; set; }
        public string TaxCategory { get; set; }
        public string TaxType { get; set; }
        public double TaxValue { get; set; }
        public IEnumerable<TaxDetailViewModel> TaxDetails { get; set; }
    }
}