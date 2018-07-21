using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.ViewModels
{
    public class InvoiceViewModel
    {
        public int ID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<bool> InOut { get; set; }
        public Nullable<decimal> Value { get; set; }
        public int Status { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public IEnumerable<InvoiceDetailViewModel> InvoiceDetails { get; set; }
    }

    public class InvoiceInput
    {
        public int id { get; set; }
        public List<InvoiceInputDetails> details { get; set; }
    }

    public class InvoiceInputDetails
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int ID { get; set; }
        public bool InOut { get; set; }
        public int InvoiceID { get; set; }
        public bool IsActive { get; set; }
        public int TaxValue { get; set; }
        public double TaxValueValue { get; set; }
        public decimal Value { get; set; }
    }
}