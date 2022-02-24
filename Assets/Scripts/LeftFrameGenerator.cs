using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFrameGenerator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private void Start()
    {
        var types = Database.Types;
        var position = 769;
        foreach (Type type in types)
        {
            GameObject frame = Instantiate(prefab);
            frame.transform.parent = gameObject.transform;
            frame.transform.position = new Vector3(0, position);
            position -= 150;
        }
    }
}
