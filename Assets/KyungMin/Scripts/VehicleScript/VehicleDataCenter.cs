using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

public class VehicleDataCenter : MonoBehaviour
{
    public string ServerURL;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator TotalMoniterDataReceive(string officeCode)   //종합상황 모니터링
    {
        Debug.Log($"서버에 요청할 officeCode: {officeCode}");
        UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/car/TotalMoniter?OfficeCode={officeCode}");       ///car/carView?distance={searchDistance}

        request.SetRequestHeader("Client-Type", "Unity"); //헤더 추가
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.Log("TotalMoniterDataReceive: " + response);

            FindObjectOfType<TotalMonitoringScript>().HandleReceivedData(response);
        }
    }

    public IEnumerator TransitCommunicationObstacleDataReceive(string startDate, string endDate, string departOfficeCode, string arriveOfficeCode)  //운송소통 장애현황
    {
        UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/car/TransitCommunication?startDate={startDate}&endDate={endDate}&departOfficeCode={departOfficeCode}&arriveOfficeCode={arriveOfficeCode}");       ///car/carView?distance={searchDistance}

        request.SetRequestHeader("Client-Type", "Unity"); //헤더 추가
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.Log("TransitCommunicationObstacleDataReceive: " + response);

            FindObjectOfType<TransitCommunicationObstacleScript>().HandleReceivedData(response);
        }
    }

    public IEnumerator RegionTimeQuantityDataReceive(string firstOfficeCode, string secondOfficeCode, string dateTime)  //청별시간대별 접수물량 조회
    {
        UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/car/RegionTime?firstOfficeCode={firstOfficeCode}&secondOfficeCode={secondOfficeCode}&dateTime={dateTime}");       ///car/carView?distance={searchDistance}

        request.SetRequestHeader("Client-Type", "Unity"); //헤더 추가
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.Log("RegionTimeQuantity: " + response);

            FindObjectOfType<RegionTimeQuantityScript>().HandleReceivedData(response);
        }
    }

    public IEnumerator ArriveQuantityDataReceive(string firstOfficeCode, string startDate, string endDate)  //집중국별 도착 에정물량 조회
    {
        UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/car/ArriveQuantity?firstOfficeCode={firstOfficeCode}&startDate={startDate}&endDate={endDate}");       ///car/carView?distance={searchDistance}

        request.SetRequestHeader("Client-Type", "Unity"); //헤더 추가
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.Log("ArriveQuantity: " + response);

            FindObjectOfType<ArriveQuantityScript>().HandleReceivedData(response);
        }
    }

    public IEnumerator OperationDetailDataReceive(string firstOfficeCode, string secondOfficeCode, string netType)  //운행사항 모니터링
    {
        UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/car/OperationDetail?firstOfficeCode={firstOfficeCode}&secondOfficeCode={secondOfficeCode}&netType={netType}");       ///car/carView?distance={searchDistance}

        request.SetRequestHeader("Client-Type", "Unity"); //헤더 추가
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.Log("OperationDetail: " + response);

            FindObjectOfType<OperationDetailScript>().HandleReceivedData(response);
        }
    }
}
