using System;

namespace OHTTaxSupportApplication.Model.ViewModels
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int UnitID { get; set; }
        public Nullable<int> UnitID2 { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string UnitName { get; set; }
        public string Unit2Name { get; set; }
    }
}