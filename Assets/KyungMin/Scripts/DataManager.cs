using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public float RepeatTimer;                               //������ �޾ƿ��� �ݺ��� �ð�

    public bool CarDistanceOn = false;                      //���� �����Ÿ� �������� ó�� Ȱ��ȭ �Ǿ����� Ȯ��
    private Text[] CarInfo;                                 //���� ����
    private int RecentCount = 0;                            //������ ������ ī��Ʈ

    private Transform CallObject;                           //�ٸ� ������Ʈ�� ������ ���� Transform
    private Transform CarDistanceContent;                   //�������� ǥ�� ��ũ�� ���� ������ ��ü Transform
    private Text CarDistanceDropdownText;                   //�Ÿ� DropDown�� ��ǥ Text

    public int MaxDockNum = 0;                              //dock�� �ִ� ����
    public string[][] DockList = new string[14][];          //dock�� ���� = 14

    Queue<GameObject> pool = new Queue<GameObject>();       //pool

    public bool firstStart = true;                          //ó�� �����ϴ��� Ȯ��

    private int[] DownPalletArray = new int[5];             //5��ġ �������� �ޱ����� �迭
    private int[] UpPalletArray = new int[5];               //5��ġ �������� �ޱ����� �迭
    private int[] TotalPalletArray = new int[5];            //5��ġ �������� �ޱ����� �迭

    public bool[] belt = { false, false, false, false, false, false, false, false };    //�����̾� ��Ʈ�� �������� ����

    private int dataNum = 0;                                //���� �޴� �������� ����(ó�� ������� �� �����Ͽ� �������� real ���� �Ϸ��� -1 �������)
    private int preDataNum = 0;                             //������ ������ ����(���� ���� -1)

    Button[] contentChildsBtn;                              //���� ������ ���� ��ư�� ĳ���� ���� ��ư �迭
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

    public void ShortDistancePrefabActive()                 //������ �������� ��ü �����ϴ� �Լ�
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

    private void StartRoutine()                             //�ݺ���ų �ڷ�ƾ�� ������ �Լ�
    {
        StartCoroutine(_LoadCarList());
    }
    public void StopRoutine()                               //�ڷ�ƾ�� ���ߴ� �Լ�
    {
        CancelInvoke("StartRoutine");
        firstStart = true;
    }

    private void FindObject()                       //������ �Ǵ� ������Ʈ�� ã�� �Լ�
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

    IEnumerator _LoadCarList()                          //������ �޾� ������, ������, ��Ȳ����, ���� ���� �����Ϳ� ���� �޾ƿ��� �Լ�
    {
        UnityWebRequest request = UnityWebRequest.Get($"{GameManager.Instance.serverURL}/car/carView");
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
            //Debug.Log(requestText);

            Array.Fill(DownPalletArray, 0);
            Array.Fill(UpPalletArray, 0);

            UnloadCount(requestText, today);
            LoadCount(requestText, today);
            VolumeCount(today);
            DockListUpdate(requestText);
            CarDistance(requestText, today);
        }
    }

    private void DockListUpdate(string data)                            //��Ȳ���ÿ� �� ��ũ ���¿� ���� �����͸� �޴´�.
    {
        int temp_MaxNum = 0;
        DockList = new string[14][];
        Array.Fill(DockList, null);

        string[] preDockNo = data.Split("\"dockNo\":");
        string[] preCarNo = data.Split("\"carNo\":");
        string[] preLogisticsWeight = data.Split("\"logisticsWeight\":");
        string[] preDriveStatus = data.Split("\"driveStatus\":");

        for (int i = 1; i < preDockNo.Length; i++)
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
                DockList[temp][2] = (int.Parse(logisticsWeight[0])).ToString();         //pallet ������ ���� ���� ������ �ߴµ� ���̵����� pallet ���� ��� �׳� ��
                DockList[temp][3] = "����";
            }
            else if (driveStatus[0].Equals("2"))
            {
                DockList[temp] = new string[4];
                DockList[temp][0] = dockNo[0];
                DockList[temp][1] = carNo[0];
                DockList[temp][2] = (int.Parse(logisticsWeight[0])).ToString();         //pallet ������ ���� ���� ������ �ߴµ� ���̵����� pallet ���� ��� �׳� ��
                DockList[temp][3] = "����";
            }
        }
        MaxDockNum = temp_MaxNum;
    }

    private void UnloadCount(string data, DateTime today)                       //���������͸� �޴� �Լ�
    {
        if (CallObject != null)
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
                else if (driveStatus[0].Equals("13")) continue;                     //��������� �Ǵ�

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
                    Debug.Log("����! = " + DownPalletArray[4]);
                }
                else
                {

                }
            }
        }
    }

    private void LoadCount(string data, DateTime today)                     //���������͸� �޴� �Լ�
    {
        if (CallObject != null)
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
                else if (driveStatus[0].Equals("13")) continue;                     //��������� �Ǵ�
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
    private void VolumeCount(DateTime today)                                //������ �����͸� �޴� �Լ�
    {
        if (CallObject != null)
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

    private void CarDistance(string data, DateTime today)                           //���� �����Ÿ� �����͸� �޴� �Լ�
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
                    Debug.Log("����");
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

            //������ȣ, �����, �Ÿ� ���ϴ� �ڵ�
            string[] preCarNumber = data.Split("\"carNo\":");
            string[] preDepartment = data.Split("\"departPoint\":");
            string[] preDistance = data.Split("\"currDist\":");

            dataLength = preCarNumber.Length - 1;

            string[] realCarNumber = new string[dataLength];
            string[] realDepartment = new string[dataLength];
            string[] realDistance = new string[dataLength];

            //1���� ī��Ʈ �ϴ� ���� : ������ 0��° �迭���� Ű���� ���� ���� ������ �� �� ���� ���� ���� ��
            for (int i = 1; i < preCarNumber.Length; i++)
            {
                /*1�� �迭 ���� ���ʹ� ���� Ű���� �������� ���� ���ԵǾ������Ƿ�
                Ű���� ���� ������ �������� �����ڷ� Split �ϰ� ���� ���� ��ȣ Replace�� ����*/

                //���� ��ȣ ���ϴ� �ڵ�
                string[] carNumber = preCarNumber[i].Split(",");
                realCarNumber[i - 1] = carNumber[0].Trim('"');
                //����� ���ϴ� �ڵ�
                string[] department = preDepartment[i].Split(",");
                realDepartment[i - 1] = department[0].Trim('"');
                //�����Ÿ� ���ϴ� �ڵ�
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

                    if (CarDistanceDropdownText.text.Equals("���� ��ü"))
                    {

                    }
                    else if (CarDistanceDropdownText.text.Equals("3km ����"))
                    {
                        if (float.Parse(CarInfo[i + 2].text) > 3000)
                            CarInfo[i].transform.parent.gameObject.SetActive(false);
                    }
                    else if (CarDistanceDropdownText.text.Equals("5km ����"))
                    {
                        if (float.Parse(CarInfo[i + 2].text) > 5000)
                            CarInfo[i].transform.parent.gameObject.SetActive(false);
                    }
                    else if (CarDistanceDropdownText.text.Equals("10km ����"))
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

    void GetPooledObject(GameObject prefab)                 //Ǯ�� ��ü�� ���� ����ϴ� �Լ�
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            if (dataNum != preDataNum || CarDistanceOn) Instantiate(prefab, CarDistanceContent);
        }
    }

    void ReturnToPool(GameObject obj)                       //Ǯ�� ��ü�� ��Ƴ��� �Լ�
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
