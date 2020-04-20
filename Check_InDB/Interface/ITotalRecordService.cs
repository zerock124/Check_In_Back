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
        Task<ResponseViewModel> GetTotalRecordList(SearchModel searchModel);
        
        Task<VerityResult> CheckTotalRecord(SearchTime searchModel);

        Task<ResponseViewModel> GetOptions();

        //Task<ResponseViewModel> GetCheck_All_Item(string ur_id);

        //Task<VerityResult> SubmitCheck_All(Check_AllViewModel model);
    }
}
