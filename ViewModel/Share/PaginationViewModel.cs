using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Share
{
    public class PaginationViewModel
    {
        public PaginationViewModel()
        {
            this.PerPage = 10;
            this.CurrentPage = 1;
            this.TotalCounts = 0;
        }

        /// <summary>
        /// 每頁顯示數量
        /// </summary>
        public int PerPage { get; set; }
        /// <summary>
        /// 當前頁數
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 總計數量
        /// </summary>
        public int TotalCounts { get; set; }
        /// <summary>
        /// 總頁數(自動計算)
        /// </summary>
        public int TotalPage
        {
            get => TotalCounts > 0 ? (int)Math.Ceiling((double)TotalCounts / PerPage) : 1;
        }

        public int GetSkipLength() => (this.CurrentPage - 1) * this.PerPage;
    }
}
