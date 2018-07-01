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
    public interface ICustomerService
    {

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);

        ApiResponseViewModel Add(Customer customer);

        ApiResponseViewModel Update(Customer customer);

        ApiResponseViewModel Delete(int id);

        ApiResponseViewModel SetInActive(int id);

    }

    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;
        private ICompanyRepository _companyRepository;
        private ICustomerTypeRepository _customerTypeRepository;

        private IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepository, ICompanyRepository companyRepository, ICustomerTypeRepository customerTypeRepository, IUnitOfWork unitOfWork)
        {
            this._customerRepository = customerRepository;
            this._companyRepository = companyRepository;
            this._customerTypeRepository = customerTypeRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all Customer
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<CustomerViewModel>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _customerRepository.GetAll().Where(m => m.IsActive == true).Select(m => new CustomerViewModel
                {
                    ID = m.ID,
                    CustomerTypeID = m.CustomerTypeID,
                    CustomerName = m.CustomerName,
                    CompanyID = m.CompanyID,
                    Address = m.Address,
                    PhoneNumber = m.PhoneNumber,
                    IsActive = m.IsActive ?? false,
                    Company = m.Company.CompanyName,
                    CustomerType = m.CustomerType.CustomerTypeName
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
        /// Get all Customer with pagging
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
        {
            var result = new List<CustomerViewModel>();
            var paginationSet = new PaginationSet<CustomerViewModel>();
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
                var query = _customerRepository.GetAll().Where(m => m.IsActive == true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(m => new CustomerViewModel
                    {
                        ID = m.ID,
                        CustomerTypeID = m.CustomerTypeID,
                        CustomerName = m.CustomerName,
                        CompanyID = m.CompanyID,
                        Address = m.Address,
                        PhoneNumber = m.PhoneNumber,
                        IsActive = m.IsActive ?? false,
                        Company = m.Company.CompanyName,
                        CustomerType = m.CustomerType.CustomerTypeName
                    })
                    .ToList();

                paginationSet = new PaginationSet<CustomerViewModel>()
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
        /// Get Customer by id
        /// </summary>
        /// <param name="id">ID of Customer</param>
        /// <returns></returns>
        public ApiResponseViewModel GetById(int id)
        {
            var result = new CustomerViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _customerRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    var tempResult = _customerRepository.GetSingleById(id);
                    result.ID = tempResult.ID;
                    result.CustomerTypeID = tempResult.CustomerTypeID;
                    result.CustomerName = tempResult.CustomerName;
                    result.CompanyID = tempResult.CompanyID;
                    result.Address = tempResult.Address;
                    result.PhoneNumber = tempResult.PhoneNumber;
                    result.IsActive = tempResult.IsActive ?? false;
                    result.CustomerType = tempResult.CustomerType.CustomerTypeName;
                    result.Company = tempResult.Company.CompanyName;
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
        /// Add new Customer
        /// </summary>
        /// <param name="obj">Customer</param>
        /// <returns></returns>
        public ApiResponseViewModel Add(Customer obj)
        {
            var result = new Customer();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _customerRepository.Add(obj);
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
        /// Update Customer
        /// </summary>
        /// <param name="obj">New Customer</param>
        /// <returns></returns>
        public ApiResponseViewModel Update(Customer obj)
        {
            var result = new CustomerViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _customerRepository.GetSingleById(obj.ID);
                if (exists != null)
                {
                    exists.Address = obj.Address;
                    exists.CompanyID = obj.CompanyID;
                    exists.CustomerName = obj.CustomerName;
                    exists.CustomerTypeID = obj.CustomerTypeID;
                    exists.IsActive = obj.IsActive;
                    exists.PhoneNumber = obj.PhoneNumber;
                    _customerRepository.Update(exists);
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
        /// Delete Customer by Id
        /// </summary>
        /// <param name="id">ID of Customer</param>
        /// <returns></returns>
        public ApiResponseViewModel Delete(int id)
        {
            var result = new Customer();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {

                var exists = _customerRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _customerRepository.Delete(id);
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
        /// Make Customer in-active by Id
        /// </summary>
        /// <param name="id">ID of Customer</param>
        /// <returns></returns>
        public ApiResponseViewModel SetInActive(int id)
        {
            var result = new Customer();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _customerRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _customerRepository.GetSingleById(id);
                    result.IsActive = false;
                    _customerRepository.Update(result);
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