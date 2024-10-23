using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ArriveQuantityScript : MonoBehaviour
{
    string prefabName = "프리펩 경로/PostQuantityPrefab";
    public VehicleDataCenter vehicleDataCenter;

    private string firstOfficeCode = "0005";
    private string secondOfficeCode = string.Empty;

    [Header("조회조건")]
    public Dropdown firstOfficeDropdown;
    public Dropdown secondOfficeDropdown;
    public Dropdown startYearDropdown;
    public Dropdown startMonthDropdown;
    public Dropdown startdayDropdown;
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


    public void OnclickSearchBtn()
    {
        string startYearText = startYearDropdown.GetComponentInChildren<Text>().text;
        string startMonthText = startMonthDropdown.GetComponentInChildren<Text>().text;
        string startDayText = startdayDropdown.GetComponentInChildren<Text>().text;
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
        StartCoroutine(vehicleDataCenter.ArriveQuantityDataReceive(firstOfficeCode, startDate, endDate));
    }

    public void HandleReceivedData(UnityWebRequest response)
    {
        Debug.Log("데이터 받고나서 파싱 해야함.");
    }
}
