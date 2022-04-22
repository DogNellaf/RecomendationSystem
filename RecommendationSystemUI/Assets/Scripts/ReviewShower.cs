using RecommendationSystem.Models;
using UnityEngine;

public class ReviewShower : MonoBehaviour
{
    [SerializeField] private GameObject ReviewPage;
    [SerializeField] private GameObject PageParent;

    [SerializeField] public Item Item;

    private void Start()
    {
        PageParent = GameObject.Find("Page");
    }

    public void ShowReviews()
    {
        var page = Instantiate(ReviewPage);
        page.GetComponent<ReviewPageController>().Item = Item;
        page.transform.SetParent(PageParent.transform);
        page.transform.localScale = new Vector3(1, 1, 1);
        Destroy(transform.parent.gameObject);
    }
}
