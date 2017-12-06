using APIService.Model;
using BusinessLogic;
using BusinessLogic.Event;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 設備API(IM)
    /// </summary>
    public class DevicesController : ApiController
    {
        /// <summary>
        /// 設備清單取得
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDevices()
        {
            try
            {
                //User Info
                var login = GenericAPIService.GetUserInfo();

                //查詢參數
                var opt = new QueryOption { User = true, Plan = new QueryPlan { Join = "Detail" } };

                var bll = GenericBusinessFactory.CreateInstance<MemberDevice>();
                var output = bll.GetList(opt, login, null);

                return Ok(output);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }

        /// <summary>
        /// 設備維修
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Repair(Log log)
        {
            try
            {
                //User Info
                var login = GenericAPIService.GetUserInfo();
                log.USERID = login.USERID;

                //紀錄動作處理物件
                var bll = new EventBusinessLogic();

                //對應設備編號
                string device = bll.GetDeviceByID(log);

                if (!string.IsNullOrEmpty(device))
                {
                    //對應設備編號擴充
                    log.DEVICE_SN = device;
                    //紀錄處理
                    var deviceLog = bll.LogModify(log);
                    //詳細記錄資訊取得
                    var detail = bll.GetLogDetail(deviceLog.LOG_SN);

                    //Slack訊息推送結果
                    var slackResponse = await bll.PushSlack(log.ACTION_TYPE, detail);
                    //IM訊息推送結果
                    //var imResponse = await bll.PushIM(log.ACTION_TYPE, detail);

                    if (slackResponse != HttpStatusCode.OK)
                        return Content(slackResponse, new APIResponse("Log紀錄成功，但推送至Slack未獲得授權"));

                    //if(imResponse != HttpStatusCode.OK)
                    //    return Content(slackResponse, new APIResponse("Log紀錄成功，但推送至IM時失敗"));

                    return Ok();
                }
                else
                {
                    return Content(HttpStatusCode.Forbidden, new APIResponse("無對應設備，或對應設備狀態不符"));
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }
    }
}