using System;
using System.Collections.Generic;
using OHTTaxSupportApplication.Model.Models;

namespace OHTTaxSupportApplication.Model.ViewModels
{
    public class InvoiceDetailViewModel
    {
        public int ID { get; set; }
        public int InvoiceID { get; set; }
		public string InvoiceCode { get; set; }
		public string Value { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int DepartmentID { get; set; }
        public string Department { get; set; }
        public int CategoryID { get; set; }
        public string Category { get; set; }
        public int TaxValueID { get; set; }
        public string TaxValue { get; set; }
        public int CustomerID { get; set; }
        public string Customer { get; set; }
        public string CreateDate { get; set; }
        public bool InOut { get; set; }
        public int Status { get; set; }
        public IEnumerable<InvoiceDetailViewModel> InvoiceDetails { get; set; }
    }
}