using Check_InDB.Interface;
using Check_InDB.Service;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModels.CheckRecord;
using ViewModels.Excel;
using ViewModels.Share;

namespace Check_In.Controllers
{
    public class CheckRecordController : BaseController
    {
        protected ICheckRecordService _checkRecord;
        protected ITotalRecordService _totalRecord;

        public CheckRecordController()
        {
            _checkRecord = new CheckRecordService();
            _totalRecord = new TotalRecordService();
        }

        // GET: Check
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 取得打卡紀錄
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetCheckRecord(SearchCheckModel model, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel res = new ResWithPaginationViewModel();

            try
            {
                var result = await _checkRecord.GetCheckRecord(model, pagination);
                res.Data = result.Data;
                res.Pagination = result.Pagination;
                res.Message = result.Message;
                res.Success = result.Success;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            catch
            {
                res.Message = "與伺服器連線發生錯誤";
                res.Success = false;
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 取得使用者列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetUserList()
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var result = await _checkRecord.GetUserList();
                res.Data = result.Data;
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                res.Exception = ex;
                res.Success = false;
                res.Message = "與伺服器連接發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 取得打卡紀錄的時間
        /// </summary>
        /// <param name="ci_sn"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetClick_In(int ci_sn)
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var result = await _checkRecord.GetClick_In(ci_sn);
                res.Data = result.Data;
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                res.Exception = ex;
                res.Success = false;
                res.Message = "與伺服器連接發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 編輯打卡紀錄的時間
        /// </summary>
        /// <param name="model"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task<JsonResult> SubmitClick_In(CheckRecordViewModel model, UpdateCheckTimeViewModel time)
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                if (time != null)
                {
                    if (time.ci_ut != "-")
                        model.ci_ut = TimeSpan.Parse(time.ci_ut);
                    if (time.ci_dt != "-")
                        model.ci_dt = TimeSpan.Parse(time.ci_dt);

                    var result = await _checkRecord.SubmitClick_In(model);

                    SearchTime modelTime = new SearchTime
                    {
                        StartDateTime = DateTime.Parse(model.ci_da.ToString("s")),
                    };

                    var totalRecord = _totalRecord.CheckTotalRecord(modelTime);

                    res.Success = result.Success;
                    res.Message = result.Message;
                    res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                res.Exception = ex;
                res.Success = false;
                res.Message = "與伺服器連接發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 匯入
        /// </summary>
        /// <param name="ExcelFile"></param>
        /// <param 時間="ExcelDate"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<JsonResult> ImportExcel(HttpPostedFileBase ExcelFile, string ExcelDate, string VehicleModelName)
        //{
        //    ResponseViewModel res = new ResponseViewModel();
        //    List<CheckRecordViewModel> excelviewmodel = new List<CheckRecordViewModel>();
        //    try
        //    {
        //        if (ExcelFile.ContentLength > 0)
        //        {
        //            var fileName = Path.GetFileName(ExcelFile.FileName);
        //            var fileExtension = Path.GetExtension(ExcelFile.FileName);
        //            string[] fileExtensions =
        //            {
        //                ".xls",
        //                ".xlsx"
        //            };

        //            #region 檢驗檔案格式
        //            if (fileExtensions.Contains(fileExtension))
        //            {
        //                var dirPath = Server.MapPath("~/FileUploads");
        //                var path = Path.Combine(dirPath, fileName);

        //                if (!System.IO.Directory.Exists(dirPath))
        //                    Directory.CreateDirectory(dirPath);

        //                ExcelFile.SaveAs(path);

        //                CheckRecordViewModel checkRecord = new CheckRecordViewModel();
        //                ImportResult importResult = new ImportResult();
        //                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
        //                {
        //                    FileUploadEntity fileUploadEntity = new FileUploadEntity();
        //                    fileUploadEntity.FileStram = fileStream;

        //                    if (fileExtension == ".xls")
        //                    {
        //                        //importResult = await _excelService.ImportDetail(fileUploadEntity, true, ExcelDate, CurrendUserid, VehicleModelName);
        //                        //excelviewmodel = importResult._bookingviewmodel;
        //                    }

        //                    else if (fileExtension == ".xlsx")
        //                    {
        //                        //fileUploadEntity.FileData = FileHelper.FileStreamToByteArrary(fileStream);
        //                        //fileStream.Position = 0;
        //                        //importResult = await _excelService.ImportDetail(fileUploadEntity, false, ExcelDate, CurrendUserid, VehicleModelName);
        //                        //excelviewmodel = importResult._bookingviewmodel;
        //                    }
        //                }

        //                if (importResult.Result)
        //                {
        //                    TempData["Message"] = "匯入成功!";
        //                    res.Data = excelviewmodel;
        //                    res.Success = true;
        //                    res.Message = "匯入Excel成功";
        //                }
        //                else
        //                {
        //                    string messages = string.Empty;
        //                    foreach (var item in importResult.ErrorList)
        //                    {
        //                        messages += "第" + item.RowNumber + "列[" + item.ColumnValue + "] : " + item.ErrorMessage + "<br>";
        //                    }

        //                    res.Success = false;
        //                    res.Message = "匯入Excel失敗" + "「" + messages + "」";
        //                }
        //            }
        //            else
        //                res.Success = false;
        //            res.Message = "請上傳xls或xlsx格式的檔案!";
        //            #endregion
        //        }
        //    }
        //    catch
        //    {
        //        res.Success = false;
        //        res.Message = "請確認上傳的Excel檔案是否正確";
        //    }

        //    res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        //    return Json(res, JsonRequestBehavior.DenyGet);
        //}

        [HttpPost]
        public JsonResult GetExportCheckRecord(SearchTimeModel SearchTime)
        {
            ResponseViewModel res = new ResponseViewModel();
            try
            {
                res.Data = SearchTime;
                res.Success = true;
                res.Message = "打卡紀錄匯出Excel成功";
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                res.Exception = ex;
                res.Success = false;
                res.Message = "與伺服器連接發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 打卡紀錄列表轉Excel
        /// </summary>
        /// <param name="StartDateTime"></param>
        /// <param name="EndDateTime"></param>
        /// <returns></returns>
        public async Task<FileContentResult> ExportCheckRecord(string StartDateTime, string EndDateTime)
        {
            List<CheckRecordExportViewModel> xlsList = new List<CheckRecordExportViewModel>();

            SearchTimeModel SearchTime = new SearchTimeModel
            {
                StartDateTime = DateTime.Parse(StartDateTime),
                EndDateTime = DateTime.Parse(EndDateTime)
            };

            var result = await _checkRecord.GetCheckRecordExcel(SearchTime);
            var data = (List<CheckRecordExportViewModel>)result.Data;
            xlsList = data;

            ////-----------------------------------------------------------------------------------------
            var workbook = new XSSFWorkbook();
            ISheet sheet;
            MemoryStream output = new MemoryStream();   //Write the workbook to a memory stream

            foreach (var item in xlsList)
            {
                sheet = workbook.CreateSheet(item.ur_ac);
                _checkRecord.ExportExcel(item.checkRecordTestViewModel, workbook, sheet, item.ur_ac);
            }

            string fileName = string.Format("打卡紀錄-{0}.xlsx", string.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now));
            workbook.Write(output);
            ////---------------------------------------- Return the result to the end user

            return File(output.ToArray(),           //The binary data of the XLS file
                "application/vnd.ms-excel",         //MIME type of Excel files
                fileName);                          //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }
    }
}