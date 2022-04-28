using RecommendationSystem.Models;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviewPageController : MonoBehaviour
{
    [SerializeField] private string title = "Отзывы к товару";
    [SerializeField] private GameObject reviewPanel;
    [SerializeField] private GameObject panel;
    [SerializeField] private Sprite star;
    [SerializeField] private Vector3 scale = new Vector3(1, 1, 1);
    public Item Item { private get;  set; }
    void Start()
    {
        var database = MenuInteractions.Current.Database;
        SetText("Title", $"{title} {Item.Name}", transform);
        var reviews = database.GetReviewsByItem(Item);
        var position = 600;
        foreach (Review review in reviews)
        {
            GameObject frame = Instantiate(reviewPanel);
            frame.transform.SetParent(panel.transform);
            frame.transform.localPosition = new Vector3(0, position, 0);
            frame.transform.localScale = scale;

            var user = database.GetUserByReview(review.Id);

            SetText("User", user.Name, frame.transform);
            SetText("Text", review.Text, frame.transform);
            SetText("Date", review.Date, frame.transform);

            var stars = GetStars(frame.transform.Find("Rating"));
            for (int i = 1; i <= review.Rating; i++)
            {
                if (i <= 5)
                {
                    stars[i - 1].GetComponent<Image>().sprite = star;
                }
                else
                {
                    Debug.LogWarning("[WARNING] Попытка записать рейтинг выше пяти. Проверьте базу данных.");
                }
            }

            position -= 199;
        }
    }

    private void SetText(string objectName, string text, Transform parent) => 
        parent.transform.Find(objectName).GetComponentInChildren<TextMeshProUGUI>().text = text;

    private static List<GameObject> GetStars(Transform frame)
    {
        var stars = new List<GameObject>();
        foreach (Transform child in frame.transform)
        {
            if (child.gameObject.name == "Star")
            {
                stars.Add(child.gameObject);
            }
        }
        return stars;
    }
}
