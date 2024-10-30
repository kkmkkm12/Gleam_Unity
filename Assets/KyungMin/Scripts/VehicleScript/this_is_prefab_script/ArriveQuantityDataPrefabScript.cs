using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArriveQuantityDataPrefabScript : MonoBehaviour
{
    [Header("ui ¸ñ·Ï")]
    public Text secondOfficeName_Text;
    public Text packageGeneral_Text;
    public Text packageWindow_Text;
    public Text factorQuantity_Text;
    public Text receiptQuantity_Text;
    public Text littlelittlesum_Text;
    public Text littleSum_Text;

    public Text international_Text;
    public Text totalSum_Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(ArriveQuantityInfo data)
    {
        secondOfficeName_Text.text = data.secondOfficeName;
        packageGeneral_Text.text = (string)data.packageGeneral.ToString();
        packageWindow_Text.text = (string)data.packageWindow.ToString();
        factorQuantity_Text.text = (string)data.factorQuantity.ToString();
        littleSum_Text.text = (string)(data.packageWindow + data.factorQuantity).ToString();
    }
}
