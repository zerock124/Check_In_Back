using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.TotalRecord
{
    public class Check_AllViewModel
    {
        /// <summary>
        /// 彙總編號
        /// </summary>
        public int ca_sn { get; set; }
        /// <summary>
        /// 帳號ID
        /// </summary>
        public string ur_id { get; set; }
        /// <summary>
        /// 彙總年份
        /// </summary>
        public int ca_ya { get; set; }
        /// <summary>
        /// 彙總月份
        /// </summary>
        public int ca_mo { get; set; }
        /// <summary>
        /// 工作天數
        /// </summary>
        public int ca_dy { get; set; }
        /// <summary>
        /// 出勤天數
        /// </summary>
        public int ca_on { get; set; }
        /// <summary>
        /// 工作時數
        /// </summary>
        public double ca_hr { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        public string ur_ac { get; set; }
        /// <summary>
        /// 平均工時
        /// </summary>
        public double ca_hr_avg { get; set; }
    }

    public class OptionsViewModel
    {
        public Options[] YearOptions { get; set; }
        public Options[] MonthOptions { get; set; }
    }

    public class Options
    {
        public int value { get; set; }
        public string text { get; set; }
    }
}
