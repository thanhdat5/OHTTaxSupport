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
    public interface ITaxDetailService
    {

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);

        ApiResponseViewModel Add(TaxDetail taxDetail);

        ApiResponseViewModel Update(TaxDetail taxDetail);

        ApiResponseViewModel Delete(int id);

        ApiResponseViewModel SetInActive(int id);

    }

    public class TaxDetailService : ITaxDetailService
    {
        private ITaxDetailRepository _taxDetailRepository;

        private IUnitOfWork _unitOfWork;

        public TaxDetailService(ITaxDetailRepository taxDetailRepository, IUnitOfWork unitOfWork)
        {
            this._taxDetailRepository = taxDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all TaxDetail
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<TaxDetailViewModel>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _taxDetailRepository.GetAll().Where(m => m.IsActive == true).Select(m => new TaxDetailViewModel
                {
                    ID = m.ID,
                    TaxID = m.TaxID,
                    ProductID = m.ProductID,
                    UnitID = m.UnitID,
                    Value = m.Value,
                    Quanlity = m.Quanlity,
                    Note = m.Note,
                    IsActive = m.IsActive ?? false,
                    Product = m.Product.ProductName,
                    Unit = m.Unit.Value
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
        /// Get all TaxDetail with pagging
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
        {
            var result = new List<TaxDetailViewModel>();
            var paginationSet = new PaginationSet<TaxDetailViewModel>();
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
                var query = _taxDetailRepository.GetAll().Where(m => m.IsActive == true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(m => new TaxDetailViewModel
                    {
                        ID = m.ID,
                        TaxID = m.TaxID,
                        ProductID = m.ProductID,
                        UnitID = m.UnitID,
                        Value = m.Value,
                        Quanlity = m.Quanlity,
                        Note = m.Note,
                        IsActive = m.IsActive ?? false,
                        Product = m.Product.ProductName,
                        Unit = m.Unit.Value
                    })
                    .ToList();

                paginationSet = new PaginationSet<TaxDetailViewModel>()
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
        /// Get TaxDetail by id
        /// </summary>
        /// <param name="id">ID of TaxDetail</param>
        /// <returns></returns>
        public ApiResponseViewModel GetById(int id)
        {
            var result = new TaxDetailViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _taxDetailRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    var tempResult = _taxDetailRepository.GetSingleById(id);
                    result.ID = tempResult.ID;
                    result.TaxID = tempResult.TaxID;
                    result.ProductID = tempResult.ProductID;
                    result.UnitID = tempResult.UnitID;
                    result.Value = tempResult.Value;
                    result.Quanlity = tempResult.Quanlity;
                    result.Note = tempResult.Note;
                    result.IsActive = tempResult.IsActive ?? false;
                    result.Product = tempResult.Product.ProductName;
                    result.Unit = tempResult.Unit.Value;
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
        /// Add new TaxDetail
        /// </summary>
        /// <param name="obj">TaxDetail</param>
        /// <returns></returns>
        public ApiResponseViewModel Add(TaxDetail obj)
        {
            var result = new TaxDetail();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _taxDetailRepository.Add(obj);
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
        /// Update TaxDetail
        /// </summary>
        /// <param name="obj">New TaxDetail</param>
        /// <returns></returns>
        public ApiResponseViewModel Update(TaxDetail obj)
        {
            var result = new TaxDetailViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _taxDetailRepository.CheckContains(m => m.ID == obj.ID);
                if (exists)
                {
                    _taxDetailRepository.Update(obj);
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
        /// Delete TaxDetail by Id
        /// </summary>
        /// <param name="id">ID of TaxDetail</param>
        /// <returns></returns>
        public ApiResponseViewModel Delete(int id)
        {
            var result = new TaxDetail();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {

                var exists = _taxDetailRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _taxDetailRepository.Delete(id);
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
        /// Make TaxDetail in-active by Id
        /// </summary>
        /// <param name="id">ID of TaxDetail</param>
        /// <returns></returns>
        public ApiResponseViewModel SetInActive(int id)
        {
            var result = new TaxDetail();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _taxDetailRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _taxDetailRepository.GetSingleById(id);
                    result.IsActive = false;
                    _taxDetailRepository.Update(result);
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