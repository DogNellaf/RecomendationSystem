using RecommendationSystem.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageController : MonoBehaviour
{
    [SerializeField] private GameObject itemPagePrefab;
    
    public void ShowItemInfo(Item Item)
    {
        var page = GameObject.Find("Page");
        page.GetComponent<PageController>().Clear();

        var itemPage = Instantiate(itemPagePrefab);
        var transform = itemPage.transform;

        FindComponent<TextMeshProUGUI>("ItemName", transform).text = Item.Name;
        FindComponent<TextMeshProUGUI>("ItemDescription", transform).text = Item.Description;
        FindComponent<ReviewShower>("ReviewsButton", transform).Item = Item;
        SetImage(Item.Photo, FindComponent<Image>("ItemImage", transform)); 

        itemPage.transform.SetParent(gameObject.transform, false);
    }

    private void SetImage(string url, Image image) => StartCoroutine(Database.Find().GetImageByName(url, image));

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

    private T FindComponent<T>(string childName, Transform transform) where T : Component => transform.Find(childName).GetComponentInChildren<T>();
}
