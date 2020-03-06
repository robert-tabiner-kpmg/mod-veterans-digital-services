using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Common.Constants;
using Forms.Core.Forms;
using Forms.Core.Models.InFlight;
using Forms.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Forms.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Forms.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IFormService _formService;
        private readonly IStaticFormProvider _staticFormProvider;

        public HomeController(IFormService formService, IStaticFormProvider staticFormProvider)
        {
            _formService = formService;
            _staticFormProvider = staticFormProvider;
        }

        public IActionResult Index(string form)
        {
            if (!Enum.TryParse(form, true, out FormType formType)) return View("QuestionnaireNotRecognised");
             
            AddCookie(FormConstants.RequestedFormCookieName, form);
            
            var startPage = _staticFormProvider.GetStartPage(formType);
            ViewData[FormConstants.ViewDataFormTitle] = _staticFormProvider.GetFormName(formType);

            return View(startPage);
        }
        
        [HttpGet]
        public async Task<IActionResult> SkipAuthentication(CancellationToken cancellationToken)
        {
            var fakeUserId = Guid.NewGuid().ToString();
            
            Enum.TryParse(GetCookie(FormConstants.RequestedFormCookieName), true, out FormType formType);
            var questionnaireKey = $"{fakeUserId}-{formType.ToString()}";
            AddCookie(FormConstants.InFlightFormCookieName, questionnaireKey);

            await IssueAuthenticationCookie(fakeUserId);
            
            DeleteCookie(FormConstants.RequestedFormCookieName);
            
            var form = await _formService.InitialiseForm(questionnaireKey, formType);
            
            AddCookie(FormConstants.InFlightFormCookieName, questionnaireKey);

            var node = form.GetGraphNode(Key.ForDecisionFormRouter());

            return RedirectToAction("Node", "Form", new {nodeId = node.Key.Value });
        }

        [HttpGet("[action]/{code}")]
        public new IActionResult StatusCode(int code)
        {
            return code switch
            {
                404 => (IActionResult) View("NotFound"),
                _ => RedirectToAction("Error")
            };
        }

        [HttpGet("[action]")]
        public IActionResult FormNotRecognised()
        {
            return View("QuestionnaireNotRecognised");
        }

        [HttpGet("[action]")]
        public IActionResult Error()
        {
            return View("ProblemWithService");
        }
        
        private async Task IssueAuthenticationCookie(string userId)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-3.1
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userId),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);
        }
    }
}