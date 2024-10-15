using System.Collections;
using System.Collections.Generic; // 이 줄을 추가하세요
using System.Net;
using System.Runtime.InteropServices;
using OSMClient;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
[System.Serializable]
public class Leg
{
    public Step[] steps; // 경로 단계
    public float duration; // 소요 시간
    public float distance; // 거리
}
[System.Serializable]
public class Step
{
    public string name; // 도로 이름
    public float distance; // 거리
    public float duration; // 소요 시간
}
// 경로 응답에 대한 구조체
[System.Serializable]
public class RouteResponse
{
    public Route[] routes;
    public Waypoint[] waypoints;
}

[System.Serializable]
public class Route
{
    public string geometry; // 차량 길찾기 루트
    public float distance; // 차량 거리
    public float duration; // 차량 예상 소요 시간
    public Leg[] legs; //
}

[System.Serializable]
public class Waypoint
{
    public float[] location;
}
[System.Serializable]
public class ResponseData
{
    public Vector2Serializable[] route;
    public Vector2Serializable[] alternativeRoute;
    public float[] startPoint;
    public float[] endPoint;
    public float distance;
    public float alternativeDistance;
    public float duration;
    public float alternativeDuration;
}

[System.Serializable]
public class Vector2Serializable
{
    public float x;
    public float y;

    public Vector2Serializable(Vector2 vector)
    {
        x = vector.x;
        y = vector.y;
    }
}
public class MapAPITest : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void InitNaverMap();
    [DllImport("__Internal")]
    private static extern void AddNaverPolyline(double[] path, int size);

    [DllImport("__Internal")]
    private static extern void InitKakaoMap();
    [DllImport("__Internal")]
    private static extern void AddKakaoPolyline(double[] path, int size);
    public RawImage mapRawImage;

    [Header("맵 정보 입력")]
    //public string strBaseURL = "";
    private string latitude = "";
    private string longitude = "";
    public string level = "";
    public string mapWidth = "";
    public string mapHeight = "";

    private Vector2 touchStartPos;
    private bool isTouching = false;

    [Header("OSRM 길찾기 api")]
    public TMP_InputField originLatInput;
    public TMP_InputField originLonInput;
    public TMP_InputField destLatInput;
    public TMP_InputField destLonInput;
    public Button submitBtn;
    public PoiStartEndController poiStartEndController;
    public List<Vector2> routePoints; // 경로 점 리스트
    public Image CarInfoFather;
    public TMP_Text etaDurationText; // 소요 시간
    public TMP_Text distanceText; // 거리
    public TMP_Text roadNameText; // 해당차량 위치에 있는 도로
    public TMP_Text addressText; // 해당차량 위치에 있는 주소명
    float currDuration;
    float currDistance;
    public float originLon;
    public float originLat;
    public float destLon;
    public float destLat;

    private void Start()
    {
        destLatInput.text = 36.350457876854506.ToString(); //IMC위치 
        destLonInput.text = 127.3848009109497.ToString();
    }

    public void OnClickExternalNaverMap()
    {
        InitNaverMap();
        Debug.Log("네이버 지도를 엽니다");
        // 경로 데이터를 설정합니다.
        double[] path = new double[]
        {
            37.359924641705476, 127.1148204803467,
            37.36343797188166, 127.11486339569092,
            37.368520071054576, 127.11473464965819,
            37.3685882848096, 127.1088123321533,
            37.37295383612657, 127.10876941680907,
            37.38001321351567, 127.11851119995116,
            37.378546827477855, 127.11984157562254,
            37.376637072444105, 127.12052822113036,
            37.37530703574853, 127.12190151214598,
            37.371657839593894, 127.11645126342773,
            37.36855417793982, 127.1207857131958
        };

        // JavaScript 함수 호출
        AddNaverPolyline(path, path.Length);

        Debug.Log("JavaScript 함수 호출합니다");
        // JavaScript 함수 호출
    }
    public void OnClickExternalKakaoMap()
    {
        InitKakaoMap();
        Debug.Log("카카오 지도를 엽니다");
        // 경로 데이터를 설정합니다.
        double[] path = new double[]
        {
            37.359924641705476, 127.1148204803467,
            37.36343797188166, 127.11486339569092,
            37.368520071054576, 127.11473464965819,
            37.3685882848096, 127.1088123321533,
            37.37295383612657, 127.10876941680907,
            37.38001321351567, 127.11851119995116,
            37.378546827477855, 127.11984157562254,
            37.376637072444105, 127.12052822113036,
            37.37530703574853, 127.12190151214598,
            37.371657839593894, 127.11645126342773,
            37.36855417793982, 127.1207857131958
        };

        // JavaScript 함수 호출
        AddKakaoPolyline(path, path.Length);

        Debug.Log("JavaScript 함수 호출합니다");
        // JavaScript 함수 호출
    }
    public void OnClickCloseRawImage()
    {
        mapRawImage.gameObject.SetActive(false);
    }
    public void MapLoader(float currLongitude, float currLatitude)
    {
        mapRawImage.gameObject.SetActive(true);
        StartCoroutine(_MapLoader(currLongitude, currLatitude));
    }

    //hari:서버(node.js)로 맵 요청할때 쓰는 함수
    IEnumerator _MapLoader(float longitude, float latitude)
    {
        // 요청 URL 생성
        string str = $"http://192.168.0.168:3000/api/map/naverMap/getLocation?w={mapWidth}&h={mapHeight}&center={longitude},{latitude}&level={level}";

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(str);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log($"지도 응답을 받았습니다: {request}");
            mapRawImage.texture = DownloadHandlerTexture.GetContent(request);
        }
    }

    public void DistanceLoader(float currLongitude, float currLatitude)
    {
        StartCoroutine(_DistanceLoader(currLongitude, currLatitude));
    }
    IEnumerator _DistanceLoader(float longitude, float latitude)
    {
        string str = $"http://192.168.0.168:3000/api/map/naverMap/getDistance?w={mapWidth}&h={mapHeight}&center={longitude},{latitude}&level={level}";

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(str);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log($"거리 응답을 받았습니다: {request}");
            mapRawImage.texture = DownloadHandlerTexture.GetContent(request);
        }
    }

    public void OnClickCarLocation(float longitude, float latitude, string address)
    {
        string destLatStr = destLatInput.text;
        string destLonStr = destLonInput.text;

        addressText.text = address;

        if (longitude == 0 || latitude == 0) //originLat == "" 이건식으로 비교하면 NullReferenceException이 발생
        {
            originLon = 37.572389f;
            originLat = 126.9769117f;
        }
        else
        {
            originLon = longitude;
            originLat = latitude;
        }
        // float originLon = float.Parse(originLonStr);
        // float originLat = float.Parse(originLatStr);
        // float destLon = float.Parse(destLonStr);
        // float destLat = float.Parse(destLatStr);
        destLon = float.Parse(destLonStr);
        destLat = float.Parse(destLatStr);

        Debug.Log($"OSRMRoute길찾기 서버에 보낼 출발지: {originLat}, {originLon}, 도착지: {destLat}, {destLon}");
        //GetOSRMRoute(originLon, originLat, destLon, destLat);
        StartCoroutine(_GetOSRMRoute(originLon, originLat, destLon, destLat));
    }

    public void OnClickOSRMRouteSubmit()
    {
        string originLatStr = originLatInput.text;
        string originLonStr = originLonInput.text;
        string destLatStr = destLatInput.text;
        string destLonStr = destLonInput.text;

        if (string.IsNullOrEmpty(originLatStr) || string.IsNullOrEmpty(originLonStr)) //originLat == "" 이건식으로 비교하면 NullReferenceException이 발생
        {
            originLatStr = 37.572389.ToString();
            originLonStr = 126.9769117.ToString();
        }
        // float originLon = float.Parse(originLonStr);
        // float originLat = float.Parse(originLatStr);
        // float destLon = float.Parse(destLonStr);
        // float destLat = float.Parse(destLatStr);
        originLon = float.Parse(originLonStr);
        originLat = float.Parse(originLatStr);
        destLon = float.Parse(destLonStr);
        destLat = float.Parse(destLatStr);

        Debug.Log($"OSRMRoute길찾기 서버에 보낼 출발지: {originLat}, {originLon}, 도착지: {destLat}, {destLon}");
        //GetOSRMRoute(originLon, originLat, destLon, destLat);
        StartCoroutine(_GetOSRMRoute(originLon, originLat, destLon, destLat));
    }

    //Haribo: 출발지, 도착지 정보로 길찾기 경로 데이터를 정제
    public IEnumerator _GetOSRMRoute(float originLon, float originLat, float destLon, float destLat)
    {
        string loc = $"{originLon},{originLat};{destLon},{destLat}";
        //alternatives=true 부분을 없애면 대체경로없이 하나경로만 뜸 / overview=full: 해야 전체 경로 다 볼수있음 //출처: https://project-osrm.org/docs/v5.5.1/api/#nearest-service
        //string url = $"http://localhost:5000/route/v1/driving/{loc}?alternatives=true&overview=full";
        //http://localhost:5000/route/v1/driving/{loc}?steps=true&overview=full
        //string url = $"http://localhost:5000/route/v1/driving/127.95576518079,37.40011999044;127.4734,36.27069?steps=true";
        string url = $"http://192.168.0.21:5000/route/v1/driving/{loc}?steps=true&overview=full";
        //string url = $"http://192.168.0.168:5000/route/v1/driving/{loc}?alternatives=false&overview=full";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("osrm서버에 webRequest요청이 실패하였습니다.: " + webRequest.error);
            }
            else
            {
                ProcessOSRMRouteData(webRequest.downloadHandler.text);
            }
        }
    }
    private void ProcessOSRMRouteData(string jsonResponse)
    {
        RouteResponse routeResponse = JsonUtility.FromJson<RouteResponse>(jsonResponse); // JSON 파싱에서 객체에 담아주기

        Debug.Log($"추천하는 경로 수: {routeResponse.routes.Length}"); // 경로 수 확인

        if (routeResponse.routes.Length > 0) // 첫 번째 경로의 점 수 확인
        {
            List<Vector2> routes = DecodePolyline(routeResponse.routes[0].geometry); // 인코딩되는 원리 설명 출처: 
            Debug.Log($"첫번째 추천 경로의 꺾이는 지점 수: {routes.Count}");
            poiStartEndController.HariboSetLocation(originLon, originLat, destLon, destLat, routes);
            routePoints = routes;
            // 경로 점들을 LineRenderer에 설정
            //DrawRoute(routes);//routes[0]:(37.57, 126.98) // poiStartTrans.y, poiStartTrans.x: 37.5723876953125, 126.976913452148

            // 첫 번째 경로의 duration 값 정제하여 출력
            currDuration = routeResponse.routes[0].duration; // duration 값 가져오기
            Debug.Log($"첫번째 추천 경로의 소요 시간: {currDuration / 60f:F2}분"); // 초를 분으로 변환하여 소수점 2자리까지 표시

            string roadName = GetFirstRoadName(routeResponse.routes[0].legs);
            if (string.IsNullOrEmpty(roadName))
            {
                roadName = GetNextRoadName(routeResponse.routes[0].legs);
            }

            if (!string.IsNullOrEmpty(roadName))
            {
                DisplayRoadName(roadName);
            }
        }

        Vector2 startPoint = new Vector2(routeResponse.waypoints[0].location[1], routeResponse.waypoints[0].location[0]);
        Vector2 endPoint = new Vector2(routeResponse.waypoints[1].location[1], routeResponse.waypoints[1].location[0]);
        Debug.Log($"시작점: {startPoint}, 도착점: {endPoint}"); // 시작점과 끝점

        Debug.Log($"첫번째 추천 경로의 거리: {routeResponse.routes[0].distance / 1000f:F4}km"); // 거리 // 미터를 킬로미터로 변환 & 소수점 4자리까지 표시

        if (routeResponse.routes.Length > 1) // 대체 경로가 더 있으면, 대체 경로도 처리해주기(Optional)
        {
            List<Vector2> alternativeRoutes = DecodePolyline(routeResponse.routes[1].geometry);

            // 두 번째 경로의 duration 값 정제하여 출력
            float alternativeDuration = routeResponse.routes[1].duration; // 대체 경로 duration 값 가져오기
            Debug.Log($"두번째 추천 경로의 소요 시간: {alternativeDuration / 60f:F2}분"); // 초를 분으로 변환하여 소수점 2자리까지 표시
        }

        if (routeResponse.routes.Length > 2) // 대체 경로가 3개 이상이고, 더 대체경로 표현해주고 싶으면 여기 코드에 작성하면됨.
        {
            Debug.Log($"대체 경로가 3개 이상입니다. 더 경로를 표현하고 싶다면 여기를 클릭해주세요:)");
        }

        currDistance = routeResponse.routes[0].distance;
        float alternativeDistance = routeResponse.routes.Length > 1 ? routeResponse.routes[1].distance : 0;

        int hours = (int)(currDuration / 60) / 60; // 전체 시
        int minutes = (int)(currDuration / 60) % 60; // 남은 분

        if (hours > 0)
        {
            if (minutes > 0)
            {
                etaDurationText.text = $"{hours}시간 {minutes}분 후 도착예정"; // 1시간 이상일 때
            }
            else
            {
                etaDurationText.text = $"{hours}시간 후 도착예정"; // 분이 0일 때
            }
        }
        else
        {
            etaDurationText.text = $"{minutes}분 후 도착예정"; // 1시간 미만일 때
        }

        distanceText.text = $"거리: {currDistance / 1000f:F2} km"; // 미터를 킬로미터로 변환
        CarInfoFather.enabled = true;
    }

    //OSRM길찾기서버에서 해당차량 위치한 도로명 이름 무엇인지 가져오는 첫번째 함수
    private string GetFirstRoadName(Leg[] legs)
    {
        if (legs.Length > 0 && legs[0].steps.Length > 0)
        {
            return legs[0].steps[0].name; // 첫 번째 단계의 도로 이름 반환
        }
        return null;
    }

    //OSRM길찾기서버에서 해당차량 위치한 도로명 이름 계속 찾는 함수
    private string GetNextRoadName(Leg[] legs)
    {
        for (int i = 0; i < legs.Length; i++)
        {
            foreach (var step in legs[i].steps)
            {
                if (!string.IsNullOrEmpty(step.name))
                {
                    return step.name; // 빈칸이 아닌 첫번째 도로 이름 반환
                }
            }
        }
        return null;
    }

    // 도로 이름을 UI에 띄워주는 함수
    private void DisplayRoadName(string roadName)
    {
        roadNameText.text = $"현재위치 : {roadName}";

    }

    //routeResponse.routes[0].geometry 부분 디코딩 해주는 함수  //C#에서 디코딩하는법 출처: https://gist.github.com/shinyzhu/4617989
    public static List<Vector2> DecodePolyline(string encoded)
    {
        List<Vector2> polyline = new List<Vector2>();
        int index = 0, len = encoded.Length;
        int lat = 0, lon = 0;

        while (index < len)
        {
            int b, shift = 0, result = 0;
            do
            {
                b = encoded[index++] - 63;
                result |= (b & 0x1F) << shift;
                shift += 5;
            }
            while (b >= 0x20);

            int dlat = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
            lat += dlat;

            shift = 0;
            result = 0;
            do
            {
                b = encoded[index++] - 63;
                result |= (b & 0x1F) << shift;
                shift += 5;
            }
            while (b >= 0x20);

            int dlon = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
            lon += dlon;

            float latFloat = lat / 1E5f;
            float lonFloat = lon / 1E5f;
            polyline.Add(new Vector2(latFloat, lonFloat));
        }
        return polyline;
    }
}
