using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Share;
using ViewModels.TotalRecord;
using ViewModels.Verity;

namespace Check_InDB.Interface
{
    public interface ITotalRecordService
    {
        /// <summary>
        /// 取得彙總紀錄列表
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<ResponseViewModel> GetTotalRecordList(SearchModel searchModel);
        /// <summary>
        /// 更新彙整記錄
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<VerityResult> CheckTotalRecord(SearchTime searchModel);
        /// <summary>
        /// 取得日期選項
        /// </summary>
        /// <returns></returns>
        Task<ResponseViewModel> GetOptions();

        //Task<ResponseViewModel> GetCheck_All_Item(string ur_id);

        //Task<VerityResult> SubmitCheck_All(Check_AllViewModel model);
    }
}
