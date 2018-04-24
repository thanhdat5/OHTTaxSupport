namespace OHTTaxSupportApplication.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}