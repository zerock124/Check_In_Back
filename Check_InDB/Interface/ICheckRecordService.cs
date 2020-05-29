using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.CheckRecord;
using ViewModels.Share;
using ViewModels.Verity;

namespace Check_InDB.Interface
{
    public interface ICheckRecordService
    {
        /// <summary>
        /// 取得打卡列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<ResWithPaginationViewModel> GetCheckRecord(SearchCheckModel model, PaginationViewModel pagination);
        /// <summary>
        /// 取得使用者列表
        /// </summary>
        /// <returns></returns>
        Task<ResponseViewModel> GetUserList();
        /// <summary>
        /// 取得打卡時間
        /// </summary>
        /// <param name="ci_sn"></param>
        /// <returns></returns>
        Task<ResponseViewModel> GetClick_In(int ci_sn);
        /// <summary>
        /// 邊更打卡時間
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<VerityResult> SubmitClick_In(CheckRecordViewModel model);
        /// <summary>
        /// 取得匯出Excel資料
        /// </summary>
        /// <param name="SearchTime"></param>
        /// <returns></returns>
        Task<ResponseViewModel> GetCheckRecordExcel(SearchTimeModel SearchTime);
        /// <summary>
        /// check_in匯出Excel
        /// </summary>
        /// <param name="xlsList"></param>
        /// <param name="wk"></param>
        /// <param name="sht"></param>
        /// <param name="Title"></param>
        void ExportExcel(List<CheckRecordViewModel> xlsList, XSSFWorkbook wk, ISheet sht, string Title);
    }
}
