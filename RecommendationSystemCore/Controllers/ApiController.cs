using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecommendationSystem.Core.Helpers;
using RecommendationSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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
        public IActionResult Error(string message)
        {
            // возвращаем статус Плохой запрос
            Response.StatusCode = (int)HttpStatusCode.BadRequest;

            // сообщаем, что произошла ошибка
            return Json(new { ErrorMessage = message });
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
            return Redirect("api/error?message=user doesn't exists");
        }

        [HttpGet] // получить список типов продуктов, путь /api/types
        public IActionResult Types() => Ok<Models.Type>();

        [HttpGet] // получить список продуктов по типу /api/itemsbytype/?type_id=<type_id>
        public IActionResult ItemsByType(string type_id) => Ok<Item>($"[type_id] = {type_id}");

        [HttpGet] // получить список всех продуктов, путь /api/items
        public IActionResult Items() => Ok<Item>();

        [HttpGet] // получить список всех отзывов, путь /api/reviews
        public IActionResult Reviews() => Ok<Review>();

        [HttpGet] // получить список отзывов по продукту /api/reviewsbyitem/?item_id=<type_id>
        public IActionResult ReviewsByItem(string item_id) => Ok<Review>($"[item_id] = {item_id}");


        [HttpGet] // получить пользователя по имени /api/getuserbyname?name=<имя>
        public IActionResult GetUserByName(string name) => Ok<User>($"[username] = '{name}'");

        [HttpGet] // получить хэш пароля текущего пользователя /api/getuserhash?user_id=<id>
        public IActionResult GetUserHash(string user_id)
        {
            var users = Database.GetObject<User>();
            int userId = int.Parse(user_id);
            var user = users.Where(x => x.Id == userId).FirstOrDefault();
            return Ok(user.Password);
        }

        [HttpGet("api/createuser")]
        public IActionResult CreateUser(string username, string password)
        {
            try
            {
                var users = Database.GetObject<User>();
                var names = users.Select(x => x.Name);
                if (!names.Contains(username))
                {
                    Database.AddUser(username, Encrypt(password));
                    return Json(new { Message = "User added" });
                }
                else
                {
                    return Json(new { Message = "User already exists" });
                }
            }
            catch (Exception ex)
            {
                return Redirect($"api/error?message={ex.Message}");
            }
        }

        [HttpGet] // проверка на корректность хэша /api/checkuserhash?user_id=<id>&password=<password>
        public IActionResult CheckUserHash(string user_id, string password)
        {
            try
            {
                var users = Database.GetObject<User>();
                int userId = int.Parse(user_id);
                var user = users.Where(x => x.Id == userId).FirstOrDefault();

                var hash = Encrypt(password);
                var result = new List<User>();

                if (hash == user.Password)
                {
                    result.Add(user);
                }
                else
                {
                    result.Add(new User());
                    
                }
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                return Redirect($"api/error?message={ex.Message}");
            }
            
        }

        [HttpPost] // загрузка аватарки /api/sendavatar
        [Route("api/sendavatar")]
        public IActionResult SendAvatar(IFormFile files, string id)
        {
            var path = Environment.CurrentDirectory;
            try
            {
                if (files.Length > 0)
                {
                    string staticPath = $"{path}/static";

                    if (!Directory.Exists(staticPath))
                    {
                        Directory.CreateDirectory(staticPath);
                    }

                    string filePath = $"{staticPath}/{files.FileName}";

                    if (System.IO.File.Exists(filePath))
                        return Ok($"File uplodaded");

                    using (FileStream fileStream = System.IO.File.Create(filePath))
                    {
                        files.CopyTo(fileStream);
                        fileStream.Flush();
                    }

                    Database.UploadAvatar(files.FileName, int.Parse(id));

                    Response.StatusCode = 200;
                    return Ok($"File uplodaded");
                }
                else
                {
                    Response.StatusCode = 422;
                    return Redirect("api/error?message=no one files");
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Redirect($"api/error?message={ex.Message}");
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
