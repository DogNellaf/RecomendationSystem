using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteractions : MonoBehaviour
{
    #region LeftFrameInteractions

    //левая выпадающая менюшка
    [SerializeField] private GameObject leftFrame;

    //логическая переменная для переключения состояния панели
    [SerializeField] private bool leftFrameIsOpened = false;

    //свойство для быстрого получения позиции по х для левой менюшки
    private float leftFrameXPosition => leftFrame.transform.position.x;

    //предотвращение дабл-клика
    private bool onButtonClicked = false;

    //открыть\закрыть менюшки
    public void OnLeftButtonClick()
    {
        //если левое меню не открыто
        if (!leftFrameIsOpened && !onButtonClicked)
        {
            onButtonClicked = true;
            StartCoroutine("MoveLeftFrameToRight");
        }

        //иначе если левое меню открыто
        else if (leftFrameIsOpened && !onButtonClicked)
        {
            onButtonClicked = true;
            StartCoroutine("MoveRightFrameToLeft");
        }
        else
        {
            return;
        }
        leftFrameIsOpened = !leftFrameIsOpened;
    }

    //плавное движение менюшки вправо
    private IEnumerator MoveLeftFrameToRight()
    {
        while (leftFrameXPosition < -0.04)
        {
            leftFrame.transform.position = new Vector3(leftFrame.transform.position.x + 0.06f, leftFrame.transform.position.y, leftFrame.transform.position.z);
            yield return new WaitForSeconds(0.001f);
        }
        onButtonClicked = false;
    }

    //плавное движение менюшки влево
    private IEnumerator MoveRightFrameToLeft()
    {
        while (leftFrameXPosition > -5.6)
        {
            leftFrame.transform.position = new Vector3(leftFrame.transform.position.x - 0.06f, leftFrame.transform.position.y, leftFrame.transform.position.z);
            yield return new WaitForSeconds(0.001f);
        }
        onButtonClicked = false;
    }

    #endregion
}
