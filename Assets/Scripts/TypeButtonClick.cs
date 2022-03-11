using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeButtonClick : MonoBehaviour
{
    [SerializeField] public int typeId;
    public void OnClick()
    {
        var items = Database.GetItemsByType(typeId);
    }
}
