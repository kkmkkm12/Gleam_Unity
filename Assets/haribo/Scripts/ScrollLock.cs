using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollLock : MonoBehaviour, IDragHandler
{
    private ScrollRect scrollRect;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))// 좌우로 드래그할 때, 세로 스크롤 비활성화
        {
            scrollRect.vertical = false;
            scrollRect.horizontal = true;
        }
        else// 상하로 드래그할 때, 가로 스크롤 비활성화
        {
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
        }
    }
}