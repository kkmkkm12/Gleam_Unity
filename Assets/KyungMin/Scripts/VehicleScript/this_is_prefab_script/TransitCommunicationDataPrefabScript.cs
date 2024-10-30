using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitCommunicationDataPrefabScript : MonoBehaviour
{
    public Text no_Text;
    public Text date_Text;
    public Text departOfficeName_Text;
    public Text firstOfficeName_Text;
    public Text generalOfficeName_Text;
    public Text arriveOfficeName_Text;
    public Text obstacleType_Text;
    public Text addOffice_Text;
    public Text complete_Text;

    private string addPerson;
    private string obstacleContent;
    private string obstacleRegion;
    private string actionContent;
    private string futurePlan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(int sn, TransitCommunityInfo data)
    {
        no_Text.text = sn.ToString();
        date_Text.text = data.dateTime1;
        departOfficeName_Text.text = data.secondOfficeName2;
        firstOfficeName_Text.text = data.firstOfficeName3;
        generalOfficeName_Text.text = data.secondOfficeName2;
        arriveOfficeName_Text.text = data.secondOfficeName4;
        obstacleType_Text.text = data.obstacleType5;
        addOffice_Text.text = data.secondOfficeName2;

        addPerson = data.addPerson3;
        obstacleContent = data.obstacleContent8;
        obstacleRegion = data.obstacleRegion9;
        actionContent = data.actionContent1;
        futurePlan = data.futurePlan2;
    }
}
