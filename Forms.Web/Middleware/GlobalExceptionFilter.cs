using System;
using System.Collections.Generic;
using Forms.Core.Exceptions;
using Forms.Web.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Forms.Web.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case FormNotRecognisedException _:
                    context.Result = new RedirectToActionResult("FormNotRecognised", "Home", null);
                    return;
                case KeyNotFoundException _:
                case StaticFormNotFoundException _:
                    context.Result = new RedirectResult("~/StatusCode/404");
                    return;
                default:
                    context.Result = new RedirectResult("~/Error");
            
                    base.OnException(context);
                    break;
            }
        }
    }
}