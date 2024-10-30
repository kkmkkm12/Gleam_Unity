using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class DatePhone
{
    public string phoneNum;
    public DateTime dateTime;

    public DatePhone(string phoneNum, DateTime dateTime)
    {
        this.phoneNum = phoneNum;
        this.dateTime = dateTime;
    }
}

[System.Serializable]
public class CarControl  //haribo: 디비에서 가져온 데이터 담아줄 클래스인데 아직 정확히 어떤 필드 선언해줘야될지 결정되지않음.상황따라 필드 빼줄거빼주고 쓸거 넣고 하면 됨.
{
    public string sn;
    public string hpNo;
    public string carNo;
    public string departTime;
    public string departPoint;
    public string arrivePreTime;
    public string arrivePoint;
    //public string preArriveDate;
    public float logisticsWeight;
    public int dockNo;
    public string driveStatus;
    public float currLatitude;
    public float currLongitude;
    public float currDist;
    public string address;
}
[System.Serializable]
public class CarControlList
{
    public CarControl[] cars; // JSON 배열을 담기 위한 필드
    public int totalPages;
}
public class CarList : MonoBehaviour
{
    //private List<CarControl> cachedCarControlList; //계속 DB에 불러올 필요없이 데이터 캐시된거 있으면 캐싱데이터 쓰게끔하기
    public GameObject carInfoPrefab; // 차량 정보 Scroll View 리스트에 쏴줄 객체
    public Transform content; // Scroll View Content
    public Dropdown sort_Dropdown;
    public InputField inputPhoneNum;
    private List<CarControl> carControlList; // 차량 정보 리스트
    public Image CarListImage;
    public Image CarCreateImage;
    public float updateIntervalTime; //몇초마다 서버에 데이터 호출시킬지 정해주는 변수
    public int poolSize;
    private ObjectPool<CarControlUI> carControlPool;
    [Header("책 페이지 UI")]
    int pageIndex = 1;
    [SerializeField]
    int pageSize = 10;
    [SerializeField]                                                                                                          
    TMP_Text pageText;                                                                                                        
    [SerializeField]                                                                                                          
    TMP_Text pageTotalIndexText;                                                                                              
    int pageTotalIndex = 1;
    List<DatePhone> datePhone;
                                                                                                                              
    float searchDistance = 500000;
    string searchRegion = string.Empty;



    private void Start()
    {
        //carControlPool = new ObjectPool<CarControlUI>(carInfoPrefab.GetComponent<CarControlUI>(), pageSize, content); //시작할때 carInfo 풀 하나 생성해주기
        carControlPool = new ObjectPool<CarControlUI>(carInfoPrefab.GetComponent<CarControlUI>(), poolSize, content); //시작할때 carInfo 풀 하나 생성해주기

        UpdateCarList();
    }
    
    public void OnClickCarListLoader()
    {
        CarListImage.gameObject.SetActive(true);
        //UpdateCarList(maxDistance); // 캐시된 데이터 없이 항상 업데이트
    }
    // // 캐시된 데이터가 없으면 업데이트
    // if (cachedCarControlList == null)
    // {
    //     InvokeRepeating("UpdateCarList", 0, updateIntervalTime);// (호출할함수 / 첨시작할 시간 / 몇초간격으로 실행할지 시간가격)
    // }
    // else // 있으면 캐시된 데이터 사용하기
    // {
    //     UpdateUIWithCachedData();
    // }

    // void UpdateUIWithCachedData()
    // {
    //     Debug.Log("DB호출안하고 유니티 캐싱데이터를 이용합니다.");
    //     ResetUIList();

    //     foreach (CarControl carControl in cachedCarControlList)
    //     {
    //         CarControlUI carInfoUI = carControlPool.GetObj();
    //         carInfoUI.transform.SetParent(content, false);
    //         carInfoUI.SetData(carControl);
    //     }
    //     GameManager.Instance.carInfoCount.text = cachedCarControlList.Count.ToString();
    // }
    public void UpdateCarList()
    {
        // 캐시된 데이터가 있다면 초기화
        //cachedCarControlList = null;
        //StartCoroutine(_LoadCarList(pageIndex, pageSize));
        StartCoroutine(_LoadCarList());
    }
    public void DontUpdateCarList()
    {
        StopCoroutine(_LoadCarList());
    }
    //IEnumerator _LoadCarList(int page, int pageSize)
    IEnumerator _LoadCarList()
    {
        UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/car/carView?distance={searchDistance}");

        request.SetRequestHeader("Client-Type", "Unity"); //헤더 추가
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            var response = JsonUtility.FromJson<CarControlList>(request.downloadHandler.text);
            carControlList = new List<CarControl>(response.cars); ;

            datePhone = new List<DatePhone>();
        ResetUIList();

            /// <summary>
            /// 곽경민 : 거리로 값 전달해주면 날라오는 데이터 만큼 만 활성화 시키고 foreach문을
            ///         탈출해서 거리 및 지역으로 필터 선택시 오브젝트 풀 삭제 및 Destroy안됨
            ///         수정 필요
            /// </summary>
            int count = 0;
            foreach (CarControl carControl in carControlList)
            {
                datePhone.Add(new DatePhone(carControl.hpNo, DateTime.Parse(carControl.arrivePreTime)));

                CarControlUI carInfoUI = carControlPool.GetObj();
                carInfoUI.transform.SetParent(content, false);
                carInfoUI.SetData(carControl);

                //kkm 지역 거리 필터링
                if (carControl.address.Contains(searchRegion) && carControl.currDist < searchDistance)
                {
                    carInfoUI.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log("파괴하잣");
                    carControlPool.RemovePool(carInfoUI);
                    Destroy(carInfoUI.gameObject);
                }
                count++;
            }

            for (int i = count; i < content.childCount; i++)
            {
                //CarControlUI carInfoUI = content
            }
        }
    }

    /// <summary>
    /// Haribo: 스크롤바 안에 있는 UI 초기화 리셋해주는 함수. 초기화 할때 한번만 씀
    /// </summary>
    public void ResetUIList()
    {
        foreach (Transform child in content)
        {
            CarControlUI carInfoUI = child.GetComponent<CarControlUI>();
            if (carInfoUI != null) //방어로직
            {
                carControlPool.OffActive(carInfoUI); // 오브젝트 풀에 계속 있음. 비활성화만 해줌
            }
            else
            {
                Debug.LogError($"{child}객체가 carInfoUI 컴포넌스가 없습니다. ");
            }
        }
    }

    public void OnClickCloseCarList()
    {
        CarListImage.gameObject.SetActive(false);
    }
    public void OnClickCloseCarCreate()
    {
        CarCreateImage.gameObject.SetActive(false);
    }
    public void OnClickCarCreate()
    {
        StartCoroutine(_CarCreate());
    }
    IEnumerator _CarCreate()
    {
        string url = $"{GameManager.Instance.serverURL}/car/carCreate";
        Debug.Log($"요청할 URL: {url}");

        // POST 요청 생성 (빈 요청 본문)
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.SetRequestHeader("Client-Type", "Unity"); //헤더 추가
        request.downloadHandler = new DownloadHandlerBuffer(); // 응답을 받을 수 있도록 설정

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("데이터 생성중. 서버나 디비 확인해보세요");
        }
    }



    public void OnClickCarRemove()
    {
        StartCoroutine(_CarRemove());
    }

    IEnumerator _CarRemove()
    {
        string url = $"{GameManager.Instance.serverURL}/car/carDelete";
        Debug.Log($"요청할 URL: {url}");

        // POST 요청 생성 (빈 요청 본문)
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.SetRequestHeader("Client-Type", "Unity"); //헤더 추가
        request.downloadHandler = new DownloadHandlerBuffer(); // 응답을 받을 수 있도록 설정

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("차량 데이터를 서버에서 삭제하였습니다.");
        }
    }

    public void OnClickGetDistance()
    {
        var (currentLongitude, currentLatitude) = GameManager.Instance.GetCurrentLocationInfo();

        Debug.Log($"IMC(도착지)까지 남은 거리를 구하려는 차량의 현재 경도: {currentLongitude} / 현재 위도: {currentLatitude}");

        GameManager.Instance.OnClickGetDistanceOnNaverMap(currentLongitude, currentLatitude);
    }
    // public void OnClickPage(int pageDirection) // 0: left, 1: right
    // {
    //     if (pageDirection == 0)
    //     {
    //         if (pageIndex > 1) // 페이지 인덱스가 1보다 클 때만 감소
    //         {
    //             pageIndex--;
    //         }
    //         else
    //         {
    //             Debug.Log("현재 페이지는 첫 페이지입니다.");
    //             return; // 페이지 인덱스가 1 이하일 경우 더 이상 진행하지 않음
    //         }
    //     }
    //     else if (pageDirection == 1)
    //     {
    //         pageIndex++;
    //     }
    //     else
    //     {
    //         Debug.LogError("왼쪽, 오른쪽 버튼 이외의 버튼을 눌렀습니다. 페이지 버튼을 다시 확인해주세요.");
    //         return;
    //     }
    //     pageText.text = pageIndex.ToString(); // 페이지 텍스트 업데이트
    //     UpdateCarList(); // 페이지 변경 시 새로운 데이터 로드
    // }

    public void SortTime_Dropdown()
    {
        int selectBtn = sort_Dropdown.value;

        GameObject[] childObjects = new GameObject[content.transform.childCount];

        for (int i = 0; i < content.transform.childCount; i++)
        {
            childObjects[i] = content.transform.GetChild(i).gameObject;
        }

        List<int> originalIndices = new List<int>();
        for (int i = 0; i < datePhone.Count; i++)
        {
            originalIndices.Add(i);
        }

        switch (selectBtn)
        {
            case 1:
                // 오름차순 정렬 함수
                for(int i = 0; i < datePhone.Count; i++)
                {
                    for(int j = i + 1; j < datePhone.Count; j++)
                    {
                        if (datePhone[i].dateTime > datePhone[j].dateTime)
                        {
                            //리스트에서 우선 변경 후
                            DatePhone temp = datePhone[i];
                            datePhone[i] = datePhone[j];
                            datePhone[j] = temp;

                            //인덱스 변경
                            int tempIndex = originalIndices[i];
                            originalIndices[i] = originalIndices[j];
                            originalIndices[j] = tempIndex;
                        }
                    }
                }

                for (int i = 0; i < originalIndices.Count; i++)
                {
                    childObjects[originalIndices[i]].transform.SetSiblingIndex(i + 1); // 인덱스를 하이어라키에 적용
                }
                break;
            case 2:
                // 정렬 함수
                for (int i = 0; i < datePhone.Count; i++)
                {
                    for (int j = i + 1; j < datePhone.Count; j++)
                    {
                        if (datePhone[i].dateTime < datePhone[j].dateTime)
                        {
                            //리스트에서 우선 변경 후
                            DatePhone temp = datePhone[i];
                            datePhone[i] = datePhone[j];
                            datePhone[j] = temp;

                            //hierarchy 창에서 위치 변경
                            int tempIndex = originalIndices[i];
                            originalIndices[i] = originalIndices[j];
                            originalIndices[j] = tempIndex;
                        }
                    }
                }

                for (int i = 0; i < originalIndices.Count; i++)
                {
                    childObjects[originalIndices[i]].transform.SetSiblingIndex(i + 1); // 인덱스를 하이어라키에 적용
                }
                break;
        }
    }

    public void OnclickSearchBtn()
    {
        string searchPhoneNum = inputPhoneNum.text;

        GameObject[] childObjects = new GameObject[content.transform.childCount];
        for (int i = 0; i < content.transform.childCount; i++)
        {
            childObjects[i] = content.transform.GetChild(i).gameObject;
            if (searchPhoneNum.Equals(string.Empty))
            {
                childObjects[i].SetActive(true);
            }
            else if (!searchPhoneNum.Equals(string.Empty))
            {
                if (datePhone[i].phoneNum.Contains(searchPhoneNum))
                {
                    childObjects[i].SetActive(true);
                }
                else
                {
                    childObjects[i].SetActive(false);
                }
            }
        }
    }
}

//하리보: 계속 서버로 2.5초 동안 호출해주는 함수
// IEnumerator _LoadInitCarList()
// {
//     UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/carView");
//     yield return request.SendWebRequest();

//     if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
//     {
//         Debug.Log(request.error);
//     }
//     else
//     {
//         // JSON 문자열을 CarInfo 배열로 변환
//         CarInfoList carInfoListWrapper = JsonUtility.FromJson<CarInfoList>(request.downloadHandler.text);
//         carInfoList = new List<CarInfo>(carInfoListWrapper.cars); //호출할때마다 초기화

//         Debug.Log($"_LoadInitCarList(): 차량 리스트의 개수: {carInfoList.Count}");

//         ResetUIList(); // UI 초기화

//         // 처음 차량 정보 UI에 추가해줌
//         foreach (CarInfo carInfo in carInfoList)
//         {
//             CarInfoUI carInfoUI = carInfoPool.GetObj();
//             carInfoUI.transform.SetParent(content, false); //비활성화로 해서 콘텐츠 안에 넣어주기
//             carInfoUI.SetData(carInfo);
//         }
//         GameManager.Instance.carInfoCount.text = carInfoList.Count.ToString(); //텍스트 몇개인지 업데이트
//     }
//     // while문 안쓰고, 유니티에서 계속 호출 반복해주는 함수 있음 //출처: https://docs.unity3d.com/ScriptReference/MonoBehaviour.InvokeRepeating.html
//     InvokeRepeating("UpdateCarList", updateIntervalTime, updateIntervalTime); //(호출할함수 / 처음 시작할 시간 / 호출 간격 시간)
// }

// void UpdateCarList() { StartCoroutine(_UpdateCarList()); }

// //하리보: 계속 서버로 2.5초 동안 호출해주는 함수
// IEnumerator _UpdateCarList()
// {
//     UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/carView");

//     yield return request.SendWebRequest();

//     if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
//     {
//         Debug.Log(request.error);
//     }
//     else
//     {
//         // JSON 문자열을 CarInfo 배열로 변환
//         CarInfoList carInfoListWrapper = JsonUtility.FromJson<CarInfoList>(request.downloadHandler.text);
//         carInfoList = new List<CarInfo>(carInfoListWrapper.cars); //호출할때마다 초기화

//         ResetUIList(); // UI 초기화

//         // 처음 차량 정보 UI에 추가해줌
//         foreach (CarInfo carInfo in carInfoList)
//         {
//             CarInfoUI carInfoUI = carInfoPool.GetObj();
//             carInfoUI.transform.SetParent(content, false);
//             carInfoUI.SetData(carInfo);
//         }
//         GameManager.Instance.carInfoCount.text = carInfoList.Count.ToString(); //텍스트 몇개인지 업데이트
//         Debug.Log($"carInfoPool에 있는 총 갯수: {carInfoPool.GetCountInPool()}");
//         Debug.Log($"content에 있는 프리펩 갯수들: {content.transform.childCount}");
//         // for (int i = 0; i < carInfoList.Count; i++)
//         // {
//         //     CarInfoUI carInfoUI = carInfoPool.GetObj();
//         //     carInfoUI.transform.SetParent(content, false);
//         //     carInfoUI.SetData(carInfoList[i]);
//         // }
//     }
// }
//하리보: 처음 시작으로 리스트 로드할때 서버로 호출하는 함수(한번만 호출됨)
// IEnumerator _LoadInitCarList()
// {
//     UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/carView");
//     yield return request.SendWebRequest();

//     if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
//     {
//         Debug.Log(request.error);
//     }
//     else
//     {
//         // JSON 문자열을 CarInfo 배열로 변환
//         CarInfoList carInfoListWrapper = JsonUtility.FromJson<CarInfoList>(request.downloadHandler.text);
//         carInfoList = new List<CarInfo>(carInfoListWrapper.cars); //호출할때마다 초기화

//         Debug.Log($"_LoadInitCarList(): 차량 리스트의 개수: {carInfoList.Count}");

//         ResetUIList(); // UI 초기화

//         // 처음 차량 정보 UI에 추가해줌
//         foreach (CarInfo carInfo in carInfoList)
//         {
//             int carInfoCount = 0;
//             GameObject carInfoUI = Instantiate(carInfoPrefab, content);
//             CarInfoUI carInfoUIComponent = carInfoUI.GetComponent<CarInfoUI>();
//             carInfoCount++;
//             carInfoUIComponent.SetData(carInfo, carInfoCount);
//         }
//     }
//     // while문 안쓰고, 유니티에서 계속 호출 반복해주는 함수 있음 //출처: https://docs.unity3d.com/ScriptReference/MonoBehaviour.InvokeRepeating.html
//     InvokeRepeating("UpdateCarList", updateIntervalTime, updateIntervalTime); //(호출할함수 / 처음 시작할 시간 / 호출 간격 시간)
// }

// void UpdateCarList() { StartCoroutine(_UpdateCarList()); }

// //하리보: 계속 서버로 2.5초 동안 호출해주는 함수
// IEnumerator _UpdateCarList()
// {
//     UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/carView");

//     yield return request.SendWebRequest();

//     if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
//     {
//         Debug.Log(request.error);
//     }
//     else
//     {
//         // JSON 문자열을 CarInfo 배열로 변환
//         CarInfoList carInfoListWrapper = JsonUtility.FromJson<CarInfoList>(request.downloadHandler.text);
//         carInfoList = new List<CarInfo>(carInfoListWrapper.cars); //호출할때마다 초기화
//         Debug.Log($"_UpdateCarList(): 차량 리스트의 개수: {carInfoList.Count}");

//         ResetUIList(); // UI 초기화

//         // 처음 차량 정보 UI에 추가해줌
//         foreach (CarInfo carInfo in carInfoList)
//         {
//             int carInfoCount = 0;
//             GameObject carInfoUI = Instantiate(carInfoPrefab, content);
//             CarInfoUI carInfoUIComponent = carInfoUI.GetComponent<CarInfoUI>();
//             carInfoCount++;
//             carInfoUIComponent.SetData(carInfo, carInfoCount);
//         }
//     }
// }

// public void ResetUIList()// 스크롤바 안에 있는 콘텐츠 초기화 리셋해주는 함수
// {
//     foreach (Transform child in content)
//     {
//         Destroy(child.gameObject); ///xxx
//     }
// }

//하리보: 실시간 동기화 안하고 그냥 로딩만 하는 함수
// IEnumerator _CarListLoader()
// {
//     string url = $"{GameManager.Instance.serverURL}/carView";
//     Debug.Log($"요청할 URL: {url}");

//     UnityWebRequest request = UnityWebRequest.Get(url);
//     yield return request.SendWebRequest();

//     if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
//     {
//         Debug.Log(request.error);
//     }
//     else
//     {
//         // JSON 문자열을 CarInfo 배열로 변환
//         CarInfoList carInfoListWrapper = JsonUtility.FromJson<CarInfoList>(request.downloadHandler.text);
//         carInfoList = new List<CarInfo>(carInfoListWrapper.cars);
//         Debug.Log($"차량 리스트의 개수: {carInfoList.Count}");

//         ResetUIList(); // UI 초기화

//         for (int i = 0; i < carInfoList.Count; i++)// 데이터 확인 및 UI에 추가
//         {
//             GameObject carInfoUI = Instantiate(carInfoPrefab, content);
//             CarInfoUI carInfo = carInfoUI.GetComponent<CarInfoUI>();
//             carInfo.SetData(carInfoList[i]);
//         }
//     }
// }

// else
// {
//     CarInfoList newCarInfoListWrapper = new CarInfoList();
//     newCarInfoListWrapper = JsonUtility.FromJson<CarInfoList>(request.downloadHandler.text);
//     Debug.Log($" _UpdateCarList()에서 호출한 CarInfoList 갯수들 : {newCarInfoListWrapper.cars.Count()}");
//     //CarInfoList carInfoListWrapper = JsonUtility.FromJson<CarInfoList>(request.downloadHandler.text);
//     foreach (CarInfo TempCarInfo in newCarInfoListWrapper.cars)
//     {//하리보: 중복 데이터가 아닐때만 추가하는 로직. 일단 갯수로만 중복체크했는데, 데이터 다 조사해서 없는 id있을때 중복하는게 더 나은지 확인바람
//         //if (!carInfoList.Exists(originCarInfo => originCarInfo.carId == TempCarInfo.carId))
//         if (carInfoList.Count != newCarInfoListWrapper.cars.Count()) //TODO_하리보
//         {
//             carInfoList.Add(TempCarInfo); // 유니티 carInfoList에 추가

//             Debug.Log("새로운 데이터가 추가되었습니다! 얏호");
//             GameObject carInfoUI = Instantiate(carInfoPrefab, content); // UI에도 추가해줘야함
//             CarInfoUI carInfoUIComponent = carInfoUI.GetComponent<CarInfoUI>();
//             carInfoCount++;
//             carInfoUIComponent.SetData(TempCarInfo, carInfoCount);
//         }
//         else
//         {
//             Debug.Log(carInfoList.Exists(originCarInfo => originCarInfo.carId == TempCarInfo.carId));
//             Debug.Log("새로운 데이터가 추가되지 않았습니다. 똑같아요:(");
//         }
//     }
// }

