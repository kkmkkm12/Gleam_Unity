using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public string ServerURL;                                //서버 주소
    public float RepeatTimer;                               //데이터 받아오기 반복할 시간

    public bool CarDistanceOn = false;                      //차량 인접거리 컨텐츠가 처음 활성화 되었는지 확인
    private Text[] CarInfo;                                 //차량 정보
    private int RecentCount = 0;                            //이전의 데이터 카운트

    private Transform CallObject;                           //다른 오브젝트에 접근을 위한 Transform
    private Transform CarDistanceContent;                   //차량정보 표시 스크롤 뷰의 컨텐츠 객체 Transform
    private Text CarDistanceDropdownText;                   //거리 DropDown에 대표 Text

    public int MaxDockNum = 0;                              //dock의 최대 개수
    public string[][] DockList = new string[14][];          //dock의 개수 = 14

    Queue<GameObject> pool = new Queue<GameObject>();       //pool

    public bool firstStart = true;                          //처음 실행하는지 확인

    private int[] DownPalletArray = new int[5];             //5일치 하차량을 받기위한 배열
    private int[] UpPalletArray = new int[5];               //5일치 상차량을 받기위한 배열
    private int[] TotalPalletArray = new int[5];            //5일치 물동량을 받기위한 배열

    public bool[] belt = { false, false, false, false, false, false, false, false };    //컨베이어 벨트의 느려지는 구간

    private int dataNum = 0;                                //새로 받는 데이터의 개수(처음 쓸대없는 것 포함하여 데이터의 real 수만 하려면 -1 해줘야함)
    private int preDataNum = 0;                             //이전의 데이터 개수(이하 동문 -1)

    Button[] contentChildsBtn;                              //차량 정보에 대한 버튼을 캐싱해 놓는 버튼 배열
    // Start is called before the first frame update
    void Start()
    {
        FindObject();

        ShortDistancePrefabActive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShortDistancePrefabActive()                 //생성된 프리펩의 객체 연결하는 함수
    {
        FindObject();
        foreach (Transform objName in CallObject.GetComponentsInChildren<Transform>())
        {
            if (objName.name.Equals("CarListScroll_Content"))
                CarDistanceContent = objName;
            else if (objName.name.Equals("DistanceRangeLabel_Text"))
                CarDistanceDropdownText = objName.GetComponent<Text>();
        }
        InvokeRepeating("StartRoutine", 0, RepeatTimer);
    }

    private void StartRoutine()                             //반복시킬 코루틴을 포함한 함수
    {
        StartCoroutine(_LoadCarList());
    }
    public void StopRoutine()                               //코루틴을 멈추는 함수
    {
        CancelInvoke("StartRoutine");
        firstStart = true;
    }

    private void FindObject()                       //기준이 되는 오브젝트를 찾는 함수
    {
        bool car = false;
        CallObject = GameObject.Find("MainFunctionArrangement").GetComponent<Transform>();
        foreach (Transform obj in CallObject.GetComponentsInChildren<Transform>())
        {
            if (obj.name.Contains("ShortDistanceVehicle_Prefab"))
            {
                car = true;
                CallObject = obj;
                break;
            }
        }
        if (!car)
        {
            CallObject = CallObject.GetComponentInChildren<Transform>();
            CallObject = CallObject.GetComponentInChildren<Transform>();
        }
    }

    IEnumerator _LoadCarList()                          //데이터 받아 상하차, 물동량, 상황전시, 차량 인접 데이터에 대해 받아오는 함수
    {
        UnityWebRequest request = UnityWebRequest.Get($"{ServerURL}/car/carView");
        DateTime today = DateTime.Today;

        request.SetRequestHeader("Client-Type", "Unity");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string requestText = request.downloadHandler.text.ToString();
            Debug.Log(requestText);

            Array.Fill(DownPalletArray, 0);
            Array.Fill(UpPalletArray, 0);

            UnloadCount(requestText, today);
            LoadCount(requestText, today);
            VolumeCount(today);
            DockListUpdate(requestText);
            CarDistance(requestText, today);
        }
    }

    private void DockListUpdate(string data)                            //상황전시에 들어갈 도크 상태에 대한 데이터를 받는다.
    {
        int temp_MaxNum = 0;
        DockList = new string[14][];
        Array.Fill(DockList, null);

        string[] preDockNo = data.Split("\"dockNo\":");
        string[] preCarNo = data.Split("\"carNo\":");
        string[] preLogisticsWeight = data.Split("\"logisticsWeight\":");
        string[] preDriveStatus = data.Split("\"driveStatus\":");

        for(int i = 1; i < preDockNo.Length; i++)
        {
            string[] dockNo = preDockNo[i].Split(',');
            string[] carNo = preCarNo[i].Split(',');
            string[] logisticsWeight = preLogisticsWeight[i].Split(",");
            string[] driveStatus = preDriveStatus[i].Split(',');

            dockNo[0] = dockNo[0].Trim('"');
            carNo[0] = carNo[0].Trim('"');
            driveStatus[0] = driveStatus[0].Trim('"');
            int temp = int.Parse(dockNo[0]);
            if (temp > temp_MaxNum) temp_MaxNum = temp + 1;

            if (driveStatus[0].Equals("1"))
            {
                DockList[temp] = new string[4];
                DockList[temp][0] = dockNo[0];
                DockList[temp][1] = carNo[0];
                DockList[temp][2] = (int.Parse(logisticsWeight[0])).ToString();         //pallet 갯수의 반절 정도 쓰려고 했는데 더미데이터 pallet 양이 적어서 그냥 씀
                DockList[temp][3] = "하차";
            }
            else if (driveStatus[0].Equals("2"))
            {
                DockList[temp] = new string[4];
                DockList[temp][0] = dockNo[0];
                DockList[temp][1] = carNo[0];
                DockList[temp][2] = (int.Parse(logisticsWeight[0])).ToString();         //pallet 갯수의 반절 정도 쓰려고 했는데 더미데이터 pallet 양이 적어서 그냥 씀
                DockList[temp][3] = "상차";
            }
        }
        MaxDockNum = temp_MaxNum;
    }

    private void UnloadCount(string data, DateTime today)                       //하차데이터를 받는 함수
    {
        if(CallObject != null)
        {
            
            Transform prefabParentParent = CallObject.parent.parent;
            string[] preDepartTime = data.Split("\"departTime\":");
            string[] preLogisticsWeight = data.Split("\"logisticsWeight\":");
            string[] preDriveStatus = data.Split("\"driveStatus\":");
            dataNum = preDepartTime.Length;

            for (int i = 1; i < dataNum; i++)
            {
                string[] departTime = preDepartTime[i].Trim('"').Split(" ");
                DateTime dateTime = DateTime.Parse(departTime[0]);
                if (dateTime < today.AddDays(-4)) continue;

                string[] logisticsWeight = preLogisticsWeight[i].Split(",");

                string[] driveStatus = preDriveStatus[i].Split(",");
                driveStatus[0] = driveStatus[0].Trim('"');

                if (driveStatus[0].Equals("1"))
                {
                    if (dateTime == today)
                    {
                        DownPalletArray[4] += int.Parse(logisticsWeight[0]);
                    }
                    else if (dateTime == today.AddDays(-1))
                    {
                        DownPalletArray[3] += int.Parse(logisticsWeight[0]);
                    }
                    else if (dateTime == today.AddDays(-2))
                    {
                        DownPalletArray[2] += int.Parse(logisticsWeight[0]);
                    }
                    else if (dateTime == today.AddDays(-3))
                    {
                        DownPalletArray[1] += int.Parse(logisticsWeight[0]);
                    }
                    else if (dateTime == today.AddDays(-4))
                    {
                        DownPalletArray[0] += int.Parse(logisticsWeight[0]);
                    }
                }
                else if (driveStatus[0].Equals("13")) continue;                     //운행종료로 판단

            }

            foreach (Transform findUnloadPrefab in prefabParentParent.GetComponentsInChildren<Transform>())
            {
                if (findUnloadPrefab.name.Contains("UnloadingVolume_Prefab") && findUnloadPrefab.Find("BarChart(Clone)") != null)
                {
                    CustomAxisValueFormatterExample UnLoadingGrahp;
                    UnLoadingGrahp = findUnloadPrefab.Find("BarChart(Clone)").GetComponent<CustomAxisValueFormatterExample>();

                    UnLoadingGrahp.DrowGraph(DownPalletArray, transform);
                }
                else if (findUnloadPrefab.name.Contains("UnloadingVolume_Prefab") && findUnloadPrefab.Find("PieChart(Clone)") != null)
                {
                    PieValueController UnLoadingGrahp;
                    UnLoadingGrahp = findUnloadPrefab.Find("PieChart(Clone)").GetComponent<PieValueController>();

                    UnLoadingGrahp.DrowGraph((float)DownPalletArray[4]);
                    Debug.Log("원형! = " + DownPalletArray[4]);
                }
                else
                {
                    
                }
            }
        }
    }

    private void LoadCount(string data, DateTime today)                     //상차데이터를 받는 함수
    {
        if(CallObject != null)
        {
            Transform prefabParentParent = CallObject.parent.parent;

            string[] preDepartTime = data.Split("\"departTime\":");
            string[] preLogisticsWeight = data.Split("\"logisticsWeight\":");
            string[] preDriveStatus = data.Split("\"driveStatus\":");

            for (int i = 1; i < dataNum; i++)
            {
                string[] departTime = preDepartTime[i].Trim('"').Split(" ");
                DateTime dateTime = DateTime.Parse(departTime[0]);
                if (dateTime < today.AddDays(-4)) continue;

                string[] logisticsWeight = preLogisticsWeight[i].Split(",");

                string[] driveStatus = preDriveStatus[i].Split(",");
                driveStatus[0] = driveStatus[0].Trim('"');

                if (driveStatus[0].Equals("2") && dateTime == today)
                {
                    UpPalletArray[4] += int.Parse(logisticsWeight[0]);
                }
                else if (driveStatus[0].Equals("2") && dateTime == today.AddDays(-1))
                {
                    UpPalletArray[3] += int.Parse(logisticsWeight[0]);
                }
                else if (driveStatus[0].Equals("2") && dateTime == today.AddDays(-2))
                {
                    UpPalletArray[2] += int.Parse(logisticsWeight[0]);
                }
                else if (driveStatus[0].Equals("2") && dateTime == today.AddDays(-3))
                {
                    UpPalletArray[1] += int.Parse(logisticsWeight[0]);
                }
                else if (driveStatus[0].Equals("2") && dateTime == today.AddDays(-4))
                {
                    UpPalletArray[0] += int.Parse(logisticsWeight[0]);
                }
                else if (driveStatus[0].Equals("13")) continue;                     //운행종료로 판단
            }

            foreach (Transform findUnloadPrefab in prefabParentParent.GetComponentsInChildren<Transform>())
            {
                if (findUnloadPrefab.name.Contains("LoadingVolume_Prefab") && findUnloadPrefab.Find("BarChart(Clone)") != null)
                {
                    CustomAxisValueFormatterExample LoadingGraph;
                    LoadingGraph = findUnloadPrefab.Find("BarChart(Clone)").GetComponent<CustomAxisValueFormatterExample>();

                    LoadingGraph.DrowGraph(UpPalletArray, transform);
                }
                else if (findUnloadPrefab.name.Contains("LoadingVolume_Prefab") && findUnloadPrefab.Find("PieChart(Clone)") != null)
                {
                    PieValueController LoadingGraph;
                    LoadingGraph = findUnloadPrefab.Find("PieChart(Clone)").GetComponent<PieValueController>();

                    LoadingGraph.DrowGraph((float)UpPalletArray[4]);
                }
                else
                {
                    
                }
            }
        }
    }
    private void VolumeCount(DateTime today)                                //물동량 데이터를 받는 함수
    {
        if(CallObject != null)
        {
            Transform prefabParentParent = CallObject.parent.parent;

            int[] totalPallet = new int[5] { DownPalletArray[0] + UpPalletArray[0],
            DownPalletArray[1] + UpPalletArray[1],
            DownPalletArray[2] + UpPalletArray[2],
            DownPalletArray[3] + UpPalletArray[3],
            DownPalletArray[4] + UpPalletArray[4]};

            foreach (Transform findUnloadPrefab in prefabParentParent.GetComponentsInChildren<Transform>())
            {
                if (findUnloadPrefab.name.Contains("TotalVolume_Prefab") && findUnloadPrefab.Find("BarChart(Clone)") != null)
                {
                    CustomAxisValueFormatterExample LoadingGraph;
                    LoadingGraph = findUnloadPrefab.Find("BarChart(Clone)").GetComponent<CustomAxisValueFormatterExample>();

                    LoadingGraph.DrowGraph(totalPallet, transform);
                }
                else if (findUnloadPrefab.name.Contains("TotalVolume_Prefab") && findUnloadPrefab.Find("PieChart(Clone)") != null)
                {
                    PieValueController LoadingGraph;
                    LoadingGraph = findUnloadPrefab.Find("PieChart(Clone)").GetComponent<PieValueController>();

                    LoadingGraph.DrowGraph((float)totalPallet[4]);
                }
                else
                {

                }
            }
        }
    }

    private void CarDistance(string data, DateTime today)                           //차량 인접거리 데이터를 받는 함수
    {
        bool refrash = false;
        
        if (CarDistanceContent != null && dataNum != preDataNum)
        {
            refrash = true;
            int count = dataNum - 1;
            contentChildsBtn = CarDistanceContent.GetComponentsInChildren<Button>(true);

            foreach (Button carDistanceContentChild in contentChildsBtn)
            {
                if (carDistanceContentChild == CarDistanceContent)
                    continue;

                if (count > 0)
                {
                    ReturnToPool(carDistanceContentChild.gameObject);
                    Debug.Log("생성");
                    count--;
                }
                else
                {
                    Destroy(carDistanceContentChild.gameObject);
                }
            }
        }

        if (CallObject != null && CallObject.gameObject.activeInHierarchy && CarDistanceContent != null)
        {
            string prefabName = "MainContentPrefabs/ShortDistanceInfoButton_Prefab";
            GameObject prefab = Resources.Load<GameObject>(prefabName);
            int dataLength = 0;

            //차량번호, 출발지, 거리 구하는 코드
            string[] preCarNumber = data.Split("\"carNo\":");
            string[] preDepartment = data.Split("\"departPoint\":");
            string[] preDistance = data.Split("\"currDist\":");

            dataLength = preCarNumber.Length - 1;

            string[] realCarNumber = new string[dataLength];
            string[] realDepartment = new string[dataLength];
            string[] realDistance = new string[dataLength];

            //1부터 카운트 하는 이유 : 분할한 0번째 배열에는 키워드 보다 뒤의 값으로 쓸 수 없는 값이 대입 됨
            for (int i = 1; i < preCarNumber.Length; i++)
            {
                /*1번 배열 이후 부터는 다음 키워드 전까지의 값이 대입되어있으므로
                키워드 값을 나누는 값까지의 구분자로 Split 하고 쓸대 없는 기호 Replace로 삭제*/

                //차량 번호 구하는 코드
                string[] carNumber = preCarNumber[i].Split(",");
                realCarNumber[i - 1] = carNumber[0].Trim('"');
                //출발지 구하는 코드
                string[] department = preDepartment[i].Split(",");
                realDepartment[i - 1] = department[0].Trim('"');
                //인접거리 구하는 코드
                realDistance[i - 1] = preDistance[i].Split("}")[0];

                GetPooledObject(prefab);
            }

            if (RecentCount != dataLength || CarDistanceOn)
            {
                CarInfo = CarDistanceContent.GetComponentsInChildren<Text>();
            }

            if (CarInfo != null) 
                for (int i = 0; i < (dataNum - 1) * 3; i += 3)
                {
                    CarInfo[i].text = realCarNumber[(i == 0) ? 0 : i / 3];
                    CarInfo[i + 1].text = realDepartment[(i == 0) ? 0 : i / 3];
                    CarInfo[i + 2].text = realDistance[(i == 0) ? 0 : i / 3];

                    if (CarDistanceDropdownText.text.Equals("운행 전체"))
                    {

                    }
                    else if (CarDistanceDropdownText.text.Equals("3km 이하"))
                    {
                        if (float.Parse(CarInfo[i + 2].text) > 3000)
                            CarInfo[i].transform.parent.gameObject.SetActive(false);
                    }
                    else if (CarDistanceDropdownText.text.Equals("5km 이하"))
                    {
                        if (float.Parse(CarInfo[i + 2].text) > 5000)
                            CarInfo[i].transform.parent.gameObject.SetActive(false);
                    }
                    else if (CarDistanceDropdownText.text.Equals("10km 이하"))
                    {
                        if (float.Parse(CarInfo[i + 2].text) > 10000)
                            CarInfo[i].transform.parent.gameObject.SetActive(false);
                    }
                }
            RecentCount = dataLength;
        }

        if (refrash || CarDistanceOn)
        {
            CarDistanceOn = false;
            preDataNum = dataNum;
            contentChildsBtn = CarDistanceContent.GetComponentsInChildren<Button>(true);
            float[] btnChild = new float[contentChildsBtn.Length];
            for (int i = 0; i < (dataNum - 1); i++)
            {
                btnChild[i] = float.Parse(contentChildsBtn[i].GetComponentsInChildren<Text>()[2].text.ToString());
            }
            for (int i = 0; i < contentChildsBtn.Length; i++)
            {
                for (int j = i + 1; j < contentChildsBtn.Length; j++)
                {
                    if (btnChild[i] >= btnChild[j])
                    {
                        float tempFloat = btnChild[j];
                        btnChild[j] = btnChild[i];
                        btnChild[i] = tempFloat;

                        Button tempBtn = contentChildsBtn[j];
                        contentChildsBtn[j] = contentChildsBtn[i];
                        contentChildsBtn[i] = tempBtn;

                        int tempSibl = contentChildsBtn[i].transform.GetSiblingIndex();
                        contentChildsBtn[i].transform.SetSiblingIndex(contentChildsBtn[j].transform.GetSiblingIndex());
                        contentChildsBtn[j].transform.SetSiblingIndex(tempSibl);
                    }
                }
            }
            Canvas.ForceUpdateCanvases();
        }
    }

    void GetPooledObject(GameObject prefab)                 //풀의 객체를 꺼내 사용하는 함수
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            if (dataNum != preDataNum || CarDistanceOn)  Instantiate(prefab, CarDistanceContent);
        }
    }

    void ReturnToPool(GameObject obj)                       //풀에 객체를 담아놓는 함수
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
