using RecommendationSystem.Models;
using UnityEngine;
using TMPro;

public class LeftFrameGenerator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private void Start()
    {
        var database = MenuInteractions.Current.Database;
        Debug.Log("database");
        var types = database.Types;
        Debug.Log("types");
        var position = 550;
        foreach (Type type in types)
        {
            GameObject frame = Instantiate(prefab);
            frame.transform.SetParent(gameObject.transform);
            frame.transform.localPosition = new Vector3(0, position, 0);
            frame.GetComponentInChildren<TextMeshProUGUI>().text = type.Name;
            frame.GetComponentInChildren<TypeButtonClick>().typeId = type.Id;
            position -= 200;
        }
    }
}
