using System;

namespace OHTTaxSupportApplication.Model.ViewModels
{
    public class AccountViewModel
    {
        public int ID { get; set; }
        public string AccountCode { get; set; }
        public int TaxCategoryID { get; set; }
        public int TaxValueID { get; set; }
        public string SH { get; set; }
        public Nullable<bool> IsActive { get; set; }  
        public string TaxCategory { get; set; }    
        public double TaxValue { get; set; }     
    }
}