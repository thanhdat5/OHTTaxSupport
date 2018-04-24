using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.ViewModels
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public int CompanyID { get; set; }
        public string Image { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Company { get; set; }
        public IEnumerable<UserAccountViewModel> UserAccounts { get; set; }
    }
}