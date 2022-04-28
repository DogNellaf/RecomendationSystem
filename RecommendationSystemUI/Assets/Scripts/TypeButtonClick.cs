using RecommendationSystem.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeButtonClick : MonoBehaviour
{
    [SerializeField] public int typeId;
    [SerializeField] private GameObject itemButtonPrefab;

    private void Start()
    {
    }

    public void OnClick()
    {
        var menu = MenuInteractions.Current;
        var items = menu.Database.GetItemsByType(typeId);
        var page = menu.Page;

        MenuInteractions.Current.ClosePanel(transform.parent.gameObject);
        
        var position = 750;

        menu.PageController.Clear();
        foreach (Item item in items)
        {
            GameObject frame = Instantiate(itemButtonPrefab);
            frame.transform.SetParent(page.transform, false);
            frame.transform.localPosition = new Vector3(0, position, 0);
            frame.name = item.Name;
            frame.GetComponentInChildren<TextMeshProUGUI>().text = item.Name;
            frame.GetComponent<Button>().onClick.AddListener(delegate { page.GetComponent<PageController>().ShowItemInfo(item); });
            position -= 200;
        }
    }
}
