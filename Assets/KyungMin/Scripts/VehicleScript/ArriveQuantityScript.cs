using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveQuantityScript : MonoBehaviour
{
    string prefabName = "프리펩 경로/PostQuantityPrefab";
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
        StartCoroutine(vehicleDataCenter.ArriveQuantityDataReceive(null, null, null));
    }

    public void HandleReceivedData(string response)
    {
        Debug.Log("데이터 받고나서 파싱 해야함.");
    }
}
