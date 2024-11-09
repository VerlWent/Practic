using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracticeWeb.Classes.LocalClasses;
using PracticeWeb.Interfaces;
using Refit;
using System.Net;

namespace PracticeWeb.Controllers
{
    public class SystemAdministratorController : Controller
    {
        private readonly ILogger<SystemAdministratorController> logger;
        private readonly IUserClient client;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SystemAdministratorController(ILogger<SystemAdministratorController> logger, IUserClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ActionResult<List<UserForSystemClass>>> Index()
        {
            var token = httpContextAccessor.HttpContext.Request.Cookies["Authorization"];

            var result = await client.GetAllUser(token);

            return View(result);
        }

        public async Task<IActionResult> Edit(int Id, string NickName, string Login, string Password, string Role)
        {
            return View();
        }
        public async Task<IActionResult> EditSave(int Id, string NickName, string Login, string Password, string Role)
        {
            if (NickName == "" || Login == "" || Password == "" || Role == "" || NickName == null || Login == null || Password == null || Role == null)
            {
                TempData["DanderMessage"] = "Заполните все поля";
            }
            else
            {
                var token = httpContextAccessor.HttpContext.Request.Cookies["Authorization"];

                await client.EditUser(token, Id, NickName, Login, Password, Role);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> CreateSave(string NickName, string Login, string Password, string Role)
        {
            if (NickName == "" || Login == "" || Password == "" || Role == "" || NickName == null || Login == null || Password == null || Role == null)
            {
                TempData["DanderMessage"] = "Заполните все поля";
            }
            else
            {
                var token = httpContextAccessor.HttpContext.Request.Cookies["Authorization"];

                await client.CreateUser(token, NickName, Login, Password, Role);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var token = httpContextAccessor.HttpContext.Request.Cookies["Authorization"];

            logger.LogInformation(Id.ToString());

            await client.DeleteUser(token, Id);

            return RedirectToAction("Index");
        }
    }
}
