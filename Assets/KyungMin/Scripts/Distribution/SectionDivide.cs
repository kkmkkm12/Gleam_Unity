using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionDivide : MonoBehaviour
{
    private GameObject prefab;
    private GameObject newObject;
    private Transform MainFunctionArrangement;
    private ScrollRect ScrollRect;
    // Start is called before the first frame update
    void Awake()
    {
        MainFunctionArrangement = GameObject.Find("MainFunctionArrangement").GetComponent<Transform>();
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PrefabsManage(string prefabName)
    {
        foreach(Transform objTransform in MainFunctionArrangement)
        {
            if (objTransform == MainFunctionArrangement) continue;
            objTransform.gameObject.SetActive(false);
        }
        string ObjectName = prefabName.Substring(15);
        Debug.Log(ObjectName);
        
        foreach(Transform objTransform in MainFunctionArrangement)
        {
            if(objTransform.name.Contains(ObjectName))
            {
                objTransform.gameObject.SetActive(true);
                return;
            }
        }
        prefab = Resources.Load<GameObject>(prefabName);
        newObject = Instantiate(prefab, MainFunctionArrangement);

        Canvas.ForceUpdateCanvases();
    }

    public void OnClickX()
    {
        Destroy(gameObject);
        Destroy(newObject);
    }
    //44
    public void WhoAmI()
    {
        Button clickBtn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        string myText = clickBtn.GetComponentInChildren<Text>().text;

        if(myText.Equals("Ư�� �۴��� ���"))
        {
            string prefabName = "SectionPrefabs/RegistrationAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("Ư�� �۴��� ���_clx"))
        {
            string prefabName = "SectionPrefabs/RegistrationAddClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("Ư�� �۴��� �ڷ����� (����)"))
        {
            string prefabName = "SectionPrefabs/RegistrationSend_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("������ Ư�� �۴��� ���"))
        {
            string prefabName = "SectionPrefabs/RegistrationReceiptAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("������ Ư�� �۴��� ���_clx"))
        {
            string prefabName = "SectionPrefabs/RegistrationReceiptAddClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("Ư�� �۴��� ��ȸ/����_clx"))
        {
            string prefabName = "SectionPrefabs/RegistrationViewEditClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("��� �۴���(���) ���_clx"))
        {
            string prefabName = "SectionPrefabs/CRegistrationAddClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("��� �۴��� �ڷ�����(����)"))
        {
            string prefabName = "SectionPrefabs/CRegistrationSend_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("��� �۴���(�Ϲ�) ���"))
        {
            string prefabName = "SectionPrefabs/GCRegistrationAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("��� �۴��� ��ȸ/����_clx"))
        {
            string prefabName = "SectionPrefabs/CRegistrationViewEditClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("��������"))
        {
            string prefabName = "SectionPrefabs/IntegratedLoading_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("Ư�� �۴��� ���� ��ȸ"))
        {
            string prefabName = "SectionPrefabs/RegistrationCountView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("����(Ư���۴�) ��ȸ"))
        {
            string prefabName = "SectionPrefabs/PostRegistrationView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("�����۴� ���� �߼� ���� ��ȸ"))
        {
            string prefabName = "SectionPrefabs/ReceiptPostSendView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("���� ���� ü�Ῡ�� ��ȸ"))
        {
            string prefabName = "SectionPrefabs/ArrivePostConclusionView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("ü������ ��ȯ_clx"))
        {
            string prefabName = "SectionPrefabs/ConclusionInfoTransClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("ü������ ��ȯ ���� ��ȸ"))
        {
            string prefabName = "SectionPrefabs/ConclusionInfoTransView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("��� ��ȣ�� ü������ ��ȸ"))
        {
            string prefabName = "SectionPrefabs/CNumConclusionInfoView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("�������� ���Է� ��ȸ"))
        {
            string prefabName = "SectionPrefabs/ReceiptNCorrectView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("��� ���ڵ� ����/��������"))
        {
            string prefabName = "SectionPrefabs/RegistrationNReceipt_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("��� ���ڵ� ���� ��� ��ȸ"))
        {
            string prefabName = "SectionPrefabs/RegistrationStatsView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("�߼ۼ����� ���_clx"))
        {
            string prefabName = "SectionPrefabs/SendAddClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("�߼ۼ����� ���"))
        {
            string prefabName = "SectionPrefabs/SendAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("�߼ۿ����� ��ȸ"))
        {
            string prefabName = "SectionPrefabs/SendView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("���� �ݼ� ���� ����"))
        {
            string prefabName = "SectionPrefabs/AirportPost_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("��ȭ ���� ü�� ������"))
        {
            string prefabName = "SectionPrefabs/ForeignPost_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("���� ��ġ�� ��ȸ/Ȯ��"))
        {
            string prefabName = "SectionPrefabs/ArriveView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("���� ��ġ�� ���� ���ε�"))
        {
            string prefabName = "SectionPrefabs/ArriveUpload_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("���� ��ġ��(����) ���"))
        {
            string prefabName = "SectionPrefabs/ArriveAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("������ ��ȸ"))
        {
            string prefabName = "SectionPrefabs/NArriveView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("���� ���� ���� ��ȸ"))
        {
            string prefabName = "SectionPrefabs/ArriveExpectView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("ȥ�� �߰�ó�� ���� ���� ��ȸ"))
        {
            string prefabName = "SectionPrefabs/MixedExpectView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("�ӽ��� �ʿ� �䱸����"))
        {
            string prefabName = "SectionPrefabs/TempManage_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("���Ȯ�μ� ���"))
        {
            string prefabName = "SectionPrefabs/TransitAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("���Ȯ�μ� ���_clx"))
        {
            string prefabName = "SectionPrefabs/TransitAddClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("���Ȯ�μ� ��ȸ/����"))
        {
            string prefabName = "SectionPrefabs/TransitViewEdit_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("�����������(�ӽ���)"))
        {
            string prefabName = "SectionPrefabs/VehicleAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("�������� ���(�ӽ���)_clx"))
        {
            string prefabName = "SectionPrefabs/VehicleAddClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("�������� ���"))
        {
            string prefabName = "SectionPrefabs/VehicleInfoAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("�������� ��ȸ/����"))
        {
            string prefabName = "SectionPrefabs/VehicleInfoViewEdit_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("�������� ����"))
        {
            string prefabName = "SectionPrefabs/DelayManage_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("������ ����"))
        {
            string prefabName = "SectionPrefabs/DriverManage_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("�� ��Ź ��۾�ü ����"))
        {
            string prefabName = "SectionPrefabs/ReConsignmentManage_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("�� ��Ź ��۾�ü ����(new)"))
        {
            string prefabName = "SectionPrefabs/ReConsignmentManageNew_Prefab";
            PrefabsManage(prefabName);
        }
    }
}
