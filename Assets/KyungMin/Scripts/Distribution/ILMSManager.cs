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
    public DataManager DataCenter;                                  //�������� �׷��� �����͸� �޴� ��ũ��Ʈ

    public GameObject InfoPanelArea;                                //���� �г��� ������� ������Ʈ
    public GameObject NavigationBtnArea;                            //Navigation ��ư�� ������� ������Ʈ
    //public Transform SectionBtnScroll_Content;                      //Ŭ���Ͽ� ����ϴ� ��ɸ޴� ���� ������� Ʈ������
    public GameObject ConclusionPart;                               //��ɸ޴� ��ũ�Ѻ��� ü����� ������Ʈ
    public GameObject SendPart;                                     //" �߼۰��� ������Ʈ
    public GameObject ArrivePart;                                   //" �������� ������Ʈ
    public GameObject TSDMPart;                                     //" ����� ���� ���� ������Ʈ

    public GameObject SettingObject;                                //Dashboard_SettingSet�� �����ϴ� ������Ʈ
    
    public GameObject MainFunctionSettingArea;                      //����ILMS ������ ������ �� ǥ�õǴ� ȭ���� ���� ������Ʈ
    public Transform MainFunctionSettingPanel;                      //����ILMS ������ ��밡�� ��ư�� �����ϱ� ���� �ǳ�
    public Button MainFunctionSaveBtn;                              //����ILMS ������ ���� �� ������ �ϴ¹�ư

    public GameObject MainFunction1;                                //����� ����ILMS UI�� ��� ���� ������Ʈ
    public GameObject MainFunction2;                                //����� ����ILMS UI�� �ϴ� ���� ������Ʈ

    public InputField SearchInputField;                             //��ɸ޴��� �˻��� �� �ִ� InputField
    public GameObject FunctionContent;                              //��ɸ޴��� ����ִ� Content ������Ʈ

    public RectTransform DistributionCanvas;                        //���������� ����ϴ� ĵ����
    private RectTransform MyRect;                                   //���������� �ػ󵵸� ����ϴ� RectTransform                                

    public Camera mainCamera;                                       //���� ī�޶�
    public Camera secondaryCamera;                                  //�ι�° ī�޶�

    private string LastClickBtnName;                                //���ÿ��� ��� �޴� ��ư �� �������� ���� ��ư ��ü �̸��� �����ϴ� ����
    private string LastClickBtnLabel;                               //��ư�� Text�� �����ϴ� ����

    private string[] MainFunctionArray = {"", "", "", "", "" }; //���������� ������ first ~ fifth�� ��ġ�� ���� ������ �����ϴ� ����
    private string MainType;                                        //����, Bar Ÿ���� �����ϴ� ����
    private string SelectFunc;                                      //����, ����, ������, ��Ȳ����, �����Ÿ����� �����ϴ� ����

    public Text AddNavigationLabel_Text;                            //�߰��� �׺���̼� ��ư�� Text

    private Button addButton;                                       //�߰��� �׺���̼� ��ư

    private float prefabWidth;                                      //�� ������ View�� �ʺ� ����ϴ� ����
    private float prefabHeight;                                     //�� ������ View�� ���̸� �����ϴ� ����

    private bool DuplicationCheck = false;                          //�ߺ� ��� üũ�ڽ��� ���� ����ϴ� ����

    private void OnEnable()
    {
        MyRect = GetComponent<RectTransform>();

        //�ػ󵵿� ���� UI ũ�� ����
        MyRect.sizeDelta = new Vector2(DistributionCanvas.rect.width, DistributionCanvas.rect.height);

        MainFunctionActivate();                                     //���� �׷��� �����ϴ� �Լ�
                                                                    
        AddNaviBtn();                                               //�׺���̼� �޴��� �߰��� ��ɸ޴� ��ư �����ϴ� �Լ�
    }

    // Start is called before the first frame update
    void Start()
    {
        // �߰� ���÷��̸� Ȱ��ȭ�մϴ�.
        for (int i = 1; i < Display.displays.Length; i++)           
        {
            Display.displays[i].Activate();
        }

        // �� ��° ���÷��̰� ������ ��� �����մϴ�.
        if (Display.displays.Length > 1)
        {
            secondaryCamera.targetDisplay = 1; // �� ��° ���÷��̿� �Ҵ�
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape") && Input.GetKey("q"))            //���� ����Ű
            Application.Quit();
    }
    public void OnInputfieldEndEdit()                               //��ɸ޴� �˻��� ����Ű�� �˻��ǰ� �ϴ� �Լ�
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
            OnClickSearchBtn();
    }
    public void OnClickSearchBtn()                                  //�˻� ��ư�� �� �Լ�(InputField�� ���� ���� �˻�)
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

    private void FunctionScrollClear()                              //�˻� �� ��ũ���� content�� ������ü�� Ȱ��ȭ ��Ű�� �Լ�
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

    public void ConclusionClick()                                   //�׺���̼��� ü����� ��ư Ŭ���� ������ �Լ��� ��ɸ޴��� Ȱ��ȭ�� ���
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
    public void SendClick()                                         //�׺���̼��� �߼۰��� ��ư Ŭ���� ������ �Լ��� ��ɸ޴��� Ȱ��ȭ�� ���
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
    public void ArriveClick()                                       //�׺���̼��� �������� ��ư Ŭ���� ������ �Լ��� ��ɸ޴��� Ȱ��ȭ�� ���
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
    public void TSDMClick()                                         //�׺���̼��� ����� ���� ���� ��ư Ŭ���� ������ �Լ��� ��ɸ޴��� Ȱ��ȭ�� ���
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

    public void InfoOnOffClick()                                    //�����г��� �����ְ� �ݴ� ��ư�� ������ �� ������ �Լ�
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

    public void OnClickDistributionMain()                           //Section�κ��� �� ó�� ���� ���� �׷��� ���� ������ �� ����, ����, ������, ��Ȳ����, �����Ÿ� ���� �����ֱ� ���� ��ư �Լ�
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

    public void OnClickFunctionBtn()                                //��ɸ޴��� �׸��ư�� ������ �� ������ �Լ�
    {
        // ����� : ���� ��ɸ޴��� ������ �������ش�.
        Button clickBtn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        string clickBtnText = clickBtn.GetComponentInChildren<Text>().text;
        if (clickBtn.transform.parent.name.Equals("Content"))
        {
            FunctionTextColorClear(clickBtn);
            clickBtn.GetComponentInChildren<Text>().color = Color.blue;
        }

        // ����� : SectionBtnScroll_ScrollView�� content�� �̹� �����س��� ��ɸ޴� ��ư�� �ִٸ� 
        //         �ش� Section ��ư�� ���� ���������� ������ ��ư�� OnClick�Լ� ���� �� ��ũ���� ���� ���������� ������.
        //���� ���� ��ũ�Ѻ�
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

        // ����� : ���� �ݺ����� ����� �Ʒ��� �Դٸ� SectionBtnScroll_ScrollView�� content�� ��ɸ޴� ��ư�� �����ϰ� 
        //         ��ư�� OnClick�Լ��� ���� �� ��ũ���� ���� ���������� ������.
        string prefabName = "SectionDivide_Prefab";
        GameObject prefab = Resources.Load<GameObject>(prefabName);
        Button newBtn = Instantiate(prefab, SectionBtnScroll_Content).GetComponent<Button>();
        newBtn.onClick.Invoke();
        Text btnText = newBtn.GetComponentInChildren<Text>();
        btnText.text = clickBtnText;

        Canvas.ForceUpdateCanvases();
        SRect.horizontalNormalizedPosition = 1f;*/
    }

    private void FunctionTextColorClear(Button btn)                 //��� �޴��� Text ���� �⺻ ������ �������� ����
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

    public void OnClickSetting_In_Type()                            //���� ��ư ������ ��
    {
        LastClickBtnName = string.Empty;
        SettingObject.SetActive(true);
    }
    public void OnClickSetting_Out_Type()                           //���� ��ư ������ ������ ��
    {
        AddNaviBtn();
        DataCenter.ShortDistancePrefabActive();
        SettingObject.SetActive(false);
    }

    private void AddNaviBtn()                                       //�������� ��ɸ޴� ������ �� ������ �Լ�
    {
        string addBtnText = PlayerPrefs.GetString("addNavi", "");

        //��ư�� ������ 5�� ������ ��
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

    public void OnClickSettingFunctionBtn()                         //����â�� ��ɸ޴��� ������ �� ������ �Լ�
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
    private void OnClickAddedBtn()                                  //�������� �߰��Ǿ��ִ� ��ư�� ������ �� ������ �Լ�
    {
        GameObject clickBtn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>().gameObject;
        if(addButton != null)
            DestroyImmediate(addButton.gameObject);

        Debug.Log(addButton);
        PlayerPrefs.DeleteKey("addNavi");
        DestroyImmediate(clickBtn);
    }
    public void OnClickDistributionILMSSetting()                    //���� â�� ��ɸ޴� ��ũ���� �� �� "���� ILMS ��ġ ����"�� ������ �� ����Ʈ ��ġ�� ������ �� �ִ� ����â�� �����ִ� �Լ�
    {
        MainFunctionSettingArea.SetActive(true);
    }
    public void OnClickUnload()                                     //"���� ILMS ��ġ ����"�� ���������� ������ �� �Լ�
    {
        Transform Object_Parent = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>().parent;
        Object_Parent.SetSiblingIndex(0);
        MainFunctionSettingPanel.SetSiblingIndex(MainFunctionSettingPanel.GetSiblingIndex() - 1);
        SelectFunc = "����,";
    }
    public void OnClickLoad()                                       //"���� ILMS ��ġ ����"�� ���������� ������ �� �Լ�
    {
        Transform Object_Parent = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>().parent;
        Object_Parent.SetSiblingIndex(0);
        MainFunctionSettingPanel.SetSiblingIndex(MainFunctionSettingPanel.GetSiblingIndex() - 1);
        SelectFunc = "����,";
    }
    public void OnClickVolume()                                     //"���� ILMS ��ġ ����"�� �������� ������ �� �Լ�
    {
        Transform Object_Parent = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>().parent;
        Object_Parent.SetSiblingIndex(0);
        MainFunctionSettingPanel.SetSiblingIndex(MainFunctionSettingPanel.GetSiblingIndex() - 1);
        SelectFunc = "������,";
    }
    public void OnClickSituation()                                  //"���� ILMS ��ġ ����"�� ��Ȳ���ø� ������ �� �Լ�
    {
        Transform Object_Parent = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>().parent;
        Object_Parent.SetSiblingIndex(0);
        Transform uncle = Object_Parent.parent.Find("TypeBtnSet").GetComponentInChildren<Transform>();
        uncle.SetSiblingIndex(0);
        MainFunctionSettingPanel.SetSiblingIndex(MainFunctionSettingPanel.GetSiblingIndex() - 1);
        SelectFunc = "��Ȳ����,";
        MainType = "��Ȳ����Ʈ,";
    }
    public void OnClickShortDistance()                              //"���� ILMS ��ġ ����"�� IMC �����Ÿ� ���������� ������ �� �Լ�
    {
        Transform Object_Parent = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>().parent;
        Object_Parent.SetSiblingIndex(0);
        Transform uncle = Object_Parent.parent.Find("TypeBtnSet").GetComponentInChildren<Transform>();
        uncle.SetSiblingIndex(0);
        MainFunctionSettingPanel.SetSiblingIndex(MainFunctionSettingPanel.GetSiblingIndex() - 1);
        SelectFunc = "IMC,";
        MainType = "����Ʈ,";
    }

    public void OnClickSettingCircle()                              //�׷����� ���� Ÿ���� ������ �� �Լ�
    {
        ChangeHierarchy();

        MainType = "����,";
    }

    public void OnClickSettingBarHorizontal()                       //�׷����� Bar(����) Ÿ���� ������ �� �Լ�
    {
        ChangeHierarchy();

        MainType = "Bar(����),";
    }
    public void OnClickSettingVertical()                            //�׷����� Bar(����) Ÿ���� ������ �� �Լ�
    {
        ChangeHierarchy();

        MainType = "Bar(����),";
    }
    public void OnClickSettingBarComparisonAnalysis()               //�׷����� Bar(�񱳺м�) Ÿ���� ������ �� �Լ�
    {
        ChangeHierarchy();

        MainType = "Bar(�񱳺м�),";
    }
    private void ChangeHierarchy()
    {
        Transform Object_Parent = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>().parent;
        Object_Parent.SetSiblingIndex(0);
        MainFunctionSettingPanel.SetSiblingIndex(MainFunctionSettingPanel.GetSiblingIndex() - 1);
    }

    public void OnClickSetting_First()                              //firstArea�� ������ �� �Լ�
    {
        if (!MainType.Equals(string.Empty))
        {
            DoubleCheck();

            SaveArray(0, "First Area");
        }
    }
    public void OnClickSetting_Second()                             //SecondArea�� ������ �� �Լ�
    {
        if (!MainType.Equals(string.Empty))
        {
            DoubleCheck();

            SaveArray(1, "Second Area");
        }
    }
    public void OnClickSetting_Third()                              //ThirdArea�� ������ �� �Լ�
    {
        if (!MainType.Equals(string.Empty))
        {
            DoubleCheck();

            SaveArray(2, "Third Area");
        }
    }
    public void OnClickSetting_Fourth()                             //FourthArea�� ������ �� �Լ�
    {
        if (!MainType.Equals(string.Empty))
        {
            DoubleCheck();

            SaveArray(3, "Fourth Area");
        }
    }
    public void OnClickSetting_Fifth()                              //FifthArea�� ������ �� �Լ�
    {
        if (!MainType.Equals(string.Empty))
        {
            DoubleCheck();

            SaveArray(4, "Fifth Area");
        }
    }
    public void OnClickSetting_Sixth()                              //SixthArea�� ������ �� �Լ�
    {
        if (!MainType.Equals(string.Empty))
        {
            DoubleCheck();

            SaveArray(5, "Sixth Area");
        }
    }

    private void SaveArray(int index, string area)                  //MainFunctionArray�� 5���� ������ �����ϴ� �Լ�
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

    public void OnClickMainFunctionSettingSave()                    //"���� ILMS ��ġ ����"�� �����ư�� ������ �� �Լ�
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
    public void OnClickMainFunctionSettingCancel()                  //"���� ILMS ��ġ ����"�� ��ҹ�ư�� ������ �� �Լ�
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

    private void DoubleCheck()                                      //�ߺ� ����� �ȵǾ����� �� ������ �Լ�
    {
        if (!DuplicationCheck)          //���� ����� �ȵǾ��ִٸ�? ��� �Ұ���?
        {
            for (int i = 0; i < 5; i++)
            {
                if (MainFunctionArray[i] != null && MainFunctionArray[i].Contains(SelectFunc))   //�̰� ��� �Ǹ�? �ߺ� �Ǵ°� �ִٴ� �Ҹ��ϱ�
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

    public void OnChangeToggle()                                    //�ߺ� ��� ����� ������ �� �Լ�
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

    private void MainFunctionActivate()                             //�� �������� ���� �׷��� �������� �����ϴ� �Լ�
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
                if (setting[j].Equals("����"))
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
                else if (setting[j].Equals("����"))
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
                else if (setting[j].Equals("������"))
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
                else if (setting[j].Equals("��Ȳ����"))
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
                if (setting[j].Equals("����"))
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
                else if (setting[j].Equals("Bar(����)"))
                {
                    string prefabName = "GraphPrefabs/BarChart";
                    GameObject prefab = Resources.Load<GameObject>(prefabName);

                    if (prefab != null)
                    {
                        Instantiate(prefab, parentPrefabObj.transform);
                    }
                }
                else if (setting[j].Equals("Bar(����)"))
                {
                    string prefabName = "GraphPrefabs/BarChart";
                    GameObject prefab = Resources.Load<GameObject>(prefabName);

                    if (prefab != null)
                    {
                        Instantiate(prefab, parentPrefabObj.transform);
                    }
                }
                else if (setting[j].Equals("Bar(�񱳺м�)"))
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
                if (setting[j].Equals("����"))
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
                else if (setting[j].Equals("����"))
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
                else if (setting[j].Equals("������"))
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
                else if (setting[j].Equals("��Ȳ����"))
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
                if (setting[j].Equals("����"))
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
                else if (setting[j].Equals("Bar(����)"))
                {
                    string prefabName = "GraphPrefabs/BarChart";
                    GameObject prefab = Resources.Load<GameObject>(prefabName);

                    if (prefab != null)
                    {
                        Instantiate(prefab, parentPrefabObj.transform);
                    }
                }
                else if (setting[j].Equals("Bar(����)"))
                {
                    string prefabName = "GraphPrefabs/BarChart";
                    GameObject prefab = Resources.Load<GameObject>(prefabName);

                    if (prefab != null)
                    {
                        Instantiate(prefab, parentPrefabObj.transform);
                    }
                }
                else if (setting[j].Equals("Bar(�񱳺м�)"))
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
    
    public void mainClear()                                         //�����ϱ� ��ư�� �ƹ� ����� ��� �ϴ� �ʱ�ȭ �����س��� ����Ʈ ��� �ʱ�ȭ �ϴ� �Լ��� ���
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
