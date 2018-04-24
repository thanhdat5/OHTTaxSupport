using System;

namespace OHTTaxSupportApplication.Model.Abstract
{
    public interface IAuditable
    {
        int ID { set; get; }  
        bool IsActive { set; get; }
    }
}