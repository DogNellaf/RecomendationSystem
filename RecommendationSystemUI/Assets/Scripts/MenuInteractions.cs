using RecommendationSystem.Models;
using System.Collections;
using UnityEngine;

public class MenuInteractions : MonoBehaviour
{
    #region Left Frame Interactions

    //левая выпадающая менюшка
    [SerializeField] private GameObject leftFrame;

    //логическая переменная для переключения состояния панели
    [SerializeField] private bool leftFrameIsOpened = false;

    //свойство для быстрого получения позиции по х для левой менюшки
    private float leftFrameXPosition => leftFrame.transform.position.x;

    //предотвращение дабл-клика
    private bool onButtonClicked = false;

    //закрыть панель
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

    #region Prefs
    
    // класс работы с базой данных
    [SerializeField] private Database database;
    public Database Database { get => database; }

    // сам объект с содержимым страницы
    [SerializeField] private GameObject page;
    public GameObject Page { get => page; }
    public PageController PageController { get => page.GetComponent<PageController>(); }

    // контроллер аккаунта
    [SerializeField] private GameObject account;
    public GameObject Account { get => account; }
    public AccountManagement AccountController { get => Account.GetComponent<AccountManagement>(); }

    // поиск главного класса
    public static MenuInteractions Current => GameObject.Find("Canvas").GetComponent<MenuInteractions>();

    #endregion
}
