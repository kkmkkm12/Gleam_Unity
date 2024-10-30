using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


[System.Serializable]
public class TransitCommunityInfo
{
    public string dateTime1;
    public string secondOfficeName2;
    public string firstOfficeName3;
    public string secondOfficeName4;
    public string obstacleType5;
    public string departArriveStatus6;
    public string netType7;
    public string obstacleContent8;
    public string obstacleRegion9;
    public string actionContent1;
    public string futurePlan2;
    public string addPerson3;
}

[System.Serializable]
public class TransitCommunityInfoList
{
    public TransitCommunityInfo[] prefabDatas;
}


public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
public class TransitCommunicationObstacleScript : MonoBehaviour
{
    public VehicleDataCenter vehicleDataCenter;

    public GameObject TransitComPrefab;
    private List<TransitCommunityInfo> transitCommunityInfoList;

    public Transform content;

    [Header("조회조건")]
    public Dropdown startYearDropdown;
    public Dropdown startMonthDropdown;
    public Dropdown startDayDropdown;
    public Dropdown endYearDropdown;
    public Dropdown endMonthDropdown;
    public Dropdown endDayDropdown;

    string startDate = string.Empty;
    string endDate = string.Empty;

    [Header("클릭시 출력 왼쪽 데이터")]
    public Text leftFirstLine;
    public Text leftSecondLine1;
    public Text leftSecondLine2;
    public Text leftThirdLine;
    public Text leftFourthLine1;
    public Text leftFourthLine2;
    public Text leftFifthLine;
    public Text leftSixthLine;

    [Header("클릭시 출력 오른쪽 데이터")]
    public Text rightFirstLine;
    public Text rightSecondLine;
    public Text rightThirdLine;
    public Text rightFourthLine1;
    public Text rightFourthLine2;

    [Header("장애내역 출력 데이터")]
    public Text bottomFirstLine;
    public Text bottomSecondLine;
    public Text bottomThirdLine;
    public Text bottomFourthLine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnclickResetBtn()
    {
        startYearDropdown.value = 0;
        startMonthDropdown.value = 0;
        startDayDropdown.value = 0;
        endYearDropdown.value = 0;
        endMonthDropdown.value = 0;
        endDayDropdown.value = 0;
    }

    public void OnclickSearchBtn()
    {
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

        Debug.Log("시작일 : " + startDate + ", 최종일 : " + endDate);
        //StartCoroutine(vehicleDataCenter.TransitCommunicationObstacleDataReceive(null, null, null, null));
       //StartCoroutine(vehicleDataCenter.TransitCommunicationObstacleDataReceive(startDate, endDate, "", ""));
        StartCoroutine(vehicleDataCenter.TransitCommunicationObstacleDataReceive(startDate, endDate, "", ""));
    }

    public void HandleReceivedData(UnityWebRequest response)
    {
        int sn = 1;
        foreach(Transform obj in content.GetComponentsInChildren<Transform>())
        {
            if (content.transform != obj)
                Destroy(obj.gameObject);
        }

        Debug.Log(response.downloadHandler.text);

        TransitCommunityInfo[] dataArray = JsonHelper.FromJson<TransitCommunityInfo>(response.downloadHandler.text);

        // 3. 파싱된 데이터를 리스트로 저장
        transitCommunityInfoList = new List<TransitCommunityInfo>(dataArray);

        // 4. UI에 데이터를 적용
        foreach (TransitCommunityInfo TCI in transitCommunityInfoList)
        {
            GameObject newObj = Instantiate(TransitComPrefab, content);
            TransitCommunicationDataPrefabScript TCDPS = newObj.GetComponent<TransitCommunicationDataPrefabScript>();
            TCDPS.SetData(sn, TCI);
            sn++;
        }
    }
}
