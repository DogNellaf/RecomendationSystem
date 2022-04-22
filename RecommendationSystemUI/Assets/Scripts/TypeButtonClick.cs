using RecommendationSystem.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeButtonClick : MonoBehaviour
{
    [SerializeField] public int typeId;
    [SerializeField] private GameObject page;
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private MenuInteractions leftFrame;
    [SerializeField] private Database database;

    private void Start()
    {
        page = GameObject.Find("Page");
        leftFrame = GameObject.Find("Canvas").GetComponent<MenuInteractions>();
        database = GameObject.Find("Database").GetComponent<Database>();
    }

    public void OnClick()
    {
        leftFrame.ClosePanel(transform.parent.gameObject);
        var items = database.GetItemsByType(typeId);
        var position = 750;
        page.GetComponent<PageController>().Clear();
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
