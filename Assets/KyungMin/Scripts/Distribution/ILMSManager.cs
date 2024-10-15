using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ILMSManager : MonoBehaviour
{
    public DataManager DataCenter;                                  //물류관제 그래프 데이터를 받는 스크립트

    public GameObject InfoPanelArea;                                //정보 패널을 묶어놓은 오브젝트
    public GameObject NavigationBtnArea;                            //Navigation 버튼을 묶어놓은 오브젝트
    //public Transform SectionBtnScroll_Content;                      //클릭하여 사용하는 기능메뉴 탭을 묶어놓는 트렌스폼
    public GameObject ConclusionPart;                               //기능메뉴 스크롤뷰의 체결관리 오브젝트
    public GameObject SendPart;                                     //" 발송관리 오브젝트
    public GameObject ArrivePart;                                   //" 도착관리 오브젝트
    public GameObject TSDMPart;                                     //" 운송편 수급 관리 오브젝트

    public GameObject SettingObject;                                //Dashboard_SettingSet에 관여하는 오브젝트
    
    public GameObject MainFunctionSettingArea;                      //물류ILMS 설정을 눌렀을 때 표시되는 화면을 띄우는 오브젝트
    public Transform MainFunctionSettingPanel;                      //물류ILMS 설정의 사용가능 버튼을 제어하기 위한 판넬
    public Button MainFunctionSaveBtn;                              //물류ILMS 설정을 저장 및 적용을 하는버튼

    public GameObject MainFunction1;                                //사용자 물류ILMS UI의 상단 묶음 오브젝트
    public GameObject MainFunction2;                                //사용자 물류ILMS UI의 하단 묶음 오브젝트

    public InputField SearchInputField;                             //기능메뉴를 검색할 수 있는 InputField
    public GameObject FunctionContent;                              //기능메뉴를 담고있는 Content 오브젝트

    public RectTransform DistributionCanvas;                        //물류관제를 담당하는 캔버스
    private RectTransform MyRect;                                   //물류관제의 해상도를 담당하는 RectTransform                                

    public Camera mainCamera;                                       //메인 카메라
    public Camera secondaryCamera;                                  //두번째 카메라

    private string LastClickBtnName;                                //세팅에서 기능 메뉴 버튼 중 마지막에 누른 버튼 객체 이름을 저장하는 변수
    private string LastClickBtnLabel;                               //버튼의 Text를 저장하는 변수

    private string[] MainFunctionArray = {"", "", "", "", "" }; //주컨텐츠의 설정을 first ~ fifth의 위치에 뭐가 들어가는지 저장하는 변수
    private string MainType;                                        //원형, Bar 타입을 저장하는 변수
    private string SelectFunc;                                      //하차, 상차, 물동량, 상황전시, 인접거리인지 저장하는 변수

    public Text AddNavigationLabel_Text;                            //추가할 네비게이션 버튼의 Text

    private Button addButton;                                       //추가한 네비게이션 버튼

    private float prefabWidth;                                      //주 컨텐츠 View의 너비를 계산하는 변수
    private float prefabHeight;                                     //주 컨텐츠 View의 높이를 개산하는 변수

    private bool DuplicationCheck = false;                          //중복 허용 체크박스의 값을 계산하는 변수

    private void OnEnable()
    {
        MyRect = GetComponent<RectTransform>();

        //해상도에 따른 UI 크기 조절
        MyRect.sizeDelta = new Vector2(DistributionCanvas.rect.width, DistributionCanvas.rect.height);

        MainFunctionActivate();                                     //관제 그래프 실행하는 함수
                                                                    
        AddNaviBtn();                                               //네비게이션 메뉴에 추가한 기능메뉴 버튼 생성하는 함수
    }

    // Start is called before the first frame update
    void Start()
    {
        // 추가 디스플레이를 활성화합니다.
        for (int i = 1; i < Display.displays.Length; i++)           
        {
            Display.displays[i].Activate();
        }

        // 두 번째 디스플레이가 존재할 경우 설정합니다.
        if (Display.displays.Length > 1)
        {
            secondaryCamera.targetDisplay = 1; // 두 번째 디스플레이에 할당
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape") && Input.GetKey("q"))            //빌드 종료키
            Application.Quit();
    }
    public void OnInputfieldEndEdit()                               //기능메뉴 검색시 엔터키로 검색되게 하는 함수
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
            OnClickSearchBtn();
    }
    public void OnClickSearchBtn()                                  //검색 버튼에 들어갈 함수(InputField의 값을 통해 검색)
    {
        FunctionScrollClear();

        string searchText = SearchInputField.text;

        for(int i = 0; i < FunctionContent.transform.childCount; i++)
        {
            GameObject obj = FunctionContent.transform.GetChild(i).gameObject;
            obj.SetActive(true);

            foreach(Button childBtn in obj.GetComponentsInChildren<Button>())
            {
                if (childBtn.GetComponentInChildren<Text>().text.Contains(searchText))
                    childBtn.gameObject.SetActive(true);
                else
                    childBtn.gameObject.SetActive(false);
            }
        }
    }

    private void FunctionScrollClear()                              //검색 전 스크롤의 content의 하위객체를 활성화 시키는 함수
    {
        foreach(Transform child in FunctionContent.GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.SetActive(true);
        }
        for(int i = 0; i < FunctionContent.transform.childCount; i++)
        {
            GameObject obj = FunctionContent.transform.GetChild(i).gameObject;
            obj.SetActive(false);
        }
    }

    public void ConclusionClick()                                   //네비게이션의 체결관리 버튼 클릭시 실행할 함수로 기능메뉴의 활성화를 담당
    {
        ConclusionPart.SetActive(true);
        SendPart.SetActive(false);
        ArrivePart.SetActive(false);
        TSDMPart.SetActive(false);

        foreach(Transform child in ConclusionPart.GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.SetActive(true);
        }
    }
    public void SendClick()                                         //네비게이션의 발송관리 버튼 클릭시 실행할 함수로 기능메뉴의 활성화를 담당
    {
        ConclusionPart.SetActive(false);
        SendPart.SetActive(true);
        ArrivePart.SetActive(false);
        TSDMPart.SetActive(false);

        foreach (Transform child in SendPart.GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.SetActive(true);
        }
    }
    public void ArriveClick()                                       //네비게이션의 도착관리 버튼 클릭시 실행할 함수로 기능메뉴의 활성화를 담당
    {
        ConclusionPart.SetActive(false);
        SendPart.SetActive(false);
        ArrivePart.SetActive(true);
        TSDMPart.SetActive(false);
        
        foreach (Transform child in ArrivePart.GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.SetActive(true);
        }
    }
    public void TSDMClick()                                         //네비게이션의 운송편 수급 관리 버튼 클릭시 실행할 함수로 기능메뉴의 활성화를 담당
    {
        ConclusionPart.SetActive(false);
        SendPart.SetActive(false);
        ArrivePart.SetActive(false);
        TSDMPart.SetActive(true);

        foreach (Transform child in TSDMPart.GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.SetActive(true);
        }
    }

    public void InfoOnOffClick()                                    //정보패널을 보여주고 닫는 버튼을 눌렀을 때 실행할 함수
    {
        Text infoOnOff = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>();
        if (InfoPanelArea.activeInHierarchy)
        {
            InfoPanelArea.SetActive(false);
            infoOnOff.text = "...";
        }
        else
        {
            InfoPanelArea.SetActive(true);
            infoOnOff.text = "X";
        }
    }

    public void OnClickDistributionMain()                           //Section부분의 맨 처음 물류 관제 그래프 탭을 눌렀을 때 하차, 상차, 물동량, 상황전시, 인접거리 등을 보여주기 위한 버튼 함수
    {
        GameObject mainFunctionArrangement = MainFunction1.transform.parent.gameObject;
        foreach(Transform objTransform in mainFunctionArrangement.transform)
        {
            if(objTransform == mainFunctionArrangement.transform)
            {
                objTransform.gameObject.SetActive(true);
            }

            else objTransform.gameObject.SetActive(false);
        }

        MainFunction1.SetActive(true);
        MainFunction2.SetActive(true);
    }

    public void OnClickFunctionBtn()                                //기능메뉴의 항목버튼을 눌렀을 대 실행할 함수
    {
        // 곽경민 : 누른 기능메뉴의 색상을 변경해준다.
        Button clickBtn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        string clickBtnText = clickBtn.GetComponentInChildren<Text>().text;
        if (clickBtn.transform.parent.name.Equals("Content"))
        {
            FunctionTextColorClear(clickBtn);
            clickBtn.GetComponentInChildren<Text>().color = Color.blue;
        }

        // 곽경민 : SectionBtnScroll_ScrollView의 content에 이미 생성해놓은 기능메뉴 버튼이 있다면 
        //         해당 Section 버튼을 가장 오른쪽으로 보내고 버튼의 OnClick함수 실행 및 스크롤을 가장 오른쪽으로 보낸다.
        //섹션 구분 스크롤뷰
        /*ScrollRect SRect = SectionBtnScroll_Content.parent.transform.parent.GetComponent<ScrollRect>();
        foreach (Button btn in SectionBtnScroll_Content.GetComponentsInChildren<Button>())
        {
            if (btn.GetComponentInChildren<Text>() != null && clickBtnText.Equals(btn.GetComponentInChildren<Text>().text))
            {
                btn.transform.SetSiblingIndex(btn.transform.parent.childCount - 1);
                btn.onClick.Invoke();
                SRect.horizontalNormalizedPosition = 1f;
                return;
            }
        }

        // 곽경민 : 위의 반복문을 통과해 아래로 왔다면 SectionBtnScroll_ScrollView의 content에 기능메뉴 버튼을 생성하고 
        //         버튼의 OnClick함수를 실행 및 스크롤을 가장 오른쪽으로 보낸다.
        string prefabName = "SectionDivide_Prefab";
        GameObject prefab = Resources.Load<GameObject>(prefabName);
        Button newBtn = Instantiate(prefab, SectionBtnScroll_Content).GetComponent<Button>();
        newBtn.onClick.Invoke();
        Text btnText = newBtn.GetComponentInChildren<Text>();
        btnText.text = clickBtnText;

        Canvas.ForceUpdateCanvases();
        SRect.horizontalNormalizedPosition = 1f;*/
    }

    private void FunctionTextColorClear(Button btn)                 //기능 메뉴의 Text 색을 기본 색상인 검정으로 변경
    {
        Transform btn_Parent;
        if (!SettingObject.activeInHierarchy)
        {
            btn_Parent = btn.transform.parent.parent;
        }
        else
        {
            btn_Parent = btn.transform.parent;
        }
        foreach(Text text in btn_Parent.GetComponentsInChildren<Text>(true))
        {
            text.color = Color.black;
        }
    }

    public void OnClickSetting_In_Type()                            //설정 버튼 눌렀을 때
    {
        LastClickBtnName = string.Empty;
        SettingObject.SetActive(true);
    }
    public void OnClickSetting_Out_Type()                           //설정 버튼 나가기 눌렀을 때
    {
        AddNaviBtn();
        DataCenter.ShortDistancePrefabActive();
        SettingObject.SetActive(false);
    }

    private void AddNaviBtn()                                       //설정안의 기능메뉴 눌렀을 때 실행할 함수
    {
        string addBtnText = PlayerPrefs.GetString("addNavi", "");

        //버튼의 개수가 5개 이하일 때
        if (!addBtnText.Equals("") && NavigationBtnArea.GetComponentsInChildren<Button>().Length < 5)
        {
            foreach (Button btn in FunctionContent.GetComponentsInChildren<Button>(true))
            {
                string tempText = btn.GetComponentInChildren<Text>(true).text;
                if (addBtnText.Equals(tempText))
                {
                    addButton = Instantiate(btn, NavigationBtnArea.transform);
                    Color color = addButton.GetComponent<Image>().color;
                    color = new Color(color.r, color.g, color.b, 1f);
                    addButton.GetComponent<Image>().color = color;
                    Text btnText = addButton.GetComponentInChildren<Text>();
                    btnText.fontSize = 20;
                    btnText.alignment = TextAnchor.MiddleCenter;
                    btnText.horizontalOverflow = HorizontalWrapMode.Overflow;

                    foreach (Button tempBtn in AddNavigationLabel_Text.transform.parent.GetComponentsInChildren<Button>())
                    {
                        return;
                    }

                    string prefabName = "AddNavigationButton_Prefab";
                    GameObject prefab = Resources.Load<GameObject>(prefabName);
                    GameObject newBtn = Instantiate(prefab, AddNavigationLabel_Text.transform.parent.transform);
                    Button addSettingBtn = newBtn.GetComponent<Button>();

                    addSettingBtn.onClick.AddListener(() => OnClickAddedBtn());
                    addSettingBtn.GetComponentInChildren<Text>().text = btnText.text;

                    break;
                }
            }
        }
    }

    public void OnClickSettingFunctionBtn()                         //설정창의 기능메뉴를 눌렀을 때 실행할 함수
    {
        Button clickBtn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        string clickLabel = clickBtn.GetComponentInChildren<Text>().text;
        LastClickBtnName = clickBtn.name;
        LastClickBtnLabel = clickLabel;
        FunctionTextColorClear(clickBtn);
        clickBtn.GetComponentInChildren<Text>().color = Color.blue;
        
        foreach(Button btn in AddNavigationLabel_Text.transform.parent.GetComponentsInChildren<Button>())
        {
            return;
        }

        string prefabName = "AddNavigationButton_Prefab";
        GameObject prefab = Resources.Load<GameObject>(prefabName);
        GameObject newBtn = Instantiate(prefab, AddNavigationLabel_Text.transform.parent.transform);
        Button addSettingBtn = newBtn.GetComponent<Button>();
        
        addSettingBtn.onClick.AddListener(() => OnClickAddedBtn());
        addSettingBtn.GetComponentInChildren<Text>().text = clickLabel;
        
        PlayerPrefs.SetString("addNavi", clickLabel);
        PlayerPrefs.Save();
    }
    private void OnClickAddedBtn()                                  //설정에서 추가되어있는 버튼을 눌렀을 때 실행할 함수
    {
        GameObject clickBtn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>().gameObject;
        if(addButton != null)
            DestroyImmediate(addButton.gameObject);

        Debug.Log(addButton);
        PlayerPrefs.DeleteKey("addNavi");
        DestroyImmediate(clickBtn);
    }
    public void OnClickDistributionILMSSetting()                    //설정 창의 기능메뉴 스크롤의 맨 위 "물류 ILMS 위치 설정"을 눌렀을 때 가제트 위치를 설정할 수 있는 설정창을 보여주는 함수
    {
        MainFunctionSettingArea.SetActive(true);
    }
    public void OnClickUnload()                                     //"물류 ILMS 위치 설정"의 하차물류를 눌렀을 때 함수
    {
        Transform Object_Parent = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>().parent;
        Object_Parent.SetSiblingIndex(0);
        MainFunctionSettingPanel.SetSiblingIndex(MainFunctionSettingPanel.GetSiblingIndex() - 1);
        SelectFunc = "하차,";
    }
    public void OnClickLoad()                                       //"물류 ILMS 위치 설정"의 상차물류를 눌렀을 때 함수
    {
        Transform Object_Parent = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>().parent;
        Object_Parent.SetSiblingIndex(0);
        MainFunctionSettingPanel.SetSiblingIndex(MainFunctionSettingPanel.GetSiblingIndex() - 1);
        SelectFunc = "상차,";
    }
    public void OnClickVolume()                                     //"물류 ILMS 위치 설정"의 물류량을 눌렀을 때 함수
    {
        Transform Object_Parent = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>().parent;
        Object_Parent.SetSiblingIndex(0);
        MainFunctionSettingPanel.SetSiblingIndex(MainFunctionSettingPanel.GetSiblingIndex() - 1);
        SelectFunc = "물동량,";
    }
    public void OnClickSituation()                                  //"물류 ILMS 위치 설정"의 상황전시를 눌렀을 때 함수
    {
        Transform Object_Parent = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>().parent;
        Object_Parent.SetSiblingIndex(0);
        Transform uncle = Object_Parent.parent.Find("TypeBtnSet").GetComponentInChildren<Transform>();
        uncle.SetSiblingIndex(0);
        MainFunctionSettingPanel.SetSiblingIndex(MainFunctionSettingPanel.GetSiblingIndex() - 1);
        SelectFunc = "상황전시,";
        MainType = "상황리스트,";
    }
    public void OnClickShortDistance()                              //"물류 ILMS 위치 설정"의 IMC 인접거리 운행차량을 눌렀을 때 함수
    {
        Transform Object_Parent = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>().parent;
        Object_Parent.SetSiblingIndex(0);
        Transform uncle = Object_Parent.parent.Find("TypeBtnSet").GetComponentInChildren<Transform>();
        uncle.SetSiblingIndex(0);
        MainFunctionSettingPanel.SetSiblingIndex(MainFunctionSettingPanel.GetSiblingIndex() - 1);
        SelectFunc = "IMC,";
        MainType = "리스트,";
    }

    public void OnClickSettingCircle()                              //그래프의 원형 타입을 눌렀을 때 함수
    {
        ChangeHierarchy();

        MainType = "원형,";
    }

    public void OnClickSettingBarHorizontal()                       //그래프의 Bar(가로) 타입을 눌렀을 때 함수
    {
        ChangeHierarchy();

        MainType = "Bar(가로),";
    }
    public void OnClickSettingVertical()                            //그래프의 Bar(세로) 타입을 눌렀을 때 함수
    {
        ChangeHierarchy();

        MainType = "Bar(세로),";
    }
    public void OnClickSettingBarComparisonAnalysis()               //그래프의 Bar(비교분석) 타입을 눌렀을 때 함수
    {
        ChangeHierarchy();

        MainType = "Bar(비교분석),";
    }
    private void ChangeHierarchy()
    {
        Transform Object_Parent = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>().parent;
        Object_Parent.SetSiblingIndex(0);
        MainFunctionSettingPanel.SetSiblingIndex(MainFunctionSettingPanel.GetSiblingIndex() - 1);
    }

    public void OnClickSetting_First()                              //firstArea를 눌렀을 때 함수
    {
        if (!MainType.Equals(string.Empty))
        {
            DoubleCheck();

            SaveArray(0, "First Area");
        }
    }
    public void OnClickSetting_Second()                             //SecondArea를 눌렀을 때 함수
    {
        if (!MainType.Equals(string.Empty))
        {
            DoubleCheck();

            SaveArray(1, "Second Area");
        }
    }
    public void OnClickSetting_Third()                              //ThirdArea를 눌렀을 때 함수
    {
        if (!MainType.Equals(string.Empty))
        {
            DoubleCheck();

            SaveArray(2, "Third Area");
        }
    }
    public void OnClickSetting_Fourth()                             //FourthArea를 눌렀을 때 함수
    {
        if (!MainType.Equals(string.Empty))
        {
            DoubleCheck();

            SaveArray(3, "Fourth Area");
        }
    }
    public void OnClickSetting_Fifth()                              //FifthArea를 눌렀을 때 함수
    {
        if (!MainType.Equals(string.Empty))
        {
            DoubleCheck();

            SaveArray(4, "Fifth Area");
        }
    }
    public void OnClickSetting_Sixth()                              //SixthArea를 눌렀을 때 함수
    {
        if (!MainType.Equals(string.Empty))
        {
            DoubleCheck();

            SaveArray(5, "Sixth Area");
        }
    }

    private void SaveArray(int index, string area)                  //MainFunctionArray에 5가지 영역에 저장하는 함수
    {
        MainFunctionArray[index] = SelectFunc + MainType;
        Text clickBtnText = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>();
        string tempString = area + "\n" + SelectFunc + MainType;
        clickBtnText.text = tempString.Substring(0, tempString.Length - 1);

        Transform Object_GrandParent = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>().parent.parent;
        Object_GrandParent.SetSiblingIndex(0);
        MainFunctionSettingPanel.SetSiblingIndex(MainFunctionSettingPanel.GetSiblingIndex() - 1);

        for (int i = 0; i < 5; i++)
        {
            if (MainFunctionArray[i] == null || MainFunctionArray[i].Equals(string.Empty))
            {
                MainFunctionSaveBtn.interactable = false;
                break;
            }
            if (i == 4)
            {
                MainFunctionSaveBtn.interactable = true;
            }
        }
    }

    public void OnClickMainFunctionSettingSave()                    //"물류 ILMS 위치 설정"의 적용버튼을 눌렀을 때 함수
    {
        string preSaveString = PlayerPrefs.GetString("MainSection1Setting", ",,,,,,,,,,");
        string mainFuncSave = string.Empty;

        DataCenter.StopRoutine();

        for (int i = 0; i < 5; i++)
        {
            mainFuncSave += MainFunctionArray[i];
        }

        if(!preSaveString.Equals(mainFuncSave)) 
        {
            PlayerPrefs.SetString("MainSection1Setting", mainFuncSave);
            PlayerPrefs.Save();
            Debug.Log(mainFuncSave);

            if (!preSaveString.Equals(PlayerPrefs.GetString("MainSection1Setting", ",,,,,,,,,,")))
            {
                foreach (Transform obj in MainFunction1.GetComponentsInChildren<Transform>())
                {
                    if (obj != MainFunction1.transform && obj != null)
                        DestroyImmediate(obj.gameObject);
                }
                foreach (Transform obj in MainFunction2.GetComponentsInChildren<Transform>())
                {
                    if (obj != MainFunction2.transform && obj != null)
                        DestroyImmediate(obj.gameObject);
                }
                Canvas.ForceUpdateCanvases();
            }

            MainFunctionActivate();

            for (int k = 0; k < MainFunctionArray.Length; k++)
            {
                MainFunctionArray[k] = string.Empty;
            }
        }

        foreach (Button btn in MainFunctionSettingArea.GetComponentsInChildren<Button>())
        {
            if (btn.name.Contains("First")) btn.GetComponentInChildren<Text>().text = "First Area";
            else if (btn.name.Contains("Second")) btn.GetComponentInChildren<Text>().text = "Second Area";
            else if (btn.name.Contains("Third")) btn.GetComponentInChildren<Text>().text = "Third Area";
            else if (btn.name.Contains("Fourth")) btn.GetComponentInChildren<Text>().text = "Fourth Area";
            else if (btn.name.Contains("Fifth")) btn.GetComponentInChildren<Text>().text = "Fifth Area";
        }

        MainFunctionSaveBtn.interactable = false;
        MainFunctionSettingArea.SetActive(false);
    }
    public void OnClickMainFunctionSettingCancel()                  //"물류 ILMS 위치 설정"의 취소버튼을 눌렀을 때 함수
    {
        for (int k = 0; k < MainFunctionArray.Length; k++)
        {
            MainFunctionArray[k] = string.Empty;
        }

        foreach (Button btn in MainFunctionSettingArea.GetComponentsInChildren<Button>())
        {
            if (btn.name.Contains("First")) btn.GetComponentInChildren<Text>().text = "First Area";
            else if (btn.name.Contains("Second")) btn.GetComponentInChildren<Text>().text = "Second Area";
            else if (btn.name.Contains("Third")) btn.GetComponentInChildren<Text>().text = "Third Area";
            else if (btn.name.Contains("Fourth")) btn.GetComponentInChildren<Text>().text = "Fourth Area";
            else if (btn.name.Contains("Fifth")) btn.GetComponentInChildren<Text>().text = "Fifth Area";
        }

        MainFunctionSettingArea.SetActive(false);
    }

    private void DoubleCheck()                                      //중복 허용이 안되어있을 때 실행할 함수
    {
        if (!DuplicationCheck)          //증복 허용이 안되어있다면? 어떻게 할거지?
        {
            for (int i = 0; i < 5; i++)
            {
                if (MainFunctionArray[i] != null && MainFunctionArray[i].Contains(SelectFunc))   //이게 통과 되면? 중복 되는게 있다는 소리니까
                {
                    string btnText = string.Empty;
                    if (i == 0) btnText = "First Area";
                    else if (i == 1) btnText = "Second Area";
                    else if (i == 2) btnText = "Third Area";
                    else if (i == 3) btnText = "Fourth Area";
                    else if (i == 4) btnText = "Fifth Area";
                    else if (i == 5) btnText = "Sixth Area";
                    MainFunctionArray[i] = string.Empty;
                    Transform parent = EventSystem.current.currentSelectedGameObject.GetComponent<Button>().transform.parent.transform.parent;
                    parent.GetComponentsInChildren<Button>()[i].GetComponentInChildren<Text>().text = btnText;
                }
            }
        }
    }

    public void OnChangeToggle()                                    //중복 허용 토글을 눌렀을 때 함수
    {
        DuplicationCheck ^= true;
        if (!DuplicationCheck)
        {
            foreach (Button btn in MainFunctionSettingArea.GetComponentsInChildren<Button>())
            {
                if (btn.name.Contains("First")) btn.GetComponentInChildren<Text>().text = "First Area";
                else if (btn.name.Contains("Second")) btn.GetComponentInChildren<Text>().text = "Second Area";
                else if (btn.name.Contains("Third")) btn.GetComponentInChildren<Text>().text = "Third Area";
                else if (btn.name.Contains("Fourth")) btn.GetComponentInChildren<Text>().text = "Fourth Area";
                else if (btn.name.Contains("Fifth")) btn.GetComponentInChildren<Text>().text = "Fifth Area";
            }
            for(int i = 0; i < 5; i++) MainFunctionArray[i] = string.Empty;

            MainFunctionSaveBtn.interactable = false;
        }
    }

    private void MainFunctionActivate()                             //주 컨텐츠의 물류 그래프 프리펩을 생성하는 함수
    {
        string section1Setting = PlayerPrefs.GetString("MainSection1Setting", ",,,,,,,,,,");
        Debug.Log(section1Setting);
        string[] setting = section1Setting.Split(',');
        Array.Resize(ref setting, 10);

        prefabWidth = MainFunction1.GetComponent<RectTransform>().rect.width / 3;
        prefabHeight = MainFunction1.GetComponent<RectTransform>().rect.height;
        Vector2 sizeDelta;
        sizeDelta.x = prefabWidth;
        sizeDelta.y = prefabHeight;

        for (int j = 0; j < setting.Length; j++)
        {
            if (j == 2) sizeDelta.x *= 2;
            if (j == 4) sizeDelta.x /= 2;

            GameObject parentPrefabObj = null;

            if (j < 4)
            {
                if (setting[j].Equals("하차"))
                {
                    string prefabName = "MainContentPrefabs/UnloadingVolume_Prefab";
                    GameObject parent = Resources.Load<GameObject>(prefabName);
                    RectTransform prefabRect = parent.GetComponent<RectTransform>();
                    prefabRect.sizeDelta = sizeDelta;

                    if (parent != null)
                    {
                        parentPrefabObj = Instantiate(parent, MainFunction1.transform);
                    }
                }
                else if (setting[j].Equals("상차"))
                {
                    string prefabName = "MainContentPrefabs/LoadingVolume_Prefab";
                    GameObject parent = Resources.Load<GameObject>(prefabName);
                    RectTransform prefabRect = parent.GetComponent<RectTransform>();
                    prefabRect.sizeDelta = sizeDelta;

                    if (parent != null)
                    {
                        parentPrefabObj = Instantiate(parent, MainFunction1.transform);
                    }
                }
                else if (setting[j].Equals("물동량"))
                {
                    string prefabName = "MainContentPrefabs/TotalVolume_Prefab";
                    GameObject parent = Resources.Load<GameObject>(prefabName);
                    RectTransform prefabRect = parent.GetComponent<RectTransform>();
                    prefabRect.sizeDelta = sizeDelta;

                    if (parent != null)
                    {
                        parentPrefabObj = Instantiate(parent, MainFunction1.transform);
                    }
                }
                else if (setting[j].Equals("상황전시"))
                {
                    string prefabName = "MainContentPrefabs/SituationDisplay_Prefab";
                    GameObject parent = Resources.Load<GameObject>(prefabName);
                    RectTransform prefabRect = parent.GetComponent<RectTransform>();
                    prefabRect.sizeDelta = sizeDelta;

                    if (parent != null)
                    {
                        parentPrefabObj = Instantiate(parent, MainFunction1.transform);
                    }
                }
                else if (setting[j].Equals("IMC"))
                {
                    string prefabName = "MainContentPrefabs/ShortDistanceVehicle_Prefab";
                    GameObject parent = Resources.Load<GameObject>(prefabName);
                    RectTransform prefabRect = parent.GetComponent<RectTransform>();
                    prefabRect.sizeDelta = sizeDelta;

                    if (parent != null)
                    {
                        parentPrefabObj = Instantiate(parent, MainFunction1.transform);
                    }
                }

                Canvas.ForceUpdateCanvases();

                j++;
                if (setting[j].Equals("원형"))
                {
                    string prefabName = "GraphPrefabs/PieChart";
                    GameObject prefab = Resources.Load<GameObject>(prefabName);
                    RectTransform prefabRect = prefab.GetComponent<RectTransform>();
                    prefabRect.sizeDelta = sizeDelta * 0.7f;

                    if (prefab != null)
                    {
                        Instantiate(prefab, parentPrefabObj.transform);
                    }
                }
                else if (setting[j].Equals("Bar(가로)"))
                {
                    string prefabName = "GraphPrefabs/BarChart";
                    GameObject prefab = Resources.Load<GameObject>(prefabName);

                    if (prefab != null)
                    {
                        Instantiate(prefab, parentPrefabObj.transform);
                    }
                }
                else if (setting[j].Equals("Bar(세로)"))
                {
                    string prefabName = "GraphPrefabs/BarChart";
                    GameObject prefab = Resources.Load<GameObject>(prefabName);

                    if (prefab != null)
                    {
                        Instantiate(prefab, parentPrefabObj.transform);
                    }
                }
                else if (setting[j].Equals("Bar(비교분석)"))
                {
                    string prefabName = "GraphPrefabs/BarChart";
                    GameObject prefab = Resources.Load<GameObject>(prefabName);

                    if (prefab != null)
                    {
                        Instantiate(prefab, parentPrefabObj.transform);
                    }
                }

            }
            else if (j >= 4)
            {
                if (setting[j].Equals("하차"))
                {
                    string prefabName = "MainContentPrefabs/UnloadingVolume_Prefab";
                    GameObject parent = Resources.Load<GameObject>(prefabName);
                    RectTransform prefabRect = parent.GetComponent<RectTransform>();
                    prefabRect.sizeDelta = sizeDelta;

                    if (parent != null)
                    {
                        parentPrefabObj = Instantiate(parent, MainFunction2.transform);
                    }
                }
                else if (setting[j].Equals("상차"))
                {
                    string prefabName = "MainContentPrefabs/LoadingVolume_Prefab";
                    GameObject parent = Resources.Load<GameObject>(prefabName);
                    RectTransform prefabRect = parent.GetComponent<RectTransform>();
                    prefabRect.sizeDelta = sizeDelta;

                    if (parent != null)
                    {
                        parentPrefabObj = Instantiate(parent, MainFunction2.transform);
                    }
                }
                else if (setting[j].Equals("물동량"))
                {
                    string prefabName = "MainContentPrefabs/TotalVolume_Prefab";
                    GameObject parent = Resources.Load<GameObject>(prefabName);
                    RectTransform prefabRect = parent.GetComponent<RectTransform>();
                    prefabRect.sizeDelta = sizeDelta;

                    if (parent != null)
                    {
                        parentPrefabObj = Instantiate(parent, MainFunction2.transform);
                    }
                }
                else if (setting[j].Equals("상황전시"))
                {
                    string prefabName = "MainContentPrefabs/SituationDisplay_Prefab";
                    GameObject parent = Resources.Load<GameObject>(prefabName);
                    RectTransform prefabRect = parent.GetComponent<RectTransform>();
                    prefabRect.sizeDelta = sizeDelta;

                    if (parent != null)
                    {
                        parentPrefabObj = Instantiate(parent, MainFunction2.transform);
                    }
                }
                else if (setting[j].Equals("IMC"))
                {
                    string prefabName = "MainContentPrefabs/ShortDistanceVehicle_Prefab";
                    GameObject parent = Resources.Load<GameObject>(prefabName);
                    RectTransform prefabRect = parent.GetComponent<RectTransform>();
                    prefabRect.sizeDelta = sizeDelta;

                    if (parent != null)
                    {
                        parentPrefabObj = Instantiate(parent, MainFunction2.transform);
                    }
                }

                Canvas.ForceUpdateCanvases();
                j++;
                if (setting[j].Equals("원형"))
                {
                    string prefabName = "GraphPrefabs/PieChart";
                    GameObject prefab = Resources.Load<GameObject>(prefabName);
                    RectTransform prefabRect = prefab.GetComponent<RectTransform>();
                    prefabRect.sizeDelta = sizeDelta * 0.7f;

                    if (prefab != null)
                    {
                        Instantiate(prefab, parentPrefabObj.transform);
                    }
                }
                else if (setting[j].Equals("Bar(가로)"))
                {
                    string prefabName = "GraphPrefabs/BarChart";
                    GameObject prefab = Resources.Load<GameObject>(prefabName);

                    if (prefab != null)
                    {
                        Instantiate(prefab, parentPrefabObj.transform);
                    }
                }
                else if (setting[j].Equals("Bar(세로)"))
                {
                    string prefabName = "GraphPrefabs/BarChart";
                    GameObject prefab = Resources.Load<GameObject>(prefabName);

                    if (prefab != null)
                    {
                        Instantiate(prefab, parentPrefabObj.transform);
                    }
                }
                else if (setting[j].Equals("Bar(비교분석)"))
                {
                    string prefabName = "GraphPrefabs/BarChart";
                    GameObject prefab = Resources.Load<GameObject>(prefabName);

                    if (prefab != null)
                    {
                        Instantiate(prefab, parentPrefabObj.transform);
                    }
                }
            }
        }
    }
    
    public void mainClear()                                         //복사하기 버튼에 아무 기능이 없어서 일단 초기화 저장해놓은 가제트 모듈 초기화 하는 함수로 사용
    {
        PlayerPrefs.DeleteKey("MainSection1Setting");

        int i = 0;
        foreach (Transform obj in MainFunction1.GetComponentsInChildren<Transform>())
        {
            if (obj != null && obj != MainFunction1.transform)
                DestroyImmediate(obj.gameObject);
        }
        foreach (Transform obj in MainFunction2.GetComponentsInChildren<Transform>())
        {
            if (obj != null && obj != MainFunction2.transform)
                DestroyImmediate(obj.gameObject);
        }

        MainFunctionActivate();
    }
}
