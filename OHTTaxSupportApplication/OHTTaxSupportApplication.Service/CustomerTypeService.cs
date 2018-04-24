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
    public interface ICustomerTypeService
    {

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);

        ApiResponseViewModel Add(CustomerType customerType);

        ApiResponseViewModel Update(CustomerType customerType);

        ApiResponseViewModel Delete(int id);

        ApiResponseViewModel SetInActive(int id);

    }

    public class CustomerTypeService : ICustomerTypeService
    {
        private ICustomerTypeRepository _customerTypeRepository;

        private IUnitOfWork _unitOfWork;

        public CustomerTypeService(ICustomerTypeRepository customerTypeRepository, IUnitOfWork unitOfWork)
        {
            this._customerTypeRepository = customerTypeRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all CustomerType
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<CustomerTypeViewModel>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _customerTypeRepository.GetAll().Where(m => m.IsActive == true).Select(m => new CustomerTypeViewModel
                {
                    ID = m.ID,
                    CustomerTypeName = m.CustomerTypeName,   
                    IsActive = m.IsActive ?? false,    
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
        /// Get all CustomerType with pagging
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
        {
            var result = new List<CustomerTypeViewModel>();
            var paginationSet = new PaginationSet<CustomerTypeViewModel>();
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
                var query = _customerTypeRepository.GetAll().Where(m => m.IsActive == true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(m => new CustomerTypeViewModel
                    {
                        ID = m.ID,
                        CustomerTypeName = m.CustomerTypeName,
                        IsActive = m.IsActive ?? false,
                    })
                    .ToList();

                paginationSet = new PaginationSet<CustomerTypeViewModel>()
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
        /// Get CustomerType by id
        /// </summary>
        /// <param name="id">ID of CustomerType</param>
        /// <returns></returns>
        public ApiResponseViewModel GetById(int id)
        {
            var result = new CustomerTypeViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _customerTypeRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    var tempResult = _customerTypeRepository.GetSingleById(id);
                    result.ID = tempResult.ID;
                    result.CustomerTypeName = tempResult.CustomerTypeName;     
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
        /// Add new CustomerType
        /// </summary>
        /// <param name="obj">CustomerType</param>
        /// <returns></returns>
        public ApiResponseViewModel Add(CustomerType obj)
        {
            var result = new CustomerType();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _customerTypeRepository.Add(obj);
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
        /// Update CustomerType
        /// </summary>
        /// <param name="obj">New CustomerType</param>
        /// <returns></returns>
        public ApiResponseViewModel Update(CustomerType obj)
        {
            var result = new CustomerTypeViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _customerTypeRepository.CheckContains(m => m.ID == obj.ID);
                if (exists)
                {
                    _customerTypeRepository.Update(obj);
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
        /// Delete CustomerType by Id
        /// </summary>
        /// <param name="id">ID of CustomerType</param>
        /// <returns></returns>
        public ApiResponseViewModel Delete(int id)
        {
            var result = new CustomerType();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {

                var exists = _customerTypeRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _customerTypeRepository.Delete(id);
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
        /// Make CustomerType in-active by Id
        /// </summary>
        /// <param name="id">ID of CustomerType</param>
        /// <returns></returns>
        public ApiResponseViewModel SetInActive(int id)
        {
            var result = new CustomerType();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _customerTypeRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _customerTypeRepository.GetSingleById(id);
                    result.IsActive = false;
                    _customerTypeRepository.Update(result);
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