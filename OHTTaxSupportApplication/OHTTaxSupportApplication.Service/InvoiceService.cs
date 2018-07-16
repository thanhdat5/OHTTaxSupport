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
    public interface IInvoiceService
    {

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);

        ApiResponseViewModel Add(Invoice invoice);
        ApiResponseViewModel SaveAll(List<InvoiceInput> invoice);

        ApiResponseViewModel Update(Invoice invoice);

        ApiResponseViewModel Delete(int id);

        ApiResponseViewModel SetInActive(int id);

    }

    public class InvoiceService : IInvoiceService
    {
        private IInvoiceRepository _InvoiceRepository;
        private IInvoiceDetailRepository _InvoiceDetailRepository;

        private IUnitOfWork _unitOfWork;

        public InvoiceService(IInvoiceRepository InvoiceRepository, IInvoiceDetailRepository InvoiceDetailRepository, IUnitOfWork unitOfWork)
        {
            this._InvoiceRepository = InvoiceRepository;
            this._InvoiceDetailRepository = InvoiceDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all Invoice
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<InvoiceViewModel>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _InvoiceRepository.GetMulti(m => m.IsActive == true).Select(m => new InvoiceViewModel
                {
                    ID = m.ID,
                    TypeID = m.TypeID,
                    CreatedDate = m.CreatedDate,
                    CustomerID = m.CustomerID,
                    TaxValueID = m.TaxValueID,
                    Status = m.Status,
                    Value = m.Value,
                    IsActive = m.IsActive ?? false,
                    Type = m.Type.TypeName,
                    TaxValue = m.TaxValue.Value,
                    InvoiceDetails = _InvoiceDetailRepository.GetListInvoiceDetailsByInvoiceID(m.ID)
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
        /// Get all invoice with pagging
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
        {
            var result = new List<InvoiceViewModel>();
            var paginationSet = new PaginationSet<InvoiceViewModel>();
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
                var query = _InvoiceRepository.GetMulti(m => m.IsActive == true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(m => new InvoiceViewModel
                    {
                        ID = m.ID,
                        TypeID = m.TypeID,
                        CreatedDate = m.CreatedDate != null ? m.CreatedDate : DateTime.Now,
                        CustomerID = m.CustomerID,
                        TaxValueID = m.TaxValueID,
                        Status = m.Status,
                        IsActive = m.IsActive ?? false,
                        Type = m.Type.TypeName,
                        Value = m.Value,
                        TaxValue = m.TaxValue.Value,
                        InvoiceDetails = _InvoiceDetailRepository.GetListInvoiceDetailsByInvoiceID(m.ID)
                    })
                    .ToList();

                paginationSet = new PaginationSet<InvoiceViewModel>()
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
        /// Get invoice by id
        /// </summary>
        /// <param name="id">ID of invoice</param>
        /// <returns></returns>
        public ApiResponseViewModel GetById(int id)
        {
            var result = new InvoiceViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _InvoiceRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    var tempResult = _InvoiceRepository.GetSingleById(id);
                    result.ID = tempResult.ID;
                    result.TypeID = tempResult.TypeID;
                    result.CreatedDate = tempResult.CreatedDate;
                    result.CustomerID = tempResult.CustomerID;
                    result.TaxValueID = tempResult.TaxValueID;
                    result.Status = tempResult.Status;
                    result.IsActive = tempResult.IsActive ?? false;
                    result.Type = tempResult.Type.TypeName;
                    result.TaxValue = tempResult.TaxValue.Value;
                    result.Value = tempResult.Value;
                    result.InvoiceDetails = _InvoiceDetailRepository.GetListInvoiceDetailsByInvoiceID(result.ID);
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
        /// Add new invoice
        /// </summary>
        /// <param name="obj">invoice</param>
        /// <returns></returns>
        public ApiResponseViewModel Add(Invoice obj)
        {
            var result = new Invoice();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                obj.CreatedDate = DateTime.Now;
                result = _InvoiceRepository.Add(obj);
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
        /// Save all invoices
        /// </summary>
        /// <param name="lst">invoices</param>
        /// <returns></returns>
        public ApiResponseViewModel SaveAll(List<InvoiceInput> lst)
        {
            var result = new Invoice();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            //try
            //{
            //    obj.CreatedDate = DateTime.Now;
            //    result = _InvoiceRepository.Add(obj);
            //    _unitOfWork.Commit();
            //    response.Message = CommonConstants.AddSuccess;
            //    response.Result = result;
            //}
            //catch (Exception ex)
            //{
            //    response.Code = CommonConstants.ApiResponseExceptionCode;
            //    response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
            //}
            return response;

        }

        /// <summary>
        /// Update invoice
        /// </summary>
        /// <param name="obj">New invoice</param>
        /// <returns></returns>
        public ApiResponseViewModel Update(Invoice obj)
        {
            var result = new InvoiceViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _InvoiceRepository.CheckContains(m => m.ID == obj.ID);
                if (exists)
                {
                    _InvoiceRepository.Update(obj);
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
        /// Delete invoice by Id
        /// </summary>
        /// <param name="id">ID of invoice</param>
        /// <returns></returns>
        public ApiResponseViewModel Delete(int id)
        {
            var result = new Invoice();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _InvoiceRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _InvoiceRepository.Delete(id);
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
        /// Make invoice in-active by Id
        /// </summary>
        /// <param name="id">ID of invoice</param>
        /// <returns></returns>
        public ApiResponseViewModel SetInActive(int id)
        {
            var result = new Invoice();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _InvoiceRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _InvoiceRepository.GetSingleById(id);
                    result.IsActive = false;
                    _InvoiceRepository.Update(result);
                    _unitOfWork.Commit();
                    response.Message = CommonConstants.DeleteSuccess;
                    var temp = new InvoiceViewModel();
                    temp.ID = result.ID;
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