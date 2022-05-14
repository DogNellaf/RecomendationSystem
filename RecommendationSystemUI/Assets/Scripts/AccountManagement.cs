using RecommendationSystem.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountManagement : MonoBehaviour
{
    // префаб страницы-аккаунта
    [SerializeField] private GameObject accountPrefab;

    [SerializeField] private GameObject loginPrefab;

    // текущий пользователь
    private User currentUser;
    public User User { get => currentUser; }

    private MenuInteractions menu;

    // стартовая функция
    private void Start()
    {
        menu = MenuInteractions.Current;
    }

    // функция авторизации
    public void SetUser(GameObject parent)
    {
        bool isNewUser = Find("NewUser", parent.transform).GetComponent<Toggle>().isOn;
        string username = Find("NameField", parent.transform).GetComponent<TMP_InputField>().text;
        string password = Find("PasswordField", parent.transform).GetComponent<TMP_InputField>().text;
        var database = MenuInteractions.Current.Database;
        if (isNewUser)
        {
            database.AddUser(username, password);
        }

        var rawUser = database.GetObject<User>($"getuserbyname?name={username}");
        var user = database.GetObject<User>($"checkuserhash?user_id={rawUser.Id}&password={password}");
        if (!string.IsNullOrEmpty(user.Name))
        {
            currentUser = user;
            ShowAccount();
        }
        else
        {
            Debug.Log("false");
        }
    }

    // загрузить фото
    public void UploadPhoto() => MenuInteractions.Current.Database.UploadTexture(currentUser.Id);

    // найти объект по имени
    private GameObject Find(string name, Transform parent) => parent.Find(name).gameObject; 

    // обработка нажатия
    public void OnClick()
    {
        menu.ClosePanel(transform.parent.gameObject);

        if (currentUser == null)
        {
            ShowLogin();
        }
        else
        {
            ShowAccount();
        }
    }

    // функция показа данных пользователя
    private void ShowAccount()
    {
        var page = menu.PageController.ShowPage(accountPrefab);
        Find("UserName", page.transform).GetComponent<TextMeshProUGUI>().text = currentUser.Name;
        if (currentUser.Photo != null)
        {
            var avatar = Find("Avatar", page.transform).GetComponent<Image>();
            MenuInteractions.Current.Database.SetImage(currentUser.Photo, avatar);
        }
        Find("Exit", page.transform).GetComponent<Button>().onClick.AddListener(delegate { Exit(); });
    }

    // функция показа страницы авторизации
    private void ShowLogin()
    {
        var login = menu.PageController.ShowPage(loginPrefab);
        Find("Button", login.transform).GetComponent<Button>().onClick.AddListener(delegate { SetUser(login); });
    }

    // функция выхода из аккаунта
    private void Exit()
    {
        currentUser = null;
        ShowLogin();
    }
}
