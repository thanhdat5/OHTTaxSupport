using System;

namespace OHTTaxSupportApplication.Model.ViewModels
{
    public class DepartmentViewModel
    {
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public int CompanyID { get; set; }
        public string Address { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Company { get; set; }      
    }
}