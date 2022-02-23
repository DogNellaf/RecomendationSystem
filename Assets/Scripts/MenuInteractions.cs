using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteractions : MonoBehaviour
{
    #region LeftFrameInteractions

    //����� ���������� �������
    [SerializeField] private GameObject leftFrame;

    //���������� ���������� ��� ������������ ��������� ������
    [SerializeField] private bool leftFrameIsOpened = false;

    //�������� ��� �������� ��������� ������� �� � ��� ����� �������
    private float leftFrameXPosition => leftFrame.transform.position.x;

    //�������������� ����-�����
    private bool onButtonClicked = false;

    //�������\������� �������
    public void OnLeftButtonClick()
    {
        //���� ����� ���� �� �������
        if (!leftFrameIsOpened && !onButtonClicked)
        {
            onButtonClicked = true;
            StartCoroutine("MoveLeftFrameToRight");
        }

        //����� ���� ����� ���� �������
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

    //������� �������� ������� ������
    private IEnumerator MoveLeftFrameToRight()
    {
        while (leftFrameXPosition < -0.04)
        {
            leftFrame.transform.position = new Vector3(leftFrame.transform.position.x + 0.06f, leftFrame.transform.position.y, leftFrame.transform.position.z);
            yield return new WaitForSeconds(0.001f);
        }
        onButtonClicked = false;
    }

    //������� �������� ������� �����
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
