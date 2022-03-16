using RecomendationSystemClasses;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeButtonClick : MonoBehaviour
{
    [SerializeField] public int typeId;
    [SerializeField] private GameObject page;
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private MenuInteractions leftFrame;

    private void Start()
    {
        page = GameObject.Find("Page");
        leftFrame = GameObject.Find("Canvas").GetComponent<MenuInteractions>();
    }

    public void OnClick()
    {
        leftFrame.ClosePanel();
        var items = Database.GetItemsByType(typeId);
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
