using FMS_UI_PROJECT.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FMS_UI_PROJECT.Controllers
{
    public class ReportController : BaseController
    {
        public ActionResult ReportData()
        {
            string token = GetCookie("AuthToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");

            }
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GET_REPORT_DATA(DateTime ST_DATE, DateTime END_DATE)
        {
            bool result = false;
            List<FMS_Report> _list = new List<FMS_Report>();
            string msg = "no data found.";
            var options = new RestClientOptions(base_url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);

            var request = new RestRequest("api/report", Method.Get);

            string token = GetCookie("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return Json(new { has_data = result, message = "Invalid Token.", list = "" }, JsonRequestBehavior.AllowGet);
            }
            request.AddHeader("Authorization", $"Bearer {token}");

            request.AddParameter("ST_DATE", ST_DATE, ParameterType.QueryString);
            request.AddParameter("END_DATE", END_DATE, ParameterType.QueryString);
            try
            {
                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

                    if (jsonResponse?.has_data == true)
                    {

                        _list = JsonConvert.DeserializeObject<List<FMS_Report>>(jsonResponse.list.ToString());
                        Session["reports"] = _list;
                        result = true;
                        msg = "data retrived.";
                    }
                }
                return Json(new { has_data = result, message = msg, list=_list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { has_data = false, message = "An error occurred while attempting Registration.", error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}