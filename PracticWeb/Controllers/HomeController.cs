using Microsoft.AspNetCore.Mvc;
using PracticeWeb.Classes;
using PracticeWeb.Models;
using System.Diagnostics;

using Refit;
using PracticeWeb.Interfaces;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using PracticeWeb.Classes.LocalClasses;

namespace PracticeWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IUserClient client;

        public HomeController(ILogger<HomeController> logger, IUserClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<IActionResult> Index()
        {
            var result = await client.GetProduct();

            return View(result);
        }

        public async Task<IActionResult> AddToCart(Product product)
        {
            List<CartClass> cookieList = new List<CartClass>();

            if (HttpContext.Request.Cookies.ContainsKey("CartCookie"))
            {
                var cookieValues = HttpContext.Request.Cookies["CartCookie"];
                cookieList = JsonConvert.DeserializeObject<List<CartClass>>(cookieValues);
            }

            var Element = cookieList.Find(x => x.Id == product.Id);

            if (product.CountInStock <= 0)
            {
                TempData["DangerMessage"] = "Товар закончился";
            }
            else if (Element == null)
            {
                cookieList.Add(new CartClass()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Image = product.Image,
                    Price = product.Price,
                    CountInStock = product.CountInStock,
                    resultPrice = product.Price,
                    Count = 1
                });
            }
            
            string serializedList = JsonConvert.SerializeObject(cookieList);

            HttpContext.Response.Cookies.Append("CartCookie", serializedList);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}