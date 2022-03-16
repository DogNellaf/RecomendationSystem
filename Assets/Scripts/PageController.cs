using RecomendationSystemClasses;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PageController : MonoBehaviour
{
    [SerializeField] private GameObject itemPagePrefab;
    
    public void ShowItemInfo(Item Item)
    {
        var page = GameObject.Find("Page");
        page.GetComponent<PageController>().Clear();
        var itemPage = Instantiate(itemPagePrefab);

        var image = itemPage.transform.Find("ItemImage").GetComponent<Image>();
        StartCoroutine(SetImage(Item.Photo, image));

        itemPage.transform.Find("ItemName").GetComponentInChildren<TextMeshProUGUI>().text = Item.Name;
        itemPage.transform.Find("ItemDescription").GetComponentInChildren<TextMeshProUGUI>().text = Item.Description;
        AddChild(itemPage);
    }

    private IEnumerator SetImage(string url, Image image)
    {
        UnityWebRequest request = new UnityWebRequest(url);
        yield return request;

        Debug.Log("Why on earh is this never called?");
        image.material.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }

    public void AddChild(GameObject child) => child.transform.SetParent(gameObject.transform, false);

    public void Clear()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.gameObject != gameObject)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
