using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountManagement : MonoBehaviour
{
    [SerializeField] private GameObject AccountPage;

    public void OnClick()
    {
        AccountPage.SetActive(true);
    }


    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
