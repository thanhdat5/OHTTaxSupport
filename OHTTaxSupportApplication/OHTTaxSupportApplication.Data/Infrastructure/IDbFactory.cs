using System;
using OHTTaxSupportApplication.Model.Models;

namespace OHTTaxSupportApplication.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        OHTTaxSupport_dbContext Init();
    }
}