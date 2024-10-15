using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public class Project_Login : MonoBehaviour
{
    public InputField Name_Input;
    public InputField Password_Input;
    public Toggle Remember_Toggle;

    private bool LoginRemember_state = false;
    public bool TMS_state = false;
    public bool ILMS_state = false;
    public bool HWM_state = false;

    public void LoginRemember_Check()
    {
        LoginRemember_state ^= true;
    }
    public void TMS_Check()
    {
        TMS_state ^= true;
    }
    public void ILMS_Check()
    {
        ILMS_state ^= true;
    }
    public void HWM_Check()
    {
        HWM_state ^= true;
    }

    public void LoginBtn_Click()
    {
        string name = Name_Input.text;
        string password = Password_Input.text;

        //User Name과 Password에 무언가 입력이 되었을 때 
        if (name == "" || password == "")
        {
            string prefab_name = "ErrorPopup_Prefab";
            GameObject prefab = Resources.Load<GameObject>(prefab_name);
            ErrorPopupMessage text = prefab.GetComponent<ErrorPopupMessage>();

            if (prefab != null)
            {
                text.Receive_Message = "이름과 비밀번호를 확인해 주세요.";
                Instantiate(prefab, gameObject.transform);
            }
        }
        //차량관제, 물류관제, 장비관제가 모두 체크가 되어있지 않을 때
        else if(!TMS_state && !ILMS_state && !HWM_state)
        {
            //팝업으로 띄울 프리펩의 Object Name
            //리소스 폴더에서 prefab_name의 문자열로 프리펩을 찾아 prefab에 대입한다.
            //프리펩 오브젝트의 컴포넌트 스크립트를 불러온다.
            string prefab_name = "ErrorPopup_Prefab";
            GameObject prefab = Resources.Load<GameObject>(prefab_name);
            ErrorPopupMessage text = prefab.GetComponent<ErrorPopupMessage>();

            /*
            프리펩을 찾았을 때 프리펩 스크립트의 변수에 표출할 문자열 대입 후
            현재 스크립트가 들어있는 오브젝트의 자식 객체로 프리펩을 생성.
             */
            if (prefab != null)
            {
                text.Receive_Message = "차량관제, 물류관제, 장비관제 중 최소 한개 이상 체크 해 주세요.";
                Instantiate(prefab, gameObject.transform);
            }
        }
        //else if(디비의 정보와 로그인하려는 정보가 일치하지 않는다는 조건)에 따른 문장도 있어야함.
        //


        else
        {
            //로그인 정보 기억하기가 체크 되어있을 때 FinalLogin_Info를 키값으로 Name과 Password를 저장한다.
            if (LoginRemember_state)
            {
                PlayerPrefs.SetString("FinalLogin_Info", name + "," + password);
                PlayerPrefs.Save();
            }
            //아닐 때 키 FinalLogin_Info를 삭제한다.
            else
                PlayerPrefs.DeleteKey("FinalLogin_Info");
        }
    }

    private void OnEnable()
    {
        //FinalLogin_Info의 키값을 불러와 파싱 후 로그인 InputField에 기입 시켜놓는다.
        string pre_data = PlayerPrefs.GetString("FinalLogin_Info", null);
        if(pre_data != "")
        {
            string[] pre_login = pre_data.Split(",");
            Name_Input.text = pre_login[0];
            Password_Input.text = pre_login[1];
            Remember_Toggle.isOn = true;
            LoginRemember_state = true;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
