using RecommendationSystem.Models;
using System.Collections;
using UnityEngine;

public class MenuInteractions : MonoBehaviour
{
    #region Left Frame Interactions

    //����� ���������� �������
    [SerializeField] private GameObject leftFrame;

    //���������� ���������� ��� ������������ ��������� ������
    [SerializeField] private bool leftFrameIsOpened = false;

    //�������� ��� �������� ��������� ������� �� � ��� ����� �������
    private float leftFrameXPosition => leftFrame.transform.position.x;

    //�������������� ����-�����
    private bool onButtonClicked = false;

    //������� ������
    public void ClosePanel(GameObject element)
    {
        if (element.name != null)
        {
            if (element.name != "Page")
            {
                OnLeftButtonClick();
            }
        }
        else
        {
            OnLeftButtonClick();
        }
    }

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

    #region Prefs
    
    // ����� ������ � ����� ������
    [SerializeField] private Database database;
    public Database Database { get => database; }

    // ��� ������ � ���������� ��������
    [SerializeField] private GameObject page;
    public GameObject Page { get => page; }
    public PageController PageController { get => page.GetComponent<PageController>(); }

    // ���������� ��������
    [SerializeField] private GameObject account;
    public GameObject Account { get => account; }
    public AccountManagement AccountController { get => Account.GetComponent<AccountManagement>(); }

    // ����� �������� ������
    public static MenuInteractions Current => GameObject.Find("Canvas").GetComponent<MenuInteractions>();

    #endregion
}
