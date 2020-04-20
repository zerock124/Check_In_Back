using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ViewModels.User
{
    public class UserViewModel
    {
        /// <summary>
        /// 帳號編號
        /// </summary>
        public int ur_sn { get; set; }
        /// <summary>
        /// 帳號ID
        /// </summary>
        public string ur_id { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        public string ur_ac { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        public string ur_pw { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime ur_ct { get; set; }
        /// <summary>
        /// 是否停用
        /// </summary>
        public bool? ur_is { get; set; }
        /// <summary>
        /// 帳號圖片
        /// </summary>
        public string ur_im { get; set; }
        public HttpPostedFileBase PhotoFile { get; set; }
    }

    public class CreateUserViewModel
    {
        public string ur_ac { get; set; }
        public string ur_pw { get; set; }
        public HttpPostedFileBase PhotoFile { get; set; }
        public string ur_im { get; set; }
    }
}
