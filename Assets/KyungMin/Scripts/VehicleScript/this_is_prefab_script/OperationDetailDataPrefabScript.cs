using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationDetailDataPrefabScript : MonoBehaviour
{

    [Header("UI Ç¥½Ã")]
    public Text sn_Text;
    public Text netType_Text;
    public Text carNum_Text;
    public Text driverPhoneNum_Text;
    public Text departOfficeName_Text;
    public Text arriveOfficeName_Text;
    public Text lateTime_Text;
    public Text departExpectTime_Text;
    public Text arriveExpectTime_Text;
    public Text finalLateTime_Text;
    public Text deliveryDate_Text;
    public Text driverName_Text;
    public Text departArriveStatus_Text;
    public Text transitCheckNum_Text;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(int sn, OperationDetailInfo data)
    {
        sn_Text.text = sn.ToString();
        netType_Text.text = data.netType;
        carNum_Text.text = data.carNum;
        driverPhoneNum_Text.text = data.driverPhoneNum;
        departOfficeName_Text.text = data.secondOfficeName;
        arriveOfficeName_Text.text= data.secondOfficeNamee;
        departExpectTime_Text.text = data.departExpectTime;
        arriveExpectTime_Text.text = data.arriveExpectTime;
        driverName_Text.text = data.driverName;
        departArriveStatus_Text.text = data.departArriveStatus;
        transitCheckNum_Text.text = data.transitCheckNum;
    }
}
