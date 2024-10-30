using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class OperationDetailInfo
{
    public string netType;
    public string carNum;
    public string driverPhoneNum;
    public string secondOfficeName;
    public string secondOfficeNamee;
    public string departExpectTime;
    public string arriveExpectTime;
    public string driverName;
    public string departArriveStatus;
    public string transitCheckNum;
}

[System.Serializable]
public class OperationDetailList
{
    public OperationDetailInfo[] prefabDatas;   // json �迭�� ������� �ʵ�
}

public static class JsonHelper1
{
    public static T[] FromJson<T>(string json)
    {
        // JSON �����͸� ��ü �迭�� �����ϴ� JSON �������� ���Ѵ�.
        string newJson = "{ \"prefabDatas\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.prefabDatas;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] prefabDatas; // JSON �������� �迭 �ʵ忡 ���߾� �̸��� ��ġ��Ų��.
    }
}

public class OperationDetailScript : MonoBehaviour
{
    public VehicleDataCenter vehicleDataCenter;
    private string firstOfficeCode = "0005";
    private string secondOfficeCode = string.Empty;

    public GameObject operationPrefab;
    private List<OperationDetailInfo> operationDetailInfoList;

    public Transform content;

    [Header("��ȸ����")]
    public Dropdown firstOfficeDropdown;
    public Dropdown secondOfficeDropdown;
    public Dropdown netTypeDropdown;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstOfficeSelect()
    {
        int select = firstOfficeDropdown.value;
        secondOfficeDropdown.options.Clear();
        secondOfficeDropdown.options.Add(new Dropdown.OptionData("���߱�"));
        switch (select)
        {
            case 0:
                firstOfficeCode = "0005";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("�����������߱�"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("û�ֿ������߱�"));
                break;
            case 1:
                firstOfficeCode = "0001";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("������������߱�"));
                break;
            case 2:
                firstOfficeCode = "0002";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("���ֿ������߱�"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("�����������߱�"));
                break;
            case 3:
                firstOfficeCode = "0003";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("�����������߱�"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("��õ�������߱�"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("�����ο������߱�"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("���������߱�"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("�����������߱�"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("�Ⱦ�������߱�"));
                break;
            case 4:
                firstOfficeCode = "0004";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("�λ�������߱�"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("â���������߱�"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("���������߱�"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("���ֿ������߱�"));
                break;
            case 5:
                firstOfficeCode = "0006";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("���ֿ������߱�"));
                break;
            case 6:
                firstOfficeCode = "0007";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("���ֿ������߱�"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("���Ͽ������߱�"));
                break;
            case 7:
                firstOfficeCode = "0008";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("���ֿ������߱�"));
                break;
            case 8:
                firstOfficeCode = "0009";
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("�뱸�������߱�"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("���׿������߱�"));
                secondOfficeDropdown.options.Add(new Dropdown.OptionData("�ȵ��������߱�"));
                break;
        }

        Debug.Log(secondOfficeCode + "�Դϴ�");
    }

    public void SecondOfficeSelect()
    {
        string selectText = secondOfficeDropdown.transform.GetComponentInChildren<Text>().text;
        switch (selectText)
        {
            case "�����������߱�":
                secondOfficeCode = "0100";
                break;
            case "û�ֿ������߱�":
                secondOfficeCode = "0200";
                break;
            case "���ֿ������߱�":
                secondOfficeCode = "0300";
                break;
            case "���ֿ������߱�":
                secondOfficeCode = "0400";
                break;
            case "�����������߱�":
                secondOfficeCode = "0500";
                break;
            case "��õ�������߱�":
                secondOfficeCode = "0600";
                break;
            case "���ֿ������߱�":
                secondOfficeCode = "0700";
                break;
            case "�����ο������߱�":
                secondOfficeCode = "0800";
                break;
            case "���������߱�":
                secondOfficeCode = "0900";
                break;
            case "�λ�������߱�":
                secondOfficeCode = "1000";
                break;
            case "���ֿ������߱�":
                secondOfficeCode = "1100";
                break;
            case "������������߱�":
                secondOfficeCode = "1200";
                break;
            case "�����������߱�":
                secondOfficeCode = "1300";
                break;
            case "�뱸�������߱�":
                secondOfficeCode = "1400";
                break;
            case "�Ⱦ�������߱�":
                secondOfficeCode = "1500";
                break;
            case "�����������߱�":
                secondOfficeCode = "1600";
                break;
            case "â���������߱�":
                secondOfficeCode = "1700";
                break;
            case "���׿������߱�":
                secondOfficeCode = "1800";
                break;
            case "�ȵ��������߱�":
                secondOfficeCode = "1900";
                break;
            case "���������߱�":
                secondOfficeCode = "2000";
                break;
            case "���Ͽ������߱�":
                secondOfficeCode = "2100";
                break;
            case "���ֿ������߱�":
                secondOfficeCode = "2200";
                break;
            default:
                secondOfficeCode = string.Empty;
                break;
        }
    }

    public void OnclickResetBtn()
    {
        firstOfficeDropdown.value = 0;
        secondOfficeDropdown.value = 0;
        netTypeDropdown.value = 0;
    }

    public void OnclickSearchBtn()
    {
        string netText = netTypeDropdown.GetComponentInChildren<Text>().text;
        netText = netText.Equals("��ü") ? string.Empty : netText;

        //StartCoroutine(vehicleDataCenter.OperationDetailDataReceive(firstOfficeCode, secondOfficeCode, netText));
        StartCoroutine(vehicleDataCenter.OperationDetailDataReceive(firstOfficeCode, secondOfficeCode, netText));
    }

    public void HandleReceivedData(UnityWebRequest request)
    {
        int sn = 1;
        Debug.Log("������ �ް��� �Ľ� �ؾ���.");
        foreach (Transform obj in content.GetComponentsInChildren<Transform>())
        {
            if(content != obj)
                Destroy(obj.gameObject);
        }

        OperationDetailInfo[] dataArray = JsonHelper1.FromJson<OperationDetailInfo>(request.downloadHandler.text);

        // 3. �Ľ̵� �����͸� ����Ʈ�� ����
        operationDetailInfoList = new List<OperationDetailInfo>(dataArray);
        
        // 4. UI�� �����͸� ����

        foreach (OperationDetailInfo ODI in operationDetailInfoList)
        {
            GameObject newObj = Instantiate(operationPrefab, content);
            OperationDetailDataPrefabScript prefabScript = newObj.GetComponent<OperationDetailDataPrefabScript>();
            prefabScript.SetData(sn, ODI);
            sn++;
        }
        sn = 1;
    }
}
