using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // 정적 인스턴스

    public string serverURL;
    //public Button OpenNaverMapButton;
    public MapAPITest mapAPITest;
    public CarList carList;

    float currLongitude;
    float currLatitude;
    string currAddress;

    void Awake()
    {
        if (Instance != null && Instance != this)// 이미 인스턴스 존재하면 현재 오브젝트 파괴(싱글톤 기능)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this; //싱글톤 인스턴스를 설정

        DontDestroyOnLoad(gameObject);// 이 오브젝트가 파괴되지 않도록 설정 (해도되고 안되도 되는옵션.)
    }

    void Start()
    {
        Init();
    }

    void Init() //초기화 해주는 함수
    {

    }
    public void OnClickOpenNaverMap(float longitude, float latitude, string address)
    {
        mapAPITest.MapLoader(longitude, latitude);
        UpdateCurrentLocationInfo(longitude, latitude, address);
    }

    public void OpenMap(float longitude, float latitude, string address)
    {
        mapAPITest.OnClickCarLocation(longitude, latitude, address);
        UpdateCurrentLocationInfo(longitude, latitude, address);
    }
    public void UpdateCurrentLocationInfo(float longitude, float latitude, string address)
    {
        currLongitude = longitude;
        currLatitude = latitude;
        currAddress = address;

        Debug.Log($"현재 보고있는 위도와 경도(GameManager): {currLongitude} / {currLatitude}");
    }

    public (float Longitude, float Latitude) GetCurrentLocationInfo()
    {
        return (currLongitude, currLatitude);
    }

    public void OnClickGetDistanceOnNaverMap(float longitude, float latitude)
    {
        mapAPITest.DistanceLoader(longitude, latitude);
    }

    public void OnClickOpenCarList()
    {
        carList.OnClickCarListLoader();
    }
}
