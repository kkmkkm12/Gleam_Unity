using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleScript : MonoBehaviour
{
    public Image CarListScrollImage;
    public CarList CarListBtn_Script;
    public Transform MapImage;
    private bool mapSee = false;

    public GameObject TotalMonitoringSet;
    public GameObject TransitCommunicationObstacleSet;
    public GameObject RegionTimeQuantitySet;
    public GameObject ArriveQuantitySet;
    public GameObject OperationDetailSet;
    public GameObject TransitStatusSet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickHome()
    {
        TotalMonitoringSet.SetActive(false);
        TransitCommunicationObstacleSet.SetActive(false);
        RegionTimeQuantitySet.SetActive(false);
        ArriveQuantitySet.SetActive(false);
        OperationDetailSet.SetActive(false);
        TransitStatusSet.SetActive(false);

        CarListScrollImage.gameObject.SetActive(true);
        CarListBtn_Script.UpdateCarList();
        MapImage.gameObject.SetActive(true);
    }

    public void OnClickTotalMonitoring() //종합 상황 모니터링 버튼
    {
        TotalMonitoringSet.SetActive(true);
        TransitCommunicationObstacleSet.SetActive(false);
        RegionTimeQuantitySet.SetActive(false);
        ArriveQuantitySet.SetActive(false);
        OperationDetailSet.SetActive(false);
        TransitStatusSet.SetActive(false);

        CarListScrollImage.gameObject.SetActive(false);
        CarListBtn_Script.DontUpdateCarList();
        MapImage.gameObject.SetActive(false);
    }
    public void OnClickTransitCommunication() //운송소통 장애현황 버튼
    {
        TotalMonitoringSet.SetActive(false);
        TransitCommunicationObstacleSet.SetActive(true);
        RegionTimeQuantitySet.SetActive(false);
        ArriveQuantitySet.SetActive(false);
        OperationDetailSet.SetActive(false);
        TransitStatusSet.SetActive(false);

        CarListScrollImage.gameObject.SetActive(false);
        CarListBtn_Script.DontUpdateCarList();
        MapImage.gameObject.SetActive(false);
    }
    public void OnClickRegionTime() //청별시간대별 접수물량 조회
    {
        TotalMonitoringSet.SetActive(false);
        TransitCommunicationObstacleSet.SetActive(false);
        RegionTimeQuantitySet.SetActive(true);
        ArriveQuantitySet.SetActive(false);
        OperationDetailSet.SetActive(false);
        TransitStatusSet.SetActive(false);

        CarListScrollImage.gameObject.SetActive(false);
        CarListBtn_Script.DontUpdateCarList();
        MapImage.gameObject.SetActive(false);
    }
    public void OnClickArriveQuantity() //집중국별 도착 예정 물량 조회
    {
        TotalMonitoringSet.SetActive(false);
        TransitCommunicationObstacleSet.SetActive(false);
        RegionTimeQuantitySet.SetActive(false);
        ArriveQuantitySet.SetActive(true);
        OperationDetailSet.SetActive(false);
        TransitStatusSet.SetActive(false);

        CarListScrollImage.gameObject.SetActive(false);
        CarListBtn_Script.DontUpdateCarList();
        MapImage.gameObject.SetActive(false);
    }
    public void OnClickOperationDetail()
    {
        TotalMonitoringSet.SetActive(false);
        TransitCommunicationObstacleSet.SetActive(false);
        RegionTimeQuantitySet.SetActive(false);
        ArriveQuantitySet.SetActive(false);
        OperationDetailSet.SetActive(true);
        TransitStatusSet.SetActive(false);

        CarListScrollImage.gameObject.SetActive(false);
        CarListBtn_Script.DontUpdateCarList();
        MapImage.gameObject.SetActive(false);
    }
    public void OnClickTransitStatus()
    {
        TotalMonitoringSet.SetActive(false);
        TransitCommunicationObstacleSet.SetActive(false);
        RegionTimeQuantitySet.SetActive(false);
        ArriveQuantitySet.SetActive(false);
        OperationDetailSet.SetActive(false);
        TransitStatusSet.SetActive(true);

        CarListScrollImage.gameObject.SetActive(false);
        CarListBtn_Script.DontUpdateCarList();
        MapImage.gameObject.SetActive(false);
    }
}
