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
    public interface IInvoiceDetailService
    {

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);
        ApiResponseViewModel GetByInvoiceId(int id);

        ApiResponseViewModel Add(InvoiceDetail InvoiceDetail);

        ApiResponseViewModel Update(InvoiceDetail InvoiceDetail);

        ApiResponseViewModel Delete(int id);

        ApiResponseViewModel SetInActive(int id);

    }

    public class InvoiceDetailService : IInvoiceDetailService
    {
        private IInvoiceDetailRepository _InvoiceDetailRepository;
        private IInvoiceRepository _invoiceRepository;

        private IUnitOfWork _unitOfWork;

        public InvoiceDetailService(IInvoiceDetailRepository InvoiceDetailRepository, IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
        {
            _InvoiceDetailRepository = InvoiceDetailRepository;
            _invoiceRepository = invoiceRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all InvoiceDetail
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<InvoiceDetailViewModel>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _InvoiceDetailRepository.GetMulti(m => m.IsActive == true).Select(m => new InvoiceDetailViewModel
                {
                    ID = m.ID,
                    InvoiceID = m.InvoiceID,
                    ProductID = m.ProductID,
                    UnitID = m.UnitID,
                    Value = m.Value,
                    Quanlity = m.Quanlity,
                    IsActive = m.IsActive ?? false,
                    Product = m.Product.ProductName,
                    InOut = m.InOut,
                    DepartmentID = m.DepartmentID,
                    Department = m.Department.DepartmentName,
                    CategoryID = m.CategoryID,
                    Category = m.Category.CategoryName,
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
        /// Get all InvoiceDetail with pagging
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
        {
            var result = new List<InvoiceDetailViewModel>();
            var paginationSet = new PaginationSet<InvoiceDetailViewModel>();
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
                var query = _InvoiceDetailRepository.GetMulti(m => m.IsActive == true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(m => new InvoiceDetailViewModel
                    {
                        ID = m.ID,
                        InvoiceID = m.InvoiceID,
                        ProductID = m.ProductID,
                        UnitID = m.UnitID,
                        Value = m.Value,
                        Quanlity = m.Quanlity,
                        IsActive = m.IsActive ?? false,
                        Product = m.Product.ProductName,
                        InOut = m.InOut,
                        DepartmentID = m.DepartmentID,
                        Department = m.Department.DepartmentName,
                        CategoryID = m.CategoryID,
                        Category = m.Category.CategoryName,
                        Unit = m.Unit.Value
                    })
                    .ToList();

                paginationSet = new PaginationSet<InvoiceDetailViewModel>()
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
        /// Get InvoiceDetail by id
        /// </summary>
        /// <param name="id">ID of InvoiceDetail</param>
        /// <returns></returns>
        public ApiResponseViewModel GetById(int id)
        {
            var result = new InvoiceDetailViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _InvoiceDetailRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    var tempResult = _InvoiceDetailRepository.GetSingleById(id);
                    result.ID = tempResult.ID;
                    result.InvoiceID = tempResult.InvoiceID;
                    result.ProductID = tempResult.ProductID;
                    result.UnitID = tempResult.UnitID;
                    result.Value = tempResult.Value;
                    result.Quanlity = tempResult.Quanlity;
                    result.IsActive = tempResult.IsActive ?? false;
                    result.Product = tempResult.Product.ProductName;
                    result.Unit = tempResult.Unit.Value;
                    result.InOut = tempResult.InOut;
                    result.DepartmentID = tempResult.DepartmentID;
                    result.Department = tempResult.Department.DepartmentName;
                    result.CategoryID = tempResult.CategoryID;
                    result.Category = tempResult.Category.CategoryName;
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
        /// Get InvoiceDetail by invoice id
        /// </summary>
        /// <param name="id">ID of Invoice</param>
        /// <returns></returns>
        public ApiResponseViewModel GetByInvoiceId(int id)
        {
            var lstResult = new List<InvoiceDetailViewModel>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _InvoiceDetailRepository.CheckContains(m => m.InvoiceID == id);
                var invoice = _invoiceRepository.GetSingleById(id);
                if (exists)
                {
                    var lst = _InvoiceDetailRepository.GetMulti(m => m.InvoiceID == id && m.IsActive == true).ToList();
                    foreach (var tempResult in lst)
                    {
                        var result = new InvoiceDetailViewModel();
                        result.ID = tempResult.ID;
                        result.InvoiceID = tempResult.InvoiceID;
                        result.ProductID = tempResult.ProductID;
                        result.UnitID = tempResult.UnitID;
                        result.Value = tempResult.Value;
                        result.Quanlity = tempResult.Quanlity;
                        result.IsActive = tempResult.IsActive ?? false;
                        result.Product = tempResult.Product != null ? tempResult.Product.ProductName : "";
                        result.Unit = tempResult.Unit != null ? tempResult.Unit.Value : "";
                        result.InOut = tempResult.InOut;
                        result.DepartmentID = tempResult.DepartmentID;
                        result.Department = tempResult.Department != null ? tempResult.Department.DepartmentName : "";
                        result.CategoryID = tempResult.CategoryID;
                        result.Category = tempResult.Category != null ? tempResult.Category.CategoryName : "";
                        result.TaxValue = invoice.TaxValueID != null ? invoice.TaxValue.Value.ToString() : "0";
                        lstResult.Add(result);
                    }
                    response.Result = lstResult;
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
        /// Add new InvoiceDetail
        /// </summary>
        /// <param name="obj">InvoiceDetail</param>
        /// <returns></returns>
        public ApiResponseViewModel Add(InvoiceDetail obj)
        {
            var result = new InvoiceDetail();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _InvoiceDetailRepository.Add(obj);
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
        /// Update InvoiceDetail
        /// </summary>
        /// <param name="obj">New InvoiceDetail</param>
        /// <returns></returns>
        public ApiResponseViewModel Update(InvoiceDetail obj)
        {
            var result = new InvoiceDetailViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _InvoiceDetailRepository.CheckContains(m => m.ID == obj.ID);
                if (exists)
                {
                    _InvoiceDetailRepository.Update(obj);
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
        /// Delete InvoiceDetail by Id
        /// </summary>
        /// <param name="id">ID of InvoiceDetail</param>
        /// <returns></returns>
        public ApiResponseViewModel Delete(int id)
        {
            var result = new InvoiceDetail();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _InvoiceDetailRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _InvoiceDetailRepository.Delete(id);
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
        /// Make InvoiceDetail in-active by Id
        /// </summary>
        /// <param name="id">ID of InvoiceDetail</param>
        /// <returns></returns>
        public ApiResponseViewModel SetInActive(int id)
        {
            var result = new InvoiceDetail();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _InvoiceDetailRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _InvoiceDetailRepository.GetSingleById(id);
                    result.IsActive = false;
                    _InvoiceDetailRepository.Update(result);
                    _unitOfWork.Commit();
                    response.Message = CommonConstants.DeleteSuccess;

                    var temp = new InvoiceDetailViewModel();
                    temp.ID = result.ID;
                    temp.InvoiceID = result.InvoiceID;
                    response.Result = temp;
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