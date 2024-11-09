using Microsoft.AspNetCore.Mvc;
using PracticeWeb.Classes;
using PracticeWeb.Classes.LocalClasses;
using PracticeWeb.Interfaces;
using Refit;
using System.Net;

namespace PracticeWeb.Controllers
{
    public class StoreAdministratorController : Controller
    {
        private readonly ILogger<StoreAdministratorController> logger;
        private readonly IUserClient client;
        private readonly IHttpContextAccessor httpContextAccessor;

        public StoreAdministratorController(ILogger<StoreAdministratorController> logger, IUserClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ActionResult<List<Product>>> Index()
        {
            var token = httpContextAccessor.HttpContext.Request.Cookies["Authorization"];

            var result = await client.GetAllProduct(token);

            return View(result);
        }

        public async Task<IActionResult> Edit(int Id, string Name, string Description, string Image, double Price, String Category, int CountInStock)
        {
            return View();
        }

        public async Task<IActionResult> EditSave(int Id, string Name, string Description, string Image, double Price, String Category, int CountInStock)
        {
            if (Name == "" || Description == "" || Image == "" || Category == "" || Name == null || Description == null || Image == null || Category == null || CountInStock == null || Price == null)
            {
                TempData["DanderMessage"] = "Заполните все поля";
            }
        
            else
            {
                var token = httpContextAccessor.HttpContext.Request.Cookies["Authorization"];

                await client.EditProduct(token, Id, Name, Description, Image, Price, Category, CountInStock);
            }
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> CreateSave(string Name, string Description, string Image, double Price, String Category, int CountInStock)
        {
            if (Name == "" || Description == "" || Image == "" || Category == "" || Name == null || Description == null || Image == null || Category == null || CountInStock == null || Price == null)
            {
                TempData["DanderMessage"] = "Заполните все поля";
            }
            else
            {
                var token = httpContextAccessor.HttpContext.Request.Cookies["Authorization"];

                await client.CreateProduct(token, Name, Description, Image, Price, Category, CountInStock);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var token = httpContextAccessor.HttpContext.Request.Cookies["Authorization"];

            logger.LogInformation(Id.ToString());

            await client.DeleteProduct(token, Id);

            return RedirectToAction("Index");
        }
    }
}
