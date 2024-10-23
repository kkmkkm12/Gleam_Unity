using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegionTimeQuantityScript : MonoBehaviour
{
    public VehicleDataCenter vehicleDataCenter;

    private string firstOfficeCode = "0005";
    private string secondOfficeCode = string.Empty;

    [Header("��ȸ����")]
    public Dropdown firstOfficeDropdown;
    public Dropdown secondOfficeDropdown;
    public Dropdown yearDropdown;
    public Dropdown monthDropdown;
    public Dropdown dayDropdown;
    public Dropdown startTimeDropdown;
    public Dropdown endTimeDropdown;

    [Header("�Ϲݼ���")]
    public Text fisrtLine1;
    public Text fisrtLine2;
    public Text fisrtLine3;
    public Text fisrtLine4;
    public Text fisrtLine5;
    public Text fisrtLine6;
    public Text fisrtLine7;
    public Text fisrtLine8;
    public Text fisrtLine9;

    [Header("â������")]
    public Text secondLine1;
    public Text secondLine2;
    public Text secondLine3;
    public Text secondLine4;
    public Text secondLine5;
    public Text secondLine6;
    public Text secondLine7;
    public Text secondLine8;
    public Text secondLine9;

    [Header("����Ư��-����")]
    public Text thirdLine1;
    public Text thirdLine2;
    public Text thirdLine3;
    public Text thirdLine4;
    public Text thirdLine5;
    public Text thirdLine6;
    public Text thirdLine7;
    public Text thirdLine8;
    public Text thirdLine9;

    [Header("�湮����")]
    public Text fourthLine1;
    public Text fourthLine2;
    public Text fourthLine3;
    public Text fourthLine4;
    public Text fourthLine5;
    public Text fourthLine6;
    public Text fourthLine7;
    public Text fourthLine8;
    public Text fourthLine9;

    [Header("����Ư��")]
    public Text fifthLine1;
    public Text fifthLine2;
    public Text fifthLine3;
    public Text fifthLine4;
    public Text fifthLine5;
    public Text fifthLine6;
    public Text fifthLine7;
    public Text fifthLine8;
    public Text fifthLine9;

    [Header("�հ�")]
    public Text totalPlusLine1;
    public Text totalPlusLine2;
    public Text totalPlusLine3;
    public Text totalPlusLine4;
    public Text totalPlusLine5;
    public Text totalPlusLine6;
    public Text totalPlusLine7;
    public Text totalPlusLine8;
    public Text totalPlusLine9;

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
        StartCoroutine(vehicleDataCenter.RegionTimeQuantityDataReceive("0001", "1200", "2024-10-15"));
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

    public void OnclickSearchBtn()
    {
        StartCoroutine(vehicleDataCenter.RegionTimeQuantityDataReceive(firstOfficeCode, secondOfficeCode, string.Empty));
    }

    public void HandleReceivedData(UnityWebRequest response)
    {
        Debug.Log("������ �ް����� �Ľ� �ؾ���.");
    }
}
