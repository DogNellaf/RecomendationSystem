using Newtonsoft.Json;
using RecommendationSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Database: MonoBehaviour
{

    [SerializeField] private string Host = "localhost:44381";

    [SerializeField] private bool isConnected = true;

    [SerializeField] private GameObject noConnectionFrame;

    public List<User> Users => GetObjects<User>("users");

    public List<RecommendationSystem.Models.Type> Types => GetObjects<RecommendationSystem.Models.Type>("types");

    public List<Item> GetItemsByType(int typeId) => GetObjects<Item>($"itemsbytype/?type_id={typeId}");

    public List<Review> GetReviewsByItem(Item item) => GetObjects<Review>($"reviewsbyitem/?item_id={item.Id}");

    #region utils

    public IEnumerator GetImageByName(string name, Image image)
    {
        string path = $"{Host}/static/{name}";

        UnityWebRequest request = UnityWebRequest.Get(path);
        DownloadHandlerTexture downloader = new();
        request.downloadHandler = downloader;
        Debug.Log($"Попытка загрузки файла по пути {path}");
        yield return request.SendWebRequest();
        var texture = downloader.texture;
        image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    private string GetRequest(string path)
    {
        try
        {
            HttpWebRequest proxy_request = (HttpWebRequest)WebRequest.Create($"{Host}/api/{path}");
            proxy_request.Method = "GET";
            proxy_request.ContentType = @"text/html; charset=windows-1251";
            proxy_request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.0.249.89 Safari/532.5";
            proxy_request.KeepAlive = true;
            HttpWebResponse resp = proxy_request.GetResponse() as HttpWebResponse;
            string text = "";
            using (StreamReader sr = new(resp.GetResponseStream(), Encoding.Default))
                text = sr.ReadToEnd();

            text = text.Trim(new char[] { '"' });
            isConnected = true;
            SwitchFrame();
            return text;
        }
        catch
        {
            isConnected = false;
            SwitchFrame();
            return string.Empty;
        }
    }

    public List<T> GetObjects<T>(string query) where T : Model
    {
        try
        {
            string text = GetRequest(query);
            var raw = JsonConvert.DeserializeObject<dynamic>(text);
            List<T> result = new();
            foreach (var data in raw.Children())
            {
                var parameters = new object[1];
                parameters[0] = data;
                result.Add((T)Activator.CreateInstance(typeof(T), parameters));
            }
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"Не удалось считать {typeof(T).Name}, причина {ex.Message}");
        }
    }

    private void SwitchFrame()
    {
        noConnectionFrame.SetActive(!isConnected);
    }

    #endregion
}
