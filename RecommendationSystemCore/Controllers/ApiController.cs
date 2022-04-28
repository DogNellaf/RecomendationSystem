using Microsoft.AspNetCore.Mvc;
using RecommendationSystem.Models;
using RecommendationSystem.Core.Helpers;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RecommendationSystem.Controllers
{
    // API контроллер 
    public class ApiController : Controller
    {

        #region API Methods

        [HttpGet] // главная страница
        public IActionResult Index()
        {
            // возвращаем статус ОК
            Response.StatusCode = (int)HttpStatusCode.OK;

            // выводим стартовое сообщение
            return Json(new { Message = "Its starting page. Use GET/POST requests to use data" });
        }

        [HttpGet] // любой нестандартный путь + ошибки, путь /api/error
        public IActionResult Error()
        {
            // возвращаем статус Плохой запрос
            Response.StatusCode = (int)HttpStatusCode.BadRequest;

            // сообщаем, что произошла ошибка
            return Json(new { ErrorMessage = "Bad Request" });
        }

        [HttpGet] // получить список пользователей, путь /api/users
        public IActionResult Users() => Ok<User>();

        [HttpGet] // получить список отзывов по продукту /api/userbyreview/?review_id=<type_id>
        public IActionResult UserByReview(string review_id)
        {
            var review = Database.GetObject<Review>($"[id] = {review_id}").FirstOrDefault();
            if (review is not null)
            {
                var user = Database.GetJson<User>($"[id] = {review.AuthorId}");
                return Ok(user);
            }
            return Redirect("api/error");
        }

        [HttpGet] // получить список типов продуктов, путь /api/types
        public IActionResult Types() => Ok<Type>();

        [HttpGet] // получить список продуктов по типу /api/itemsbytype/?type_id=<type_id>
        public IActionResult ItemsByType(string type_id) => Ok<Item>($"[type_id] = {type_id}");

        [HttpGet] // получить список всех продуктов, путь /api/items
        public IActionResult Items() => Ok<Item>();

        [HttpGet] // получить список всех отзывов, путь /api/reviews
        public IActionResult Reviews() => Ok<Review>();

        [HttpGet] // получить список отзывов по продукту /api/reviewsbyitem/?item_id=<type_id>
        public IActionResult ReviewsByItem(string item_id) => Ok<Review>($"[item_id] = {item_id}");

       
        [HttpGet] // получить пользователя по имени
        public IActionResult GetUserByName(string name) => Ok(Database.GetJson<User>($"[username] = '{name}'").Trim('[').Trim(']'));

        [HttpGet] // получить хэш пароля текущего пользователя
        public IActionResult GetUserHash(string user_id)
        {
            var users = Database.GetObject<User>();
            int userId = int.Parse(user_id);
            var user = users.Where(x => x.Id == userId).FirstOrDefault();
            return Ok(user.Password);
        }

        [HttpGet] // проверка на корректность хэша
        public IActionResult CheckUserHash(string user_id, string password)
        {
            try
            {
                var users = Database.GetObject<User>();
                int userId = int.Parse(user_id);
                var user = users.Where(x => x.Id == userId).FirstOrDefault();

                var hash = Encrypt(password);

                if (hash == user.Password)
                {
                    return Ok(user);
                }
                else
                {
                    return Ok(new User());
                }
            }
            catch
            {
                return Redirect("api/error");
            }
            
        }

        #endregion

        #region Utils

        // вспомогательный метод для сокращения
        private ActionResult Ok<T>(string where = "") where T: Model => Ok(Database.GetJson<T>(where));

        // функция формирования хэша
        public static string Encrypt(string value)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(value));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        #endregion
    }
}
