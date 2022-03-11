using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    private List<GameObject> child = new List<GameObject>();

    public void Clear() => child.Clear();
}
