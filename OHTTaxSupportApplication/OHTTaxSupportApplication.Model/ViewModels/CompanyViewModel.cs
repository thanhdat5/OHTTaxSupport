using System;

namespace OHTTaxSupportApplication.Model.ViewModels
{
    public class CompanyViewModel
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }   
    }
}