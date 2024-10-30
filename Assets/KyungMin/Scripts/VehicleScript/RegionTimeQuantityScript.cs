using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegionTimeQuantityScript : MonoBehaviour
{
    public VehicleDataCenter vehicleDataCenter;

    int[][] count = new int[5][];

    DateTime[] standardDateTime = {new DateTime(2024, 10, 15, 9, 0, 0), new DateTime(2024, 10, 15, 10, 0, 0), new DateTime(2024, 10, 15, 11, 0, 0), new DateTime(2024, 10, 15, 12, 0, 0),
    new DateTime(2024, 10, 15, 13, 0, 0), new DateTime(2024, 10, 15, 14, 0, 0), new DateTime(2024, 10, 15, 15, 0, 0), new DateTime(2024, 10, 15, 16, 0, 0)};


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
    public Text firstLine1;
    public Text firstLine2;
    public Text firstLine3;
    public Text firstLine4;
    public Text firstLine5;
    public Text firstLine6;
    public Text firstLine7;
    public Text firstLine8;
    public Text firstLine9;

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

    private void Awake()
    {
          
    }

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

    public void SetData(int[][] array)
    {
        int sum = 0;
        firstLine1.text = array[0][0].ToString();
        firstLine2.text = array[0][1].ToString();
        firstLine3.text = array[0][2].ToString();
        firstLine4.text = array[0][3].ToString();
        firstLine5.text = array[0][4].ToString();
        firstLine6.text = array[0][5].ToString();
        firstLine7.text = array[0][6].ToString();
        firstLine8.text = array[0][7].ToString();
        for(int i = 0; i < 8; i++)
        {
            sum += array[0][i];
        }
        firstLine9.text = sum.ToString();

        sum = 0;
        secondLine1.text = array[1][0].ToString();
        secondLine2.text = array[1][1].ToString();
        secondLine3.text = array[1][2].ToString();
        secondLine4.text = array[1][3].ToString();
        secondLine5.text = array[1][4].ToString();
        secondLine6.text = array[1][5].ToString();
        secondLine7.text = array[1][6].ToString();
        secondLine8.text = array[1][7].ToString();
        for (int i = 0; i < 8; i++)
        {
            sum += array[1][i];
        }
        secondLine9.text = sum.ToString();

        sum = 0;
        thirdLine1.text = array[2][0].ToString();
        thirdLine2.text = array[2][1].ToString();
        thirdLine3.text = array[2][2].ToString();
        thirdLine4.text = array[2][3].ToString();
        thirdLine5.text = array[2][4].ToString();
        thirdLine6.text = array[2][5].ToString();
        thirdLine7.text = array[2][6].ToString();
        thirdLine8.text = array[2][7].ToString();
        for (int i = 0; i < 8; i++)
        {
            sum += array[2][i];
        }
        thirdLine9.text = sum.ToString();

        sum = 0;
        fourthLine1.text = array[3][0].ToString();
        fourthLine2.text = array[3][1].ToString();
        fourthLine3.text = array[3][2].ToString();
        fourthLine4.text = array[3][3].ToString();
        fourthLine5.text = array[3][4].ToString();
        fourthLine6.text = array[3][5].ToString();
        fourthLine7.text = array[3][6].ToString();
        fourthLine8.text = array[3][7].ToString();
        for (int i = 0; i < 8; i++)
        {
            sum += array[3][i];
        }
        fourthLine9.text = sum.ToString();

        sum = 0;
        fifthLine1.text = array[4][0].ToString();
        fifthLine2.text = array[4][1].ToString();
        fifthLine3.text = array[4][2].ToString();
        fifthLine4.text = array[4][3].ToString();
        fifthLine5.text = array[4][4].ToString();
        fifthLine6.text = array[4][5].ToString();
        fifthLine7.text = array[4][6].ToString();
        fifthLine8.text = array[4][7].ToString();
        for (int i = 0; i < 8; i++)
        {
            sum += array[4][i];
        }
        fifthLine9.text = sum.ToString();


        sum = 0;
        for(int i = 0; i < 5; i++)
        {
            sum += array[i][0];
        }
        totalPlusLine1.text = sum.ToString();

        sum = 0;
        for (int i = 0; i < 5; i++)
        {
            sum += array[i][1];
        }
        totalPlusLine2.text = sum.ToString();

        sum = 0;
        for (int i = 0; i < 5; i++)
        {
            sum += array[i][2];
        }
        totalPlusLine3.text = sum.ToString();

        sum = 0;
        for (int i = 0; i < 5; i++)
        {
            sum += array[i][3];
        }
        totalPlusLine4.text = sum.ToString();

        sum = 0;
        for (int i = 0; i < 5; i++)
        {
            sum += array[i][4];
        }
        totalPlusLine5.text = sum.ToString();

        sum = 0;
        for (int i = 0; i < 5; i++)
        {
            sum += array[i][5];
        }
        totalPlusLine6.text = sum.ToString();

        sum = 0;
        for (int i = 0; i < 5; i++)
        {
            sum += array[i][6];
        }
        totalPlusLine7.text = sum.ToString();

        sum = 0;
        for (int i = 0; i < 5; i++)
        {
            sum += array[i][7];
        }
        totalPlusLine8.text = sum.ToString();



        totalPlusLine9.text = (int.Parse(totalPlusLine1.text) + int.Parse(totalPlusLine2.text) + int.Parse(totalPlusLine3.text) + 
            int.Parse(totalPlusLine4.text) + int.Parse(totalPlusLine5.text) + int.Parse(totalPlusLine6.text) + int.Parse(totalPlusLine7.text) + int.Parse(totalPlusLine8.text)).ToString(); ;
        Debug.Log(sum.ToString());
    }

    public void OnclickResetBtn()
    {
        firstOfficeDropdown.value = 0;
        secondOfficeDropdown.value = 0;
        yearDropdown.value = 0;
        monthDropdown.value = 0;
        dayDropdown.value = 0;
    }

    public void OnclickSearchBtn()
    {
        //StartCoroutine(vehicleDataCenter.RegionTimeQuantityDataReceive(firstOfficeCode, secondOfficeCode, string.Empty));
        for (int i = 0; i < 5; i++)
        {
            count[i] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        string startYearText = yearDropdown.GetComponentInChildren<Text>().text;
        string startMonthText = monthDropdown.GetComponentInChildren<Text>().text;
        string startDayText = dayDropdown.GetComponentInChildren<Text>().text;

        string startDate = startYearText.Equals("년도") ? string.Empty : startYearText;
        startDate += startDate.Equals(string.Empty) || startMonthText.Equals("월") ? string.Empty : "-" + startMonthText;
        startDate += startDate.Equals(string.Empty) || startDayText.Equals("일") ? string.Empty : "-" + startDayText;

        StartCoroutine(vehicleDataCenter.RegionTimeQuantityDataReceive(firstOfficeCode, secondOfficeCode, startDate));
    }

    public void HandleReceivedData(UnityWebRequest response)
    {
        int firstIndex = 0;
        int secondIndex = 0;

        string dataString = response.downloadHandler.text;
        Debug.Log(dataString);

        string[] preReceiptDataTime = dataString.Split("\"receiptDateTime\":");
        string[] prePackageStatus = dataString.Split("\"packageStatus\":");

        for(int i = 1; i < preReceiptDataTime.Length; i++)
        {
            string[] receiptDataTime = preReceiptDataTime[i].Split(',');
            string[] packageStatus = prePackageStatus[i].Split('}');

            receiptDataTime[0] = receiptDataTime[0].Trim('"');
            packageStatus[0] = packageStatus[0].Trim('"');

            string format = "yyyy-MM-ddTHH:mm:ss.fff'Z'";
            DateTime dataDateTime = DateTime.ParseExact(receiptDataTime[0], format, CultureInfo.InvariantCulture);

            if(dataDateTime.TimeOfDay < standardDateTime[0].TimeOfDay)
            {
                firstIndex = 0;
                switch(packageStatus[0])
                {
                    case "일반소포":
                        secondIndex = 0;
                        break;
                    case "창구소포":
                        secondIndex = 1;
                        break;
                    case "국내특급-소포":
                        secondIndex = 2;
                        break;
                    case "방문소포":
                        secondIndex = 3;
                        break;
                    case "국제특급":
                        secondIndex = 4;
                        break;
                }
            }
            else if (standardDateTime[0].TimeOfDay <= dataDateTime.TimeOfDay && dataDateTime.TimeOfDay < standardDateTime[1].TimeOfDay)
            {
                firstIndex = 1;
                switch (packageStatus[0])
                {
                    case "일반소포":
                        secondIndex = 0;
                        break;
                    case "창구소포":
                        secondIndex = 1;
                        break;
                    case "국내특급-소포":
                        secondIndex = 2;
                        break;
                    case "방문소포":
                        secondIndex = 3;
                        break;
                    case "국제특급":
                        secondIndex = 4;
                        break;
                }
            }
            else if (standardDateTime[1].TimeOfDay <= dataDateTime.TimeOfDay && dataDateTime.TimeOfDay < standardDateTime[2].TimeOfDay)
            {
                firstIndex = 2;
                switch (packageStatus[0])
                {
                    case "일반소포":
                        secondIndex = 0;
                        break;
                    case "창구소포":
                        secondIndex = 1;
                        break;
                    case "국내특급-소포":
                        secondIndex = 2;
                        break;
                    case "방문소포":
                        secondIndex = 3;
                        break;
                    case "국제특급":
                        secondIndex = 4;
                        break;
                }
            }
            else if (standardDateTime[2].TimeOfDay <= dataDateTime.TimeOfDay && dataDateTime.TimeOfDay < standardDateTime[3].TimeOfDay)
            {
                firstIndex = 3;
                switch (packageStatus[0])
                {
                    case "일반소포":
                        secondIndex = 0;
                        break;
                    case "창구소포":
                        secondIndex = 1;
                        break;
                    case "국내특급-소포":
                        secondIndex = 2;
                        break;
                    case "방문소포":
                        secondIndex = 3;
                        break;
                    case "국제특급":
                        secondIndex = 4;
                        break;
                }
            }
            else if (standardDateTime[3].TimeOfDay <= dataDateTime.TimeOfDay && dataDateTime.TimeOfDay < standardDateTime[4].TimeOfDay)
            {
                firstIndex = 4;
                switch (packageStatus[0])
                {
                    case "일반소포":
                        secondIndex = 0;
                        break;
                    case "창구소포":
                        secondIndex = 1;
                        break;
                    case "국내특급-소포":
                        secondIndex = 2;
                        break;
                    case "방문소포":
                        secondIndex = 3;
                        break;
                    case "국제특급":
                        secondIndex = 4;
                        break;
                }
            }
            else if (standardDateTime[4].TimeOfDay <= dataDateTime.TimeOfDay && dataDateTime.TimeOfDay < standardDateTime[5].TimeOfDay)
            {
                firstIndex = 5;
                switch (packageStatus[0])
                {
                    case "일반소포":
                        secondIndex = 0;
                        break;
                    case "창구소포":
                        secondIndex = 1;
                        break;
                    case "국내특급-소포":
                        secondIndex = 2;
                        break;
                    case "방문소포":
                        secondIndex = 3;
                        break;
                    case "국제특급":
                        secondIndex = 4;
                        break;
                }
            }
            else if (standardDateTime[5].TimeOfDay <= dataDateTime.TimeOfDay && dataDateTime.TimeOfDay < standardDateTime[6].TimeOfDay)
            {
                firstIndex = 6;
                switch (packageStatus[0])
                {
                    case "일반소포":
                        secondIndex = 0;
                        break;
                    case "창구소포":
                        secondIndex = 1;
                        break;
                    case "국내특급-소포":
                        secondIndex = 2;
                        break;
                    case "방문소포":
                        secondIndex = 3;
                        break;
                    case "국제특급":
                        secondIndex = 4;
                        break;
                }
            }
            else if (standardDateTime[6].TimeOfDay <= dataDateTime.TimeOfDay && dataDateTime.TimeOfDay < standardDateTime[7].TimeOfDay)
            {
                firstIndex = 7;
                switch (packageStatus[0])
                {
                    case "일반소포":
                        secondIndex = 0;
                        break;
                    case "창구소포":
                        secondIndex = 1;
                        break;
                    case "국내특급-소포":
                        secondIndex = 2;
                        break;
                    case "방문소포":
                        secondIndex = 3;
                        break;
                    case "국제특급":
                        secondIndex = 4;
                        break;
                }
            }

            count[secondIndex][firstIndex] += 1;
        }
        SetData(count);
    }
}
