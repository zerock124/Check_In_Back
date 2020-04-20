using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Check_InDB.Models;

namespace Check_In.Controllers
{
    [HandleError]
    [Authorize]
    public class BaseController : Controller
    {
        protected Check_testEntities _db;

        public BaseController()
        {
            _db = new Check_testEntities();
        }

        /// <summary>
        /// Serilog 
        /// </summary>
        //public readonly ILogger _logger = Log.Logger;
        //public readonly AuthRepository _repository = new AuthRepository();
        //public readonly AuthRepository _repository = new AuthRepository();
        public ApplicationUserManager _userManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        /// <summary>
        /// 取得當前的使用者ID
        /// </summary>
        public string CurrendUserid => User.Identity.GetUserId();
        /// <summary>
        /// 取得當前的使用者IP
        /// </summary>
        public string CurrendUserIp => GetClientIP();

        public string GetClientIP()
        {
            //判所client端是否有設定代理伺服器
            if (Request.ServerVariables["HTTP_VIA"] == null)
                return Request.ServerVariables["REMOTE_ADDR"].ToString();
            else
                return Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        }

    }
}