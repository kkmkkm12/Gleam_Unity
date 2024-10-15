using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortDistanceList : MonoBehaviour
{
    private DataManager DataCenter;
    public Text CarDistanceDropdownText;
    public Transform CarDistanceScroll_Content;
    private ScrollRect scrollRectView;
    private void OnEnable()
    {
        DataCenter = GameObject.Find("DataCenter").GetComponent<DataManager>();
        scrollRectView = CarDistanceScroll_Content.parent.parent.GetComponent<ScrollRect>();
        DataCenter.CarDistanceOn = true;
        //DataCenter.ShortDistancePrefabActive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickSearchBtn()
    {
        if (CarDistanceDropdownText.text.Equals("운행 전체"))
        {
            foreach(Button objTransform in CarDistanceScroll_Content.GetComponentsInChildren<Button>(true))
                objTransform.gameObject.SetActive(true);
        }
        else if (CarDistanceDropdownText.text.Equals("3km 이하"))
        {
            foreach (Button objTransform in CarDistanceScroll_Content.GetComponentsInChildren<Button>(true))
            {
                objTransform.gameObject.SetActive(true);
                if (float.Parse(objTransform.GetComponentsInChildren<Text>()[2].text) > 3000.0f)
                    objTransform.gameObject.SetActive(false);
            }
            Canvas.ForceUpdateCanvases();
            scrollRectView.verticalNormalizedPosition = 0f;
        }
        else if (CarDistanceDropdownText.text.Equals("5km 이하"))
        {
            foreach (Button objTransform in CarDistanceScroll_Content.GetComponentsInChildren<Button>(true))
            {
                objTransform.gameObject.SetActive(true);
                if (float.Parse(objTransform.GetComponentsInChildren<Text>()[2].text) > 5000f)
                    objTransform.gameObject.SetActive(false);
            }
            Canvas.ForceUpdateCanvases();
            scrollRectView.verticalNormalizedPosition = 0f;
        }
        else if (CarDistanceDropdownText.text.Equals("10km 이하"))
        {
            foreach (Button objTransform in CarDistanceScroll_Content.GetComponentsInChildren<Button>(true))
            {
                objTransform.gameObject.SetActive(true);
                if (float.Parse(objTransform.GetComponentsInChildren<Text>()[2].text) > 10000)
                    objTransform.gameObject.SetActive(false);
            }
            Canvas.ForceUpdateCanvases();
            scrollRectView.verticalNormalizedPosition = 0f;
        }
    }
}