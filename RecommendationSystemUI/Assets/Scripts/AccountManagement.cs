using RecommendationSystem.Models;
using UnityEngine;

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
    public void SetUser(string name, string password)
    {
        var database = MenuInteractions.Current.Database;

        var rawUser = database.GetObject<User>($"getuserbyname?name={name}");
        var user = database.GetObject<User>($"checkuserhash?user_id={rawUser.Id}&password={password}");
        if (!string.IsNullOrEmpty(user.Name))
        {
            currentUser = user;
            Debug.Log("true");
        }
        else
        {
            Debug.Log("false");
        }
    }

    public void OnClick()
    {
        menu.ClosePanel(transform.parent.gameObject);

        if (currentUser == null)
        {
            menu.PageController.ShowPage(loginPrefab);
        }
        else
        {
            menu.PageController.ShowPage(accountPrefab);
        }
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
