using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class BoxScript : MonoBehaviour
{
    private SplineContainer splineContainer;
    private Transform[] myTransform;
    private RectTransform myRect;
    private SituationDisplayScript situationDisplayScript;

    float slowSpeed = 10f;
    float normalSpeed = 40f;

    //현재 지나는 구역을 저장하는 변수
    private int ZoneIndex = 0;
    private float epsilon = 0.0001f;

    private void Awake()
    {
        splineContainer = transform.parent.Find("IMCConveyorBelt_Spline").GetComponent<SplineContainer>();
        myRect = GetComponent<RectTransform>();
        situationDisplayScript = transform.parent.parent.GetComponent<SituationDisplayScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int arraySize = splineContainer.Spline.Count;
        myTransform = new Transform[arraySize];

        for (int i = 0; i < arraySize; i++)
        {
            myTransform[i] = new GameObject().transform;
            myTransform[i].position = splineContainer.Spline[i].Position;
        }
        myRect.anchoredPosition = myTransform[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (myTransform[0].position.y <= myRect.anchoredPosition.y
                    && myRect.anchoredPosition.x <= myTransform[1].position.x)
        {
            ZoneIndex = 0;
            if (situationDisplayScript.slowZone[ZoneIndex])
            {
                Vector2 moveAmount = new Vector2(slowSpeed * Time.deltaTime, 0);
                myRect.anchoredPosition += moveAmount;
            }
            else
            {
                Vector2 moveAmount = new Vector2(normalSpeed * Time.deltaTime, 0);
                myRect.anchoredPosition += moveAmount;
            }
        }
        else if (myTransform[1].position.y <= myRect.anchoredPosition.y
                    && myRect.anchoredPosition.x <= myTransform[2].position.x)
        {
            ZoneIndex = 1;
            if (situationDisplayScript.slowZone[ZoneIndex])
            {
                Vector2 moveAmount = new Vector2(slowSpeed * Time.deltaTime, 0);
                myRect.anchoredPosition += moveAmount;
            }
            else
            {
                Vector2 moveAmount = new Vector2(normalSpeed * Time.deltaTime, 0);
                myRect.anchoredPosition += moveAmount;
            }
        }
        else if (myTransform[2].position.y <= myRect.anchoredPosition.y
                    && myRect.anchoredPosition.x <= myTransform[3].position.x)
        {
            ZoneIndex = 2;
            if (situationDisplayScript.slowZone[ZoneIndex])
            {
                Vector2 moveAmount = new Vector2(slowSpeed * Time.deltaTime, 0);
                myRect.anchoredPosition += moveAmount;
            }
            else
            {
                Vector2 moveAmount = new Vector2(normalSpeed * Time.deltaTime, 0);
                myRect.anchoredPosition += moveAmount;
            }
        }
        else if (myTransform[3].position.x <= myRect.anchoredPosition.x
                    && myRect.anchoredPosition.y > myTransform[4].position.y
                    && myRect.anchoredPosition.x > 0)
        {
            ZoneIndex = 3;
            if (situationDisplayScript.slowZone[ZoneIndex])
            {
                Vector2 moveAmount = new Vector2(0, -(slowSpeed * Time.deltaTime));
                myRect.anchoredPosition += moveAmount;
            }
            else
            {
                Vector2 moveAmount = new Vector2(0, -(normalSpeed * Time.deltaTime));
                myRect.anchoredPosition += moveAmount;
            }
        }
        else if (myTransform[4].position.y >= myRect.anchoredPosition.y
                    && myRect.anchoredPosition.x > myTransform[5].position.x
                    && myRect.anchoredPosition.y < 0)
        {
            ZoneIndex = 4;
            if (situationDisplayScript.slowZone[ZoneIndex])
            {
                Vector2 moveAmount = new Vector2(-(slowSpeed * Time.deltaTime), 0);
                myRect.anchoredPosition += moveAmount;
            }
            else
            {
                Vector2 moveAmount = new Vector2(-(normalSpeed * Time.deltaTime), 0);
                myRect.anchoredPosition += moveAmount;
            }
        }
        else if (myTransform[5].position.y >= myRect.anchoredPosition.y
                    && myRect.anchoredPosition.x > myTransform[6].position.x
                    && myRect.anchoredPosition.y < 0)
        {
            ZoneIndex = 5;
            if (situationDisplayScript.slowZone[ZoneIndex])
            {
                Vector2 moveAmount = new Vector2(-(slowSpeed * Time.deltaTime), 0);
                myRect.anchoredPosition += moveAmount;
            }
            else
            {
                Vector2 moveAmount = new Vector2(-(normalSpeed * Time.deltaTime), 0);
                myRect.anchoredPosition += moveAmount;
            }
        }
        else if (myTransform[6].position.y >= myRect.anchoredPosition.y
                    && myRect.anchoredPosition.x > myTransform[7].position.x
                    && myRect.anchoredPosition.y < 0)
        {
            ZoneIndex = 6;
            if (situationDisplayScript.slowZone[ZoneIndex])
            {
                Vector2 moveAmount = new Vector2(-(slowSpeed * Time.deltaTime), 0);
                myRect.anchoredPosition += moveAmount;
            }
            else
            {
                Vector2 moveAmount = new Vector2(-(normalSpeed * Time.deltaTime), 0);
                myRect.anchoredPosition += moveAmount;
            }
        }
        else if (myTransform[7].position.x >= myRect.anchoredPosition.x
                    && myRect.anchoredPosition.y < myTransform[0].position.y
                    && myRect.anchoredPosition.x < 0)
        {
            ZoneIndex = 7;
            if (situationDisplayScript.slowZone[ZoneIndex])
            {
                Vector2 moveAmount = new Vector2(0, (slowSpeed * Time.deltaTime));
                myRect.anchoredPosition += moveAmount;
            }
            else
            {
                Vector2 moveAmount = new Vector2(0, (normalSpeed * Time.deltaTime));
                myRect.anchoredPosition += moveAmount;
            }
        }
    }
}

