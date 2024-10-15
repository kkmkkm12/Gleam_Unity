using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class SituationDisplaySplineKnotController : MonoBehaviour
{
    private RectTransform myRect;
    private SplineContainer mySpline;
    private float horizontalLength = 0f;
    private float verticalLength = 0f;

    private void Awake()
    {
        myRect = GetComponent<RectTransform>();
        horizontalLength = myRect.rect.width;
        verticalLength = myRect.rect.height;
        mySpline = GetComponent<SplineContainer>();
        int knotCount = mySpline.Spline.Count;

        for (int i = 0; i < knotCount; i++)
        {
            BezierKnot knot = mySpline.Spline[i];
            float newX = 0f;
            float newY = 0f;

            if (i == 0)
            {
                newX = horizontalLength * (-0.5f);
                newY = verticalLength * (0.5f);
            }
            else if (i == 1)
            {
                newX = horizontalLength * (-0.5f) / 3;
                newY = verticalLength * (0.5f);
            }
            else if (i == 2)
            {
                newX = horizontalLength * (0.5f) / 3;
                newY = verticalLength * (0.5f);
            }
            else if (i == 3)
            {
                newX = horizontalLength * (0.5f);
                newY = verticalLength * (0.5f);
            }
            else if (i == 4)
            {
                newX = horizontalLength * (0.5f);
                newY = verticalLength * (-0.5f);
            }
            else if (i == 5)
            {
                newX = horizontalLength * (0.5f) / 3;
                newY = verticalLength * (-0.5f);
            }
            else if (i == 6)
            {
                newX = horizontalLength * (-0.5f) / 3;
                newY = verticalLength * (-0.5f);
            }
            else if (i == 7)
            {
                newX = horizontalLength * (-0.5f);
                newY = verticalLength * (-0.5f);
            }

            float3 newPosition = new float3(newX, newY, knot.Position.z);

            knot.Position = newPosition;

            mySpline.Spline.SetKnot(i, knot);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
