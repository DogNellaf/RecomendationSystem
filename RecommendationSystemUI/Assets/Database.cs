using Newtonsoft.Json;
using RecommendationSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Database: MonoBehaviour
{

    // ����� �������
    [SerializeField] private string Host = "localhost:44381";

    // ��������� �� ������
    //[SerializeField] private bool isConnected = true;

    // �������� �� �������
    [SerializeField] private bool isDebug = true;

    // ��������, ��������� ��� ���������� �����������
    [SerializeField] private GameObject noConnectionFrame;

    // ������ �������������
    public List<User> Users => GetObjects<User>("users");

    // ������ ����� ���������
    public List<RecommendationSystem.Models.Type> Types => GetObjects<RecommendationSystem.Models.Type>("types");

    // ������ ��������� �� ����
    public List<Item> GetItemsByType(int typeId) => GetObjects<Item>($"itemsbytype/?type_id={typeId}");

    // ������ ������� �� ��������
    public List<Review> GetReviewsByItem(Item item) => GetObjects<Review>($"reviewsbyitem/?item_id={item.Id}");

    // ��������� ������ ������
    public User GetUserByReview(int reviewId) => GetObject<User>($"userbyreview/?review_id={reviewId}");

    // ��������� ���� ������ ������������ �� ��� id
    public string GetUserHash(int user_id) => GetRequest($"getuserhash?user_id={user_id}");

    #region Utils

    // ��������� �������� � ������� �� ��������
    public IEnumerator GetImageByName(string name, Image image)
    {
        // ���� �� ��������� ������
        string path = $"{Host}/static/{name}";

        // ���������� ������ �� ���������� �����
        UnityWebRequest request = UnityWebRequest.Get(path);
        DownloadHandlerTexture downloader = new();
        request.downloadHandler = downloader;

        // ���� ������� ��������
        Log($"������� �������� ����� �� ���� {path}");

        // ���� �������� ��������
        yield return request.SendWebRequest();

        // �������� ����������� ��������
        var texture = downloader.texture;

        // �������� � � ���������
        image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

        Log($"�������� {texture.name} ������� ���������");
    }

    // ��������� json ��� ����� � �������
    private string GetRequest(string path)
    {
        try
        {
            string allPath = $"{Host}/api/{path}";

            Log($"��������� ������ �� ����: {allPath}");

            // ������ ������
            HttpWebRequest proxy_request = (HttpWebRequest)WebRequest.Create(allPath);
            proxy_request.Method = "GET";
            proxy_request.ContentType = @"text/html; charset=windows-1251";
            proxy_request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.0.249.89 Safari/532.5";
            proxy_request.KeepAlive = true;

            // �������� �����
            HttpWebResponse resp = proxy_request.GetResponse() as HttpWebResponse;
            string text = "";
            using (StreamReader sr = new(resp.GetResponseStream(), Encoding.Default))
                text = sr.ReadToEnd();
            text = text.Trim(new char[] { '"' });

            // ��������, ��� ���������� ����
            //isConnected = true;

            // ��������� ������ ���������� ����������
            SwitchFrame(true);

            // ���������� json
            return text;
        }
        catch (Exception ex)
        {
            // �������� ������ ���������� ����������
            SwitchFrame(false);

            // ������ ������

            throw new Exception($"�� ������� ������� �� ���� '{path}', �������: '{ex.Message}'");
        }
    }

    // ������� ����������� ������ �������� �� json
    public List<T> GetObjects<T>(string query) where T : Model
    {
        try
        {
            // ������ ������ �� ������ � �������� �����
            string text = GetRequest(query);

            // ������������ json � ������������ ������� �����
            var raw = JsonConvert.DeserializeObject<dynamic>(text);

            // ������� ������-������ ���������
            List<T> result = new();

            // ���������� �� ������� ���� json 
            foreach (var data in raw.Children())
            {
                var parameters = new object[1];
                parameters[0] = data;

                // ������� ��������� �������
                result.Add((T)Activator.CreateInstance(typeof(T), parameters));
            }
            
            // ���������� ���������
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"�� ������� ������� {typeof(T).Name}, ������� {ex.Message}");
        }
    }

    // ������� ��������� ������ ������� �� json
    public T GetObject<T>(string query) where T : Model
    {
        try
        {
            // ������ ������
            string text = GetRequest(query);

            // ������������ json � ������������ ������� �����
            var raw = JsonConvert.DeserializeObject<dynamic>(text);
            var parameters = new object[1];
            parameters[0] = raw.Last;

            // ������� ��������� �������
            return (T)Activator.CreateInstance(typeof(T), parameters);
        }
        catch (Exception ex)
        {
            throw new Exception($"�� ������� ������� {typeof(T).Name}, ������� {ex.Message}");
        }
    }

    // ������� �������� ��������
    public void SetImage(string url, Image image) => StartCoroutine(GetImageByName(url, image));

    // ������� �������� �����������
    public void UploadTexture(int id) => StartCoroutine(UploadTextureRoutine(id));

    // ��������� ��� �������� �����������
    private IEnumerator UploadTextureRoutine(int id)
    {
        string path = EditorUtility.OpenFilePanel("Load new avatar", "", "png");
        if (path.Length != 0)
        {
            WWW www = new($@"file:/{path}");
            while (!www.isDone)
                yield return null;
            Texture2D avatar = www.texture;
            var bytes = avatar.EncodeToJPG();
            List<IMultipartFormSection> form = new();
            form.Add(new MultipartFormFileSection("files", bytes, "test.jpeg", "image/jpeg"));
            form.Add(new MultipartFormDataSection("id", $"{id}"));

            using (var unityWebRequest = UnityWebRequest.Post($"{Host}/api/sendavatar", form))
            {
                yield return unityWebRequest.SendWebRequest();

                if (unityWebRequest.result != UnityWebRequest.Result.Success)
                {
                    print($"Failed to upload {avatar.name}: {unityWebRequest.result} - {unityWebRequest.error}");
                }
                else
                {
                    print($"Finished Uploading {avatar.name}");
                }
            }
        }
    }
    // ������� ������������ ��������� ���������� ������ ���������� �����������
    private void SwitchFrame(bool isConnected) => noConnectionFrame.SetActive(!isConnected);

    // ������� �����������
    private void Log(string text)
    {
        if (isDebug)
        {
            Debug.Log($"[LOG|{DateTime.Now:HH:mm:ss}] {text}");
        }
    }
    #endregion
}
