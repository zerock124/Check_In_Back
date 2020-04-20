using Check_In.Utility;
using Check_InDB.Interface;
using Check_InDB.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModels.Share;
using ViewModels.User;

namespace Check_In.Controllers
{
    public class UserController : BaseController
    {
        public string _UserPath = ConfigurationManager.AppSettings["UserPath"];

        protected IUserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// 取得案例圖片
        /// </summary>
        /// <param name="filesname"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<FileResult> UserPhoto(string filename)
        {
            string path = Server.MapPath(_UserPath) + filename;
            if (!System.IO.File.Exists(path))
                throw new HttpException(404, "Some description");

            var imgData = await Task.Run(() => System.IO.File.ReadAllBytes(path));
            string mimeType = MimeMapping.GetMimeMapping(filename);
            return await Task.Run(() => new FileStreamResult(new System.IO.MemoryStream(imgData), mimeType));
        }

        public async Task<JsonResult> GetUserList(UserSearchModel searchModel, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel res = new ResWithPaginationViewModel();

            try
            {
                var result = await _userService.GetUserList(searchModel, pagination);
                res.Data = result.Data;
                res.Pagination = result.Pagination;
                res.MinDateTime = result.MinDateTime;
                res.MaxDateTime = result.MaxDateTime;
                res.Success = true;
                res.Message = "取得權限管理列表成功";
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                //await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "查詢", CurrendUserIp);
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

        public async Task<JsonResult> CreateUser(CreateUserViewModel model)
        {
            ResponseViewModel res = new ResponseViewModel();

            string _path = Server.MapPath(_UserPath);

            try
            {
                if (model.PhotoFile != null)
                {
                    var PhoneFileName = model.PhotoFile.FileName;

                    var saveResult = ImageHandler.SaveFileToPath(model.PhotoFile, _path, PhoneFileName);

                    model.ur_im = model.PhotoFile.FileName;
                }

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
            return Json(res, JsonRequestBehavior.DenyGet);
        }

        public async Task<JsonResult> GetEditUserItem(string ur_id)
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var data = await _userService.GetEditUserItem(ur_id);
                res.Data = data;
                res.Success = true;
                res.Message = "取得帳號資料成功";
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

        public async Task<JsonResult> EditUserItem(UserViewModel model)
        {
            ResponseViewModel res = new ResponseViewModel();

            string _path = Server.MapPath(_UserPath);

            try
            {
                if (model.PhotoFile != null)
                {
                    var PhoneFileName = model.PhotoFile.FileName;

                    var saveResult = ImageHandler.SaveFileToPath(model.PhotoFile, _path, PhoneFileName);

                    model.ur_im = model.PhotoFile.FileName;
                }

                var data = await _userService.EditUserItem(model);
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
            return Json(res, JsonRequestBehavior.DenyGet);
        }

        public async Task<JsonResult> DeleteUserItem(string ur_id, string ur_im)
        {
            ResponseViewModel res = new ResponseViewModel();

            string _path = Server.MapPath(_UserPath);

            try
            {
                if (ur_im != null)
                {
                    FileHandler.DeleteFileIfDefind(_path, ur_im);
                }

                var data = await _userService.DeleteUserItem(ur_id);
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
            return Json(res, JsonRequestBehavior.DenyGet);
        }
    }
}