using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;








[System.Serializable]
public class ArriveQuantityInfo
{
    public string secondOfficeName;
    public int packageGeneral;
    public int packageWindow;
    public int factorQuantity;
    public int international;

    public ArriveQuantityInfo(string secondOfficeName, int packageGeneral, int packageWindow, int factorQuantity, int international)
    {
        this.secondOfficeName = secondOfficeName;
        this.packageGeneral = packageGeneral;
        this.packageWindow = packageWindow;
        this.factorQuantity = factorQuantity;
        this.international = international;
    }
}

[System.Serializable]
public class ArriveQuantityList
{
    public ArriveQuantityInfo[] prefabDatas;
}

public class ArriveQuantityScript : MonoBehaviour
{
    public VehicleDataCenter vehicleDataCenter;

    private string firstOfficeCode = "0005";
    private string secondOfficeCode = string.Empty;

    public GameObject arrivePrefab;
    private List<ArriveQuantityInfo> arriveQuantityInfoList;
    public Transform content;

    [Header("조회조건")]
    public Dropdown firstOfficeDropdown;
    public Dropdown secondOfficeDropdown;
    public Dropdown startYearDropdown;
    public Dropdown startMonthDropdown;
    public Dropdown startDayDropdown;
    public Dropdown endYearDropdown;
    public Dropdown endMonthDropdown;
    public Dropdown endDayDropdown;




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

    private string TranslationCode(string code)
    {
        string officeName = string.Empty;

        switch (code)
        {
            case "0100":
                officeName = "대전우편집중국";
                break;
            case "0200":
                officeName = "청주우편집중국";
                break;
            case "0300":
                officeName = "원주우편집중국";
                break;
            case "0400":
                officeName = "제주우편집중국";
                break;
            case "0500":
                officeName = "수원우편집중국";
                break;
            case "0600":
                officeName = "부천우편집중국";
                break;
            case "0700":
                officeName = "광주우편집중국";
                break;
            case "0800":
                officeName = "의정부우편집중국";
                break;
            case "0900":
                officeName = "고양우편집중국";
                break;
            case "1000":
                officeName = "부산우편집중국";
                break;
            case "1100":
                officeName = "전주우편집중국";
                break;
            case "1200":
                officeName = "동서울우편집중국";
                break;
            case "1300":
                officeName = "성남우편집중국";
                break;
            case "1400":
                officeName = "대구우편집중국";
                break;
            case "1500":
                officeName = "안양우편집중국";
                break;
            case "1600":
                officeName = "강릉우편집중국";
                break;
            case "1700":
                officeName = "창원우편집중국";
                break;
            case "1800":
                officeName = "포항우편집중국";
                break;
            case "1900":
                officeName = "안동우편집중국";
                break;
            case "2000":
                officeName = "울산우편집중국";
                break;
            case "2100":
                officeName = "영암우편집중국";
                break;
            case "2200":
                officeName = "진주우편집중국";
                break;

        }

        return officeName;
    }

    public void OnclickResetBtn()
    {
        firstOfficeDropdown.value = 0;
        startYearDropdown.value = 0;
        startMonthDropdown.value = 0;
        startDayDropdown.value = 0;
        endYearDropdown.value = 0;
        endMonthDropdown.value = 0;
        endDayDropdown.value = 0;
    }

    public void OnclickSearchBtn()
    {
        arriveQuantityInfoList = new List<ArriveQuantityInfo>();

        string startYearText = startYearDropdown.GetComponentInChildren<Text>().text;
        string startMonthText = startMonthDropdown.GetComponentInChildren<Text>().text;
        string startDayText = startDayDropdown.GetComponentInChildren<Text>().text;
        string endYearText = endYearDropdown.GetComponentInChildren<Text>().text;
        string endMonthText = endMonthDropdown.GetComponentInChildren<Text>().text;
        string endDayText = endDayDropdown.GetComponentInChildren<Text>().text;

        string startDate = startYearText.Equals("년도") ? string.Empty : startYearText;
        startDate += startDate.Equals(string.Empty) || startMonthText.Equals("월") ? string.Empty : "-" + startMonthText;
        startDate += startDate.Equals(string.Empty) || startDayText.Equals("일") ? string.Empty : "-" + startDayText;

        string endDate = endYearText.Equals("년도") ? string.Empty : endYearText;
        endDate += endDate.Equals(string.Empty) || endMonthText.Equals("월") ? string.Empty : "-" + endMonthText;
        endDate += endDate.Equals(string.Empty) || endDayText.Equals("일") ? string.Empty : "-" + endDayText;

        Debug.Log($"관할청 : {firstOfficeCode}, 시작일 : {startDate}, 마감일 : {endDate}");
        //StartCoroutine(vehicleDataCenter.ArriveQuantityDataReceive(firstOfficeCode, startDate, endDate));


        StartCoroutine(vehicleDataCenter.ArriveQuantityDataReceive(firstOfficeCode, startDate, endDate));
    }

    public void HandleReceivedData(UnityWebRequest response)
    {

        foreach(Transform obj in content.GetComponentsInChildren<Transform>())
        {
            if (content != obj)
            {
                Destroy(obj.gameObject);
            }
        }

        string dataString = response.downloadHandler.text;
        bool duplicate = false;

        string[] preGeneralOfficeCode = dataString.Split("\"generalOfficeCode\":");
        string[] prePackageStatus = dataString.Split("\"packageStatus\":");

        for(int i = 1; i < preGeneralOfficeCode.Length; i++)
        {
            int listIndex = 0;

            string[] generalOfficeCode = preGeneralOfficeCode[i].Split(',');
            string[] packageStatus = prePackageStatus[i].Split("}");


            generalOfficeCode[0] = generalOfficeCode[0].Trim('"');
            packageStatus[0] = packageStatus[0].Trim('"');

            if(arriveQuantityInfoList.Count > 0)
                for (int j = 0; j < arriveQuantityInfoList.Count; j++)
                {
                    //우체국 중복부터 처리해야함
                    if (generalOfficeCode[0].Equals(arriveQuantityInfoList[j].secondOfficeName))
                    {
                        //해당 우체죽 구조체의 물품에 덧샘을 해야함
                        duplicate = true;
                        listIndex = j;
                        break;
                    }
                }

            if (duplicate)      //중복되는 코드가 있으면
            {
                switch (packageStatus[0])
                {
                    case "일반소포":
                        arriveQuantityInfoList[listIndex].packageGeneral += 1;
                        break;
                    case "창구소포":
                        arriveQuantityInfoList[listIndex].packageWindow += 1;
                        break;
                    case "방문소포":
                        arriveQuantityInfoList[listIndex].factorQuantity += 1;
                        break;
                    case "국제특급":
                        arriveQuantityInfoList[listIndex].international += 1;
                        break;
                }
            }
            else
            {
                switch (packageStatus[0])
                {
                    case "일반소포":
                        arriveQuantityInfoList.Add(new ArriveQuantityInfo(generalOfficeCode[0], 1, 0, 0, 0));
                        break;
                    case "창구소포":
                        arriveQuantityInfoList.Add(new ArriveQuantityInfo(generalOfficeCode[0], 0, 1, 0, 0));
                        break;
                    case "방문소포":
                        arriveQuantityInfoList.Add(new ArriveQuantityInfo(generalOfficeCode[0], 0, 0, 1, 0));
                        break;
                    case "국제특급":
                        arriveQuantityInfoList.Add(new ArriveQuantityInfo(generalOfficeCode[0], 0, 0, 0, 1));
                        break;
                }
            }


            duplicate = false;
        }

        for(int i = 0; i < arriveQuantityInfoList.Count; i++)
        {
            GameObject newObj = Instantiate(arrivePrefab, content);
            ArriveQuantityDataPrefabScript AQDPS = newObj.GetComponent<ArriveQuantityDataPrefabScript>();
            arriveQuantityInfoList[i].secondOfficeName = TranslationCode(arriveQuantityInfoList[i].secondOfficeName);
            AQDPS.SetData(arriveQuantityInfoList[i]);
        }
    }
}
