using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Image 컴포넌트를 사용하기 위한 네임스페이스 추가

[RequireComponent(typeof(BoxCollider2D))] // BoxCollider2D를 자동으로 추가
public class ItemComponent : MonoBehaviour, IPointerClickHandler
{
    public Item item; // ScriptableObject 참조

    private Image itemImage; // Image 컴포넌트 참조

    private void Start()
    {
        itemImage = GetComponent<Image>(); // Image 컴포넌트 가져오기

        if (itemImage == null)
        {
            Debug.LogError("Image component is missing on this GameObject.");
            return; // Image가 없으면 더 이상 진행하지 않음
        }
        else
        {
            Debug.Log(itemImage);
        }

        AdjustCollider(); // Collider 크기 조정
    }

    private void AdjustCollider()
    {
        // RectTransform 가져오기
        RectTransform rectTransform = GetComponent<RectTransform>();

        // BoxCollider2D 가져오기
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        // RectTransform의 크기에 맞춰 Collider 크기 조정
        collider.size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // UIController에 클릭된 부품 정보를 전달
        FacilityUIController.Instance.SelectItem(this);
    }

    public void Highlight()
    {
        if (itemImage == null)
        {
            Debug.LogError("itemImage가 null입니다. 이미지 컴포넌트가 부탁되어있는지 확인하세요");
            return;
        }
        itemImage.color = Color.yellow; // 색깔 변경
    }

    public void ResetColor()
    {
        itemImage.color = Color.white; // 원래 색깔로 복원
    }
}
