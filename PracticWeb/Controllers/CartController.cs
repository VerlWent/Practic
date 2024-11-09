using Microsoft.AspNetCore.Mvc;
using Refit;
using PracticeWeb.Interfaces;
using Newtonsoft.Json;
using PracticeWeb.Classes;
using PracticeWeb.Classes.LocalClasses;
using System.Collections.Generic;

namespace PracticeWeb.Controllers
{
	public class CartController : Controller
    {

        private readonly ILogger<CartController> logger;
        private readonly IUserClient client;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(ILogger<CartController> logger, IUserClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.client = client;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
		{
            List<CartClass> values = GetFromCookieList();

            return View(values);
        }

        public List<CartClass> GetFromCookieList()
        {
            List<CartClass> cookieList = new List<CartClass>();

            // Получаем текущий список из куки
            if (HttpContext.Request.Cookies.ContainsKey("CartCookie"))
            {
                var cookieValue = HttpContext.Request.Cookies["CartCookie"];
                cookieList = JsonConvert.DeserializeObject<List<CartClass>>(cookieValue);
            }

            double FullPrice = 0;

            foreach(var item in cookieList)
            {
                FullPrice += item.resultPrice;
            }

            TempData["FullPriceMessage"] = "Итог к оплате: " + FullPrice + "₽";

            return cookieList;
        }

        public async Task<IActionResult> ChangeCountPlus(CartClass product)
        {
            bool IsAvail = false;
            List<CartClass> cookieList = new List<CartClass>();

            if (HttpContext.Request.Cookies.ContainsKey("CartCookie"))
            {
                var cookieValues = HttpContext.Request.Cookies["CartCookie"];
                cookieList = JsonConvert.DeserializeObject<List<CartClass>>(cookieValues);
            }

            foreach (var item in cookieList)
            {
                if (item.Id == product.Id && item.CountInStock > item.Count)
                {
                    item.Count++;
                    item.resultPrice += item.Price;
                    IsAvail = true;
                    break;
                }
            }

            string serializedList = JsonConvert.SerializeObject(cookieList);

            HttpContext.Response.Cookies.Append("CartCookie", serializedList);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangeCountMinus(CartClass product)
        {
            bool IsAvail = false;
            List<CartClass> cookieList = new List<CartClass>();

            if (HttpContext.Request.Cookies.ContainsKey("CartCookie"))
            {
                var cookieValues = HttpContext.Request.Cookies["CartCookie"];
                cookieList = JsonConvert.DeserializeObject<List<CartClass>>(cookieValues);
            }

            foreach (var item in cookieList)
            {
                if (item.Id == product.Id && item.Count > 1)
                {
                    item.Count--;
                    item.resultPrice -= item.Price;
                    IsAvail = true;
                    break;
                }
            }

            string serializedList = JsonConvert.SerializeObject(cookieList);

            HttpContext.Response.Cookies.Append("CartCookie", serializedList);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteConfirmed(int productId)
        {
            logger.LogInformation("Удаление: " + productId.ToString());
            List<CartClass> cookieList = new List<CartClass>();

            if (HttpContext.Request.Cookies.ContainsKey("CartCookie"))
            {
                var cookieValues = HttpContext.Request.Cookies["CartCookie"];
                cookieList = JsonConvert.DeserializeObject<List<CartClass>>(cookieValues);

                // Удаляем все элементы с заданным Id
                cookieList.RemoveAll(item => item.Id == productId);

                string serializedList = JsonConvert.SerializeObject(cookieList);

                HttpContext.Response.Cookies.Append("CartCookie", serializedList);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BuyFullCart()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["Authorization"];

            List<CartClass> values = GetFromCookieList();

            List<NewOrderClass> orderlist = new List<NewOrderClass>();

            foreach (var item in values)
            {
                orderlist.Add(new NewOrderClass
                {
                    IdProduct = item.Id,
                    Count = item.Count
                });
            }

            await client.BuyFullCart(token, orderlist);

            return RedirectToAction("Index");
        }
    }
}
