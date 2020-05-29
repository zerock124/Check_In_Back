using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.CheckRecord
{
    public class CheckRecordViewModel
    {
        /// <summary>
        /// 打卡記錄編號
        /// </summary>
        public int ci_sn { get; set; }
        /// <summary>
        /// 使用者ID
        /// </summary>
        public string ur_id { get; set; }
        /// <summary>
        /// 打卡日期
        /// </summary>
        public DateTime ci_da { get; set; }
        /// <summary>
        /// 上班時間
        /// </summary>
        public TimeSpan ci_ut { get; set; }
        /// <summary>
        /// 下班時間
        /// </summary>
        public TimeSpan ci_dt { get; set; }
        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string ur_ac { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime ci_ct { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 上班時數
        /// </summary>
        public double? ci_hr { get; set; }
    }

    public class UpdateCheckTimeViewModel
    {
        /// <summary>
        /// 上班時間
        /// </summary>
        public string ci_ut { get; set; }
        /// <summary>
        /// 下班時間
        /// </summary>
        public string ci_dt { get; set; }
    }

    public class SearchCheckModel
    {
        /// <summary>
        /// 搜尋ID
        /// </summary>
        public string ur_id { get; set; }
        /// <summary>
        /// 起始時間
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime EndDateTime { get; set; }
        /// <summary>
        /// 搜尋字串
        /// </summary>
        public string Query { get; set; }
    }

    public class SearchTimeModel
    {
        /// <summary>
        /// 搜尋起始間
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// 搜尋結束時間
        /// </summary>
        public DateTime EndDateTime { get; set; }
    }

    public class CheckRecordExportViewModel
    {
        public string ur_id { get; set; }
        public string ur_ac { get; set; }
        public List<CheckRecordViewModel> checkRecordTestViewModel { get; set; }
    }
}
