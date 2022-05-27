using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIScroller : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private float dragTreshold = 0.2f;
    [SerializeField] private float easing = 0.5f;
    [SerializeField] private Shop shop;
    [SerializeField] private RectTransform shopPanel;
    [SerializeField] private RectTransform canvas;
    private Vector3 panelLocation;
    private float maxPages = 1;
    private float currentPage = 1;

    private void Start()
    {
        panelLocation = transform.position;
        maxPages = shop.Containers.Length;
    }

    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.y - data.position.y;
        transform.position = panelLocation - new Vector3(0, difference, 0);

    }
    public void OnEndDrag(PointerEventData data)
    {
        Debug.Log(shopPanel.rect.height);
        Debug.Log(shopPanel.sizeDelta.y);
        float percentage = (data.pressPosition.y - data.position.y) / (shopPanel.rect.height * canvas.localScale.y);
        if (Mathf.Abs(percentage) >= percentage)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0 && currentPage > 1)
            {
                currentPage--;
                newLocation += new Vector3(0, -shopPanel.rect.height * canvas.localScale.y, 0);
            }
            else if (percentage < 0 && currentPage < maxPages)
            {
                currentPage++;
                newLocation += new Vector3(0, shopPanel.rect.height * canvas.localScale.y, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;

        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }

    }

    private IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
