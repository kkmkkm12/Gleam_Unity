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

        //User Name�� Password�� ���� �Է��� �Ǿ��� �� 
        if (name == "" || password == "")
        {
            string prefab_name = "ErrorPopup_Prefab";
            GameObject prefab = Resources.Load<GameObject>(prefab_name);
            ErrorPopupMessage text = prefab.GetComponent<ErrorPopupMessage>();

            if (prefab != null)
            {
                text.Receive_Message = "�̸��� ��й�ȣ�� Ȯ���� �ּ���.";
                Instantiate(prefab, gameObject.transform);
            }
        }
        //��������, ��������, �������� ��� üũ�� �Ǿ����� ���� ��
        else if(!TMS_state && !ILMS_state && !HWM_state)
        {
            //�˾����� ��� �������� Object Name
            //���ҽ� �������� prefab_name�� ���ڿ��� �������� ã�� prefab�� �����Ѵ�.
            //������ ������Ʈ�� ������Ʈ ��ũ��Ʈ�� �ҷ��´�.
            string prefab_name = "ErrorPopup_Prefab";
            GameObject prefab = Resources.Load<GameObject>(prefab_name);
            ErrorPopupMessage text = prefab.GetComponent<ErrorPopupMessage>();

            /*
            �������� ã���� �� ������ ��ũ��Ʈ�� ������ ǥ���� ���ڿ� ���� ��
            ���� ��ũ��Ʈ�� ����ִ� ������Ʈ�� �ڽ� ��ü�� �������� ����.
             */
            if (prefab != null)
            {
                text.Receive_Message = "��������, ��������, ������ �� �ּ� �Ѱ� �̻� üũ �� �ּ���.";
                Instantiate(prefab, gameObject.transform);
            }
        }
        //else if(����� ������ �α����Ϸ��� ������ ��ġ���� �ʴ´ٴ� ����)�� ���� ���嵵 �־����.
        //


        else
        {
            //�α��� ���� ����ϱⰡ üũ �Ǿ����� �� FinalLogin_Info�� Ű������ Name�� Password�� �����Ѵ�.
            if (LoginRemember_state)
            {
                PlayerPrefs.SetString("FinalLogin_Info", name + "," + password);
                PlayerPrefs.Save();
            }
            //�ƴ� �� Ű FinalLogin_Info�� �����Ѵ�.
            else
                PlayerPrefs.DeleteKey("FinalLogin_Info");
        }
    }

    private void OnEnable()
    {
        //FinalLogin_Info�� Ű���� �ҷ��� �Ľ� �� �α��� InputField�� ���� ���ѳ��´�.
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
