using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationDetailScript : MonoBehaviour
{
    public VehicleDataCenter vehicleDataCenter;

    [Header("��ȸ����")]
    public Dropdown firstOfficeDropdown;
    public Dropdown secondOfficeDropdown;
    public Dropdown netType;

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
        StartCoroutine(vehicleDataCenter.OperationDetailDataReceive(null, null, null));
    }

    public void HandleReceivedData(string response)
    {
        Debug.Log("������ �ް��� �Ľ� �ؾ���.");
    }
}
