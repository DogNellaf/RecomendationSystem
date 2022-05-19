using RecommendationSystem.Models;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeButtonClick : MonoBehaviour
{
    [SerializeField] public int typeId;
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private bool itemsFromRecommendation = false;
    [SerializeField] private List<Item> items = new();
    [SerializeField] private MenuInteractions menu;

    private void Start()
    {
        menu = MenuInteractions.Current;
        if (!itemsFromRecommendation)
        {
            items = menu.Database.GetItemsByType(typeId);
        }
        else
        {
            var user = menu.AccountController.User;
            if (user == null)
            {
                Debug.LogError("ѕопытка получить рекомендации без авторизации");
            }
            else
            {
                items = menu.Database.GetItemsByUser(user.Id);
            }
        }
    }

    public void OnClick()
    {
        var page = menu.Page;

        
        
        var position = 750;

        if (!itemsFromRecommendation)
            MenuInteractions.Current.ClosePanel(transform.parent.gameObject);
        menu.PageController.Clear();
        foreach (Item item in items)
        {
            GameObject frame = Instantiate(itemButtonPrefab);
            frame.transform.SetParent(page.transform, false);
            frame.transform.localPosition = new Vector3(0, position, 0);
            frame.transform.localScale = new Vector3(1.9f, 1.9f, 1);
            frame.name = item.Name;
            frame.GetComponentInChildren<TextMeshProUGUI>().text = item.Name;
            frame.GetComponent<Button>().onClick.AddListener(delegate { page.GetComponent<PageController>().ShowItemInfo(item); });
            position -= 200;
        }
    }
}
