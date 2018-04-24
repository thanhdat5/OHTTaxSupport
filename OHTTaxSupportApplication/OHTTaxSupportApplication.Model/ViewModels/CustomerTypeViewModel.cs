using System;

namespace OHTTaxSupportApplication.Model.ViewModels
{
    public class CustomerTypeViewModel
    {
        public int ID { get; set; }
        public string CustomerTypeName { get; set; }  
        public Nullable<bool> IsActive { get; set; }  
    }
}