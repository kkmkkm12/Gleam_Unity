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

    [Header("��ȸ����")]
    public Dropdown startYearDropdown;
    public Dropdown startMonthDropdown;
    public Dropdown startDayDropdown;
    public Dropdown endYearDropdown;
    public Dropdown endMonthDropdown;
    public Dropdown endDayDropdown;

    string startDate = string.Empty;
    string endDate = string.Empty;

    [Header("Ŭ���� ��� ���� ������")]
    public Text leftFirstLine;
    public Text leftSecondLine1;
    public Text leftSecondLine2;
    public Text leftThirdLine;
    public Text leftFourthLine1;
    public Text leftFourthLine2;
    public Text leftFifthLine;
    public Text leftSixthLine;

    [Header("Ŭ���� ��� ������ ������")]
    public Text rightFirstLine;
    public Text rightSecondLine;
    public Text rightThirdLine;
    public Text rightFourthLine1;
    public Text rightFourthLine2;

    [Header("��ֳ��� ��� ������")]
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

        string startDate = startYearText.Equals("�⵵") ? string.Empty : startYearText;
        startDate += startDate.Equals(string.Empty) || startMonthText.Equals("��") ? string.Empty : "-" + startMonthText;
        startDate += startDate.Equals(string.Empty) || startDayText.Equals("��") ? string.Empty : "-" + startDayText;

        string endDate = endYearText.Equals("�⵵") ? string.Empty : endYearText;
        endDate += endDate.Equals(string.Empty) || endMonthText.Equals("��") ? string.Empty : "-" + endMonthText;
        endDate += endDate.Equals(string.Empty) || endDayText.Equals("��") ? string.Empty : "-" + endDayText;

        Debug.Log("������ : " + startDate + ", ������ : " + endDate);
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

        // 3. �Ľ̵� �����͸� ����Ʈ�� ����
        transitCommunityInfoList = new List<TransitCommunityInfo>(dataArray);

        // 4. UI�� �����͸� ����
        foreach (TransitCommunityInfo TCI in transitCommunityInfoList)
        {
            GameObject newObj = Instantiate(TransitComPrefab, content);
            TransitCommunicationDataPrefabScript TCDPS = newObj.GetComponent<TransitCommunicationDataPrefabScript>();
            TCDPS.SetData(sn, TCI);
            sn++;
        }
    }
}
