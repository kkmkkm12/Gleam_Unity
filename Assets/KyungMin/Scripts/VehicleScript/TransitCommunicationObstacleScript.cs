using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitCommunicationObstacleScript : MonoBehaviour
{
    string prefabName = "ObstacleDataPrefab";
    public VehicleDataCenter vehicleDataCenter;

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

    public void OnclickSearchBtn()
    {
        StartCoroutine(vehicleDataCenter.TransitCommunicationObstacleDataReceive("2024-10-15", "2024-10-16", "0300", "0200"));
    }

    public void HandleReceivedData(string response)
    {
        Debug.Log("������ �ް����� �Ľ� �ؾ���.");
    }
}
