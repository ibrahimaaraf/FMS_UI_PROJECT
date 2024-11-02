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
    public class UserController : BaseController
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SignIn(string email, string password)
        {
            var options = new RestClientOptions(base_url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);

            var request = new RestRequest("api/login", Method.Post);

            request.AddParameter("email", email, ParameterType.QueryString);
            request.AddParameter("password", password, ParameterType.QueryString);

            string token = GetCookie("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                request.AddHeader("Authorization", $"Bearer {token}");
            }

            try
            {
                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

                    if (jsonResponse?.has_data == true)
                    {
                        token = jsonResponse.token;

                        SetCookie("AuthToken", token.ToString(), 1);
                        SetCookie("ui", jsonResponse.list.ToString(), 1);

                        return Json(new { has_data = true, message = jsonResponse.message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { has_data = false, message = "Login failed." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { has_data = false, message = "Login failed.", error = response.ErrorMessage }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { has_data = false, message = "An error occurred while attempting to log in.", error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Registration(string USER_NAME,string FULL_NAME, string EMAIL, string USER_ID,string PHONE,string PASSWORD,int STATUS)
        {
            bool result = false;
            var options = new RestClientOptions(base_url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);

            var request = new RestRequest("api/create-user", Method.Post);
            
            var userPayload = new
            {
                USER_NAME = USER_NAME,
                FULL_NAME = FULL_NAME,
                EMAIL = EMAIL,
                USER_ID = USER_ID,
                PHONE = PHONE,
                PASSWORD = PASSWORD,
                STATUS = STATUS
            };

            // Add the payload as JSON to the request body
            request.AddJsonBody(userPayload);

            //string token = GetCookie("AuthToken");
            //if (string.IsNullOrEmpty(token))
            //{
            //    return Json(new { has_data = result, message = "Registration failed." }, JsonRequestBehavior.AllowGet);

            //}
            //request.AddHeader("Authorization", $"Bearer {token}");

            try
            {
                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

                    if (jsonResponse?.has_data == true)
                    {
                        result = true;
                    }
                }
                return Json(new { has_data = result, message = "Registration failed." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { has_data = false, message = "An error occurred while attempting Registration.", error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}