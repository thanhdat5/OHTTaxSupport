using OHTTaxSupportApplication.Model.Models;

namespace OHTTaxSupportApplication.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private OHTTaxSupport_dbContext dbContext;

        public OHTTaxSupport_dbContext Init()
        {
            return dbContext ?? (dbContext = new OHTTaxSupport_dbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}