using RecomendationSystemClasses;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;


public static class Database
{
    public static List<Type> Types => GetTypes();

    private static string Host = "https://localhost:44381/api";

    public static List<Item> GetItemsByType(int typeId)
    {
        string text = GetRequest($"itemsbytype/?type_id={typeId}");
        var elements = GetDictionaryByJson(text);
        List<Item> items = new List<Item>();
        foreach (var element in elements)
        {
            Item item = new Item(new object[]
            {
                int.Parse(element["Id"]),
                element["Name"],
                element["Photo"],
                element["Description"],
                int.Parse(element["TypeId"])
            });
            items.Add(item);
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

    private static List<Dictionary<string, string>> GetDictionaryByJson(string json)
    {
        var result = new List<Dictionary<string, string>>();
        var clearedJson = json.Replace("[", "").Replace("]", "");
        var rawItems = clearedJson.Replace("{","").Split('}');
        foreach (var item in rawItems)
        {
            if (item != string.Empty)
            {
                var itemValues = new Dictionary<string, string>();
                string key = string.Empty;
                for (int i = 0; i < item.Length; i++)
                {
                    var currentChar = item[i];
                    if (currentChar == '\"' && key == string.Empty)
                    {
                        var endIndex = item.IndexOf('\"', i + 1);
                        key = item.Substring(i + 1, endIndex-1-i);
                        
                        if (i < item.Length)
                        {
                            if (item[endIndex + 3] != '\"')
                            {
                                i = endIndex + 2;
                                continue;
                            }
                        }

                        i = endIndex + 3;
                    }
                    else if (key != string.Empty)
                    {
                        var endIndex = item.IndexOf("\",", i);

                        if (endIndex < 0)
                        {
                            endIndex = item.IndexOf(",\"", i);
                        }

                        if (endIndex < 0)
                        {
                            endIndex = item.IndexOf("\"", i);
                        }

                        if (endIndex < 0)
                        {
                            endIndex = item.IndexOf("}", i);
                        }

                        if (endIndex < 0)
                        {
                            endIndex = item.Length;
                        }

                        string value = item.Substring(i, endIndex - i);
                        itemValues.Add(key, value);
                        key = string.Empty;
                        i = endIndex;
                    }
                }
                result.Add(itemValues);
            }
        }
        return result;
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
        //Debug.Log(text);
        return text;
    }
}
