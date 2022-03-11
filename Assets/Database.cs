using Newtonsoft.Json;
using RecomendationSystemClasses;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
//using RecomendationSystemClasses;


public static class Database
{
    public static List<Type> Types => GetTypes();

    private static string Host = "https://localhost:44381/api";

    public static List<Item> GetItemsByType(int typeId)
    {
        string text = GetRequest($"itemsbytype/?type_id={typeId}").Replace("[", "").Replace("]", "");
        string[] elements = text.Split('{');
        List<Item> items = new List<Item>();
        for (int i = 1; i < elements.Length; i++)
        {
            var raw = elements[i].Replace("{", "").Replace("}", "").Split(',');

            //название
            string name = raw[0].Split(' ')[1].Trim('\"');
            string? description = raw[1].Split(' ')[1].Trim('\"');

            if (description == "null")
                description = null;

            int id = int.Parse(raw[2].Split(' ')[1]);

            //Item item = new Item(id, name, description);
            //items.Add(item);
        }

        return items;
    }

    private static List<Type> GetTypes()
    {
        string text = GetRequest("types").Replace("[","").Replace("]","");
        string[] elements = text.Split('{');
        List<Type> types = new List<Type>();
        for (int i = 1; i < elements.Length; i++)
        {
            var raw = elements[i].Replace("{","").Replace("}","").Split(',');

            //название
            string name = raw[0].Split(' ')[1].Trim('\"');
            string? description = raw[1].Split(' ')[1].Trim('\"');

            if (description == "null")
                description = null;

            int id = int.Parse(raw[2].Split(' ')[1]);

            Type type = new Type(id, name, description);
            types.Add(type);
        }

        return types;
    }

    private static string GetRequest(string path)
    {
        HttpWebRequest proxy_request = (HttpWebRequest)WebRequest.Create($"{Host}/{path}");
        proxy_request.Method = "GET";
        proxy_request.ContentType = @"text/html; charset=windows-1251";
        proxy_request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.0.249.89 Safari/532.5";
        proxy_request.KeepAlive = true;
        HttpWebResponse resp = proxy_request.GetResponse() as HttpWebResponse;
        string text = "";
        using (StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.Default))
            text = sr.ReadToEnd();

        text = text.Trim(new char[] { '"' });
        Debug.Log(text);
        return text;
    }
}
