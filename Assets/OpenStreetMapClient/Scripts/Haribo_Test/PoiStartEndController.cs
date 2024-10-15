//PoiStartEndController
using OSMClient;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 테스트용으로 도착지, 목적지의 위치정보를 보내는 함수(테스트 끝나면 삭제)
/// </summary>
public class PoiStartEndController : MonoBehaviour
{
    //[SerializeField] MapAPITest mapAPITest;
    [SerializeField] Transform poiStartTrans; //출발지(차량)
    [SerializeField] Transform poiEndTrans;//도착지(IMC중앙국)
    [SerializeField] Transform poiRouteLineTrans; //길경로
    public LineRenderer lineRenderer; // LineRenderer 컴포넌트
    [SerializeField] bool scaleWithMap = false; //마커의 크기가 지도 스케일과 함께 조정될지를 결정하는 불리언 변수
    [SerializeField] float referenceScale = 100; //커의 기본 크기를 설정하는 변수
    Vector2d location; //POI의 위치를 저장하는 Vector2d 변수
    Vector3 initScale; //마커의 초기 크기를 저장하는 Vector3 변수

    void Start()
    {
        // LineRenderer 초기 설정
        lineRenderer.positionCount = 0;
        lineRenderer.useWorldSpace = true; // 월드 좌표 사용

        OSMController.Instance.MapChanged += UpdatePosition; //OSMController의 MapChanged 이벤트에 UpdatePosition 메소드를 등록. 지도에 변화가 생기면 이 메소드가 호출
        Init();
    }

    public void Init()//마커의 초기 크기를 설정
    {
        initScale = poiStartTrans.localScale;
    }

    // public void SetLocation()
    // {
    //     SetLocation(OSMController.Instance.Location); //OSMController의 현재 위치를 사용하여 마커를 설정
    // }

    public void HariboSetLocation(float originLon, float originLat, float destLon, float destLat, List<Vector2> linePoints)
    {
        lineRenderer.positionCount = linePoints.Count;

        for (int i = 0; i < linePoints.Count; i++)
        {
            // 각 점을 월드 좌표로 변환하여 LineRenderer에 설정
            //Vector3 worldPosition = OSMController.Instance.LocationToPosition(points[i].y, points[i].x);
            Vector3 worldPosition = OSMController.Instance.LocationToPosition(linePoints[i].x, linePoints[i].y);
            lineRenderer.SetPosition(i, worldPosition);
        }

        Debug.Log("osrm서버에서 받아온 길찾기 경로가 라인 렌더러로그려졌습니다:)");


        Vector2d startLocation = new Vector2d(originLon, originLat);
        Vector2d endLocation = new Vector2d(destLon, destLat);
        SetLocation(startLocation, endLocation, linePoints); //OSMController의 현재 위치를 사용하여 마커를 설정
    }

    public void SetLocation(Vector2d startLocation, Vector2d endLocation, List<Vector2> linePoints) //외부에서 주어진 위치 값을 사용하여 마커를 설정
    {
        //this.location = location;
        UpdatePosition(OSMController.Instance, startLocation, endLocation, linePoints);
    }

    private void UpdatePosition(OSMController map, Vector2d startLocation, Vector2d endLocation, List<Vector2> linePoints) //주어진 위도와 경도 값을 기반으로 마커의 위치를 업데이트
    {
        for (int i = 0; i < linePoints.Count; i++)
        {
            // 각 점을 월드 좌표로 변환하여 LineRenderer에 설정
            //Vector3 worldPosition = OSMController.Instance.LocationToPosition(points[i].y, points[i].x);
            Vector3 worldPosition = OSMController.Instance.LocationToPosition(linePoints[i].x, linePoints[i].y);
            lineRenderer.SetPosition(i, worldPosition);
        }

        //transform.localPosition = map.LocationToPosition(location.y, location.x); //LocationToPosition(): 위도/경도를 월드 좌표로 변환
        poiStartTrans.localPosition = map.LocationToPosition(startLocation.y, startLocation.x); //LocationToPosition(): 위도/경도를 월드 좌표로 변환
        poiEndTrans.localPosition = map.LocationToPosition(endLocation.y, endLocation.x); //LocationToPosition(): 위도/경도를 월드 좌표로 변환
        poiRouteLineTrans.localPosition = map.LocationToPosition(endLocation.y, endLocation.x); //LocationToPosition(): 위도/경도를 월드 좌표로 변환
        //Debug.Log($"poiStartTrans.y, poiStartTrans.x: {startLocation.y}, {startLocation.x}");
        //Debug.Log(poiStartTrans.localPosition); 디버그용


        if (scaleWithMap) //지도 스케일에 따라 마커의 크기를 조정
        {
            var meterSize = map.GetMeterSize();
            //transform.localScale = initScale * referenceScale * meterSize;
            poiStartTrans.localScale = initScale * referenceScale * meterSize;
            poiEndTrans.localScale = initScale * referenceScale * meterSize;
        }
    }

    private void OnDestroy() //객체가 파괴될 때, 이벤트 핸들러를 해제
    {
        OSMController.Instance.MapChanged -= UpdatePosition;
    }
}
