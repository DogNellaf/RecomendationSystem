using RecommendationSystem.Models;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AccountManagement : MonoBehaviour
{
    // ������ ��������-��������
    [SerializeField] private GameObject accountPrefab;

    [SerializeField] private GameObject loginPrefab;

    // ������� ������������
    private User currentUser;
    public User User { get => currentUser; }

    private MenuInteractions menu;

    // ��������� �������
    private void Start()
    {
        menu = MenuInteractions.Current;
    }

    // ������� �����������
    public void SetUser(GameObject parent)
    {
        string name = Find("NameField", parent.transform).GetComponent<TMP_InputField>().text;
        string password = Find("PasswordField", parent.transform).GetComponent<TMP_InputField>().text;
        var database = MenuInteractions.Current.Database;

        var rawUser = database.GetObject<User>($"getuserbyname?name={name}");
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

    // ��������� ����
    public void UploadPhoto() => MenuInteractions.Current.Database.UploadTexture(currentUser.Id);

    // ����� ������ �� �����
    private GameObject Find(string name, Transform parent) => parent.Find(name).gameObject; 

    // ��������� �������
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

    // ������� ������ ������ ������������
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

    // ������� ������ �������� �����������
    private void ShowLogin()
    {
        var login = menu.PageController.ShowPage(loginPrefab);
        Find("Button", login.transform).GetComponent<Button>().onClick.AddListener(delegate { SetUser(login); });
    }

    // ������� ������ �� ��������
    private void Exit()
    {
        currentUser = null;
        ShowLogin();
    }
}
