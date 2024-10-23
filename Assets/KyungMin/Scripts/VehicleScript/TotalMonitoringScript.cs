using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TotalMonitoringScript : MonoBehaviour
{
    public VehicleDataCenter vehicleDataCenter;

    public Dropdown firstOfficeDropdown;                            //����û, ����û, ����û, �λ�û, ��ûû, ����û, ����û, ����û, ���û
    string firstOfficeCode = string.Empty;
    public Dropdown secondOfficeDropdown;
    public string secondOfficeCode = "0100";

    [Header("TotalQuantitySet�� Text")]
    public Text tb1FirstLineText;
    public Text tb1SecondLineText;
    public Text tb1ThirdLineText;
    public Text tb1FourthLineText;

    [Header("ReceiptQuantitySet�� Text")]
    public Text tb2FirstLineText1;
    public Text tb2FirstLineText2;
    public Text tb2FirstLineText3;
    public Text tb2SecondLineText1;
    public Text tb2SecondLineText2;
    public Text tb2SecondLineText3;
    public Text tb2ThirdLineText1;
    public Text tb2ThirdLineText2;
    public Text tb2ThirdLineText3;
    public Text tb2FourthLineText1;
    public Text tb2FourthLineText2;
    public Text tb2FourthLineText3;

    [Header("DeliveryQuantitySet�� Text")]
    public Text tb3FirstLineText1;
    public Text tb3FirstLineText2;
    public Text tb3FirstLineText3;
    public Text tb3FirstLineText4;
    public Text tb3FirstLineText5;
    public Text tb3SecondLineText1;
    public Text tb3SecondLineText2;
    public Text tb3SecondLineText3;
    public Text tb3SecondLineText4;
    public Text tb3SecondLineText5;
    public Text tb3ThirdLineText1;
    public Text tb3ThirdLineText3;
    public Text tb3ThirdLineText4;
    public Text tb3FourthLineText1;
    public Text tb3FourthLineText3;
    public Text tb3FourthLineText4;
    public Text tb3FifthLineText1;
    public Text tb3FifthLineText2;
    public Text tb3FifthLineText3;
    public Text tb3FifthLineText4;
    public Text tb3FifthLineText5;

    [Header("ConcentrationQuantitySet�� Text")]
    public Text tb4FirstLineText1;
    public Text tb4FirstLineText2;
    public Text tb4FirstLineText3;
    public Text tb4FirstLineText4;
    public Text tb4FirstLineText5;
    public Text tb4SecondLineText1;
    public Text tb4SecondLineText2;
    public Text tb4SecondLineText3;
    public Text tb4SecondLineText4;
    public Text tb4SecondLineText5;
    public Text tb4ThirdLineText1;
    public Text tb4ThirdLineText2;
    public Text tb4ThirdLineText3;
    public Text tb4ThirdLineText4;
    public Text tb4ThirdLineText5;

    [Header("TransitSituationSet�� Text")]
    public Text tb5FirstLineText1;
    public Text tb5FirstLineText2;
    public Text tb5FirstLineText3;
    public Text tb5SecondLineText1;
    public Text tb5SecondLineText2;
    public Text tb5SecondLineText3;

    [Header("LateSituationSet�� Text")]
    public Text tb6FirstLineText1;
    public Text tb6FirstLineText2;

    [Header("ObstacleSituationSet�� Text")]
    public Text tb7FirstLineText1;
    public Text tb7FirstLineText2;
    public Text tb7FirstLineText3;
    public Text tb7FirstLineText4;

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

    public void OnClickSearchBtn()
    {
        //������ �޴� ��ü �ϳ� ����� ������ �ش� ��ũ��Ʈ���� ������ �޾ƿ��� �Լ� ȣ����Ѽ� ���⼭ �޾Ƽ� ȭ�鿡 ���� ��Ű�� �Ұ���.
        StartCoroutine(vehicleDataCenter.TotalMoniterDataReceive(secondOfficeCode));
    }

    public void HandleReceivedData(UnityWebRequest response)
    {
        Debug.Log("������ �ް��� �Ľ� �ؾ���.");
    }
}
