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
    public interface IUserService
    {

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);

        ApiResponseViewModel Add(User user);

        ApiResponseViewModel Update(User user);

        ApiResponseViewModel Delete(int id);

        ApiResponseViewModel SetInActive(int id);

        ApiResponseViewModel CheckLogin(UserViewModel obj);

    }

    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IUserAccountRepository _userAccountRepository;

        private IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUserAccountRepository userAccountRepository, IUnitOfWork unitOfWork)
        {
            this._userRepository = userRepository;
            this._userAccountRepository = userAccountRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all Tax value
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<UserViewModel>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _userRepository.GetAll().Where(m => m.IsActive == true).Select(m => new UserViewModel
                {
                    ID = m.ID,
                    Username = m.Username,
                    Password = m.Password,
                    Fullname = m.Fullname,
                    CompanyID = m.CompanyID,
                    Image = m.Image,
                    Age = m.Age,
                    Address = m.Address,
                    AboutMe = m.AboutMe,
                    LastOnline = Common.Utilities.TimeAgo(m.LastOnline),
                    IsActive = m.IsActive ?? false,
                    Company = m.Company.CompanyName,
                    UserAccounts = _userAccountRepository.GetListUserAccountByUserID(m.ID)
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
            var result = new List<UserViewModel>();
            var paginationSet = new PaginationSet<UserViewModel>();
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
                var query = _userRepository.GetAll().Where(m => m.IsActive == true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(m => new UserViewModel
                    {
                        ID = m.ID,
                        Username = m.Username,
                        Password = m.Password,
                        Fullname = m.Fullname,
                        CompanyID = m.CompanyID,
                        Image = m.Image,
                        Age = m.Age,
                        Address = m.Address,
                        AboutMe = m.AboutMe,
                        LastOnline = Common.Utilities.TimeAgo(m.LastOnline),
                        IsActive = m.IsActive ?? false,
                        Company = m.Company.CompanyName,
                        UserAccounts = _userAccountRepository.GetListUserAccountByUserID(m.ID)
                    })
                    .ToList();

                paginationSet = new PaginationSet<UserViewModel>()
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
            var result = new UserViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _userRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    var tempResult = _userRepository.GetSingleById(id);
                    result.ID = tempResult.ID;
                    result.Username = tempResult.Username;
                    result.Password = tempResult.Password;
                    result.Fullname = tempResult.Fullname;
                    result.CompanyID = tempResult.CompanyID;
                    result.Image = tempResult.Image;
                    result.Age = tempResult.Age;
                    result.Address = tempResult.Address;
                    result.AboutMe = tempResult.AboutMe;
                    result.LastOnline = Common.Utilities.TimeAgo(tempResult.LastOnline);
                    result.IsActive = tempResult.IsActive ?? false;
                    result.Company = tempResult.Company.CompanyName;
                    result.UserAccounts = _userAccountRepository.GetListUserAccountByUserID(tempResult.ID);
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
        public ApiResponseViewModel Add(User obj)
        {
            var result = new User();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _userRepository.Add(obj);
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
        public ApiResponseViewModel Update(User obj)
        {
            var result = new UserViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _userRepository.CheckContains(m => m.ID == obj.ID);
                if (exists)
                {
                    _userRepository.Update(obj);
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
            var result = new User();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {

                var exists = _userRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _userRepository.Delete(id);
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
            var result = new User();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _userRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _userRepository.GetSingleById(id);
                    result.IsActive = false;
                    _userRepository.Update(result);
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
        /// Check login
        /// </summary>
        /// <param name="obj">Username and password</param>
        /// <returns></returns>
        public ApiResponseViewModel CheckLogin(UserViewModel obj)
        {
            var result = new UserViewModel();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var user = _userRepository.GetUserByUsername(obj.Username);
                if (user == null)
                {
                    response.Code = 2;
                    response.Message = "Your account is not exists.";
                }
                else
                {
                    if (user.IsActive != true)
                    {
                        response.Code = 3;
                        response.Message = "Your account is inactive. Please contact adminitrator.";
                    }
                    else
                    {
                        if (Utilities.Md5(obj.Password) != user.Password)
                        {
                            response.Code = 4;
                            response.Message = "Password is incorect.";
                        }
                        else
                        {
                            result.ID = user.ID;
                            result.Username = user.Username;
                            result.Password = user.Password;
                            result.Fullname = user.Fullname;
                            result.CompanyID = user.CompanyID;
                            result.Image = user.Image;
                            result.Age = user.Age;
                            result.Address = user.Address;
                            result.AboutMe = user.AboutMe;
                            result.LastOnline = Common.Utilities.TimeAgo(user.LastOnline);
                            result.IsActive = user.IsActive ?? false;
                            result.Company = user.Company.CompanyName;
                            result.UserAccounts = _userAccountRepository.GetListUserAccountByUserID(user.ID);
                            response.Result = result;
                        }
                    }
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