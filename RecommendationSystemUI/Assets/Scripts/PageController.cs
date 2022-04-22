using RecommendationSystem.Models;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PageController : MonoBehaviour
{
    [SerializeField] private GameObject itemPagePrefab;
    [SerializeField] private Database database;
    
    public void ShowItemInfo(Item Item)
    {
        var page = GameObject.Find("Page");
        page.GetComponent<PageController>().Clear();

        var itemPage = Instantiate(itemPagePrefab);
        var transform = itemPage.transform;
        transform.Find("ItemName").GetComponentInChildren<TextMeshProUGUI>().text = Item.Name;
        transform.Find("ItemDescription").GetComponentInChildren<TextMeshProUGUI>().text = Item.Description;
        transform.Find("ReviewsButton").GetComponent<ReviewShower>().Item = Item;
        var image = transform.Find("ItemImage").GetComponent<Image>();
        SetImage(Item.Photo, image);

        itemPage.transform.SetParent(gameObject.transform, false);
    }

    private void SetImage(string url, Image image) => StartCoroutine(database.GetImageByName(url, image));

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
