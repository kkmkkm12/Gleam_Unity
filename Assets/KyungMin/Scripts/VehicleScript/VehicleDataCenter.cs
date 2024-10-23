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

    public IEnumerator TotalMoniterDataReceive(string officeCode)   //���ջ�Ȳ ����͸�
    {
        Debug.Log("������ ��! " + officeCode);
        UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/car/TotalMoniter?OfficeCode={officeCode}");       ///car/carView?distance={searchDistance}


        request.SetRequestHeader("Client-Type", "Unity"); //��� �߰�
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            //string response = request.downloadHandler.text;

            FindObjectOfType<TotalMonitoringScript>().HandleReceivedData(request);
        }
    }

    public IEnumerator TransitCommunicationObstacleDataReceive(string startDate, string endDate, string departOfficeCode, string arriveOfficeCode)  //��ۼ��� �����Ȳ
    {
        UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/car/TransitCommunication?startDate={startDate}&endDate={endDate}&departOfficeCode={departOfficeCode}&arriveOfficeCode={arriveOfficeCode}");       ///car/carView?distance={searchDistance}

        request.SetRequestHeader("Client-Type", "Unity"); //��� �߰�
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            //string response = request.downloadHandler.text;

            FindObjectOfType<TransitCommunicationObstacleScript>().HandleReceivedData(request);
        }
    }

    public IEnumerator RegionTimeQuantityDataReceive(string firstOfficeCode, string secondOfficeCode, string dateTime)  //û���ð��뺰 �������� ��ȸ
    {
        UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/car/RegionTime?firstOfficeCode={firstOfficeCode}&secondOfficeCode={secondOfficeCode}&dateTime={dateTime}");       ///car/carView?distance={searchDistance}

        request.SetRequestHeader("Client-Type", "Unity"); //��� �߰�
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            //string response = request.downloadHandler.text;

            FindObjectOfType<RegionTimeQuantityScript>().HandleReceivedData(request);
        }
    }

    public IEnumerator ArriveQuantityDataReceive(string firstOfficeCode, string startDate, string endDate)  //���߱��� ���� �������� ��ȸ
    {
        UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/car/ArriveQuantity?firstOfficeCode={firstOfficeCode}&startDate={startDate}&endDate={endDate}");       ///car/carView?distance={searchDistance}

        request.SetRequestHeader("Client-Type", "Unity"); //��� �߰�
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            //string response = request.downloadHandler.text;

            FindObjectOfType<ArriveQuantityScript>().HandleReceivedData(request);
        }
    }

    public IEnumerator OperationDetailDataReceive(string firstOfficeCode, string secondOfficeCode, string netType)  //������� ����͸�
    {
        UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/car/OperationDetail?firstOfficeCode={firstOfficeCode}&secondOfficeCode={secondOfficeCode}&netType={netType}");       ///car/carView?distance={searchDistance}

        request.SetRequestHeader("Client-Type", "Unity"); //��� �߰�
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            //string response = request.downloadHandler.text;

            FindObjectOfType<OperationDetailScript>().HandleReceivedData(request);
        }
    }
}
