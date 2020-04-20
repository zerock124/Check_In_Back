using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Verity;

namespace ViewModels.Verity
{
    /// <summary>
    /// 執行存檔後的回應
    /// </summary>
    public class SaveFileVerityResult : VerityResult
    {
        /// <summary>
        /// 檔案名稱
        /// </summary>
        public string FileName { get; set; }
    }
}
