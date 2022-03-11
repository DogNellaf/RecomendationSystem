using RecomendationSystemClasses;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeftFrameGenerator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private void Start()
    {
        var types = Database.Types;
        var position = 750;
        foreach (Type type in types)
        {
            GameObject frame = Instantiate(prefab);
            frame.transform.parent = gameObject.transform;
            frame.transform.localPosition = new Vector3(0, position, 0);
            frame.GetComponentInChildren<TextMeshProUGUI>().text = type.Name;
            frame.GetComponentInChildren<TypeButtonClick>().typeId = type.Id;
            position -= 200;
        }
    }
}
