using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Verity
{
    /// <summary>
    /// 驗證結果
    /// </summary>
    public class VerityResult
    {
        /// <summary>
        /// 結果
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; } = string.Empty;

    }
}
