using System.Collections.Generic;
using System.Linq;
using OHTTaxSupportApplication.Data.Infrastructure;
using OHTTaxSupportApplication.Model.Models;
using OHTTaxSupportApplication.Model.ViewModels;

namespace OHTTaxSupportApplication.Data.Repositories
{
    public interface IUserAccountRepository : IRepository<UserAccount>
    {
        IEnumerable<UserAccountViewModel> GetListUserAccountByUserID(int userId);
    }

    public class UserAccountRepository : RepositoryBase<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<UserAccountViewModel> GetListUserAccountByUserID(int userId)
        {
            return DbContext.UserAccounts
                .Where(m => m.UserID == userId && m.IsActive == true)
                .Select(m => new UserAccountViewModel
                {
                    ID = m.ID,
                    UserID = m.UserID,
                    AccountID = m.AccountID,
                    IsActive = m.IsActive ?? false,
                    AccountCode = m.Account.AccountCode,
                    AccountSH = m.Account.SH,
                    AccountTaxVaue = m.Account.TaxValue.Value,
                    AccountTaxCategory = m.Account.TaxCategory.Category,
                    UserName = m.User.Username,
                    UserImage = m.User.Username,
                    UserCompany = m.User.Fullname,
                    FullName = m.User.Fullname
                }).ToList();
        }
    }
}