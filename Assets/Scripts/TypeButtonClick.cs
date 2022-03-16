using RecomendationSystemClasses;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeButtonClick : MonoBehaviour
{
    [SerializeField] public int typeId;
    [SerializeField] private GameObject page;
    [SerializeField] private GameObject prefab;
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
        PageClear();
        foreach (Item item in items)
        {
            GameObject frame = Instantiate(prefab);
            frame.transform.SetParent(page.transform, false);
            frame.transform.localPosition = new Vector3(0, position, 0);
            frame.name = item.Name;
            frame.GetComponentInChildren<TextMeshProUGUI>().text = item.Name;
            position -= 200;
        }
    }

    private void PageClear()
    {
        Transform[] children = page.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.gameObject != page)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
