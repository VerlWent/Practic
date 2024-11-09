using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using PracticeWeb.Classes;
using PracticeWeb.Classes.LocalClasses;
using PracticeWeb.Interfaces;
using Refit;
using System.Net;
using System.Net.Http.Headers;

namespace PracticeWeb.Controllers
{
	public class IdentificationController : Controller
	{
        private readonly ILogger<IdentificationController> logger;
        private readonly IUserClient client;
		private readonly IHttpContextAccessor httpContextAccessor;

        public IdentificationController(ILogger<IdentificationController> logger, IUserClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult AuthorizationView()
		{
			return View();
		}

		public IActionResult RegistrationView()
		{
			return View();
		}

        public async Task<IActionResult> Authorization(string Login, string Password)
		{
			try
			{
				//string result = "Bearer " + await client.GetUsers(Login, Password);

				var result = await client.GetUsers(Login, Password);

				string token = "Bearer " + result.Token;

				User user = result.User;

                Response.Cookies.Append("Authorization", token, new CookieOptions
				{
					HttpOnly = true,
					SameSite = SameSiteMode.Strict
				});
                
                logger.LogInformation("-" + token + "-");

				if (user.RoleId == 1)
				{
					return RedirectToAction("Index", "Home");
				}
				else if (user.RoleId == 2)
				{
					return RedirectToAction("Index", "StoreAdministrator");
				}
				else
				{
					return RedirectToAction("Index", "SystemAdministrator");
				}

				
			}
			catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
			{
				TempData["DanderMessage"] = "Неверные данные";
				return RedirectToAction("AuthorizationView", "Identification");
			}
		}


        public async Task<IActionResult> Registration(LocalUser localuser)
		{

			if (localuser.Login == null || localuser.Password == null || localuser.RepeatPassword == null || localuser.NickName == null)
			{
				TempData["DanderMessage"] = "Заполните все поля";
				return RedirectToAction("RegistrationView", "Identification");
			}

			if (localuser.Login == "" || localuser.Password == "" || localuser.RepeatPassword == "" || localuser.NickName == null)
			{
				TempData["DanderMessage"] = "Заполните все поля";
				return RedirectToAction("RegistrationView", "Identification");
			}

			if (localuser.Password != localuser.RepeatPassword)
			{
				TempData["DanderMessage"] = "Пароли не совпадают";
				return RedirectToAction("RegistrationView", "Identification");
			}

			User newuser = new User
			{
				NickName = localuser.NickName,
				Login = localuser.Login,
				Password = localuser.Password,
				RoleId = 1
			};

			try
			{
				await client.CreateUser(newuser);

				return RedirectToAction("AuthorizationView", "Identification");
			}
			catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
			{
				TempData["DanderMessage"] = "Логин уже занят";
				return RedirectToAction("RegistrationView", "Identification");
			}
		}

		public async Task<IActionResult> Logout()
		{
            Response.Cookies.Delete("Authorization");

            return RedirectToAction("Index", "Home");
        }
	}
}
