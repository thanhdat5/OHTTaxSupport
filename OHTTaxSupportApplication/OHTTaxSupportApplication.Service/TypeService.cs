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
    public interface ITypeService
    {

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);

        ApiResponseViewModel Add(Model.Models.Type Type);

        ApiResponseViewModel Update(Model.Models.Type Type);

        ApiResponseViewModel Delete(int id);

        ApiResponseViewModel SetInActive(int id);

    }

    public class TypeService : ITypeService
    {
        private ITypeRepository _TypeRepository;

        private IUnitOfWork _unitOfWork;

        public TypeService(ITypeRepository TypeRepository, IUnitOfWork unitOfWork)
        {
            this._TypeRepository = TypeRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all Tax value
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<TypeViewModel>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _TypeRepository.GetAll().Where(m => m.IsActive == true).Select(m => new TypeViewModel
                {
                    ID = m.ID,
                    TypeName = m.TypeName,
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
        /// Get all Tax value with pagging
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
        {
            var result = new List<TypeViewModel>();
            var paginationSet = new PaginationSet<TypeViewModel>();
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
                var query = _TypeRepository.GetAll().Where(m => m.IsActive == true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(m => new TypeViewModel
                    {
                        ID = m.ID,
                        TypeName = m.TypeName,
                        IsActive = m.IsActive ?? false
                    })
                    .ToList();

                paginationSet = new PaginationSet<TypeViewModel>()
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
            var result = new TypeViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _TypeRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    var tempResult = _TypeRepository.GetSingleById(id);
                    result.ID = tempResult.ID;
                    result.TypeName = tempResult.TypeName;
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
        /// Add new Tax value
        /// </summary>
        /// <param name="obj">Tax value</param>
        /// <returns></returns>
        public ApiResponseViewModel Add(Model.Models.Type obj)
        {
            var result = new Model.Models.Type();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _TypeRepository.Add(obj);
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
        public ApiResponseViewModel Update(Model.Models.Type obj)
        {
            var result = new TypeViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _TypeRepository.CheckContains(m => m.ID == obj.ID);
                if (exists)
                {
                    _TypeRepository.Update(obj);
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
            var result = new Model.Models.Type();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {

                var exists = _TypeRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _TypeRepository.Delete(id);
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
            var result = new Model.Models.Type();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _TypeRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _TypeRepository.GetSingleById(id);
                    result.IsActive = false;
                    _TypeRepository.Update(result);
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