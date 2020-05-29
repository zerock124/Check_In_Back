using Check_InDB.Interface;
using Check_InDB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModels.Share;
using ViewModels.TotalRecord;

namespace Check_In.Controllers
{
    public class TotalRecordController : BaseController
    {
        protected ITotalRecordService _totalService;

        public TotalRecordController()
        {
            _totalService = new TotalRecordService();
        }

        // GET: TotalRecord
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 取得彙總紀錄列表
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetTotalRecord(SearchModel searchModel)
        {
            ResWithPaginationViewModel res = new ResWithPaginationViewModel();

            try
            {
                var result = await _totalService.GetTotalRecordList(searchModel);
                res.Data = result.Data;
                res.Success = result.Success;
                res.Message = result.Message;
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

        public async Task<JsonResult> GetOptions()
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var result = await _totalService.GetOptions();
                res.Data = result.Data;
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            catch(Exception e)
            {
                res.Success = false;
                res.Message = e.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        //public async Task<JsonResult> GetCheck_All(string ur_id)
        //{
        //    ResponseViewModel res = new ResponseViewModel();

        //    try
        //    {
        //        var result = await _totalService.GetCheck_All_Item(ur_id);
        //        res.Data = result.Data;
        //        res.Success = result.Success;
        //        res.Message = result.Message;
        //        res.HttpStatusCode = System.Net.HttpStatusCode.OK;
        //    }
        //    catch (Exception e)
        //    {
        //        res.Success = false;
        //        res.Message = e.Message;
        //        res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
        //    }
        //    res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        //    return Json(res, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public async Task<JsonResult> SubmitCheck_All(Check_AllViewModel model)
        //{
        //    ResponseViewModel res = new ResponseViewModel();

        //    try
        //    {
        //        var result = await _totalService.SubmitCheck_All(model);
        //        res.Success = result.Success;
        //        res.Message = result.Message;
        //        res.HttpStatusCode = System.Net.HttpStatusCode.OK;
        //    }
        //    catch (Exception e)
        //    {
        //        res.Success = false;
        //        res.Message = e.Message;
        //        res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
        //    }
        //    res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        //    return Json(res, JsonRequestBehavior.DenyGet);
        //}
    }
}