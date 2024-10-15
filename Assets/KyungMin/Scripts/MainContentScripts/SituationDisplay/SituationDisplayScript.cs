using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class SituationDisplayScript : MonoBehaviour
{
    private DataManager DataManager;

    private Transform[] DockLists;
    public Transform DockListContent;

    private bool firstCreate = false;
    //데이터 메니저에서 어느 구간 정체인지 값이 있을건데 있다면 해당 레일색과 레일을 지나는 상자UI의 속도 조절 해야함.
    public Image[] BeltImage;
    public bool[] slowZone;

    private void Awake()
    {
        DataManager = GameObject.Find("DataCenter").GetComponent<DataManager>();
        slowZone = new bool[BeltImage.Length];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstCreate && DataManager.MaxDockNum != 0)
        {
            firstCreate = true;
            string prefabName = "MainContentPrefabs/DockList";
            GameObject prefab = Resources.Load<GameObject>(prefabName);
            DockLists = new Transform[DataManager.MaxDockNum];
            for(int i = 0; i < DataManager.MaxDockNum; i++)
            {
                DockLists[i] = Instantiate(prefab, DockListContent).transform;
            }
        }
        else if(firstCreate)
        {
            string[][] dockState = DataManager.DockList;
            for (int i = 0; i < DockLists.Length; i++)
            {
                Text[] texts = DockLists[i].GetComponentsInChildren<Text>();
                Image image = DockLists[i].GetComponentInChildren<Image>();
                if (dockState[i] == null)
                {
                    texts[0].text = "도크 " + i.ToString();
                    texts[1].text = "-";
                    texts[2].text = "-";
                    image.color = Color.green;
                    continue;
                }
                texts[0].text = "도크 " + dockState[i][0];
                texts[1].text = dockState[i][1];
                texts[2].text = dockState[i][2];
                image.color = Color.red;
            }
        }

        for(int i = 0; i < BeltImage.Length; i++)
        {
            if (DataManager.belt[i])
            {
                BeltImage[i].color = Color.red;
                slowZone[i] = true;
            }
            else
            {
                BeltImage[i].color = Color.white;
                slowZone[i] = false;
            }
        }
    }
}
