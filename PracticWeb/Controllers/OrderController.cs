using Microsoft.AspNetCore.Mvc;
using PracticeWeb.Classes.LocalClasses;
using PracticeWeb.Interfaces;

namespace PracticeWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> logger;
        private readonly IUserClient client;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderController(ILogger<OrderController> logger, IUserClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.client = client;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ActionResult<List<OrderClass>>> Index()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["Authorization"];

            var result = await client.GetOrder(token);

            return View(result);
        }
    }
}
