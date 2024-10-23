using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class OperationDetailInfo
{
    public string netType;
    public string carNum;
    public string driverPhoneNum;
    public string departOfficeName;
    public string arriveOfficeName;
    public string departExpectTime;
    public string arriveExpectTime;
    public string driverName;
    public string departArriveStatus;
    public string transitCheckNum;
}

[System.Serializable]
public class OperationDetailList
{
    public OperationDetailInfo[] prefabDatas;   // json 배열을 담기위한 필드
}


public class OperationDetailScript : MonoBehaviour
{
    public VehicleDataCenter vehicleDataCenter;
    private string firstOfficeCode = "0005";
    private string secondOfficeCode = string.Empty;

    public GameObject operationPrefab;
    private List<OperationDetailInfo> operationDetailInfoList;

    public Transform content;

    [Header("조회조건")]
    public Dropdown firstOfficeDropdown;
    public Dropdown secondOfficeDropdown;
    public Dropdown netTypeDropdown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstOfficeSelect()
    {
        int select = firstOfficeDropdown.value;
        secondOfficeDropdown.options.Clear();
        secondOfficeDropdown.options.Add(new Dropdown.OptionData("집중국"));
        switch (select)
        {
            case 0:
                firstOfficeCode = "0005";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("대전우편집중국"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("청주우편집중국"));
                break;
            case 1:
                firstOfficeCode = "0001";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("동서울우편집중국"));
                break;
            case 2:
                firstOfficeCode = "0002";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("원주우편집중국"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("강릉우편집중국"));
                break;
            case 3:
                firstOfficeCode = "0003";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("수원우편집중국"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("부천우편집중국"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("의정부우편집중국"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("고양우편집중국"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("성남우편집중국"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("안양우편집중국"));
                break;
            case 4:
                firstOfficeCode = "0004";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("부산우편집중국"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("창원우편집중국"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("울산우편집중국"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("진주우편집중국"));
                break;
            case 5:
                firstOfficeCode = "0006";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("전주우편집중국"));
                break;
            case 6:
                firstOfficeCode = "0007";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("광주우편집중국"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("영암우편집중국"));
                break;
            case 7:
                firstOfficeCode = "0008";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("제주우편집중국"));
                break;
            case 8:
                firstOfficeCode = "0009";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("대구우편집중국"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("포항우편집중국"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("안동우편집중국"));
                break;
        }

        Debug.Log(secondOfficeCode + "입니다");
    }

    public void SecondOfficeSelect()
    {
        string selectText = secondOfficeDropdown.transform.GetComponentInChildren<Text>().text;
        switch (selectText)
        {
            case "대전우편집중국":
                secondOfficeCode = "0100";
                break;
            case "청주우편집중국":
                secondOfficeCode = "0200";
                break;
            case "원주우편집중국":
                secondOfficeCode = "0300";
                break;
            case "제주우편집중국":
                secondOfficeCode = "0400";
                break;
            case "수원우편집중국":
                secondOfficeCode = "0500";
                break;
            case "부천우편집중국":
                secondOfficeCode = "0600";
                break;
            case "광주우편집중국":
                secondOfficeCode = "0700";
                break;
            case "의정부우편집중국":
                secondOfficeCode = "0800";
                break;
            case "고양우편집중국":
                secondOfficeCode = "0900";
                break;
            case "부산우편집중국":
                secondOfficeCode = "1000";
                break;
            case "전주우편집중국":
                secondOfficeCode = "1100";
                break;
            case "동서울우편집중국":
                secondOfficeCode = "1200";
                break;
            case "성남우편집중국":
                secondOfficeCode = "1300";
                break;
            case "대구우편집중국":
                secondOfficeCode = "1400";
                break;
            case "안양우편집중국":
                secondOfficeCode = "1500";
                break;
            case "강릉우편집중국":
                secondOfficeCode = "1600";
                break;
            case "창원우편집중국":
                secondOfficeCode = "1700";
                break;
            case "포항우편집중국":
                secondOfficeCode = "1800";
                break;
            case "안동우편집중국":
                secondOfficeCode = "1900";
                break;
            case "울산우편집중국":
                secondOfficeCode = "2000";
                break;
            case "영암우편집중국":
                secondOfficeCode = "2100";
                break;
            case "진주우편집중국":
                secondOfficeCode = "2200";
                break;
            default:
                secondOfficeCode = string.Empty;
                break;
        }
    }

    public void OnclickSearchBtn()
    {
        string netText = netTypeDropdown.GetComponentInChildren<Text>().text;
        netText = netText.Equals("선택") ? string.Empty : netText;

        StartCoroutine(vehicleDataCenter.OperationDetailDataReceive(firstOfficeCode, secondOfficeCode, netText));
    }

    public void HandleReceivedData(UnityWebRequest request)
    {
        Debug.Log("데이터 받고나서 파싱 해야함.");
        foreach (Transform obj in content.GetComponentsInChildren<Transform>())
        {
            Destroy(obj.gameObject);
        }

        var response = JsonUtility.FromJson<OperationDetailList>(request.downloadHandler.text);
        operationDetailInfoList = new List<OperationDetailInfo>(response.prefabDatas);

        foreach (OperationDetailInfo ODI in operationDetailInfoList)
        {
            GameObject newObj = Instantiate(operationPrefab, content);
            OperationDetailDataPrefabScript prefabScript = newObj.GetComponent<OperationDetailDataPrefabScript>();
            prefabScript.SetData(ODI);
        }
    }
}
