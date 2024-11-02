using FMS_UI_PROJECT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FMS_UI_PROJECT.Controllers
{
    public class BaseController : Controller
    {
        public readonly string base_url="https://localhost:44304/";
        //public readonly string base_url= "http://192.168.0.155:8010/";

        #region [//////// Get User Info ////////]
        public string GET_USER_INFO(string type)
        {
            string user_info = string.Empty;
            string user_data = GetCookie("ui");

            if (!string.IsNullOrEmpty(user_data))
            {
                try
                {
                    string decodedJsonData = WebUtility.UrlDecode(user_data).Trim();

                    // Enclose in square brackets to form a valid JSON array
                    decodedJsonData = "[" + decodedJsonData + "]"; // Assumes a single user object

                    // Deserialize into List<User_Info>
                    List<User_Info> userList = JsonConvert.DeserializeObject<List<User_Info>>(decodedJsonData);

                    //var userList = JsonConvert.DeserializeObject<List<User_Info>>(user_data);

                    switch (type.ToLower())
                    {
                        case "user_id":
                            user_info = userList.FirstOrDefault()?.ID?.ToString();
                            break;
                        case "email":
                            user_info = userList.FirstOrDefault()?.EMAIL; 
                            break;
                        case "username":
                            user_info = userList.FirstOrDefault()?.USER_NAME;
                            break;
                        case "password":
                            user_info = userList.FirstOrDefault()?.PASSWORD;
                            break;
                        case "full_name":
                            user_info = userList.FirstOrDefault()?.FULL_NAME;
                            break;
                        case "emp_id":
                            user_info = userList.FirstOrDefault()?.USER_ID;
                            break;
                        case "phone":
                            user_info = userList.FirstOrDefault()?.PHONE;
                            break;
                        case "status":
                            user_info = userList.FirstOrDefault()?.STATUS?.ToString();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    user_info = $"Error reading user info: {ex.Message}";
                }
            }
            else
            {
                user_info = "User data not found";
            }

            return user_info;
        }

        #endregion

        #region [/////////// Cookies ///////////]

        public void SetCookie(string key, string value, int days)
        {
            // Create a new cookie
            HttpCookie cookie = new HttpCookie(key);
            cookie.Value = value;

            // Set the cookie's expiration date
            cookie.Expires = DateTime.Now.AddDays(days);

            // Add the cookie to the response
            Response.Cookies.Add(cookie);

        }
        public string GetCookie(string key)
        {
            string cookieValue = string.Empty;
            // Check if the cookie exists
            if (Request.Cookies[key] != null)
            {
                // Get the cookie's value
                cookieValue = Request.Cookies[key].Value;

            }
            return cookieValue;
        }
        public void DeleteCookie(string key)
        {
            // Check if the cookie exists
            if (Request.Cookies[key] != null)
            {
                // Expire the cookie by setting its expiration date to a date in the past
                Response.Cookies[key].Expires = DateTime.Now.AddDays(-1);
            }
        }

        protected void DeleteAllCookies()
        {
            // Get all existing cookies
            HttpCookieCollection cookies = Request.Cookies;


            foreach (string cookieName in cookies.AllKeys)
            {
                if (cookieName.Contains("_"))
                {
                    HttpCookie cookie = new HttpCookie(cookieName);
                    cookie.Expires = DateTime.Now.AddDays(-1); // Set an expired date
                    HttpContext.Response.Cookies.Add(cookie); // Add the expired cookie to the response
                }
            }
        }


        #endregion
    }
}