using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TotalMonitoringScript : MonoBehaviour
{
    public VehicleDataCenter vehicleDataCenter;

    public Dropdown firstOfficeDropdown;                            //서울청, 강원청, 경인청, 부산청, 충청청, 전북청, 전남청, 제주청, 경북청
    string firstOfficeCode = string.Empty;
    public Dropdown secondOfficeDropdown;
    string secondOfficeCode = null;

    [Header("TotalQuantitySet의 Text")]
    public Text tb1FirstLineText;
    public Text tb1SecondLineText;
    public Text tb1ThirdLineText;
    public Text tb1FourthLineText;

    [Header("ReceiptQuantitySet의 Text")]
    public Text tb2FirstLineText1;
    public Text tb2FirstLineText2;
    public Text tb2FirstLineText3;
    public Text tb2SecondLineText1;
    public Text tb2SecondLineText2;
    public Text tb2SecondLineText3;
    public Text tb2ThirdLineText1;
    public Text tb2ThirdLineText2;
    public Text tb2ThirdLineText3;
    public Text tb2FourthLineText1;
    public Text tb2FourthLineText2;
    public Text tb2FourthLineText3;

    [Header("DeliveryQuantitySet의 Text")]
    public Text tb3FirstLineText1;
    public Text tb3FirstLineText2;
    public Text tb3FirstLineText3;
    public Text tb3FirstLineText4;
    public Text tb3FirstLineText5;
    public Text tb3SecondLineText1;
    public Text tb3SecondLineText2;
    public Text tb3SecondLineText3;
    public Text tb3SecondLineText4;
    public Text tb3SecondLineText5;
    public Text tb3ThirdLineText1;
    public Text tb3ThirdLineText3;
    public Text tb3ThirdLineText4;
    public Text tb3FourthLineText1;
    public Text tb3FourthLineText3;
    public Text tb3FourthLineText4;
    public Text tb3FifthLineText1;
    public Text tb3FifthLineText2;
    public Text tb3FifthLineText3;
    public Text tb3FifthLineText4;
    public Text tb3FifthLineText5;

    [Header("ConcentrationQuantitySet의 Text")]
    public Text tb4FirstLineText1;
    public Text tb4FirstLineText2;
    public Text tb4FirstLineText3;
    public Text tb4FirstLineText4;
    public Text tb4FirstLineText5;
    public Text tb4SecondLineText1;
    public Text tb4SecondLineText2;
    public Text tb4SecondLineText3;
    public Text tb4SecondLineText4;
    public Text tb4SecondLineText5;
    public Text tb4ThirdLineText1;
    public Text tb4ThirdLineText2;
    public Text tb4ThirdLineText3;
    public Text tb4ThirdLineText4;
    public Text tb4ThirdLineText5;

    [Header("TransitSituationSet의 Text")]
    public Text tb5FirstLineText1;
    public Text tb5FirstLineText2;
    public Text tb5FirstLineText3;
    public Text tb5SecondLineText1;
    public Text tb5SecondLineText2;
    public Text tb5SecondLineText3;

    [Header("LateSituationSet의 Text")]
    public Text tb6FirstLineText1;
    public Text tb6FirstLineText2;

    [Header("ObstacleSituationSet의 Text")]
    public Text tb7FirstLineText1;
    public Text tb7FirstLineText2;
    public Text tb7FirstLineText3;
    public Text tb7FirstLineText4;

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

    public void OnClickSearchBtn()
    {
        //데이터 받는 객체 하나 만들어 놓고나서 해당 스크립트에서 데이터 받아오는 함수 호출시켜서 여기서 받아서 화면에 도출 시키기 할거임.
        StartCoroutine(vehicleDataCenter.TotalMoniterDataReceive(secondOfficeCode));
    }

    public void HandleReceivedData(UnityWebRequest response)
    {
        //우선 파싱부터 하자 tb에 따른 파싱부터 해야함
        string[] tb = response.downloadHandler.text.Split("tb");
        SetDataTB1(tb[1]);
        SetDataTB2(tb[2]);
        SetDataTB3(tb[3]);
        SetDataTB4(tb[4]);
    }

    public void SetDataTB1(string data)
    {
        string[] tb1Array = data.Split(",");
        string[] todayCount = tb1Array[2].Split("당일접수\":");                //1 사용
        string[] preYesterdayCount = tb1Array[3].Split("\"기준일접수량\":");
        string[] yesterdayCount = preYesterdayCount[1].Split("}]");             //0 사용

        tb1FirstLineText.text = todayCount[1];
        tb1SecondLineText.text = yesterdayCount[0];
    }

    public void SetDataTB2(string data)
    {
        string[] tb2Array = data.Split(",");
        string[] todayGCount = tb2Array[2].Split("\"기준일일반\":");      //1 사용
        string[] todayDCount = tb2Array[3].Split("\"기준일등기\":");      //1 사용
        string[] todayICount = tb2Array[4].Split("\"기준일국제우편\":");   //1 사용
        string[] yesterdayGCount = tb2Array[5].Split("\"기준일일반_어제\":");   //1사용
        string[] yesterdayDCount = tb2Array[6].Split("\"기준일등기_어제\":");   //1사용
        string[] preYesterdayICount = tb2Array[7].Split("\"기준일국제우편_어제\":");   //1사용
        string[] yesterdayICount = preYesterdayICount[1].Split("}]");               //0사용

        tb2FirstLineText1.text = todayGCount[1];
        tb2SecondLineText1.text = todayDCount[1];
        tb2ThirdLineText1.text = todayICount[1];
        tb2FirstLineText2.text = yesterdayGCount[1];
        tb2SecondLineText2.text = yesterdayDCount[1];
        tb2ThirdLineText2.text = yesterdayICount[0];

        tb2FourthLineText1.text = (int.Parse(todayGCount[1]) + int.Parse(todayDCount[1]) + int.Parse(todayICount[1])).ToString();
        tb2FourthLineText2.text = (int.Parse(yesterdayGCount[1]) + int.Parse(yesterdayDCount[1]) + int.Parse(yesterdayICount[0])).ToString();

    }
    public void SetDataTB3(string data)
    {
        string[] tb3Array = data.Split(",");
        string[] yesterdayCCount = tb3Array[0].Split("\"기준일배달완료\":");   //1 사용
        string[] todayPCount = tb3Array[1].Split("\"당일배달예정\":");
        string[] todayICount = tb3Array[2].Split("\"당일진행중\":");
        string[] preTodayCCount = tb3Array[3].Split("\"당일배달완료\":");
        string[] todayCCount = preTodayCCount[1].Split("}]");               //0 사용

        tb3FirstLineText1.text = yesterdayCCount[1];
        tb3FirstLineText2.text = todayPCount[1];
        tb3FirstLineText3.text = todayICount[1];
        tb3FirstLineText4.text = todayCCount[0];
    }
    public void SetDataTB4(string data)
    {
        string[] tb4Array = data.Split("}],");
        string[] boxCount = tb4Array[0].Split("[{\"집중국상자\":");
        string[] palletCount = tb4Array[1].Split("[{\"집중국파렛\":");
        string[] rollCount = tb4Array[2].Split("[{\"집중국롤파렛\":");
        string[] trollCount = tb4Array[3].Split("[{\"집중국트롤리\":");
        string[] bagCount = tb4Array[4].Split("[{\"집중국자루\":");               //1사용   
        string[] preNoBottleCount = tb4Array[5].Split("[{\"집중국무용기\":");
        string[] noBottleCount = preNoBottleCount[1].Split("}]]}");             //0사용

        tb4SecondLineText1.text = boxCount[1];
        tb4SecondLineText2.text = (int.Parse(palletCount[1]) + int.Parse(rollCount[1])).ToString();
        tb4SecondLineText3.text = trollCount[1];
        tb4SecondLineText4.text = bagCount[1];
        tb4SecondLineText5.text = noBottleCount[0];
    }
}
