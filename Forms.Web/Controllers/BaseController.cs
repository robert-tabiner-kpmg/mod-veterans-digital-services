using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forms.Web.Controllers
{
    public class BaseController : Controller
    {
        protected void AddCookie(string key, string value)
        {
            HttpContext.Response.Cookies.Append(
                key,
                value,
                new CookieOptions
                {
                    HttpOnly = true, 
                    Secure = true, 
                    Expires = DateTime.Today.AddDays(1),
                    SameSite = SameSiteMode.None
                }
            );
        }

        protected string GetCookie(string key)
        {
            var cookieValue = Request.Cookies[key];
             
            return cookieValue;
        }
         
        protected void DeleteCookie(params string[] keys)
        {
            foreach (var key in keys)
            {
                HttpContext.Response.Cookies.Delete(key);
            }
        }
    }
}