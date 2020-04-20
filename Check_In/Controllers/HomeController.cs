using Check_InDB.Interface;
using Check_InDB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModels.Share;
using ViewModels.User;

namespace Check_In.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        protected IUserService _userService;

        public HomeController()
        {
            _userService = new UserService();
        }

        public async Task<JsonResult> CreateUserTest()
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                CreateUserViewModel model = new CreateUserViewModel();

                model.ur_ac = "test";
                model.ur_pw = "test";
                model.ur_im = "test";

                var data = await _userService.CreateUser(model);
                res.Success = data.Success;
                res.Message = data.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;

            }
            catch
            {
                res.Success = false;
                res.Message = "與伺服器連線發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}