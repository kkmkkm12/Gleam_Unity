using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VehicleScript : MonoBehaviour
{
    public Image CarListScrollImage;
    public CarList CarListBtn_Script;
    public Transform MapImage;
    public MapAPITest MapAPITest;

    private bool mapSee = false;

    public Transform NaviBtnSet;

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
        MapAPITest.CarInfoOpen();
    }

    public void OnClickTotalMonitoring()
    {
        ColorSetting();
        TotalMonitoringSet.SetActive(true);
        TransitCommunicationObstacleSet.SetActive(false);
        RegionTimeQuantitySet.SetActive(false);
        ArriveQuantitySet.SetActive(false);
        OperationDetailSet.SetActive(false);
        TransitStatusSet.SetActive(false);

        CarListScrollImage.gameObject.SetActive(false);
        CarListBtn_Script.DontUpdateCarList();
        MapImage.gameObject.SetActive(false);
        MapAPITest.carInfoClose();
    }
    public void OnClickTransitCommunication()
    {
        ColorSetting();
        TotalMonitoringSet.SetActive(false);
        TransitCommunicationObstacleSet.SetActive(true);
        RegionTimeQuantitySet.SetActive(false);
        ArriveQuantitySet.SetActive(false);
        OperationDetailSet.SetActive(false);
        TransitStatusSet.SetActive(false);

        CarListScrollImage.gameObject.SetActive(false);
        CarListBtn_Script.DontUpdateCarList();
        MapImage.gameObject.SetActive(false);
        MapAPITest.carInfoClose();
    }
    public void OnClickRegionTime()
    {
        ColorSetting();
        TotalMonitoringSet.SetActive(false);
        TransitCommunicationObstacleSet.SetActive(false);
        RegionTimeQuantitySet.SetActive(true);
        ArriveQuantitySet.SetActive(false);
        OperationDetailSet.SetActive(false);
        TransitStatusSet.SetActive(false);

        CarListScrollImage.gameObject.SetActive(false);
        CarListBtn_Script.DontUpdateCarList();
        MapImage.gameObject.SetActive(false);
        MapAPITest.carInfoClose();
    }
    public void OnClickArriveQuantity()
    {
        ColorSetting();
        TotalMonitoringSet.SetActive(false);
        TransitCommunicationObstacleSet.SetActive(false);
        RegionTimeQuantitySet.SetActive(false);
        ArriveQuantitySet.SetActive(true);
        OperationDetailSet.SetActive(false);
        TransitStatusSet.SetActive(false);

        CarListScrollImage.gameObject.SetActive(false);
        CarListBtn_Script.DontUpdateCarList();
        MapImage.gameObject.SetActive(false);
        MapAPITest.carInfoClose();
    }
    public void OnClickOperationDetail()
    {
        ColorSetting();
        TotalMonitoringSet.SetActive(false);
        TransitCommunicationObstacleSet.SetActive(false);
        RegionTimeQuantitySet.SetActive(false);
        ArriveQuantitySet.SetActive(false);
        OperationDetailSet.SetActive(true);
        TransitStatusSet.SetActive(false);

        CarListScrollImage.gameObject.SetActive(false);
        CarListBtn_Script.DontUpdateCarList();
        MapImage.gameObject.SetActive(false);
        MapAPITest.carInfoClose();
    }
    public void OnClickTransitStatus()
    {
        ColorSetting();
        TotalMonitoringSet.SetActive(false);
        TransitCommunicationObstacleSet.SetActive(false);
        RegionTimeQuantitySet.SetActive(false);
        ArriveQuantitySet.SetActive(false);
        OperationDetailSet.SetActive(false);
        TransitStatusSet.SetActive(true);

        CarListScrollImage.gameObject.SetActive(false);
        CarListBtn_Script.DontUpdateCarList();
        MapImage.gameObject.SetActive(false);
        MapAPITest.carInfoClose();
    }

    public void ColorSetting()
    {
        foreach (Image clearImg in NaviBtnSet.GetComponentsInChildren<Image>())
        {
            clearImg.color = Color.white;
        }
        Image btnImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        btnImage.color = new Color(0.6084906f, 0.9242597f, 1f, 1f);
    }
}
