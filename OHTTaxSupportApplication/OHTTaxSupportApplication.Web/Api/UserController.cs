using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using OHTTaxSupportApplication.Common;
using OHTTaxSupportApplication.Model.Models;
using OHTTaxSupportApplication.Model.ViewModels;
using OHTTaxSupportApplication.Service;
using OHTTaxSupportApplication.Web.Infrastructure.Core;

namespace OHTTaxSupportApplication.Web.Api
{
    [System.Web.Http.RoutePrefix("api/Users")]
    public class UserController : ApiControllerBase
    {
        #region Initialize
        private IUserService _userService;

        public UserController(IErrorService errorService, IUserService userService)
            : base(errorService)
        {
            this._userService = userService;
        }

        #endregion

        [System.Web.Http.Route("getall")]
        [System.Web.Http.HttpGet]
        public ApiResponseViewModel GetAll()
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _userService.GetAll();
        }

        [System.Web.Http.Route("getallwithpaging")]
        [System.Web.Http.HttpGet]
        public ApiResponseViewModel GetAllWithPagging(int page, int pageSize)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _userService.GetAllWithPagging(page, pageSize);
        }

        [System.Web.Http.Route("getbyid/{id:int}")]
        [System.Web.Http.HttpGet]
        public ApiResponseViewModel GetById(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _userService.GetById(id);
        }

        [System.Web.Http.Route("GetProfile")]
        [System.Web.Http.HttpGet]
        public ApiResponseViewModel GetProfile()
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            var userLogged = (UserViewModel)System.Web.HttpContext.Current.Session["UserLogged"];
            return _userService.GetById(userLogged.ID);
        }

        [System.Web.Http.Route("create")]
        [System.Web.Http.HttpPost]
        public ApiResponseViewModel Create(User obj)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _userService.Add(obj);
        }

        [System.Web.Http.Route("update")]
        [System.Web.Http.HttpPost]
        public ApiResponseViewModel Update(User obj)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _userService.Update(obj);
        }

        [System.Web.Http.Route("delete")]
        [System.Web.Http.HttpPost]
        public ApiResponseViewModel Delete(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _userService.Delete(id);
        }

        [System.Web.Http.Route("setinactive")]
        [System.Web.Http.HttpPost]
        public ApiResponseViewModel SetInActive(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _userService.SetInActive(id);
        }

        [System.Web.Http.Route("CheckLogin")]
        [System.Web.Http.HttpPost]
        public ApiResponseViewModel CheckLogin(UserViewModel obj)
        {
            var response = _userService.CheckLogin(obj);
            if (response.Result != null)
            {
                System.Web.HttpContext.Current.Session["UserLogged"] = response.Result;
            }
            return response;
        }

        [System.Web.Http.Route("UploadAvatar")]
        [System.Web.Http.HttpPost]
        public ApiResponseViewModel UploadAvatar()
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            var result = "";
            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var pic = HttpContext.Current.Request.Files["HelpSectionImages"];
                    var fileName = Path.GetFileName(pic.FileName);
                    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Images"), fileName);
                    pic.SaveAs(path);
                    result = "/Content/Images/" + fileName;
                }
                return new ApiResponseViewModel
                {
                    Code = 0,
                    Message = "Upload successfully!",
                    Result = result
                };
            }
            catch (Exception)
            {
                return new ApiResponseViewModel
                {
                    Code = 1,
                    Message = "Upload error!",
                    Result = result
                };
            }
        }
    }
}
