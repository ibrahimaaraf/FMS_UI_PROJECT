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
    public class AccountController : BaseController
    {
        #region [//////// chats of account /////////]
        public ActionResult ChartofAccounts()
        {
            string token = GetCookie("AuthToken");

            // Check if the token is not null or empty
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");

            }

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GET_ALL_COA()
        {
            bool result = false;
            List<COA_List> coaList = new List<COA_List>();
            string msg= "no data found.";
            var options = new RestClientOptions(base_url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);

            var request = new RestRequest("api/get-all-coa", Method.Get);

            string token = GetCookie("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return Json(new { has_data = result, message = "Invalid Token.", list="" }, JsonRequestBehavior.AllowGet);
            }
            request.AddHeader("Authorization", $"Bearer {token}");
            try
            {
                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

                    if (jsonResponse?.has_data == true)
                    {
                        coaList = JsonConvert.DeserializeObject<List<COA_List>>(jsonResponse.list.ToString());
                        result = true;
                        msg = "data retrived.";
                    }
                }
                return Json(new { has_data = result, message = msg,list=coaList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { has_data = false, message = "An error occurred while attempting Registration.", error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> COA_POSTING(int ACC_NO, int ACC_TYPE, string ACC_DESCRIPTION)
        {
            bool result = false;
            string msg = "No data found.";
            int USER_ID = Convert.ToInt32(GET_USER_INFO("user_id"));
            var options = new RestClientOptions(base_url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("api/coa-posting", Method.Post);

            string token = GetCookie("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return Json(new { has_data = result, message = "Invalid Token."}, JsonRequestBehavior.AllowGet);
            }

            request.AddHeader("Authorization", $"Bearer {token}");

            request.AddParameter("ACC_NO", ACC_NO, ParameterType.QueryString);
            request.AddParameter("ACC_TYPE", ACC_TYPE, ParameterType.QueryString);
            request.AddParameter("ACC_DESCRIPTION", ACC_DESCRIPTION, ParameterType.QueryString);
            request.AddParameter("USER_ID", USER_ID, ParameterType.QueryString);

            try
            {
                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

                    if (jsonResponse?.result == 1)
                    {
                        result = true;
                        msg = jsonResponse?.message ?? "Inserted successfully.";
                    }
                    else if (jsonResponse?.result == 2)
                    {
                        result = false;
                        msg = jsonResponse?.message ?? "Already exists.";
                    }
                    else
                    {
                        result = false;
                        msg = jsonResponse?.message ?? "Insertion failed.";
                    }
                }
                else
                {
                    msg = "Failed to retrieve data.";
                }

                return Json(new { has_data = result, message = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { has_data = false, message = "An error occurred while coa posting.", error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> UPDATE_COA(int COA_ID, int ACC_NO, int ACC_TYPE, string ACC_DESCRIPTION)
        {
            bool result = false;
            string msg = "No data found.";
            int USER_ID = Convert.ToInt32(GET_USER_INFO("user_id"));
            var options = new RestClientOptions(base_url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("api/coa-update", Method.Put);

            string token = GetCookie("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return Json(new { has_data = result, message = "Invalid Token." }, JsonRequestBehavior.AllowGet);
            }

            request.AddHeader("Authorization", $"Bearer {token}");

            request.AddParameter("COA_ID", COA_ID, ParameterType.QueryString);
            request.AddParameter("ACC_NO", ACC_NO, ParameterType.QueryString);
            request.AddParameter("ACC_TYPE", ACC_TYPE, ParameterType.QueryString);
            request.AddParameter("ACC_DESCRIPTION", ACC_DESCRIPTION, ParameterType.QueryString);
            request.AddParameter("USER_ID", USER_ID, ParameterType.QueryString);

            //request.AddParameter("COA_ID", COA_ID);
            //request.AddParameter("ACC_NO", ACC_NO);
            //request.AddParameter("ACC_TYPE", ACC_TYPE);
            //request.AddParameter("ACC_DESCRIPTION", ACC_DESCRIPTION);
            //request.AddParameter("USER_ID", USER_ID);

            try
            {
                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

                    if (jsonResponse?.has_data == 1)
                    {
                        result = true;
                        
                    }
                    msg = jsonResponse?.message;
                }
                else
                {
                    msg = "Failed to retrieve data.";
                }

                return Json(new { has_data = result, message = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { has_data = false, message = "An error occurred while updating coa.", error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> DELETE_COA(int COA_ID)
        {
            bool result = false;
            string msg = "No data found.";
            int USER_ID = Convert.ToInt32(GET_USER_INFO("user_id"));
            var options = new RestClientOptions(base_url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("api/coa-delete", Method.Delete);

            string token = GetCookie("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return Json(new { has_data = result, message = "Invalid Token." }, JsonRequestBehavior.AllowGet);
            }

            request.AddHeader("Authorization", $"Bearer {token}");

            request.AddParameter("COA_ID", COA_ID, ParameterType.QueryString);
            try
            {
                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

                    if (jsonResponse?.has_data == 1)
                    {
                        result = true;

                    }
                    msg = jsonResponse?.message;
                }
                else
                {
                    msg = "Failed to retrieve data.";
                }

                return Json(new { has_data = result, message = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { has_data = false, message = "An error occurred while updating coa.", error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region [////////// Journals ////////]

        public ActionResult journals()
        {
            string token = GetCookie("AuthToken");

            // Check if the token is not null or empty
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");

            }

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GET_ALL_JOURNALS()
        {
            bool result = false;
            List<Journal_List> journal_list = new List<Journal_List>();
            List<COA_D> coa_list = new List<COA_D>();
            string msg = "no data found.";
            var options = new RestClientOptions(base_url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);

            var request = new RestRequest("api/get-all-journal", Method.Get);

            string token = GetCookie("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return Json(new { has_data = result, message = "Invalid Token.", list = "" }, JsonRequestBehavior.AllowGet);
            }
            request.AddHeader("Authorization", $"Bearer {token}");
            try
            {
                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

                    if (jsonResponse?.has_data == true)
                    {
                        journal_list = JsonConvert.DeserializeObject<List<Journal_List>>(jsonResponse.list.ToString());
                        coa_list = JsonConvert.DeserializeObject<List<COA_D>>(jsonResponse.coa_list.ToString());
                        result = true;
                        msg = "data retrived.";
                    }
                }
                return Json(new { has_data = result, message = msg, list = journal_list, coa_list= coa_list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { has_data = false, message = "An error occurred while attempting Registration.", error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> JOURNAL_POSTING(int ACC_NO, int ACC_TYPE, int COA_ID, DateTime DATE, float AMOUNT)
        {
            bool result = false;
            string msg = "No data found.";
            int USER_ID = Convert.ToInt32(GET_USER_INFO("user_id"));
            var options = new RestClientOptions(base_url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("api/journal-posting", Method.Post);

            string token = GetCookie("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return Json(new { has_data = result, message = "Invalid Token." }, JsonRequestBehavior.AllowGet);
            }

            request.AddHeader("Authorization", $"Bearer {token}");

            request.AddParameter("ACC_NO", ACC_NO, ParameterType.QueryString);
            request.AddParameter("ACC_TYPE", ACC_TYPE, ParameterType.QueryString);
            request.AddParameter("COA_ID", COA_ID, ParameterType.QueryString);
            request.AddParameter("DATE", DATE, ParameterType.QueryString);
            request.AddParameter("AMOUNT", AMOUNT, ParameterType.QueryString);
            request.AddParameter("USER_ID", USER_ID, ParameterType.QueryString);

            try
            {
                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

                    if (jsonResponse?.result == 1)
                    {
                        result = true;
                        msg = jsonResponse?.message ?? "Inserted successfully.";
                    }
                    else if (jsonResponse?.result == 2)
                    {
                        result = false;
                        msg = jsonResponse?.message ?? "Already exists.";
                    }
                    else
                    {
                        result = false;
                        msg = jsonResponse?.message ?? "Insertion failed.";
                    }
                }
                else
                {
                    msg = "Failed to retrieve data.";
                }

                return Json(new { has_data = result, message = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { has_data = false, message = "An error occurred while coa posting.", error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> UPDATE_JOURNAL(int JOURNAL_ID, int ACC_TYPE, int COA_ID, DateTime DATE, float AMOUNT)
        {
            bool result = false;
            string msg = "No data found.";
            int USER_ID = Convert.ToInt32(GET_USER_INFO("user_id"));
            var options = new RestClientOptions(base_url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("api/journal-update", Method.Put);

            string token = GetCookie("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return Json(new { has_data = result, message = "Invalid Token." }, JsonRequestBehavior.AllowGet);
            }

            request.AddHeader("Authorization", $"Bearer {token}");

            request.AddParameter("JOURNAL_ID", JOURNAL_ID, ParameterType.QueryString);
            request.AddParameter("ACC_TYPE", ACC_TYPE, ParameterType.QueryString);
            request.AddParameter("COA_ID", COA_ID, ParameterType.QueryString);
            request.AddParameter("DATE", DATE, ParameterType.QueryString);
            request.AddParameter("AMOUNT", AMOUNT, ParameterType.QueryString);
            request.AddParameter("USER_ID", USER_ID, ParameterType.QueryString);

            try
            {
                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

                    if (jsonResponse?.has_data == 1)
                    {
                        result = true;

                    }
                    msg = jsonResponse?.message;
                }
                else
                {
                    msg = "Failed to retrieve data.";
                }

                return Json(new { has_data = result, message = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { has_data = false, message = "An error occurred while updating coa.", error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> DELETE_JOURNAL(int JOURNAL_ID)
        {
            bool result = false;
            string msg = "No data found.";
            int USER_ID = Convert.ToInt32(GET_USER_INFO("user_id"));
            var options = new RestClientOptions(base_url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("api/journal-delete", Method.Put);

            string token = GetCookie("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return Json(new { has_data = result, message = "Invalid Token." }, JsonRequestBehavior.AllowGet);
            }

            request.AddHeader("Authorization", $"Bearer {token}");

            request.AddParameter("JOURNAL_ID", JOURNAL_ID, ParameterType.QueryString);
            request.AddParameter("USER_ID", USER_ID, ParameterType.QueryString);

            try
            {
                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

                    if (jsonResponse?.has_data == 1)
                    {
                        result = true;

                    }
                    msg = jsonResponse?.message;
                }
                else
                {
                    msg = "Failed to retrieve data.";
                }

                return Json(new { has_data = result, message = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { has_data = false, message = "An error occurred while updating coa.", error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}