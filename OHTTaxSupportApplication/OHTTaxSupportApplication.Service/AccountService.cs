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
    public interface IAccountService
    {

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);

        ApiResponseViewModel Add(Account account);

        ApiResponseViewModel Update(Account account);

        ApiResponseViewModel Delete(int id);

        ApiResponseViewModel SetInActive(int id);

    }

    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;

        private IUnitOfWork _unitOfWork;

        public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            this._accountRepository = accountRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all account
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<AccountViewModel>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _accountRepository.GetAll().Where(m => m.IsActive == true).Select(m => new AccountViewModel
                {
                    ID = m.ID,
                    AccountCode = m.AccountCode,
                    CategoryID = m.CategoryID,
                    TaxValueID = m.TaxValueID,
                    SH = m.SH,
                    IsActive = m.IsActive ?? false,
                    Category = m.Category.CategoryName,
                    TaxValue = m.TaxValue.Value
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
        /// Get all account with pagging
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
        {
            var result = new List<AccountViewModel>();
            var paginationSet = new PaginationSet<AccountViewModel>();
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
                var query = _accountRepository.GetAll().Where(m => m.IsActive == true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(m => new AccountViewModel
                    {
                        ID = m.ID,
                        AccountCode = m.AccountCode,
                        CategoryID = m.CategoryID,
                        TaxValueID = m.TaxValueID,
                        SH = m.SH,
                        IsActive = m.IsActive ?? false,
                        Category = m.Category.CategoryName,
                        TaxValue = m.TaxValue.Value
                    })
                    .ToList();

                paginationSet = new PaginationSet<AccountViewModel>()
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
        /// Get account by id
        /// </summary>
        /// <param name="id">ID of account</param>
        /// <returns></returns>
        public ApiResponseViewModel GetById(int id)
        {
            var result = new AccountViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _accountRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    var tempResult = _accountRepository.GetSingleById(id);
                    result.ID = tempResult.ID;
                    result.AccountCode = tempResult.AccountCode;
                    result.CategoryID = tempResult.CategoryID;
                    result.TaxValueID = tempResult.TaxValueID;
                    result.SH = tempResult.SH;
                    result.IsActive = tempResult.IsActive ?? false;
                    result.Category = tempResult.Category.CategoryName;
                    result.TaxValue = tempResult.TaxValue.Value;
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
        /// Add new account
        /// </summary>
        /// <param name="obj">account</param>
        /// <returns></returns>
        public ApiResponseViewModel Add(Account obj)
        {
            var result = new Account();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _accountRepository.Add(obj);
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
        /// Update account
        /// </summary>
        /// <param name="obj">New account</param>
        /// <returns></returns>
        public ApiResponseViewModel Update(Account obj)
        {
            var result = new AccountViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _accountRepository.CheckContains(m => m.ID == obj.ID);
                if (exists)
                {
                    _accountRepository.Update(obj);
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
        /// Delete account by Id
        /// </summary>
        /// <param name="id">ID of account</param>
        /// <returns></returns>
        public ApiResponseViewModel Delete(int id)
        {
            var result = new Account();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {

                var exists = _accountRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _accountRepository.Delete(id);
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
        /// Make account in-active by Id
        /// </summary>
        /// <param name="id">ID of account</param>
        /// <returns></returns>
        public ApiResponseViewModel SetInActive(int id)
        {
            var result = new Account();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _accountRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _accountRepository.GetSingleById(id);
                    result.IsActive = false;
                    _accountRepository.Update(result);
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