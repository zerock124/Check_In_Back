using Check_InDB.Interface;
using Check_InDB.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using ViewModels.Share;
using ViewModels.Verity;

namespace Check_In.ControllersApi
{
    public class CheckTotalRecordController : ApiController
    {
        protected ITotalRecordService _totalRecord;

        public CheckTotalRecordController()
        {
            _totalRecord = new TotalRecordService();
        }

        // POST: api/CheckTotalRecord
        public async Task<IHttpActionResult> Get(string StartDateTime)
        {
            VerityResult result = new VerityResult();

            try
            {
                SearchTime modelTime = new SearchTime
                {
                    StartDateTime = DateTime.Parse(StartDateTime),
                };

                var totalRecord = await _totalRecord.CheckTotalRecord(modelTime);
                result.Success = true;
                result.Message = "更新彙總紀錄成功";
                return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.OK, result));
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = JsonConvert.SerializeObject(ex);
                return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.InternalServerError, result));
                throw;
            }
        }
    }
}
