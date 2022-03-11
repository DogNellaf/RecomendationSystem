using Microsoft.AspNetCore.Mvc;
using RecomendationSystemClasses;
using RecommendationSystemCore.Helpers;
using System.Net;

namespace RecommendationSystemCore.Controllers
{
    // API контроллер 
    public class ApiController : Controller
    {
        [HttpGet] // главная страница
        public IActionResult Index()
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { Message = "Its starting page. Use GET/POST requests to use dat" });
        }

        [HttpGet] // любой нестандартный путь + ошибки, путь /api/error
        public IActionResult Error()
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { ErrorMessage = "Bad Request" });
        }

        [HttpGet] // получить список пользователей, путь /api/users
        public IActionResult Users() => Ok<User>();

        [HttpGet] // получить список типов продуктов, путь /api/types
        public IActionResult Types() => Ok<Type>();

        [HttpGet] // получить список продуктов по типу /api/itemsbytype/?type_id=<type_id>
        public IActionResult ItemsByType(string type_id) => Ok<Item>($"[type_id] = {type_id}");

        [HttpGet] // получить список всех продуктов, путь /api/items
        public IActionResult Items() => Ok<Item>();

        [HttpGet] // получить список всех отзывов, путь /api/reviews
        public IActionResult Reviews() => Ok<Review>();

        // вспомогательный метод для сокращения
        private ActionResult Ok<T>(string where = "") where T: Model => Ok(Database.GetJson<T>(where));
    }
}
