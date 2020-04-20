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
        Task<ResWithPaginationViewModel> GetCheckRecord(SearchCheckModel model, PaginationViewModel pagination);

        Task<ResponseViewModel> GetUserList();

        Task<ResponseViewModel> GetClick_In(int ci_sn);

        Task<VerityResult> SubmitClick_In(CheckRecordViewModel model);

        Task<ResponseViewModel> GetCheckRecordExcel(SearchTimeModel SearchTime);

        void ExportExcel(List<CheckRecordViewModel> xlsList, XSSFWorkbook wk, ISheet sht, string Title);
    }
}
