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
    public interface ITaxService
    {

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);

        ApiResponseViewModel Add(Tax tax);

        ApiResponseViewModel Update(Tax tax);

        ApiResponseViewModel Delete(int id);

        ApiResponseViewModel SetInActive(int id);

    }

    public class TaxService : ITaxService
    {
        private ITaxRepository _taxRepository;
        private ITaxDetailRepository _taxDetailRepository;

        private IUnitOfWork _unitOfWork;

        public TaxService(ITaxRepository taxRepository, ITaxDetailRepository taxDetailRepository, IUnitOfWork unitOfWork)
        {
            this._taxRepository = taxRepository;
            this._taxDetailRepository = taxDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all Tax
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<TaxViewModel>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _taxRepository.GetAll().Where(m => m.IsActive == true).Select(m => new TaxViewModel
                {
                    ID = m.ID,
                    TaxTypeID = m.TaxTypeID,
                    CreatedDate = m.CreatedDate,
                    Value = m.TaxValueID,
                    CustomerID = m.CustomerID,
                    TaxValueID = m.TaxValueID,
                    DepartmentID = m.DepartmentID,
                    TaxCategoryID = m.TaxCategoryID,
                    InOut = m.InOut,
                    Status = m.Status,
                    IsActive = m.IsActive ?? false,
                    Department = m.Department.DepartmentName,
                    TaxCategory = m.TaxCategory.Category,
                    TaxType = m.TaxType.TaxTypeName,
                    TaxValue = m.TaxValue.Value,
                    TaxDetails = _taxDetailRepository.GetListTaxDetailsByTaxID(m.ID)
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
        /// Get all Tax with pagging
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
        {
            var result = new List<TaxViewModel>();
            var paginationSet = new PaginationSet<TaxViewModel>();
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
                var query = _taxRepository.GetAll().Where(m => m.IsActive == true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(m => new TaxViewModel
                    {
                        ID = m.ID,
                        TaxTypeID = m.TaxTypeID,
                        CreatedDate = m.CreatedDate,
                        Value = m.TaxValueID,
                        CustomerID = m.CustomerID,
                        TaxValueID = m.TaxValueID,
                        DepartmentID = m.DepartmentID,
                        TaxCategoryID = m.TaxCategoryID,
                        InOut = m.InOut,
                        Status = m.Status,
                        IsActive = m.IsActive ?? false,
                        Department = m.Department.DepartmentName,
                        TaxCategory = m.TaxCategory.Category,
                        TaxType = m.TaxType.TaxTypeName,
                        TaxValue = m.TaxValue.Value,
                        TaxDetails = _taxDetailRepository.GetListTaxDetailsByTaxID(m.ID)
                    })
                    .ToList();

                paginationSet = new PaginationSet<TaxViewModel>()
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
        /// Get Tax by id
        /// </summary>
        /// <param name="id">ID of Tax</param>
        /// <returns></returns>
        public ApiResponseViewModel GetById(int id)
        {
            var result = new TaxViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _taxRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    var tempResult = _taxRepository.GetSingleById(id);
                    result.ID = tempResult.ID;
                    result.TaxTypeID = tempResult.TaxTypeID;
                    result.CreatedDate = tempResult.CreatedDate;
                    result.Value = tempResult.TaxValueID;
                    result.CustomerID = tempResult.CustomerID;
                    result.TaxValueID = tempResult.TaxValueID;
                    result.DepartmentID = tempResult.DepartmentID;
                    result.TaxCategoryID = tempResult.TaxCategoryID;
                    result.InOut = tempResult.InOut;
                    result.Status = tempResult.Status;
                    result.IsActive = tempResult.IsActive ?? false;
                    result.Department = tempResult.Department.DepartmentName;
                    result.TaxCategory = tempResult.TaxCategory.Category;
                    result.TaxType = tempResult.TaxType.TaxTypeName;
                    result.TaxValue = tempResult.TaxValue.Value;
                    result.TaxDetails = _taxDetailRepository.GetListTaxDetailsByTaxID(result.ID);
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
        /// Add new Tax
        /// </summary>
        /// <param name="obj">Tax</param>
        /// <returns></returns>
        public ApiResponseViewModel Add(Tax obj)
        {
            var result = new Tax();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _taxRepository.Add(obj);
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
        /// Update Tax
        /// </summary>
        /// <param name="obj">New Tax</param>
        /// <returns></returns>
        public ApiResponseViewModel Update(Tax obj)
        {
            var result = new TaxViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _taxRepository.CheckContains(m => m.ID == obj.ID);
                if (exists)
                {
                    _taxRepository.Update(obj);
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
        /// Delete Tax by Id
        /// </summary>
        /// <param name="id">ID of Tax</param>
        /// <returns></returns>
        public ApiResponseViewModel Delete(int id)
        {
            var result = new Tax();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {

                var exists = _taxRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _taxRepository.Delete(id);
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
        /// Make Tax in-active by Id
        /// </summary>
        /// <param name="id">ID of Tax</param>
        /// <returns></returns>
        public ApiResponseViewModel SetInActive(int id)
        {
            var result = new Tax();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _taxRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _taxRepository.GetSingleById(id);
                    result.IsActive = false;
                    _taxRepository.Update(result);
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