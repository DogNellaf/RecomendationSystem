using Newtonsoft.Json;
using RecommendationSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Database: MonoBehaviour
{

    // адрес сервера
    [SerializeField] private string localHost = @"http://localhost:44381";

    [SerializeField] private string internetHost = @"http://u1674941.plsk.regruhosting.ru";

    [SerializeField] private bool isLocal = false;

    private string host = "";

    private void Start()
    {
        if (isLocal)
        {
            host = localHost;
        }
        else
        {
            host = internetHost;
        }
    }

    // подключен ли клиент
    //[SerializeField] private bool isConnected = true;

    // включена ли отладка
    [SerializeField] private bool isDebug = true;

    // страница, выводимая при отсутствии подключения
    [SerializeField] private GameObject noConnectionFrame;

    // список пользователей
    public List<User> Users => GetObjects<User>("users");

    // список типов продуктов
    public List<RecommendationSystem.Models.Type> Types => GetObjects<RecommendationSystem.Models.Type>("types");

    // список продуктов по типу
    public List<Item> GetItemsByType(int typeId) => GetObjects<Item>($"itemsbytype/?type_id={typeId}");

    // список продуктов из рекомендации по пользователю
    public List<Item> GetItemsByUser(int userId) => GetObjects<Item>($"getrecommend?user_id={userId}");

    // список отзывов по продукту
    public List<Review> GetReviewsByItem(Item item) => GetObjects<Review>($"reviewsbyitem/?item_id={item.Id}");

    // получение автора отзыва
    public User GetUserByReview(int reviewId) => GetObject<User>($"userbyreview/?review_id={reviewId}");

    // получение хэша пароля пользователя по его id
    public string GetUserHash(int user_id) => GetRequest($"getuserhash?user_id={user_id}");

    #region Utils

    // получение картинки с сервера по названию
    public IEnumerator GetImageByName(string name, Image image)
    {
        // путь до статичных файлов
        string path = $"{host}/static/{name}";

        // отправляем запрос на скачивание файла
        UnityWebRequest request = UnityWebRequest.Get(path);
        DownloadHandlerTexture downloader = new();
        request.downloadHandler = downloader;
        request.certificateHandler = new ForceAcceptAll();

        // если отладка включена
        Log($"Попытка загрузки файла по пути {path}");

        request.SendWebRequest();

        // ждем загрузки картинки
        while (!request.isDone)
            yield return null;

        // получаем загруженную картинку
        var texture = downloader.texture;

        // передаем её в интерфейс
        image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

        Log($"Картинка {texture.name} успешно загружена");
    }

    // получение json как ответ с сервера
    private string GetRequest(string path, string method = "GET")
    {
        try
        {
            string finalPath = $"{host}/api/{path}";

            Log($"Отправлен запрос по пути: {finalPath}");

            // кидаем запрос
            var request = WebRequest.Create(finalPath) as HttpWebRequest;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            request.ServerCertificateValidationCallback = delegate { return true; };
            request.Method = method;
            request.ContentType = @"text/html; charset=windows-1251";
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.0.249.89 Safari/532.5";
            request.Timeout = 5000;

            // получаем ответ
            HttpWebResponse resp = request.GetResponse() as HttpWebResponse;
            string text = "";
            using (StreamReader sr = new(resp.GetResponseStream(), Encoding.Default))
                text = sr.ReadToEnd();
            text = text.Trim(new char[] { '"' });

            // отмечаем, что соединение есть
            //isConnected = true;

            // отключаем панель отсутствия соединения
            SwitchFrame(true);

            // вовзращаем json
            return text;
        }
        catch (Exception ex)
        {
            // включаем панель отсутствия соединения
            SwitchFrame(false);

            // кидаем ошибку

            throw new Exception($"Не удалось считать по пути '{path}', причина: '{ex.Message}'");
        }
    }

    // функция конвертации списка объектов из json
    public List<T> GetObjects<T>(string query) where T : Model
    {
        try
        {
            // кидаем запрос на сервер и получаем ответ
            string text = GetRequest(query);

            // конвертируем json в динамический словарь узлов
            var raw = JsonConvert.DeserializeObject<dynamic>(text);

            // создаем пустой-список результат
            List<T> result = new();

            // проходимся по каждому узлу json 
            foreach (var data in raw.Children())
            {
                var parameters = new object[1];
                parameters[0] = data;

                // создаем экземпляр объекта
                result.Add((T)Activator.CreateInstance(typeof(T), parameters));
            }
            
            // возвращаем результат
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"Не удалось считать {typeof(T).Name}, причина {ex.Message}");
        }
    }

    // функция получения одного объекта из json
    public T GetObject<T>(string query) where T : Model
    {
        try
        {
            // кидаем запрос
            string text = GetRequest(query);

            // конвертируем json в динамический словарь узлов
            var raw = JsonConvert.DeserializeObject<dynamic>(text);
            var parameters = new object[1];
            parameters[0] = raw.Last;

            // создаем экземпляр объекта
            return (T)Activator.CreateInstance(typeof(T), parameters);
        }
        catch (Exception ex)
        {
            throw new Exception($"Не удалось считать {typeof(T).Name}, причина {ex.Message}");
        }
    }

    // функция загрузки картинки
    public void SetImage(string url, Image image) => StartCoroutine(GetImageByName(url, image));

    // функция отправки изображения
    public void UploadTexture(int id) => StartCoroutine(UploadTextureRoutine(id));

    // функция создания пользователя
    public void AddUser(string username, string password) 
    {
        string allPath = $"createuser?username={username}&password={password}";
        Log($"Отправлен запрос по пути: {allPath}");
        GetRequest(allPath);
    } 

    // корунтина для отправки изображения
    private IEnumerator UploadTextureRoutine(int id)
    {
        Texture2D texture = null;
        string filePath = "";
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            filePath = path;

            if (isDebug)
                Debug.Log("Image path: " + path);

            if (path != null)
            {
                // Create Texture from selected image
                texture = NativeGallery.LoadImageAtPath(path, -1, false);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }
            }
        });

        if (texture != null)
        {
            var bytes = texture.EncodeToJPG();
            List<IMultipartFormSection> form = new();
            form.Add(new MultipartFormFileSection("files", bytes, "test.jpeg", "image/jpeg"));
            form.Add(new MultipartFormDataSection("id", $"{id}"));

            using (var unityWebRequest = UnityWebRequest.Post($"{host}/api/sendavatar", form))
            {
                var cert = new ForceAcceptAll();
                unityWebRequest.certificateHandler = cert;
                yield return unityWebRequest.SendWebRequest();

                if (unityWebRequest.result != UnityWebRequest.Result.Success)
                {
                    print($"Failed to upload {texture.name}: {unityWebRequest.result} - {unityWebRequest.error}");
                }
                else
                {
                    print($"Finished Uploading {texture.name}");
                }
            }
        }
    }
    // функция переключения состояния активности панели отсутствия подключения
    private void SwitchFrame(bool isConnected) => noConnectionFrame.SetActive(!isConnected);

    // функция логирования
    private void Log(string text)
    {
        if (isDebug)
        {
            Debug.Log($"[LOG|{DateTime.Now:HH:mm:ss}] {text}");
        }
    }
    #endregion

    public class ForceAcceptAll : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}
