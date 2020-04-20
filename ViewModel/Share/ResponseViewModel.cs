using System;
using System.Net;
using ViewModels.Verity;

namespace ViewModels.Share
{
    /// <summary>
    /// 回覆JSON統一格式
    /// </summary>
    public class ResponseViewModel : VerityResult
    {
        /// <summary>
        /// HttpStatusCode 狀態碼
        /// </summary>
        public HttpStatusCode HttpStatusCode { set; get; }

        /// <summary>
        /// SEVER回覆時間
        /// </summary>
        public string ResponseTime { get; set; }

        /// <summary>
        /// 例外訊息
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 傳回資料
        /// </summary>
        public object Data { set; get; }
    }

    /// <summary>
    /// 回覆JSON統一格式
    /// </summary>
    public class ResponseViewModel<T> : VerityResult
    {
        /// <summary>
        /// HttpStatusCode 狀態碼
        /// </summary>
        public HttpStatusCode HttpStatusCode { set; get; }

        /// <summary>
        /// SEVER回覆時間
        /// </summary>
        public string ResponseTime { get; set; }

        /// <summary>
        /// 例外訊息
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 傳回資料
        /// </summary>
        public T Data { set; get; }
    }
}
