using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionTimeQuantityScript : MonoBehaviour
{
    public VehicleDataCenter vehicleDataCenter;

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

    public void OnclickSearchBtn()
    {
        StartCoroutine(vehicleDataCenter.RegionTimeQuantityDataReceive(null, null, null));
    }

    public void HandleReceivedData(string response)
    {
        Debug.Log("데이터 받고나서 파싱 해야함.");
    }
}
