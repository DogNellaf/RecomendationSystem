using RecommendationSystem.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageController : MonoBehaviour
{
    [SerializeField] private GameObject itemPagePrefab;

    private MenuInteractions Menu;

    private void Start()
    {
        Menu = MenuInteractions.Current;
    }

    public void ShowItemInfo(Item Item)
    {
        var itemPage = ShowPage(itemPagePrefab);
        var transform = itemPage.transform;

        FindComponent<TextMeshProUGUI>("ItemName", transform).text = Item.Name;
        FindComponent<TextMeshProUGUI>("ItemDescription", transform).text = Item.Description;
        FindComponent<ReviewShower>("ReviewsButton", transform).Item = Item;
        SetImage(Item.Photo, FindComponent<Image>("ItemImage", transform)); 
    }

    public GameObject ShowPage(GameObject etalon)
    {
        Clear();
        var newPage = Instantiate(etalon);
        newPage.transform.SetParent(gameObject.transform, false);
        return newPage;
    }

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

    #region Utils

    private T FindComponent<T>(string childName, Transform transform) where T : Component => transform.Find(childName).GetComponentInChildren<T>();

    private void SetImage(string url, Image image) => StartCoroutine(Menu.Database.GetImageByName(url, image));

    #endregion


}
