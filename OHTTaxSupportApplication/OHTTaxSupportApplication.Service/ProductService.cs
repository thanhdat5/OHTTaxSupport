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
    public interface IProductService
    {

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);

        ApiResponseViewModel Add(Product product);

        ApiResponseViewModel Update(Product product);

        ApiResponseViewModel Delete(int id);

        ApiResponseViewModel SetInActive(int id);

    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IUnitRepository _unitRepository;

        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitRepository unitRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._unitRepository = unitRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all Product
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<ProductViewModel>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _productRepository.GetAll().Where(m => m.IsActive == true).Select(m => new ProductViewModel
                {
                    ID = m.ID,
                    ProductName = m.ProductName,
                    Description = m.Description,
                    UnitID = m.UnitID,
                    UnitID2 = m.UnitID2,
                    IsActive = m.IsActive ?? false,
                    UnitName = _unitRepository.GetSingleById(m.UnitID).Value,
                    Unit2Name = m.UnitID2 == null ? null : _unitRepository.GetSingleById(m.UnitID2 ?? 0).Value
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
        /// Get all Product with pagging
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
        {
            var result = new List<ProductViewModel>();
            var paginationSet = new PaginationSet<ProductViewModel>();
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
                var query = _productRepository.GetAll().Where(m => m.IsActive == true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(m => new ProductViewModel
                    {
                        ID = m.ID,
                        ProductName = m.ProductName,
                        Description = m.Description,
                        UnitID = m.UnitID,
                        UnitID2 = m.UnitID2,
                        IsActive = m.IsActive ?? false,
                        UnitName = _unitRepository.GetSingleById(m.UnitID).Value,
                        Unit2Name = m.UnitID2 == null ? null : _unitRepository.GetSingleById(m.UnitID2 ?? 0).Value
                    })
                    .ToList();

                paginationSet = new PaginationSet<ProductViewModel>()
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
        /// Get Product by id
        /// </summary>
        /// <param name="id">ID of Product</param>
        /// <returns></returns>
        public ApiResponseViewModel GetById(int id)
        {
            var result = new ProductViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _productRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    var tempResult = _productRepository.GetSingleById(id);
                    result.ID = tempResult.ID;
                    result.ProductName = tempResult.ProductName;
                    result.Description = tempResult.Description;
                    result.UnitID = tempResult.UnitID;
                    result.UnitID2 = tempResult.UnitID2;
                    result.IsActive = tempResult.IsActive ?? false;
                    result.UnitName = _unitRepository.GetSingleById(tempResult.UnitID).Value;
                    result.Unit2Name = tempResult.UnitID2 == null ? null : _unitRepository.GetSingleById(tempResult.UnitID2 ?? 0).Value;
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
        /// Add new Product
        /// </summary>
        /// <param name="obj">Product</param>
        /// <returns></returns>
        public ApiResponseViewModel Add(Product obj)
        {
            var result = new Product();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _productRepository.Add(obj);
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
        /// Update Product
        /// </summary>
        /// <param name="obj">New Product</param>
        /// <returns></returns>
        public ApiResponseViewModel Update(Product obj)
        {
            var result = new ProductViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _productRepository.CheckContains(m => m.ID == obj.ID);
                if (exists)
                {
                    _productRepository.Update(obj);
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
        /// Delete Product by Id
        /// </summary>
        /// <param name="id">ID of Product</param>
        /// <returns></returns>
        public ApiResponseViewModel Delete(int id)
        {
            var result = new Product();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {

                var exists = _productRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _productRepository.Delete(id);
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
        /// Make Product in-active by Id
        /// </summary>
        /// <param name="id">ID of Product</param>
        /// <returns></returns>
        public ApiResponseViewModel SetInActive(int id)
        {
            var result = new Product();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _productRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _productRepository.GetSingleById(id);
                    result.IsActive = false;
                    _productRepository.Update(result);
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