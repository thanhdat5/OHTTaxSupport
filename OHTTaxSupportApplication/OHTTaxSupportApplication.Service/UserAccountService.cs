using System;
using System.Collections.Generic;
using System.Linq;
using OHTTaxSupportApplication.Common;
using OHTTaxSupportApplication.Data.Infrastructure;
using OHTTaxSupportApplication.Data.Repositories;
using OHTTaxSupportApplication.Model.Models;
using OHTTaxSupportApplication.Model.ViewModels;

namespace OHTTaxSupportApplication.Service
{
    public interface IUserAccountService
    {

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);

        ApiResponseViewModel Add(UserAccount userAccount);

        ApiResponseViewModel Update(UserAccount userAccount);

        ApiResponseViewModel Delete(int id);

        ApiResponseViewModel SetInActive(int id);

    }

    public class UserAccountService : IUserAccountService
    {
        private IUserAccountRepository _userAccountRepository;

        private IUnitOfWork _unitOfWork;

        public UserAccountService(IUserAccountRepository userAccountRepository, IUnitOfWork unitOfWork)
        {
            this._userAccountRepository = userAccountRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all Tax value
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<UserAccountViewModel>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _userAccountRepository.GetAll().Where(m => m.IsActive == true).Select(m => new UserAccountViewModel
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
                response.Result = result;
            }
            catch (Exception ex)
            {
                response.Code = CommonConstants.ApiResponseExceptionCode;
                response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Get all Tax value with pagging
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
        {
            var result = new List<UserAccountViewModel>();
            var paginationSet = new PaginationSet<UserAccountViewModel>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var currentPage = page ?? 1;
                int totalRow = 0;
                var query = _userAccountRepository.GetAll().Where(m => m.IsActive == true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
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
                        FullName = m.User.Fullname,
                    })
                    .ToList();

                paginationSet = new PaginationSet<UserAccountViewModel>()
                {
                    Items = result,
                    Page = currentPage,
                    TotalCount = result.Count(),
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                response.Result = paginationSet;
            }
            catch (Exception ex)
            {
                response.Code = CommonConstants.ApiResponseExceptionCode;
                response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Get Tax value by id
        /// </summary>
        /// <param name="id">ID of Tax value</param>
        /// <returns></returns>
        public ApiResponseViewModel GetById(int id)
        {
            var result = new UserAccountViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _userAccountRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    var tempResult = _userAccountRepository.GetSingleById(id);
                    result.ID = tempResult.ID;
                    result.UserID = tempResult.UserID;
                    result.AccountID = tempResult.AccountID;
                    result.IsActive = tempResult.IsActive ?? false;
                    result.AccountCode = tempResult.Account.AccountCode;
                    result.AccountSH = tempResult.Account.SH;
                    result.AccountTaxVaue = tempResult.Account.TaxValue.Value;
                    result.AccountTaxCategory = tempResult.Account.TaxCategory.Category;
                    result.UserName = tempResult.User.Username;
                    result.UserImage = tempResult.User.Username;
                    result.UserCompany = tempResult.User.Fullname;
                    result.FullName = tempResult.User.Fullname;
                    response.Result = result;
                }
                else
                {
                    response.Code = CommonConstants.ApiResponseNotFoundCode;
                    response.Message = CommonConstants.NotFoundMessage;
                }
            }
            catch (Exception ex)
            {
                response.Code = CommonConstants.ApiResponseExceptionCode;
                response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Add new Tax value
        /// </summary>
        /// <param name="obj">Tax value</param>
        /// <returns></returns>
        public ApiResponseViewModel Add(UserAccount obj)
        {
            var result = new UserAccount();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _userAccountRepository.Add(obj);
                _unitOfWork.Commit();
                response.Message = CommonConstants.AddSuccess;
                response.Result = result;
            }
            catch (Exception ex)
            {
                response.Code = CommonConstants.ApiResponseExceptionCode;
                response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
            }
            return response;

        }

        /// <summary>
        /// Update tax value
        /// </summary>
        /// <param name="obj">New Tax value</param>
        /// <returns></returns>
        public ApiResponseViewModel Update(UserAccount obj)
        {
            var result = new UserAccountViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _userAccountRepository.CheckContains(m => m.ID == obj.ID);
                if (exists)
                {
                    _userAccountRepository.Update(obj);
                    _unitOfWork.Commit();
                    response.Message = CommonConstants.SaveSuccess;
                }
                else
                {
                    response.Code = CommonConstants.ApiResponseNotFoundCode;
                    response.Message = CommonConstants.NotFoundMessage;
                }

            }
            catch (Exception ex)
            {
                response.Code = CommonConstants.ApiResponseExceptionCode;
                response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Delete Tax value by Id
        /// </summary>
        /// <param name="id">ID of Tax value</param>
        /// <returns></returns>
        public ApiResponseViewModel Delete(int id)
        {
            var result = new UserAccount();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {

                var exists = _userAccountRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _userAccountRepository.Delete(id);
                    _unitOfWork.Commit();
                    response.Message = CommonConstants.DeleteSuccess;
                    response.Result = result;
                }
                else
                {
                    response.Code = CommonConstants.ApiResponseNotFoundCode;
                    response.Message = CommonConstants.NotFoundMessage;
                }
            }
            catch (Exception ex)
            {
                response.Code = CommonConstants.ApiResponseExceptionCode;
                response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Make Tax value in-active by Id
        /// </summary>
        /// <param name="id">ID of Tax value</param>
        /// <returns></returns>
        public ApiResponseViewModel SetInActive(int id)
        {
            var result = new UserAccount();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _userAccountRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _userAccountRepository.GetSingleById(id);
                    result.IsActive = false;
                    _userAccountRepository.Update(result);
                    _unitOfWork.Commit();
                    response.Message = CommonConstants.DeleteSuccess;
                    response.Result = result;
                }
                else
                {
                    response.Code = CommonConstants.ApiResponseNotFoundCode;
                    response.Message = CommonConstants.NotFoundMessage;
                }
            }
            catch (Exception ex)
            {
                response.Code = CommonConstants.ApiResponseExceptionCode;
                response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
            }
            return response;
        }
    }
}