using System;

namespace OHTTaxSupportApplication.Model.ViewModels
{
    public class UserAccountViewModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string AccountCode { get; set; }
        public string AccountSH { get; set; }
        public double AccountTaxVaue { get; set; }
        public string AccountCategory { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string UserCompany { get; set; }
        public string FullName { get; set; }
    }
}