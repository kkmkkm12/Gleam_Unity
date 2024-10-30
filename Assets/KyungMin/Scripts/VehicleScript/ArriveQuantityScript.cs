using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;








[System.Serializable]
public class ArriveQuantityInfo
{
    public string secondOfficeName;
    public int packageGeneral;
    public int packageWindow;
    public int factorQuantity;
    public int international;

    public ArriveQuantityInfo(string secondOfficeName, int packageGeneral, int packageWindow, int factorQuantity, int international)
    {
        this.secondOfficeName = secondOfficeName;
        this.packageGeneral = packageGeneral;
        this.packageWindow = packageWindow;
        this.factorQuantity = factorQuantity;
        this.international = international;
    }
}

[System.Serializable]
public class ArriveQuantityList
{
    public ArriveQuantityInfo[] prefabDatas;
}

public class ArriveQuantityScript : MonoBehaviour
{
    public VehicleDataCenter vehicleDataCenter;

    private string firstOfficeCode = "0005";
    private string secondOfficeCode = string.Empty;

    public GameObject arrivePrefab;
    private List<ArriveQuantityInfo> arriveQuantityInfoList;
    public Transform content;

    [Header("��ȸ����")]
    public Dropdown firstOfficeDropdown;
    public Dropdown secondOfficeDropdown;
    public Dropdown startYearDropdown;
    public Dropdown startMonthDropdown;
    public Dropdown startDayDropdown;
    public Dropdown endYearDropdown;
    public Dropdown endMonthDropdown;
    public Dropdown endDayDropdown;




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

    private string TranslationCode(string code)
    {
        string officeName = string.Empty;

        switch (code)
        {
            case "0100":
                officeName = "�����������߱�";
                break;
            case "0200":
                officeName = "û�ֿ������߱�";
                break;
            case "0300":
                officeName = "���ֿ������߱�";
                break;
            case "0400":
                officeName = "���ֿ������߱�";
                break;
            case "0500":
                officeName = "�����������߱�";
                break;
            case "0600":
                officeName = "��õ�������߱�";
                break;
            case "0700":
                officeName = "���ֿ������߱�";
                break;
            case "0800":
                officeName = "�����ο������߱�";
                break;
            case "0900":
                officeName = "���������߱�";
                break;
            case "1000":
                officeName = "�λ�������߱�";
                break;
            case "1100":
                officeName = "���ֿ������߱�";
                break;
            case "1200":
                officeName = "������������߱�";
                break;
            case "1300":
                officeName = "�����������߱�";
                break;
            case "1400":
                officeName = "�뱸�������߱�";
                break;
            case "1500":
                officeName = "�Ⱦ�������߱�";
                break;
            case "1600":
                officeName = "�����������߱�";
                break;
            case "1700":
                officeName = "â���������߱�";
                break;
            case "1800":
                officeName = "���׿������߱�";
                break;
            case "1900":
                officeName = "�ȵ��������߱�";
                break;
            case "2000":
                officeName = "���������߱�";
                break;
            case "2100":
                officeName = "���Ͽ������߱�";
                break;
            case "2200":
                officeName = "���ֿ������߱�";
                break;

        }

        return officeName;
    }

    public void OnclickResetBtn()
    {
        firstOfficeDropdown.value = 0;
        startYearDropdown.value = 0;
        startMonthDropdown.value = 0;
        startDayDropdown.value = 0;
        endYearDropdown.value = 0;
        endMonthDropdown.value = 0;
        endDayDropdown.value = 0;
    }

    public void OnclickSearchBtn()
    {
        arriveQuantityInfoList = new List<ArriveQuantityInfo>();

        string startYearText = startYearDropdown.GetComponentInChildren<Text>().text;
        string startMonthText = startMonthDropdown.GetComponentInChildren<Text>().text;
        string startDayText = startDayDropdown.GetComponentInChildren<Text>().text;
        string endYearText = endYearDropdown.GetComponentInChildren<Text>().text;
        string endMonthText = endMonthDropdown.GetComponentInChildren<Text>().text;
        string endDayText = endDayDropdown.GetComponentInChildren<Text>().text;

        string startDate = startYearText.Equals("�⵵") ? string.Empty : startYearText;
        startDate += startDate.Equals(string.Empty) || startMonthText.Equals("��") ? string.Empty : "-" + startMonthText;
        startDate += startDate.Equals(string.Empty) || startDayText.Equals("��") ? string.Empty : "-" + startDayText;

        string endDate = endYearText.Equals("�⵵") ? string.Empty : endYearText;
        endDate += endDate.Equals(string.Empty) || endMonthText.Equals("��") ? string.Empty : "-" + endMonthText;
        endDate += endDate.Equals(string.Empty) || endDayText.Equals("��") ? string.Empty : "-" + endDayText;

        Debug.Log($"����û : {firstOfficeCode}, ������ : {startDate}, ������ : {endDate}");
        //StartCoroutine(vehicleDataCenter.ArriveQuantityDataReceive(firstOfficeCode, startDate, endDate));


        StartCoroutine(vehicleDataCenter.ArriveQuantityDataReceive(firstOfficeCode, startDate, endDate));
    }

    public void HandleReceivedData(UnityWebRequest response)
    {

        foreach(Transform obj in content.GetComponentsInChildren<Transform>())
        {
            if (content != obj)
            {
                Destroy(obj.gameObject);
            }
        }

        string dataString = response.downloadHandler.text;
        bool duplicate = false;

        string[] preGeneralOfficeCode = dataString.Split("\"generalOfficeCode\":");
        string[] prePackageStatus = dataString.Split("\"packageStatus\":");

        for(int i = 1; i < preGeneralOfficeCode.Length; i++)
        {
            int listIndex = 0;

            string[] generalOfficeCode = preGeneralOfficeCode[i].Split(',');
            string[] packageStatus = prePackageStatus[i].Split("}");


            generalOfficeCode[0] = generalOfficeCode[0].Trim('"');
            packageStatus[0] = packageStatus[0].Trim('"');

            if(arriveQuantityInfoList.Count > 0)
                for (int j = 0; j < arriveQuantityInfoList.Count; j++)
                {
                    //��ü�� �ߺ����� ó���ؾ���
                    if (generalOfficeCode[0].Equals(arriveQuantityInfoList[j].secondOfficeName))
                    {
                        //�ش� ��ü�� ����ü�� ��ǰ�� ������ �ؾ���
                        duplicate = true;
                        listIndex = j;
                        break;
                    }
                }

            if (duplicate)      //�ߺ��Ǵ� �ڵ尡 ������
            {
                switch (packageStatus[0])
                {
                    case "�Ϲݼ���":
                        arriveQuantityInfoList[listIndex].packageGeneral += 1;
                        break;
                    case "â������":
                        arriveQuantityInfoList[listIndex].packageWindow += 1;
                        break;
                    case "�湮����":
                        arriveQuantityInfoList[listIndex].factorQuantity += 1;
                        break;
                    case "����Ư��":
                        arriveQuantityInfoList[listIndex].international += 1;
                        break;
                }
            }
            else
            {
                switch (packageStatus[0])
                {
                    case "�Ϲݼ���":
                        arriveQuantityInfoList.Add(new ArriveQuantityInfo(generalOfficeCode[0], 1, 0, 0, 0));
                        break;
                    case "â������":
                        arriveQuantityInfoList.Add(new ArriveQuantityInfo(generalOfficeCode[0], 0, 1, 0, 0));
                        break;
                    case "�湮����":
                        arriveQuantityInfoList.Add(new ArriveQuantityInfo(generalOfficeCode[0], 0, 0, 1, 0));
                        break;
                    case "����Ư��":
                        arriveQuantityInfoList.Add(new ArriveQuantityInfo(generalOfficeCode[0], 0, 0, 0, 1));
                        break;
                }
            }


            duplicate = false;
        }

        for(int i = 0; i < arriveQuantityInfoList.Count; i++)
        {
            GameObject newObj = Instantiate(arrivePrefab, content);
            ArriveQuantityDataPrefabScript AQDPS = newObj.GetComponent<ArriveQuantityDataPrefabScript>();
            arriveQuantityInfoList[i].secondOfficeName = TranslationCode(arriveQuantityInfoList[i].secondOfficeName);
            AQDPS.SetData(arriveQuantityInfoList[i]);
        }
    }
}
