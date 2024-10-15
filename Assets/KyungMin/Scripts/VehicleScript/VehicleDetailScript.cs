using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleDetailScript : MonoBehaviour
{
    public Text CarNoLabel_Text;
    public Text TotalDistanceLabel_Text;
    public Text RemainDistanceLabel_Text;
    public Text CurrentLocationLabel_Text;
    public Text TransitCheckNoLabel_Text;
    public Text PostCountLabel_Text;
    public Text PalletCountLabel_Text;
    public Text BagCountLabel_Text;
    public Text NoBottleCountLabel_Text;
    public Text FlatPalletCountLabel_Text;
    public Text EachPostCountLabel_Text;
    public Text DomesticCountLabel_Text;
    public Text OverseasCountLabel_Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickCloseDetail();
        }
    }
    public void OnClickCloseDetail()
    {
        Destroy(gameObject);
    }
}
