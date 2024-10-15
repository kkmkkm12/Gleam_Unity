using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FacilityUIController : MonoBehaviour
{
    public static FacilityUIController Instance; // Singleton 패턴
    public ItemComponent[] itemComponents; // 모든 부품의 ItemComponent를 배열로 저장
    public TMP_Text itemNameText; // 아이템 이름을 표시할 텍스트
    public TMP_Dropdown itemDropdown; // 부품 선택을 위한 드롭다운

    private ItemComponent currentSelectedItem;
    [SerializeField]
    private GameObject[] items;

    private void Awake()
    {
        Instance = this; // Singleton 인스턴스 설정
    }

    private void Start()
    {
        // 드롭다운에 부품 목록 추가
        foreach (var itemComponent in itemComponents)
        {
            itemDropdown.options.Add(new TMP_Dropdown.OptionData(itemComponent.item.itemName));
        }

        // 드롭다운 선택 변경 이벤트 추가
        itemDropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        items = GameObject.FindGameObjectsWithTag("Item");
        // 색상 변경 시작
        StartCoroutine(ChangeColorRandomly());
    }

    private IEnumerator ChangeColorRandomly()
    {
        Color[] colors = { Color.red, Color.yellow, Color.green, Color.blue }; // 사용할 색상 배열

        while (true)
        {
            // 랜덤 인덱스 선택
            int randomIndex = Random.Range(0, items.Length);
            GameObject selectedItem = items[randomIndex];

            // 이미지 컴포넌트 가져오기
            Image itemImage = selectedItem.GetComponent<Image>();
            if (itemImage != null)
            {
                // 랜덤 색상 선택
                Color randomColor = colors[Random.Range(0, colors.Length)];

                // 색깔 변경
                itemImage.color = randomColor;

                // 1초 대기
                yield return new WaitForSeconds(1.5f);

                // 색깔 원래대로 복원
                itemImage.color = Color.white;
            }

            // 잠시 대기 후 다음 아이템으로 넘어감
            yield return new WaitForSeconds(0.5f); // 추가 대기 시간
        }
    }

    public void OnDropdownValueChanged(int index)
    {
        // 이전 선택된 아이템 색상 복원
        if (currentSelectedItem != null)
        {
            currentSelectedItem.ResetColor();
        }

        // 선택한 부품의 아이템 정보를 표시
        currentSelectedItem = itemComponents[index];
        itemNameText.text = "아이템 이름: " + currentSelectedItem.item.itemName;

        // 색깔 변경
        currentSelectedItem.Highlight();
    }

    public void SelectItem(ItemComponent itemComponent)
    {
        // 선택된 아이템의 색깔 변경
        if (currentSelectedItem != null)
        {
            currentSelectedItem.ResetColor();
        }

        currentSelectedItem = itemComponent;
        currentSelectedItem.Highlight();
        itemNameText.text = "아이템 이름: " + currentSelectedItem.item.itemName;
    }
}



