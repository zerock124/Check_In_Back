using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Share
{
    public class UserSearchModel
    {
        /// <summary>
        /// 搜尋類別
        /// </summary>
        public int SearchEnum { get; set; }
        /// <summary>
        /// 起始時間
        /// </summary>
        public DateTime? StartDateTime { get; set; }
        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime? EndDateTime { get; set; }
        /// <summary>
        /// 搜尋字串
        /// </summary>
        public string Query { get; set; }
    }

    public class SearchModel
    {
        /// <summary>
        /// 搜尋類別
        /// </summary>
        public int SearchEnum { get; set; }
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

    public class SearchTime
    {
        /// <summary>
        /// 起始時間
        /// </summary>
        public DateTime StartDateTime { get; set; }
    }
}
