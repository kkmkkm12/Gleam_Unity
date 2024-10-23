using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveQuantityScript : MonoBehaviour
{
    string prefabName = "������ ���/PostQuantityPrefab";
    public VehicleDataCenter vehicleDataCenter;



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
        StartCoroutine(vehicleDataCenter.ArriveQuantityDataReceive("0003", "2024-10-15", "2024-10-16"));
    }

    public void HandleReceivedData(string response)
    {
        Debug.Log("������ �ް����� �Ľ� �ؾ���.");
    }
}
