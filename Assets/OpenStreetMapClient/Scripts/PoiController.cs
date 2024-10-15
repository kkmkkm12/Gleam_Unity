using OSMClient;
using System;
using UnityEngine;

public class PoiController : MonoBehaviour
{
    [SerializeField] bool scaleWithMap = false; //마커의 크기가 지도 스케일과 함께 조정될지를 결정하는 불리언 변수
    [SerializeField] float referenceScale = 100; //커의 기본 크기를 설정하는 변수
    Vector2d location; //POI의 위치를 저장하는 Vector2d 변수
    Vector3 initScale; //마커의 초기 크기를 저장하는 Vector3 변수

    void Start()
    {
        //OSMController.Instance.MapChanged += UpdatePosition; //OSMController의 MapChanged 이벤트에 UpdatePosition 메소드를 등록. 지도에 변화가 생기면 이 메소드가 호출
        Init();
    }

    public void Init()//마커의 초기 크기를 설정
    {
        initScale = transform.localScale;
    }

    public void SetLocation()
    {
        SetLocation(OSMController.Instance.Location); //OSMController의 현재 위치를 사용하여 마커를 설정
    }

    public void SetLocation(Vector2d location) //외부에서 주어진 위치 값을 사용하여 마커를 설정
    {
        this.location = location;
        UpdatePosition(OSMController.Instance);
    }

    private void UpdatePosition(OSMController map) //주어진 위도와 경도 값을 기반으로 마커의 위치를 업데이트
    {
        transform.localPosition = map.LocationToPosition(location.y, location.x); //LocationToPosition(): 위도/경도를 월드 좌표로 변환
        Debug.Log($"location.y, location.x: {location.y}, {location.x}");
        Debug.Log(transform.localPosition);

        if (scaleWithMap) //지도 스케일에 따라 마커의 크기를 조정
        {
            var meterSize = map.GetMeterSize();
            transform.localScale = initScale * referenceScale * meterSize;
        }
    }

    private void OnDestroy() //객체가 파괴될 때, 이벤트 핸들러를 해제
    {
        //OSMController.Instance.MapChanged -= UpdatePosition;
    }
}