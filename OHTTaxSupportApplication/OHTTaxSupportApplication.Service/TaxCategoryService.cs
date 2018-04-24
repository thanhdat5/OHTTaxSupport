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
    public interface ITaxCategoryService
    {

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);

        ApiResponseViewModel Add(TaxCategory TaxCategory);

        ApiResponseViewModel Update(TaxCategory TaxCategory);

        ApiResponseViewModel Delete(int id);

        ApiResponseViewModel SetInActive(int id);

    }

    public class TaxCategoryService : ITaxCategoryService
    {
        private ITaxCategoryRepository _taxCategoryRepository;

        private IUnitOfWork _unitOfWork;

        public TaxCategoryService(ITaxCategoryRepository taxCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._taxCategoryRepository = taxCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all Tax Category
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<TaxCategoryViewModel>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _taxCategoryRepository.GetAll().Where(m => m.IsActive == true).Select(m => new TaxCategoryViewModel
                {
                    ID = m.ID,
                    Category = m.Category,
                    IsActive = m.IsActive ?? false
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
        /// Get all Tax Category with pagging
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
        {
            var result = new List<TaxCategoryViewModel>();
            var paginationSet = new PaginationSet<TaxCategoryViewModel>();
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
                var query = _taxCategoryRepository.GetAll().Where(m => m.IsActive == true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(m => new TaxCategoryViewModel
                    {
                        ID = m.ID,
                        Category = m.Category,
                        IsActive = m.IsActive ?? false
                    })
                    .ToList();

                paginationSet = new PaginationSet<TaxCategoryViewModel>()
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
        /// Get Tax Category by id
        /// </summary>
        /// <param name="id">ID of Tax Category</param>
        /// <returns></returns>
        public ApiResponseViewModel GetById(int id)
        {
            var result = new TaxCategoryViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _taxCategoryRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    var tempResult = _taxCategoryRepository.GetSingleById(id);
                    result.ID = tempResult.ID;
                    result.Category = tempResult.Category;
                    result.IsActive = tempResult.IsActive ?? false;
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
        /// Add new Tax Category
        /// </summary>
        /// <param name="obj">Tax Category</param>
        /// <returns></returns>
        public ApiResponseViewModel Add(TaxCategory obj)
        {
            var result = new TaxCategory();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _taxCategoryRepository.Add(obj);
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
        /// Update Tax Category
        /// </summary>
        /// <param name="obj">New Tax Category</param>
        /// <returns></returns>
        public ApiResponseViewModel Update(TaxCategory obj)
        {
            var result = new TaxCategoryViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _taxCategoryRepository.CheckContains(m => m.ID == obj.ID);
                if (exists)
                {
                    _taxCategoryRepository.Update(obj);
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
        /// Delete Tax Category by Id
        /// </summary>
        /// <param name="id">ID of Tax Category</param>
        /// <returns></returns>
        public ApiResponseViewModel Delete(int id)
        {
            var result = new TaxCategory();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {

                var exists = _taxCategoryRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _taxCategoryRepository.Delete(id);
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
        /// Make Tax Category in-active by Id
        /// </summary>
        /// <param name="id">ID of Tax Category</param>
        /// <returns></returns>
        public ApiResponseViewModel SetInActive(int id)
        {
            var result = new TaxCategory();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _taxCategoryRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _taxCategoryRepository.GetSingleById(id);
                    result.IsActive = false;
                    _taxCategoryRepository.Update(result);
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