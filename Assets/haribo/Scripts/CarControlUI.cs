using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarControlUI : MonoBehaviour
{
    public GameObject DetailPrefab;

    [Header("------------UI 표시")]
    public TMP_Text driverSNText;
    public TMP_Text driverContactText;
    public TMP_Text carNoText;
    public TMP_Text departTimeText;
    public TMP_Text departPointText;
    public TMP_Text arrivalTimeText;
    public TMP_Text arrivalPointText;
    public TMP_Text logisticsWeight;
    public TMP_Text dockNo;
    public TMP_Text driveStatus;
    public TMP_Text currLatitude;
    public TMP_Text currLongitude;
    public TMP_Text currDist;
    public TMP_Text address;
    // public Button myCarBtn;

    // private void Start()
    // {
    //     myCarBtn = GetComponent<Button>();
    // }

    //Haribo: 서버에 가져온 데이터를 유니티 UI로 세팅해주는 함수
    public void SetData(CarControl carControl)
    {

        driverSNText.text = carControl.sn;
        driverContactText.text = carControl.hpNo;
        carNoText.text = carControl.carNo;
        departTimeText.text = carControl.departTime;
        departPointText.text = carControl.departPoint;
        arrivalTimeText.text = carControl.arrivePreTime;
        arrivalPointText.text = carControl.arrivePreTime;

        logisticsWeight.text = carControl.logisticsWeight.ToString();
        dockNo.text = carControl.dockNo.ToString();
        driveStatus.text = carControl.driveStatus;
        currLatitude.text = carControl.currLatitude.ToString();
        currLongitude.text = carControl.currLongitude.ToString();
        currDist.text = carControl.currDist.ToString();
        address.text = carControl.address;
        address.text = FormatAddress(carControl.address);
    }

    //Haribo: 응답받은 주소데이터 유니티UI에 맞게 알맞는 형태로 정제해주는 함수_2024.09.05(경민님, 이 함수 경민님한테 유니티에 필요한대로 바꿔요)
    private string FormatAddress(string address)
    {
        List<string> metropolitanCities = new List<string> // 광역시 목록
    {
        "대전"//, "부산", "대구", "인천", "광주", "울산", "서울"
    };

        string[] parts = address.Split(',');

        for (int i = 0; i < parts.Length; i++)
        {
            string trimmedPart = parts[i].Trim();

            if (metropolitanCities.Contains(trimmedPart))
            {
                parts[i] = trimmedPart + "광역시"; // "광역시" 추가
            }
        }

        return string.Join(", ", parts); // 수정된 주소 반환
    }



    public void OnClickCarLocation()
    {
        Debug.Log("차 위치를 지도에 띄웁니다.");

        float latitude;
        float longitude;

        if (float.TryParse(currLatitude.text, out latitude) && float.TryParse(currLongitude.text, out longitude))
        {
            GameManager.Instance.OnClickOpenNaverMap(longitude, latitude, address.text);
        }
        else
        {
            Debug.LogError("위도 또는 경도 변환에 실패:(");
        }
    }

    public void OnClickGetCarLocation()
    {
        Debug.Log("차 위치를 지도에 띄웁니다.");

        float latitude;
        float longitude;

        if (float.TryParse(currLatitude.text, out latitude) && float.TryParse(currLongitude.text, out longitude))
        {
            //GameManager.Instance.OpenMap(latitude, longitude);
            GameManager.Instance.OpenMap(longitude, latitude, address.text);
        }
        else
        {
            Debug.LogError("위도 또는 경도 변환에 실패:(");
        }

        Canvas parentCanvas = GetComponentInParent<Canvas>();
        Instantiate(DetailPrefab, parentCanvas.transform);
    }
}
