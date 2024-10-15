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

        if(myText.Equals("특수 송달증 등록"))
        {
            string prefabName = "SectionPrefabs/RegistrationAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("특수 송달증 등록_clx"))
        {
            string prefabName = "SectionPrefabs/RegistrationAddClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("특수 송달증 자료전송 (수기)"))
        {
            string prefabName = "SectionPrefabs/RegistrationSend_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("접수용 특수 송달증 등록"))
        {
            string prefabName = "SectionPrefabs/RegistrationReceiptAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("접수용 특수 송달증 등록_clx"))
        {
            string prefabName = "SectionPrefabs/RegistrationReceiptAddClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("특수 송달증 조회/수정_clx"))
        {
            string prefabName = "SectionPrefabs/RegistrationViewEditClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("용기 송달증(등기) 등록_clx"))
        {
            string prefabName = "SectionPrefabs/CRegistrationAddClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("용기 송달증 자료전송(수기)"))
        {
            string prefabName = "SectionPrefabs/CRegistrationSend_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("용기 송달증(일반) 등기"))
        {
            string prefabName = "SectionPrefabs/GCRegistrationAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("용기 송달증 조회/수정_clx"))
        {
            string prefabName = "SectionPrefabs/CRegistrationViewEditClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("통합적재"))
        {
            string prefabName = "SectionPrefabs/IntegratedLoading_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("특수 송달증 수량 조회"))
        {
            string prefabName = "SectionPrefabs/RegistrationCountView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("우편물(특수송달) 조회"))
        {
            string prefabName = "SectionPrefabs/PostRegistrationView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("접수송달 우편물 발송 여부 조회"))
        {
            string prefabName = "SectionPrefabs/ReceiptPostSendView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("도착 우편물 체결여부 조회"))
        {
            string prefabName = "SectionPrefabs/ArrivePostConclusionView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("체결정보 전환_clx"))
        {
            string prefabName = "SectionPrefabs/ConclusionInfoTransClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("체결정보 전환 내역 조회"))
        {
            string prefabName = "SectionPrefabs/ConclusionInfoTransView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("용기 번호별 체결정보 조회"))
        {
            string prefabName = "SectionPrefabs/CNumConclusionInfoView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("접수정보 오입력 조회"))
        {
            string prefabName = "SectionPrefabs/ReceiptNCorrectView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("등기 바코드 오류/미접수내"))
        {
            string prefabName = "SectionPrefabs/RegistrationNReceipt_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("등기 바코드 오류 통계 조회"))
        {
            string prefabName = "SectionPrefabs/RegistrationStatsView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("발송송차증 등록_clx"))
        {
            string prefabName = "SectionPrefabs/SendAddClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("발송송차증 등록"))
        {
            string prefabName = "SectionPrefabs/SendAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("발송예정률 조회"))
        {
            string prefabName = "SectionPrefabs/SendView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("공항 반송 우편물 관리"))
        {
            string prefabName = "SectionPrefabs/AirportPost_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("외화 우편물 체결 수수부"))
        {
            string prefabName = "SectionPrefabs/ForeignPost_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("도착 송치증 조회/확인"))
        {
            string prefabName = "SectionPrefabs/ArriveView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("도착 송치증 파일 업로드"))
        {
            string prefabName = "SectionPrefabs/ArriveUpload_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("도착 송치증(정착) 등록"))
        {
            string prefabName = "SectionPrefabs/ArriveAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("오미착 조회"))
        {
            string prefabName = "SectionPrefabs/NArriveView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("도착 예정 물량 조회"))
        {
            string prefabName = "SectionPrefabs/ArriveExpectView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("혼재 중계처리 예정 물량 조회"))
        {
            string prefabName = "SectionPrefabs/MixedExpectView_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("임시편 필요 요구관리"))
        {
            string prefabName = "SectionPrefabs/TempManage_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("운송확인서 등록"))
        {
            string prefabName = "SectionPrefabs/TransitAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("운송확인서 등록_clx"))
        {
            string prefabName = "SectionPrefabs/TransitAddClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("운송확인서 조회/수정"))
        {
            string prefabName = "SectionPrefabs/TransitViewEdit_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("차량배차등록(임시편)"))
        {
            string prefabName = "SectionPrefabs/VehicleAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("차량배차 등록(임시편)_clx"))
        {
            string prefabName = "SectionPrefabs/VehicleAddClx_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("차량정보 등록"))
        {
            string prefabName = "SectionPrefabs/VehicleInfoAdd_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("차량정보 조회/수정"))
        {
            string prefabName = "SectionPrefabs/VehicleInfoViewEdit_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("지연사유 관리"))
        {
            string prefabName = "SectionPrefabs/DelayManage_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("운전원 관리"))
        {
            string prefabName = "SectionPrefabs/DriverManage_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("재 위탁 운송업체 관리"))
        {
            string prefabName = "SectionPrefabs/ReConsignmentManage_Prefab";
            PrefabsManage(prefabName);
        }
        else if (myText.Equals("재 위탁 운송업체 관리(new)"))
        {
            string prefabName = "SectionPrefabs/ReConsignmentManageNew_Prefab";
            PrefabsManage(prefabName);
        }
    }
}
