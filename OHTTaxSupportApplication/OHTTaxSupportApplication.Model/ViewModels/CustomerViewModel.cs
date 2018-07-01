using System;

namespace OHTTaxSupportApplication.Model.ViewModels
{
    public class CustomerViewModel
    {
        public int ID { get; set; }
        public int CustomerTypeID { get; set; }
        public string CustomerName { get; set; }
        public int CompanyID { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Company { get; set; }
        public string CustomerType { get; set; }
}
}