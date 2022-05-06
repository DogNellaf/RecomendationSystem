using RecommendationSystem.Models;
using UnityEditor;
using UnityEngine;

public class ReviewShower : MonoBehaviour
{
    [SerializeField] private GameObject ReviewPage;
    public Item Item { private get; set; }

    public void ShowReviews()
    {
        var pageParent = MenuInteractions.Current.Page;
        var page = Instantiate(ReviewPage);
        page.GetComponent<ReviewPageController>().Item = Item;
        page.transform.SetParent(pageParent.transform);
        page.transform.localScale = new Vector3(1, 1, 1);
        Destroy(transform.parent.gameObject);
    }
}
