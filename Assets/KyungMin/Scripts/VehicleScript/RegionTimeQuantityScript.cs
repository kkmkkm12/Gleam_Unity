using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegionTimeQuantityScript : MonoBehaviour
{
    public VehicleDataCenter vehicleDataCenter;

    private string firstOfficeCode = "0005";
    private string secondOfficeCode = string.Empty;

    [Header("조회조건")]
    public Dropdown firstOfficeDropdown;
    public Dropdown secondOfficeDropdown;
    public Dropdown yearDropdown;
    public Dropdown monthDropdown;
    public Dropdown dayDropdown;
    public Dropdown startTimeDropdown;
    public Dropdown endTimeDropdown;

    [Header("일반소포")]
    public Text fisrtLine1;
    public Text fisrtLine2;
    public Text fisrtLine3;
    public Text fisrtLine4;
    public Text fisrtLine5;
    public Text fisrtLine6;
    public Text fisrtLine7;
    public Text fisrtLine8;
    public Text fisrtLine9;

    [Header("창구소포")]
    public Text secondLine1;
    public Text secondLine2;
    public Text secondLine3;
    public Text secondLine4;
    public Text secondLine5;
    public Text secondLine6;
    public Text secondLine7;
    public Text secondLine8;
    public Text secondLine9;

    [Header("국내특급-소포")]
    public Text thirdLine1;
    public Text thirdLine2;
    public Text thirdLine3;
    public Text thirdLine4;
    public Text thirdLine5;
    public Text thirdLine6;
    public Text thirdLine7;
    public Text thirdLine8;
    public Text thirdLine9;

    [Header("방문소포")]
    public Text fourthLine1;
    public Text fourthLine2;
    public Text fourthLine3;
    public Text fourthLine4;
    public Text fourthLine5;
    public Text fourthLine6;
    public Text fourthLine7;
    public Text fourthLine8;
    public Text fourthLine9;

    [Header("국제특급")]
    public Text fifthLine1;
    public Text fifthLine2;
    public Text fifthLine3;
    public Text fifthLine4;
    public Text fifthLine5;
    public Text fifthLine6;
    public Text fifthLine7;
    public Text fifthLine8;
    public Text fifthLine9;

    [Header("합계")]
    public Text totalPlusLine1;
    public Text totalPlusLine2;
    public Text totalPlusLine3;
    public Text totalPlusLine4;
    public Text totalPlusLine5;
    public Text totalPlusLine6;
    public Text totalPlusLine7;
    public Text totalPlusLine8;
    public Text totalPlusLine9;

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
        StartCoroutine(vehicleDataCenter.RegionTimeQuantityDataReceive(firstOfficeCode, secondOfficeCode, string.Empty));
    }

    public void HandleReceivedData(UnityWebRequest response)
    {
        Debug.Log("데이터 받고나서 파싱 해야함.");
    }
}
